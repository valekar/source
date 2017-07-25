using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Upload
{
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
