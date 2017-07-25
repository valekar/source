using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class Birth
    {
        public IList<Business.Constituents.Birth> getConstituentBirth(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.Birth gd = new Data.Constituents.Birth();
            var AcctLst = gd.getConstituentBirth(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.Birth, Business.Constituents.Birth>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.Birth>, IList<Business.Constituents.Birth>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentBirthOutput> addConstituentBirth(ARC.Donor.Business.Constituents.ConstituentBirthInput ConstBirthInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentBirthInput, Data.Entities.Constituents.ConstituentBirthInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentBirthInput, Data.Entities.Constituents.ConstituentBirthInput>(ConstBirthInput);
            Data.Constituents.Birth gd = new Data.Constituents.Birth();
            var AcctLst = gd.addConstituentBirth(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentBirthOutput, Business.Constituents.ConstituentBirthOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentBirthOutput>, IList<Business.Constituents.ConstituentBirthOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentBirthOutput> deleteConstituentBirth(ARC.Donor.Business.Constituents.ConstituentBirthInput ConstBirthInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentBirthInput, Data.Entities.Constituents.ConstituentBirthInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentBirthInput, Data.Entities.Constituents.ConstituentBirthInput>(ConstBirthInput);
            Data.Constituents.Birth gd = new Data.Constituents.Birth();
            var AcctLst = gd.deleteConstituentBirth(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentBirthOutput, Business.Constituents.ConstituentBirthOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentBirthOutput>, IList<Business.Constituents.ConstituentBirthOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentBirthOutput> updateConstituentBirth(ARC.Donor.Business.Constituents.ConstituentBirthInput ConstBirthInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentBirthInput, Data.Entities.Constituents.ConstituentBirthInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentBirthInput, Data.Entities.Constituents.ConstituentBirthInput>(ConstBirthInput);
            Data.Constituents.Birth gd = new Data.Constituents.Birth();
            var AcctLst = gd.updateConstituentBirth(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentBirthOutput, Business.Constituents.ConstituentBirthOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentBirthOutput>, IList<Business.Constituents.ConstituentBirthOutput>>(AcctLst);
            return result;
        }
    }
}