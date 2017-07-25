using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class ExternalBridge
    {
        public IList<Business.Constituents.ExternalBridge> getConstituentExternalBridge(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.ExternalBridge gd = new Data.Constituents.ExternalBridge();
            var AcctLst = gd.getConstituentExternalBridge(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.ExternalBridge, Business.Constituents.ExternalBridge>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ExternalBridge>, IList<Business.Constituents.ExternalBridge>>(AcctLst);
            return result;
        }
    }
}