using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Entities.Constituents
{
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
    }

    public class ConstituentAddressInput
    {
        public string RequestType { get; set; }
        public Int64 MasterID { get; set; }
        public string UserName { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public Int64? CaseNumber { get; set; }
        public string OldSourceSystemCode { get; set; }
        public string OldAddressTypeCode { get; set; }
        public string OldBestLOSInd { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip4 { get; set; }
        public string Zip5 { get; set; }
        public byte? UndeliveredIndicator { get; set; }
        public string SourceSystemCode { get; set; }
        public string AddressTypeCode { get; set; }
        public byte BestLOS { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }


        public ConstituentAddressInput()
        {
            UserName = string.Empty;
            ConstType = string.Empty;
            Notes = string.Empty;
            CaseNumber = 0;
            OldSourceSystemCode = string.Empty;
            OldAddressTypeCode = string.Empty;
            OldBestLOSInd = "0";
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Country = string.Empty;
            Zip4 = string.Empty;
            Zip5 = string.Empty;
            UndeliveredIndicator = 0;
            SourceSystemCode = string.Empty;
            AddressTypeCode = string.Empty;
            BestLOS = 0;
        }

    }

    public class ConstituentAddressOutput
    {
        public string o_transaction_key { get; set; }
        public string o_outputMessage { get; set; }
    }
}