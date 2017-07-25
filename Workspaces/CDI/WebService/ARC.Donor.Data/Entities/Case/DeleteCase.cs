using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Case
{
    public class DeleteCaseInput
    {
        public Int64? case_nm { get; set; }
        public string o_outputMessage { get; set; }

        public DeleteCaseInput()
        {
            case_nm = 0;
        }
    }

    public class DeleteCaseOutput
    {
        public string o_outputMessage { get; set; }
    }
}
