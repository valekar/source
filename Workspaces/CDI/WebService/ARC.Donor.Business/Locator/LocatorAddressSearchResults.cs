using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.LocatorAddress
{
    public class LocatorAddressOutputSearchResults
    {
        public string locator_addr_key { get; set; }
        public string line1_addr { get; set; }
        public string line2_addr { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip_5 { get; set; }
        public string deliv_loc_type { get; set; }
        public string dpv_cd { get; set; }
        public string mailability_score { get; set; }
        public string assessmnt_key { get; set; }
        public string assessmnt_cd { get; set; }
        public string assessmnt_cd_title { get; set; }
        public string assessmnt_ctg { get; set; }
        public string code_category { get; set; }




        public string addr_key { get; set; }
        public string street_num { get; set; }
        public string street_name { get; set; }
        public string street_suffix { get; set; }
        public string street_predir { get; set; }
        public string street_postdir { get; set; }
        public string sec_sub_bldg1 { get; set; }
        public string sec_sub_bldg2 { get; set; }
        public string postal_phrase { get; set; }
        public string zip_4 { get; set; }
        public string county { get; set; }
        public string country_cd { get; set; }
        public string assessmnt_overridden { get; set; }
        public string final_assessmnt_date { get; set; }
        public string trans_key { get; set; }


        public string prev_assessmnt_key { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string locator_addr_strt_ts { get; set; }
        public string locator_addr_end_ts { get; set; }

        // dw_stuart_vws.strx_locator_addr_mstr//      
        public string constituent_id { get; set; }
        public string cnst_dsp_id { get; set; }
        public string name { get; set; }
        public string constituent_type { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string state_cd { get; set; }
        public string zip { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string arc_srcsys_cd { get; set; }

        // arc_mdm_tbls.assessmnt_addr_int
        public string res_deliv_ind { get; set; }
        public string dpv_false_pos { get; set; }
        public string dpv_fn1 { get; set; }
        public string dpv_fn2 { get; set; }
        public string dpv_fn3 { get; set; }
        public string high_rise_default { get; set; }
        public string match_cd { get; set; }
        public string postcode5_result_cd { get; set; }
        public string postcode4_result_cd { get; set; }
        public string loc_result_cd { get; set; }
        public string state_result_cd { get; set; }
        public string streetname_result_cd { get; set; }
        public string streetnum_result_cd { get; set; }
        public string deliv_serv_result_cd { get; set; }
        public string bldg_result_cd { get; set; }
        public string suite_result_cd { get; set; }
        public string org_result_cd { get; set; }
        public string country_result_cd { get; set; }
        public string cln_cnst_addr_latitude { get; set; }
        public string cln_cnst_addr_longitude { get; set; }
        public string geocode_acc_cd { get; set; }
        public string geo_loc { get; set; }
        public string assessmnt_addr_int_strt_ts { get; set; }
        public string assessmnt_addr_int_end_ts { get; set; }


        public LocatorAddressOutputSearchResults()
        {
            locator_addr_key = string.Empty;
            line1_addr = string.Empty;
            line2_addr = string.Empty;
            city = string.Empty;
            state = string.Empty;
            zip_5 = string.Empty;
            deliv_loc_type = string.Empty;
            dpv_cd = string.Empty;
            mailability_score = string.Empty;
            assessmnt_key = string.Empty;
            assessmnt_cd = string.Empty;
            assessmnt_cd_title = string.Empty;
            assessmnt_ctg = string.Empty;
            code_category = string.Empty;

            addr_key = string.Empty;
            street_num = string.Empty;
            street_name = string.Empty;
            street_suffix = string.Empty;
            street_predir = string.Empty;
            street_postdir = string.Empty;
            sec_sub_bldg1 = string.Empty;
            sec_sub_bldg2 = string.Empty;
            postal_phrase = string.Empty;
            zip_4 = string.Empty;
            county = string.Empty;
            country_cd = string.Empty;
            assessmnt_overridden = string.Empty;
            final_assessmnt_date = string.Empty;
            trans_key = string.Empty;
            prev_assessmnt_key = string.Empty;
            dw_srcsys_trans_ts = string.Empty;
            row_stat_cd = string.Empty;
            locator_addr_strt_ts = string.Empty;
            locator_addr_end_ts = string.Empty;

            // dw_stuart_vws.strx_locator_addr_mstr//      
            constituent_id = string.Empty;
            cnst_dsp_id = string.Empty;
            name = string.Empty;
            constituent_type = string.Empty;
            phone_number = string.Empty;
            email_address = string.Empty;
            addr_line_1 = string.Empty;
            addr_line_2 = string.Empty;
            state_cd = string.Empty;
            zip = string.Empty;
            srcsys_trans_ts = string.Empty;
            arc_srcsys_cd = string.Empty;

            // arc_mdm_tbls.assessmnt_addr_int
            res_deliv_ind = string.Empty;
            dpv_false_pos = string.Empty;
            dpv_fn1 = string.Empty;
            dpv_fn2 = string.Empty;
            dpv_fn3 = string.Empty;
            high_rise_default = string.Empty;
            match_cd = string.Empty;
            postcode5_result_cd = string.Empty;
            postcode4_result_cd = string.Empty;
            loc_result_cd = string.Empty;
            state_result_cd = string.Empty;
            streetname_result_cd = string.Empty;
            streetnum_result_cd = string.Empty;
            deliv_serv_result_cd = string.Empty;
            bldg_result_cd = string.Empty;
            suite_result_cd = string.Empty;
            org_result_cd = string.Empty;
            country_result_cd = string.Empty;
            cln_cnst_addr_latitude = string.Empty;
            cln_cnst_addr_longitude = string.Empty;
            geocode_acc_cd = string.Empty;
            geo_loc = string.Empty;
            assessmnt_addr_int_strt_ts = string.Empty;
            assessmnt_addr_int_end_ts = string.Empty;

        }
    }

    public class LocatorAddressConstituentsOutputSearchResults
    {


        public string locator_addr_key { get; set; }
        public string constituent_id { get; set; }
        public string cnst_dsp_id { get; set; }
        public string name { get; set; }
        public string constituent_type { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string state_cd { get; set; }
        public string zip { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string arc_srcsys_cd { get; set; }




        public LocatorAddressConstituentsOutputSearchResults()
        {

            // dw_stuart_vws.strx_locator_addr_mstr//   
            locator_addr_key = string.Empty;
            constituent_id = string.Empty;
            cnst_dsp_id = string.Empty;
            name = string.Empty;
            constituent_type = string.Empty;
            phone_number = string.Empty;
            email_address = string.Empty;
            addr_line_1 = string.Empty;
            addr_line_2 = string.Empty;
            state_cd = string.Empty;
            zip = string.Empty;
            srcsys_trans_ts = string.Empty;
            arc_srcsys_cd = string.Empty;

        }
    }

    public class CreateLocatorAddressInput
    {
        public Int64? LocAddrKey { get; set; }
        public string LocAddressLine { get; set; }
        public string LocAddressLine2 { get; set; }
        public string LocState { get; set; }
        public string LocCity { get; set; }
        public string LocZip { get; set; }
        public string LocZip4 { get; set; }
        public string LocDelType { get; set; }
        public string LocDelCode { get; set; }
        public string LocAssessCode { get; set; }
        public string code_category { get; set; }
        public string LoggedInUser { get; set; }
        public string o_outputMessage { get; set; }



        public CreateLocatorAddressInput()
        {           
            LocAddrKey = 0;
            LocAddressLine = string.Empty;
            LocAddressLine2 = string.Empty;
            LocState = string.Empty;
            LocCity = string.Empty;
            LocZip = string.Empty;
            LocZip4 = string.Empty;
            LocDelType = string.Empty;
            LocDelCode = string.Empty;
            LocAssessCode = string.Empty;
            code_category = string.Empty;
            LoggedInUser = string.Empty;
            o_outputMessage = string.Empty;
        }
    }

    public class CreateLocatorAddressOutput
    {
        //public Int64? o_case_seq { get; set; }
        public string o_outputMessage { get; set; }
    }


}
