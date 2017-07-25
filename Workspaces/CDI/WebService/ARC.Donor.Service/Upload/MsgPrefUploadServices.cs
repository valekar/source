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
   public class MsgPrefUploadServices
    {
       public Business.Upload.MsgPrefSubmitOutput msgPrefUploadData(MsgPrefUploadDetails msgPrefUploadDetails)
       {
           Mapper.CreateMap<Business.Upload.MsgPrefUploadParams, Data.Entities.Upload.MsgPrefUploadParams>();
           Mapper.CreateMap<Business.Upload.MsgPrefUploadedFileInfo, Data.Entities.Upload.MsgPrefUploadedFileInfo>();
           Mapper.CreateMap<Business.Upload.ListMsgPrefUploadInput, Data.Entities.Upload.ListMsgPrefUploadInput>();
           Mapper.CreateMap<Business.Upload.ChapterUploadFileDetailsHelper, Data.Entities.Upload.ChapterUploadFileDetailsHelper>();
           Mapper.CreateMap<Data.Entities.Upload.ChapterUploadFileDetailsHelper,Business.Upload.ChapterUploadFileDetailsHelper >();
           Mapper.CreateMap<Business.Upload.MsgPrefUploadDetails, Data.Entities.Upload.MsgPrefUploadDetails>();

           var input = Mapper.Map<Business.Upload.ChapterUploadFileDetailsHelper, Data.Entities.Upload.ChapterUploadFileDetailsHelper>
               (msgPrefUploadDetails.ListChapterUploadFileDetailsInput[msgPrefUploadDetails.ListChapterUploadFileDetailsInput.Count-1]);
               

           Data.Upload.MsgPrefUploadDetails gd = new Data.Upload.MsgPrefUploadDetails();

           long trans_key = gd.getMsgPrefUploadTransKeyDetails(input.UserName);

           var MsgPrefUploadSubmitOutput = computeMsgPrefUploadDetails(msgPrefUploadDetails, trans_key);

          // writeSerializedUploadJsonData(MsgPrefUploadSubmitOutput,msgPrefUploadDetails.userEmailId);

           return MsgPrefUploadSubmitOutput;
       }




       private MsgPrefSubmitOutput computeMsgPrefUploadDetails(MsgPrefUploadDetails msgPrefUploadDetails, long trans_key)
       {
           Mapper.CreateMap<Business.Upload.MsgPrefUploadParams, Data.Entities.Upload.MsgPrefUploadParams>();
           Mapper.CreateMap<Business.Upload.MsgPrefUploadedFileInfo, Data.Entities.Upload.MsgPrefUploadedFileInfo>();
           Mapper.CreateMap<Business.Upload.ListMsgPrefUploadInput, Data.Entities.Upload.ListMsgPrefUploadInput>();
           Mapper.CreateMap<Business.Upload.ChapterUploadFileDetailsHelper, Data.Entities.Upload.ChapterUploadFileDetailsHelper>();
           Mapper.CreateMap<Business.Upload.MsgPrefUploadDetails, Data.Entities.Upload.MsgPrefUploadDetails>();

           Data.Upload.MsgPrefUploadDetails gd = new Data.Upload.MsgPrefUploadDetails();

           try
           {
               var max_seq_key = gd.getMsgPrefSeqKeyDetails();
               //var max_seq_key = 0;
               var input = Mapper.Map<Business.Upload.MsgPrefUploadDetails, Data.Entities.Upload.MsgPrefUploadDetails>(msgPrefUploadDetails);


               //Create the output object
               Data.Entities.Upload.MsgPrefSubmitOutput go = new Data.Entities.Upload.MsgPrefSubmitOutput();

               Mapper.CreateMap<Data.Entities.Upload.MsgPrefSubmitOutput, Business.Upload.MsgPrefSubmitOutput>();

               var output = Mapper.Map<Data.Entities.Upload.MsgPrefSubmitOutput, Business.Upload.MsgPrefSubmitOutput>(go);
               output.insertFlag = 1;
               output.insertOutput = "Success";
               output.transactionKey = trans_key;
               output.ListChapterUploadFileDetailsInput = msgPrefUploadDetails.ListChapterUploadFileDetailsInput;


               foreach (var msgPrefUploadItem in input.ListMsgPrefUploadInput.msgPrefUploadInputList)
               {

                   try
                   {

                       if (!(string.IsNullOrEmpty(msgPrefUploadItem.arc_srcsys_cd) && string.IsNullOrEmpty(msgPrefUploadItem.cnst_srcsys_id))
                           || !(string.IsNullOrEmpty(msgPrefUploadItem.init_mstr_id)))
                       {
                           msgPrefUploadItem.msg_prefc_submissn_typ = "specific_master";
                       }
                       else
                       {
                           msgPrefUploadItem.msg_prefc_submissn_typ = "locator_level";
                       }

                       var incr_max_key = Interlocked.Increment(ref max_seq_key);

                       Task.Run(() => gd.insertMsgPrefUploadDetails(msgPrefUploadItem, input.ListChapterUploadFileDetailsInput[input.ListChapterUploadFileDetailsInput.Count-1].UserName, trans_key, incr_max_key));
                   }


                   catch
                   {
                       output.insertFlag = 0;
                       output.insertOutput = "Failure";
                       output.transactionKey = trans_key;
                       output.ListChapterUploadFileDetailsInput = msgPrefUploadDetails.ListChapterUploadFileDetailsInput;
                       break;
                   }
               }

               //Return the output object to the service
               return output;
           }
           catch (Exception e)
           {
               MsgPrefSubmitOutput go = new MsgPrefSubmitOutput();
               go.insertFlag = 2;
               go.insertOutput = e.Message;
               go.ListChapterUploadFileDetailsInput = msgPrefUploadDetails.ListChapterUploadFileDetailsInput;
               go.transactionKey = trans_key;

               //Append the error info and return the output object to the service
               return go;
           }
       }


       private void writeSerializedUploadJsonData(MsgPrefSubmitOutput msgPrefOP,string userEmailId)
       {
           var serializer = new JavaScriptSerializer();
           string emailSubject = "Message Preference Upload";


           List<ChapterUploadFileDetailsHelper> msgPrefJson = msgPrefOP.ListChapterUploadFileDetailsInput;
           long trans_key = msgPrefOP.transactionKey;
          // string uploadedUserDomain = (System.Configuration.ConfigurationManager.AppSettings["UploadedUserDomain"] ?? "").ToString();

           if (msgPrefJson != null && msgPrefJson.Count > 0)
           {
               if (msgPrefOP.insertFlag == 1)
               {

                   msgPrefJson[msgPrefJson.Count - 1].UploadStatus = "Success"; // set as pass
                   msgPrefJson[msgPrefJson.Count - 1].UploadEnd = DateTime.Now.ToString();
                   msgPrefJson[msgPrefJson.Count - 1].TransactionKey = trans_key.ToString();

                   string strEmailMessage = "The File, " + msgPrefJson[msgPrefJson.Count - 1].FileName.ToString() + " has been uploaded to the server successfully! Transaction key generated for this upload is " + trans_key + ". You will receive another email in the next 24 hours regarding the status of the uploaded data migration.";

                   Utility.sendUploadStatusMail(strEmailMessage, msgPrefJson[msgPrefJson.Count - 1].UserEmail.ToString(), emailSubject);

                   //Write details to JSON
                   var serializedResult = serializer.Serialize(msgPrefJson);
                   string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                   if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                       System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));
               }

               else if (msgPrefOP.insertFlag == 0)
               {
                   msgPrefJson[msgPrefJson.Count - 1].UploadStatus = "Failure"; // set as pass
                   msgPrefJson[msgPrefJson.Count - 1].UploadEnd = DateTime.Now.ToString();
                   msgPrefJson[msgPrefJson.Count - 1].TransactionKey = trans_key.ToString();

                   string strEmailMessage = "The File, " + msgPrefJson[msgPrefJson.Count - 1].FileName.ToString() + " has failed to upload to the server.";
                   Utility.sendUploadStatusMail(strEmailMessage, msgPrefJson[msgPrefJson.Count - 1].UserEmail.ToString(), emailSubject);

                   //Write details to JSON
                   var serializedResult = serializer.Serialize(msgPrefJson);
                   string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                   if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                       System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));
               }
               else if (msgPrefOP.insertFlag == 2)
               {
                   msgPrefJson[msgPrefJson.Count - 1].UploadStatus = "Failure"; // set as pass
                   msgPrefJson[msgPrefJson.Count - 1].UploadEnd = DateTime.Now.ToString();
                   msgPrefJson[msgPrefJson.Count - 1].TransactionKey = trans_key.ToString();

                   string strEmailMessage = "The File, " + msgPrefJson[msgPrefJson.Count - 1].FileName.ToString() + " has failed to upload to the server.";
                   Utility.sendUploadStatusMail(strEmailMessage, msgPrefJson[msgPrefJson.Count - 1].UserEmail.ToString(), emailSubject);

                   //Write details to JSON
                   var serializedResult = serializer.Serialize(msgPrefJson);
                   string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                   if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                       System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));

               }
           }
       }


    }
}
