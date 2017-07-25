using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.NewAccount
{
    public class StuartDetails
    {
        public class DetailsInput
        {
            public string cnst_mstr_id { get; set; }
            public string cnst_srcsys_id { get; set; }
            public string arc_srcsys_cd { get; set; }
        }

        public class Address
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
            public string transNotes { get; set; }
            public string sel_arc_srcsys_cd { get; set; }
            public string sel_cnst_srcsys_id { get; set; }

            public Address()
            {
                this.transNotes = string.Empty;
            }

        }

        public class Phone
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
            public string transNotes { get; set; }
            public string sel_arc_srcsys_cd { get; set; }
            public string sel_cnst_srcsys_id { get; set; }

            public Phone()
            {
                this.transNotes = string.Empty;
            }
        }

        public class OrgName
        {
            public string act_ind { get; set; }
            public string appl_src_cd { get; set; }
            public string arc_srcsys_cd { get; set; }
            public string best_org_nm_ind { get; set; }
            public string cln_cnst_org_nm { get; set; }
            public string cnst_mstr_id { get; set; }
            public string cnst_org_nm { get; set; }
            public string cnst_org_nm_best_los_ind { get; set; }
            public string cnst_org_nm_end_dt { get; set; }
            public string cnst_org_nm_seq { get; set; }
            public string cnst_org_nm_strt_dt { get; set; }
            public string cnst_org_nm_typ_cd { get; set; }
            public string cnst_srcsys_id { get; set; }
            public string dw_srcsys_trans_ts { get; set; }
            public string inactive_ind { get; set; }
            public string is_previous { get; set; }
            public string load_id { get; set; }
            public string row_stat_cd { get; set; }
            public string strx_row_stat_cd { get; set; }
            public string trans_key { get; set; }
            public string trans_status { get; set; }
            public string transaction_key { get; set; }
            public string unique_trans_key { get; set; }
            public string user_id { get; set; }
            public string transNotes { get; set; }
            public string sel_arc_srcsys_cd { get; set; }
            public string sel_cnst_srcsys_id { get; set; }

            public OrgName()
            {
                this.transNotes = string.Empty;
            }
        }
    }
}