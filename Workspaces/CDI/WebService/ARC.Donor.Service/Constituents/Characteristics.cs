using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class Characteristicscs
    {
        public IList<Business.Constituents.Characteristics> getConstituentCharacteristics(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.Characteristics gd = new Data.Constituents.Characteristics();
            var AcctLst = gd.getConstituentCharacteristics(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.Characteristics, Business.Constituents.Characteristics>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.Characteristics>, IList<Business.Constituents.Characteristics>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentCharacteristicsOutput> addConstituentCharacteristics(ARC.Donor.Business.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentCharacteristicsInput, Data.Entities.Constituents.ConstituentCharacteristicsInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentCharacteristicsInput, Data.Entities.Constituents.ConstituentCharacteristicsInput>(ConstCharacteristicsInput);
            Data.Constituents.Characteristics gd = new Data.Constituents.Characteristics();
            var AcctLst = gd.addConstituentCharacteristics(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentCharacteristicsOutput, Business.Constituents.ConstituentCharacteristicsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentCharacteristicsOutput>, IList<Business.Constituents.ConstituentCharacteristicsOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentCharacteristicsOutput> deleteConstituentCharacteristics(ARC.Donor.Business.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentCharacteristicsInput, Data.Entities.Constituents.ConstituentCharacteristicsInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentCharacteristicsInput, Data.Entities.Constituents.ConstituentCharacteristicsInput>(ConstCharacteristicsInput);
            Data.Constituents.Characteristics gd = new Data.Constituents.Characteristics();
            var AcctLst = gd.deleteConstituentCharacteristics(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentCharacteristicsOutput, Business.Constituents.ConstituentCharacteristicsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentCharacteristicsOutput>, IList<Business.Constituents.ConstituentCharacteristicsOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentCharacteristicsOutput> updateConstituentCharacteristics(ARC.Donor.Business.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentCharacteristicsInput, Data.Entities.Constituents.ConstituentCharacteristicsInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentCharacteristicsInput, Data.Entities.Constituents.ConstituentCharacteristicsInput>(ConstCharacteristicsInput);
            Data.Constituents.Characteristics gd = new Data.Constituents.Characteristics();
            var AcctLst = gd.updateConstituentCharacteristics(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentCharacteristicsOutput, Business.Constituents.ConstituentCharacteristicsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentCharacteristicsOutput>, IList<Business.Constituents.ConstituentCharacteristicsOutput>>(AcctLst);
            return result;
        }
    }
}