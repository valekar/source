using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class Private
    {
        public IList<Business.Constituents.Private> getConstituentPrivateInformation(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.Private gd = new Data.Constituents.Private();
            var AcctLst = gd.getConstituentPrivateInformation(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.Private, Business.Constituents.Private>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.Private>, IList<Business.Constituents.Private>>(AcctLst);
            return result;
        }
    }
}