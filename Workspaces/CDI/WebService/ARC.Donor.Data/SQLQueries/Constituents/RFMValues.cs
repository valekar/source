using ARC.Donor.Business.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.SQL.Constituents
{
  public  class RFMValues
    {
        public static string getRFMValuesSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                  PageNumber, string.Join(",", Master_id),
                  (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                  (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT cnst_mstr_id,
line_of_srvc_cd,
rcnt_patrng_dt,
totl_dntn_cnt,
totl_dntn_val,
rcncy_scr,
freq_scr,
dntn_scr,
totl_rfm_scr,
rfm_scr_strt_ts,
rfm_scr_end_dt,
srcsys_trans_ts,
dw_srcsys_trans_ts,
row_stat_cd,
appl_src_cd,load_id
from	arc_orgler_tbls.orgler_cnst_mstr_org_rfm
        WHERE cnst_mstr_id = {2}";



        public static string getRFMValuesDataSQL(int NoOfRecords, int PageNumber, RFMValuesInput input)
        {
            return string.Format(StuartQry, NoOfRecords,
                  PageNumber, string.Join(",", input.cnst_mstr_id),
                  (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                  (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string StuartQry = @"SELECT cnst_mstr_id,
line_of_srvc_cd,
rcnt_patrng_dt,
CASE	WHEN line_of_srvc_cd = 'FR' THEN TRIM(COALESCE(totl_dntn_cnt,
		0)) || ' gift(s)'
					 WHEN line_of_srvc_cd = 'BIO' THEN TRIM(COALESCE(totl_dntn_cnt,
		0)) || ' drive(s)' 
					 WHEN line_of_srvc_cd = 'PHSS' THEN TRIM(COALESCE(totl_dntn_cnt,
		0)) || ' order(s)' 
		ELSE TRIM(COALESCE(totl_dntn_cnt,0))
		END AS totl_dntn_cnt,
CASE	WHEN line_of_srvc_cd = 'FR' THEN TRIM(COALESCE(totl_dntn_val,
		0) (FORMAT'$$$,$$9.99') (VARCHAR(10)))
					 WHEN line_of_srvc_cd = 'BIO' THEN TRIM(COALESCE(totl_dntn_val,
		0)) || ' blood unit(s)' 
					 WHEN line_of_srvc_cd = 'PHSS' THEN TRIM(COALESCE(totl_dntn_val,
		0) (FORMAT'$$$,$$9.99') (VARCHAR(10)))
		ELSE TRIM(COALESCE(totl_dntn_val,0))
		END AS totl_dntn_val,

rcncy_scr,
freq_scr,
dntn_scr,
totl_rfm_scr,
rfm_scr_strt_ts,
rfm_scr_end_dt,
srcsys_trans_ts,
dw_srcsys_trans_ts,
row_stat_cd,
appl_src_cd,load_id
from	arc_orgler_tbls.orgler_cnst_mstr_org_rfm
        WHERE cnst_mstr_id = {2}";

    }
}
