using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Constituents
{
    public class MessagePreference
    {
        public string msg_pref_mstr_id { get; set; }
        public string init_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string msg_pref_typ { get; set; }
        public string msg_pref_val { get; set; }     
        public string msg_pref_exp_dt { get; set; }
        public string comm_typ { get; set; }
        public string msg_pref_strt_ts { get; set; }
        public string msg_pref_end_ts { get; set; }
        public string transaction_key { get; set; }
        public string user_id { get; set; }
        public string notes { get; set; }
        //public string status { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string dw_trans_ts { get; set; }
        public string trans_status {get;set;}
        public string inactive_ind {get;set;}
        public string unique_trans_key { get; set; }
        public string is_previous { get; set; }
            
    }

    public class AllMessagePreference
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
        public string msg_pref_exp_dt { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string notes { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string dw_trans_ts { get; set; }
    }


    public class MessagePreferenceInput
    {
        public string i_req_typ { get; set; }
        public Int64 i_mstr_id { get; set; }
        public string i_cnst_typ { get; set; }
        public Int64 i_case_num_seq { get; set; }
        public string i_notes { get; set; }
        public string i_user_id { get; set; }
        public string i_msg_pref_exp_dt { get; set; }
        public string i_bk_arc_srcsys_cd { get; set; }
        public string i_bk_msg_pref_typ { get; set; }
        public string i_bk_msg_pref_val { get; set; }
        public string i_bk_msg_pref_comm_chan { get; set; }
        public string i_bk_msg_pref_comm_chan_typ { get; set; }
        public string i_bk_line_of_service { get; set; }
        public string i_new_arc_srcsys_cd { get; set; }
        public string i_new_msg_pref_typ { get; set; }
        public string i_new_msg_pref_val { get; set; }
        public string i_new_msg_pref_comm_chan { get; set; }
        public string i_new_msg_pref_comm_chan_typ { get; set; }
        public string i_new_line_of_service { get; set; }
    }

    public class MessagePreferenceOutput
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }


    public class MessagePreferenceOptions
    {
        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string msg_prefc_typ { get; set; }
        public string msg_prefc_val { get; set; }
        public string full_msg_prefc { get; set; }
        public string comm_typ { get; set; }
    }
}
