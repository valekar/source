using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class TransactionHistory
    {
        /* Method name:getTagsDetails
        * Input Parameters: Enterprise org id whose tags needs to be known.
        * Output Parameters: A list of TagOutputModel class  
        * Purpose: This method get the tags details of particular enterprise */
        public IList<Business.Orgler.EnterpriseOrgs.TransactionHistoryOutputModel> getTransactionHistoryDetails(int NoOfRecs, int PageNum, string enterpriseOrgId)
        {
            //Instantiate the data layer object for tags functionality
            Data.Orgler.EnterpriseOrgs.TransactionHistory gd = new Data.Orgler.EnterpriseOrgs.TransactionHistory();

            //call the data layer method to find the tags of an enterprise from database.
            var TransHistLst = gd.getTransactionHistoryDetails(NoOfRecs, PageNum, enterpriseOrgId);

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.TransactionHistoryOutputModel, Business.Orgler.EnterpriseOrgs.TransactionHistoryOutputModel>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.TransactionHistoryOutputModel>, IList<Business.Orgler.EnterpriseOrgs.TransactionHistoryOutputModel>>(TransHistLst);
            return result;
        }
    }
}
