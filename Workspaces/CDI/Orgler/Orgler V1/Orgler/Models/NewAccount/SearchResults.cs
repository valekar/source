using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Orgler.Models.NewAccount
{
    //Model to store the search results for New Account search
    public class SearchResults
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
        //public List<listString> listNames { get; set; }
        //public List<listString> listAddresses { get; set; }
        //public List<listString> listPhones { get; set; }
        //public List<listString> listEmails { get; set; }
        public List<listString> listNAICSCodes { get; set; }
        public List<listString> listNAICSDesc { get; set; }
        public List<listString> listNAICSRuleKeyword { get; set; }
        public bool has_potential_merge { get; set; }
        public bool has_potential_unmerge { get; set; }
        public string pot_unmerge_rsn { get; set; }
        public string status { get; set; }
        public List<NAICSCodesNDesc> listNAICSCodesAndDesc { get; set; }
        public string mstr_metric_ts { get; set; }

      
        public string confirmImgPath 
        { 
            get
            {
                return ConfigurationManager.AppSettings["ResourceURL"] + "Images/ConfirmButton.png";
            }
        }
    }

    public class listString
    {
        public string strText { get; set; }
        public string status { get; set; }
    }
    public class NAICSCodesNDesc
    {
        public string naicsCode { get; set; }
        public string naicsTitle { get; set; }
        public string status { get; set; }
    }
    public class ExportSearchResults
    {      
        public string Master_ID { get; set; }       
        public string Name { get; set; }
        public string Address { get; set; }       
        public string NAICS_Code { get; set; }
        public string NAICS_Title { get; set; }
        public string NAICS_Match_Keyword { get; set; }
        public string NAICS_Status { get; set; }
        public string Action { get; set; }
    }

}   