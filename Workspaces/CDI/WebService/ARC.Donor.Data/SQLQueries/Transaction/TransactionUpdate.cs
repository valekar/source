using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Transaction;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Transaction
{
    public class TransactionUpdate
    {
        public static TransactionStatusUpdateInput getTransactionStatusUpdateParameters(ARC.Donor.Data.Entities.Transaction.TransactionStatusUpdateInput TransStatusUpdateInput, out string strSPQuery, out List<object> parameters)
        {
            TransactionStatusUpdateInput TransactionHelper = new TransactionStatusUpdateInput();
            TransactionHelper = TransStatusUpdateInput; 
            int intNumberOfInputParameters = 3;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };
            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_update_trans_status", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_key", TransactionHelper.TransactionKey, "IN", TdType.BigInt, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_status", TransactionHelper.TransactionStatus, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_approver_nm", TransactionHelper.ApproverName, "IN", TdType.VarChar, 50));
            parameters = ParamObjects;
            return TransactionHelper;

        }
        public static TransactionCaseAssociationInput getTransactionCaseAssociationUpdateParameters(ARC.Donor.Data.Entities.Transaction.TransactionCaseAssociationInput TransStatusUpdateInput, out string strSPQuery, out List<object> parameters)
        {
            TransactionCaseAssociationInput TransactionHelper = new TransactionCaseAssociationInput();
            TransactionHelper = TransStatusUpdateInput; 
            int intNumberOfInputParameters = 2;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };
            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_upd_trans_case_id", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_key", TransStatusUpdateInput.TransactionKey, "IN", TdType.BigInt, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_case_num", TransStatusUpdateInput.AssociatedCaseKey, "IN", TdType.BigInt, 100));
            parameters = ParamObjects;
            return TransStatusUpdateInput;
        }
    }
}
