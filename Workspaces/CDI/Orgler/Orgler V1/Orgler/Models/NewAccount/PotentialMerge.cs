using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.NewAccount
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
    }
}