using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Case
{
    public class CreateCaseModel
    {

    }

    public class CompositeCreateCaseInput
    {
        public CreateCaseInput CreateCaseInput { get; set; }
        public SaveCaseSearchInput SaveCaseSearchInput { get; set; }
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
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            crtd_by_usr_id = p.GetUserName();  //p.Identity.Name;
           
        }

    }

    public class SaveCaseSearchInput
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address_line { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public Int64? master_id { get; set; }
        public string source_system { get; set; }
        public string chapter_source_system { get; set; }
        public string source_system_id { get; set; }
        public string constituent_type { get; set; }
        public string o_outputMessage { get; set; }
    }

    public class CreateCaseOutput
    {
        public Int64? o_case_seq { get; set; }
        public string o_outputMessage { get; set; }
    }

    public class DeleteCaseInput
    {
        public Int64? case_nm { get; set; }
        public string o_outputMessage { get; set; }
    }
}