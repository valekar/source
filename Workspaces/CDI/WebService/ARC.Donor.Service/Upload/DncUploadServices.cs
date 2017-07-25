using ARC.Donor.Business.Upload;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
namespace ARC.Donor.Service.Upload
{
    public class DncUploadServices
    {
        public Business.Upload.DncSubmitOutput dncUploadData(DncUploadDetails dncUploadDetails)
        {
            Mapper.CreateMap<Business.Upload.DncUploadParams, Data.Entities.Upload.DncUploadParams>();
            Mapper.CreateMap<Business.Upload.DncUploadedFileInfo, Data.Entities.Upload.DncUploadedFileInfo>();
            Mapper.CreateMap<Business.Upload.ListDncUploadInput, Data.Entities.Upload.ListDncUploadInput>();
            Mapper.CreateMap<Business.Upload.ChapterUploadFileDetailsHelper, Data.Entities.Upload.ChapterUploadFileDetailsHelper>();
            Mapper.CreateMap<Business.Upload.DncUploadDetails, Data.Entities.Upload.DncUploadDetails>();

            var input = Mapper.Map<Business.Upload.ChapterUploadFileDetailsHelper, Data.Entities.Upload.ChapterUploadFileDetailsHelper>
                (dncUploadDetails.ListChapterUploadFileDetailsInput[dncUploadDetails.ListChapterUploadFileDetailsInput.Count - 1]);

            Data.Upload.DncUploadDetails gd = new Data.Upload.DncUploadDetails();

            long trans_key = gd.getDncUploadTransKeyDetails(input.UserName);

            var dncUploadSubmitOutput = computeDncUploadDetails(dncUploadDetails, trans_key);

          //  writeSerializedUploadJsonData(dncUploadSubmitOutput, dncUploadDetails.userEmailId);

            return dncUploadSubmitOutput;
        }



        private DncSubmitOutput computeDncUploadDetails(DncUploadDetails dncUploadDetails,long trans_key)
        {
            Mapper.CreateMap<Business.Upload.DncUploadParams, Data.Entities.Upload.DncUploadParams>();
            Mapper.CreateMap<Business.Upload.DncUploadedFileInfo, Data.Entities.Upload.DncUploadedFileInfo>();
            Mapper.CreateMap<Business.Upload.ListDncUploadInput, Data.Entities.Upload.ListDncUploadInput>();
            Mapper.CreateMap<Business.Upload.ChapterUploadFileDetailsHelper, Data.Entities.Upload.ChapterUploadFileDetailsHelper>();
            Mapper.CreateMap<Business.Upload.DncUploadDetails, Data.Entities.Upload.DncUploadDetails>();

            Data.Upload.DncUploadDetails gd = new Data.Upload.DncUploadDetails();

            try
            {
                var max_seq_key = gd.getDncSeqKeyDetails();
                var input = Mapper.Map<Business.Upload.DncUploadDetails, Data.Entities.Upload.DncUploadDetails>(dncUploadDetails);
               

                //Create the output object
                Data.Entities.Upload.DncSubmitOutput go = new Data.Entities.Upload.DncSubmitOutput();

                Mapper.CreateMap<Data.Entities.Upload.DncSubmitOutput, Business.Upload.DncSubmitOutput>();

                var output = Mapper.Map<Data.Entities.Upload.DncSubmitOutput, Business.Upload.DncSubmitOutput>(go);
                output.insertFlag = 1;
                output.insertOutput = "Success";
                output.transactionKey = trans_key;
                output.ListChapterUploadFileDetailsInput = dncUploadDetails.ListChapterUploadFileDetailsInput;


                foreach (var dncUploadItem in input.ListDncUploadInput.dncUploadInputList)
                {

                   try
                    {
                        if (!(string.IsNullOrEmpty(dncUploadItem.arc_srcsys_cd) && string.IsNullOrEmpty(dncUploadItem.cnst_srcsys_id))
                            || !(string.IsNullOrEmpty(dncUploadItem.init_mstr_id)))
                        {
                            dncUploadItem.dnc_type = "specific_master";
                        }
                        else
                        {
                            dncUploadItem.dnc_type = "locator_level";
                        }

                        var incr_max_key = Interlocked.Increment(ref max_seq_key);

                        Task.Run(() => gd.insertDncUploadDetails(dncUploadItem, input.ListChapterUploadFileDetailsInput[input.ListChapterUploadFileDetailsInput.Count - 1].UserName, trans_key, incr_max_key));
                    }
                    catch
                    {
                        output.insertFlag = 0;
                        output.insertOutput = "Failure";
                        output.transactionKey = trans_key;
                        output.ListChapterUploadFileDetailsInput = dncUploadDetails.ListChapterUploadFileDetailsInput;
                        break;
                    }
                }

                //Return the output object to the service
                return output;
            }
            catch (Exception e)
            {
                DncSubmitOutput go = new DncSubmitOutput();
                go.insertFlag = 2;
                go.insertOutput = e.Message;
                go.ListChapterUploadFileDetailsInput = dncUploadDetails.ListChapterUploadFileDetailsInput;
                go.transactionKey = trans_key;

                //Append the error info and return the output object to the service
                return go;
            }
           

        }


