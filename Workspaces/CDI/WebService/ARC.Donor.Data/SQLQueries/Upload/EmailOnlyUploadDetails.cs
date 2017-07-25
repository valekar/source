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
    public class EmailOnlyUploadSQLDetails
    {
        public static CrudOperationOutput postEmailUploadCreateTrans(string userId)
        {

            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 7;
            List<string> listOutputParameters = new List<string> { "transKey", "transOutput" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_inst_trans", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("typ", "chptr_upl_email", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("subTyp", "chptr_upl_email", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("actionType", DBNull.Value, "IN", TdType.VarChar, 5));  
            ParamObjects.Add(SPHelper.createTdParameter("transStat", "In Progress", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("transNotes", "Email-only Upload", "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("userId", userId, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("caseSeqNum", DBNull.Value, "IN", TdType.BigInt, 20));

            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }

        public static string getComUnitKeySQL(EmailOnlyUploadDetails gm)
        {
            string nk_ecode_string = "";

            foreach (var item in gm.ListEmailOnlyUploadDetails.EmailOnlyUploadInputList)
            {
                nk_ecode_string += "'" + item.nk_ecode.ToString() + "',";
            }

            nk_ecode_string = nk_ecode_string.Remove(nk_ecode_string.Length - 1);
            return "SELECT distinct nk_ecode, unit_key FROM dw_common_vws.dim_unit DimUnit WHERE nk_ecode IN (" + nk_ecode_string + ") AND nk_branch_cd = '000' AND unit_typ_cd = 'C' AND appl_src_cd='FOCS'";
        }

        public static CrudOperationOutput insertEmailOnlyUploadRecords(EmailOnlyUploadInput gm, List<ChapterUploadFileDetailsHelper> ch, 
            long max_seq_key, long max_grp_seq_key, List<ComUnitKeyOutput> com_unit_key_lst, long strTransactionKey)
        {
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            EmailOnlyUploadInput gmHelper = new EmailOnlyUploadInput();
            gmHelper = gm;
            int com_unit_key = -1;
            foreach (var item in com_unit_key_lst)
            {
                if (item.nk_ecode == gm.nk_ecode)
                    com_unit_key = item.unit_key;
            }

            int intNumberOfInputParameters = 42;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_inst_cnst_grp_mbrshp", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_seq_key", max_seq_key.CheckDBNull(), "IN", TdType.BigInt, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_chpt_cd", string.IsNullOrEmpty(gm.appl_src_cd) ? DBNull.Value : ("S" + gm.appl_src_cd.Substring(1)).CheckDBNull(), "IN", TdType.VarChar, 4));
            ParamObjects.Add(SPHelper.createTdParameter("i_nk_ecode", gm.nk_ecode.CheckDBNull(), "IN", TdType.Char, 5));
            if (com_unit_key != -1)
                ParamObjects.Add(SPHelper.createTdParameter("i_unit_key", com_unit_key.CheckDBNull(), "IN", TdType.Integer, 10));
            else
                ParamObjects.Add(SPHelper.createTdParameter("i_unit_key", DBNull.Value, "IN", TdType.Integer, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_mstr_id", DBNull.Value, "IN", TdType.BigInt, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_prefix_nm", DBNull.Value, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_first_nm", DBNull.Value, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_middle_nm",  DBNull.Value , "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_last_nm", DBNull.Value, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr1_street1", DBNull.Value, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr1_street2", DBNull.Value, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr1_city", DBNull.Value, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr1_state", DBNull.Value, "IN", TdType.Char, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr1_zip", DBNull.Value, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr2_street1", DBNull.Value, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr2_street2", DBNull.Value, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr2_city", DBNull.Value, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr2_state", DBNull.Value, "IN", TdType.Char, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr2_zip", DBNull.Value, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_phn1_num", DBNull.Value, "IN", TdType.VarChar, 15));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_phn2_num", DBNull.Value, "IN", TdType.VarChar, 15));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_phn3_num", DBNull.Value, "IN", TdType.VarChar, 15));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_email1_addr", gm.cnst_email_addr, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_email2_addr", DBNull.Value, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_job_title", DBNull.Value, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_company_nm", DBNull.Value, "IN", TdType.VarChar, 150));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_cd", gm.grp_cd.CheckDBNull(), "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_nm", gm.grp_nm.CheckDBNull(), "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", "FR", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_rm_ind", string.IsNullOrEmpty(gm.rm_ind) ? 0 : gm.rm_ind.CheckDBNull(), "IN", TdType.ByteInt, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", gm.notes.CheckDBNull(), "IN", TdType.VarChar, 1000));
            ParamObjects.Add(SPHelper.createTdParameter("i_stuart_cnst_grp_key", max_grp_seq_key.CheckDBNull(), "IN", TdType.BigInt, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_mbrshp_eff_strt_dt", gm.grp_mbrshp_strt_dt.CheckDBNull(), "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_mbrshp_eff_end_dt", gm.grp_mbrshp_end_dt.CheckDBNull(), "IN", TdType.Date, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_key", strTransactionKey.CheckDBNull(), "IN", TdType.BigInt, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", ch[0].UserName.CheckDBNull(), "IN", TdType.VarChar, 1000));
            ParamObjects.Add(SPHelper.createTdParameter("i_row_stat_cd", "I", "IN", TdType.Char, 4));
            ParamObjects.Add(SPHelper.createTdParameter("i_appl_src_cd", "STRX", "IN", TdType.VarChar, 4));
            ParamObjects.Add(SPHelper.createTdParameter("i_load_id", 10, "IN", TdType.Integer, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_upld_typ_key", 2, "IN", TdType.Integer, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_upld_typ_dsc", "Email-only Upload", "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_status", gm.status.CheckDBNull(), "IN", TdType.VarChar, 20));

            crudOutput.parameters = ParamObjects;
           // return gmHelper;
            return crudOutput;
        }

    }
}
