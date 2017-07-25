using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.NewAccount
{
    public class UpdateNAICSCodes
    {
        public string cnst_mstr_id { get; set; }
        public string source_system_id { get; set; }
        public string source_system_code { get; set; }
        public List<string> added_naics_codes { get; set; }
        public List<string> approved_naics_codes { get; set; }
        public List<string> rejected_naics_codes { get; set; }
        public string usr_nm { get; set; }
    }
}