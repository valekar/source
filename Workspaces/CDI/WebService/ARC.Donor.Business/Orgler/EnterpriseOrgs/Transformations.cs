using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Orgler.EnterpriseOrgs
{

    /* Name:TransformationOutputModel
* Purpose: This class is the output model for  Transformation of an enterprise. */
    public class TransformationOutputModel
    {
        public string ent_org_id { get; set; }
        public string cdim_transform_id { get; set; }
        public string strx_transform_id { get; set; }
        public string ent_org_branch { get; set; }
        public string act_ind { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string org_nm_transform_strt_dt { get; set; }
        public string org_nm_transform_end_dt { get; set; }
        public string transaction_key { get; set; }
        public string is_previous { get; set; }
        public string mstr_cnt { get; set; }
        public string conditional { get; set; }
    }

    public class TransformationModel
    {
        public List<TransformationOutputModel> output { get; set; }
        public string manual_affil_cnt { get; set; }
    }

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
}
