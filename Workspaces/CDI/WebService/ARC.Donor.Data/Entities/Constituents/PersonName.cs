using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Constituents
{
    //class for person name data 
    public class PersonName
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_prsn_seq { get; set; }
        public string cnst_prsn_nm_typ_cd { get; set; }
        public string cnst_nm_strt_dt { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string line_of_service_cd { get; set; }
        public string cnst_prsn_nm_end_dt { get; set; }
        public string best_prsn_nm_ind { get; set; }
        public string cnst_prsn_first_nm { get; set; }
        public string cnst_prsn_middle_nm { get; set; }
        public string cnst_prsn_last_nm { get; set; }
        public string cnst_prsn_prefix_nm { get; set; }
        public string cnst_prsn_suffix_nm { get; set; }
        public string cnst_prsn_full_nm { get; set; }
        public string cnst_prsn_nick_nm { get; set; }
        public string cnst_prsn_mom_maiden_nm { get; set; }
        public string cnst_alias_out_saltn_nm { get; set; }
        public string cnst_alias_in_saltn_nm { get; set; }
        public string cnst_prsn_nm_best_los_ind { get; set; }
        public string trans_key { get; set; }
        public string act_ind { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
        public string assessmnt_ctg { get; set; }
        //added for arc best
        public string locator_prsn_nm_key { get; set; }
    }

    //class for name input 
    public class ConstituentPersonNameInput
    {
        public string RequestType { get; set; }
        public Int64 MasterID { get; set; }
        public string UserName { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public Int64? CaseNumber { get; set; }
        public string OldSourceSystemCode { get; set; }
        public string OldNameTypeCode { get; set; }
        public byte? OldBestLOSInd { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PrefixName { get; set; }
        public string SuffixName { get; set; }
        public string NickName { get; set; }
        public string MaidenName { get; set; }
        public string FullName { get; set; }
        public string InSalutationName { get; set; }
        public string OutSalutationName { get; set; }
        public string SourceSystemCode { get; set; }
        public string NameTypeCode { get; set; }
        public byte? BestLOS { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }

        public ConstituentPersonNameInput()
        {
            PrefixName = string.Empty;
            UserName = string.Empty;
            ConstType = string.Empty;
            Notes = string.Empty;
            CaseNumber = 0;
            OldSourceSystemCode = string.Empty;
            OldNameTypeCode = string.Empty;
            OldBestLOSInd = 0;
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            PrefixName = string.Empty;
            SuffixName = string.Empty;
            NickName = string.Empty;
            MaidenName = string.Empty;
            FullName = string.Empty;
            InSalutationName = string.Empty;
            OutSalutationName = string.Empty;
            SourceSystemCode = string.Empty;
            NameTypeCode = string.Empty;
            BestLOS = 0;

        }
    }

    //class for name output
    public class ConstituentPersonNameOutput
    {
        public string o_transaction_key { get; set; }
        public string o_outputMessage { get; set; }
    }
}
