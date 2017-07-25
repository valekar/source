using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class ARCBest
    {
        public IList<Business.Constituents.ARCBest> getARCBest(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.ARCBest gd = new Data.Constituents.ARCBest();
            var AcctLst = gd.getARCBest(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.ARCBest, Business.Constituents.ARCBest>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ARCBest>, IList<Business.Constituents.ARCBest>>(AcctLst);
            return result;
        }
    }
}