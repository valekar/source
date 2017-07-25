using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using Teradata.Client.Provider;
using ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs;
namespace ARC.Donor.Data.SQL.Orgler.EnterpriseOrgs
{
    public class Hierarchy
    {
        //static query to know the hierarchy
        static readonly string strHierarchyQuery = @"select * from arc_orgler_vws.ent_org_rlshp where superior_ent_org_key = ?
        or subord_ent_org_key = ?;";


        /* Method name: getHierarchySQL
* Input Parameters:enterprise org id whose hieracrchy needs to found
* Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
* Purpose:This method is used to know the hierarchy of an enterprise.  */
        public static CrudOperationOutput getHierarchySQL(int NoOfRecords, int PageNumber,string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for hierarchy
            crudOperationsOutput.strSPQuery = strHierarchyQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        public static CrudOperationOutput getHierarchyTreeSQL(string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for hierarchy
            crudOperationsOutput.strSPQuery = @"SEL *
                                                FROM arc_orgler_vws.ent_org_dtl_hier
                                                WHERE lvl1_ent_org_id IN 
                                                (
                                                    SELECT DISTINCT lvl1_ent_org_id
                                                    FROM arc_orgler_vws.ent_org_id_hier
                                                    WHERE (lvl1_ent_org_id IN (?)
			                                            OR lvl2_ent_org_id IN (?)
			                                            OR lvl3_ent_org_id IN (?)
			                                            OR lvl4_ent_org_id IN (?)
			                                            OR lvl5_ent_org_id IN (?))
                                                )";

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        //Method to build the SP call statement for performing updates to hierarchy
        public static CrudOperationOutput getHierarchyUpdateSQL(HierarchyUpdateInput input)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            int intInputParameters = 6;
            List<string> outParams = new List<string> { "o_outputMessage", "o_transaction_key" };

            //populate the query part of the object with the query for transformations
            crudOperationsOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_ent_org_hierarchy", intInputParameters, outParams);

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_superior_ent_org_key", input.superior_ent_org_key, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_subodinate_ent_org_key", input.subodinate_ent_org_key, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_rlshp_desc", input.rlshp_desc, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_rlshp_cd", input.rlshp_cd, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_userid", input.userid, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_action_type", input.action_type, "IN", TdType.VarChar, 100));

            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }
    }
}
