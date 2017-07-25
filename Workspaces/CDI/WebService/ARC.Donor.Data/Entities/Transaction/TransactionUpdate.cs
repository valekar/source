using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Transaction
{
    public class TransactionStatusUpdateInput
    {
        public Int64  TransactionKey { get; set; }
        public string TransactionStatus { get; set; }
        public string ApproverName { get; set; }
        public string o_outputMessage { get; set; }
    }

    public class TransactionStatusUpdateOutput
    {
        public string o_outputMessage { get; set; }
    }

    public class TransactionCaseAssociationInput
    {
        public Int64 TransactionKey { get; set; }
        public Int64 AssociatedCaseKey { get; set; }
        public string o_outputMessage { get; set; }
    }

    public class TransactionCaseAssociationOutput
    {
        public string o_outputMessage { get; set; }
    }
}
