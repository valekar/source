using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{
    public class OrgAffiliators
    {
        public IList<Business.Constituents.OrgAffiliators> getConstituentOrgAffiliators(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.OrgAffiliators gd = new Data.Constituents.OrgAffiliators();
            var AcctLst = gd.getConstituentOrgAffiliators(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.OrgAffiliators, Business.Constituents.OrgAffiliators>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.OrgAffiliators>, IList<Business.Constituents.OrgAffiliators>>(AcctLst);
            return result;
        }

        /*Create and Delete services for Org Affiliators*/
        public IList<ARC.Donor.Business.Constituents.OrgAffiliatorsOutput> addOrgAffiliators(ARC.Donor.Business.Constituents.OrgAffiliatorsInput OrgAffiliatorsInput)
        {
            Mapper.CreateMap<Business.Constituents.OrgAffiliatorsInput, Data.Entities.Constituents.OrgAffiliatorsInput>();
            var Input = Mapper.Map<Business.Constituents.OrgAffiliatorsInput, Data.Entities.Constituents.OrgAffiliatorsInput>(OrgAffiliatorsInput);
            Data.Constituents.OrgAffiliators gd = new Data.Constituents.OrgAffiliators();
            var AcctLst = gd.addOrgAffiliators(Input);
            Mapper.CreateMap<Data.Entities.Constituents.OrgAffiliatorsOutput, Business.Constituents.OrgAffiliatorsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.OrgAffiliatorsOutput>, IList<Business.Constituents.OrgAffiliatorsOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.OrgAffiliatorsOutput> deleteOrgAffiliators(ARC.Donor.Business.Constituents.OrgAffiliatorsInput OrgAffiliatorsInput)
        {
            Mapper.CreateMap<Business.Constituents.OrgAffiliatorsInput, Data.Entities.Constituents.OrgAffiliatorsInput>();
            var Input = Mapper.Map<Business.Constituents.OrgAffiliatorsInput, Data.Entities.Constituents.OrgAffiliatorsInput>(OrgAffiliatorsInput);
            Data.Constituents.OrgAffiliators gd = new Data.Constituents.OrgAffiliators();
            var AcctLst = gd.deleteOrgAffiliators(Input);
            Mapper.CreateMap<Data.Entities.Constituents.OrgAffiliatorsOutput, Business.Constituents.OrgAffiliatorsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.OrgAffiliatorsOutput>, IList<Business.Constituents.OrgAffiliatorsOutput>>(AcctLst);
            return result;
        }
    }
}
