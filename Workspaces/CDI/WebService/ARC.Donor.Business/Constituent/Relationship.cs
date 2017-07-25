using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Business.Constituents
{
    public class Relationship
    {
        public string superior_cnst_mstr_id { get; set; }
        public string subord_cnst_mstr_id { get; set; }
        public string superior_cnst_key { get; set; }
        public string subord_cnst_key { get; set; }
        public string superior_nm_line { get; set; }
        public string subord_nm_line { get; set; }
        public string cnst_rlshp_typ_cd { get; set; }
        public string cnst_rlshp_typ_key { get; set; }
        public string strt_dt { get; set; }
        public string end_dt { get; set; }
        public string act_ind { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string dw_trans_ts { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string user_id { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }

    }

    public class OrgRelationship
    {
        public string org_mstr_id { get; set; }
        public string indv_mstr_id { get; set; }
        public string rlshp_typ { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public string phn_num { get; set; }
        public string extn_phn_num { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
    }


}