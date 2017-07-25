using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ARC.Donor.Service.Constituents
{
    public class Anonymous
    {
        public IList<Business.Constituent.Anonymous> getConstituentAnonymousInformation(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.Anonymous an = new Data.Constituents.Anonymous();
            var resultList = an.getConstituentAnonymousInformation(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.Anonymous, Business.Constituent.Anonymous>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.Anonymous>, IList<Business.Constituent.Anonymous>>(resultList);
            return result;
        }
    }
}
