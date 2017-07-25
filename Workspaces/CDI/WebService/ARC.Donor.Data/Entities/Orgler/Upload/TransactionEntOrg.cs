using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.Upload
{
    public class TransactionEntOrgInput
    {
        public string transKey { get; set; }
        public string entOrgId { get; set; }
        public string transNotes { get; set; }
        public string applSrcCd { get; set; }
    }

    public class TransactionEntOrgOutput
    {
        public string o_trans_out { get; set; }
    }
}
