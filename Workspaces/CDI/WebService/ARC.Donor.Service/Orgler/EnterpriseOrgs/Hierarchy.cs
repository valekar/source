using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class Hierarchy
    {
        /* Method name: getHierarchyDetails
     * Input Parameters: Enterprise org id whose hierarchy needs to be known.
     * Output Parameters: A list of OrgHierarchyOutputModel class  
     * Purpose: This method get the hierarchy details of particular enterprise */
        public IList<Business.Orgler.EnterpriseOrgs.OrgHierarchyOutputModel> getHierarchyDetails(int NoOfRecs, int PageNum, string enterpriseOrgId)
        {
            //Instantiate the data layer object for hierarchy functionality
            Data.Orgler.EnterpriseOrgs.Hierarchy gd = new Data.Orgler.EnterpriseOrgs.Hierarchy();

            //call the data layer method to find the hierarchy of an enterprise from database.
            var HierarchyLst = gd.getHierarchySQLResults(NoOfRecs, PageNum, enterpriseOrgId);

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.OrgHierarchyOutputModel, Business.Orgler.EnterpriseOrgs.OrgHierarchyOutputModel>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.OrgHierarchyOutputModel>, IList<Business.Orgler.EnterpriseOrgs.OrgHierarchyOutputModel>>(HierarchyLst);
            return result;
        }

        public IList<Business.Orgler.EnterpriseOrgs.OrganizationHierarchyOutputModel> getHierarchyResults(string enterpriseOrgId)
        {
            //Instantiate the data layer object for hierarchy functionality
            Data.Orgler.EnterpriseOrgs.Hierarchy gd = new Data.Orgler.EnterpriseOrgs.Hierarchy();

            //call the data layer method to find the hierarchy of an enterprise from database.
            var HierarchyLst = gd.getHierarchyResults(enterpriseOrgId);

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.OrganizationHierarchyOutputModel, Business.Orgler.EnterpriseOrgs.OrganizationHierarchyOutputModel>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.OrganizationHierarchyOutputModel>, IList<Business.Orgler.EnterpriseOrgs.OrganizationHierarchyOutputModel>>(HierarchyLst);
            return result;
        }

        //Method to update EO Hierarchy
        public IList<Business.Orgler.EnterpriseOrgs.HierarchyUpdateOutput> updateHierarchy(Business.Orgler.EnterpriseOrgs.HierarchyUpdateInput input)
        {
            //Instantiate the data layer object for hierarchy functionality
            Data.Orgler.EnterpriseOrgs.Hierarchy gd = new Data.Orgler.EnterpriseOrgs.Hierarchy();

            Mapper.CreateMap<Business.Orgler.EnterpriseOrgs.HierarchyUpdateInput, Data.Entities.Orgler.EnterpriseOrgs.HierarchyUpdateInput>();
            
            //call the data layer method to find the hierarchy of an enterprise from database.
            var HierarchyLst = gd.updateHierarchy(Mapper.Map<Business.Orgler.EnterpriseOrgs.HierarchyUpdateInput, Data.Entities.Orgler.EnterpriseOrgs.HierarchyUpdateInput>(input));

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.HierarchyUpdateOutput, Business.Orgler.EnterpriseOrgs.HierarchyUpdateOutput>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.HierarchyUpdateOutput>, IList<Business.Orgler.EnterpriseOrgs.HierarchyUpdateOutput>>(HierarchyLst);
            return result;
        }
    }
}
