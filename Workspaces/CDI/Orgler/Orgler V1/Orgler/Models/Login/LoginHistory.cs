using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Login
{
    //Class to store the Login history information
    public class LoginHistoryInput
    {
        public string UserName { get; set; }
        public string ActiveDirectoryGroupName { get; set; }
        public string LoginStatus { get; set; }
    }
}