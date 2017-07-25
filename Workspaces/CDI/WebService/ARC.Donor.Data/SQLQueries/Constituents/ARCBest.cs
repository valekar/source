using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class ARCBest
    {
        public static string getARCBestSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"select constituent_id,
                                        cnst_dsp_id,
                                        name,
                                        first_name,
                                        last_name,
                                        constituent_type,
                                        phone_number,
                                        email_address , 
                                        addr_line_1,
                                        addr_line_2,
                                        city,
                                        state_cd,
                                        zip,
                                        ( COALESCE(addr_line_1,'') || ' '  ||COALESCE(addr_line_2,'')  ||  x'0A'   || COALESCE(city, '')  ||  CASE WHEN city is not null then ',' WHEN city is null then ' ' end  || COALESCE(state_cd, '')  || ' '  || COALESCE(zip, '')) ||  CASE   WHEN zip_4 is null then '' WHEN zip_4 is not null then '-' END   || COALESCE(zip_4,'') AS address,
                                        row_stat_cd,
                                        appl_src_cd,
                                        srcsys_trans_ts 
                                    from dw_stuart_vws.stwrd_dnr_prfle 
                                    where constituent_id in {2};";
    }
}


//( COALESCE(addr_line_1,'') || ' '  ||COALESCE(addr_line_2,'')  || ' '  || COALESCE(city, '')  || ' '  || COALESCE(state_cd, '')  || ' '  || COALESCE(zip, '')) AS addr_line_1,