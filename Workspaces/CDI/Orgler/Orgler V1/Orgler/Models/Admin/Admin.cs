using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Admin
{
    public class Admin
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
        public string row_stat_cd { get; set; }
        public string dw_trans_ts { get; set; }
    }


    public class AdminTabSecurityInput
    {
        public string loggedInUser { get; set; }
        public int NoOfRecs { get; set; }
        public int PageNum { get; set; }

        public AdminTabSecurityInput()
        {
            this.NoOfRecs = 1000;
            this.PageNum = 10;
        }
    }


    public class AdminPostInput
    {
        public Admin adminInput { get; set; }
        public string loggedInUser { get; set; }
    }
}