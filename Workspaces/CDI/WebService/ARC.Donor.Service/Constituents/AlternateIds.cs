using ARC.Donor.Business.Constituents;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{

    public interface IAlternateIds
    {
         IList<Business.Constituents.AlternateIds> getSourceSystemAlternateIds(int NoOfRecs, int PageNum, string id);
         IList<Business.Constituents.AlternateIds> getSourceSystemAlternateIds(int NoOfRecs, int PageNum, AlternateIdsInput input);
    }


    public class AlternateIds : IAlternateIds
    {
        public IList<Business.Constituents.AlternateIds> getSourceSystemAlternateIds(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.AlternateIds gd = new Data.Constituents.AlternateIds();
            var AcctLst = gd.getSourceSystemAlternateIds(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.AlternateIds, Business.Constituents.AlternateIds>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.AlternateIds>, IList<Business.Constituents.AlternateIds>>(AcctLst);
            return result;
        }


        public IList<Business.Constituents.AlternateIds> getSourceSystemAlternateIds(int NoOfRecs, int PageNum, AlternateIdsInput input)
        {
           
            Mapper.CreateMap<Business.Constituents.AlternateIdsInput,Data.Entities.Constituents.AlternateIdsInput>();
            Mapper.CreateMap<Data.Entities.Constituents.AlternateIds, Business.Constituents.AlternateIds>();
            Data.Constituents.AlternateIds gd = new Data.Constituents.AlternateIds();
            var input_data = Mapper.Map<Business.Constituents.AlternateIdsInput,Data.Entities.Constituents.AlternateIdsInput>(input);
            var AcctLst = gd.getSourceSystemAlternateIds(NoOfRecs, PageNum, input_data);
            
            var result = Mapper.Map<IList<Data.Entities.Constituents.AlternateIds>, IList<Business.Constituents.AlternateIds>>(AcctLst);
            return result;
        }
    }
}
