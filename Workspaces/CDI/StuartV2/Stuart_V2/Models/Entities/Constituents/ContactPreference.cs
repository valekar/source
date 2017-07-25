using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Constituents
{
    public class ContactPreference
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cntct_prefc_typ_cd { get; set; }
        public string cntct_prefc_val { get; set; }
        public string act_ind { get; set; }
        public string cnst_cntct_prefc_strt_ts { get; set; }
        public string cnst_cntct_prefc_end_ts { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string dw_trans_ts { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
        public string transNotes { get; set; }
        //for missy
        public string distinct_count { get; set; }

        public ContactPreference()
        {
            this.transNotes = string.Empty;
        }
    }

    public class ConstituentContactPrefcInput
    {
        public string RequestType { get; set; }
        public Int64 MasterID { get; set; }
        public string UserName { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public Int64? CaseNumber { get; set; }
        public string OldContactPrefcValue { get; set; }
        public string OldSourceSystemCode { get; set; }
        public string OldContactPrefcTypeCode { get; set; }
        public string ContactPrefcValue { get; set; }
        public string SourceSystemCode { get; set; }
        public string ContactPrefcTypeCode { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }

        public ConstituentContactPrefcInput()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            UserName = p.GetUserName(); //p.Identity.Name;
            ConstType = string.Empty;
           // Notes = "This is a test";
           // CaseNumber = 0;
            OldContactPrefcValue = string.Empty;
            OldSourceSystemCode = string.Empty;
            OldContactPrefcTypeCode = string.Empty;
            ContactPrefcValue = string.Empty;
            SourceSystemCode = string.Empty;
            ContactPrefcTypeCode = string.Empty;
        }

    }

    public class ConstituentContactPrefcOutput
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }
}