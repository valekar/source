using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class ConstituentMaster
    {
        public static string getConstituentMasterSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @" select cnst_mstr_id, cnst_typ_cd, cnst_strt_dt, cnst_dsp_id, cnst_hsld_id, cnst_mstr_origin_dt, cnst_birth_dt, 
        cnst_birth_yr_num, cnst_dsp_death_dt, cnst_dsp_deceased_cd, cnst_arc_death_dt, cnst_arc_deceased_cd, new_cnst_mstr_id, mrg_typ_cd, 
        dw_srcsys_trans_ts, row_stat_cd, appl_src_cd, load_id, lst_bio_dntn_dt, lst_fr_dntn_dt, lst_phss_cours_cmpltn_dt, lst_volntrng_dt
        from dw_stuart_vws.strx_cnst_dtl_main where cnst_mstr_id in {2}; ";
    }
}
