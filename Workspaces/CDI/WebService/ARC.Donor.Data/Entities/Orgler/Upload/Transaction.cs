using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.Upload
{
    public class TransInput
    {
        public string typ { get; set; }
        public string subTyp { get; set; }
        public string actionType { get; set; }
        public string transStat { get; set; }
        public string transNotes { get; set; }
        public string userId { get; set; }
        public string caseSeqNum { get; set; }
    }

    public class TransOutput
    {
        public long transKey { get; set; }
        public string transOutput { get; set; }
    }
}
