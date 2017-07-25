using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Security
{
    public class SessionUtils
    {
        //Parameter to hold the tab denied flag
        public static bool TabDenyIndicator
        {
            get
            {
                if (HttpContext.Current.Session["TabDenyIndicator"] != null)
                {
                    return (bool)HttpContext.Current.Session["TabDenyIndicator"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                HttpContext.Current.Session["TabDenyIndicator"] = value;
            }
        }

        //Parameter to store the logged in user name
        public static string strUserName
        {
            get
            {
                if (HttpContext.Current.Session["strUserName"] != null)
                {
                    return (string)HttpContext.Current.Session["strUserName"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["strUserName"] = value;
            }
        }


        //Parameter to store the UserGroup 
        public static string strGroupName
        {
            get
            {
                if (HttpContext.Current.Session["strGroupName"] != null)
                {
                    return (string)HttpContext.Current.Session["strGroupName"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["strGroupName"] = value;
            }
        }

    }
    [Serializable]
    public class TabLevelSecurityParams
    {
        public string usr_nm { get; set; }
        public string grp_nm { get; set; }
        public string email_address { get; set; }
        public string telephone_number { get; set; }
        public string newaccount_tb_access { get; set; }
        public string topaccount_tb_access { get; set; }
        public string enterprise_orgs_tb_access { get; set; }
        public string constituent_tb_access { get; set; }
        public string transaction_tb_access { get; set; }
        public string admin_tb_access { get; set; }
        public string help_tb_access { get; set; }
        public string upload_eosi_tb_access { get; set; }
        public string upload_affil_tb_access { get; set; }
        public string upload_eo_tb_access { get; set; }        
        public string has_merge_unmerge_access { get; set; }
        public string is_approver { get; set; }        
    }
}