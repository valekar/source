using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Locator
{
    public class LocatorEmailInputSearchModel
    {
        public string LocEmailId { get; set; }
        public string LocEmailKey { get; set; }
        public string IntAssessCode { get; set; }
        public string ExtAssessCode { get; set; }
        public bool ExactMatch { get; set; }
        public string Type { get; set; }
    }
    public class ListlocatorEmailInputSearchModel
    {
        public List<LocatorEmailInputSearchModel> LocatorEmailInputSearchModel { get; set; }
    }

    
}
