using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities.Constituents
{
    public class MasteringAttempts
    {
        public string srcsys_unique_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string new_cnst_ind { get; set; }
        public string min_quality_ind { get; set; }
        public string sent_to_ln_ind { get; set; }
        public string cnst_dsp_id { get; set; }
        public string cnst_dsp_id_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string load_id { get; set; }
        public string cnst_mstr_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }

    }
}