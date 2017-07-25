using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class ARCBest
    {
        public IList<Entities.Constituents.ARCBest> getARCBest(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.ARCBest>(SQL.Constituents.ARCBest.getARCBestSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}