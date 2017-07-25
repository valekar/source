using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class OldMaster
    {
        public IList<Business.Constituents.OldMaster> getConstituentOldMasters(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.OldMaster gd = new Data.Constituents.OldMaster();
            var AcctLst = gd.getConstituentOldMasters(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.OldMaster, Business.Constituents.OldMaster>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.OldMaster>, IList<Business.Constituents.OldMaster>>(AcctLst);
            return result;
        }
    }
}