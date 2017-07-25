using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Constituents
{
    /* Unmerge Request Details */
    public class UnMasterRequest
    {
        public string MasterId { get; set; }
        public string ConstituentType { get; set; }
        public string SourceSystemId { get; set; }
        public string SourceSystemCode { get; set; }
        public int MasterGroup { get; set; }
        public int intPersistence { get; set; }
    }

    /* Unmerge Input Classes */
    public class UnmergeInput
    {
        public List<UnMasterRequest> UnmergeRequestDetails { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }
        public string CaseNumber { get; set; }
    }

    //Class for unmerge SP output
    public class UnmergeSPOutput
    {
        public string o_transaction_key { get; set; }
        public string o_outputMessage { get; set; }
    }
}
