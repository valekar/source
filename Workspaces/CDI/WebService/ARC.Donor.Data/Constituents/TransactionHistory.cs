using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class TransactionHistory
    {
        public IList<Entities.Constituents.TransactionHistory> getConstituentTransactionHistory(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.TransactionHistory>(SQL.Constituents.TransactionHistory.getTransactionHistorySQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}