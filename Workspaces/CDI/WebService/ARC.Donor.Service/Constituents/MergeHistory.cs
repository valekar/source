using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class MergeHistory
    {
        public IList<Business.Constituents.MergeHistory> getConstituentMergeHistory(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.MergeHistory gd = new Data.Constituents.MergeHistory();
            var AcctLst = gd.getConstituentMergeHistory(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.MergeHistory, Business.Constituents.MergeHistory>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.MergeHistory>, IList<Business.Constituents.MergeHistory>>(AcctLst);
            return result;
        }
    }
}