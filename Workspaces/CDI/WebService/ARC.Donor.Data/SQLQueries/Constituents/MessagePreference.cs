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
    public class MessagePreference
    {

        public static string getMessagePreferenceOptionsSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(OptionsQry, NoOfRecords,
                    PageNumber, string.Join(",", Master_id),
                    (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                    (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string OptionsQry = @"select * from  dw_stuart_vws.msg_pref_ref_cd_map";

         public static string getMessagePreferenceSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

         static readonly string Qry = @"sel * from dw_stuart_vws.bz_strx_cnst_msg_pref  where msg_pref_mstr_id  = {2}  
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

         public static string getAllMessagePreferenceSQL(int NoOfRecords, int PageNumber, string Master_id)
         {
             return string.Format(showQry, NoOfRecords,
                      PageNumber, string.Join(",", Master_id),
                      (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                      (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
         }

         static readonly string showQry = @"sel * from arc_cmm_vws.bz_cnst_msg_pref where msg_pref_mstr_id  = {2}";


         public static CrudOperationOutput addMsgPrefParamters(ARC.Donor.Data.Entities.Constituents.MessagePreferenceInput prefInput)
         {
             string requestType = "insert";
             CrudOperationOutput crudOutput = new CrudOperationOutput();
             int intNumberOfInputParameters = 19;
             List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
             crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_msg_pref", intNumberOfInputParameters, listOutputParameters);
             var ParamObjects = new List<object>();
             ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 20));
             ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", prefInput.i_mstr_id, "IN", TdType.BigInt, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", prefInput.i_cnst_typ, "IN", TdType.VarChar, 20));
             ParamObjects.Add(SPHelper.createTdParameter("i_case_num_seq", prefInput.i_case_num_seq, "IN", TdType.BigInt, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_notes", prefInput.i_notes, "IN", TdType.VarChar, 5000));
            // ParamObjects.Add(SPHelper.createTdParameter("i_created_by", prefInput.i_created_by, "IN", TdType.VarChar, 100));
             ParamObjects.Add(SPHelper.createTdParameter("i_user_id", prefInput.i_user_id, "IN", TdType.VarChar, 100));
             ParamObjects.Add(SPHelper.createTdParameter("i_msg_pref_exp_dt", prefInput.i_msg_pref_exp_dt, "IN", TdType.Date, 200));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", prefInput.i_bk_arc_srcsys_cd, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_typ", prefInput.i_bk_msg_pref_typ, "IN", TdType.VarChar, 100));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_val", prefInput.i_bk_msg_pref_val, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_comm_chan", prefInput.i_bk_msg_pref_comm_chan, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_comm_chan_typ", prefInput.i_bk_msg_pref_comm_chan_typ, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_line_of_service", prefInput.i_bk_line_of_service, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", prefInput.i_new_arc_srcsys_cd, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_typ", prefInput.i_new_msg_pref_typ, "IN", TdType.VarChar, 100));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_val", prefInput.i_new_msg_pref_val, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_comm_chan", prefInput.i_new_msg_pref_comm_chan, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_comm_chan_typ", prefInput.i_new_msg_pref_comm_chan_typ, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_line_of_service", prefInput.i_new_line_of_service, "IN", TdType.VarChar, 40));

             crudOutput.parameters = ParamObjects;
             return crudOutput;
         }



         public static CrudOperationOutput editMsgPrefParamters(ARC.Donor.Data.Entities.Constituents.MessagePreferenceInput prefInput)
         {
             string requestType = "update";
             CrudOperationOutput crudOutput = new CrudOperationOutput();
             int intNumberOfInputParameters = 19;
             List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
             crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_msg_pref", intNumberOfInputParameters, listOutputParameters);
             var ParamObjects = new List<object>();
             ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 20));
             ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", prefInput.i_mstr_id, "IN", TdType.BigInt, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", prefInput.i_cnst_typ, "IN", TdType.VarChar, 20));
             ParamObjects.Add(SPHelper.createTdParameter("i_case_num_seq", prefInput.i_case_num_seq, "IN", TdType.BigInt, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_notes", prefInput.i_notes, "IN", TdType.VarChar, 5000));
             ParamObjects.Add(SPHelper.createTdParameter("i_user_id", prefInput.i_user_id, "IN", TdType.VarChar, 100));
             ParamObjects.Add(SPHelper.createTdParameter("i_msg_pref_exp_dt", prefInput.i_msg_pref_exp_dt, "IN", TdType.Date, 200));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", prefInput.i_bk_arc_srcsys_cd, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_typ", prefInput.i_bk_msg_pref_typ, "IN", TdType.VarChar, 100));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_val", prefInput.i_bk_msg_pref_val, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_comm_chan", prefInput.i_bk_msg_pref_comm_chan, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_comm_chan_typ", prefInput.i_bk_msg_pref_comm_chan_typ, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_line_of_service", prefInput.i_bk_line_of_service, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", prefInput.i_new_arc_srcsys_cd, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_typ", prefInput.i_new_msg_pref_typ, "IN", TdType.VarChar, 100));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_val", prefInput.i_new_msg_pref_val, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_comm_chan", prefInput.i_new_msg_pref_comm_chan, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_comm_chan_typ", prefInput.i_new_msg_pref_comm_chan_typ, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_line_of_service", prefInput.i_new_line_of_service, "IN", TdType.VarChar, 40));

             crudOutput.parameters = ParamObjects;
             return crudOutput;
         }

         public static CrudOperationOutput deleteMsgPrefParamters(ARC.Donor.Data.Entities.Constituents.MessagePreferenceInput prefInput)
         {
             string requestType = "delete";
             CrudOperationOutput crudOutput = new CrudOperationOutput();
             int intNumberOfInputParameters = 19;
             DateTime dateTime = DateTime.UtcNow.Date;
             List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
             crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_msg_pref", intNumberOfInputParameters, listOutputParameters);
             var ParamObjects = new List<object>();
             ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 20));
             ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", prefInput.i_mstr_id, "IN", TdType.BigInt, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", prefInput.i_cnst_typ, "IN", TdType.VarChar, 20));
             ParamObjects.Add(SPHelper.createTdParameter("i_case_num_seq", prefInput.i_case_num_seq, "IN", TdType.BigInt, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_notes", prefInput.i_notes, "IN", TdType.VarChar, 5000));
             ParamObjects.Add(SPHelper.createTdParameter("i_user_id", prefInput.i_user_id, "IN", TdType.VarChar, 100));
             ParamObjects.Add(SPHelper.createTdParameter("i_msg_pref_exp_dt", dateTime, "IN", TdType.Date, 200));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", prefInput.i_bk_arc_srcsys_cd, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_typ", prefInput.i_bk_msg_pref_typ, "IN", TdType.VarChar, 100));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_val", prefInput.i_bk_msg_pref_val, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_comm_chan", prefInput.i_bk_msg_pref_comm_chan, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_msg_pref_comm_chan_typ", prefInput.i_bk_msg_pref_comm_chan_typ, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_bk_line_of_service", prefInput.i_bk_line_of_service, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", prefInput.i_new_arc_srcsys_cd, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_typ", prefInput.i_new_msg_pref_typ, "IN", TdType.VarChar, 100));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_val", prefInput.i_new_msg_pref_val, "IN", TdType.VarChar, 40));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_comm_chan", prefInput.i_new_msg_pref_comm_chan, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_msg_pref_comm_chan_typ", prefInput.i_new_msg_pref_comm_chan_typ, "IN", TdType.VarChar, 50));
             ParamObjects.Add(SPHelper.createTdParameter("i_new_line_of_service", prefInput.i_new_line_of_service, "IN", TdType.VarChar, 40));

             crudOutput.parameters = ParamObjects;
             return crudOutput;
         }
    }




   
    
}
