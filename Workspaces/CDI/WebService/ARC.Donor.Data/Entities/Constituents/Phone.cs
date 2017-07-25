using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Entities.Constituents
{
    public class Phone
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_phn_num { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_phn_extsn_num { get; set; }
        public string phn_typ_cd { get; set; }
        public string cntct_stat_typ_cd { get; set; }
        public string cnst_phn_best_ind { get; set; }
        public string cnst_phn_strt_ts { get; set; }
        public string cnst_phn_end_dt { get; set; }
        public string best_phn_ind { get; set; }
        public string cnst_phn_best_los_ind { get; set; }
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

    // locator phone key
        public string locator_phn_key { get; set; }

    }

    //class for name input 
    public class ConstituentPhoneInput
    {
        public string RequestType { get; set; }
        public Int32 MasterID { get; set; }
        public string UserName { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public int CaseNumber { get; set; }
        public string OldSourceSystemCode { get; set; }
        public string OldPhoneTypeCode { get; set; }
        public string OldBestLOSInd { get; set; }
        public string PhoneNumber { get; set; }
        public string SourceSystemCode { get; set; }
        public string PhoneTypeCode { get; set; }
        public byte BestLOS { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }

        public ConstituentPhoneInput()
        {
            UserName = string.Empty;
            ConstType = string.Empty;
            Notes = string.Empty;
            CaseNumber = 0;
            OldSourceSystemCode = string.Empty;
            OldPhoneTypeCode = string.Empty;
            OldBestLOSInd = "0";
            PhoneNumber = string.Empty;
            SourceSystemCode = string.Empty;
            PhoneTypeCode = string.Empty;
            BestLOS = 0;
        }
    }

    //class for name output
    public class ConstituentPhoneOutput
    {
        public string o_transaction_key { get; set; }
        public string o_outputMessage { get; set; }
    }
}