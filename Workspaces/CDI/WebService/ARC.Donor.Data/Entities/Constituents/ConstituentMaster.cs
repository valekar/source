using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Constituents
{
    public class ConstituentMaster
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_typ_cd { get; set; }
        public string cnst_strt_dt { get; set; }
        public string cnst_dsp_id { get; set; }
        public string cnst_hsld_id { get; set; }
        public string cnst_mstr_origin_dt { get; set; }
        public string cnst_birth_dt { get; set; }
        public string cnst_birth_yr_num { get; set; }
        public string cnst_dsp_death_dt { get; set; }
        public string cnst_dsp_deceased_cd { get; set; }
        public string cnst_arc_death_dt { get; set; }
        public string cnst_arc_deceased_cd { get; set; }
        public string new_cnst_mstr_id { get; set; }
        public string mrg_typ_cd { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string lst_bio_dntn_dt { get; set; }
        public string lst_fr_dntn_dt { get; set; }
        public string lst_phss_cours_cmpltn_dt { get; set; }
        public string lst_volntrng_dt { get; set; }
    }
}
