using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Entities.Constituents
{
    public class Summary
    {
        public string cnst_smry_key { get; set; }
        public string bzd_smry_typ_cd { get; set; }
        public string cnst_mstr_id { get; set; }
        public string cnst_arc_deceased_cd { get; set; }
        public string cnst_prsn_prfx_nm { get; set; }
        public string cnst_prsn_f_nm { get; set; }
        public string cnst_prsn_m_nm { get; set; }
        public string cnst_prsn_l_nm { get; set; }
        public string cnst_prsn_sfx_nm { get; set; }
        public string cnst_line_1_addr { get; set; }
        public string cnst_line_2_addr { get; set; }
        public string cnst_city_nm { get; set; }
        public string cnst_st_cd { get; set; }
        public string cnst_zip_5_cd { get; set; }
        public string cnst_zip_4_cd { get; set; }
        public string cnst_email_addr { get; set; }
        public string smry_start_ts { get; set; }
        public string smry_end_ts { get; set; }

    }
}