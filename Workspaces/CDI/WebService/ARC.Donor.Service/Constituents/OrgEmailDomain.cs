using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{
   public class OrgEmailDomain
    {
        public IList<Business.Constituents.OrgEmailDomain> getOrgEmailDomain(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.OrgEmailDomain gd = new Data.Constituents.OrgEmailDomain();
            var EmailLst = gd.getOrgEmailDomain(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.OrgEmailDomain, Business.Constituents.OrgEmailDomain>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.OrgEmailDomain>, IList<Business.Constituents.OrgEmailDomain>>(EmailLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.OrgEmailDomainOutput> addOrgEmailDomainMapping(ARC.Donor.Business.Constituents.OrgEmailDomainAddInput OrgEmailDomainAddInput)
        {
            Mapper.CreateMap<Business.Constituents.OrgEmailDomainAddInput, Data.Entities.Constituents.OrgEmailDomainAddInput>();
            var Input = Mapper.Map<Business.Constituents.OrgEmailDomainAddInput, Data.Entities.Constituents.OrgEmailDomainAddInput>(OrgEmailDomainAddInput);
            Data.Constituents.OrgEmailDomain gd = new Data.Constituents.OrgEmailDomain();
            var AcctLst = gd.addOrgEmailDomainMapping(Input);
            Mapper.CreateMap<Data.Entities.Constituents.OrgEmailDomainOutput, Business.Constituents.OrgEmailDomainOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.OrgEmailDomainOutput>, IList<Business.Constituents.OrgEmailDomainOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.OrgEmailDomainOutput> deleteOrgEmailDomainMapping(ARC.Donor.Business.Constituents.OrgEmailDomainDeleteInput OrgEmailDomainDeleteInput)
        {
            Mapper.CreateMap<Business.Constituents.OrgEmailDomainDeleteInput, Data.Entities.Constituents.OrgEmailDomainDeleteInput>();
            var Input = Mapper.Map<Business.Constituents.OrgEmailDomainDeleteInput, Data.Entities.Constituents.OrgEmailDomainDeleteInput>(OrgEmailDomainDeleteInput);
            Data.Constituents.OrgEmailDomain gd = new Data.Constituents.OrgEmailDomain();
            var AcctLst = gd.deleteOrgEmailDomainMapping(Input);
            Mapper.CreateMap<Data.Entities.Constituents.OrgEmailDomainOutput, Business.Constituents.OrgEmailDomainOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.OrgEmailDomainOutput>, IList<Business.Constituents.OrgEmailDomainOutput>>(AcctLst);
            return result;
        }
    }
}
