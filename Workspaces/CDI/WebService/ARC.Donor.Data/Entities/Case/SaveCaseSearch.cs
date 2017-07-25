using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Case
{
    public class SaveCaseSearchInput
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address_line { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public Int64? master_id { get; set; }
        public string source_system { get; set; }
        public string chapter_source_system { get; set; }
        public string source_system_id { get; set; }
        public string constituent_type { get; set; }
        public string o_outputMessage { get; set; }

        public SaveCaseSearchInput()
        {
            first_name = string.Empty;
            last_name = string.Empty;
            address_line = string.Empty;
            city = string.Empty;
            state = string.Empty;
            zip = string.Empty;
            phone_number = string.Empty;
            email_address = string.Empty;
            master_id = 0;
            source_system = string.Empty;
            chapter_source_system = string.Empty;
            source_system_id = string.Empty;
            constituent_type = string.Empty;            
        }
    }

    public class SaveCaseSearchOutput
    {
        public string o_outputMessage { get; set; }
    }
}
