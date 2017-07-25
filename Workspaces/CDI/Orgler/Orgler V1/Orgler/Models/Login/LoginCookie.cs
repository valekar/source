using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Login
{
    public class LoginCookie
    {
        public string strUserName { get; set; }
        public string strUserEmail { get; set; }
        public string strUserGroup { get; set; }
        public string strUserRole { get; set; }
    }
}