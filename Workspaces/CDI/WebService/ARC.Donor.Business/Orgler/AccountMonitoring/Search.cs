using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Orgler.AccountMonitoring
{
    /* Name: NewAccountsInputModel
     * Purpose: This class is the input model for searching new accounts */
    public class NewAccountsInputModel
    {
        public string los { get; set; }
        public string createdDateFrom { get; set; }
        public string createdDateTo { get; set; }
        public string naicsStatus { get; set; }
        public string masteringType { get; set; }
        public List<string> listRuleKeyword { get; set; }
        public string enterpriseOrgAssociation { get; set; }
        public string AnswerSetLimit { get; set; }
        public List<string> listNaicsCodes { get; set; }
        public string enterpriseOrgId { get; set; }
    }

    /* Name: NewAccountsOutputModel
     * Purpose: This class is the output model for the search results of new accounts */
    public class NewAccountsOutputModel
    {
        public string line_of_service_cd { get; set; }
        public string source_system_code { get; set; }
        public string source_system_id { get; set; }
        public string master_id { get; set; }
        public string mastering_result { get; set; }
        public string lexis_nexis_id { get; set; }
        public string monetary_value { get; set; }
        public string rfm_score { get; set; }
        public string ent_org_id { get; set; }
        public string ent_org_name { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public List<listString> listNAICSCodes { get; set; }
        public List<listString> listNAICSDesc { get; set; }
        public List<listString> listNAICSRuleKeyword { get; set; }
        public bool has_potential_merge { get; set; }
        public bool has_potential_unmerge { get; set; }
        public string pot_unmerge_rsn { get; set; }
        public string status { get; set; }
        public string mstr_metric_ts { get; set; }
    }

    

    /* Name: TopOrgsInputModel
    * Purpose: This class is the input model for searching top organizations. */
    public class TopOrgsInputModel
    {
        public string los { get; set; }
        public string naics_cd { get; set; }
        public string naicsStatus { get; set; }
        public string rfm_scr { get; set; }
        public List<string> listRuleKeyword { get; set; }
        public List<string> listNaicsCodes { get; set; }
        public string enterpriseOrgAssociation { get; set; }
        public string AnswerSetLimit { get; set; }
    }

    /* Name:TopOrgsOutputModel
     * Purpose: This class is the output model for the search results of top organizations. */
    public class TopOrgsOutputModel
    {
        public string line_of_service_cd { get; set; }
        public string master_id { get; set; }
        public string lexis_nexis_id { get; set; }
        public string monetary_value { get; set; }
        public string freq_value { get; set; }
        public string mst_rcnt_patrng_dt { get; set; }
        public string rfm_score { get; set; }
        public string ent_org_id { get; set; }
        public string ent_org_name { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public List<listString> listNAICSCodes { get; set; }
        public List<listString> listNAICSDesc { get; set; }
        public List<listString> listNAICSRuleKeyword { get; set; }
        public bool has_potential_merge { get; set; }
        public bool has_potential_unmerge { get; set; }
        public string pot_unmerge_rsn { get; set; }
        public string status { get; set; }
    }
}
