using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class MasteringAttempts
    {
        public IList<Business.Constituents.MasteringAttempts> getConstituentMasteringAttempts(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.MasteringAttempts gd = new Data.Constituents.MasteringAttempts();
            var AcctLst = gd.getConstituentMasteringAttempts(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.MasteringAttempts, Business.Constituents.MasteringAttempts>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.MasteringAttempts>, IList<Business.Constituents.MasteringAttempts>>(AcctLst);
            return result;
        }
    }
}