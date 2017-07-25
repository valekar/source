using ARC.Donor.Business.Upload;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ARC.Donor.Service.Upload
{
    public class EmailOnlyUploadValidationServices
    {
        public Business.Upload.EmailOnlyUploadValidationOutput validateEmailOnlyUploadDetails(EmailOnlyUploadValidationInput emvi, ListEmailOnlyUploadInput listEmailOnlyUploadInput)
        {

            Mapper.CreateMap<Business.Upload.EmailOnlyUploadValidationInput, Data.Entities.Upload.EmailOnlyUploadValidationInput>();
            Mapper.CreateMap<Business.Upload.EmailOnlyUploadInput, Data.Entities.Upload.EmailOnlyUploadInput>();
            Mapper.CreateMap<Business.Upload.UploadedEmailFileInfo, Data.Entities.Upload.UploadedEmailFileInfo>();
            Mapper.CreateMap<Business.Upload.ListEmailOnlyUploadInput, Data.Entities.Upload.ListEmailOnlyUploadInput>();

            var _input1 = Mapper.Map<Business.Upload.EmailOnlyUploadValidationInput, Data.Entities.Upload.EmailOnlyUploadValidationInput>(emvi);
            var _input2 = Mapper.Map<Business.Upload.ListEmailOnlyUploadInput, Data.Entities.Upload.ListEmailOnlyUploadInput>(listEmailOnlyUploadInput);
            var _input3 = Mapper.Map<Business.Upload.UploadedEmailFileInfo, Data.Entities.Upload.UploadedEmailFileInfo>(listEmailOnlyUploadInput.UploadedEmailFileInputInfo);


            Data.Upload.EmailOnlyUploadDetailsData gd = new Data.Upload.EmailOnlyUploadDetailsData();

            //TPL - Chiranjib 27/06/2016 - Fire all the teaks parallely

            var _validChapterCodesTask = Task.Factory.StartNew(() => emptyChapterCodeValidationOutputList());


            if (emvi._chapterCodes != null || emvi._chapterCodes.Count != 0)
            {
                if (!emvi._chapterCodes.All(x => x.Equals("")))
                {
                    _validChapterCodesTask = Task.Factory.StartNew(() => gd.validateChapterCodeDetails(_input1));
                }
            }

            var _validGroupCodesTask = Task.Factory.StartNew(() => validateGroupCodeDetailsList());

            if (emvi._groupCodes != null || emvi._groupCodes.Count != 0)
            {
                if (!emvi._groupCodes.All(x => x.Equals("")))
                {
                    _validGroupCodesTask = Task.Factory.StartNew(() => gd.validateGroupCodeDetails(_input1));
                }
            }
             
           

            //Wait for all of them to complete

            Task.WaitAll(_validChapterCodesTask, _validGroupCodesTask);

            //Define the business logic here in the services(Keep the DAL clean)

            //Get the computed results from each of the task

            var _validChapterCodes = _validChapterCodesTask.Result;

            var _validGroupCodes = _validGroupCodesTask.Result;


            //Form the output object

            Data.Entities.Upload.EmailOnlyUploadValidationOutput gmvo = new Data.Entities.Upload.EmailOnlyUploadValidationOutput();

            if (_validChapterCodes.Result != null)
                gmvo._invalidChapterCodes = emvi._chapterCodes.Where(x => !_validChapterCodes.Result.Select(y => y.chpt_cd).Contains(x)).ToList();
            else
                gmvo._invalidChapterCodes = emvi._chapterCodes;

            if (_validGroupCodes.Result != null)
                gmvo._invalidGroupCodes = emvi._groupCodes.Where(x => !_validGroupCodes.Result.Select(y => y.grp_cd).Contains(x)).ToList();
            else 
                gmvo._invalidGroupCodes = emvi._groupCodes;


            if (_validGroupCodes.Result != null || _validChapterCodes.Result != null)
            {

                gmvo._invalidEmailAddresses = emvi._emailAddress.Where(x => ARC.Utility.ValidateExtensions.IsInValidEmailAddress(x)).ToList();
            }

            Data.Entities.Upload.ListEmailOnlyUploadInput lgl = new Data.Entities.Upload.ListEmailOnlyUploadInput();

            //Check if any of the invalid lists has a positive count 

            if (gmvo._invalidChapterCodes.Count != 0 || gmvo._invalidGroupCodes.Count != 0 || gmvo._invalidEmailAddresses.Count !=0)
            {
                lgl.EmailOnlyUploadInputList = new List<Data.Entities.Upload.EmailOnlyUploadInput>();
                var _result = _input2.EmailOnlyUploadInputList
                                .Where(x => 
                                            gmvo._invalidChapterCodes.Select(y => y).Contains(x.chpt_cd) ||
                                            gmvo._invalidGroupCodes.Select(y => y).Contains(x.grp_cd) ||
                                            gmvo._invalidEmailAddresses.Select(y => y).Contains(x.cnst_email_addr)).ToList();

                if (_result.Count > 0)
                {
                    //If yes, for and return the incoming list as the invalid one to display the erroneous records

                    if (_result.Count > 100)
                    {
                        lgl.EmailOnlyUploadInputList = _result.Take(100).ToList();

                        gmvo.EmailUploadInvalidList = lgl;
                        gmvo.EmailUploadValidList = new Data.Entities.Upload.ListEmailOnlyUploadInput
                        {
                            EmailOnlyUploadInputList = new List<Data.Entities.Upload.EmailOnlyUploadInput>()
                        };

                        gmvo.UploadedEmailFileOutputInfo = new Data.Entities.Upload.UploadedEmailFileInfo()
                        {
                            fileExtension = "",
                            fileName = "",
                            intFileSize = 0
                        };
                    }
                    else
                    {
                        lgl.EmailOnlyUploadInputList = _result;

                        gmvo.EmailUploadInvalidList = lgl;
                        gmvo.EmailUploadValidList = new Data.Entities.Upload.ListEmailOnlyUploadInput
                        {
                            EmailOnlyUploadInputList = new List<Data.Entities.Upload.EmailOnlyUploadInput>()
                        };

                        gmvo.UploadedEmailFileOutputInfo = new Data.Entities.Upload.UploadedEmailFileInfo()
                        {
                            fileExtension = "",
                            fileName = "",
                            intFileSize = 0
                        };
                    }
                }


            }
            else
            {
                //If no , incoming list should be returned as a valid one

                lgl.EmailOnlyUploadInputList = new List<Data.Entities.Upload.EmailOnlyUploadInput>();
                gmvo.EmailUploadInvalidList = lgl;
                gmvo.EmailUploadValidList = _input2;
                foreach (var input in gmvo.EmailUploadValidList.EmailOnlyUploadInputList)
                {
                    var chapter = _validChapterCodes.Result.FirstOrDefault(e =>
                        e.chpt_cd == input.chpt_cd);
                    if (chapter != null) input.appl_src_cd = chapter.appl_src_cd;
                }
                gmvo.UploadedEmailFileOutputInfo = new Data.Entities.Upload.UploadedEmailFileInfo()
                {
                    fileExtension = emvi.uploadedFileExtension,
                    fileName = emvi.uploadedFileName,
                    intFileSize = emvi.uploadedFileSize
                };
            }

            Mapper.CreateMap<Data.Entities.Upload.EmailOnlyUploadValidationOutput, Business.Upload.EmailOnlyUploadValidationOutput>();
            Mapper.CreateMap<Data.Entities.Upload.ChapterCodeValidationOutput, Business.Upload.ChapterCodeValidationOutput>();
            Mapper.CreateMap<Data.Entities.Upload.GroupCodeValidationOutput, Business.Upload.GroupCodeValidationOutput>();
            Mapper.CreateMap<Data.Entities.Upload.EmailOnlyUploadInput, Business.Upload.EmailOnlyUploadInput>();
            Mapper.CreateMap<Data.Entities.Upload.UploadedEmailFileInfo, Business.Upload.UploadedEmailFileInfo>();
            Mapper.CreateMap<Data.Entities.Upload.ListEmailOnlyUploadInput, Business.Upload.ListEmailOnlyUploadInput>();

            var result = Mapper.Map<Data.Entities.Upload.EmailOnlyUploadValidationOutput, Business.Upload.EmailOnlyUploadValidationOutput>(gmvo);

            return result;
        }


        private async Task<IList<Data.Entities.Upload.ChapterCodeValidationOutput>> emptyChapterCodeValidationOutputList()
        {
            return  new List<Data.Entities.Upload.ChapterCodeValidationOutput>();
        }

        private async Task<IList<Data.Entities.Upload.GroupCodeValidationOutput>> validateGroupCodeDetailsList()
        {
            return new List<Data.Entities.Upload.GroupCodeValidationOutput>();
        }

        public Business.Upload.EmailOnlyUploadSubmitOutput PostEmaiOnlyUploadData(EmailOnlyUploadDetails emailOnlyUploadDetails)
        {
            Mapper.CreateMap<Business.Upload.EmailOnlyUploadInput, Data.Entities.Upload.EmailOnlyUploadInput>();
            Mapper.CreateMap<Business.Upload.UploadedEmailFileInfo, Data.Entities.Upload.UploadedEmailFileInfo>();
            Mapper.CreateMap<Business.Upload.ListEmailOnlyUploadInput, Data.Entities.Upload.ListEmailOnlyUploadInput>();
            Mapper.CreateMap<Business.Upload.ChapterUploadFileDetailsHelper, Data.Entities.Upload.ChapterUploadFileDetailsHelper>();
            Mapper.CreateMap<Business.Upload.EmailOnlyUploadDetails, Data.Entities.Upload.EmailOnlyUploadDetails>();

            var input = Mapper.Map<Business.Upload.ChapterUploadFileDetailsHelper, Data.Entities.Upload.ChapterUploadFileDetailsHelper>(emailOnlyUploadDetails.ListChapterUploadFileDetailsInput[emailOnlyUploadDetails.ListChapterUploadFileDetailsInput.Count - 1]);

            Data.Upload.EmailOnlyUploadDetailsData gd = new Data.Upload.EmailOnlyUploadDetailsData();

            var trans_key = gd.getEmailOnlyUploadTransKeyDetails(input.UserName);

            var emailOnlyUploadSubmitOutput = computeUploadEmailOnlyDetails(emailOnlyUploadDetails, trans_key);

            //writeSerializedUploadJsonData(emailOnlyUploadSubmitOutput,emailOnlyUploadDetails.userEmailId);

            return emailOnlyUploadSubmitOutput;
        }

        //Moved uploadGroupMembershipDetails to service
        public Business.Upload.EmailOnlyUploadSubmitOutput computeUploadEmailOnlyDetails(EmailOnlyUploadDetails gm, long trans_key)
        {

            Mapper.CreateMap<Business.Upload.EmailOnlyUploadInput, Data.Entities.Upload.EmailOnlyUploadInput>();
            Mapper.CreateMap<Business.Upload.UploadedEmailFileInfo, Data.Entities.Upload.UploadedEmailFileInfo>();
            Mapper.CreateMap<Business.Upload.ListEmailOnlyUploadInput, Data.Entities.Upload.ListEmailOnlyUploadInput>();
            Mapper.CreateMap<Business.Upload.ChapterUploadFileDetailsHelper, Data.Entities.Upload.ChapterUploadFileDetailsHelper>();
            Mapper.CreateMap<Business.Upload.EmailOnlyUploadDetails, Data.Entities.Upload.EmailOnlyUploadDetails>();
            Mapper.CreateMap<Business.Upload.ComUnitKeyOutput, Data.Entities.Upload.ComUnitKeyOutput>();
            Mapper.CreateMap<Data.Entities.Upload.ComUnitKeyOutput,Business.Upload.ComUnitKeyOutput>();
            var input = Mapper.Map<Business.Upload.EmailOnlyUploadDetails, Data.Entities.Upload.EmailOnlyUploadDetails>(gm);

            Data.Upload.EmailOnlyUploadDetailsData gd = new Data.Upload.EmailOnlyUploadDetailsData();

            try
            {

                var Max_seq_key = gd.getEmailOnlyUploadSeqKeyDetails();
                var Max_grp_seq_key = gd.getEmailOnlyUploadGrpSeqKeyDetails();

                //assign the initial value
                var Lst_com_unit_key = new List<Data.Entities.Upload.ComUnitKeyOutput>()
                    {
                        new Data.Entities.Upload.ComUnitKeyOutput() {
                            nk_ecode = string.Empty,
                            unit_key = -1                  
                        }
                    };

                if (gm != null || gm.ListEmailOnlyUploadDetails.EmailOnlyUploadInputList.Count != 0)
                {
                    Lst_com_unit_key = gd.getEmailOnlyUploadUnitKeyDetails(input);
                }

                Data.Entities.Upload.EmailOnlyUploadSubmitOutput go = new Data.Entities.Upload.EmailOnlyUploadSubmitOutput();

                Mapper.CreateMap<Data.Entities.Upload.EmailOnlyUploadSubmitOutput, Business.Upload.EmailOnlyUploadSubmitOutput>();

                var output = Mapper.Map<Data.Entities.Upload.EmailOnlyUploadSubmitOutput, Business.Upload.EmailOnlyUploadSubmitOutput>(go);
                output.insertFlag = 1;
                output.insertOutput = "Success";
                output.transactionKey = trans_key;
                output.ListChapterUploadFileDetailsInput = gm.ListChapterUploadFileDetailsInput;

                foreach (var emailUploadItem in input.ListEmailOnlyUploadDetails.EmailOnlyUploadInputList)
                {
                    var incr_max_key = Interlocked.Increment(ref Max_seq_key); //To make the variable thread safe across multiple parallel calls
                    var incr_max_grp_seq_key = Interlocked.Increment(ref Max_grp_seq_key);
                    try
                    {
                        Task.Run(() => gd.insertEmailOnlyUploadDetails(emailUploadItem,
                                                        input.ListChapterUploadFileDetailsInput, incr_max_key, incr_max_grp_seq_key, Lst_com_unit_key, trans_key));
                    }
                    catch
                    {
                        output.insertFlag = 0;
                        output.insertOutput = "Failure";
                        output.transactionKey = trans_key;
                        output.ListChapterUploadFileDetailsInput = gm.ListChapterUploadFileDetailsInput;

                        break;
                    }
                }

                //Return the output object to the service
                return output;
            }
            catch (Exception e)
            {
                EmailOnlyUploadSubmitOutput go = new EmailOnlyUploadSubmitOutput();
                go.insertFlag = 2;
                go.insertOutput = e.Message;
                go.ListChapterUploadFileDetailsInput = gm.ListChapterUploadFileDetailsInput;
                go.transactionKey = trans_key;

                //Append the error info and return the output object to the service
                return go;
            }
        }

        //public void writeSerializedUploadJsonData(Business.Upload.EmailOnlyUploadSubmitOutput gso,string emailId)
        //{
        //    var serializer = new JavaScriptSerializer();

        //    List<EmailUploadJsonFileDetailsHelper> gm = gso.ListChapterUploadFileDetailsInput;
        //    long trans_key = gso.transactionKey;
        //    //string uploadedUserDomain = (System.Configuration.ConfigurationManager.AppSettings["UploadedUserDomain"] ?? "").ToString();

        //    if (gm != null && gm.Count > 0)
        //    {
        //        if (gso.insertFlag == 1)
        //        {

        //            gm[gm.Count - 1].UploadStatus = "Success"; // set as pass
        //            gm[gm.Count - 1].UploadEnd = DateTime.Now.ToString();
        //            gm[gm.Count - 1].TransactionKey = trans_key.ToString();

        //            string strEmailMessage = "The File, " + gm[gm.Count - 1].FileName.ToString() + " has been uploaded to the server successfully!. Transaction key generated for this upload is " + trans_key + ". You will receive another email in the next 24 hours regarding the status of the uploaded data migration.";

        //            sendUploadStatusMail(strEmailMessage, gm[gm.Count - 1].UserEmail.ToString());

        //            //Write details to JSON
        //            var serializedResult = serializer.Serialize(gm);
        //            string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

        //            if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
        //                System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));
        //        }

        //        else if (gso.insertFlag == 0)
        //        {
        //            gm[gm.Count - 1].UploadStatus = "Failure"; // set as pass
        //            gm[gm.Count - 1].UploadEnd = DateTime.Now.ToString();
        //            gm[gm.Count - 1].TransactionKey = trans_key.ToString();

        //            string strEmailMessage = "The File, " + gm[gm.Count - 1].FileName.ToString() + " has failed to upload to the server.";
        //            sendUploadStatusMail(strEmailMessage, gm[gm.Count - 1].UserEmail.ToString());

        //            //Write details to JSON
        //            var serializedResult = serializer.Serialize(gm);
        //            string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

        //            if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
        //                System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));
        //        }
        //        else if (gso.insertFlag == 2)
        //        {
        //            gm[gm.Count - 1].UploadStatus = "Failure"; // set as pass
        //            gm[gm.Count - 1].UploadEnd = DateTime.Now.ToString();
        //            gm[gm.Count - 1].TransactionKey = trans_key.ToString();

        //            string strEmailMessage = "The File, " + gm[gm.Count - 1].FileName.ToString() + " has failed to upload to the server.";
        //            sendUploadStatusMail(strEmailMessage, gm[gm.Count - 1].UserEmail.ToString());

        //            //Write details to JSON
        //            var serializedResult = serializer.Serialize(gm);
        //            string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

        //            if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
        //                System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));

        //        }
        //    }
        //}

        //public void sendUploadStatusMail(string strEmailMessage, string loggedInUser)
        //{
        //    //Send email to the uploader
        //    string fromAddress, ccAddress, bccAddress, ToAddress;
        //    if (ConfigurationManager.AppSettings["FromEmail"] != null)
        //        fromAddress = ConfigurationManager.AppSettings["FromEmail"].ToString();
        //    else
        //        fromAddress = "";

        //    if (ConfigurationManager.AppSettings["ToAddress"] != null)
        //        ToAddress = ConfigurationManager.AppSettings["ToAddress"].ToString();
        //    else
        //        ToAddress = "";

        //    if (ConfigurationManager.AppSettings["CCEmail"] != null)
        //        ccAddress = ConfigurationManager.AppSettings["CCEmail"].ToString();
        //    else
        //        ccAddress = "";

        //    if (ConfigurationManager.AppSettings["BCCEmail"] != null)
        //        bccAddress = ConfigurationManager.AppSettings["BCCEmail"].ToString();
        //    else
        //        bccAddress = "";

        //    var sendMail = new Mail();
        //    sendMail.ToAddress = loggedInUser; //Changed Email Address hard coded to configurable from Web config(Only domain name will take from config)
        //    sendMail.FromAddress = fromAddress;
        //    sendMail.ccAddress = ccAddress;
        //    sendMail.bccAddress = bccAddress;
        //    sendMail.Subject = "Email-only Upload Status";
        //    sendMail.Body = strEmailMessage;

        //    sendMail.sendMail();
        //}


    }
}
