using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Constituents
{
    //class for name data 
    public class Name
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
        //added by srini
        public string transNotes { get; set; }
        public string locator_prsn_nm_key { get; set; }
        //for missy
        public string distinct_count { get; set; }
        //srini  - added assessment code
        public string assessmnt_ctg { get; set; }
        //written by srini
        public Name()
        {
            this.transNotes = string.Empty;
        }
    }

    //class for name input 
    public class ConstituentNameInput
    {
        public string RequestType { get; set; }
        public Int32 MasterID { get; set; }
        public string UserName { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public int    CaseNumber { get; set; }
        public string OldSourceSystemCode { get; set; }
        public string OldNameTypeCode { get; set; }
        public string OldBestLOSInd { get; set; }
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
        public byte   BestLOS { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }

        public ConstituentNameInput()
        {
            PrefixName = string.Empty;
            UserName = "";
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            UserName = p.GetUserName(); //p.Identity.Name;
            OldSourceSystemCode = string.Empty;
            OldNameTypeCode = string.Empty;
            OldBestLOSInd = "0";
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
    public class ConstituentNameOutput
    {
        public string o_transaction_key { get; set; }
        public string o_outputMessage { get; set; }
    }

    public class ConstituentOrgNameInput
    {
        public string RequestType { get; set; }
        public Int64 MasterID { get; set; }
        public string UserName { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public Int64? CaseNumber { get; set; }
        public string OldSourceSystemCode { get; set; }
        public string OldOrgNameTypeCode { get; set; }
        public byte OldOrgNameBestLOSInd { get; set; }
        public string OrgName { get; set; }
        public string SourceSystemCode { get; set; }
        public string OrgNameTypeCode { get; set; }
        public byte? OrgNameBestLOS { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }

        public ConstituentOrgNameInput()
        {
            UserName = "";
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            UserName = p.Identity.Name;
            OldSourceSystemCode = string.Empty;
            OldOrgNameTypeCode = string.Empty;
            OldOrgNameBestLOSInd = 0;
            OrgName = string.Empty;
            SourceSystemCode = string.Empty;
            OrgNameTypeCode = string.Empty;
            OrgNameBestLOS = 0;
        }
    }

    public class OrgName
    {
        public string act_ind { get; set; }
        public string appl_src_cd { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string best_org_nm_ind { get; set; }
        public string cln_cnst_org_nm { get; set; }
        public string cnst_mstr_id { get; set; }
        public string cnst_org_nm { get; set; }
        public string cnst_org_nm_best_los_ind { get; set; }
        public string cnst_org_nm_end_dt { get; set; }
        public string cnst_org_nm_seq { get; set; }
        public string cnst_org_nm_strt_dt { get; set; }
        public string cnst_org_nm_typ_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string inactive_ind { get; set; }
        public string is_previous { get; set; }
        public string load_id { get; set; }
        public string row_stat_cd { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string trans_key { get; set; }
        public string trans_status { get; set; }
        public string transaction_key { get; set; }
        public string unique_trans_key { get; set; }
        public string user_id { get; set; }
        public string transNotes { get; set; }
        //for missy
        public string distinct_count { get; set; }

        public OrgName()
        {
            this.transNotes = string.Empty;
        }
    }

}