using Stuart_V2.Models;
using Stuart_V2.Models.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Stuart_V2.Controllers.MVC
{
    [RouteArea("General")]
    [RoutePrefix("")]
    public class GeneralController : Controller
    {
        // GET: General
        public JsonResult Unauthorized()
        {
            Response.StatusCode = 423;
            return Json("LoginDenied", JsonRequestBehavior.AllowGet);
        }

        public JsonResult InternalError()
        {
            Response.StatusCode = 500;
            return Json("Internal Error", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DatabaseError()
        {
            Response.StatusCode = 500;
            return Json("Database Error", JsonRequestBehavior.AllowGet);
        }

        public JsonResult TimedOut()
        {
            Response.StatusCode = 500;
            return Json("Timed out", JsonRequestBehavior.AllowGet);
        }

        public void ResponseFromUpload(GroupMembershipSubmitOutput gso)
        {
            Trace.Write("My return url object is " + gso);

            var serializer = new JavaScriptSerializer();

            List<ChapterUploadFileDetailsHelper> gm = gso.ListChapterUploadFileDetailsInput;
            long trans_key = gso.transactionKey;

            if (gm != null && gm.Count > 0)
            {
                if (gso.insertFlag == 1)
                {
                   
                    gm[gm.Count - 1].UploadStatus = "Success"; // set as pass
                    gm[gm.Count - 1].UploadEnd = DateTime.Now.ToString();
                    gm[gm.Count - 1].TransactionKey = trans_key.ToString();
                    
                    string strEmailMessage = "The File, " + gm[gm.Count - 1].FileName.ToString() + " has been uploaded to the server successfully!. Transaction key generated for this upload is " + trans_key + ". You will receive another email in the next 24 hours regarding the status of the uploaded data migration.";

                     sendUploadStatusMail(strEmailMessage);

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
                    sendUploadStatusMail(strEmailMessage);

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
                    sendUploadStatusMail(strEmailMessage);

                    //Write details to JSON
                    var serializedResult = serializer.Serialize(gm);
                    string jpath = (System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"] ?? "").ToString();

                    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
                        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));

                }
            }
        }

        public void sendUploadStatusMail(string strEmailMessage)
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

            var sendMail = new Models.Mail();
            sendMail.ToAddress = "chiranjib.nandy@mindtree.com";
            sendMail.FromAddress = fromAddress;
            sendMail.ccAddress = ccAddress;
            sendMail.bccAddress = bccAddress;
            sendMail.Subject = "Group Membership Upload Status";
            sendMail.Body = strEmailMessage;

            sendMail.sendMail();
        }

        public class JSON_PrettyPrinter
        {
            public static string Process(string inputText)
            {
                bool escaped = false;
                bool inquotes = false;
                int column = 0;
                int indentation = 0;
                Stack<int> indentations = new Stack<int>();
                int TABBING = 8;
                StringBuilder sb = new StringBuilder();
                foreach (char x in inputText)
                {
                    sb.Append(x);
                    column++;
                    if (escaped)
                    {
                        escaped = false;
                    }
                    else
                    {
                        if (x == '\\')
                        {
                            escaped = true;
                        }
                        else if (x == '\"')
                        {
                            inquotes = !inquotes;
                        }
                        else if (!inquotes)
                        {
                            if (x == ',')
                            {
                                // if we see a comma, go to next line, and indent to the same depth
                                sb.Append("\r\n");
                                column = 0;
                                for (int i = 0; i < indentation; i++)
                                {
                                    sb.Append(" ");
                                    column++;
                                }
                            }
                            else if (x == '[' || x == '{')
                            {
                                // if we open a bracket or brace, indent further (push on stack)
                                indentations.Push(indentation);
                                indentation = column;
                            }
                            else if (x == ']' || x == '}')
                            {
                                // if we close a bracket or brace, undo one level of indent (pop)
                                indentation = indentations.Pop();
                            }
                            else if (x == ':')
                            {
                                // if we see a colon, add spaces until we get to the next
                                // tab stop, but without using tab characters!
                                while ((column % TABBING) != 0)
                                {
                                    sb.Append(' ');
                                    column++;
                                }
                            }
                        }
                    }
                }
                return sb.ToString();
            }
        }
    }
}