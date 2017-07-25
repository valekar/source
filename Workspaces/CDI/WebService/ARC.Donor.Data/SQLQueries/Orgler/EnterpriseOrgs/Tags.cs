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
    public class Tags
    {
        static readonly string strTagQuery = @" select  * from arc_orgler_vws.ent_org_dtl_tags where ent_org_id = ? order by tag;";

        /* Method name: getTagsSQL
        * Input Parameters:enterprise org id whose tags needs to found
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose:This method is used to know the tags of an enterprise.  */
        public static CrudOperationOutput getTagsSQL(int NoOfRecords, int PageNumber,string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for tags
            crudOperationsOutput.strSPQuery = strTagQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 100));
         
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        /* Method name: updateTagsSQL
        * Input Parameters:Input to the SP
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose: This method is used to remove/add tags to an enterprise */
        public static CrudOperationOutput updateTagsSQL(TagUpdateInputModel input)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //Framing the SP Call statement
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_outputTrans" };
            int intNumberOfInputParameters = 4;

            //populate the query part of the object with the query for tags
            crudOperationsOutput.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.sp_ld_ent_tag_mappings", intNumberOfInputParameters, listOutputParameters);

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_key", input.ent_org_id, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_tag", input.tag, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_action_type", input.action_type, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", input.user_nm, "IN", TdType.VarChar, 100));

            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        /* Method name: getTagDDListSQL
        * Input Parameters: NA
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose: This method is used to get the active tag records which user can use it for the selection */
        public static CrudOperationOutput getTagDDListSQL()
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for tags
            crudOperationsOutput.strSPQuery = @"SELECT *
                                                FROM arc_orgler_vws.tags
                                                ORDER BY tag";

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }
    }
}
