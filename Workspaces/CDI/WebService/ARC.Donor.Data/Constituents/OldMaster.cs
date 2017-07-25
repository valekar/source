using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class OldMaster
    {
        public IList<Entities.Constituents.OldMaster> getConstituentOldMasters(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.OldMaster>(SQL.Constituents.OldMaster.getOldMasterSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}