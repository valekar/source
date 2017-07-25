using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Case
{
    public class CaseOutputSearchResults
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
        public string ref_src_dsc { get; set; }
        public string typ_key_dsc { get; set; }
        public string intake_chan_value { get; set; }
        public string intake_owner_dept_value { get; set; }
        public DateTime? report_dt { get; set; }
        public string row_stat_cd { get; set; }
    }

    public class CreateCaseInput
    {
        public Int64? case_seq { get; set; }
        public string case_nm { get; set; }
        public string case_desc { get; set; }
        public string ref_src_desc { get; set; }
        public string ref_id { get; set; }
        public string typ_key_desc { get; set; }
        public string intake_chan_desc { get; set; }
        public string intake_owner_dept_desc { get; set; }
        public string cnst_nm { get; set; }
        public string crtd_by_usr_id { get; set; }
        public string status { get; set; }
        public string report_dt { get; set; }
        public string attchmnt_url { get; set; }
        public Int64? o_case_seq { get; set; }
        public string o_outputMessage { get; set; }

        public CreateCaseInput()
        {
            case_seq = 0;
            case_nm = string.Empty;
            case_desc = string.Empty;
            ref_src_desc = string.Empty;
            ref_id = string.Empty;
            typ_key_desc = string.Empty;
            intake_chan_desc = string.Empty;
            intake_owner_dept_desc = string.Empty;
            cnst_nm = string.Empty;
            crtd_by_usr_id = string.Empty;
            status = string.Empty;
            report_dt = string.Empty;
            attchmnt_url = string.Empty;
        }
    }

    public class CreateCaseOutput
    {
        public Int64? o_case_seq { get; set; }
        public string o_outputMessage { get; set; }
    }
}
