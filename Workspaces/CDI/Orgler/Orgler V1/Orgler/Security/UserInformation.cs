using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Orgler.Security
{
    //Class to store the user information
    public class UserInformation
    {
        #region properties

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool isAuthenticated { get; set; }
        public List<string> UserGroups { get; set; }
        public Boolean isAdmin { get; set; }
        public Boolean isHrisUser { get; set; }
        public Boolean isCdiUser { get; set; }
        public Boolean isFsaUser { get; set; }
        public string UserDomain { get; set; }
        public string UserLoggedInGroup { get; set; }

        public string AdminGroup
        {
            get
            {
                return ConfigurationManager.AppSettings["Admin"];
            }
        }

        public string HrisGroup
        {
            get
            {
                return ConfigurationManager.AppSettings["HrisUser"];
            }
        }

        public string CdiGroup
        {
            get
            {
                return ConfigurationManager.AppSettings["CdiUser"];
            }
        }

        public string FsaGroup
        {
            get
            {
                return ConfigurationManager.AppSettings["FsaUser"];
            }
        }

        #endregion
    }
}