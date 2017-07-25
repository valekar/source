using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class RFM
    {
        public IList<ARC.Donor.Business.Orgler.EnterpriseOrgs.RFMDetails> getRFMDetails(int NoOfRecs, int PageNum, string strEntOrgId)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.RFMDetails, Business.Orgler.EnterpriseOrgs.RFMDetails>();
            //Instantiate the data layer object for confirm functionality
            Data.Orgler.EnterpriseOrgs.RFM rfm = new Data.Orgler.EnterpriseOrgs.RFM();

            //call the data layer method to get NAICS details from the database
            var RFMDetails = rfm.getRFMDetails(NoOfRecs, PageNum, strEntOrgId);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.RFMDetails>, IList<Business.Orgler.EnterpriseOrgs.RFMDetails>>(RFMDetails);

            //return the results back to the controller
            return result;
        }
    }
}
