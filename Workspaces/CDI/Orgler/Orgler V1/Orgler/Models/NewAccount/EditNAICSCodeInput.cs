using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.NewAccount
{
    public class EditNAICSCodeInput
    {
        public string cnst_mstr_id { get; set; }
        public string source_system_id { get; set; }
        public string source_system_code { get; set; }
        public string naics_cd { get; set; }
        public string status { get; set; }
        public string usr_nm { get; set; }
    }
}