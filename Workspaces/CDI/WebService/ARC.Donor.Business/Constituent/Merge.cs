using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Constituents
{
    /* Entity classes to retrieve the Master details */
    public class MasterDetailsInput
    {
        public string ConstituentType { get; set; }
        public List<string> MasterId { get; set; }
    }

    /* Merge Input Classes */
    public class MergeInput
    {
        public List<string> MasterIds { get; set; }
        public string ConstituentType { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }
        public string CaseNumber { get; set; }
        public string PreferredMasterIdForLn { get; set; }

        public MergeInput()
        {
            CaseNumber = string.Empty;
            PreferredMasterIdForLn = string.Empty;
        }
    }

    //Class for master merge SP output
    public class MergeSPOutput
    {
        public string o_transaction_key { get; set; }
        public string o_outputMessage { get; set; }
    }

    /* Compare Output Classes */
    public class CompareOutput
    {
        public string header { get; set; }
        public string MasterId1 { get; set; }
        public string Detail1 { get; set; }
        public string MasterId2 { get; set; }
        public string Detail2 { get; set; }
        public string MasterId3 { get; set; }
        public string Detail3 { get; set; }
        public string MasterId4 { get; set; }
        public string Detail4 { get; set; }
        public string MasterId5 { get; set; }
        public string Detail5 { get; set; }
    }

    /* Merge Conflict Input Classes */
    public class MergeConflictInput
    {
        public List<string> MasterIds { get; set; }
        public string ConstituentType { get; set; }
        public string InternalSourceSystemGroupId { get; set; }
        public string TrustedSource { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }
        public string CaseNumber { get; set; }
        public string PreferredMasterIdForLn { get; set; }

        public MergeConflictInput()
        {
            CaseNumber = string.Empty;
            PreferredMasterIdForLn = string.Empty;
        }
    }
}
