using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class OrgEmailDomain
    {
        public static string getOrgEmailSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                  PageNumber, string.Join(",", Master_id),
                  (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                  (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT *
        FROM arc_mdm_vws.bzfc_locator_email_domain
        WHERE cnst_mstr_id = {2};";

        /* Method name: getDeleteMasterEmailDomainParameters
       * Input Parameters: An object of OrgEmailDomainAddInput class which has the master id and other information to deactivate an email domain mapping
       * Output Parameters: An object of CrudOperationOutput class which contains the SP query and the parameters required for execution.
       * Purpose: This method is used to deactivate a mapping between the master and email domain*/
        public static CrudOperationOutput getDeleteOrgEmailDomainMappingParameters(OrgEmailDomainDeleteInput orgEmailDomainDeleteInput)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crud = new CrudOperationOutput();

            //populate the count of number of input parameters
            int intNumberOfInputParameters = 6;

            //create a list of all the output parameters
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_transaction_key" };

            //create the SP query query using the input parameter count and output parameters and populate it to the crud object
            crud.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.org_del_email_domain_mstr_map", intNumberOfInputParameters, listOutputParameters);

            //create a list of parameters that have to be passed to the procedure
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", orgEmailDomainDeleteInput.MasterID, "IN", TdType.BigInt, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_email_domain", orgEmailDomainDeleteInput.EmailDomain, "IN", TdType.VarChar, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", orgEmailDomainDeleteInput.ConstType, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", orgEmailDomainDeleteInput.UserName, "IN", TdType.VarChar, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", orgEmailDomainDeleteInput.CaseNumber, "IN", TdType.BigInt, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", orgEmailDomainDeleteInput.Notes, "IN", TdType.VarChar, 200));

            //populate the parameters to the crud object's parameter property
            crud.parameters = ParamObjects;

            //return the crud object to the calling method
            return crud;
        }

        /* Method name: getAddMasterEmailDomainParameters
       * Input Parameters: An object of OrgEmailDomainAddInput class which has the master id and other information to add an email domain mapping
       * Output Parameters: An object of CrudOperationOutput class which contains the SP query and the parameters required for execution.
       * Purpose: This method is used to add a mapping between the master and email domain*/
        public static CrudOperationOutput getAddOrgEmailDomainMappingParameters(OrgEmailDomainAddInput orgEmailDomainAddInput)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crud = new CrudOperationOutput();

            //populate the count of number of input parameters
            int intNumberOfInputParameters = 6;

            //create a list of all the output parameters
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_transaction_key" };

            //create the SP query query using the input parameter count and output parameters and populate it to the crud object
            crud.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.org_add_email_domain_mstr_map", intNumberOfInputParameters, listOutputParameters);

            //create a list of parameters that have to be passed to the procedure
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", orgEmailDomainAddInput.MasterID, "IN", TdType.BigInt, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_email_domain", orgEmailDomainAddInput.EmailDomain, "IN", TdType.VarChar, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", orgEmailDomainAddInput.ConstType, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", orgEmailDomainAddInput.UserName, "IN", TdType.VarChar, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", orgEmailDomainAddInput.CaseNumber, "IN", TdType.BigInt, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", orgEmailDomainAddInput.Notes, "IN", TdType.VarChar, 200));

            //populate the parameters to the crud object's parameter property
            crud.parameters = ParamObjects;

            //return the crud object to the calling method
            return crud;
        }
    }
}
