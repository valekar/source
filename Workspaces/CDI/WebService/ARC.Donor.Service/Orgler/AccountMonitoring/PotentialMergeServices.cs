using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Business;


using NLog;
using AutoMapper;
using ARC.Donor.Business.Orgler.AccountMonitoring;

namespace ARC.Donor.Service.Orgler.AccountMonitoring
{
   public  class PotentialMergeServices
    {
        /* Method name: getMergeResults
          * Input Parameters: Master id
          * Output Parameters: A list of PotentialMergeOutput class  
          * Purpose: This method gets the potential merge master and related information for an input master id */
        public IList<ARC.Donor.Business.Orgler.AccountMonitoring.PotentialMergeOutput> getMergeResults(int NoOfRecords, int PageNumber,string masterId)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.AccountMonitoring.PotentialMergeOutput, ARC.Donor.Business.Orgler.AccountMonitoring.PotentialMergeOutput>();

            //Instantiate the data layer object for potential merge functionality
            ARC.Donor.Data.Orgler.AccountMonitoring.PotentialMerge pm = new ARC.Donor.Data.Orgler.AccountMonitoring.PotentialMerge();

            //call the data layer method to get potential merge results from DB
            var searchResults = pm.getPotentialMergeResults(NoOfRecords,PageNumber,masterId);

            //for each record returned, concatinate the address component and store it in the respective variable
            foreach(Data.Entities.Orgler.AccountMonitoring.PotentialMergeOutput potentialMerge in searchResults)
            {
                string strAddress = string.Empty;
                if (!string.IsNullOrEmpty(potentialMerge.addr_line_1))
                    strAddress = potentialMerge.addr_line_1;
                if (!string.IsNullOrEmpty(potentialMerge.addr_line_2))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + "," + potentialMerge.addr_line_2 : potentialMerge.addr_line_2;
                if (!string.IsNullOrEmpty(potentialMerge.city))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + "," + potentialMerge.city : potentialMerge.city;
                if (!string.IsNullOrEmpty(potentialMerge.state))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + "," + potentialMerge.state : potentialMerge.state;
                if (!string.IsNullOrEmpty(potentialMerge.zip))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + "," + potentialMerge.zip : potentialMerge.zip;
                potentialMerge.address = strAddress;
            }

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.AccountMonitoring.PotentialMergeOutput>, IList<ARC.Donor.Business.Orgler.AccountMonitoring.PotentialMergeOutput>>(searchResults);

            //return the results back to the controller
            return result;
        } 
    }
}
