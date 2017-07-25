using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Locator
{
    public class LocatorEmailOutputSearchResults
    {
        public string cnst_email_cnt { get; set; }
        public string email_key { get; set; }
        public string cnst_email_addr { get; set; }
        public string final_assessmnt_date { get; set; }
        public string trans_key { get; set; }
        public string assessmnt_key{ get; set; }
        public string assessmnt_overridden { get; set; }
        public string mailbox_corrctd_ind { get; set; }
        public string domain_corrctd_ind { get; set; }

        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string locator_email_strt_ts { get; set; }

        public string locator_email_end_ts { get; set; }
        public string int_assessmnt_cd { get; set; }
        public string ext_hygiene_result { get; set; }
        public string assessmnt_cd { get; set; }

        public string assessmnt_cd_title { get; set; }
        public string assessmnt_ctg { get; set; }
        public string code_category { get; set; }
        public string ext_assessmnt_cd { get; set; }

        public string ext_status_cd { get; set; }
        public string ext_reason_cd { get; set; }
        public string ext_net_protected_ind { get; set; }
        public string ext_net_protected_by { get; set; }

        public string ext_disp_domain_ind { get; set; }
        public string ext_poss_disp_addr_ind { get; set; }
        public string ext_role_based_addr_ind { get; set; }

        public string local_part { get; set; }
        public string domain_part { get; set; }
        public string ext_poss_domain_corrctn_ind { get; set; }
        public string ext_poss_addr_corrctn { get; set; }

        public string ext_pot_vulgar_domain_ind { get; set; }
        public string ext_pot_vulgar_addr_ind { get; set; }
        public string ext_returned_from_cache_ind { get; set; }
        public string constituent_id { get; set; }
        public string cnst_dsp_id { get; set; }
        public string name { get; set; }
        public string constituent_type { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string city { get; set; }
        public string state_cd { get; set; }
        public string zip { get; set; }
        public string srcsys_trans_ts { get; set; }



        public LocatorEmailOutputSearchResults()
        {
            cnst_email_cnt = string.Empty;
            email_key = string.Empty;
            cnst_email_addr = string.Empty;
            final_assessmnt_date = string.Empty;
            trans_key = string.Empty;
            assessmnt_key = string.Empty;
            assessmnt_overridden = string.Empty;
            mailbox_corrctd_ind = string.Empty;
            domain_corrctd_ind = string.Empty;

            dw_srcsys_trans_ts = string.Empty;
            row_stat_cd = string.Empty;
            appl_src_cd = string.Empty;
            load_id = string.Empty;
            locator_email_strt_ts = string.Empty;

            locator_email_end_ts = string.Empty;
            int_assessmnt_cd = string.Empty;
            ext_hygiene_result = string.Empty;
            assessmnt_cd = string.Empty;

            assessmnt_cd_title = string.Empty;
            assessmnt_ctg = string.Empty;
            code_category = string.Empty;
            ext_assessmnt_cd = string.Empty;

            ext_status_cd = string.Empty;
            ext_reason_cd = string.Empty;
            ext_net_protected_ind = string.Empty;
            ext_net_protected_by = string.Empty;

            ext_disp_domain_ind = string.Empty;
            ext_poss_disp_addr_ind = string.Empty;
            ext_role_based_addr_ind = string.Empty;

            local_part = string.Empty;
            domain_part = string.Empty;
            ext_poss_domain_corrctn_ind = string.Empty;
            ext_poss_addr_corrctn = string.Empty;

            ext_pot_vulgar_domain_ind = string.Empty;
            ext_pot_vulgar_addr_ind = string.Empty;
            ext_returned_from_cache_ind = string.Empty;

            constituent_id = string.Empty;
            cnst_dsp_id = string.Empty;
            name = string.Empty;
            constituent_type = string.Empty;
            phone_number = string.Empty;
            email_address = string.Empty;
            addr_line_1 = string.Empty;
            addr_line_2 = string.Empty;
            city = string.Empty;
            state_cd = string.Empty;
            zip = string.Empty;
            srcsys_trans_ts = string.Empty;
        }
    }

    public class LocatorEmailConstOutputSearchResults
    {
        public string cnst_email_cnt { get; set; }
        public string constituent_id { get; set; }
        public string cnst_dsp_id { get; set; }
        public string name { get; set; }
        public string constituent_type { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string city { get; set; }
        public string state_cd { get; set; }
        public string zip { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string locator_addr_key { get; set; }
        public string arc_srcsys_cd { get; set; }


        public LocatorEmailConstOutputSearchResults()
        {
            locator_addr_key = string.Empty;
            cnst_email_cnt = string.Empty;
            constituent_id = string.Empty;
            cnst_dsp_id = string.Empty;
            name = string.Empty;
            constituent_type = string.Empty;
            phone_number = string.Empty;
            email_address = string.Empty;
            addr_line_1 = string.Empty;
            addr_line_2 = string.Empty;
            city = string.Empty;
            state_cd = string.Empty;
            zip = string.Empty;
            srcsys_trans_ts = string.Empty;
            arc_srcsys_cd = string.Empty;
        }
    }

    public class CreateLocatorEmailInput
    {
        public string LocEmailId { get; set; }
        public Int64? LocEmailKey { get; set; }
        public string IntAssessCode { get; set; }
        public string ExtAssessCode { get; set; }
        public string ExactMatch { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string LoggedInUser { get; set; }
        public string o_outputMessage { get; set; }
       

        public CreateLocatorEmailInput()
        {
            LocEmailId = string.Empty;
            LocEmailKey = 0;
            IntAssessCode = string.Empty;
            ExtAssessCode = string.Empty;
            LoggedInUser = string.Empty;
            o_outputMessage = string.Empty;
        }
    }

    public class CreateLocatorEmailOutput
    {
       // public Int64? o_case_seq { get; set; }
        public string o_outputMessage { get; set; }
    }

    public class BridgeCount
    {
        public string cnst_email_addr { get; set; }
        public string cnst_mstr_subj_area_cd { get; set; }
        public string cnt { get; set; }
    }

}
