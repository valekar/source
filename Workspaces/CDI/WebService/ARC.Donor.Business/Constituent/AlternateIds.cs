using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Constituents
{
    public class AlternateIds
    {
        public string cnst_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_srcsys_scndry_id { get; set; }
        public string cnst_typ_cd { get; set; }
        public string alt_arc_srcsys_cd { get; set; }
        public string alt_cnst_srcsys_id { get; set; }
        public string alt_cnst_srcsys_scndry_id { get; set; }
        public string cnst_alt_id_strt_ts { get; set; }
        public string cnst_alt_id_end_dt { get; set; }
        public string dw_trans_ts { get; set; }
    }


    public class AlternateIdsInput
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_typ_cd { get;set; }
    }
}
