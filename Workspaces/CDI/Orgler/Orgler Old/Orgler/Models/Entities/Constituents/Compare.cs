using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Orgler.Data.Entities.Constituents
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
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            UserName = p.GetUserName(); //p.Identity.Name;
            CaseNumber = string.Empty;
            PreferredMasterIdForLn = string.Empty;
        }
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
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            UserName = p.GetUserName(); //p.Identity.Name;
        }
    }


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

        public UnmergeInput()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            UserName = p.GetUserName(); //p.Identity.Name;
        }
    }
  

}