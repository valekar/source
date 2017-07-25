using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.LocatorAddress
{
    public class LocatorAddressInputSearchModel
    {
        public string LocAddrKey { get; set; }
        public string LocAddressLine { get; set; }
        public string LocState { get; set; }
        public string LocCity { get; set; }
        public string LocZip { get; set; }
        public string LocDelType { get; set; }
        public string LocDelCode { get; set; }
        public string LocAssessCode { get; set; }
        public string code_category { get; set; }
    }
    public class ListLocatorAddressInputSearchModel
    {
        public List<LocatorAddressInputSearchModel> LocatorAddressInputSearchModel { get; set; }
    }

    
}
