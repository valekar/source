using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.SQL.Transaction
{
    public class TransactionDetails
    {

        public static string getTransactionMergeSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionMergeQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionMergeQuery = @"SELECT	appl_src_cd, srcsys_cnst_uid, cdi_batch_id, cnst_mstr_id,
		cnst_dsp_id, cnst_type, intnl_srcsys_grp_id, alert_type_cd, alert_msg_txt,
		reprocess_ind, merge_sts_cd, merge_msg_txt, steward_actn_cd,
		steward_actn_dsc, user_id, transaction_key, trans_status
        FROM	dw_stuart_vws.stew_merge
        where transaction_key = {2}";

        public static string getTransactionPersonNameSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionPersonNameQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionPersonNameQuery = @"SELECT	cnst_mstr_id, cnst_srcsys_id, cnst_prsn_seq, cnst_prsn_nm_typ_cd,
		cnst_nm_strt_dt, arc_srcsys_cd, appl_src_cd, line_of_service_cd,
		cnst_prsn_nm_end_dt, best_prsn_nm_ind, cnst_prsn_first_nm, cnst_prsn_middle_nm,
		cnst_prsn_last_nm, cnst_prsn_prefix_nm, cnst_prsn_suffix_nm,
		cnst_prsn_full_nm, cnst_prsn_nick_nm, cnst_prsn_mom_maiden_nm,
		cnst_alias_out_saltn_nm, cnst_alias_in_saltn_nm, cnst_prsn_nm_best_los_ind,
		trans_key, act_ind, user_id, dw_srcsys_trans_ts, row_stat_cd,
		load_id, is_previous, transaction_key, trans_status, inactive_ind,
		strx_row_stat_cd, unique_trans_key
		FROM	dw_stuart_vws.strx_cnst_dtl_cnst_prsn_nm
        where transaction_key = {2}";

        public static string getTransactionOrgNameSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionOrgNameQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionOrgNameQuery = @"SELECT	cnst_mstr_id, cnst_srcsys_id, cnst_org_nm_typ_cd, arc_srcsys_cd,
		cnst_org_nm_strt_dt, cnst_org_nm_seq, cnst_org_nm_end_dt, best_org_nm_ind,
		cnst_org_nm, cln_cnst_org_nm, cnst_org_nm_best_los_ind, trans_key,
		act_ind, user_id, dw_srcsys_trans_ts, row_stat_cd, appl_src_cd,
		load_id, is_previous, transaction_key, trans_status, inactive_ind,
		strx_row_stat_cd, unique_trans_key
		FROM	dw_stuart_vws.strx_cnst_dtl_cnst_org_nm
        where transaction_key = {2}";

        public static string getTransactionPhoneSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionPhoneQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionPhoneQuery = @"SELECT	cnst_mstr_id, cnst_srcsys_id, cnst_phn_num, arc_srcsys_cd,
		cnst_phn_extsn_num, phn_typ_cd, cntct_stat_typ_cd, cnst_phn_best_ind,
		cnst_phn_strt_ts, cnst_phn_end_dt, best_phn_ind, cnst_phn_best_los_ind,
		trans_key, act_ind, user_id, dw_srcsys_trans_ts, row_stat_cd,
		appl_src_cd, load_id, is_previous, transaction_key, trans_status,
		inactive_ind, strx_row_stat_cd, unique_trans_key
		FROM	dw_stuart_vws.strx_cnst_dtl_cnst_phn
        where transaction_key = {2}";

        public static string getTransactionEmailSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionEmailQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionEmailQuery = @"SELECT	cnst_mstr_id, cnst_srcsys_id, cnst_email_addr, arc_srcsys_cd,
		email_typ_cd, cntct_stat_typ_cd, cnst_best_email_ind, cnst_email_strt_ts,
		cnst_email_end_dt, best_email_ind, email_key, domain_corrctd_ind,
		cnst_email_validtn_dt, cnst_email_validtn_method, cnst_email_validtn_ind,
		cnst_email_best_los_ind, trans_key, act_ind, user_id, dw_srcsys_trans_ts,
		row_stat_cd, appl_src_cd, load_id, is_previous, transaction_key,
		trans_status, inactive_ind, strx_row_stat_cd, unique_trans_key
		FROM	dw_stuart_vws.strx_cnst_dtl_cnst_email
        where transaction_key = {2}";

        public static string getTransactionAddressSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionAddressQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionAddressQuery = @"SELECT	cnst_mstr_id, cnst_srcsys_id, cnst_addr_strt_ts, arc_srcsys_cd,
		arc_srcsys_uid, addr_typ_cd, cnst_addr_end_dt, best_addr_ind,
		cnst_addr_group_ind, cnst_addr_clssfctn_ind, cnst_addr_line1_addr,
		cnst_addr_line2_addr, cnst_addr_city_nm, cnst_addr_state_cd,
		cnst_addr_zip_5_cd, cnst_addr_zip_4_cd, cnst_addr_carrier_route,
		cnst_addr_county_nm, cnst_addr_country_cd, cnst_addr_latitude,
		cnst_addr_longitude, cnst_addr_non_us_pstl_c, cnst_addr_prefd_ind,
		cnst_addr_ff_mov_ind, cnst_addr_unserv_ind, cnst_addr_undeliv_ind,
		cnst_addr_best_los_ind, trans_key, act_ind, user_id, dw_srcsys_trans_ts,
		row_stat_cd, appl_src_cd, load_id, is_previous, transaction_key,
		trans_status, inactive_ind, strx_row_stat_cd, unique_trans_key,
		assessmnt_ctg, dpv_cd, res_deliv_ind, locator_addr_key
		FROM	dw_stuart_vws.strx_cnst_dtl_cnst_addr
        where transaction_key = {2}";

        public static string getTransactionBirthSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionBirthQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionBirthQuery = @"SELECT	cnst_mstr_id, cnst_srcsys_id, arc_srcsys_cd, cnst_birth_dy_num,
		cnst_birth_mth_num, cnst_birth_yr_num, cnst_birth_strt_ts, cnst_birth_end_dt,
		cnst_birth_best_los_ind, trans_key, user_id, dw_srcsys_trans_ts,
		row_stat_cd, appl_src_cd, load_id, is_previous, transaction_key,
		trans_status, inactive_ind, strx_row_stat_cd, unique_trans_key
		FROM	dw_stuart_vws.strx_cnst_dtl_cnst_birth
        where transaction_key = {2}";

        public static string getTransactionDeathSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionDeathQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionDeathQuery = @"SELECT	cnst_mstr_id, cnst_srcsys_id, arc_srcsys_cd, cnst_death_dt,
		cnst_deceased_cd, cnst_death_strt_ts, cnst_death_end_dt, cnst_death_best_los_ind,
		trans_key, user_id, dw_srcsys_trans_ts, row_stat_cd, appl_src_cd,
		load_id, is_previous, transaction_key, trans_status, inactive_ind,
		strx_row_stat_cd, unique_trans_key
		FROM	dw_stuart_vws.strx_cnst_dtl_cnst_death
        where transaction_key = {2}";

        public static string getTransactionContactPreferenceSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionContactPreferenceQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionContactPreferenceQuery = @"SELECT	cnst_mstr_id, cnst_srcsys_id, arc_srcsys_cd, cntct_prefc_typ_cd,
		cntct_prefc_val, act_ind, cnst_cntct_prefc_strt_ts, cnst_cntct_prefc_end_ts,
		srcsys_trans_ts, trans_key, user_id, row_stat_cd, appl_src_cd,
		load_id, dw_trans_ts, is_previous, transaction_key, trans_status,
		inactive_ind, strx_row_stat_cd, unique_trans_key
		FROM	dw_stuart_vws.strx_cnst_dtl_cntct_prefc
        where transaction_key = {2}";

        public static string getTransactionCharactericticsSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionCharacteristicsQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionCharacteristicsQuery = @"SELECT	cnst_mstr_id, cnst_srcsys_id, arc_srcsys_cd, cnst_chrctrstc_typ_cd,
		cnst_chrctrstc_val, cnst_chrctrstc_strt_dt, cnst_chrctrstc_end_dt,
		trans_key, user_id, row_stat_cd, appl_src_cd, load_id, dw_srcsys_trans_ts,
		is_previous, transaction_key, cnst_chrctrstc_typ_cnfdntl_ind,
		trans_status, inactive_ind, strx_row_stat_cd, unique_trans_key
		FROM	dw_stuart_vws.strx_cnst_dtl_chrctrstc
        where transaction_key = {2}";

        public static string getTransactionUnmergeRequestLogSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionUnmergeRequestLogQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionUnmergeRequestLogQuery = @"SELECT	dw_request_tracking_key, nk_request_case_id, subj_area_cd,
		batch_id, request_actn_typ_cd, request_create_ts, user_requesting,
		request_step_cd, request_reason, user_approving, request_approved_ts,
		dw_trans_ts, transaction_key, user_id, trans_status
		FROM	dw_stuart_vws.stew_request_log
        where transaction_key = {2}";

        public static string getTransactionUnmergeProcessLogSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionUnmergeProcessLogQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionUnmergeProcessLogQuery = @"SELECT	dw_request_tracking_key, arc_srcsys_cd, srcsys_cnst_uid,
		cnst_mstr_id, cdi_batch_id, cnst_typ_cd, valid_sts_ind, unmerge_sts_ind,
		unmerge_msg_txt, unmerge_del_actn_cd, unmerge_mstr_grp_rec_cnt,
		unmerge_srcsys_grp_rec_cnt, persistence_ind, dw_trans_ts, load_id,
		transaction_key, user_id, trans_status
		FROM	dw_stuart_vws.stew_unmerge_srcsys_prcs_log
        where transaction_key = {2}";


        public static string getTransactionUploadSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionUploadQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionUploadQuery = @"SELECT  TOP {0} constituent_id, trans_key, name, addr_line_1, addr_line_2,
		city, state_cd, zip, email_address, phone_number, address
		FROM	dw_stuart_vws.strx_upload_trans_details
        where trans_key = {2}";

        public static string getTransactionOrgAffiliatorSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionOrgAffiliatorQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionOrgAffiliatorQuery = @"SELECT	ent_org_id, dw_srcsys_trans_ts, ent_org_name, cln_cnst_org_nm,
		cnst_mstr_id, cnst_affil_strt_ts, cnst_affil_end_ts, trans_key,
		user_id, row_stat_cd, appl_src_cd, load_id, is_previous, transaction_key,
		trans_status, inactive_ind, strx_row_stat_cd, unique_trans_key
		FROM	dw_stuart_vws.strx_cnst_dtl_cnst_org_affil
        where transaction_key = {2}";

        public static string getTransactionOrgTransformationSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionOrgTransformationQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionOrgTransformationQuery = @"SELECT	strx_transform_id, cdim_transform_id, ent_org_id, ent_org_branch,
		act_ind, transform_condn_sql, org_nm_transform_strt_dt, org_nm_transform_end_dt,
		dw_srcsys_trans_ts, trans_key, user_id, is_previous, row_stat_cd
		FROM	dw_stuart_vws.strx_cdim_org_nm_transform
        where trans_key = {2}";

        public static string getTransactionAffiliatorTagsSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionAffiliatorTagsQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionAffiliatorTagsQuery = @"SELECT	ent_org_id, tag_key, tag, dw_trans_ts, start_dt, end_dt,
		trans_key, user_id, row_stat_cd
		FROM	arc_cmm_vws.ent_tag_mapping
        where trans_key = {2}";

        public static string getTransactionAffiliatorHierarchySQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionAffiliatorHierarchyQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionAffiliatorHierarchyQuery = @"SELECT	superior_ent_org_key, subord_ent_org_key, superior_ent_org_name,
		subord_ent_org_name, rlshp_typ_cd, rlshp_typ_desc, start_dt,
		end_dt, dw_trans_ts, row_stat_cd, appl_src_cd, load_id, trans_key,
		user_id
		FROM	dw_stuart_vws.ent_org_rlshp
        where trans_key = {2}";

        public static string getTransactionAffiliatorTagsUploadSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionAffiliatorTagsUploadQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionAffiliatorTagsUploadQuery = @"SELECT	ent_org_id, tag_key, tag, dw_trans_ts, start_dt, end_dt,
		trans_key, user_id, row_stat_cd
		FROM	arc_cmm_vws.ent_tag_mapping
        where trans_key = {2}";

        public static string getTransactionNAICSSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionNAICSQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionNAICSQuery = @"SELECT	*
        FROM	arc_orgler_vws.trans_naics_dtl
        where transaction_key = {2}";

        public static string getTransactionNAICSUploadSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionNAICSUploadQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionNAICSUploadQuery = @"SELECT	*
        FROM	arc_orgler_vws.trans_naics_upld_dtl
        where transaction_key = {2}";

        public static string getTransactionOrgEmailDomainSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionOrgEmailDomainQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionOrgEmailDomainQuery = @"SELECT	*
        FROM	arc_orgler_vws.trans_email_domain_dtl
        where transaction_key = {2}";

        public static string getTransactionOrgConfirmationSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransactionOrgConfirmationQuery, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string strTransactionOrgConfirmationQuery = @"SELECT	*
        FROM	arc_orgler_vws.trans_confirm_acc_dtl
        where transaction_key = {2}";



        // added by srini for CEM updates
        public static string getTransCemDncSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransCemDNCSQL, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }


        static readonly string strTransCemDNCSQL = @"SELECT *
                                                FROM DW_STUART_VWS.bz_strx_cnst_dnc
                                                where transaction_key = {2}";

         public static string getTransCemMsgPrefSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransCemMsgPref, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

         static readonly string strTransCemMsgPref = @"SELECT *
        FROM DW_STUART_VWS.bz_strx_cnst_msg_pref where transaction_key = {2}";



          public static string getTransCemPrefLocSQL(int NoOfRecords, int PageNumber, string Transaction_key)
        {
            return string.Format(strTransCemPrefLoc, NoOfRecords,
                     PageNumber, string.Join(",", Transaction_key),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }
          static readonly string strTransCemPrefLoc = @"SELECT * 
        FROM DW_STUART_VWS.bz_strx_cnst_pref_loc where transaction_key = {2}";


          public static string getTransCemGrpMembershipSQL(int NoOfRecords, int PageNumber, string Transaction_key)
          {
              return string.Format(strTransCemGrpMembershipQuery, NoOfRecords,
                       PageNumber, string.Join(",", Transaction_key),
                       (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                       (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
          }

          static readonly string strTransCemGrpMembershipQuery = @"SELECT *
		FROM dw_stuart_vws.strx_cnst_dtl_chptr_grp
        where transaction_key = {2}";


          public static string getTransDncUploadSQL(int NoOfRecords, int PageNumber, string Transaction_key)
          {
              return string.Format(strTransDncUploadQuery, NoOfRecords,
                       PageNumber, string.Join(",", Transaction_key),
                       (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                       (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
          }


          static readonly string strTransDncUploadQuery = @"SELECT * FROM dw_stuart_vws.strx_dnc_upld_trans_dtls 
                     where trans_key = {2}";


          public static string getTransMsgPrefUploadSQL(int NoOfRecords, int PageNumber, string Transaction_key)
          {
              return string.Format(strTransMsgPrefUploadQuery, NoOfRecords,
                       PageNumber, string.Join(",", Transaction_key),
                       (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                       (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
          }


          static readonly string strTransMsgPrefUploadQuery = @"SELECT * FROM dw_stuart_vws.strx_msg_pref_upld_trans_dtls 
                     where trans_key = {2}";

          //adding ends here
          public static string getTransactionEOAffiliationUploadSQL(int NoOfRecords, int PageNumber, string Transaction_key)
          {
              return string.Format(@"SELECT *
                                    FROM arc_orgler_vws.bz_cnst_org_affil_upld
                                    WHERE trans_key = {0}", Transaction_key);
          }

          public static string getTransactionEOSiteUploadSQL(int NoOfRecords, int PageNumber, string Transaction_key)
          {
              return string.Format(@"SELECT *
                                    FROM arc_orgler_vws.bz_eosi_upld
                                    WHERE trans_key = {0}", Transaction_key);
          }

          public static string getTransactionEOUploadSQL(int NoOfRecords, int PageNumber, string Transaction_key)
          {
              return string.Format(@"SELECT *
                                    FROM arc_orgler_vws.bz_eo_upld
                                    WHERE trans_key = {0}", Transaction_key);
          }

          public static string getTransactionEOCharacteristicsSQL(int NoOfRecords, int PageNumber, string Transaction_key)
          {
              return string.Format(@"SELECT * FROM arc_orgler_vws.ent_org_dtl_chrctrstc
                                   WHERE transaction_key = {0} ", Transaction_key);
          } 
       
    }


}
