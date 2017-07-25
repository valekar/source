using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.NewAccount
{
    public class AddNAICSCodeInput
    {
        public string cnst_mstr_id { get; set; }
        public string source_system_id { get; set; }
        public string source_system_code { get; set; }
        public List<string> new_naics_codes { get; set; }
        public string added_naics_names { get; set; }
        public string usr_nm { get; set; }
    }

    public class NAICSCode
    {
        public string naics_key { get; set; }
        public string naics_cd { get; set; }
        public string naics_indus_title { get; set; }
        public string naics_lvl { get; set; }
        public string parent_naics_cd { get; set; }
        public List<NAICSCode> children = new List<NAICSCode>();
        public List<NAICSCode> GetSubNAICSCode()
        {
            return children;
        }
        public void AddSubNAICSCode(NAICSCode ci)
        {
            children.Add(ci);
        }
    }
}