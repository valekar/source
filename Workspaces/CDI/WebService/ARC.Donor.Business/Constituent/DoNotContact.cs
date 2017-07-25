using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Constituents
{
    public class DoNotContact
    {
        public string dnc_mstr_id { get; set; }
        public string init_mstr_id { get; set; }
        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string locator_id { get; set; }
        public string dnc_typ { get; set; }
        public string cnst_dnc_strt_ts { get; set; }
        public string cnst_dnc_end_ts { get; set; }
        public string cnst_dnc_exp_ts { get; set; }
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


    public class AllDoNotContact
    {
        public string dnc_mstr_id { get; set; }
        public string init_mstr_id { get; set; }
        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string locator_id { get; set; }
        public string dnc_typ { get; set; }
        public string cnst_dnc_strt_ts { get; set; }
        public string cnst_dnc_end_ts { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string notes { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string dw_trans_ts { get; set; }
    }


    public class DoNotContactInput
    {
        public string i_req_typ { get; set; }
        public string i_mstr_id { get; set; }
        public string i_cnst_typ { get; set; }
        public string i_case_num_seq { get; set; }
       // public string i_cnst_dnc_exp_ts { get; set; }
        public string i_bk_cnst_dnc_line_of_service_cd { get; set; }
        public string i_bk_cnst_dnc_comm_chan { get; set; }
        public string i_bk_cnst_loc_id { get; set; }
        public string i_new_cnst_dnc_line_of_service_cd { get; set; }
        public string i_new_cnst_dnc_comm_chan { get; set; }
        public string i_new_cnst_dnc_loc_id { get; set; }
        public string i_notes { get; set; }
        public string i_user_id { get; set; }
    }

    public class DoNotContactOutput
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }
}
