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
    public class NAICS
    {
        //static string to get the NAICS Details query
        static readonly string strNAICSDetailsQuery = @"SELECT	distinct cnst_mstr_id,sts,
                   naics_cd, naics_indus_title, naics_indus_dsc, conf_weightg, rule_keywrd
                   FROM	arc_orgler_vws.orgler_cnst_naics_cd_dtl
                   where cnst_mstr_id = ? ; ";

        //static string to get the ALL NAICS Details query
        static readonly string strAllNAICSDetailsQuery = @"SELECT naics_key, naics_cd, naics_indus_title, naics_indus_dsc, 
                    naics_lvl FROM arc_orgler_vws.orgler_naics_ref_cd 
                    where act_ind = 1 order by naics_lvl;  ";

        /* Method name: getNAICSDetails
        * Input Parameters: An object containing the Master id for which the naics details need to be retrieved
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose: This method gets all the NAICS details for an input master id */
        public static CrudOperationOutput getNAICSDetails(int NoOfRecords, int PageNumber, GetMasterNAICSDetailsInput masterDetailsInput)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for naics details
            crudOperationsOutput.strSPQuery = strNAICSDetailsQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("cnst_mstr_id", masterDetailsInput.cnst_mstr_id, "IN", TdType.BigInt, 100));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        /* Method name: getALLNAICSCodes
        * Input Parameters: NA
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose: This method gets all the NAICS details in hierachy */
        public static CrudOperationOutput getALLNAICSCodes()
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for all naics details
            crudOperationsOutput.strSPQuery = strAllNAICSDetailsQuery;

            //create a list of paramaters required for this query, add them(no parameters for this method) and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;

        }

        /* Method name: getNAICSStatusChangeCodeParameter
        * Input Parameters: An object of NAICSStatusChangeInput class which has the list of values for submitting values for status change of naics codes for a master
        * Output Parameters: An object of CrudOperationOutput class which contains the SP query and the parameters required for execution.
        * Purpose: This method is used to change the status(approve, reject or add) of naics codes for a single master */
        public static CrudOperationOutput getNAICSStatusChangeCodeParameter(NAICSStatusChangeInput naicsStatusChangeInput)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crud = new CrudOperationOutput();

            //populate the count of number of input parameters
            int intNumberOfInputParameters = 6;

            //create a list of all the output parameters
            List<string> listOutputParameters = new List<string> { "o_trans_key", "o_message" };

            //create the SP query query using the input parameter count and output parameters and populate it to the crud object
            crud.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.orgler_naics_create_upd", intNumberOfInputParameters, listOutputParameters);
            
            //create a list of parameters that have to be passed to the procedure
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_mstr_id", naicsStatusChangeInput.cnst_mstr_id, "IN", TdType.BigInt, 100));
           
            string strNAICSApprovedCodeString = string.Empty;
            string strNAICSRejectedCodeString = string.Empty;
            string strNAICSAddedCodeString = string.Empty;
            string strNAICSAddedTitleString = string.Empty;
            //using the list, create different csv of naics codes for approved, rejected and added values for the input master id
            if (naicsStatusChangeInput.approved_naics_codes != null)
            {
                foreach (string s in naicsStatusChangeInput.approved_naics_codes)
                    strNAICSApprovedCodeString = strNAICSApprovedCodeString == string.Empty ? "" + s + "" : strNAICSApprovedCodeString + "," + s + "";
            }
            if (naicsStatusChangeInput.rejected_naics_codes != null)
            {
                foreach (string s in naicsStatusChangeInput.rejected_naics_codes)
                    strNAICSRejectedCodeString = strNAICSRejectedCodeString == string.Empty ? "" + s + "" : strNAICSRejectedCodeString + "," + s + "";
            }
            if (naicsStatusChangeInput.added_naics_codes != null)
            {
                foreach (string s in naicsStatusChangeInput.added_naics_codes)
                    strNAICSAddedCodeString = strNAICSAddedCodeString == string.Empty ? "" + s + "" : strNAICSAddedCodeString + "," + s + "";
            }            
            ParamObjects.Add(SPHelper.createTdParameter("i_csv_app_naics_cd", strNAICSApprovedCodeString, "IN", TdType.VarChar, 500));
            ParamObjects.Add(SPHelper.createTdParameter("i_csv_rej_naics_cd", strNAICSRejectedCodeString, "IN", TdType.VarChar, 500));
            ParamObjects.Add(SPHelper.createTdParameter("i_csv_add_naics_cd", strNAICSAddedCodeString, "IN", TdType.VarChar, 500));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_org_nm", naicsStatusChangeInput.cnst_org_nm, "IN", TdType.VarChar, 500));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", !string.IsNullOrEmpty(naicsStatusChangeInput.usr_nm) ? naicsStatusChangeInput.usr_nm : "StuartAdmin", "IN", TdType.VarChar, 50));
            
            //populate the parameters to the crud object's parameter property
            crud.parameters = ParamObjects;

            //return the crud object to the calling method
            return crud;
        }
    }
}
