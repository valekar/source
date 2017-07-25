using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Constituents
{
    //class for org name data 
    public class OrgName
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }      
        public string cnst_org_nm_typ_cd { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_org_nm_strt_dt { get; set; }
        public string cnst_org_nm_seq { get; set; }
        public string cnst_org_nm_end_dt { get; set; }
        public string best_org_nm_ind { get; set; }
        public string cnst_org_nm { get; set; }
        public string cln_cnst_org_nm { get; set; }
        public string cnst_org_nm_best_los_ind { get; set; }
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
    }

    //class for org name input 
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
            UserName = string.Empty;
            ConstType = string.Empty;
            Notes = string.Empty;
            CaseNumber = 0;
            OldSourceSystemCode = string.Empty;
            OldOrgNameTypeCode = string.Empty;
            OldOrgNameBestLOSInd = 0;
            OrgName = string.Empty;
            SourceSystemCode = string.Empty;
            OrgNameTypeCode = string.Empty;
            OrgNameBestLOS = 0;
        }
    }

    //class for org name output
    public class ConstituentOrgNameOutput
    {
        public string o_transaction_key { get; set; }
        public string o_outputMessage { get; set; }
    }
}
