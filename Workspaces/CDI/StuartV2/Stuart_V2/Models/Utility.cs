using Newtonsoft.Json;
using Stuart_V2.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using Stuart_V2.Models.Entities.Upload;
using System.Web.Script.Serialization;
using Stuart_V2.Exceptions;
using System.Web.Mvc;
using Stuart_V2.Models.Entities.Cem;

namespace Stuart_V2.Models
{
    public class Utility
    {
        public static string ObjectToString<T>(T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static T StringToObject<T>(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return (T)new BinaryFormatter().Deserialize(ms);
            }
        }

        public static string writeJSONToFile<T>(T Obj, string path)
        {
            string Result = "";
            StreamWriter aFile = null;
            try
            {
                //aFile = new StreamWriter(new FileStream(path, FileMode.Append));
                aFile = new StreamWriter(path);
                aFile.WriteLine(JsonConvert.SerializeObject(Obj, Formatting.Indented));
                Result = "SUCCESS";
            }
            catch (Exception ex)
            {
                Result = "FAIL: " + ex.Message;
            }
            finally
            {
                if (aFile != null) aFile.Dispose();
            }

            //using (StreamWriter aFile = new StreamWriter(path))
            //{
            //    aFile.WriteLine(JsonConvert.SerializeObject(myObj));
            //}
            return Result;
        }

        public static T readJSONTFromFile<T>(string path)
        {
            T result = default(T); ;
            StreamReader aFile = null;
            try
            {
                aFile = new StreamReader(path);
                result = JsonConvert.DeserializeObject<T>(aFile.ReadToEnd());
                aFile.Close();
            }
            catch
            {

            }
            finally
            {
                if (aFile != null) aFile.Dispose();
            }

            //using (StreamReader file = new StreamReader(path))
            //{
            //    result = JsonConvert.DeserializeObject<T>(file.ReadToEnd());
            //    file.Close();

            //}
            return result;
        }

        //Checks if an object has all its properties as null or not
        public static bool IsAnyPropertyNotNull(object o)
        {
            return o.GetType().GetProperties().Any(c => c.GetValue(o) != null);
        }


        /*written by srini for tab level securty hide feature*/
        public static List<TabLevelSecurityParams> getTabLevelSecurityList()
        {
            //Tab level security configuration entries read from JSON file
            string tabLevelSecureFilePath = ConfigurationManager.AppSettings["TabLevelSecurityConfigPath"];
            List<TabLevelSecurityParams> listTabLevelSecurity = new List<TabLevelSecurityParams>();
            string JsonDeserializeTabLevelUsers = File.ReadAllText(tabLevelSecureFilePath);
            listTabLevelSecurity = JsonConvert.DeserializeObject(JsonDeserializeTabLevelUsers, (typeof(List<TabLevelSecurityParams>))) as List<TabLevelSecurityParams>;
            return listTabLevelSecurity;

        }


        /*written by srini for tab level securty hide feature*/
        public static TabLevelSecurityParams getUserPermissions(string userName)
        {
            List<TabLevelSecurityParams> tabLevelSecurityList = Utility.getTabLevelSecurityList();
            TabLevelSecurityParams tabLevelSecurityCurrentUser = new TabLevelSecurityParams();
            if (tabLevelSecurityList.Exists(x => x.usr_nm.ToString().ToLower() == userName.ToLower()))
            {
                tabLevelSecurityCurrentUser = tabLevelSecurityList.AsEnumerable().Where(x => (x.usr_nm.ToString().ToLower() == userName.ToLower())).Select(x => x).FirstOrDefault();
            }
            return tabLevelSecurityCurrentUser;               
        }



        public static void storeUploadedFileDetails(DateTime startDate, FileDetails fileDetails, System.Web.Mvc.TempDataDictionary TempData,int businessType)
        {
            ChapterUploadFileDetailsHelper jsonhelper = new ChapterUploadFileDetailsHelper();

            DateTime endDate = DateTime.Now;
            string strUploadStatus = "Success";
            string strDocType = "Excel";
            string strEmailMessage = "The File, " + fileDetails.name + " has been uploaded to the server successfully!";
            var temp = (UploadParams)TempData["MessagePrefParams"];

            var UploadJsonDetails = (List<ChapterUploadFileDetailsHelper>)TempData["UploadJsonDetails"];

            jsonhelper.UserName = temp.loggedInUserName.ToString();
            jsonhelper.FileName = fileDetails.name;
            jsonhelper.FileSize = fileDetails.contentLength;
            jsonhelper.FileType = strDocType;
            jsonhelper.FileExtention = fileDetails.extension;
            jsonhelper.BusinessType = businessType;
            jsonhelper.UploadStart = startDate.ToString();
            jsonhelper.UploadEnd = endDate.ToString();
            jsonhelper.UploadStatus = strUploadStatus;
            jsonhelper.TransactionKey = "";


            UploadJsonDetails.Add(jsonhelper);
            TempData["UploadJsonDetails"] = UploadJsonDetails;

        }

        public static System.Web.HttpPostedFile getOneFile()
        {
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            return hfc[hfc.Count - 1];
        }

