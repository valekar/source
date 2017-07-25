using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class MergeHistory
    {
        public IList<Entities.Constituents.MergeHistory> getConstituentMergeHistory(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.MergeHistory>(SQL.Constituents.MergeHistory.getMergeHistorySQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}