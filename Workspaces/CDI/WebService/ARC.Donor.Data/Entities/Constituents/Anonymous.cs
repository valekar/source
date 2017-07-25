using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Constituents
{
    public class Anonymous
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_chrctrstc_typ_cd { get; set; }
        public string cnst_chrctrstc_strt_dt { get; set; }
        public string cnst_chrctrstc_seq { get; set; }
        public string line_of_service_cd { get; set; }
        public string cnst_chrctrstc_end_dt { get; set; }
        public string cnst_chrctrstc_val { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
    }
}
