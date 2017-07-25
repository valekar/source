using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class AlternateIds
    {
        public static string getSourceSystemAlternateIdSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                  PageNumber, string.Join(",", Master_id),
                  (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                  (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT distinct *
        FROM arc_orgler_vws.orgler_cnst_dtl_srcsys_alt_id
        WHERE cnst_mstr_id = {2};";



        public static string getSourceSystemAlternateIdSQL(int NoOfRecords, int PageNumber, AlternateIdsInput input)
        {
            return string.Format(StuartQry, NoOfRecords,
                  PageNumber, string.Join(",", input.cnst_mstr_id), "'" + input.cnst_typ_cd+ "'",
                  (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                  (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string StuartQry = @"SELECT distinct *
        FROM arc_orgler_vws.orgler_cnst_dtl_srcsys_alt_id
        WHERE cnst_mstr_id = {2} and cnst_typ_cd = {3} ";


      
    }


}
