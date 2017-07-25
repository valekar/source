
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
namespace ARC.Donor.Service.EmailFiring
{
    public class Email
    {
        private string _host;
        private int _port;
        private string _from;
        private string _toAddress;
        private string _ccAddress;
        private string _bccAddress;
        private string _attachment;
        private string _subject;
        private string _body;
        private string _message;
        private bool _isBinaryAttachemnt = false;
        private byte[] _attachmentData = null;
        private string _attachmentName;
        private bool _isSuccess = false;

        public bool IsSuccess { get { return _isSuccess; } }
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

        public Email()
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

        public string CCAddress
        {
            set { _ccAddress = value; }
        }

        public string BCCAddress
        {
            set { _bccAddress = value; }
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

        private void SetEmailDefaults()
        {
            Host = ConfigurationManager.AppSettings["HostEmail"].ToString();
            Port = Convert.ToInt16(ConfigurationManager.AppSettings["PortEmail"]);
        }

        public void sendMail(MailMessage msg)
        {
            _from = msg.From.ToString();
            _toAddress = msg.To.ToString();
            _ccAddress = msg.CC.ToString();
            _bccAddress = msg.Bcc.ToString();
          //  _attachment = msg.Attachments.ToString();
            _subject = msg.Subject;
            _body = msg.Body;
            //_message = msg.Messages;
            //_attachmentData = msg.AttachmentData;
            //_attachmentName = msg.AttachmentName;
            //_isBinaryAttachemnt = msg.IsBinaryAttachemnt;
            sendMail();
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
                        _ccAddress = _ccAddress + ",";
                        _ccAddress = _ccAddress.Replace(",,", ",");
                        _toAddress = _toAddress + ",";
                        _toAddress = _toAddress.Replace(",,", ",");
                        foreach (string address in _toAddress.Split(','))
                        {
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
                                _isSuccess = true;
                                _message = "Mail Sent Successfully with binary attachment.";
                            }
                        }
                        else if (!string.IsNullOrEmpty(_attachment))
                        {
                            Attachment attachment = new Attachment(_attachment, System.Net.Mime.MediaTypeNames.Application.Octet);
                            msg.Attachments.Add(attachment);
                            SmtpServer.Send(msg);
                            _isSuccess = true;
                            _message = "Mail Sent Successfully with an attachment.";
                        }
                        else
                        {
                            SmtpServer.Send(msg);
                            _isSuccess = true;
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