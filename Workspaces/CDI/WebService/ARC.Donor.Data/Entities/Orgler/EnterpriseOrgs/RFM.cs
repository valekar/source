using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs
{
    public class RFMDetails
    {
        public string ent_org_id { get; set; }
		public string line_of_srvc_cd { get; set; }
		public string rcnt_patrng_dt { get; set; }
		public string totl_dntn_cnt { get; set; }
		public string totl_dntn_val { get; set; }
		public string rcncy_scr { get; set; }
		public string freq_scr { get; set; }
		public string dntn_scr { get; set; }
		public string totl_rfm_scr { get; set; }
		public string rfm_scr_strt_ts { get; set; }
		public string rfm_scr_end_dt { get; set; }
		public string srcsys_trans_ts { get; set; }
		public string dw_srcsys_trans_ts { get; set; }
		public string row_stat_cd { get; set; }
		public string appl_src_cd { get; set; }
        public string load_id { get; set; }
    }
}
