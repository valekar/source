using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public class ConstituentDNC
    {
        public IList<Entities.Constituents.ConstituentDNC> getConstituentDNCrecords(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.ConstituentDNC>(SQL.Constituents.ConstituentDNC.getCnstDNCSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
    }
}
