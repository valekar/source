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
    public class UploadValidationServices
    {
        public  Business.Upload.GroupMembershipValidationOutput validateGroupMembershipUploadDetails(GroupMembershipValidationInput gmvi, ListGroupMembershipUploadInput ListGroupMembershipUploadInput)
        {

            Mapper.CreateMap<Business.Upload.GroupMembershipValidationInput, Data.Entities.Upload.GroupMembershipValidationInput>();
            Mapper.CreateMap<Business.Upload.GroupMembershipUploadInput, Data.Entities.Upload.GroupMembershipUploadInput>();
            Mapper.CreateMap<Business.Upload.UploadedFileInfo, Data.Entities.Upload.UploadedFileInfo>();
            Mapper.CreateMap<Business.Upload.ListGroupMembershipUploadInput, Data.Entities.Upload.ListGroupMembershipUploadInput>();

            var _input1 = Mapper.Map<Business.Upload.GroupMembershipValidationInput, Data.Entities.Upload.GroupMembershipValidationInput>(gmvi);
            var _input2 = Mapper.Map<Business.Upload.ListGroupMembershipUploadInput, Data.Entities.Upload.ListGroupMembershipUploadInput>(ListGroupMembershipUploadInput);
            var _input3 = Mapper.Map<Business.Upload.UploadedFileInfo, Data.Entities.Upload.UploadedFileInfo>(ListGroupMembershipUploadInput.UploadedFileInputInfo);

            Data.Upload.UploadDetails gd = new Data.Upload.UploadDetails();

            //TPL - Chiranjib 27/06/2016 - Fire all the teaks parallely

           // var _validChapterCodesTask = Task.Factory.StartNew(() => gd.validateChapterCodeDetails(_input1));
            //var _validGroupCodesTask = Task.Factory.StartNew(() => gd.validateGroupCodeDetails(_input1));
           // var _validMasterIdsTask = Task.Factory.StartNew(() => gd.validateMasterIdDetails(_input1));

            var _validChapterCodesTask = Task.Factory.StartNew(() => emptyChapterCodeValidationOutputList());


            if (gmvi._chapterCodes != null || gmvi._chapterCodes.Count != 0)
            {
                if (!gmvi._chapterCodes.All(x => x.Equals("")))
                {
                    _validChapterCodesTask = Task.Factory.StartNew(() => gd.validateChapterCodeDetails(_input1));
                }
            }

            var _validGroupCodesTask = Task.Factory.StartNew(() => validateGroupCodeDetailsList());

            if (gmvi._groupCodes != null || gmvi._groupCodes.Count != 0)
            {
                if (!gmvi._groupCodes.All(x => x.Equals("")))
                {
                    _validGroupCodesTask = Task.Factory.StartNew(() => gd.validateGroupCodeDetails(_input1));
                }
            }

            var _validMasterIdsTask = Task.Factory.StartNew(() => emptyValidateMasterIdDetails());
            if (gmvi._masterIds != null || gmvi._masterIds.Count != 0)
            {
                if (!gmvi._masterIds.All(x => x.Equals("")))
                {
                    _validMasterIdsTask = Task.Factory.StartNew(() => gd.validateMasterIdDetails(_input1));
                }
            }
            
            
           

            //Wait for all of them to complete

            Task.WaitAll(_validChapterCodesTask, _validGroupCodesTask, _validMasterIdsTask);

            //Define the business logic here in the services(Keep the DAL clean)

            //Get the computed results from each of the task

            var _validChapterCodes = _validChapterCodesTask.Result;

            var _validGroupCodes = _validGroupCodesTask.Result;

            var _validMasterIds = _validMasterIdsTask.Result;

            //Form the output object

            Data.Entities.Upload.GroupMembershipValidationOutput gmvo = new Data.Entities.Upload.GroupMembershipValidationOutput();

            if (_validChapterCodes.Result != null)
                gmvo._invalidChapterCodes = gmvi._chapterCodes.Where(x => !_validChapterCodes.Result.Select(y => y.chpt_cd).Contains(x)).ToList();
            else
                gmvo._invalidChapterCodes = gmvi._chapterCodes;

            if (_validGroupCodes.Result != null)
                gmvo._invalidGroupCodes = gmvi._groupCodes.Where(x => !_validGroupCodes.Result.Select(y => y.grp_cd).Contains(x)).ToList();
            else
                gmvo._invalidGroupCodes = gmvi._groupCodes;

            if (_validMasterIds.Result != null)
                gmvo._invalidMasterIds = gmvi._masterIds.Where(x => !string.IsNullOrEmpty(x) && !_validMasterIds.Result.Select(y => y).Contains(x)).ToList();
            else
                gmvo._invalidMasterIds = gmvi._masterIds;

            Data.Entities.Upload.ListGroupMembershipUploadInput lgl = new Data.Entities.Upload.ListGroupMembershipUploadInput();

            //Check if any of the invalid lists has a positive count 

            if (gmvo._invalidChapterCodes.Count != 0 || gmvo._invalidGroupCodes.Count != 0 || gmvo._invalidMasterIds.Count != 0)
            {
                lgl.GroupMembershipUploadInputList = new List<Data.Entities.Upload.GroupMembershipUploadInput>();
                var _result = _input2.GroupMembershipUploadInputList
                                .Where(x => gmvo._invalidMasterIds.Select(y => y).Contains(x.cnst_mstr_id) ||
                                            gmvo._invalidChapterCodes.Select(y => y).Contains(x.chpt_cd) ||
                                            gmvo._invalidGroupCodes.Select(y => y).Contains(x.grp_cd)).ToList();

                if (_result.Count > 0)
                {
                    //If yes, for and return the incoming list as the invalid one to display the erroneous records

                    if (_result.Count > 100)
                    {
                        lgl.GroupMembershipUploadInputList = _result.Take(100).ToList();

                        gmvo.GroupMembershipInvalidList = lgl;
                        gmvo.GroupMembershipValidList = new Data.Entities.Upload.ListGroupMembershipUploadInput
                        {
                            GroupMembershipUploadInputList = new List<Data.Entities.Upload.GroupMembershipUploadInput>()
                        };

                        gmvo.UploadedFileOutputInfo = new Data.Entities.Upload.UploadedFileInfo()
                        {
                            fileExtension = "",
                            fileName = "",
                            intFileSize = 0
                        };
                    }
                    else
                    {
                        lgl.GroupMembershipUploadInputList = _result;

                        gmvo.GroupMembershipInvalidList = lgl;
                        gmvo.GroupMembershipValidList = new Data.Entities.Upload.ListGroupMembershipUploadInput
                        {
                            GroupMembershipUploadInputList = new List<Data.Entities.Upload.GroupMembershipUploadInput>()
                        };

                        gmvo.UploadedFileOutputInfo = new Data.Entities.Upload.UploadedFileInfo()
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

                lgl.GroupMembershipUploadInputList = new List<Data.Entities.Upload.GroupMembershipUploadInput>();
                gmvo.GroupMembershipInvalidList = lgl;
                gmvo.GroupMembershipValidList = _input2;
                foreach (var input in gmvo.GroupMembershipValidList.GroupMembershipUploadInputList)
                {
                    var chapter = _validChapterCodes.Result.FirstOrDefault(e =>
                        e.chpt_cd == input.chpt_cd);
                    if (chapter != null) input.appl_src_cd = chapter.appl_src_cd;
                }
                gmvo.UploadedFileOutputInfo = new Data.Entities.Upload.UploadedFileInfo()
                {
                    fileExtension = gmvi.uploadedFileExtension,
                    fileName = gmvi.uploadedFileName,
                    intFileSize = gmvi.uploadedFileSize
                };
            }

            Mapper.CreateMap<Data.Entities.Upload.GroupMembershipValidationOutput, Business.Upload.GroupMembershipValidationOutput>();
            Mapper.CreateMap<Data.Entities.Upload.ChapterCodeValidationOutput, Business.Upload.ChapterCodeValidationOutput>();
            Mapper.CreateMap<Data.Entities.Upload.GroupCodeValidationOutput, Business.Upload.GroupCodeValidationOutput>();
            Mapper.CreateMap<Data.Entities.Upload.GroupMembershipUploadInput, Business.Upload.GroupMembershipUploadInput>();
            Mapper.CreateMap<Data.Entities.Upload.UploadedFileInfo, Business.Upload.UploadedFileInfo>();
            Mapper.CreateMap<Data.Entities.Upload.ListGroupMembershipUploadInput, Business.Upload.ListGroupMembershipUploadInput>();

            var result = Mapper.Map<Data.Entities.Upload.GroupMembershipValidationOutput, Business.Upload.GroupMembershipValidationOutput>(gmvo);
            
            return result;
        }


        private async Task<IList<Data.Entities.Upload.ChapterCodeValidationOutput>> emptyChapterCodeValidationOutputList()
        {
            return new List<Data.Entities.Upload.ChapterCodeValidationOutput>();
        }

        private async Task<IList<Data.Entities.Upload.GroupCodeValidationOutput>> validateGroupCodeDetailsList()
        {
            return new List<Data.Entities.Upload.GroupCodeValidationOutput>();
        }


        private async Task<IList<string>> emptyValidateMasterIdDetails()
        {
            return new List<string>();
        }

        public Business.Upload.GroupMembershipSubmitOutput uploadGroupMembershipDetails(GroupMembershipUploadDetails gm)
        {         
            Mapper.CreateMap<Business.Upload.GroupMembershipUploadInput, Data.Entities.Upload.GroupMembershipUploadInput>();
            Mapper.CreateMap<Business.Upload.UploadedFileInfo, Data.Entities.Upload.UploadedFileInfo>();
            Mapper.CreateMap<Business.Upload.ListGroupMembershipUploadInput, Data.Entities.Upload.ListGroupMembershipUploadInput>();
            Mapper.CreateMap<Business.Upload.ChapterUploadFileDetailsHelper, Data.Entities.Upload.ChapterUploadFileDetailsHelper>();
            Mapper.CreateMap<Business.Upload.GroupMembershipUploadDetails, Data.Entities.Upload.GroupMembershipUploadDetails>();

            var input = Mapper.Map<Business.Upload.GroupMembershipUploadDetails, Data.Entities.Upload.GroupMembershipUploadDetails>(gm);

            Data.Upload.UploadDetails gd = new Data.Upload.UploadDetails();

            var serializer = new JavaScriptSerializer();

            var trans_key = gd.getGroupMembershipTransKeyDetails(input);

            var grpMembershipSubmitOutput = computeUploadGroupMembershipDetails(gm,trans_key);

           // writeSerializedUploadJsonData(grpMembershipSubmitOutput,gm.userEmailId);

            return grpMembershipSubmitOutput;
        }

        //Moved uploadGroupMembershipDetails to service
        public Business.Upload.GroupMembershipSubmitOutput computeUploadGroupMembershipDetails(GroupMembershipUploadDetails gm, long trans_key)
        {

            Mapper.CreateMap<Business.Upload.GroupMembershipUploadInput, Data.Entities.Upload.GroupMembershipUploadInput>();
            Mapper.CreateMap<Business.Upload.UploadedFileInfo, Data.Entities.Upload.UploadedFileInfo>();
            Mapper.CreateMap<Business.Upload.ListGroupMembershipUploadInput, Data.Entities.Upload.ListGroupMembershipUploadInput>();
            Mapper.CreateMap<Business.Upload.ChapterUploadFileDetailsHelper, Data.Entities.Upload.ChapterUploadFileDetailsHelper>();
            Mapper.CreateMap<Business.Upload.GroupMembershipUploadDetails, Data.Entities.Upload.GroupMembershipUploadDetails>();

            var input = Mapper.Map<Business.Upload.GroupMembershipUploadDetails, Data.Entities.Upload.GroupMembershipUploadDetails>(gm);

            Data.Upload.UploadDetails gd = new Data.Upload.UploadDetails();

            try
            {

                var Max_seq_key = gd.getGroupMembershipSeqKeyDetails();
                var Max_grp_seq_key = gd.getGroupMembershipGrpSeqKeyDetails();

                //assign the initial value
                var Lst_com_unit_key = new List<Data.Entities.Upload.ComUnitKeyOutput>()
                    {
                        new Data.Entities.Upload.ComUnitKeyOutput() {
                            nk_ecode = string.Empty,
                            unit_key = -1                  
                        }
                    };

                if (gm != null || gm.ListGroupMembershipUploadDetailsInput.GroupMembershipUploadInputList.Count != 0)
                {
                    Lst_com_unit_key = gd.getGroupMembershipUnitKeyDetails(input);
                }
               // var Lst_com_unit_key = gd.getGroupMembershipUnitKeyDetails(input);

                //Create the output object
                Data.Entities.Upload.GroupMembershipSubmitOutput go = new Data.Entities.Upload.GroupMembershipSubmitOutput();

                Mapper.CreateMap<Data.Entities.Upload.GroupMembershipSubmitOutput , Business.Upload.GroupMembershipSubmitOutput>();

                var output = Mapper.Map<Data.Entities.Upload.GroupMembershipSubmitOutput, Business.Upload.GroupMembershipSubmitOutput>(go);
                output.insertFlag = 1;
                output.insertOutput = "Success";
                output.transactionKey = trans_key;
                output.ListChapterUploadFileDetailsInput = gm.ListChapterUploadFileDetailsInput;

                foreach (var grpMem in input.ListGroupMembershipUploadDetailsInput.GroupMembershipUploadInputList)
                {
                    var incr_max_key = Interlocked.Increment(ref Max_seq_key); //To make the variable thread safe across multiple parallel calls
                    var incr_max_grp_seq_key = Interlocked.Increment(ref Max_grp_seq_key);
                    try
                    {
                       // Task.Run(() => gd.insertGroupMembershipUploadDetails(input.ListGroupMembershipUploadDetailsInput.GroupMembershipUploadInputList[i], 
                                                        //input.ListChapterUploadFileDetailsInput, ++Max_seq_key, ++Max_grp_seq_key, Lst_com_unit_key, trans_key));
                        Task.Run(() => gd.insertGroupMembershipUploadDetails(grpMem,
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
                GroupMembershipSubmitOutput go = new GroupMembershipSubmitOutput();
                go.insertFlag = 2;
                go.insertOutput = e.Message;
                go.ListChapterUploadFileDetailsInput = gm.ListChapterUploadFileDetailsInput;
                go.transactionKey = trans_key;

                //Append the error info and return the output object to the service
                return go;
            }
        }

        public void writeSerializedUploadJsonData(Business.Upload.GroupMembershipSubmitOutput gso,string emailId)
        {
            var serializer = new JavaScriptSerializer();

            List<ChapterUploadFileDetailsHelper> gm = gso.ListChapterUploadFileDetailsInput;
            long trans_key = gso.transactionKey;
            string uploadedUserDomain = (System.Configuration.ConfigurationManager.AppSettings["UploadedUserDomain"] ?? "").ToString();

            if (gm != null && gm.Count > 0)
            {
                if (gso.insertFlag == 1)
                {
                   
                    gm[gm.Count - 1].UploadStatus = "Success"; // set as pass
                    gm[gm.Count - 1].UploadEnd = DateTime.Now.ToString();
                    gm[gm.Count - 1].TransactionKey = trans_key.ToString();
                    
                    string strEmailMessage = "The File, " + gm[gm.Count - 1].FileName.ToString() + " has been uploaded to the server successfully!. Transaction key generated for this upload is " + trans_key + ". You will receive another email in the next 24 hours regarding the status of the uploaded data migration.";

                    sendUploadStatusMail(strEmailMessage, gm[gm.Count - 1].UserEmail.ToString());

                    //Write details to JSON
                    var serializedResult = serializer.Serialize(gm);
                    string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));
                }

                else if (gso.insertFlag == 0)
                {
                    gm[gm.Count - 1].UploadStatus = "Failure"; // set as pass
                    gm[gm.Count - 1].UploadEnd = DateTime.Now.ToString();
                    gm[gm.Count - 1].TransactionKey = trans_key.ToString();

                    string strEmailMessage = "The File, " + gm[gm.Count - 1].FileName.ToString() + " has failed to upload to the server.";
                    sendUploadStatusMail(strEmailMessage, gm[gm.Count - 1].UserEmail.ToString());

                    //Write details to JSON
                    var serializedResult = serializer.Serialize(gm);
                    string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));
                }
                else if (gso.insertFlag == 2)
                {
                    gm[gm.Count - 1].UploadStatus = "Failure"; // set as pass
                    gm[gm.Count - 1].UploadEnd = DateTime.Now.ToString();
                    gm[gm.Count - 1].TransactionKey = trans_key.ToString();

                    string  strEmailMessage = "The File, " +  gm[gm.Count - 1].FileName.ToString() + " has failed to upload to the server.";
                    sendUploadStatusMail(strEmailMessage, gm[gm.Count - 1].UserEmail.ToString());

                    //Write details to JSON
                    var serializedResult = serializer.Serialize(gm);
                    string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));

                }
            }
        }

        public void sendUploadStatusMail (string strEmailMessage, string loggedInUser)
        {
                        //Send email to the uploader
            string fromAddress, ccAddress, bccAddress, ToAddress;
            if (ConfigurationManager.AppSettings["FromEmail"] != null)
                fromAddress = ConfigurationManager.AppSettings["FromEmail"].ToString();
            else
                fromAddress = "";

            if (ConfigurationManager.AppSettings["ToAddress"] != null)
                ToAddress = ConfigurationManager.AppSettings["ToAddress"].ToString();
            else
                ToAddress = "";

            if (ConfigurationManager.AppSettings["CCEmail"] != null)
                ccAddress = ConfigurationManager.AppSettings["CCEmail"].ToString();
            else
                ccAddress = "";

            if (ConfigurationManager.AppSettings["BCCEmail"] != null)
                bccAddress = ConfigurationManager.AppSettings["BCCEmail"].ToString();
            else
                bccAddress = "";

            var sendMail = new Mail();
            sendMail.ToAddress = loggedInUser;//Changed Email Address hard coded to configurable from Web config(Only domain name will take from config)
            sendMail.FromAddress = fromAddress;
            sendMail.ccAddress = ccAddress;
            sendMail.bccAddress = bccAddress;
            sendMail.Subject = "Group Membership Upload Status";
            sendMail.Body = strEmailMessage;

            sendMail.sendMail();
        }
    }
}
