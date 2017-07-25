using Newtonsoft.Json;
using Orgler.Models.Upload;
using Orgler.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Script.Serialization;

namespace Orgler.Security
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

        public static void writeSerializedUploadJsonData(UploadSubmitOutput upldOP, string emailId, string emailSubject)
        {
            var serializer = new JavaScriptSerializer();

            List<UploadStat> upldOutputJson = upldOP.ListUploadFileDetailsInput;
            long trans_key = upldOP.transactionKey;

            if (upldOutputJson != null && upldOutputJson.Count > 0)
            {
                if (upldOP.insertFlag == 1)
                {
                    upldOutputJson[upldOutputJson.Count - 1].UploadStatus = "Success"; // set as pass
                    upldOutputJson[upldOutputJson.Count - 1].UploadEnd = DateTime.Now.ToString();
                    upldOutputJson[upldOutputJson.Count - 1].TransactionKey = trans_key.ToString();

                    //string strEmailMessage = "The File, " + upldOutputJson[upldOutputJson.Count - 1].FileName.ToString() + " has been uploaded to the server successfully! Transaction key generated for this upload is " + trans_key + ". Please come back tomorrow and check the status of the uploaded data migration via Transaction tab.";
                    string strEmailMessage = "The File, " + upldOutputJson[upldOutputJson.Count - 1].FileName.ToString() + " has been uploaded to the server successfully! Transaction key generated for this upload is " + trans_key + ". You will receive another email in the next 24 hours regarding the status of the uploaded data migration.";

                    Utility.sendUploadStatusMail(strEmailMessage, upldOutputJson[upldOutputJson.Count - 1].UserEmail.ToString(), emailSubject);

                    //Write details to JSON
                    var serializedResult = serializer.Serialize(upldOutputJson);
                    string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));
                }

                else if (upldOP.insertFlag == 0)
                {
                    upldOutputJson[upldOutputJson.Count - 1].UploadStatus = "Failure"; // set as pass
                    upldOutputJson[upldOutputJson.Count - 1].UploadEnd = DateTime.Now.ToString();
                    upldOutputJson[upldOutputJson.Count - 1].TransactionKey = trans_key.ToString();

                    string strEmailMessage = "The File, " + upldOutputJson[upldOutputJson.Count - 1].FileName.ToString() + " has failed to upload to the server.";
                    Utility.sendUploadStatusMail(strEmailMessage, upldOutputJson[upldOutputJson.Count - 1].UserEmail.ToString(), emailSubject);

                    //Write details to JSON
                    var serializedResult = serializer.Serialize(upldOutputJson);
                    string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));
                }
                else if (upldOP.insertFlag == 2)
                {
                    upldOutputJson[upldOutputJson.Count - 1].UploadStatus = "Failure"; // set as pass
                    upldOutputJson[upldOutputJson.Count - 1].UploadEnd = DateTime.Now.ToString();
                    upldOutputJson[upldOutputJson.Count - 1].TransactionKey = trans_key.ToString();

                    string strEmailMessage = "The File, " + upldOutputJson[upldOutputJson.Count - 1].FileName.ToString() + " has failed to upload to the server.";
                    Utility.sendUploadStatusMail(strEmailMessage, upldOutputJson[upldOutputJson.Count - 1].UserEmail.ToString(), emailSubject);

                    //Write details to JSON
                    var serializedResult = serializer.Serialize(upldOutputJson);
                    string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                    if (!string.IsNullOrEmpty(jpath)) 
                        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));
                }
            }
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
    }
}