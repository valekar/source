using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class MergeHistory
    {
        public static string getMergeHistorySQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT *
        FROM DW_STUART_VWS.strx_mrg_hst
        WHERE new_mstr_id = {2} 
        ORDER BY trans_ts, intnl_srcsys_grp_id 
        QUALIFY   ROW_NUMBER() OVER(PARTITION BY cnst_mstr_id,srcsys_cnst_uid,
                intnl_srcsys_grp_id 
        ORDER BY dw_trans_ts DESC ) =1;";
    }
}