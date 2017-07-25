using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Business;
using ARC.Donor.Data.Transaction;
using NLog;
using AutoMapper;

namespace ARC.Donor.Service.Transaction
{
    public class TransactionUpdateServices
    {
        public IList<ARC.Donor.Business.Transaction.TransactionUpdate.TransactionStatusUpdateOutput> updateTransactionStatus(ARC.Donor.Business.Transaction.TransactionUpdate.TransactionStatusUpdateInput TransStatusUpdateInput)
        {
            Mapper.CreateMap<Business.Transaction.TransactionUpdate.TransactionStatusUpdateInput, Data.Entities.Transaction.TransactionStatusUpdateInput>();
            var Input = Mapper.Map<Business.Transaction.TransactionUpdate.TransactionStatusUpdateInput, Data.Entities.Transaction.TransactionStatusUpdateInput>(TransStatusUpdateInput);
            Data.Transaction.TransactionUpdate transUpdate = new Data.Transaction.TransactionUpdate();
            var AcctLst = transUpdate.updateTransactionStatus(Input);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionStatusUpdateOutput, Business.Transaction.TransactionUpdate.TransactionStatusUpdateOutput>();
            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionStatusUpdateOutput>, IList<Business.Transaction.TransactionUpdate.TransactionStatusUpdateOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Transaction.TransactionUpdate.TransactionCaseAssociationOutput> updateTransactionCaseAssocation(ARC.Donor.Business.Transaction.TransactionUpdate.TransactionCaseAssociationInput TransCaseAssocUpdateInput)
        {
            Mapper.CreateMap<Business.Transaction.TransactionUpdate.TransactionCaseAssociationInput, Data.Entities.Transaction.TransactionCaseAssociationInput>();
            var Input = Mapper.Map<Business.Transaction.TransactionUpdate.TransactionCaseAssociationInput, Data.Entities.Transaction.TransactionCaseAssociationInput>(TransCaseAssocUpdateInput);
            Data.Transaction.TransactionUpdate transUpdate = new Data.Transaction.TransactionUpdate();
            var AcctLst = transUpdate.updateTransactionCaseAssociationStatus(Input);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionCaseAssociationOutput, Business.Transaction.TransactionUpdate.TransactionCaseAssociationOutput>();
            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionCaseAssociationOutput>, IList<Business.Transaction.TransactionUpdate.TransactionCaseAssociationOutput>>(AcctLst);
            return result;
        }
    }
}
