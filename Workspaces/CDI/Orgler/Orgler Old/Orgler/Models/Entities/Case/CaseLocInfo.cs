using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities.Case
{

    public class CaseLocInfo
    {
    }
    public class CaseLocatorInput
    {
        public string req_typ { get; set; }
        public Int64? case_key { get; set; }
        public int locator_id { get; set; }
        public string usr_nm { get; set; }
        public string locator_upd { get; set; }
        public string o_outputMessage { get; set; }
        public CaseLocatorInput()
        {

            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            usr_nm = p.GetUserName(); //p.Identity.Name;

          
        }
    }


       
}