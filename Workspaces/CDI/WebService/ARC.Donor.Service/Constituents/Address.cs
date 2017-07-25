using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class Address
    {
        public IList<Business.Constituents.Address> getConstituentAddress(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.Address gd = new Data.Constituents.Address();
            var AcctLst = gd.getConstituentAddress(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.Address, Business.Constituents.Address>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.Address>, IList<Business.Constituents.Address>>(AcctLst);
            return result;
        }


        public IList<ARC.Donor.Business.Constituents.ConstituentAddressOutput> addConstituentAddress(ARC.Donor.Business.Constituents.ConstituentAddressInput ConstAddressInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentAddressInput, Data.Entities.Constituents.ConstituentAddressInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentAddressInput, Data.Entities.Constituents.ConstituentAddressInput>(ConstAddressInput);
            Data.Constituents.Address gd = new Data.Constituents.Address();
            var AcctLst = gd.addConstituentAddress(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentAddressOutput, Business.Constituents.ConstituentAddressOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentAddressOutput>, IList<Business.Constituents.ConstituentAddressOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentAddressOutput> deleteConstituentAddress(ARC.Donor.Business.Constituents.ConstituentAddressInput ConstAddressInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentAddressInput, Data.Entities.Constituents.ConstituentAddressInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentAddressInput, Data.Entities.Constituents.ConstituentAddressInput>(ConstAddressInput);
            Data.Constituents.Address gd = new Data.Constituents.Address();
            var AcctLst = gd.deleteConstituentAddress(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentAddressOutput, Business.Constituents.ConstituentAddressOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentAddressOutput>, IList<Business.Constituents.ConstituentAddressOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentAddressOutput> updateConstituentAddress(ARC.Donor.Business.Constituents.ConstituentAddressInput ConstAddressInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentAddressInput, Data.Entities.Constituents.ConstituentAddressInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentAddressInput, Data.Entities.Constituents.ConstituentAddressInput>(ConstAddressInput);
            Data.Constituents.Address gd = new Data.Constituents.Address();
            var AcctLst = gd.updateConstituentAddress(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentAddressOutput, Business.Constituents.ConstituentAddressOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentAddressOutput>, IList<Business.Constituents.ConstituentAddressOutput>>(AcctLst);
            return result;
        }
    }
}