using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class MasteringAttempts
    {
        public IList<Entities.Constituents.MasteringAttempts> getConstituentMasteringAttempts(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.MasteringAttempts>(SQL.Constituents.MasteringAttempts.getMasteringAttemptsSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}