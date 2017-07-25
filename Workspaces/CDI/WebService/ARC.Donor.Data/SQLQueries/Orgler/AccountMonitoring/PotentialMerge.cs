using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Orgler.AccountMonitoring;
using Teradata.Client.Provider;
using ARC.Donor.Data.Entities;

namespace ARC.Donor.Data.SQL.Orgler.AccountMonitoring
{
  public class PotentialMerge
    {
        //static string to get the potential merge query
        static readonly string strPotentialMergeQuery = @"SELECT	cnst_mstr_id, pot_merge_mstr_id, 
				pot_merge_mstr_nm, pot_merge_rsn, addr_line_1, addr_line_2, city, state, zip, phone, email, dsp_id
				FROM	arc_orgler_vws.orgler_pot_merge_details
				where cnst_mstr_id = ? ";

        /* Method name: getPotentialMergeSQL
        * Input Parameters: Master id for which the potential merge results need to be retrieved
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose: This method gets the Potential Merge results i.e., master and their relative information for an input master id */
        public static CrudOperationOutput getPotentialMergeSQL(int NoOfRecords, int PageNumber, string strMasterId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for potential merge
            crudOperationsOutput.strSPQuery = strPotentialMergeQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("cnst_mstr_id", strMasterId, "IN", TdType.VarChar, 100));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }
    }
}
