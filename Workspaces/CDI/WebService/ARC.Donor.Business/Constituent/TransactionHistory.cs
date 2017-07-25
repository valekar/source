using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Business.Constituents
{
    public class TransactionHistory
    {
        public string trans_key { get; set; }
        public string trans_typ_dsc { get; set; }
        public string sub_trans_typ_dsc { get; set; }
        public string sub_trans_actn_typ { get; set; }
        public string trans_stat { get; set; }
        public string trans_note { get; set; }
        public string cnst_mstr_id { get; set; }
        public string user_id { get; set; }
        public string trans_create_ts { get; set; }
        public string trans_last_modified_ts { get; set; }
        public string approved_by { get; set; }
        public string approved_dt { get; set; }

    }
}