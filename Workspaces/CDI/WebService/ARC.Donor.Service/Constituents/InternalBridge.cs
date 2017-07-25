using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class InternalBridge
    {
        public IList<Business.Constituents.InternalBridge> getConstituentInternalBridge(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.InternalBridge gd = new Data.Constituents.InternalBridge();
            var AcctLst = gd.getConstituentInternalBridge(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.InternalBridge, Business.Constituents.InternalBridge>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.InternalBridge>, IList<Business.Constituents.InternalBridge>>(AcctLst);
            return result;
        }
    }
}