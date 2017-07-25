using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public class ConstituentMaster
    {
        public IList<Entities.Constituents.ConstituentMaster> getConstituentMasterDetails(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.ConstituentMaster>(SQL.Constituents.ConstituentMaster.getConstituentMasterSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}
