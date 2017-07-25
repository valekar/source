using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ARC.Donor.Business.Orgler.EnterpriseOrgs;

namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class Tags
    {
        /* Method name:getTagsDetails
     * Input Parameters: Enterprise org id whose tags needs to be known.
     * Output Parameters: A list of TagOutputModel class  
     * Purpose: This method get the tags details of particular enterprise */
        public IList<Business.Orgler.EnterpriseOrgs.TagOutputModel> getTagsDetails(int NoOfRecs, int PageNum, string enterpriseOrgId)
        {
            //Instantiate the data layer object for tags functionality
            Data.Orgler.EnterpriseOrgs.Tags gd = new Data.Orgler.EnterpriseOrgs.Tags();

            //call the data layer method to find the tags of an enterprise from database.
            var TagsLst = gd.getTagsSQLResults(NoOfRecs, PageNum, enterpriseOrgId);

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.TagOutputModel, Business.Orgler.EnterpriseOrgs.TagOutputModel>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.TagOutputModel>, IList<Business.Orgler.EnterpriseOrgs.TagOutputModel>>(TagsLst);
            return result;
        }

        /* Purpose: This method is used to remove/add tags to an enterprise */
        public IList<Business.Orgler.EnterpriseOrgs.TagUpdateOutputModel> updateTags(TagUpdateInputModel input)
        {
            //Instantiate the data layer object for tags functionality
            Data.Orgler.EnterpriseOrgs.Tags gd = new Data.Orgler.EnterpriseOrgs.Tags();

            Mapper.CreateMap<Business.Orgler.EnterpriseOrgs.TagUpdateInputModel, Data.Entities.Orgler.EnterpriseOrgs.TagUpdateInputModel>();
            //call the data layer method to find the tags of an enterprise from database.
            var TagsLst = gd.updateTags(Mapper.Map<Business.Orgler.EnterpriseOrgs.TagUpdateInputModel, Data.Entities.Orgler.EnterpriseOrgs.TagUpdateInputModel>(input));

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.TagUpdateOutputModel, Business.Orgler.EnterpriseOrgs.TagUpdateOutputModel>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.TagUpdateOutputModel>, IList<Business.Orgler.EnterpriseOrgs.TagUpdateOutputModel>>(TagsLst);
            return result;
        }

        /* Purpose: This method is used to fetch all the active tags */
        public IList<Business.Orgler.EnterpriseOrgs.TagDDList> getTagDDList()
        {
            //Instantiate the data layer object for tags functionality
            Data.Orgler.EnterpriseOrgs.Tags gd = new Data.Orgler.EnterpriseOrgs.Tags();

            //call the data layer method to find the tags of an enterprise from database.
            var TagsLst = gd.getTagDDList();

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.TagDDList, Business.Orgler.EnterpriseOrgs.TagDDList>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.TagDDList>, IList<Business.Orgler.EnterpriseOrgs.TagDDList>>(TagsLst);
            return result;
        }
    }
}
