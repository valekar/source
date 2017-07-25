using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class Summary
    {
        public IList<Entities.Constituents.Summary> getConstituentSummary(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.Summary>(SQL.Constituents.Summary.getSummarySQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}