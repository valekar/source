using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Upload
{
    public class MsgPrefUploadSQLs
    {

        public static string getMsgPrefRefCode()
        {
            return msgPrefRefQuery;
        }

        static readonly string msgPrefRefQuery = @"Sel line_of_service_cd,comm_chan,msg_prefc_typ,msg_prefc_val,comm_typ from dw_stuart_vws.msg_pref_ref_cd_map";


        public static CrudOperationOutput msgPrefUploadCreateTrans(string userId)
        {
            string NameAndEmailUploadHelper;
            NameAndEmailUploadHelper = userId;
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 7;
            List<string> listOutputParameters = new List<string> { "transKey", "transOutput" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_inst_trans", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("typ", "msg_pref_upld", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("subTyp", "msg_pref_upld", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("actionType", DBNull.Value, "IN", TdType.VarChar, 5));
            ParamObjects.Add(SPHelper.createTdParameter("transStat", "In Progress", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("transNotes", "Message Preference Upload", "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("userId", userId, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("caseSeqNum", DBNull.Value, "IN", TdType.BigInt, 20));

            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }


        public static CrudOperationOutput insertMsgPrefUploadRecords(MsgPrefUploadParams msgPrefParams, string username, long trans_key, long max_seq_key)
        {
            CrudOperationOutput crudOutput = new CrudOperationOutput();

            int intNumberOfInputParameters = 33;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.inst_stg_strx_msg_pref", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_seq_key", max_seq_key.CheckDBNull(), "IN", TdType.BigInt, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_mstr_id", string.IsNullOrEmpty(msgPrefParams.init_mstr_id) ?
                                DBNull.Value : msgPrefParams.init_mstr_id.CheckDBNull(), "IN", TdType.BigInt, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_srcsys_id", msgPrefParams.cnst_srcsys_id, "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_arc_srcsys_cd", msgPrefParams.arc_srcsys_cd, "IN", TdType.VarChar, 15));
                
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", msgPrefParams.line_of_service_cd, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_comm_chan", msgPrefParams.comm_chan, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_msg_prefc_typ", msgPrefParams.msg_prefc_typ, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_msg_prefc_val", msgPrefParams.msg_prefc_val, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_msg_prefc_submissn_typ", msgPrefParams.msg_prefc_submissn_typ, "IN", TdType.VarChar, 40));

            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_line1_addr", msgPrefParams.cnst_addr_line1, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_line2_addr", msgPrefParams.cnst_addr_line2, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_city_nm", msgPrefParams.cnst_addr_city, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_state_cd", msgPrefParams.cnst_addr_state, "IN", TdType.Char, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_zip_5", msgPrefParams.cnst_addr_zip5.CheckDBNull(), "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_zip_4", msgPrefParams.cnst_addr_zip4.CheckDBNull(), "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_phn_num", msgPrefParams.cnst_phn_num, "IN", TdType.VarChar, 15));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_extn_phn_num", msgPrefParams.cnst_extn_phn_num, "IN", TdType.VarChar, 15));

            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_prefix_nm", msgPrefParams.prefix_nm, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_first_nm", msgPrefParams.prsn_frst_nm, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_middle_nm", msgPrefParams.prsn_mid_nm, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_last_nm", msgPrefParams.prsn_lst_nm, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_suffix_nm", msgPrefParams.suffix_nm, "IN", TdType.VarChar, 50));

            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_org_nm", msgPrefParams.cnst_org_nm, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_email_addr", msgPrefParams.cnst_email_addr, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_key", trans_key, "IN", TdType.VarChar, 1000)); 
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", msgPrefParams.notes.CheckDBNull(), "IN", TdType.VarChar, 1000));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", username, "IN", TdType.VarChar, 1000));
            ParamObjects.Add(SPHelper.createTdParameter("i_row_stat_cd", "I", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_exp_dt", string.IsNullOrEmpty(msgPrefParams.msg_pref_exp_ts) ? null : msgPrefParams.msg_pref_exp_ts, "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_load_id", 10, "IN", TdType.Integer, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_upld_typ_key", 5, "IN", TdType.Integer, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_upld_typ_dsc", "Message Preference Upload", "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_status", "In Progress", "IN", TdType.VarChar, 100));
            crudOutput.parameters = ParamObjects;

            return crudOutput;
        }


        public static string getMaxSeqKeySQL()
        {
            return maxSeqQry;
        }

        static readonly string maxSeqQry = @"sel coalesce(max(seq_key),0) from dw_stuart_vws.strx_msg_pref_max_key";


    }
}
