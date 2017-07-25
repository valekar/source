using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class OrgContacts
    {
        public static string getOrgContactSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                    PageNumber, string.Join(",", Master_id),
                    (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                    (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT top {0} *
        FROM arc_mdm_vws.bz_org_indv_rlshp
        WHERE org_mstr_id = {2};";

    
    }
}
