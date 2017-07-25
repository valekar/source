using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Business.Constituents
{
   [Serializable]
    public class ConsSearchResultsCache
    {
        public string IndexString { get; set; }
        public string constituent_id { get; set; }
        public string lnId { get; set; }
        public string sourceSystem { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string constituent_type { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string city { get; set; }
        public string state_cd { get; set; }
        public string zip { get; set; }
    }
}