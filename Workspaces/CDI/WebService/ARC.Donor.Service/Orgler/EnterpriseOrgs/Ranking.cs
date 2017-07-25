using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class Ranking
    {
        /* Method name:getTagsDetails
    * Input Parameters: Enterprise org id whose tags needs to be known.
    * Output Parameters: A list of TagOutputModel class  
    * Purpose: This method get the tags details of particular enterprise */
        public IList<Business.Orgler.EnterpriseOrgs.RankingOutputModel> getRankingDetails(int NoOfRecs, int PageNum, string enterpriseOrgId)
        {
            //Instantiate the data layer object for tags functionality
            Data.Orgler.EnterpriseOrgs.Ranking gd = new Data.Orgler.EnterpriseOrgs.Ranking();

            //call the data layer method to find the tags of an enterprise from database.
            var RankingLst = gd.getRankingDetails(NoOfRecs, PageNum, enterpriseOrgId);

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.RankingOutputModel, Business.Orgler.EnterpriseOrgs.RankingOutputModel>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.RankingOutputModel>, IList<Business.Orgler.EnterpriseOrgs.RankingOutputModel>>(RankingLst);
            return result;
        }
    }
}
