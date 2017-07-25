using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Transaction
{
    public class TransactionUpdate
    {
        public IList<Entities.Transaction.TransactionStatusUpdateOutput> updateTransactionStatus(ARC.Donor.Data.Entities.Transaction.TransactionStatusUpdateInput TransStatusUpdateInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Transaction.TransactionUpdate.getTransactionStatusUpdateParameters(TransStatusUpdateInput, out strSPQuery, out listParam);
            var TransactionStatOutput = rep.ExecuteStoredProcedure<Entities.Transaction.TransactionStatusUpdateOutput>(strSPQuery, listParam).ToList();
            return TransactionStatOutput;
        }

        public IList<Entities.Transaction.TransactionCaseAssociationOutput> updateTransactionCaseAssociationStatus(ARC.Donor.Data.Entities.Transaction.TransactionCaseAssociationInput TransCaseAssocUpdateInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Transaction.TransactionUpdate.getTransactionCaseAssociationUpdateParameters(TransCaseAssocUpdateInput, out strSPQuery, out listParam);
            var TransactionCaseAssocOutput = rep.ExecuteStoredProcedure<Entities.Transaction.TransactionCaseAssociationOutput>(strSPQuery, listParam).ToList();
            return TransactionCaseAssocOutput;
        }
    }
}
