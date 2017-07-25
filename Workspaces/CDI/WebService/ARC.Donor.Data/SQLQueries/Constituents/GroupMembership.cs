using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class GroupMembership
    {
        public static string getGroupMembershipSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT *
        FROM DW_STUART_VWS.strx_cnst_dtl_chptr_grp
        WHERE cnst_mstr_id = {2} 
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


        public static string getAllGroupMembershipSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(showQry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string showQry = @"sel * from arc_cmm_vws.grp_mbrshp where cnst_mstr_id = {2}";

        public static CrudOperationOutput addGroupMembershipParams(ARC.Donor.Data.Entities.Constituents.GroupMembershipInput groupInput)
        {
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            string requestType = "insert";
            int intNumberOfInputParameters = 16;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_chptr_grp", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", groupInput.i_mstr_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_mbrshp_eff_strt_dt", groupInput.i_grp_mbrshp_eff_strt_dt, "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_mbrshp_eff_strt_dt", groupInput.i_grp_mbrshp_eff_end_dt, "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", groupInput.i_user_id, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", groupInput.i_cnst_typ, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", groupInput.i_notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", groupInput.i_case_seq_num, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_group_key", groupInput.i_bk_group_key, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_assgnmnt_mthd", groupInput.i_bk_assgnmnt_mthd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_line_of_service_cd", groupInput.i_bk_line_of_service_cd, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", groupInput.i_bk_arc_srcsys_cd, "IN", TdType.VarChar, 100));            
            ParamObjects.Add(SPHelper.createTdParameter("i_new_group_key", groupInput.i_new_group_key, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_assgnmnt_mthd", groupInput.i_new_assgnmnt_mthd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_line_of_service_cd", groupInput.i_new_line_of_service_cd, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", groupInput.i_new_arc_srcsys_cd, "IN", TdType.VarChar, 100));  
            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }


        public static CrudOperationOutput editGroupMembershipParams(ARC.Donor.Data.Entities.Constituents.GroupMembershipInput groupInput)
        {
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            string requestType = "update";
            int intNumberOfInputParameters = 16;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_chptr_grp", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", groupInput.i_mstr_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_mbrshp_eff_strt_dt", groupInput.i_grp_mbrshp_eff_strt_dt, "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_mbrshp_eff_strt_dt", groupInput.i_grp_mbrshp_eff_end_dt, "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", groupInput.i_user_id, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", groupInput.i_cnst_typ, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", groupInput.i_notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", groupInput.i_case_seq_num, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_group_key", groupInput.i_bk_group_key, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_assgnmnt_mthd", groupInput.i_bk_assgnmnt_mthd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_line_of_service_cd", groupInput.i_bk_line_of_service_cd, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", groupInput.i_bk_arc_srcsys_cd, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_group_key", groupInput.i_new_group_key, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_assgnmnt_mthd", groupInput.i_new_assgnmnt_mthd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_line_of_service_cd", groupInput.i_new_line_of_service_cd, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", groupInput.i_new_arc_srcsys_cd, "IN", TdType.VarChar, 100));  
            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }

        public static CrudOperationOutput deleteGroupMembershipParams(ARC.Donor.Data.Entities.Constituents.GroupMembershipInput groupInput)
        {
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            string requestType = "delete";
            int intNumberOfInputParameters = 16;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_chptr_grp", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", groupInput.i_mstr_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_mbrshp_eff_strt_dt", groupInput.i_grp_mbrshp_eff_strt_dt, "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_mbrshp_eff_strt_dt", groupInput.i_grp_mbrshp_eff_end_dt, "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", groupInput.i_user_id, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", groupInput.i_cnst_typ, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", groupInput.i_notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", groupInput.i_case_seq_num, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_group_key", groupInput.i_bk_group_key, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_assgnmnt_mthd", groupInput.i_bk_assgnmnt_mthd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_line_of_service_cd", groupInput.i_bk_line_of_service_cd, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", groupInput.i_bk_arc_srcsys_cd, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_group_key", groupInput.i_new_group_key, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_assgnmnt_mthd", groupInput.i_new_assgnmnt_mthd, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_line_of_service_cd", groupInput.i_new_line_of_service_cd, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", groupInput.i_new_arc_srcsys_cd, "IN", TdType.VarChar, 100));  
            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }
     
    }
}