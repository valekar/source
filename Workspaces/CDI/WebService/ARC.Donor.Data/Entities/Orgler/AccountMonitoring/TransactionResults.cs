using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.AccountMonitoring
{
    /* Name: TransactionResult
    * Purpose: This class is the common output model for the results of most write operations */
    public class TransactionResult
    {
        public string trans_id { get; set; }
        public string o_message { get; set; }
    }
}
