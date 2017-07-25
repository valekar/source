using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class Relationship
    {
        public static string getRelationshipSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT *
        FROM DW_STUART_VWS.strx_cnst_dtl_fsa_rlshp
        WHERE  (superior_cnst_mstr_id = {2} 
        OR  subord_cnst_mstr_id = {2} ) 
        AND   (trans_status NOT IN ('Rejected') 
        OR  trans_status IS NULL) 
        AND   ((trans_status IN ('Reject') 
        AND   unique_trans_key IS NOT NULL 
        AND   unique_trans_key <> '') 
        OR  (trans_status IN ('Processed') 
        AND   strx_row_stat_cd = 'F' 
        AND   unique_trans_key IS NOT NULL 
        AND   unique_trans_key <> '') 
        OR  (trans_status NOT IN ('Reject','Processed')) 
        OR  trans_status IS NULL) ;  ";
    }



    public class OrgRelationship
    {
        public static string getOrgRelationship(int NoOfRecords, int PageNumber, string Org_mstr_id)
        {
            return string.Format(OrgQry, NoOfRecords,
                     PageNumber, string.Join(",", Org_mstr_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string OrgQry = @"Select * from arc_orgler_vws.orgler_cnst_mstr_rlshp where org_mstr_id = {2}";

    }
}