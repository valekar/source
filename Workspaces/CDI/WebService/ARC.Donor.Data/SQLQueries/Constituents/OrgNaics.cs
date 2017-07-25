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
    public class OrgNaics
    {
        public static string getOrgNaicsSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT	distinct cnst_mstr_id,sts,
                   naics_cd, naics_indus_title, naics_indus_dsc, conf_weightg, rule_keywrd
                   FROM	arc_orgler_vws.orgler_cnst_naics_cd_dtl
                   where cnst_mstr_id = {2} ;";

        /* Method name: getNAICSStatusChangeCodeParameter
        * Input Parameters: An object of NAICSStatusChangeInput class which has the list of values for submitting values for status change of naics codes for a master
        * Output Parameters: An object of CrudOperationOutput class which contains the SP query and the parameters required for execution.
        * Purpose: This method is used to change the status(approve, reject or add) of naics codes for a single master */
        public static CrudOperationOutput getNAICSStatusChangeCodeParameter(NAICSStatusUpdateInput naicsStatusChangeInput)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crud = new CrudOperationOutput();

            //populate the count of number of input parameters
            int intNumberOfInputParameters = 6;

            //create a list of all the output parameters
            List<string> listOutputParameters = new List<string> { "o_trans_key", "o_output_message" };

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

