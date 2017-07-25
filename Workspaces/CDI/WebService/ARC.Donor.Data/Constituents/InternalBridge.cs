using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class InternalBridge
    {
        public IList<Entities.Constituents.InternalBridge> getConstituentInternalBridge(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.InternalBridge>(SQL.Constituents.InternalBridge.getInternalBridgeSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}