        public static FileDetails getFileDetails()
        {
            System.Web.HttpPostedFile hpf = getOneFile();
            FileDetails fileDetails = new FileDetails();
            fileDetails.name = Path.GetFileName(hpf.FileName);
            fileDetails.extension = System.IO.Path.GetExtension(hpf.FileName);
            fileDetails.contentLength = hpf.ContentLength;
            fileDetails.InputStream = hpf.InputStream;
            return fileDetails;
        }


        public static bool isFileDuplicate(System.Web.Mvc.TempDataDictionary TempData,int businessType)
        {
            // string fileName = "";
            //System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            bool UploadFlag = false;
            System.Web.HttpPostedFile hpf = getOneFile();

            if (hpf.ContentLength > 0)
            {

                var serializer = new JavaScriptSerializer();
                string JSONString = "";

                // Get JSON data from the File
                if (System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"]))
                {
                    JSONString = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"]);
                }

                List<ChapterUploadFileDetailsHelper> UploadDetails = new List<ChapterUploadFileDetailsHelper>();

                var jsonresult = serializer.Deserialize<List<ChapterUploadFileDetailsHelper>>(JSONString);


                //Populate json details to a list 
                if (jsonresult != null)
                {
                    UploadDetails = jsonresult;
                }

                string strFileName = "";
                double intFileSize = 0;

                string strDocExtention = "";

                // get document details
                strFileName = hpf.FileName; //file name
                intFileSize = hpf.ContentLength;
                strDocExtention = System.IO.Path.GetExtension(hpf.FileName);// file extention

                // Check if document was uploaded previously by referencing against the details obtained from the json
                if (UploadDetails.Count > 0)
                {
                    foreach (var item in UploadDetails)
                    {
                        // If business type is Chapter Upload and filename/filesize is the same as in the list, then the file is a duplicate
                        if (item.BusinessType == businessType && item.FileName == strFileName && item.FileSize == intFileSize)
                        {
                            if (item.UploadStatus != "Success")
                            {
                                UploadFlag = false;
                            }
                            else
                            {
                                UploadFlag = true;
                                break;
                            }
                        }
                    }
                }
                // return UploadFlag;    

                //store the upload content in temporary data
                TempData["UploadJsonDetails"] = UploadDetails;
            }
            return UploadFlag;
        }


        public static void sendUploadStatusMail(string strEmailMessage, string loggedInUser, string subject)
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
            sendMail.ToAddress = loggedInUser; // Changed Email Address hard coded to configurable from Web config(Only domain name will take from config)
            sendMail.FromAddress = fromAddress;
            sendMail.ccAddress = ccAddress;
            sendMail.bccAddress = bccAddress;
            sendMail.Subject = subject;
            sendMail.Body = strEmailMessage;

            sendMail.sendMail();
        }

        public static void writeSerializedUploadJsonData(UploadSubmitOutput dncOP, string emailId, string emailSubject)
        {
            var serializer = new JavaScriptSerializer();
          //  string emailSubject = "Dnc Upload";


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
                    //Utility.sendUploadStatusMail(strEmailMessage, emailId, emailSubject);

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
                   // Utility.sendUploadStatusMail(strEmailMessage, emailId, emailSubject);
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
                    //Utility.sendUploadStatusMail(strEmailMessage, emailId, emailSubject);
                    //Write details to JSON
                    var serializedResult = serializer.Serialize(dncJson);
                    string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));

                }
            }
        }
    }

    public class UploadParams
    {
        public string loggedInUserName { get; set; }
        public string expirationDate { get; set; }

    }

    public class FileDetails
    {
        public string name { get; set; }
        public double contentLength { get; set; }
        public string extension { get; set; }
        public Stream InputStream { get; set; }
    }


    public class FileLimit
    {
        public string fileName { get; set; }
        public string rowCount { get; set; }
        public string message { get; set; }
    }




    public class Constant
    {
        public static Constant instance = null;
        public static Dictionary<string, string> CONSTANTS = new Dictionary<string, string>(){
            {"CONST_NAME","ConstName"},
            {"CONST_ORG_NAME","ConstOrgName"},
            {"BEST_SMRY", "BestSmry"},
            {"CONST_ADDRESS", "ConstAddress"},
            {"CONST_EMAIL", "ConstEmail"},
            {"CONST_PHONE", "ConstPhone"},
            {"CONST_EXT_BRIDGE", "ConstExtBridge"},
            {"CONST_BIRTH", "ConstBirth"},
            {"CONST_DEATH", "ConstDeath"},
            {"CONST_PREF", "ConstPref"},
            {"CHARACTERISTICS", "Character"},
            {"GRP_MEMBERSHIP", "GrpMembership"},
            {"TRANS_HISTORY", "TransHistory"},
            {"ANON_INFO", "AnonInfo"},
            {"MASTER_ATTEMPT", "MasterAttempt"},
            {"INTERNAL_BRIDGE", "InternalBridge"},
            {"MERGE_HISTORY", "MergeHistory"},
            {"AFFILIATOR", "Affiliator"} ,
            {"MASTER_DETAIL", "MasterDetail"},
            {"ALTERNATEIDS_DETAILS", "AlternateIds"}
        };


    


        private Constant()
        {            
        }

        public static Constant getInstance()
        {
            if (instance == null)
                return new Constant();
            else
                return instance;
        }

    }

}