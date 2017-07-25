using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public class Anonymous
    {
        public IList<Entities.Constituents.Anonymous> getConstituentAnonymousInformation(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.Anonymous>(SQL.Constituents.Anonymous.getAnonymousSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}
