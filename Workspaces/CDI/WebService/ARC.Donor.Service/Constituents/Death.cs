using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class Death
    {
        public IList<Business.Constituents.Death> getConstituentDeath(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.Death gd = new Data.Constituents.Death();
            var AcctLst = gd.getConstituentDeath(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.Death, Business.Constituents.Death>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.Death>, IList<Business.Constituents.Death>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentDeathOutput> addConstituentDeath(ARC.Donor.Business.Constituents.ConstituentDeathInput ConstDeathInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentDeathInput, Data.Entities.Constituents.ConstituentDeathInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentDeathInput, Data.Entities.Constituents.ConstituentDeathInput>(ConstDeathInput);
            Data.Constituents.Death gd = new Data.Constituents.Death();
            var AcctLst = gd.addConstituentDeath(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentDeathOutput, Business.Constituents.ConstituentDeathOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentDeathOutput>, IList<Business.Constituents.ConstituentDeathOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentDeathOutput> deleteConstituentDeath(ARC.Donor.Business.Constituents.ConstituentDeathInput ConstDeathInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentDeathInput, Data.Entities.Constituents.ConstituentDeathInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentDeathInput, Data.Entities.Constituents.ConstituentDeathInput>(ConstDeathInput);
            Data.Constituents.Death gd = new Data.Constituents.Death();
            var AcctLst = gd.deleteConstituentDeath(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentDeathOutput, Business.Constituents.ConstituentDeathOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentDeathOutput>, IList<Business.Constituents.ConstituentDeathOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentDeathOutput> updateConstituentDeath(ARC.Donor.Business.Constituents.ConstituentDeathInput ConstDeathInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentDeathInput, Data.Entities.Constituents.ConstituentDeathInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentDeathInput, Data.Entities.Constituents.ConstituentDeathInput>(ConstDeathInput);
            Data.Constituents.Death gd = new Data.Constituents.Death();
            var AcctLst = gd.updateConstituentDeath(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentDeathOutput, Business.Constituents.ConstituentDeathOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentDeathOutput>, IList<Business.Constituents.ConstituentDeathOutput>>(AcctLst);
            return result;
        }
    }
}