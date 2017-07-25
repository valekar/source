using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.EnterpriseAccount
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
        public string ent_org_grp_typ { get; set; }
        public string mstr_cnt { get; set; }
        public string brid_cnt { get; set; }
        public List<BridgeCount> lt_brid_cnt { get; set; }
        public string section_nm { get; set; }

        public SummaryOutputModel()
        {
            section_nm = "EntOrgDetail";
        }
    }

    public class BridgeCount
    {
        public string line_of_service_cd { get; set; }
        public string los_cnt { get; set; }
        public string srcsys_cnt { get; set; }
    }

    public class TransformationOutputModel
    {
        public string ent_org_id { get; set; }
        public string cdim_transform_id { get; set; }
        public string strx_transform_id { get; set; }
        public string ent_org_branch { get; set; }
        public string act_ind { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string org_nm_transform_strt_dt { get; set; }
        public string org_nm_transform_end_dt { get; set; }
        public string transaction_key { get; set; }
        public string is_previous { get; set; }
        public string mstr_cnt { get; set; }
        public string conditional { get; set; }
        public string section_nm { get; set; }
        public string strTransNotes
        {
            get
            {
                if (this.transaction_key != null)
                {
                    if (this.is_previous.Equals("1"))
                    {
                        return "Previous record for transaction key #" + transaction_key;
                    }
                    else
                    {
                        return "New record for transaction key #" + transaction_key;
                    }
                }
                return null;
            }
        }

        public TransformationOutputModel()
        {
            section_nm = "TransformationDetail";
        }
    }

    public class TransformationModel
    {
        public List<TransformationOutputModel> output { get; set; }
        public string manual_affil_cnt { get; set; }
    }
    
    //Tag Details
    public class TagOutputModel
    {
        public string ent_org_id { get; set; }
        public string tag_key { get; set; }
        public string tag { get; set; }
        public string dw_trans_ts { get; set; }
        public string start_dt { get; set; }
        public string end_dt { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string row_stat_cd { get; set; }
        public string section_nm { get; set; }

        public TagOutputModel()
        {
            section_nm = "Tags";
        }
    }

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
        public string mstr_residntl_flg { get; set; }
        public string section_nm { get; set; }
        public string intrnl_prod_sys 
        { 
            get
            {
                return (Convert.ToInt32(mstr_fr_dntn_scr) + Convert.ToInt32(mstr_bio_dntn_scr) + Convert.ToInt32(mstr_hs_dntn_scr)) > 0 ? "Active" : "Dormant";
            }
        }
        public string extn_sys_flag { get; set; }
        public int act_valid_cnt { get; set; }
        public int inact_valid_cnt { get; set; }
        public int prospect_cnt { get; set; }
        public int act_invalid_cnt { get; set; }
        public int inact_invalid_cnt { get; set; } 

        public AffiliatedMasterBridgeOutputModel()
        {
            section_nm = "MasterBridgeMaster";
            extn_sys_flag = "0";
            act_valid_cnt = 0;
            inact_valid_cnt = 0;
            prospect_cnt = 0;
            act_invalid_cnt = 0;
            inact_invalid_cnt = 0;
        }
    }

    public class AffiliatedMasterBridgeLocationOutputModel
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
        public string section_nm { get; set; }

        public AffiliatedMasterBridgeLocationOutputModel()
        {
            section_nm = "MasterBridgeLocation";
        }
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
        public string lob_rlshp_mgr { get; set; }
        public string mstr_residntl_flg { get; set; }
        public int pros_ind { get; set; }
        public int act_val_ind { get; set; }
        public int inact_val_ind { get; set; }
        public int act_unval_ind { get; set; }
        public int inact_unval_ind { get; set; }
    }

    public class AffiliatedMasterBridgeOutput
    {
        public List<AffiliatedMasterBridgeOutputModel> lt_affil_res { get; set; }
        public AffiliatedMasterBridgeSummary summary_info { get; set; }
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
        public string section_nm { get; set; }

        public OrgHierarchyOutputModel()
        {
            section_nm = "EntOrgHierarchy";
        }
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
        public string lvl2_start_dt { get; set; }
        public string lvl2_end_dt { get; set; }
        public string lvl2_dw_trans_ts { get; set; }
        public string lvl2_row_stat_cd { get; set; }
        public string lvl2_appl_src_cd { get; set; }
        public string lvl2_load_id { get; set; }
        public string lvl2_trans_key { get; set; }
        public string lvl2_user_id { get; set; }
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
        public string lvl3_start_dt { get; set; }
        public string lvl3_end_dt { get; set; }
        public string lvl3_dw_trans_ts { get; set; }
        public string lvl3_row_stat_cd { get; set; }
        public string lvl3_appl_src_cd { get; set; }
        public string lvl3_load_id { get; set; }
        public string lvl3_trans_key { get; set; }
        public string lvl3_user_id { get; set; }
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
        public string lvl4_start_dt { get; set; }
        public string lvl4_end_dt { get; set; }
        public string lvl4_dw_trans_ts { get; set; }
        public string lvl4_row_stat_cd { get; set; }
        public string lvl4_appl_src_cd { get; set; }
        public string lvl4_load_id { get; set; }
        public string lvl4_trans_key { get; set; }
        public string lvl4_user_id { get; set; }
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
        public string lvl5_start_dt { get; set; }
        public string lvl5_end_dt { get; set; }
        public string lvl5_dw_trans_ts { get; set; }
        public string lvl5_row_stat_cd { get; set; }
        public string lvl5_appl_src_cd { get; set; }
        public string lvl5_load_id { get; set; }
        public string lvl5_trans_key { get; set; }
        public string lvl5_user_id { get; set; }
        public string section_nm { get; set; }

        public OrganizationHierarchyOutputModel()
        {
            section_nm = "EntOrgHierarchy";
        }
    }

    public class GetNAICSDetailsOutput
    {
        public string ent_org_id { get; set; }
        public string cnst_mstr_id { get; set; }
        public string sts { get; set; }
        public string naics_cd { get; set; }
        public string naics_indus_title { get; set; }
        public string naics_indus_dsc { get; set; }
        public string rule_keywrd { get; set; }
        public string conf_weightg { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string section_nm { get; set; }

        public GetNAICSDetailsOutput()
        {
            section_nm = "NaicsCodeStew";
        }
    }

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
        public string section_nm { get; set; }

        public RankingOutputModel()
        {
            section_nm = "RankingStew";
        }
    }

    public class TransactionHistoryOutputModel
    {
        public string trans_key { get; set; }
        public string trans_typ_dsc { get; set; }
        public string sub_trans_typ_dsc { get; set; }
        public string sub_trans_actn_typ { get; set; }
        public string trans_stat { get; set; }
        public string trans_note { get; set; }
        public string ent_org_id { get; set; }
        public string user_id { get; set; }
        public string trans_create_ts { get; set; }
        public string trans_last_modified_ts { get; set; }
        public string approved_by { get; set; }
        public string approved_dt { get; set; }
        public string section_nm { get; set; }

        public TransactionHistoryOutputModel()
        {
            section_nm = "TransactionHistory";
        }
    }

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
        public string section_nm { get; set; }

        public RFMDetails()
        {
            section_nm = "RFMDetails";
        }
    }

    public class ExportDetailsInput
    {
        public string strEntOrgId { get; set; }
        public string strSectionName { get; set; }
    }
}