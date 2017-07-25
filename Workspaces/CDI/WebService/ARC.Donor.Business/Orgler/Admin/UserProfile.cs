using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Orgler.Admin
{
  
        public class UserProfile
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
            public string domn_corctn_access { get; set; }
            public string has_merge_unmerge_access { get; set; }
            public string is_approver { get; set; }
            public string row_stat_cd { get; set; }
            public string dw_trans_ts { get; set; }

        }
    public class UserProfileOutput
    {
        public string o_transOutput { get; set; }
    }
    public class AdminTabSecurityInput
    {
        public string loggedInUser { get; set; }
        public int NoOfRecs { get; set; }
        public int PageNum { get; set; }

        public AdminTabSecurityInput()
        {
            this.NoOfRecs = 100;
            this.PageNum = 10;
        }
    }

    public class AdminCRUDOutput
    {
        public string strSPQuery { get; set; }
        public List<object> parameters { get; set; }
    }


    public class AdminTransOutput
    {
        public string o_transOutput { get; set; }
    }

    public class AdminPostInput
    {
        public UserProfile adminInput { get; set; }
        public string loggedInUser { get; set; }
    }


    public class TabSecurityData
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
    public class LoginHistoryInput
    {
        public string UserName { get; set; }
        public string ActiveDirectoryGroupName { get; set; }
        public string LoginStatus { get; set; }
    }
}

