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
    public class DoNotContact
    {
        public static string getDoNotContactSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"Sel * from  dw_stuart_vws.bz_strx_cnst_dnc where dnc_mstr_id  = {2}
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
        ORDER  by transaction_key, load_id;";

        public static string getDNCShowDetailsSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(showQry, NoOfRecords,
                                 PageNumber, string.Join(",", Master_id),
                                 (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                                 (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string showQry = @"Sel * from  arc_cmm_vws.bz_cnst_dnc where dnc_mstr_id  = {2}";

        public static CrudOperationOutput addDncParamters(DoNotContactInput dncInput)
        {
            string requestType = "insert";
            CrudOperationOutput crudOpertaionOP = new CrudOperationOutput();
            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
            crudOpertaionOP.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_dnc", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", dncInput.i_mstr_id, "IN", TdType.BigInt, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", dncInput.i_cnst_typ, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_num_seq", dncInput.i_case_num_seq, "IN", TdType.BigInt, 40));
          //  ParamObjects.Add(SPHelper.createTdParameter("i_cnst_dnc_exp_ts", dncInput.i_cnst_dnc_exp_ts, "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cnst_dnc_line_of_service_cd", dncInput.i_bk_cnst_dnc_line_of_service_cd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cnst_dnc_comm_chan", dncInput.i_bk_cnst_dnc_comm_chan, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cnst_loc_id", dncInput.i_bk_cnst_loc_id, "IN", TdType.BigInt, 40));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_cnst_dnc_line_of_service_cd", dncInput.i_new_cnst_dnc_line_of_service_cd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cnst_dnc_comm_chan", dncInput.i_new_cnst_dnc_comm_chan, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cnst_dnc_loc_id", dncInput.i_new_cnst_dnc_loc_id, "IN", TdType.BigInt, 40));

            ParamObjects.Add(SPHelper.createTdParameter("i_notes", dncInput.i_notes, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", dncInput.i_user_id, "IN", TdType.VarChar, 100));
            crudOpertaionOP.parameters = ParamObjects;
            return crudOpertaionOP;
        }

        public static CrudOperationOutput editDncParamters(DoNotContactInput dncInput)
        {
            string requestType = "update";
            CrudOperationOutput crudOpertaionOP = new CrudOperationOutput();
            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
            crudOpertaionOP.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_dnc", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", dncInput.i_mstr_id, "IN", TdType.BigInt, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", dncInput.i_cnst_typ, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_num_seq", dncInput.i_case_num_seq, "IN", TdType.BigInt, 40));
            //ParamObjects.Add(SPHelper.createTdParameter("i_cnst_dnc_exp_ts", dncInput.i_cnst_dnc_exp_ts, "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cnst_dnc_line_of_service_cd", dncInput.i_bk_cnst_dnc_line_of_service_cd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cnst_dnc_comm_chan", dncInput.i_bk_cnst_dnc_comm_chan, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cnst_loc_id", dncInput.i_bk_cnst_loc_id, "IN", TdType.BigInt, 40));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_cnst_dnc_line_of_service_cd", dncInput.i_new_cnst_dnc_line_of_service_cd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cnst_dnc_comm_chan", dncInput.i_new_cnst_dnc_comm_chan, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cnst_dnc_loc_id", dncInput.i_new_cnst_dnc_loc_id, "IN", TdType.BigInt, 40));

            ParamObjects.Add(SPHelper.createTdParameter("i_notes", dncInput.i_notes, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", dncInput.i_user_id, "IN", TdType.VarChar, 100));
            crudOpertaionOP.parameters = ParamObjects;
            return crudOpertaionOP;
        }

        public static CrudOperationOutput deleteDncParamters(DoNotContactInput dncInput)
        {
            string requestType = "delete";
            CrudOperationOutput crudOpertaionOP = new CrudOperationOutput();
            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
            crudOpertaionOP.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_dnc", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", dncInput.i_mstr_id, "IN", TdType.BigInt, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", dncInput.i_cnst_typ, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_num_seq", dncInput.i_case_num_seq, "IN", TdType.BigInt, 40));
           // ParamObjects.Add(SPHelper.createTdParameter("i_cnst_dnc_exp_ts", dncInput.i_cnst_dnc_exp_ts, "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cnst_dnc_line_of_service_cd", dncInput.i_bk_cnst_dnc_line_of_service_cd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cnst_dnc_comm_chan", dncInput.i_bk_cnst_dnc_comm_chan, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cnst_loc_id", dncInput.i_bk_cnst_loc_id, "IN", TdType.BigInt, 40));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_cnst_dnc_line_of_service_cd", dncInput.i_new_cnst_dnc_line_of_service_cd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cnst_dnc_comm_chan", dncInput.i_new_cnst_dnc_comm_chan, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cnst_dnc_loc_id", dncInput.i_new_cnst_dnc_loc_id, "IN", TdType.BigInt, 40));

            ParamObjects.Add(SPHelper.createTdParameter("i_notes", dncInput.i_notes, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", dncInput.i_user_id, "IN", TdType.VarChar, 100));
            crudOpertaionOP.parameters = ParamObjects;
            return crudOpertaionOP;
        }
    }
}
