using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Case
{
    public class CaseSearchResults
    {
        public Int64? case_key { get; set; }
        public string case_nm { get; set; }
        public string case_desc { get; set; }
        public string cnst_nm { get; set; }
        public string crtd_by_usr_id { get; set; }
        public string status { get; set; }
        public string attchmnt_url { get; set; }
        public DateTime? create_ts { get; set; }
        public Int64? ref_src_key { get; set; }
        public string ref_id { get; set; }
        public Int64? typ_key { get; set; }
        public string intake_chan_value { get; set; }
        public string intake_owner_dept_value { get; set; }
        public DateTime? report_dt { get; set; }
        public char row_stat_cd { get; set; }
    }
}
