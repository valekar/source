using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Constituents
{
    public class OrgEmailDomain
    {
        public string email_domain_key { get; set; }
        public string cnst_mstr_id { get; set; }
        public string email_domain { get; set; }
        public string act_indv_email_cnt { get; set; }
        public string act_cnst_cnt { get; set; }
        public string most_rcnt_email_ts { get; set; }
        public string most_rcnt_vldtn_ts { get; set; }
        public string map_note { get; set; }
        public string transaction_key { get; set; }
        public string user_id { get; set; }
        public string row_stat_cd { get; set; }
        public string inactive_ind { get; set; }
    }

    public class OrgEmailDomainDeleteInput
    {
        public Int64 MasterID { get; set; }
        public string EmailDomain { get; set; }
        public string UserName { get; set; }
        public Int64? CaseNumber { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }

        public OrgEmailDomainDeleteInput()
        {
            UserName = string.Empty;
            ConstType = "OR";
            Notes = string.Empty;
            CaseNumber = 0;
        }
    }

    public class OrgEmailDomainAddInput
    {
        public Int64 MasterID { get; set; }
        public string EmailDomain { get; set; }
        public string UserName { get; set; }
        public Int64? CaseNumber { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }

        public OrgEmailDomainAddInput()
        {
            UserName = string.Empty;
            ConstType = "OR";
            Notes = string.Empty;
            CaseNumber = 0;
        }
    }

    public class OrgEmailDomainOutput
    {
        public string o_outputMessage { get; set; }
        public Int64? o_transaction_key { get; set; }
    }
}
