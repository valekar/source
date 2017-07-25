using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Orgler.EnterpriseOrgs
{
    public class SummaryOutputModel
    {
        public string ent_org_id { get; set; }
        public string ent_org_name { get; set; }
        public string ent_org_src_cd { get; set; }
        public string nk_ent_org_id { get; set; }
        public string ent_org_dsc { get; set; }
        public string lob_cnt { get; set; }
        public string srcsys_cnt { get; set; }
        public string created_by { get; set; }
        public string created_at { get; set; }
        public string last_modified_by { get; set; }
        public string last_modified_at { get; set; }
        public string last_modified_by_all { get; set; }
        public string last_modified_at_all { get; set; }
        public string last_reviewed_by { get; set; }
        public string last_reviewed_at { get; set; }
        public string data_stwrd_usr { get; set; }
        public string fr_rcnt_patrng_dt { get; set; }
        public string fr_totl_dntn_cnt { get; set; }
        public string fr_totl_dntn_val { get; set; }
        public string fr_rcncy_scr { get; set; }
        public string fr_freq_scr { get; set; }
        public string fr_dntn_scr { get; set; }
        public string fr_totl_rfm_scr { get; set; }
        public string bio_rcnt_patrng_dt { get; set; }
        public string bio_totl_dntn_cnt { get; set; }
        public string bio_totl_dntn_val { get; set; }
        public string bio_rcncy_scr { get; set; }
        public string bio_freq_scr { get; set; }
        public string bio_dntn_scr { get; set; }
        public string bio_totl_rfm_scr { get; set; }
        public string hs_rcnt_patrng_dt { get; set; }
        public string hs_totl_dntn_cnt { get; set; }
        public string hs_totl_dntn_val { get; set; }
        public string hs_rcncy_scr { get; set; }
        public string hs_freq_scr { get; set; }
        public string hs_dntn_scr { get; set; }
        public string hs_totl_rfm_scr { get; set; }
        public string mstr_cnt { get; set; }
        public string brid_cnt { get; set; }
        public List<BridgeCount> lt_brid_cnt { get; set; }
    }

    public class BridgeCount
    {
        public string line_of_service_cd { get; set; }
        public string los_cnt { get; set; }
        public string srcsys_cnt { get; set; }
    }
}
