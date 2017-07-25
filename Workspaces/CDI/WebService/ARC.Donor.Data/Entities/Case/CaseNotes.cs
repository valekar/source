using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Case
{
    public class CaseNotesInput
    {
        public int? case_id { get; set; }
        public int? notes_id { get; set; }
        public string notes_text { get; set; }
        public string action { get; set; }
        public string user_id { get; set; }
        public string o_outputMessage { get; set; }

        public CaseNotesInput()
        {
            case_id = 0;
            notes_id = 0;
            notes_text = string.Empty;
            action = string.Empty;
            user_id = string.Empty;
        }
    }

    public class CaseNotesOutput
    {
        public string o_outputMessage { get; set; }
    }
}
