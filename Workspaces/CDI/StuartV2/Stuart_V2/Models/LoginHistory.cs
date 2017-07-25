﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models
{
    public class LoginHistory
    {
    }
    /* Entity to retrieve the logged in user information to log it in the teradata tables */
    public class LoginHistoryInput
    {
        public string UserName { get; set; }
        public string ActiveDirectoryGroupName { get; set; }
        public string LoginStatus { get; set; }
    }
}