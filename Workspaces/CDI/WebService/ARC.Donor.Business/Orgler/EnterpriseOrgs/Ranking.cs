using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Orgler.EnterpriseOrgs
{
    public class RankingOutputModel
    {
        public string ent_org_id { get; set; }
        public string ent_org_rnk_key { get; set; }
        public string org_rnk { get; set; }
        public string org_rnk_prvdr { get; set; }
        public string org_rnk_publsh_yr { get; set; }
        public string act_ind { get; set; }
        public string create_by { get; set; }
        public string create_ts { get; set; }
        public string updt_by { get; set; }
        public string updt_ts { get; set; }
        public string org_rnk_map_strt_ts { get; set; }
        public string org_rnk_map_end_dt { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
    }
}
