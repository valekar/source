using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities.Constituents
{
    public class Relationship
    {
        public string superior_cnst_mstr_id { get; set; }
        public string subord_cnst_mstr_id { get; set; }
        public string superior_cnst_key { get; set; }
        public string subord_cnst_key { get; set; }
        public string superior_nm_line { get; set; }
        public string subord_nm_line { get; set; }
        public string cnst_rlshp_typ_cd { get; set; }
        public string cnst_rlshp_typ_key { get; set; }
        public string strt_dt { get; set; }
        public string end_dt { get; set; }
        public string act_ind { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string dw_trans_ts { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string user_id { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
        public string transNotes { get; set; }

        public Relationship()
        {
            this.transNotes = string.Empty;
        }

    }
}