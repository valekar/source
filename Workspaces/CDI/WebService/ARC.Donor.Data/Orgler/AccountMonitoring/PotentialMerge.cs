using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Orgler.AccountMonitoring;
using ARC.Donor.Data.Entities;

namespace ARC.Donor.Data.Orgler.AccountMonitoring
{
   public class PotentialMerge
    {
        /* Method name: getPotentialMergeResults
          * Input Parameters: Master id for which the potential merge results need to be retrieved
          * Output Parameters: A list of PotentialMergeOutput class.
          * Purpose: This method gets the Potential Merge results i.e., master and their relative information for an input master id */
        public IList<Entities.Orgler.AccountMonitoring.PotentialMergeOutput> getPotentialMergeResults(int NoOfRecords, int PageNumber, string masterId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            //Instantiate an object of the Repository class
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.AccountMonitoring.PotentialMerge.getPotentialMergeSQL(NoOfRecords, PageNumber, masterId);
            
            //execute the query using the statetment and the parameters retrieved above. 
            var MergeOutput = rep.ExecuteStoredProcedure<Entities.Orgler.AccountMonitoring.PotentialMergeOutput>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();
            
            //return the results back to service
            return MergeOutput;
        }
    }
}
