using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{
    public class PreferredLocator
    {
        public IList<Business.Constituents.PreferredLocator> getPreferredLocator(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.PreferredLocator gd = new Data.Constituents.PreferredLocator();
            var AcctLst = gd.getPreferredLocator(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.PreferredLocator, Business.Constituents.PreferredLocator>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.PreferredLocator>, IList<Business.Constituents.PreferredLocator>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.PreferredLocatorOutput> addPreferredLocator(Business.Constituents.PreferredLocatorInput preferredLocInput)
        {
             Mapper.CreateMap<Business.Constituents.PreferredLocatorInput, Data.Entities.Constituents.PreferredLocatorInput>();
             var input = Mapper.Map<Business.Constituents.PreferredLocatorInput, Data.Entities.Constituents.PreferredLocatorInput>(preferredLocInput);
             Data.Constituents.PreferredLocator gd = new Data.Constituents.PreferredLocator();
            var AcctLst = gd.addPreferredLocator(input);
            Mapper.CreateMap<Data.Entities.Constituents.PreferredLocatorOutput, Business.Constituents.PreferredLocatorOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.PreferredLocatorOutput>, IList<Business.Constituents.PreferredLocatorOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.PreferredLocatorOutput> updatePreferredLocator(Business.Constituents.PreferredLocatorInput preferredLocInput)
        {
            Mapper.CreateMap<Business.Constituents.PreferredLocatorInput, Data.Entities.Constituents.PreferredLocatorInput>();
            var input = Mapper.Map<Business.Constituents.PreferredLocatorInput, Data.Entities.Constituents.PreferredLocatorInput>(preferredLocInput);
            Data.Constituents.PreferredLocator gd = new Data.Constituents.PreferredLocator();
            var AcctLst = gd.updatePreferredLocator(input);
            Mapper.CreateMap<Data.Entities.Constituents.PreferredLocatorOutput, Business.Constituents.PreferredLocatorOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.PreferredLocatorOutput>, IList<Business.Constituents.PreferredLocatorOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.PreferredLocatorOutput> inactivatePreferredLocator(Business.Constituents.PreferredLocatorInput preferredLocInput)
        {
            Mapper.CreateMap<Business.Constituents.PreferredLocatorInput, Data.Entities.Constituents.PreferredLocatorInput>();
            var input = Mapper.Map<Business.Constituents.PreferredLocatorInput, Data.Entities.Constituents.PreferredLocatorInput>(preferredLocInput);
            Data.Constituents.PreferredLocator gd = new Data.Constituents.PreferredLocator();
            var AcctLst = gd.inactivatePreferredLocator(input);
            Mapper.CreateMap<Data.Entities.Constituents.PreferredLocatorOutput, Business.Constituents.PreferredLocatorOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.PreferredLocatorOutput>, IList<Business.Constituents.PreferredLocatorOutput>>(AcctLst);
            return result;
        }
    }
}
