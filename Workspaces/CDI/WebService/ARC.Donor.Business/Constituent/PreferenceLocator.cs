using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Constituents
{
    public class PreferenceLocator
    {
        public string cnst_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string line_of_service_cd { get; set; }
        public string pref_loc_typ { get; set; }
        public string pref_loc_id { get; set; }
        public string notes { get; set; }
        public string status { get; set; }
        public string created_by { get; set; }
        public string created_ts { get; set; }
        public string cnst_pref_loc_strt_ts { get; set; }
        public string cnst_pref_loc_end_ts { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string is_previous { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string dw_trans_ts { get; set; }
        public string inactive_ind { get; set; }
        public string trans_status { get; set; }
        public string case_seq_num { get; set; }
        public string transaction_key { get; set; }
    }

    public class AllPreferenceLocator
    {
        public string cnst_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string line_of_service_cd { get; set; }
        public string pref_loc_typ { get; set; }
        public string pref_loc_id { get; set; }
        public string cnst_pref_loc_strt_ts { get; set; }
        public string cnst_pref_loc_end_ts { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string notes { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string dw_trans_ts { get; set; }
    }

    public class PreferenceLocatorInput
    {
        public Int64 masterId { get; set; }
        public string constituentType { get; set; }
        public string lineOfService { get; set; }
        public string notes { get; set; }
        public string createdBy { get; set; }
        public string userId { get; set; }
        public string oldSourceSystemCode { get; set; }
        public string oldPrefLocType { get; set; }
        public Int64 oldPrefLocId { get; set; }
        public string newSourceSystemCode { get; set; }
        public string newPrefLocType { get; set; }
        public Int64 newPrefLocId { get; set; }
        public Int64 caseNo { get; set; }
       
    }


    public class PreferenceLocatorOptions
    {
        public string loc_id { get; set; }
        public string loc_value { get; set; }
        public string loc_type { get; set; }
        public string cnst_mstr_id { get; set; }
    }

    public class PreferenceLocatorOutput
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }

}
