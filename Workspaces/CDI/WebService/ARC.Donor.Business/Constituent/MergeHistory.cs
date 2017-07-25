using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Business.Constituents
{
    public class MergeHistory
    {
        public string new_mstr_id { get; set; }
        public string appl_src_cd { get; set; }
        public string srcsys_cnst_uid { get; set; }
        public string cdi_batch_id { get; set; }
        public string cnst_mstr_id { get; set; }
        public string cnst_dsp_id { get; set; }
        public string cnst_type { get; set; }
        public string intnl_srcsys_grp_id { get; set; }
        public string alert_type_cd { get; set; }
        public string alert_msg_txt { get; set; }
        public string reprocess_ind { get; set; }
        public string merge_sts_cd { get; set; }
        public string merge_msg_txt { get; set; }
        public string steward_actn_cd { get; set; }
        public string steward_actn_dsc { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string trans_note { get; set; }
        public string trans_ts { get; set; }
        public string dw_trans_ts { get; set; }

    }
}