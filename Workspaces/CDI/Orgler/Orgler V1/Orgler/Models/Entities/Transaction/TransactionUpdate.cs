using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities.Transaction
{
    public class TransactionStatusUpdateInput
    {
        public Int64 TransactionKey { get; set; }
        public string TransactionStatus { get; set; }
        public string ApproverName { get; set; }
        public string o_outputMessage { get; set; }

        public TransactionStatusUpdateInput()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            ApproverName = p.Identity.Name;
            //if (Models.Entities.SessionUtils.strUserName != "")
            //    ApproverName = Models.Entities.SessionUtils.strUserName;
            //else
            //    ApproverName = "";
        }
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

    /* Entity classes to retrieve the Master details */
    public class MasterDetailsInput
    {
        public string ConstituentType { get; set; }
        public List<string> MasterId { get; set; }
    }
}