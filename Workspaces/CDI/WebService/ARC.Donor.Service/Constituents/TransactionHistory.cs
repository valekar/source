using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class TransactionHistory
    {
        public IList<Business.Constituents.TransactionHistory> getConstituentTransactionHistory(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.TransactionHistory gd = new Data.Constituents.TransactionHistory();
            var AcctLst = gd.getConstituentTransactionHistory(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.TransactionHistory, Business.Constituents.TransactionHistory>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.TransactionHistory>, IList<Business.Constituents.TransactionHistory>>(AcctLst);
            return result;
        }
    }
}