using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{
  public  class OrgNaics
    {
        public IList<Business.Constituents.OrgNaics> getOrgNAICS(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.OrgNaics gd = new Data.Constituents.OrgNaics();
            var NAICSLst = gd.getOrgNaics(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.OrgNaics, Business.Constituents.OrgNaics>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.OrgNaics>, IList<Business.Constituents.OrgNaics>>(NAICSLst);
            return result;
        }

        /* Method name: naicsStatusChange
          * Input Parameters: An object of NAICSStatusChangeInput class
          * Output Parameters: A list of TransactionResult class  
          * Purpose: This method updates the status of naics codes(approve,reject or add) for the input master */
        public IList<ARC.Donor.Business.Orgler.AccountMonitoring.TransactionResult> naicsStatusChange(ARC.Donor.Business.Constituents.NAICSStatusUpdateInput NAICSStatusChangeInput)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<Business.Constituents.NAICSStatusUpdateInput, Data.Entities.Constituents.NAICSStatusUpdateInput>();
            Mapper.CreateMap<Data.Entities.Orgler.AccountMonitoring.TransactionResult, Business.Orgler.AccountMonitoring.TransactionResult>();

            //map the input from buisness object to the data layer object
            var Input = Mapper.Map<Business.Constituents.NAICSStatusUpdateInput, Data.Entities.Constituents.NAICSStatusUpdateInput>(NAICSStatusChangeInput);

            //Instantiate the data layer object for confirm functionality
            Data.Constituents.OrgNaics naics = new Data.Constituents.OrgNaics();

            //call the data layer method to update the status for naics codes in the database
            var AcctLst = naics.naicsStatusChange(Input);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.AccountMonitoring.TransactionResult>, IList<Business.Orgler.AccountMonitoring.TransactionResult>>(AcctLst);

            //return the results back to the controller
            return result;
        }
    }
}
