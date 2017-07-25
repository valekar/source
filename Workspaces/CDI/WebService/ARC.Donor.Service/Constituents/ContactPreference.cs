using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class ContactPreference:QueryLogger
    {
        public IList<Business.Constituents.ContactPreference> getConstituentContactPreference(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.ContactPreference gd = new Data.Constituents.ContactPreference();
            
            var AcctLst = gd.getConstituentContactPreference(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.ContactPreference, Business.Constituents.ContactPreference>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ContactPreference>, IList<Business.Constituents.ContactPreference>>(AcctLst);
            //**** Added event to catpture Query Execution Time
            OnInsertQueryLogger("Constituent Preference", gd.Query, gd.QueryStartTime, gd.QueryEndTime,"");
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentContactPrefcOutput> addConstituentContactPrefc(ARC.Donor.Business.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentContactPrefcInput, Data.Entities.Constituents.ConstituentContactPrefcInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentContactPrefcInput, Data.Entities.Constituents.ConstituentContactPrefcInput>(ConstContactPrefcInput);
            Data.Constituents.ContactPreference gd = new Data.Constituents.ContactPreference();
           
            var AcctLst = gd.addConstituentContactPrefc(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentContactPrefcOutput, Business.Constituents.ConstituentContactPrefcOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentContactPrefcOutput>, IList<Business.Constituents.ConstituentContactPrefcOutput>>(AcctLst);
            //**** Added event to catpture Query Execution Time
            OnInsertQueryLogger("Add Preference", gd.Query, gd.QueryStartTime, gd.QueryEndTime, ConstContactPrefcInput.UserName);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentContactPrefcOutput> deleteConstituentContactPrefc(ARC.Donor.Business.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentContactPrefcInput, Data.Entities.Constituents.ConstituentContactPrefcInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentContactPrefcInput, Data.Entities.Constituents.ConstituentContactPrefcInput>(ConstContactPrefcInput);
            Data.Constituents.ContactPreference gd = new Data.Constituents.ContactPreference();
            var AcctLst = gd.deleteConstituentContactPrefc(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentContactPrefcOutput, Business.Constituents.ConstituentContactPrefcOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentContactPrefcOutput>, IList<Business.Constituents.ConstituentContactPrefcOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentContactPrefcOutput> updateConstituentContactPrefc(ARC.Donor.Business.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentContactPrefcInput, Data.Entities.Constituents.ConstituentContactPrefcInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentContactPrefcInput, Data.Entities.Constituents.ConstituentContactPrefcInput>(ConstContactPrefcInput);
            Data.Constituents.ContactPreference gd = new Data.Constituents.ContactPreference();
            var AcctLst = gd.updateConstituentContactPrefc(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentContactPrefcOutput, Business.Constituents.ConstituentContactPrefcOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentContactPrefcOutput>, IList<Business.Constituents.ConstituentContactPrefcOutput>>(AcctLst);
            return result;
        }
    }
}