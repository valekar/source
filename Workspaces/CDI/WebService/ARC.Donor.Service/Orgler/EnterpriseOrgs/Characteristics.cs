using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class Characteristics
    {
        public IList<Business.Orgler.EnterpriseOrgs.Characteristics> getOrgCharacteristics(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Orgler.EnterpriseOrgs.Characteristics gd = new Data.Orgler.EnterpriseOrgs.Characteristics();
            var AcctLst = gd.getOrgCharacteristics(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.Characteristics, Business.Orgler.EnterpriseOrgs.Characteristics>();
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.Characteristics>, IList<Business.Orgler.EnterpriseOrgs.Characteristics>>(AcctLst);
            return result;
        }

        public IList<Business.Orgler.EnterpriseOrgs.Characteristics> getOrgAllCharacteristics(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Orgler.EnterpriseOrgs.Characteristics gd = new Data.Orgler.EnterpriseOrgs.Characteristics();
            var AcctLst = gd.getOrgAllCharacteristics(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.Characteristics, Business.Orgler.EnterpriseOrgs.Characteristics>();
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.Characteristics>, IList<Business.Orgler.EnterpriseOrgs.Characteristics>>(AcctLst);
            return result;
        }


        public IList<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput> addOrgCharacteristics(Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput OrgCharInput)
        {
            Mapper.CreateMap<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput, Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsInput>();
            var Input = Mapper.Map<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput, Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsInput>(OrgCharInput);
            Data.Orgler.EnterpriseOrgs.Characteristics gd = new Data.Orgler.EnterpriseOrgs.Characteristics();
            var AcctLst = gd.addOrgCharacteristics(Input);
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput, Business.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>, IList<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput> editOrgCharacteristics(Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput OrgCharInput)
        {
            Mapper.CreateMap<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput, Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsInput>();
            var Input = Mapper.Map<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput, Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsInput>(OrgCharInput);
            Data.Orgler.EnterpriseOrgs.Characteristics gd = new Data.Orgler.EnterpriseOrgs.Characteristics();
            var AcctLst = gd.editOrgCharacteristics(Input);
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput, Business.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>, IList<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>>(AcctLst);
            return result;
        }


        public IList<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput> deleteOrgCharacteristics(Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput OrgCharInput)
        {
            Mapper.CreateMap<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput, Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsInput>();
            var Input = Mapper.Map<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput, Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsInput>(OrgCharInput);
            Data.Orgler.EnterpriseOrgs.Characteristics gd = new Data.Orgler.EnterpriseOrgs.Characteristics();
            var AcctLst = gd.deleteOrgCharacteristics(Input);
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput, Business.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>, IList<Business.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>>(AcctLst);
            return result;
        }
        
    }
}
