using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{
   public class ConstituentDNC
    {
       public IList<Business.Constituents.ConstituentDNC> getConstituentDNCrecords(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.ConstituentDNC gd = new Data.Constituents.ConstituentDNC();
            var AcctLst = gd.getConstituentDNCrecords(NoOfRecs, PageNum, Master_Id);

            Mapper.CreateMap<Data.Entities.Constituents.ConstituentDNC, Business.Constituents.ConstituentDNC>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentDNC>, IList<Business.Constituents.ConstituentDNC>>(AcctLst);
            return result;
        }
    }
}
