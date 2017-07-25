using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Case
{
    public class CaseNotes
    {
    }

    public class CaseNotesInput
    {
        public Int64? case_id { get; set; }
        public int? notes_id { get; set; }
        public string notes_text { get; set; }
        public string action { get; set; }
        public string user_id { get; set; }
        public string o_outputMessage { get; set; }
        public CaseNotesInput()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            user_id = p.GetUserName(); //p.Identity.Name;
           /* if (Models.Entities.SessionUtils.strUserName != "")
                user_id = Models.Entities.SessionUtils.strUserName;
            else
                user_id = "";*/
        }
    }
}