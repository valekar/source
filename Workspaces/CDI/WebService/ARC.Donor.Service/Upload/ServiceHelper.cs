using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using ARC.Donor.Service.EmailFiring;
using System.Text;

namespace ARC.Donor.Service.Upload
{

public class SendEmail
{
    public void sendMail(string ToAddress, MailAddress FromAddress, string CCAddress, string BCCAddress, string Subject, string Body)
    {
    
        Email emailfire = new Email();
        MailMessage msg = new MailMessage();

        msg.To.Add(ToAddress);
        
        if(CCAddress != "")
            msg.CC.Add(CCAddress);

        if(BCCAddress != "")
            msg.Bcc.Add(BCCAddress);

        msg.From = FromAddress;
        msg.Subject = Subject;
        msg.Body = Body;
        emailfire.sendMail(msg);
    }
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


public class JsonLoggerHelper
{
    public string UserName { get; set; }
    public string ActionName { get; set; }
    public string Query { get; set; }
    public string StartTime { get; set; }
    public string EndTIme { get; set; }
}

public class PicklistDataHelper
{
    public string picklist_typ { get; set; }
    public string picklist_typ_key { get; set; }
    public string picklist_typ_cd { get; set; }
    public string picklist_typ_dsc { get; set; }
    public string ref_order { get; set; }
    public string dw_trans_ts { get; set; }

    public PicklistDataHelper()
    {

    }

    public PicklistDataHelper(string grpName)
    {
        this.picklist_typ = grpName;
        this.dw_trans_ts = DateTime.Now.ToString();
    }

}

}