using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Orgler.EnterpriseOrgs
{
    public class EnterpriseOrgInputSearchModel
    {
        public string EnterpriseOrgID { get; set; }
        public string EnterpriseOrgName { get; set; }
        public List<RankInput> RankProvider { get; set; }
        public string RankTo { get; set; }
        public string RankFrom { get; set; }
        public List<string> SourceSystem { get; set; }
        public List<string> ChapterSystem { set; get; }
        public List<string> listNaicsCodes { get; set; }
        public List<string> Tags { get; set; }
        public bool IncludeSuperior { get; set; }
        public bool IncludeSubordinate { get; set; }
        public bool ExcludeTransformations { get; set; }
        public string Username { get; set; }
        public bool RecentChanges { get; set; }
    }

    public class RankInput
    {
        public string Provider { get; set; }
        public string Year { get; set; }
    }

    public class ListEnterpriseOrgInputSearchModel
    {
        public List<EnterpriseOrgInputSearchModel> EnterpriseOrgInputSearchModel { get; set; }
        public string AnswerSetLimit { get; set; }
    }

    public class EnterpriseOrgOutputSearchResults
    {
        public string ent_org_id { get; set; }
        public string ent_org_name { get; set; }
        public string ent_org_src_cd { get; set; }
        public string nk_ent_org_id { get; set; }
        public string srcsys_cnt { get; set; }
        public string affil_cnt { get; set; }
        public string transformation_cnt { get; set; }
        public string trans_key { get; set; }
        public List<listString> listTags { get; set; }
        public List<listString> listNAICSCodes { get; set; }
        public List<listString> listNAICSDesc { get; set; }
        public string created_by { get; set; }
        public string created_at { get; set; }
        public string last_modified_by { get; set; }
        public string last_modified_at { get; set; }
        public string last_modified_by_all { get; set; }
        public string last_modified_at_all { get; set; }
        public string last_reviewed_by { get; set; }
        public string last_reviewed_at { get; set; }
        public string data_stwrd_usr { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string load_id { get; set; }
        public string parent_ent_org_id { get; set; }

    }
}
