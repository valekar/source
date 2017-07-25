using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Orgler.EnterpriseOrgs
{
    public class AffiliatedMasterBridgeInput
    {
        public string ent_org_id { get; set; }
        public string AffiliationLimit { get; set; }
        public string strLoadType { get; set; }
    }

    public class AffiliatedMasterBridgeOutputModel
    {
        public string ent_org_id { get; set; }
        public string cnst_mstr_id { get; set; }
        public string eosi_org_typ { get; set; }
        public string mstr_name { get; set; }
        public string mstr_addr_line1 { get; set; }
        public string mstr_addr_line2 { get; set; }
        public string mstr_city { get; set; }
        public string mstr_state { get; set; }
        public string mstr_zip { get; set; }
        public string mstr_address { get; set; }
        public string mstr_phone { get; set; }
        public string cdim_transform_id { get; set; }
        public string cnst_affil_strt_ts { get; set; }
        public string cnst_affil_end_ts { get; set; }
        public string mstr_fr_rcncy_scr { get; set; }
        public string mstr_fr_freq_scr { get; set; }
        public string mstr_fr_dntn_scr { get; set; }
        public string mstr_bio_rcncy_scr { get; set; }
        public string mstr_bio_freq_scr { get; set; }
        public string mstr_bio_dntn_scr { get; set; }
        public string mstr_hs_rcncy_scr { get; set; }
        public string mstr_hs_freq_scr { get; set; }
        public string mstr_hs_dntn_scr { get; set; }
        public string line_of_service_cd { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string bridge_name { get; set; }
        public string bridge_address { get; set; }
        public string bridge_phone { get; set; }
        public string bridge_rcncy_scr { get; set; }
        public string bridge_freq_scr { get; set; }
        public string bridge_fr_dntn_scr { get; set; }
        public string bridge_lob_rlshp_mgr { get; set; }
        public string mstr_fr_rcnt_patrng_dt { get; set; }
        public string mstr_fr_totl_dntn_cnt { get; set; }
        public string mstr_fr_totl_dntn_val { get; set; }
        public string mstr_bio_rcnt_patrng_dt { get; set; }
        public string mstr_bio_totl_dntn_cnt { get; set; }
        public string mstr_bio_totl_dntn_val { get; set; }
        public string mstr_hs_rcnt_patrng_dt { get; set; }
        public string mstr_hs_totl_dntn_cnt { get; set; }
        public string mstr_hs_totl_dntn_val { get; set; }
        public string bridge_rcnt_patrng_dt { get; set; }
        public string bridge_totl_dntn_cnt { get; set; }
        public string bridge_totl_dntn_val { get; set; }
        public string strx_transaction_key { get; set; }
        public string strx_is_previous { get; set; }
        public string transNotes { get; set; }
    }

    public class AffiliatedMasterBridgeOutput
    {
        public List<AffiliatedMasterBridgeOutputModel> lt_affil_res { get; set; }
        public AffiliatedMasterBridgeSummary summary_info { get; set; }
    }

    /* Name:AffiliationsOutputModel
   * Purpose: This class is the output model for knowing the affiliations of an enterprise */
    public class AffiliationsOutputModel
    {

        public string ent_org_id { get; set; }
        public string  cnst_mstr_id { get; set; }
        public string cnst_dsp_id { get; set; }
        public string cnst_org_nm { get; set; }
        public string cnst_addr_city_nm { get; set; }
        public string cnst_addr_state_cd { get; set; }
        public string cnst_affil_strt_dt { get; set; }
        public string transaction_key { get; set; }


    }
    /* Name: BridgeOutputModel
   * Purpose: This class is the output model for bridge */
    public class BridgeOutputModel
    {

        public string ent_org_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string cnst_org_nm { get; set; }

    }

    public class AffiliatedMasterBridgeExportOutputModel
    {
        public string mstr_typ { get; set; }
        public string dsp_verified { get; set; }
        public int mstr_typ_int { get; set; }
        public int dsp_verified_int { get; set; }
        public int ent_org_id { get; set; }
        public long cnst_mstr_id { get; set; }
        public string rec_typ { get; set; }
        public string eosi_org_typ { get; set; }
        public string line_of_service_cd { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string name { get; set; }
        public string addr_line1 { get; set; }
        public string addr_line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string cdim_transform_id { get; set; }
        public string cnst_affil_strt_ts { get; set; }
        public string cnst_affil_end_ts { get; set; }
        public string rcnt_patrng_dt { get; set; }
        public string totl_dntn_cnt { get; set; }
        public string totl_dntn_val { get; set; }
        public string mstr_fr_rcnt_patrng_dt { get; set; }
        public string mstr_bio_rcnt_patrng_dt { get; set; }
        public string mstr_hs_rcnt_patrng_dt { get; set; }
        public long? mstr_fr_totl_dntn_cnt { get; set; }
        public long? mstr_bio_totl_dntn_cnt { get; set; }
        public long? mstr_hs_totl_dntn_cnt { get; set; }
        public string mstr_fr_totl_dntn_val { get; set; }
        public string mstr_bio_totl_dntn_val { get; set; }
        public string mstr_hs_totl_dntn_val { get; set; }
        public string concatenated_val { get; set; }
        public int pros_ind { get; set; }
        public int act_val_ind { get; set; }
        public int inact_val_ind { get; set; }
        public int act_unval_ind { get; set; }
        public int inact_unval_ind { get; set; }
    }

    public class AffiliatedMasterBridgeSummary
    {
        public string ent_org_id { get; set; }
        public string total_brid_cnt { get; set; }
        public string pros_ind { get; set; }
        public string act_val_ind { get; set; }
        public string inact_val_ind { get; set; }
        public string act_unval_ind { get; set; }
        public string inact_unval_ind { get; set; }
        public string total_mstr_cnt { get; set; }
        public string str_concat_org_typ_cnt { get; set; }
    }
}
