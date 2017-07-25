﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Business.Constituents
{
    public class ExternalBridge
    {
        public string cnst_mstr_id { get; set; }
        public string source_system_id { get; set; }
        public string source_system_cd { get; set; }
        public string cnst_act_ind { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string cnst_nm { get; set; }
        public string address { get; set; }
        public string srch_srcsys_id { get; set; }
        public string request_transaction_key { get; set; }
        public string trans_status { get; set; }
        public string user_id { get; set; }
    }
}