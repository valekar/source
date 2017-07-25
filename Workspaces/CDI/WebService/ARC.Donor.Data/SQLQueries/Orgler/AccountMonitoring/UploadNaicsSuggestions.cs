using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Orgler.AccountMonitoring;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Orgler.AccountMonitoring
{
 public   class UploadNaicsSuggestions
    {
        /* Method name: postNaicsSuggestionsUploadCreateTrans
        * Input Parameters: userId- to store the User ID while creation of transaction
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose: This method creates new transaction for NAICS Suggestions upload */
        public static CrudOperationOutput postNaicsSuggestionsUploadCreateTrans(string userId)
        {
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 7;
            List<string> listOutputParameters = new List<string> { "transKey", "transOutput" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_inst_trans", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("typ", "naics_upld", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("subTyp", "naics_upld", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("actionType", DBNull.Value, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("transStat", "In Progress", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("transNotes", "Naics Suggestions Upload", "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("userId", !string.IsNullOrEmpty(userId) ? userId : "StuartAdmin", "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("caseSeqNum", DBNull.Value, "IN", TdType.BigInt, 20));

            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }
        /* Method name: insertUploadNAICSSuggestionsRecords
       * Input Parameters:An object of UploadNAICSSuggestionsInput class which contains the all input data parameters and tran_key to store in the DB
       * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
       * Purpose: This method insert the NAICS Suggestions upload data into the database stage table*/
        public static CrudOperationOutput insertUploadNAICSSuggestionsRecords(UploadNAICSSuggestionsInput nsu, 
            long strTransactionKey)
        {
            CrudOperationOutput crudOutput = new CrudOperationOutput();          

            int intNumberOfInputParameters = 10;           
            List<string> listOutputParameters = new List<string> { "o_output_message" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.orgler_naics_upld", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
           
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_mstr_id", nsu.cnst_mstr_id, "IN", TdType.BigInt, 50));           
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_org_nm", nsu.cnst_org_nm, "IN", TdType.VarChar, 500));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_org_addrs", nsu.cnst_org_addrs, "IN", TdType.VarChar, 50));           
            ParamObjects.Add(SPHelper.createTdParameter("i_naics_cd", nsu.naics_cd, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_naics_title", nsu.naics_title, "IN", TdType.VarChar, 500));
            ParamObjects.Add(SPHelper.createTdParameter("i_naics_map_rule_key", nsu.naics_map_rule_key, "IN", TdType.VarChar, 500));
            ParamObjects.Add(SPHelper.createTdParameter("i_sts", nsu.sts, "IN", TdType.VarChar, 500));
            ParamObjects.Add(SPHelper.createTdParameter("i_action", nsu.action, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", !string.IsNullOrEmpty(nsu.user_id) ? nsu.user_id : "StuartAdmin", "IN", TdType.VarChar, 500));
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_key", strTransactionKey, "IN", TdType.BigInt, 100));

            crudOutput.parameters = ParamObjects;           
            return crudOutput;
        }
        /* Method name: UpdateNAICSSuggestionsParameter
     * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
     * Purpose: This method Updates NAICS Suggestions data into the database */
        public static CrudOperationOutput UpdateNAICSSuggestionsParameter()
        {
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            List<string> listOutputParameters = new List<string> { };
            int intNumberOfInputParameters = 0;
            var ParamObjects = new List<object>();
            crudOutput.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.orgler_naics_updt_upld", intNumberOfInputParameters, listOutputParameters);
            crudOutput.parameters = ParamObjects;
            return crudOutput;          

        }

        //static string to get the potential merge query
        static readonly string strUploadMetricQuery = @"SELECT	trans_key, rej_cnt, trgt_cnt 
                from arc_orgler_vws.orgler_naics_upld_metric
				where trans_key = ? ";

        public static CrudOperationOutput getUploadMetricSQL(int NoOfRecords, int PageNumber, long strTransactionKey)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for upload metric
            crudOperationsOutput.strSPQuery = strUploadMetricQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("trans_key", strTransactionKey, "IN", TdType.BigInt, 100));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

    }
}
