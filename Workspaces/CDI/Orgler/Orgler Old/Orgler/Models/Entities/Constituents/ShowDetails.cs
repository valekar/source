using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities.Constituents
{

        public class ShowDetailsInput
        {
            public string DetailsType { get; set; }
            public string DetailsName { get; set; }
            public string ConstituentId { get; set; }
        }

        public class PersonNameDetailsOutput
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
            public string locator_prsn_nm_key { get; set; }
            public string best_prsn_nm_ind { get; set; }
            public string cnst_prsn_nm_best_los_ind { get; set; }
            public string trans_key { get; set; }
            public string user_id { get; set; }
            public string dw_srcsys_trans_ts { get; set; }
            public string row_stat_cd { get; set; }
            public string load_id { get; set; }
        }

        public class OrgNameDetailsOutput
        {
            public string cnst_mstr_id { get; set; }
            public string cnst_srcsys_id { get; set; }
            public string cnst_org_nm_typ_cd { get; set; }
            public string arc_srcsys_cd { get; set; }
            public string cnst_org_nm_strt_dt { get; set; }
            public string cnst_org_nm_seq { get; set; }
            public string cnst_org_nm_end_dt { get; set; }
            public string cnst_org_nm { get; set; }
            public string cln_cnst_org_nm { get; set; }
            public string best_org_nm_ind { get; set; }
            public string cnst_org_nm_best_los_ind { get; set; }
            public string trans_key { get; set; }
            public string user_id { get; set; }
            public string dw_srcsys_trans_ts { get; set; }
            public string row_stat_cd { get; set; }
            public string appl_src_cd { get; set; }
            public string load_id { get; set; }
        }

        public class AddressDetailsOutput
        {
            public string cnst_mstr_id { get; set; }
            public string cnst_srcsys_id { get; set; }
            public string cnst_addr_strt_ts { get; set; }
            public string arc_srcsys_cd { get; set; }
            public string arc_srcsys_uid { get; set; }
            public string addr_typ_cd { get; set; }
            public string cnst_addr_end_dt { get; set; }
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
            public string locator_addr_key { get; set; }
            public string best_addr_ind { get; set; }
            public string cnst_addr_best_los_ind { get; set; }
            public string trans_key { get; set; }
            public string user_id { get; set; }
            public string dw_srcsys_trans_ts { get; set; }
            public string row_stat_cd { get; set; }
            public string appl_src_cd { get; set; }
            public string load_id { get; set; }
        }

        public class PhoneDetailsOutput
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
            public string locator_phn_key { get; set; }
            public string best_phn_ind { get; set; }
            public string cnst_phn_best_los_ind { get; set; }
            public string trans_key { get; set; }
            public string user_id { get; set; }
            public string dw_srcsys_trans_ts { get; set; }
            public string row_stat_cd { get; set; }
            public string appl_src_cd { get; set; }
            public string load_id { get; set; }
        }

        public class EmailDetailsOutput
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
            public string email_key { get; set; }
            public string best_email_ind { get; set; }
            public string cnst_email_best_los_ind { get; set; }
            public string trans_key { get; set; }
            public string user_id { get; set; }
            public string dw_srcsys_trans_ts { get; set; }
            public string row_stat_cd { get; set; }
            public string appl_src_cd { get; set; }
            public string load_id { get; set; }
        }

        public class BirthDetailsOutput
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
        }

        public class DeathDetailsOutput
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
        }

        public class ContactPreferenceDetailsOutput
        {
            public string cnst_mstr_id { get; set; }
            public string cnst_srcsys_id { get; set; }
            public string arc_srcsys_cd { get; set; }
            public string cntct_prefc_typ_cd { get; set; }
            public string cntct_prefc_val { get; set; }
            public string act_ind { get; set; }
            public string cnst_cntct_prefc_strt_ts { get; set; }
            public string cnst_cntct_prefc_end_ts { get; set; }
            public string trans_key { get; set; }
            public string user_id { get; set; }
            public string srcsys_trans_ts { get; set; }
            public string row_stat_cd { get; set; }
            public string appl_src_cd { get; set; }
            public string load_id { get; set; }
            public string dw_trans_ts { get; set; }
        }

        public class CharacteristicsDetailsOutput
        {
            public string cnst_mstr_id { get; set; }
            public string cnst_srcsys_id { get; set; }
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

        public class ExternalBridgeDetailsOutput
        {
            public string cnst_mstr_id { get; set; }
            public string cnst_srcsys_id { get; set; }
            public string cnst_srcsys_scndry_id { get; set; }
            public string arc_srcsys_cd { get; set; }
            public string cnst_act_ind { get; set; }
            public string dw_srcsys_trans_ts { get; set; }
            public string row_stat_cd { get; set; }
            public string appl_src_cd { get; set; }
            public string load_id { get; set; }
        }

        public class InternalBridgeDetailsOutput
        {
            public string cnst_mstr_id { get; set; }
            public string cnst_mstr_subj_area_id { get; set; }
            public string cnst_mstr_subj_area_cd { get; set; }
            public string cnst_act_ind { get; set; }
            public string dw_srcsys_trans_ts { get; set; }
            public string row_stat_cd { get; set; }
            public string appl_src_cd { get; set; }
            public string load_id { get; set; }
        }

        public class MasterMetricsDetailsOutput
        {
            public string cnst_mstr_id { get; set; }
            public string mstr_metric_ts { get; set; }
            public string srcsys_unique_id { get; set; }
            public string cnst_mstr_subj_area_id { get; set; }
            public string mstr_id_ts { get; set; }
            public string arc_srcsys_cd { get; set; }
            public string new_cnst_ind { get; set; }
            public string cnst_act_ind { get; set; }
            public string min_quality_ind { get; set; }
            public string sent_to_ln_ind { get; set; }
            public string cnst_dsp_id_ts { get; set; }
            public string cnst_dsp_id { get; set; }
            public string cnst_nm_cnstrctd_ind { get; set; }
            public string cnst_addr_cnstrctd_ind { get; set; }
            public string cnst_nm_matrial_diff_ind { get; set; }
            public string cnst_addr_matrial_diff_ind { get; set; }
            public string original_min_quality_ind { get; set; }
            public string appl_src_cd { get; set; }
            public string dw_srcsys_trans_ts { get; set; }
            public string load_id { get; set; }
            public string row_stat_cd { get; set; }
        }

        public class FSARelationshipDetailsOutput
        {
            public string superior_cnst_key { get; set; }
            public string subord_cnst_key { get; set; }
            public string cnst_rlshp_typ_key { get; set; }
            public string nk_sf_rlshp_id { get; set; }
            public string nk_ta_acct_id { get; set; }
            public string nk_ta_related_acct_id { get; set; }
            public string nk_ta_rec_seq { get; set; }
            public string strt_dt { get; set; }
            public string end_dt { get; set; }
            public string job_title_nm { get; set; }
            public string second_generation_ind { get; set; }
            public string act_ind { get; set; }
            public string srcsys_trans_ts { get; set; }
            public string row_stat_cd { get; set; }
            public string appl_src_cd { get; set; }
            public string load_id { get; set; }
            public string dw_trans_ts { get; set; }
            public string trans_key { get; set; }
            public string user_id { get; set; }
        }
    

}