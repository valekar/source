using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class NAICS
    {
        /* Method name: getOrgNAICSDetails
         * Input Parameters: An object of GetMasterNAICSDetailsInput class
         * Output Parameters: A list of GetMasterNAICSDetailsOutput class  
         * Purpose: This method gets the naics details for a single master */
        public IList<ARC.Donor.Business.Orgler.EnterpriseOrgs.GetNAICSDetailsOutput> getNAICSDetails(int NoOfRecs, int PageNum, string strEntOrgId)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.GetNAICSDetailsOutput, Business.Orgler.EnterpriseOrgs.GetNAICSDetailsOutput>();
            //Instantiate the data layer object for confirm functionality
             Data.Orgler.EnterpriseOrgs.NAICS naics = new Data.Orgler.EnterpriseOrgs.NAICS();

            //call the data layer method to get NAICS details from the database
            var NAICSDetails = naics.getNAICSDetails(NoOfRecs, PageNum, strEntOrgId);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.GetNAICSDetailsOutput>, IList<Business.Orgler.EnterpriseOrgs.GetNAICSDetailsOutput>>(NAICSDetails);

            //return the results back to the controller
            return result;
        }
    }
}
