using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.locator
{
    public class LocatorEmailInput
    {
        public string LocEmailId { get; set; }
        public Int64 LocEmailKey { get; set; }
        public string IntAssessCode { get; set; }
        public string ExtAssessCode { get; set; }
        public string ExactMatch { get; set; }
        public string o_outputMessage { get; set; }

        public LocatorEmailInput()
        {
            LocEmailId = string.Empty;
            LocEmailKey = 0;
            IntAssessCode = string.Empty;
            ExtAssessCode = string.Empty;
            
        }
    }

    public class LocatorEmailOutput
    {
        public string o_outputMessage { get; set; }
    }
}
