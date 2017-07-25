using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Common
{
    public class UpdateNAICSCodes
    {
        public string cnst_mstr_id { get; set; }
        public string source_system_id { get; set; }
        public string source_system_code { get; set; }
        public List<string> added_naics_codes { get; set; }
        public List<string> approved_naics_codes { get; set; }
        public List<string> rejected_naics_codes { get; set; }
        public string cnst_org_nm { get; set; }
        public string usr_nm { get; set; }
    }
    public class NAICSRecentUpdate
    {
        public List<string> added_naics_codes { get; set; }
        public List<string> added_naics_titles { get; set; }
        public List<string> approved_naics_codes { get; set; }
        public List<string> approved_naics_titles { get; set; }
        public List<string> rejected_naics_codes { get; set; } 
        public List<string> rejected_naics_titles { get; set; }      
        public string usr_nm { get; set; }
    }

    public class NAICSRecentData
    {
        public string naics_codes { get; set; }
        public string naics_titles { get; set; }
        public string naics_status { get; set; }       
        public string usr_nm { get; set; }

    }
}