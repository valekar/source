using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class ExternalBridge
    {
        public IList<Entities.Constituents.ExternalBridge> getConstituentExternalBridge(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.ExternalBridge>(SQL.Constituents.ExternalBridge.getExternalBridgeSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}