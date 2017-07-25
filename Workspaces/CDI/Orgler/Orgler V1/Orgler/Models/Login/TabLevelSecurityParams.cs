using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Login
{
    //Class to store the tab level security params from the file
    public class TabLevelSecurityParams
    {
        public string usr_nm { get; set; }
        public string grp_nm { get; set; }
        public string hris_tb_access { get; set; }
        public string cdi_tb_access { get; set; }
        public string fsa_tb_access { get; set; }
    }

    public class TabLevelSecurityParamsInsertOP
    {
        public string o_transOutput { get; set; }
    }
}