using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.EnterpriseAccount
{
    //Tags Details
    public class TagDDList
    {
        public string tag_key { get; set; }
        public string tag { get; set; }
        public string start_dt { get; set; }
        public string end_dt { get; set; }
        public string dw_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string user_id { get; set; }
    }

    public class TagUpdateInputModel
    {
        public string ent_org_id { get; set; }
        public string tag { get; set; }
        public string action_type { get; set; }
        public string user_nm { get; set; }
    }

    public class TagUpdateOutputModel
    {
        public string o_outputMessage { get; set; }
        public long? o_outputTrans { get; set; }
    }

    //Transformation Details
    public class TransformationConditionInput
    {
        public string condition_typ { get; set; }
        public string pattern_string { get; set; }
    }

    public class TransformationUpdateInput
    {
        public string ent_org_id { get; set; }
        public string cdim_transform_id { get; set; }
        public string ent_org_branch { get; set; }
        public string active_ind { get; set; }
        public string req_typ { get; set; }
        public string notes { get; set; }
        public string userId { get; set; }
        public List<TransformationConditionInput> ltCondition { get; set; }
    }

    public class TransformationUpdateOutput
    {
        public string o_outputMessage { get; set; }
        public long? o_transaction_key { get; set; }
    }

    public class TransformationSmokeTestSummary
    {
        public string total_affil_cnt { get; set; }
        public string delta_affil_cnt { get; set; }
    }

    public class TransformationSmokeTestResults
    {
        public string ent_org_id { get; set; }
        public string ent_org_name { get; set; }
        public string cnst_mstr_id { get; set; }
        public string cnst_org_nm { get; set; }
        public string address { get; set; }
        public string cnst_phn_num { get; set; }
        public string affil_exist_ind { get; set; }
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
            req_typ = string.Empty;
            cnst_typ = string.Empty;
            notes = string.Empty;
            o_outputMessage = string.Empty;
            o_transaction_key = 0;
        }
    }

    public class OrgAffiliatorsOutput
    {
        public string o_outputMessage { get; set; }
        public Int64? o_transaction_key { get; set; }
    }

    public class HierarchyUpdateInput
    {
        public string superior_ent_org_key { get; set; }
        public string subodinate_ent_org_key { get; set; }
        public string rlshp_desc { get; set; }
        public string rlshp_cd { get; set; }
        public string userid { get; set; }
        public string action_type { get; set; }
    }

    public class HierarchyUpdateOutput
    {
        public string o_outputMessage { get; set; }
        public long? o_transaction_key { get; set; }
    }
}