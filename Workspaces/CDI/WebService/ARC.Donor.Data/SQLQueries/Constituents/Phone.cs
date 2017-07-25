using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class Phone
    {
        public static string getPhoneSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT cnst_mstr_id,locator_phn_key ,cnst_srcsys_id, cnst_phn_num, arc_srcsys_cd,
		cnst_phn_extsn_num, phn_typ_cd, cntct_stat_typ_cd, cnst_phn_best_ind,
		cnst_phn_strt_ts, cnst_phn_end_dt, best_phn_ind, cnst_phn_best_los_ind,
		trans_key, act_ind, user_id, dw_srcsys_trans_ts, row_stat_cd,
		appl_src_cd, load_id, is_previous, transaction_key, trans_status,
		inactive_ind, strx_row_stat_cd, unique_trans_key, assessmnt_ctg
        FROM DW_STUART_VWS.strx_cnst_dtl_cnst_phn
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

        public static ConstituentPhoneInput getAddPhoneParameters(ARC.Donor.Data.Entities.Constituents.ConstituentPhoneInput ConstPhoneInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentPhoneInput ConstHelper = new ConstituentPhoneInput();
            ConstHelper.RequestType = "insert";
            if (!string.IsNullOrEmpty(ConstPhoneInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstPhoneInput.MasterID;

            if (!string.IsNullOrEmpty(ConstPhoneInput.UserName))
                ConstHelper.UserName = ConstPhoneInput.UserName;

            if (!string.IsNullOrEmpty(ConstPhoneInput.ConstType))
                ConstHelper.ConstType = ConstPhoneInput.ConstType;

            if (!string.IsNullOrEmpty(ConstPhoneInput.Notes))
                ConstHelper.Notes = ConstPhoneInput.Notes;

            if (!string.IsNullOrEmpty(ConstPhoneInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstPhoneInput.CaseNumber;

            ConstHelper.OldSourceSystemCode = string.Empty;
            ConstHelper.OldBestLOSInd = "0";
            ConstHelper.OldPhoneTypeCode = string.Empty;

            if (!string.IsNullOrEmpty(ConstPhoneInput.PhoneNumber))
                ConstHelper.PhoneNumber = ConstPhoneInput.PhoneNumber;

            if (!string.IsNullOrEmpty(ConstPhoneInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstPhoneInput.SourceSystemCode;

            if (!string.IsNullOrEmpty(ConstPhoneInput.PhoneTypeCode))
                ConstHelper.PhoneTypeCode = ConstPhoneInput.PhoneTypeCode;

            ConstHelper.BestLOS = ConstPhoneInput.BestLOS;

            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_phn", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_phn_typ_cd", ConstHelper.OldPhoneTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_phn_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_phn_num", ConstHelper.PhoneNumber, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_phn_typ_cd", ConstHelper.PhoneTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_phn_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }

        public static ConstituentPhoneInput getDeletePhoneParameters(ARC.Donor.Data.Entities.Constituents.ConstituentPhoneInput ConstPhoneInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentPhoneInput ConstHelper = new ConstituentPhoneInput();
            ConstHelper.RequestType = "delete";
            if (!string.IsNullOrEmpty(ConstPhoneInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstPhoneInput.MasterID;

            if (!string.IsNullOrEmpty(ConstPhoneInput.UserName))
                ConstHelper.UserName = ConstPhoneInput.UserName;

            if (!string.IsNullOrEmpty(ConstPhoneInput.ConstType))
                ConstHelper.ConstType = ConstPhoneInput.ConstType;

            if (!string.IsNullOrEmpty(ConstPhoneInput.Notes))
                ConstHelper.Notes = ConstPhoneInput.Notes;

            if (!string.IsNullOrEmpty(ConstPhoneInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstPhoneInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstPhoneInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstPhoneInput.OldSourceSystemCode;

            ConstHelper.OldBestLOSInd = ConstPhoneInput.OldBestLOSInd;

            if (!string.IsNullOrEmpty(ConstPhoneInput.OldPhoneTypeCode))
                ConstHelper.OldPhoneTypeCode = ConstPhoneInput.OldPhoneTypeCode;

            //assign empty strings for the remaining columns which are not needed
            ConstHelper.PhoneNumber = string.Empty;
            ConstHelper.SourceSystemCode = string.Empty;
            ConstHelper.PhoneTypeCode = string.Empty;

            ConstHelper.BestLOS = 0;

            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_phn", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_phn_typ_cd", ConstHelper.OldPhoneTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_phn_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_phn_num", ConstHelper.PhoneNumber, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_phn_typ_cd", ConstHelper.PhoneTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_phn_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }

        public static ConstituentPhoneInput getUpdatePhoneParameters(ARC.Donor.Data.Entities.Constituents.ConstituentPhoneInput ConstPhoneInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentPhoneInput ConstHelper = new ConstituentPhoneInput();
            ConstHelper.RequestType = "update";
            if (!string.IsNullOrEmpty(ConstPhoneInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstPhoneInput.MasterID;

            if (!string.IsNullOrEmpty(ConstPhoneInput.UserName))
                ConstHelper.UserName = ConstPhoneInput.UserName;

            if (!string.IsNullOrEmpty(ConstPhoneInput.ConstType))
                ConstHelper.ConstType = ConstPhoneInput.ConstType;

            if (!string.IsNullOrEmpty(ConstPhoneInput.Notes))
                ConstHelper.Notes = ConstPhoneInput.Notes;

            if (!string.IsNullOrEmpty(ConstPhoneInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstPhoneInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstPhoneInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstPhoneInput.OldSourceSystemCode;

            ConstHelper.OldBestLOSInd = ConstPhoneInput.OldBestLOSInd;

            if (!string.IsNullOrEmpty(ConstPhoneInput.OldPhoneTypeCode))
                ConstHelper.OldPhoneTypeCode = ConstPhoneInput.OldPhoneTypeCode;

            if (!string.IsNullOrEmpty(ConstPhoneInput.PhoneNumber))
                ConstHelper.PhoneNumber = ConstPhoneInput.PhoneNumber;

            if (!string.IsNullOrEmpty(ConstPhoneInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstPhoneInput.SourceSystemCode;

            if (!string.IsNullOrEmpty(ConstPhoneInput.PhoneTypeCode))
                ConstHelper.PhoneTypeCode = ConstPhoneInput.PhoneTypeCode;

            ConstHelper.BestLOS = ConstPhoneInput.BestLOS;

            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_phn", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_phn_typ_cd", ConstHelper.OldPhoneTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_phn_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_phn_num", ConstHelper.PhoneNumber, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_phn_typ_cd", ConstHelper.PhoneTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_phn_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }
    }
}