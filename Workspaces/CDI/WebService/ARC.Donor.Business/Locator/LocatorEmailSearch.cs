using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Locator
{
   public class LocatorEmailInputSearchModel
    {
        
        public string LocEmailId { get; set; }
        public string LocEmailKey { get; set; }
        public string IntAssessCode { get; set; }
        public string ExtAssessCode { get; set; }
        public string ExactMatch { get; set; }
        public string Type { get; set; }
        
    }

    public class ListLocatorEmailInputSearchModel
    {
        public List<LocatorEmailInputSearchModel> LocatorEmailInputSearchModel { get; set; }
    }
}
