using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Common
{
    public class PotentialMergeInput
    {
        public string master_id { get; set; }
    }

    public class PotentialMergeOutput
    {
        public string cnst_mstr_id { get; set; }
        public string pot_merge_mstr_id { get; set; }
        public string pot_merge_mstr_nm { get; set; }
        public string pot_merge_rsn { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string dsp_id { get; set; }
    }
}