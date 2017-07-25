using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class PreferenceLocator
    {


        public static string getPreferenceLocatorSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }


        static readonly string Qry = @"select * from dw_stuart_vws.bz_strx_cnst_pref_loc WHERE cnst_mstr_id = {2}
        AND   (trans_status NOT IN ('Rejected') 
        OR  trans_status IS NULL) 
        AND   ((trans_status IN ('Reject') 
        AND   unique_trans_key IS NOT NULL 
        AND   unique_trans_key <> '') 
        OR  (trans_status IN ('Processed') 
        AND   strx_row_stat_cd = 'F' 
        AND   unique_trans_key IS NOT NULL 
        AND   unique_trans_key <> '') 
        OR  (trans_status NOT IN ('Reject','Processed')) 
        OR  trans_status IS NULL) 
        ORDER  by transaction_key, load_id";


        public static string getAllPreferenceLocatorSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(showQry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string showQry = @"select * from arc_cmm_vws.bz_cnst_pref_loc WHERE cnst_mstr_id = {2}";

        public static string getPreferenceLocatorOptionsSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(OptionsQry, NoOfRecords,
                    PageNumber, string.Join(",", Master_id),
                    (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                    (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }
        static readonly string OptionsQry = @"sel * from dw_stuart_vws.bz_strx_cnst_mstr_loc  WHERE cnst_mstr_id = {2}";

        public static CrudOperationOutput addPrefLocatorParamters(ARC.Donor.Data.Entities.Constituents.PreferenceLocatorInput prefInput)
        {

            string requestType = "insert";
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_pref_loc", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", prefInput.masterId, "IN", TdType.BigInt, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ",prefInput.constituentType, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_num_seq", prefInput.caseNo, "IN", TdType.BigInt, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", prefInput.lineOfService, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", prefInput.notes, "IN", TdType.VarChar, 5000));
            //ParamObjects.Add(SPHelper.createTdParameter("i_created_by", prefInput.createdBy, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", prefInput.userId, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", prefInput.oldSourceSystemCode, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_pref_loc_typ", prefInput.oldPrefLocType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_pref_loc_id", prefInput.oldPrefLocId, "IN", TdType.BigInt, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", prefInput.newSourceSystemCode, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_pref_loc_typ", prefInput.newPrefLocType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_pref_loc_id", prefInput.newPrefLocId, "IN", TdType.BigInt, 20));

            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }

        public static CrudOperationOutput deletePrefLocatorParamters(ARC.Donor.Data.Entities.Constituents.PreferenceLocatorInput prefInput)
        {

            string requestType = "delete";
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_pref_loc", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", prefInput.masterId, "IN", TdType.BigInt, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", prefInput.constituentType, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_num_seq", prefInput.caseNo, "IN", TdType.BigInt, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", prefInput.lineOfService, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", prefInput.notes, "IN", TdType.VarChar, 5000));
            //ParamObjects.Add(SPHelper.createTdParameter("i_created_by", prefInput.createdBy, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", prefInput.userId, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", prefInput.oldSourceSystemCode, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_pref_loc_typ", prefInput.oldPrefLocType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_pref_loc_id", prefInput.oldPrefLocId, "IN", TdType.BigInt, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", prefInput.newSourceSystemCode, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_pref_loc_typ", prefInput.newPrefLocType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_pref_loc_id", prefInput.newPrefLocId, "IN", TdType.BigInt, 20));

            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }



        public static CrudOperationOutput editPrefLocatorParamters(ARC.Donor.Data.Entities.Constituents.PreferenceLocatorInput prefInput)
        {

            string requestType = "update";
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_pref_loc", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", prefInput.masterId, "IN", TdType.BigInt, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", prefInput.constituentType, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_num_seq", prefInput.caseNo, "IN", TdType.BigInt, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", prefInput.lineOfService, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", prefInput.notes, "IN", TdType.VarChar, 5000));
            //ParamObjects.Add(SPHelper.createTdParameter("i_created_by", prefInput.createdBy, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", prefInput.userId, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", prefInput.oldSourceSystemCode, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_pref_loc_typ", prefInput.oldPrefLocType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_pref_loc_id", prefInput.oldPrefLocId, "IN", TdType.BigInt, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", prefInput.newSourceSystemCode, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_pref_loc_typ", prefInput.newPrefLocType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_pref_loc_id", prefInput.newPrefLocId, "IN", TdType.BigInt, 20));

            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }
    }
}
