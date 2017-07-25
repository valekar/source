using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Admin
{
    public class Admin
    {
        public string usr_nm { get; set; }
        public string grp_nm { get; set; }
        public string email_address { get; set; }
        public string telephone_number { get; set; }
        public string constituent_tb_access { get; set; }
        public string transaction_tb_access { get; set; }
        public string case_tb_access { get; set; }
        public string admin_tb_access { get; set; }

        public string upload_tb_access { get; set; }
        public string locator_tab_access { get; set; }

        public string domn_corctn_access { get; set; }
        public string has_merge_unmerge_access { get; set; }
        public string is_approver { get; set; }
        public string row_stat_cd { get; set; }
        public string dw_trans_ts { get; set; }

        //added these because of sp problem
        public string account_tb_access { get; set; }
        public string enterprise_orgs_tb_access { get; set; }
        public string reference_data_tb_access { get; set; }
        public string report_tb_access { get; set; }
        public string utitlity_tb_access { get; set; }
        public string help_tb_access { get; set; }
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