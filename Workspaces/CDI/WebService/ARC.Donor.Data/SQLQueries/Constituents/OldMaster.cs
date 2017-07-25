using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class OldMaster
    {
        public static string getOldMasterSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"select distinct constituent_id 
        from  dw_stuart_vws.strx_cnst_dtl_old_mstr 
        where new_cnst_mstr_id = {2};";
    }
}