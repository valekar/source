using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Common
{
    public class GetMasterNAICSDetailsInput
    {
        public string cnst_mstr_id { get; set; }
        public string source_system_id { get; set; }
        public string source_system_code { get; set; }
        public string naics_cd { get; set; }
    }

    public class GetMasterNAICSDetailsOutput
    {
        public string cnst_mstr_id { get; set; }
        public string source_system_id { get; set; }
        public string source_system_code { get; set; }
        public string naics_cd { get; set; }
        public string naics_indus_title { get; set; }
        public string naics_indus_dsc { get; set; }
        public string conf_weightg { get; set; }
        public string rule_keywrd { get; set; }
    }
}