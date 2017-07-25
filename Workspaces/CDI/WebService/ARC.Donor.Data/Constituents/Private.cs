using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class Private
    {
        public IList<Entities.Constituents.Private> getConstituentPrivateInformation(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.Private>(SQL.Constituents.Private.getPrivateSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}