        private void writeSerializedUploadJsonData(DncSubmitOutput dncOP,string emailId)
        {
            var serializer = new JavaScriptSerializer();
            string emailSubject = "Dnc Upload";


            List<ChapterUploadFileDetailsHelper> dncJson = dncOP.ListChapterUploadFileDetailsInput;
            long trans_key = dncOP.transactionKey;
            

            if (dncJson != null && dncJson.Count > 0)
            {
                if (dncOP.insertFlag == 1)
                {

                    dncJson[dncJson.Count - 1].UploadStatus = "Success"; // set as pass
                    dncJson[dncJson.Count - 1].UploadEnd = DateTime.Now.ToString();
                    dncJson[dncJson.Count - 1].TransactionKey = trans_key.ToString();

                    string strEmailMessage = "The File, " + dncJson[dncJson.Count - 1].FileName.ToString() + " has been uploaded to the server successfully! Transaction key generated for this upload is " + trans_key + ". You will receive another email in the next 24 hours regarding the status of the uploaded data migration.";

                    Utility.sendUploadStatusMail(strEmailMessage, dncJson[dncJson.Count - 1].UserEmail.ToString(), emailSubject);

                    //Write details to JSON
                    var serializedResult = serializer.Serialize(dncJson);
                    string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));
                }

                else if (dncOP.insertFlag == 0)
                {
                    dncJson[dncJson.Count - 1].UploadStatus = "Failure"; // set as pass
                    dncJson[dncJson.Count - 1].UploadEnd = DateTime.Now.ToString();
                    dncJson[dncJson.Count - 1].TransactionKey = trans_key.ToString();

                    string strEmailMessage = "The File, " + dncJson[dncJson.Count - 1].FileName.ToString() + " has failed to upload to the server.";
                    Utility.sendUploadStatusMail(strEmailMessage, dncJson[dncJson.Count - 1].UserEmail.ToString(), emailSubject);

                    //Write details to JSON
                    var serializedResult = serializer.Serialize(dncJson);
                    string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));
                }
                else if (dncOP.insertFlag == 2)
                {
                    dncJson[dncJson.Count - 1].UploadStatus = "Failure"; // set as pass
                    dncJson[dncJson.Count - 1].UploadEnd = DateTime.Now.ToString();
                    dncJson[dncJson.Count - 1].TransactionKey = trans_key.ToString();

                    string strEmailMessage = "The File, " + dncJson[dncJson.Count - 1].FileName.ToString() + " has failed to upload to the server.";
                    Utility.sendUploadStatusMail(strEmailMessage, dncJson[dncJson.Count - 1].UserEmail.ToString(), emailSubject);

                    //Write details to JSON
                    var serializedResult = serializer.Serialize(dncJson);
                    string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));

                }
            }
        }



    }
}
