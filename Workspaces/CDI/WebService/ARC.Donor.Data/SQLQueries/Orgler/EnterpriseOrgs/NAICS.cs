using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Orgler.EnterpriseOrgs
{
    public class NAICS
    {
        //static string to get the NAICS Details query
        static readonly string strNAICSDetailsQuery = @"SELECT	*
                   FROM	arc_orgler_vws.ent_org_dtl_naics
                   where ent_org_id = ? ; ";

        /* Method name: getNAICSDetails
        * Input Parameters: An object containing the Master id for which the naics details need to be retrieved
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose: This method gets all the NAICS details for an input master id */
        public static CrudOperationOutput getNAICSDetails(int NoOfRecords, int PageNumber, string strEntOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for naics details
            crudOperationsOutput.strSPQuery = strNAICSDetailsQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", strEntOrgId, "IN", TdType.BigInt, 100));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }
    }
}
