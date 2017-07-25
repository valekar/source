using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class Email
    {
        public static string getEmailSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT cnst_mstr_id, cnst_srcsys_id, cnst_email_addr, arc_srcsys_cd,
		email_typ_cd, cntct_stat_typ_cd, cnst_best_email_ind, cnst_email_strt_ts,
		cnst_email_end_dt, best_email_ind, email_key, domain_corrctd_ind,
		cnst_email_validtn_dt, cnst_email_validtn_method, cnst_email_validtn_ind,
		cnst_email_best_los_ind, trans_key, act_ind, user_id, dw_srcsys_trans_ts,
		row_stat_cd, appl_src_cd, load_id, is_previous, transaction_key,
		trans_status, inactive_ind, strx_row_stat_cd, unique_trans_key, assessmnt_ctg
        FROM DW_STUART_VWS.strx_cnst_dtl_cnst_email
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

        public static ConstituentEmailInput getAddEmailParameters(ARC.Donor.Data.Entities.Constituents.ConstituentEmailInput ConstEmailInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentEmailInput ConstHelper = new ConstituentEmailInput();
            ConstHelper.RequestType = "insert";
            if (!string.IsNullOrEmpty(ConstEmailInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstEmailInput.MasterID;

            if (!string.IsNullOrEmpty(ConstEmailInput.UserName))
                ConstHelper.UserName = ConstEmailInput.UserName;

            if (!string.IsNullOrEmpty(ConstEmailInput.ConstType))
                ConstHelper.ConstType = ConstEmailInput.ConstType;

            if (!string.IsNullOrEmpty(ConstEmailInput.Notes))
                ConstHelper.Notes = ConstEmailInput.Notes;

            if (!string.IsNullOrEmpty(ConstEmailInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstEmailInput.CaseNumber;

            ConstHelper.OldSourceSystemCode = string.Empty;
            ConstHelper.OldBestLOSInd = "0";
            ConstHelper.OldEmailTypeCode = string.Empty;

            if (!string.IsNullOrEmpty(ConstEmailInput.NewEmailAddress))
                ConstHelper.NewEmailAddress = ConstEmailInput.NewEmailAddress;

            if (!string.IsNullOrEmpty(ConstEmailInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstEmailInput.SourceSystemCode;

            if (!string.IsNullOrEmpty(ConstEmailInput.EmailTypeCode))
                ConstHelper.EmailTypeCode = ConstEmailInput.EmailTypeCode;

            ConstHelper.BestLOS = ConstEmailInput.BestLOS;

            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_email", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_email_typ_cd", ConstHelper.OldEmailTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_email_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_email_addr", ConstHelper.NewEmailAddress, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_email_typ_cd", ConstHelper.EmailTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_email_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }

        public static ConstituentEmailInput getDeleteEmailParameters(ARC.Donor.Data.Entities.Constituents.ConstituentEmailInput ConstEmailInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentEmailInput ConstHelper = new ConstituentEmailInput();
            ConstHelper.RequestType = "delete";
            if (!string.IsNullOrEmpty(ConstEmailInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstEmailInput.MasterID;

            if (!string.IsNullOrEmpty(ConstEmailInput.UserName))
                ConstHelper.UserName = ConstEmailInput.UserName;

            if (!string.IsNullOrEmpty(ConstEmailInput.ConstType))
                ConstHelper.ConstType = ConstEmailInput.ConstType;

            if (!string.IsNullOrEmpty(ConstEmailInput.Notes))
                ConstHelper.Notes = ConstEmailInput.Notes;

            if (!string.IsNullOrEmpty(ConstEmailInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstEmailInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstEmailInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstEmailInput.OldSourceSystemCode;

            //  ConstHelper.OldBestLOSInd = ConstEmailInput.OldBestLOSInd;
            //added by Srini
            if (string.IsNullOrEmpty(ConstEmailInput.OldBestLOSInd))
            {
                ConstHelper.OldBestLOSInd = "0";
            }

            if (!string.IsNullOrEmpty(ConstEmailInput.OldEmailTypeCode))
                ConstHelper.OldEmailTypeCode = ConstEmailInput.OldEmailTypeCode;

            //assign empty strings for the remaining columns which are not needed
            ConstHelper.NewEmailAddress = string.Empty;
            ConstHelper.SourceSystemCode = string.Empty;
            ConstHelper.EmailTypeCode = string.Empty;

            ConstHelper.BestLOS = 0;

            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_email", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_email_typ_cd", ConstHelper.OldEmailTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_email_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_email_num", ConstHelper.NewEmailAddress, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_email_typ_cd", ConstHelper.EmailTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_email_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;

        }

        public static ConstituentEmailInput getUpdateEmailParameters(ARC.Donor.Data.Entities.Constituents.ConstituentEmailInput ConstEmailInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentEmailInput ConstHelper = new ConstituentEmailInput();
            ConstHelper.RequestType = "update";
            if (!string.IsNullOrEmpty(ConstEmailInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstEmailInput.MasterID;

            if (!string.IsNullOrEmpty(ConstEmailInput.UserName))
                ConstHelper.UserName = ConstEmailInput.UserName;

            if (!string.IsNullOrEmpty(ConstEmailInput.ConstType))
                ConstHelper.ConstType = ConstEmailInput.ConstType;

            if (!string.IsNullOrEmpty(ConstEmailInput.Notes))
                ConstHelper.Notes = ConstEmailInput.Notes;

            if (!string.IsNullOrEmpty(ConstEmailInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstEmailInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstEmailInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstEmailInput.OldSourceSystemCode;

            //Added by Srini
            if (string.IsNullOrEmpty(ConstEmailInput.OldBestLOSInd))
            {
                ConstHelper.OldBestLOSInd = "0";
            }


            if (!string.IsNullOrEmpty(ConstEmailInput.OldEmailTypeCode))
                ConstHelper.OldEmailTypeCode = ConstEmailInput.OldEmailTypeCode;

            if (!string.IsNullOrEmpty(ConstEmailInput.NewEmailAddress))
                ConstHelper.NewEmailAddress = ConstEmailInput.NewEmailAddress;

            if (!string.IsNullOrEmpty(ConstEmailInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstEmailInput.SourceSystemCode;

            if (!string.IsNullOrEmpty(ConstEmailInput.EmailTypeCode))
                ConstHelper.EmailTypeCode = ConstEmailInput.EmailTypeCode;

            ConstHelper.BestLOS = ConstEmailInput.BestLOS;

            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_email", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_email_typ_cd", ConstHelper.OldEmailTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_email_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_email_num", ConstHelper.NewEmailAddress, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_email_typ_cd", ConstHelper.EmailTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_email_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }
    }
}