using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Common
{

    public class ListName
    {
        public string strText { get; set; }
        public string status { get; set; }
    }

    public class ListEmail
    {
        public string strText { get; set; }
        public string status { get; set; }
    }

    public class ListNAICSCode
    {
        public string strText { get; set; }
        public string status { get; set; }
    }

    public class ListNAICSDesc
    {
        public string strText { get; set; }
        public string status { get; set; }
    }

    public class RootObject
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
        public List<ListName> listNames { get; set; }
        public List<object> listAddresses { get; set; }
        public List<object> listPhones { get; set; }
        public List<ListEmail> listEmails { get; set; }
        public List<ListNAICSCode> listNAICSCodes { get; set; }
        public List<ListNAICSDesc> listNAICSDesc { get; set; }
        public bool has_potential_merge { get; set; }
        public bool has_potential_unmerge { get; set; }
        public string pot_unmerge_rsn { get; set; }
        public string status { get; set; }
    }

    public class NaicsSuggestionsOutput
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
        public string listNames { get; set; }
        public string listAddresses { get; set; }
        public string listPhones { get; set; }
        public string listEmails { get; set; }
        public string listNAICSCodes { get; set; }
        public string listNAICSDesc { get; set; }
        public bool has_potential_merge { get; set; }
        public bool has_potential_unmerge { get; set; }
        public string pot_unmerge_rsn { get; set; }
        public string status { get; set; }
    }


}