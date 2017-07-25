using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class Summary
    {
        public IList<Business.Constituents.Summary> getConstituentSummary(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.Summary gd = new Data.Constituents.Summary();
            var AcctLst = gd.getConstituentSummary(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.Summary, Business.Constituents.Summary>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.Summary>, IList<Business.Constituents.Summary>>(AcctLst);
            return result;
        }
    }
}