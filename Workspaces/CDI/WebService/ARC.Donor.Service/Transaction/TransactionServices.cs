using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Business;
using ARC.Donor.Business.Transaction;
using ARC.Donor.Data.Transaction;
using NLog;
using AutoMapper;

namespace ARC.Donor.Service.Transaction
{
    public class TransactionServices : QueryLogger
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        public IList<ARC.Donor.Business.Transaction.TransactionSearchOutputModel> getTransactionSearchResults(ListTransactionSearchInputModel listSearchInput)
        {
            Mapper.CreateMap<ARC.Donor.Business.Transaction.TransactionSearchInputModel, Data.Entities.Transaction.TransactionSearchInputModel>();
            Mapper.CreateMap<ARC.Donor.Business.Transaction.ListTransactionSearchInputModel, Data.Entities.Transaction.ListTransactionSearchInputModel>();
            Mapper.CreateMap<Data.Entities.Transaction.TransactionSearchOutputModel, ARC.Donor.Business.Transaction.TransactionSearchOutputModel>();
            var Input = Mapper.Map<ARC.Donor.Business.Transaction.ListTransactionSearchInputModel, ARC.Donor.Data.Entities.Transaction.ListTransactionSearchInputModel>(listSearchInput);
            TransactionSearchData tsd = new TransactionSearchData();
            var searchResults = tsd.getTransactionSearchResults(Input);
            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionSearchOutputModel>, IList<ARC.Donor.Business.Transaction.TransactionSearchOutputModel>>(searchResults);

            OnInsertQueryLogger("Transaction", tsd.Query, tsd.QueryStartTime, tsd.QueryEndTime, "");
            return result;
        }

    }
}
