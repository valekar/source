using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{
    public class PreferredComChannel
    {
        public IList<Business.Constituents.PreferredComChannel> getPreferredComChannel(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.PreferredComChannel gd = new Data.Constituents.PreferredComChannel();
            var AcctLst = gd.getPreferredComChannel(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.PreferredComChannel, Business.Constituents.PreferredComChannel>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.PreferredComChannel>, IList<Business.Constituents.PreferredComChannel>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.PreferredComChannelOutput> addPreferredComChannel(Business.Constituents.PreferredComChannelInput preferredComChannelInput)
        {
            Mapper.CreateMap<Business.Constituents.PreferredComChannelInput, Data.Entities.Constituents.PreferredComChannelInput>();
            var input = Mapper.Map<Business.Constituents.PreferredComChannelInput, Data.Entities.Constituents.PreferredComChannelInput>(preferredComChannelInput);
            Data.Constituents.PreferredComChannel gd = new Data.Constituents.PreferredComChannel();
            var AcctLst = gd.addPreferredComChannel(input);
            Mapper.CreateMap<Data.Entities.Constituents.PreferredComChannelOutput, Business.Constituents.PreferredComChannelOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.PreferredComChannelOutput>, IList<Business.Constituents.PreferredComChannelOutput>>(AcctLst);
            return result;
        }


        public IList<Business.Constituents.PreferredComChannelOutput> updatePreferredComChannel(Business.Constituents.PreferredComChannelInput preferredComChannelInput)
        {
            Mapper.CreateMap<Business.Constituents.PreferredComChannelInput, Data.Entities.Constituents.PreferredComChannelInput>();
            var input = Mapper.Map<Business.Constituents.PreferredComChannelInput, Data.Entities.Constituents.PreferredComChannelInput>(preferredComChannelInput);
            Data.Constituents.PreferredComChannel gd = new Data.Constituents.PreferredComChannel();
            var AcctLst = gd.updatePreferredComChannel(input);
            Mapper.CreateMap<Data.Entities.Constituents.PreferredComChannelOutput, Business.Constituents.PreferredComChannelOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.PreferredComChannelOutput>, IList<Business.Constituents.PreferredComChannelOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.PreferredComChannelOutput> inactivatePreferredComChannel(Business.Constituents.PreferredComChannelInput preferredComChannelInput)
        {
            Mapper.CreateMap<Business.Constituents.PreferredComChannelInput, Data.Entities.Constituents.PreferredComChannelInput>();
            var input = Mapper.Map<Business.Constituents.PreferredComChannelInput, Data.Entities.Constituents.PreferredComChannelInput>(preferredComChannelInput);
            Data.Constituents.PreferredComChannel gd = new Data.Constituents.PreferredComChannel();
            var AcctLst = gd.inactivatePreferredComChannel(input);
            Mapper.CreateMap<Data.Entities.Constituents.PreferredComChannelOutput, Business.Constituents.PreferredComChannelOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.PreferredComChannelOutput>, IList<Business.Constituents.PreferredComChannelOutput>>(AcctLst);
            return result;
        }
    }
}
