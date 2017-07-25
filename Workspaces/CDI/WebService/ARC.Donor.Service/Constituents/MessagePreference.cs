using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{
    public class MessagePreference
    {
        public IList<Business.Constituents.MessagePreference> getMessagePreference(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.MessagePreference gd = new Data.Constituents.MessagePreference();
            var AcctLst = gd.getMessagePreference(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.MessagePreference, Business.Constituents.MessagePreference>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.MessagePreference>, IList<Business.Constituents.MessagePreference>>(AcctLst);
            return result;
        }


        public IList<Business.Constituents.AllMessagePreference> getAllMessagePreference(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.MessagePreference gd = new Data.Constituents.MessagePreference();
            var AcctLst = gd.getAllMessagePreference(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.AllMessagePreference, Business.Constituents.AllMessagePreference>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.AllMessagePreference>, IList<Business.Constituents.AllMessagePreference>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.MessagePreferenceOptions> getMessagePreferenceOptions(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.MessagePreference gd = new Data.Constituents.MessagePreference();
            var AcctLst = gd.getMessagePreferenceOptions(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.MessagePreferenceOptions, Business.Constituents.MessagePreferenceOptions>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.MessagePreferenceOptions>, IList<Business.Constituents.MessagePreferenceOptions>>(AcctLst);
            return result;
        }


        public IList<ARC.Donor.Business.Constituents.MessagePreferenceOutput> addMessagePreference(ARC.Donor.Business.Constituents.MessagePreferenceInput msgPrefInput)
        {
            Mapper.CreateMap<Business.Constituents.MessagePreferenceInput, Data.Entities.Constituents.MessagePreferenceInput>();
            var Input = Mapper.Map<Business.Constituents.MessagePreferenceInput, Data.Entities.Constituents.MessagePreferenceInput>(msgPrefInput);
            Data.Constituents.MessagePreference gd = new Data.Constituents.MessagePreference();
            var AcctLst = gd.addMessagePreference(Input);
            Mapper.CreateMap<Data.Entities.Constituents.MessagePreferenceOutput, Business.Constituents.MessagePreferenceOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.MessagePreferenceOutput>, IList<Business.Constituents.MessagePreferenceOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.MessagePreferenceOutput> editMessagePreference(ARC.Donor.Business.Constituents.MessagePreferenceInput msgPrefInput)
        {
            Mapper.CreateMap<Business.Constituents.MessagePreferenceInput, Data.Entities.Constituents.MessagePreferenceInput>();
            var Input = Mapper.Map<Business.Constituents.MessagePreferenceInput, Data.Entities.Constituents.MessagePreferenceInput>(msgPrefInput);
            Data.Constituents.MessagePreference gd = new Data.Constituents.MessagePreference();
            var AcctLst = gd.editMessagePreference(Input);
            Mapper.CreateMap<Data.Entities.Constituents.MessagePreferenceOutput, Business.Constituents.MessagePreferenceOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.MessagePreferenceOutput>, IList<Business.Constituents.MessagePreferenceOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.MessagePreferenceOutput> deleteMessagePreference(ARC.Donor.Business.Constituents.MessagePreferenceInput msgPrefInput)
        {
            Mapper.CreateMap<Business.Constituents.MessagePreferenceInput, Data.Entities.Constituents.MessagePreferenceInput>();
            var Input = Mapper.Map<Business.Constituents.MessagePreferenceInput, Data.Entities.Constituents.MessagePreferenceInput>(msgPrefInput);
            Data.Constituents.MessagePreference gd = new Data.Constituents.MessagePreference();
            var AcctLst = gd.deleteMessagePreference(Input);
            Mapper.CreateMap<Data.Entities.Constituents.MessagePreferenceOutput, Business.Constituents.MessagePreferenceOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.MessagePreferenceOutput>, IList<Business.Constituents.MessagePreferenceOutput>>(AcctLst);
            return result;
        }
    }
}
