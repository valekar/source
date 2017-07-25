using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities
{
    public class ConstituentSearchResults
    {
        public string name { get; set; }
        public Int64 constituent_id { get; set; }
        public string sourceSystem { get; set; }
        public string email_address { get; set; }
        public string phone_number { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string city { get; set; }
        public string state_cd { get; set; }
        public string zip { get; set; }
        public int lnId { get; set; }
    }

}

