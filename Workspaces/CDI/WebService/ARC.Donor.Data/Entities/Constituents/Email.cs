using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Entities.Constituents
{
    public class Email
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_email_addr { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string email_typ_cd { get; set; }
        public string cntct_stat_typ_cd { get; set; }
        public string cnst_best_email_ind { get; set; }
        public string cnst_email_strt_ts { get; set; }
        public string cnst_email_end_dt { get; set; }
        public string best_email_ind { get; set; }
        public string email_key { get; set; }
        public string domain_corrctd_ind { get; set; }
        public string cnst_email_validtn_dt { get; set; }
        public string cnst_email_validtn_method { get; set; }
        public string cnst_email_validtn_ind { get; set; }
        public string cnst_email_best_los_ind { get; set; }
        public string trans_key { get; set; }
        public string act_ind { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
        public string assessmnt_ctg { get; set; }
    }

    //Class for Email Data Input Entity 
    public class ConstituentEmailInput
    {
        public string RequestType { get; set; }
        public Int64 MasterID { get; set; }
        public string UserName { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public Int64 CaseNumber { get; set; }
        public string OldSourceSystemCode { get; set; }
        public string OldEmailTypeCode { get; set; }
        public string OldBestLOSInd { get; set; }
        public string NewEmailAddress { get; set; }
        public string SourceSystemCode { get; set; }
        public string EmailTypeCode { get; set; }
        public byte BestLOS { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }

        public ConstituentEmailInput()
        {
            UserName = string.Empty;
            ConstType = string.Empty;
            Notes = string.Empty;
            CaseNumber = 0;
            OldSourceSystemCode = string.Empty;
            OldEmailTypeCode = string.Empty;
            OldBestLOSInd = "0";
            NewEmailAddress = string.Empty;
            SourceSystemCode = string.Empty;
            EmailTypeCode = string.Empty;
            BestLOS = 0;
        }
    }

    //Class for Email Data Output Entity 
    public class ConstituentEmailOutput
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }
}