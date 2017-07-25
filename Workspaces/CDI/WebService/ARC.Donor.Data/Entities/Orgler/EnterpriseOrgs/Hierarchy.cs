using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs
{

    /* Name:OrgHierarchyOutputModel
* Purpose: This class is the output model for knowing the hierarchy of an enterprise. */
    public class OrgHierarchyOutputModel
    {

        public string superior_ent_org_key { get; set; }
        public string subord_ent_org_key { get; set; }
        public string superior_ent_org_name { get; set; }
        public string subord_ent_org_name { get; set; }
        public string rlshp_typ_cd { get; set; }
        public string rlshp_typ_desc { get; set; }
        public string start_dt { get; set; }
        public string end_dt { get; set; }
        public string dw_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string sup_fr_rcnt_patrng_dt { get; set; }
        public string sup_fr_totl_dntn_cnt { get; set; }
        public string sup_fr_totl_dntn_val { get; set; }
        public string sup_bio_rcnt_patrng_dt { get; set; }
        public string sup_bio_totl_dntn_cnt { get; set; }
        public string sup_bio_totl_dntn_val { get; set; }
        public string sup_hs_rcnt_patrng_dt { get; set; }
        public string sup_hs_totl_dntn_cnt { get; set; }
        public string sup_hs_totl_dntn_val { get; set; }
        public string sub_fr_rcnt_patrng_dt { get; set; }
        public string sub_fr_totl_dntn_cnt { get; set; }
        public string sub_fr_totl_dntn_val { get; set; }
        public string sub_bio_rcnt_patrng_dt { get; set; }
        public string sub_bio_totl_dntn_cnt { get; set; }
        public string sub_bio_totl_dntn_val { get; set; }
        public string sub_hs_rcnt_patrng_dt { get; set; }
        public string sub_hs_totl_dntn_cnt { get; set; }
        public string sub_hs_totl_dntn_val { get; set; }

    }

    public class OrganizationHierarchyOutputModel
    {
        public string lvl1_ent_org_id { get; set; }
        public string lvl1_ent_org_name { get; set; }
        public string lvl1_fr_rcnt_patrng_dt { get; set; }
        public string lvl1_fr_totl_dntn_cnt { get; set; }
        public string lvl1_fr_totl_dntn_val { get; set; }
        public string lvl1_bio_rcnt_patrng_dt { get; set; }
        public string lvl1_bio_totl_dntn_cnt { get; set; }
        public string lvl1_bio_totl_dntn_val { get; set; }
        public string lvl1_phss_rcnt_patrng_dt { get; set; }
        public string lvl1_phss_totl_dntn_cnt { get; set; }
        public string lvl1_phss_totl_dntn_val { get; set; }
        public string lvl2_ent_org_id { get; set; }
        public string lvl2_ent_org_name { get; set; }
        public string lvl2_fr_rcnt_patrng_dt { get; set; }
        public string lvl2_fr_totl_dntn_cnt { get; set; }
        public string lvl2_fr_totl_dntn_val { get; set; }
        public string lvl2_bio_rcnt_patrng_dt { get; set; }
        public string lvl2_bio_totl_dntn_cnt { get; set; }
        public string lvl2_bio_totl_dntn_val { get; set; }
        public string lvl2_phss_rcnt_patrng_dt { get; set; }
        public string lvl2_phss_totl_dntn_cnt { get; set; }
        public string lvl2_phss_totl_dntn_val { get; set; }
        public string lvl3_ent_org_id { get; set; }
        public string lvl3_ent_org_name { get; set; }
        public string lvl3_fr_rcnt_patrng_dt { get; set; }
        public string lvl3_fr_totl_dntn_cnt { get; set; }
        public string lvl3_fr_totl_dntn_val { get; set; }
        public string lvl3_bio_rcnt_patrng_dt { get; set; }
        public string lvl3_bio_totl_dntn_cnt { get; set; }
        public string lvl3_bio_totl_dntn_val { get; set; }
        public string lvl3_phss_rcnt_patrng_dt { get; set; }
        public string lvl3_phss_totl_dntn_cnt { get; set; }
        public string lvl3_phss_totl_dntn_val { get; set; }
        public string lvl4_ent_org_id { get; set; }
        public string lvl4_ent_org_name { get; set; }
        public string lvl4_fr_rcnt_patrng_dt { get; set; }
        public string lvl4_fr_totl_dntn_cnt { get; set; }
        public string lvl4_fr_totl_dntn_val { get; set; }
        public string lvl4_bio_rcnt_patrng_dt { get; set; }
        public string lvl4_bio_totl_dntn_cnt { get; set; }
        public string lvl4_bio_totl_dntn_val { get; set; }
        public string lvl4_phss_rcnt_patrng_dt { get; set; }
        public string lvl4_phss_totl_dntn_cnt { get; set; }
        public string lvl4_phss_totl_dntn_val { get; set; }
        public string lvl5_ent_org_id { get; set; }
        public string lvl5_ent_org_name { get; set; }
        public string lvl5_fr_rcnt_patrng_dt { get; set; }
        public string lvl5_fr_totl_dntn_cnt { get; set; }
        public string lvl5_fr_totl_dntn_val { get; set; }
        public string lvl5_bio_rcnt_patrng_dt { get; set; }
        public string lvl5_bio_totl_dntn_cnt { get; set; }
        public string lvl5_bio_totl_dntn_val { get; set; }
        public string lvl5_phss_rcnt_patrng_dt { get; set; }
        public string lvl5_phss_totl_dntn_cnt { get; set; }
        public string lvl5_phss_totl_dntn_val { get; set; }
    }

    public class HierarchyUpdateInput
    {
        public string superior_ent_org_key { get; set; }
        public string subodinate_ent_org_key { get; set; }
        public string rlshp_desc { get; set; }
        public string rlshp_cd { get; set; }
        public string userid { get; set; }
        public string action_type { get; set; }
    }

    public class HierarchyUpdateOutput
    {
        public string o_outputMessage { get; set; }
        public long? o_transaction_key { get; set; }
    }
}
