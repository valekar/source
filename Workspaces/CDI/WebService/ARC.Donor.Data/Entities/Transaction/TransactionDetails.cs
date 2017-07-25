using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Transaction
{
    public class TransactionMerge
    {
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
        public string user_id { get; set; }
        public string transaction_key { get; set; }
    }

    public class TransactionEmail
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_email_addr { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string email_typ_cd { get; set; }
        public string cntct_stat_typ_cd { get; set; }
        public string cnst_best_email_ind { get; set; }
        public string cnst_email_strt_ts { get; set; }
        public string cnst_email_end_dt { get; set; }
        public string best_email_ind { get; set; }
        public string email_key { get; set; }
        public string domain_corrctd_ind { get; set; }
        public string cnst_email_validtn_dt { get; set; }
        public string cnst_email_validtn_method { get; set; }
        public string cnst_email_validtn_ind { get; set; }
        public string cnst_email_best_los_ind { get; set; }
        public string trans_key { get; set; }
        public string act_ind { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }

    public class TransactionAddress
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_addr_strt_ts { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string arc_srcsys_uid { get; set; }
        public string addr_typ_cd { get; set; }
        public string cnst_addr_end_dt { get; set; }
        public string best_addr_ind { get; set; }
        public string cnst_addr_group_ind { get; set; }
        public string cnst_addr_clssfctn_ind { get; set; }
        public string cnst_addr_line1_addr { get; set; }
        public string cnst_addr_line2_addr { get; set; }
        public string cnst_addr_city_nm { get; set; }
        public string cnst_addr_state_cd { get; set; }
        public string cnst_addr_zip_5_cd { get; set; }
        public string cnst_addr_zip_4_cd { get; set; }
        public string cnst_addr_carrier_route { get; set; }
        public string cnst_addr_county_nm { get; set; }
        public string cnst_addr_country_cd { get; set; }
        public string cnst_addr_latitude { get; set; }
        public string cnst_addr_longitude { get; set; }
        public string cnst_addr_non_us_pstl_c { get; set; }
        public string cnst_addr_prefd_ind { get; set; }
        public string cnst_addr_ff_mov_ind { get; set; }
        public string cnst_addr_unserv_ind { get; set; }
        public string cnst_addr_undeliv_ind { get; set; }
        public string cnst_addr_best_los_ind { get; set; }
        public string trans_key { get; set; }
        public string act_ind { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
        public string assessmnt_ctg { get; set; }
        public string dpv_cd { get; set; }
        public string res_deliv_ind { get; set; }
        public string locator_addr_key { get; set; }
    }

    public class TransactionBirth
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_birth_dy_num { get; set; }
        public string cnst_birth_mth_num { get; set; }
        public string cnst_birth_yr_num { get; set; }
        public string cnst_birth_strt_ts { get; set; }
        public string cnst_birth_end_dt { get; set; }
        public string cnst_birth_best_los_ind { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }

    public class TransactionCharacteristics
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_chrctrstc_typ_cd { get; set; }
        public string cnst_chrctrstc_val { get; set; }
        public string cnst_chrctrstc_strt_dt { get; set; }
        public string cnst_chrctrstc_end_dt { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string cnst_chrctrstc_typ_cnfdntl_ind { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }

    public class TransactionContactPreference
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cntct_prefc_typ_cd { get; set; }
        public string cntct_prefc_val { get; set; }
        public string act_ind { get; set; }
        public string cnst_cntct_prefc_strt_ts { get; set; }
        public string cnst_cntct_prefc_end_ts { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string dw_trans_ts { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }

    public class TransactionDeath
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_death_dt { get; set; }
        public string cnst_deceased_cd { get; set; }
        public string cnst_death_strt_ts { get; set; }
        public string cnst_death_end_dt { get; set; }
        public string cnst_death_best_los_ind { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }

    public class TransactionPersonName
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_prsn_seq { get; set; }
        public string cnst_prsn_nm_typ_cd { get; set; }
        public string cnst_nm_strt_dt { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string line_of_service_cd { get; set; }
        public string cnst_prsn_nm_end_dt { get; set; }
        public string best_prsn_nm_ind { get; set; }
        public string cnst_prsn_first_nm { get; set; }
        public string cnst_prsn_middle_nm { get; set; }
        public string cnst_prsn_last_nm { get; set; }
        public string cnst_prsn_prefix_nm { get; set; }
        public string cnst_prsn_suffix_nm { get; set; }
        public string cnst_prsn_full_nm { get; set; }
        public string cnst_prsn_nick_nm { get; set; }
        public string cnst_prsn_mom_maiden_nm { get; set; }
        public string cnst_alias_out_saltn_nm { get; set; }
        public string cnst_alias_in_saltn_nm { get; set; }
        public string cnst_prsn_nm_best_los_ind { get; set; }
        public string trans_key { get; set; }
        public string act_ind { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }

    public class TransactionOrgName
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_org_nm_typ_cd { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_org_nm_strt_dt { get; set; }
        public string cnst_org_nm_seq { get; set; }
        public string cnst_org_nm_end_dt { get; set; }
        public string best_org_nm_ind { get; set; }
        public string cnst_org_nm { get; set; }
        public string cln_cnst_org_nm { get; set; }
        public string cnst_org_nm_best_los_ind { get; set; }
        public string trans_key { get; set; }
        public string act_ind { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }

    public class TransactionPhone
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_phn_num { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_phn_extsn_num { get; set; }
        public string phn_typ_cd { get; set; }
        public string cntct_stat_typ_cd { get; set; }
        public string cnst_phn_best_ind { get; set; }
        public string cnst_phn_strt_ts { get; set; }
        public string cnst_phn_end_dt { get; set; }
        public string best_phn_ind { get; set; }
        public string cnst_phn_best_los_ind { get; set; }
        public string trans_key { get; set; }
        public string act_ind { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }

    

    public class TransactionOrgTransformations
    {
        public string strx_transform_id { get; set; }
        public string cdim_transform_id { get; set; }
        public string ent_org_id { get; set; }
        public string ent_org_branch { get; set; }
        public string act_ind { get; set; }
        public string transform_condn_sql { get; set; }
        public string org_nm_transform_strt_dt { get; set; }
        public string org_nm_transform_end_dt { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string is_previous { get; set; }
        public string row_stat_cd { get; set; }
    }

    public class TransactionOrgAffiliator
    {
        public string ent_org_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string ent_org_name { get; set; }
        public string cln_cnst_org_nm { get; set; }
        public string cnst_mstr_id { get; set; }
        public string cnst_affil_strt_ts { get; set; }
        public string cnst_affil_end_ts { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }

    public class TransactionAffiliatorTags
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
    }

    public class TransactionAffiliatorTagsUpload
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
    }

    public class TransactionAffiliatorHierarchy
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
    }

    public class TransactionUploadDetails
    {
        public string constituent_id { get; set; }
        public string trans_key { get; set; }
        public string name { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string city { get; set; }
        public string state_cd { get; set; }
        public string zip { get; set; }
        public string email_address { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
        public string status { get; set; }
    }

    

    public class TransactionUnmergeRequestLog
    {
        public string dw_request_tracking_key { get; set; }
        public string nk_request_case_id { get; set; }
        public string subj_area_cd { get; set; }
        public string batch_id { get; set; }
        public string request_actn_typ_cd { get; set; }
        public string request_create_ts { get; set; }
        public string user_requesting { get; set; }
        public string request_step_cd { get; set; }
        public string request_reason { get; set; }
        public string user_approving { get; set; }
        public string request_approved_ts { get; set; }
        public string dw_trans_ts { get; set; }
        public string transaction_key { get; set; }
        public string user_id { get; set; }
        public string trans_status { get; set; }
    }

    public class TransactionUnmergeProcessLog
    {
        public string dw_request_tracking_key { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string srcsys_cnst_uid { get; set; }
        public string cnst_mstr_id { get; set; }
        public string cdi_batch_id { get; set; }
        public string cnst_typ_cd { get; set; }
        public string valid_sts_ind { get; set; }
        public string unmerge_sts_ind { get; set; }
        public string unmerge_msg_txt { get; set; }
        public string unmerge_del_actn_cd { get; set; }
        public string unmerge_mstr_grp_rec_cnt { get; set; }
        public string unmerge_srcsys_grp_rec_cnt { get; set; }
        public string persistence_ind { get; set; }
        public string dw_trans_ts { get; set; }
        public string load_id { get; set; }
        public string transaction_key { get; set; }
        public string user_id { get; set; }
        public string trans_status { get; set; }
    }
    public class TransactionNAICS
    {
        public string cnst_mstr_id { get; set; }
        public string sts { get; set; }
        public string naics_cd { get; set; }
        public string naics_indus_title { get; set; }
        public string naics_indus_dsc { get; set; }
        public string rule_keywrd { get; set; }
        public string conf_weightg { get; set; }
        public string transaction_key { get; set; }
        public string user_id { get; set; }

    }
    public class TransactionNAICSUpload
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_org_nm { get; set; }
        public string cnst_org_addr { get; set; }
        public string naics_cd { get; set; }
        public string action { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string transaction_key { get; set; }
        public string user_id { get; set; }
        public string stwrd_sts { get; set; }
        public string stwrd_sts_rsn { get; set; }
    }
    public class TransactionOrgEmailDomain
    {
        public string cnst_mstr_id { get; set; }
        public string email_domain_key { get; set; }
        public string email_domain { get; set; }
        public string act_indv_email_cnt { get; set; }
        public string act_cnst_cnt { get; set; }
        public string most_rcnt_email_ts { get; set; }
        public string most_rcnt_vldtn_ts { get; set; }
        public string map_note { get; set; }
        public string transaction_key { get; set; }
        public string user_id { get; set; }
        public string row_stat_cd { get; set; }
        public string inactive_ind { get; set; }
    }
    public class TransactionOrgConfirmation
    {
        public string transaction_key { get; set; }
        public string cnst_id { get; set; }
        public string cnst_typ_cd { get; set; }
        public string user_id { get; set; }
    }

    //added by srini for CEM surfacing
    public class TransCemDNCDetails
    {
        public string dnc_mstr_id { get; set; }
        public string init_mstr_id { get; set; }
        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string locator_id { get; set; }
        public string dnc_typ { get; set; }
        public string cnst_dnc_strt_ts { get; set; }
        public string cnst_dnc_end_ts { get; set; }
        public string notes { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string dw_trans_ts { get; set; }
    }

    public class TransCemMsgPrefDetails
    {
        public string msg_pref_mstr_id { get; set; }
        public string init_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string msg_pref_typ { get; set; }
        public string msg_pref_val { get; set; }
        public string comm_typ { get; set; }
        public string msg_pref_strt_ts { get; set; }
        public string msg_pref_end_ts { get; set; }
        public string msg_pref_exp_ts { get; set; }
        public string user_id { get; set; }
        public string trans_status { get; set; }
        public string notes { get; set; }
        public string status { get; set; }
        public string transaction_key { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string dw_trans_ts { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string unique_trans_key { get; set; }
    }

    public class TransCemPrefLocDetails
    {
        public string cnst_mstr_id { get; set; }
        public string line_of_service_cd { get; set; }
        public string pref_loc_typ { get; set; }
        public string pref_loc_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string is_previous { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
        public string cnst_pref_loc_strt_ts { get; set; }
        public string cnst_pref_loc_end_ts { get; set; }
    }
    public class TransCemGrpMembership
    {
        public string grp_key { get; set; }
        public string cnst_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string line_of_service_cd { get; set; }
        public string assgnmnt_mthd { get; set; }
        public string grp_mbrshp_eff_strt_dt { get; set; }
        public string grp_mbrshp_eff_end_dt { get; set; }
        public string grp_strt_ts { get; set; }
        public string grp_end_dt { get; set; }
        public string grp_cd { get; set; }
        public string grp_nm { get; set; }
        public string grp_typ { get; set; }
        public string grp_owner { get; set; }
        public string trans_key { get; set; }
        public string act_ind { get; set; }
        public string user_id { get; set; }
        public string notes { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }
    //adding ends here
    public class TransactionMsgPrefUploadDetails
    {
        public string constituent_id { get; set; }
        public string trans_key { get; set; }
        public string name { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string city { get; set; }
        public string state_cd { get; set; }
        public string zip4 { get; set; }
        public string zip5 { get; set; }
        public string email_address { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public string msg_pref_typ { get; set; }
        public string msg_pref_val { get; set; }
        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string comm_typ { get; set; }

    }


    public class TransactionDncUploadDetails
    {
        public string constituent_id { get; set; }
        public string trans_key { get; set; }
        public string name { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string city { get; set; }
        public string state_cd { get; set; }
        public string zip4 { get; set; }
        public string zip5 { get; set; }
        public string email_address { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string dnc_typ { get; set; }
        public string locator_id { get; set; }
    }

    public class TransactionEOAffiliationUpload
    {
        public string ent_org_id { get; set; }
        public string cnst_mstr_id { get; set; }
        public string act { get; set; }
        public string trans_key { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string status { get; set; }
        public string reject_reason { get; set; }
    }

    public class TransactionEOSiteUpload
    {
        public string ent_org_id { get; set; }
        public string cnst_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_srcsys_scndry_id { get; set; }
        public string prnt_ent_org_id { get; set; }
        public string alt_appl_src_cd { get; set; }
        public string alt_srcsys_cnst_uid { get; set; }
        public string cnst_org_nm { get; set; }
        public string cnst_addr1_street1 { get; set; }
        public string cnst_addr1_street2 { get; set; }
        public string cnst_addr1_city { get; set; }
        public string cnst_addr1_state { get; set; }
        public string cnst_addr1_zip { get; set; }
        public string cnst_phn1_num { get; set; }
        public string cnst_phn2_num { get; set; }
        public string naics_cd { get; set; }
        public string cnst_chrctrstc1_typ_cd { get; set; }
        public string cnst_chrctrstc1_val { get; set; }
        public string cnst_chrctrstc2_typ_cd { get; set; }
        public string cnst_chrctrstc2_val { get; set; }
        public string grp_cd { get; set; }
        public string grp_nm { get; set; }
        public string rm_ind { get; set; }
        public string notes { get; set; }
        public string eosi_eff_strt_dt { get; set; }
        public string eosi_eff_end_dt { get; set; }
        public string status { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
    }

    public class TransactionEOUpload
    {
        public string eo_upld_key { get; set; }
        public string ent_org_id { get; set; }
        public string ent_org_name { get; set; }
        public string cnst_chrctrstc1_typ_cd { get; set; }
        public string cnst_chrctrstc1_val { get; set; }
        public string cnst_chrctrstc2_typ_cd { get; set; }
        public string cnst_chrctrstc2_val { get; set; }
        public string cnst_chrctrstc3_typ_cd { get; set; }
        public string cnst_chrctrstc3_val { get; set; }
        public string transform_condn1_typ_cd1 { get; set; }
        public string transform_condn1_str1 { get; set; }
        public string transform_condn1_typ_cd2 { get; set; }
        public string transform_condn1_str2 { get; set; }
        public string transform_condn1_typ_cd3 { get; set; }
        public string transform_condn1_str3 { get; set; }
        public string transform_condn2_typ_cd1 { get; set; }
        public string transform_condn2_str1 { get; set; }
        public string transform_condn2_typ_cd2 { get; set; }
        public string transform_condn2_str2 { get; set; }
        public string transform_condn2_typ_cd3 { get; set; }
        public string transform_condn2_str3 { get; set; }
        public string transform_condn3_typ_cd1 { get; set; }
        public string transform_condn3_str1 { get; set; }
        public string transform_condn3_typ_cd2 { get; set; }
        public string transform_condn3_str2 { get; set; }
        public string transform_condn3_typ_cd3 { get; set; }
        public string transform_condn3_str3 { get; set; }
        public string tag1 { get; set; }
        public string tag2 { get; set; }
        public string tag3 { get; set; }
        public string action { get; set; }
        public string eo_eff_strt_dt { get; set; }
        public string eo_eff_end_dt { get; set; }
        public string upld_typ_key { get; set; }
        public string upld_typ_dsc { get; set; }
        public string status { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
    }


    public class TransEOCharacteristics
    {
        public string ent_org_id { get; set; }
        //this is for details section
        public string cnst_mstr_id { get; set; }
        public string cnst_chrctrstc_typ_cd { get; set; }
        public string cnst_chrctrstc_val { get; set; }
        public string cnst_chrctrstc_strt_dt { get; set; }
        public string cnst_chrctrstc_end_dt { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string cnst_chrctrstc_typ_cnfdntl_ind { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }
}
