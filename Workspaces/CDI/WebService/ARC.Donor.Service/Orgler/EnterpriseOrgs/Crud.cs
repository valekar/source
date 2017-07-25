using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class Crud
    {

        public IList<Business.Orgler.EnterpriseOrgs.GetEnterpriseOrgModel> getEntOrg(int NoOfRecs, int PageNum, string ent_org_id)
        {
            Data.Orgler.EnterpriseOrgs.Crud gd = new Data.Orgler.EnterpriseOrgs.Crud();
            var EntOrgLst = gd.getEnterpriseOrg(NoOfRecs, PageNum, ent_org_id);
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.GetEnterpriseOrgModel, Business.Orgler.EnterpriseOrgs.GetEnterpriseOrgModel>();
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.GetEnterpriseOrgModel>, IList<Business.Orgler.EnterpriseOrgs.GetEnterpriseOrgModel>>(EntOrgLst);
            return result;
        }
        public IList<ARC.Donor.Business.Orgler.EnterpriseOrgs.CreateEnterpriseOrgOutputModel> createEntOrg(ARC.Donor.Business.Orgler.EnterpriseOrgs.CreateEnterpriseOrgInputModel CreateEntOrgInput)
        {
            Mapper.CreateMap<Business.Orgler.EnterpriseOrgs.CreateEnterpriseOrgInputModel, Data.Entities.Orgler.EnterpriseOrgs.CreateEnterpriseOrgInputModel>();
            var Input = Mapper.Map<Business.Orgler.EnterpriseOrgs.CreateEnterpriseOrgInputModel, Data.Entities.Orgler.EnterpriseOrgs.CreateEnterpriseOrgInputModel>(CreateEntOrgInput);
            Data.Orgler.EnterpriseOrgs.Crud gd = new Data.Orgler.EnterpriseOrgs.Crud();
            var EntOrgLst = gd.createEntOrg(Input);
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.CreateEnterpriseOrgOutputModel, Business.Orgler.EnterpriseOrgs.CreateEnterpriseOrgOutputModel>();
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.CreateEnterpriseOrgOutputModel>, IList<Business.Orgler.EnterpriseOrgs.CreateEnterpriseOrgOutputModel>>(EntOrgLst);
            return result;
        }
        public IList<ARC.Donor.Business.Orgler.EnterpriseOrgs.EditEnterpriseOrgOutputModel> updateEntOrg(ARC.Donor.Business.Orgler.EnterpriseOrgs.EditEnterpriseOrgInputModel EditEntOrgInput)
        {
            Mapper.CreateMap<Business.Orgler.EnterpriseOrgs.EditEnterpriseOrgInputModel, Data.Entities.Orgler.EnterpriseOrgs.EditEnterpriseOrgInputModel>();
            var Input = Mapper.Map<Business.Orgler.EnterpriseOrgs.EditEnterpriseOrgInputModel, Data.Entities.Orgler.EnterpriseOrgs.EditEnterpriseOrgInputModel>(EditEntOrgInput);
            Data.Orgler.EnterpriseOrgs.Crud gd = new Data.Orgler.EnterpriseOrgs.Crud();
            var EntOrgLst = gd.updateEntOrg(Input);
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.EditEnterpriseOrgOutputModel, Business.Orgler.EnterpriseOrgs.EditEnterpriseOrgOutputModel>();
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.EditEnterpriseOrgOutputModel>, IList<Business.Orgler.EnterpriseOrgs.EditEnterpriseOrgOutputModel>>(EntOrgLst);
            return result;
        }
        public IList<ARC.Donor.Business.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgOutputModel> deleteEntOrg(ARC.Donor.Business.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgInputModel DeleteEntOrgInput)
        {
            Mapper.CreateMap<Business.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgInputModel, Data.Entities.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgInputModel>();
            var Input = Mapper.Map<Business.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgInputModel, Data.Entities.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgInputModel>(DeleteEntOrgInput);
            Data.Orgler.EnterpriseOrgs.Crud gd = new Data.Orgler.EnterpriseOrgs.Crud();
            var EntOrgLst = gd.deleteEntOrg(Input);
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgOutputModel, Business.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgOutputModel>();
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgOutputModel>, IList<Business.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgOutputModel>>(EntOrgLst);
            return result;
        }

    }
}
