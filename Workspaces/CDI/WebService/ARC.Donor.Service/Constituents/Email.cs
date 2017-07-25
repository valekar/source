using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class Email
    {
        public IList<Business.Constituents.Email> getConstituentEmail(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.Email gd = new Data.Constituents.Email();
            var AcctLst = gd.getConstituentEmail(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.Email, Business.Constituents.Email>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.Email>, IList<Business.Constituents.Email>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentEmailOutput> addConstituentEmail(ARC.Donor.Business.Constituents.ConstituentEmailInput ConstEmailInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentEmailInput, Data.Entities.Constituents.ConstituentEmailInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentEmailInput, Data.Entities.Constituents.ConstituentEmailInput>(ConstEmailInput);
            Data.Constituents.Email gd = new Data.Constituents.Email();
            var AcctLst = gd.addConstituentEmail(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentEmailOutput, Business.Constituents.ConstituentEmailOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentEmailOutput>, IList<Business.Constituents.ConstituentEmailOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.ConstituentEmailOutput> deleteConstituentEmail(ARC.Donor.Business.Constituents.ConstituentEmailInput ConstEmailInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentEmailInput, Data.Entities.Constituents.ConstituentEmailInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentEmailInput, Data.Entities.Constituents.ConstituentEmailInput>(ConstEmailInput);
            Data.Constituents.Email gd = new Data.Constituents.Email();
            var AcctLst = gd.deleteConstituentEmail(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentEmailOutput, Business.Constituents.ConstituentEmailOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentEmailOutput>, IList<Business.Constituents.ConstituentEmailOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.ConstituentEmailOutput> updateConstituentEmail(ARC.Donor.Business.Constituents.ConstituentEmailInput ConstEmailInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentEmailInput, Data.Entities.Constituents.ConstituentEmailInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentEmailInput, Data.Entities.Constituents.ConstituentEmailInput>(ConstEmailInput);
            Data.Constituents.Email gd = new Data.Constituents.Email();
            var AcctLst = gd.updateConstituentEmail(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentEmailOutput, Business.Constituents.ConstituentEmailOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentEmailOutput>, IList<Business.Constituents.ConstituentEmailOutput>>(AcctLst);
            return result;
        }
    }
}