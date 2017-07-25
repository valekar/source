using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.ComplexEntity
{
        public class ConsSearchResults
        {
            public string constituent_id { get; set; }
            public string cnst_dsp_id { get; set; }
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
