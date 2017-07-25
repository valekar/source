using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Constituents
{
    public class OrgAffiliators
    {
        public string ent_org_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string ent_org_name { get; set; }
        public string cln_cnst_org_nm { get; set; }
        public string cnst_mstr_id { get; set; }
        public string cnst_affil_strt_ts { get; set; }
        public string cnst_affil_end_ts { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
          public string transNotes { get; set; }
          //for misy
          public string distinct_count { get; set; }

          public OrgAffiliators()
        {
            this.transNotes = string.Empty;
        }
    }

    public class OrgAffiliatorsInput
    {
        public string req_typ { get; set; }
        public Int64 mstr_id { get; set; }
        public string usr_nm { get; set; }
        public string cnst_typ { get; set; }
        public string notes { get; set; }
        public Int64? case_seq_num { get; set; }
        public Int64? bk_ent_org_id { get; set; }
        public Int64? new_ent_org_id { get; set; }
        public string o_outputMessage { get; set; }
        public Int64? o_transaction_key { get; set; }

        public OrgAffiliatorsInput()
        {
            usr_nm = "";
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            usr_nm = p.GetUserName(); //p.Identity.Name;
            req_typ = string.Empty;
            cnst_typ = string.Empty;
            notes = string.Empty;
            o_outputMessage = string.Empty;
            o_transaction_key = 0;
        }
    }

}