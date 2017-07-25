using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Transaction;

namespace ARC.Donor.Data.Transaction
{
    public class TransactionSearchData : Entities.QueryLogger
    {
        public IList<Entities.Transaction.TransactionSearchOutputModel> getTransactionSearchResults(ListTransactionSearchInputModel listSearchInput)
        {
            Repository rp = new Repository();
            string Qry = SQL.Transaction.TransactionSearchSQL.getTransactionSearchResultsSQL(listSearchInput);
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var SearchResults = rp.ExecuteSqlQuery<Entities.Transaction.TransactionSearchOutputModel>(Qry).ToList();
            this._EndTime = DateTime.Now;
            return SearchResults;
        }
    }
}
