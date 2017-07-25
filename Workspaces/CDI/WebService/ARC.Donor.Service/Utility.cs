﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Net.Mail;

namespace ARC.Donor.Service
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
                aFile.WriteLine(JsonConvert.SerializeObject(Obj));
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


        public static void sendUploadStatusMail(string strEmailMessage, string loggedInUser,string subject)
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



    public class Mail
    {
        private string _host;
        private int _port;
        private string _from;
        private string _toAddress;
        private string _attachment;
        private string _subject;
        private string _body;
        private string _message;
        private string _ccAddress;
        private string _bccAddress;
        private bool _isBinaryAttachemnt = false;
        private byte[] _attachmentData = null;
        private string _attachmentName;

        public string AttachmentName
        {
            set { _attachmentName = value; }
        }

        public byte[] AttachmentData
        {
            set { _attachmentData = value; }
        }

        public bool IsBinaryAttachment
        {
            set { _isBinaryAttachemnt = value; }
        }

        public Mail()
        {
            SetEmailDefaults();
        }

        public string GetMessage()
        {
            return _message;
        }

        public string Host
        {
            set { _host = value; }
        }

        public int Port
        {
            set { _port = value; }

        }

        public string FromAddress
        {
            set { _from = value; }
        }

        public string ToAddress
        {
            set { _toAddress = value; }
        }

        public string Attachment
        {
            set { _attachment = value; }
        }

        public string Subject
        {
            set { _subject = value; }
        }

        public string Body
        {
            set { _body = value; }
        }

        public string ccAddress
        {
            set { _ccAddress = value; }
        }

        public string bccAddress
        {
            set { _bccAddress = value; }
        }


        private void SetEmailDefaults()
        {
            Host = "relay.redcross.org";
            Port = 25;
            //Host = "smtp.sendgrid.net";
            //Port = 465;
            //Host = "smtp.gmail.com";
            //Port = 465;
            FromAddress = "Nagaraju.challa@redcross.org";
            ToAddress = "nagaraju.challa@redcross.org";
            Subject = "FLIA Form Submission";
            Body = "FLIA Form Submission on " + DateTime.Now.ToString() + Environment.NewLine;
            _attachmentName = "FLIA.pdf";
        }

        public void sendMail()
        {
            try
            {
                using (SmtpClient SmtpServer = new SmtpClient(_host))
                {
                    using (MailMessage msg = new MailMessage())
                    {
                        msg.From = new MailAddress(_from);
                        msg.Subject = _subject;
                        msg.Body = _body;
                        SmtpServer.Port = _port;
                        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //SmtpServer.UseDefaultCredentials = false;
                        //SmtpServer.Credentials = new System.Net.NetworkCredential("naggonline", ""); //"ARCSendGridPOC", "TrustNo1Today"
                        //SmtpServer.EnableSsl = true;
                        _toAddress = _toAddress + ",";
                        _ccAddress = _ccAddress + ",";
                        _bccAddress = _bccAddress + ",";
                        _toAddress = _toAddress.Replace(",,", ",");
                        _ccAddress = _ccAddress.Replace(",,", ",");
                        _bccAddress = _bccAddress.Replace(",,", ",");

                        foreach (string address in _toAddress.Split(','))
                        {
                            //msg.To.Add(new MailAddress("your@email1.com", "Your name 1"));
                            if (address.Trim() != "")
                                msg.To.Add(address.Trim());
                        }

                        foreach (string address in _ccAddress.Split(','))
                        {
                            if (address.Trim() != "")
                                msg.CC.Add(address.Trim());
                        }

                        foreach (string address in _bccAddress.Split(','))
                        {
                            if (address.Trim() != "")
                                msg.Bcc.Add(address.Trim());
                        }

                        //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(_body, null, MediaTypeNames.Text.Plain));
                        msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(_body, null, System.Net.Mime.MediaTypeNames.Text.Html));

                        if (_isBinaryAttachemnt)
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(_attachmentData))
                            {
                                msg.Attachments.Add(new Attachment(ms, _attachmentName, System.Net.Mime.MediaTypeNames.Application.Pdf));
                                SmtpServer.Send(msg);
                                _message = "Mail Sent Successfully with binary attachment.";
                            }
                        }
                        else if (!string.IsNullOrEmpty(_attachment))
                        {
                            Attachment attachment = new Attachment(_attachment, System.Net.Mime.MediaTypeNames.Application.Octet);
                            msg.Attachments.Add(attachment);
                            SmtpServer.Send(msg);
                            _message = "Mail Sent Successfully with an attachment.";
                        }
                        else
                        {
                            SmtpServer.Send(msg);
                            _message = "Mail Sent Successfully.";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _message = "Error: " + ex.Message;
                if (ex.InnerException != null) _message += " Inner Exception: " + ex.InnerException;

            }
        }

    }

}