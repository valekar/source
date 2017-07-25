using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.LocatorDomain
{
    public class LocatorDomainInputSearchModel
    {
        public string LocInvalidDomain { get; set; }
        public string LocValidDomain { get; set; }
        public string LocStatus { get; set; }
        public string LoggedInUser { get; set; }
        public string email_domain_map_key { get; set; }
        public string status { get; set; }

    }
    public class ListLocatorDomainInputSearchModel
    {
        public List<LocatorDomainInputSearchModel> LocatorDomainInputSearchModel { get; set; }
    }

    public class CreateLocatorDomainOutput
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }


}
