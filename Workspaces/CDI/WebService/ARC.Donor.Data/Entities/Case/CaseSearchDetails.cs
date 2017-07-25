using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Case
{
    /* Entity methods for Case search details sections */
    public class CaseTransactionDetails
    {
        public Int64? trans_key { get; set; }
        public string transaction_type { get; set; }
        public string trans_typ_dsc { get; set; }
        public string sub_trans_actn_typ { get; set; }
        public string trans_stat { get; set; }
        public string user_id { get; set; }
        public Int64? case_seq { get; set; }
        public string trans_cnst_note { get; set; }
        public DateTime? trans_create_ts { get; set; }
    }

    public class CaseLocatorDetails
    {
        public Int64? srch_criteria_key { get; set; }
        public Int64? locator_id { get; set; }
        public string srch_typ { get; set; }
        public string srch_usr { get; set; }
        public Int64? cnst_mstr_id { get; set; }
        public string srcsys { get; set; }
        public string chpt_srcsys { get; set; }
        public string srcsys_id { get; set; }
        public string f_nm { get; set; }
        public string l_nm { get; set; }
        public string addr_line { get; set; }
        public string city { get; set; }
        public string state_cd { get; set; }
        public string zip { get; set; }
        public string phn_num { get; set; }
        public string email_addr { get; set; }
        public string usr_nm { get; set; }
        public string trans_typ { get; set; }
        public string trans_stat { get; set; }
        public string case_num { get; set; }
        public DateTime? from_dt { get; set; }
        public DateTime? to_dt { get; set; }
        public string org_nm { get; set; }
        public Int64? case_key { get; set; }
        public string cnst_typ { get; set; }
        public Int64? trans_key { get; set; }
        public Int64? ent_org_id { get; set; }
        public string ent_org_name { get; set; }
        public Int64? fortune_num { get; set; }
        public string source_code { get; set; }
        public string source_id { get; set; }
        public Int64? naics { get; set; }
        public Int64? key_acct_fr { get; set; }
        public Int64? key_acct_hs { get; set; }
        public Int64? key_acct_bio { get; set; }
        public Int64? key_acct_ent { get; set; }
        public string ent_org_type { get; set; }
        public string assessmnt_email_addr { get; set; }
        public string int_assessmnt_cd { get; set; }
        public string ext_hygiene_result { get; set; }
        public string ext_assessmnt_cd { get; set; }
        public DateTime? strt_dt { get; set; }
        public DateTime? end_dt { get; set; }
        public string user_id { get; set; }
        public DateTime? dw_srcsys_trans_ts { get; set; }
        public string appl_src_cd { get; set; }
        public string row_stat_cd { get; set; }
        public Int64? load_id { get; set; }
    }

    public class CaseNotesDetails
    {
        public Int64? case_key { get; set; }
        public Int64? notes_key { get; set; }
        public string case_notes { get; set; }
        public string row_stat_cd { get; set; }
        public DateTime? start_dt { get; set; }
        public DateTime? end_dt { get; set; }
    }

    public class CasePreferenceDetails
    {
        public string chan_val { get; set; }
        public string los_value { get; set; }
        public Int64? case_id { get; set; }
    }
}
