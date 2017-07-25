using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class Birth
    {
        public static string getBirthSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT *
        FROM DW_STUART_VWS.strx_cnst_dtl_cnst_birth
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
        ORDER  by transaction_key;";


        public static ConstituentBirthInput getAddBirthParameters(ARC.Donor.Data.Entities.Constituents.ConstituentBirthInput ConstBirthInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentBirthInput ConstHelper = new ConstituentBirthInput();
            ConstHelper.RequestType = "insert";
            if (!string.IsNullOrEmpty(ConstBirthInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstBirthInput.MasterID;

            if (!string.IsNullOrEmpty(ConstBirthInput.UserName))
                ConstHelper.UserName = ConstBirthInput.UserName;

            if (!string.IsNullOrEmpty(ConstBirthInput.ConstType))
                ConstHelper.ConstType = ConstBirthInput.ConstType;

            if (!string.IsNullOrEmpty(ConstBirthInput.Notes))
                ConstHelper.Notes = ConstBirthInput.Notes;

            if (!string.IsNullOrEmpty(ConstBirthInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstBirthInput.CaseNumber;

            ConstHelper.OldSourceSystemCode = string.Empty;
            ConstHelper.OldBestLOSInd = "0";

            if (!string.IsNullOrEmpty(ConstBirthInput.NewBirthDayNumber.ToString()))
                ConstHelper.NewBirthDayNumber = ConstBirthInput.NewBirthDayNumber;

            if (!string.IsNullOrEmpty(ConstBirthInput.NewBirthMonthNumber.ToString()))
                ConstHelper.NewBirthMonthNumber = ConstBirthInput.NewBirthMonthNumber;

            if (!string.IsNullOrEmpty(ConstBirthInput.NewBirthDayNumber.ToString()))
                ConstHelper.NewBirthYearNumber = ConstBirthInput.NewBirthYearNumber;

            if (!string.IsNullOrEmpty(ConstBirthInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstBirthInput.SourceSystemCode;

            ConstHelper.BestLOS = ConstBirthInput.BestLOS;

            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_birth", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_birth_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_dy_num", ConstHelper.NewBirthDayNumber, "IN", TdType.Decimal, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_mth_num", ConstHelper.NewBirthMonthNumber, "IN", TdType.Decimal, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_yr_num", ConstHelper.NewBirthYearNumber, "IN", TdType.Decimal, 4));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));
            parameters = ParamObjects;

            return ConstHelper;
        }

        public static ConstituentBirthInput getDeleteBirthParameters(ARC.Donor.Data.Entities.Constituents.ConstituentBirthInput ConstBirthInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentBirthInput ConstHelper = new ConstituentBirthInput();
            ConstHelper.RequestType = "delete";
            if (!string.IsNullOrEmpty(ConstBirthInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstBirthInput.MasterID;

            if (!string.IsNullOrEmpty(ConstBirthInput.UserName))
                ConstHelper.UserName = ConstBirthInput.UserName;

            if (!string.IsNullOrEmpty(ConstBirthInput.ConstType))
                ConstHelper.ConstType = ConstBirthInput.ConstType;

            if (!string.IsNullOrEmpty(ConstBirthInput.Notes))
                ConstHelper.Notes = ConstBirthInput.Notes;

            if (!string.IsNullOrEmpty(ConstBirthInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstBirthInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstBirthInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstBirthInput.OldSourceSystemCode;

            ConstHelper.OldBestLOSInd = ConstBirthInput.OldBestLOSInd;

            //assign empty strings for the remaining columns which are not needed
            ConstHelper.NewBirthDayNumber = 0;
            ConstHelper.NewBirthMonthNumber = 0;
            ConstHelper.NewBirthYearNumber = 0;
            ConstHelper.SourceSystemCode = string.Empty;

            ConstHelper.BestLOS = ConstBirthInput.BestLOS;

            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_birth", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_birth_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_dy_num", ConstHelper.NewBirthDayNumber, "IN", TdType.Decimal, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_mth_num", ConstHelper.NewBirthMonthNumber, "IN", TdType.Decimal, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_yr_num", ConstHelper.NewBirthYearNumber, "IN", TdType.Decimal, 4));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }

        public static ConstituentBirthInput getUpdateBirthParameters(ARC.Donor.Data.Entities.Constituents.ConstituentBirthInput ConstBirthInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentBirthInput ConstHelper = new ConstituentBirthInput();
            ConstHelper.RequestType = "update";
            if (!string.IsNullOrEmpty(ConstBirthInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstBirthInput.MasterID;

            if (!string.IsNullOrEmpty(ConstBirthInput.UserName))
                ConstHelper.UserName = ConstBirthInput.UserName;

            if (!string.IsNullOrEmpty(ConstBirthInput.ConstType))
                ConstHelper.ConstType = ConstBirthInput.ConstType;

            if (!string.IsNullOrEmpty(ConstBirthInput.Notes))
                ConstHelper.Notes = ConstBirthInput.Notes;

            if (!string.IsNullOrEmpty(ConstBirthInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstBirthInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstBirthInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstBirthInput.OldSourceSystemCode;

            ConstHelper.OldBestLOSInd = ConstBirthInput.OldBestLOSInd;

            if (!string.IsNullOrEmpty(ConstBirthInput.NewBirthDayNumber.ToString()))
                ConstHelper.NewBirthDayNumber = ConstBirthInput.NewBirthDayNumber;

            if (!string.IsNullOrEmpty(ConstBirthInput.NewBirthMonthNumber.ToString()))
                ConstHelper.NewBirthMonthNumber = ConstBirthInput.NewBirthMonthNumber;

            if (!string.IsNullOrEmpty(ConstBirthInput.NewBirthDayNumber.ToString()))
                ConstHelper.NewBirthYearNumber = ConstBirthInput.NewBirthYearNumber;

            if (!string.IsNullOrEmpty(ConstBirthInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstBirthInput.SourceSystemCode;

            ConstHelper.BestLOS = ConstBirthInput.BestLOS;

            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_birth", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_birth_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_dy_num", ConstHelper.NewBirthDayNumber, "IN", TdType.Decimal, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_mth_num", ConstHelper.NewBirthMonthNumber, "IN", TdType.Decimal, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_yr_num", ConstHelper.NewBirthYearNumber, "IN", TdType.Decimal, 4));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_birth_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }
    }
}