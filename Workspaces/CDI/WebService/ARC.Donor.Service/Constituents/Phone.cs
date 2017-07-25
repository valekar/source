using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class Phone
    {
        public IList<ARC.Donor.Business.Constituents.Phone> getConstituentPhone(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.Phone gd = new Data.Constituents.Phone();
            var AcctLst = gd.getConstituentPhone(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.Phone, ARC.Donor.Business.Constituents.Phone>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.Phone>, IList<Business.Constituents.Phone>>(AcctLst);
            return result;
            
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentPhoneOutput> addConstituentPhone(ARC.Donor.Business.Constituents.ConstituentPhoneInput ConstPhoneInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentPhoneInput, Data.Entities.Constituents.ConstituentPhoneInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentPhoneInput, Data.Entities.Constituents.ConstituentPhoneInput>(ConstPhoneInput);
            Data.Constituents.Phone gd = new Data.Constituents.Phone();
            var AcctLst = gd.addConstituentPhone(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentPhoneOutput, Business.Constituents.ConstituentPhoneOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentPhoneOutput>, IList<Business.Constituents.ConstituentPhoneOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.ConstituentPhoneOutput> deleteConstituentPhone(ARC.Donor.Business.Constituents.ConstituentPhoneInput ConstPhoneInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentPhoneInput, Data.Entities.Constituents.ConstituentPhoneInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentPhoneInput, Data.Entities.Constituents.ConstituentPhoneInput>(ConstPhoneInput);
            Data.Constituents.Phone gd = new Data.Constituents.Phone();
            var AcctLst = gd.deleteConstituentPhone(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentPhoneOutput, Business.Constituents.ConstituentPhoneOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentPhoneOutput>, IList<Business.Constituents.ConstituentPhoneOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.ConstituentPhoneOutput> updateConstituentPhone(ARC.Donor.Business.Constituents.ConstituentPhoneInput ConstPhoneInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentPhoneInput, Data.Entities.Constituents.ConstituentPhoneInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentPhoneInput, Data.Entities.Constituents.ConstituentPhoneInput>(ConstPhoneInput);
            Data.Constituents.Phone gd = new Data.Constituents.Phone();
            var AcctLst = gd.updateConstituentPhone(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentPhoneOutput, Business.Constituents.ConstituentPhoneOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentPhoneOutput>, IList<Business.Constituents.ConstituentPhoneOutput>>(AcctLst);
            return result;
        }
    }
}