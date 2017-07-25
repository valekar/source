using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Case
{
    public class CaseLocatorInput
    {
        public string req_typ { get; set; }
        public Int64? case_key { get; set; }
        public int locator_id { get; set; }
        public string usr_nm { get; set; }
        public string locator_upd { get; set; }
        public string o_outputMessage { get; set; }

        public CaseLocatorInput()
        {
            req_typ = string.Empty;
            case_key = 0;
            locator_id = 0;
            usr_nm = string.Empty;
            locator_upd = string.Empty;
        }
    }

    public class CaseLocatorOutput
    {
        public string o_outputMessage { get; set; }
    }
}
