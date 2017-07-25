using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.Admin
{
    /* Entity for the User's tab Level Security */
    public class UserTabLevelSecurity
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

        public UserTabLevelSecurity()
        {
            newaccount_tb_access = "N";
            topaccount_tb_access = "N";
            enterprise_orgs_tb_access = "N";
            constituent_tb_access = "N";
            transaction_tb_access = "N";
            admin_tb_access = "N";
            help_tb_access = "N";
            upload_eosi_tb_access = "N";
            upload_affil_tb_access = "N";
            upload_eo_tb_access = "N";
            has_merge_unmerge_access = "0";
            is_approver = "0";
        }
    }
}
