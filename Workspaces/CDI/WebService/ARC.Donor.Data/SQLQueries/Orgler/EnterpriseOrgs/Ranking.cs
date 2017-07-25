using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Orgler.EnterpriseOrgs
{
    public class Ranking
    {
        //static query to know the hierarchy
        static readonly string strRankingQuery = @"select  * 
                        from arc_orgler_vws.ent_org_dtl_rnk  
                        where ent_org_id = ?";

        /* Method name: getHierarchySQL
        * Input Parameters:enterprise org id whose hieracrchy needs to found
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose:This method is used to know the hierarchy of an enterprise.  */
        public static CrudOperationOutput getRankingSQL(int NoOfRecords, int PageNumber,string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for hierarchy
            crudOperationsOutput.strSPQuery = strRankingQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }
    }
}
