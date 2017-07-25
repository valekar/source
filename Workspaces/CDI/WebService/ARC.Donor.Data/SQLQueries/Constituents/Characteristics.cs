using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class Characteristics
    {
        public static string getCharacteristicsSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT *
        FROM DW_STUART_VWS.strx_cnst_dtl_chrctrstc
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

        public static ConstituentCharacteristicsInput getAddCharacteristicsParameters(ARC.Donor.Data.Entities.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentCharacteristicsInput ConstHelper = new ConstituentCharacteristicsInput();
            ConstHelper.RequestType = "insert";
            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstCharacteristicsInput.MasterID;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.UserName))
                ConstHelper.UserName = ConstCharacteristicsInput.UserName;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.ConstType))
                ConstHelper.ConstType = ConstCharacteristicsInput.ConstType;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.Notes))
                ConstHelper.Notes = ConstCharacteristicsInput.Notes;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstCharacteristicsInput.CaseNumber;

            ConstHelper.OldCharacteristicTypeCode = string.Empty;
            ConstHelper.OldCharacteristicValue = string.Empty;
            ConstHelper.OldSourceSystemCode = string.Empty;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.CharacteristicTypeCode))
                ConstHelper.CharacteristicTypeCode = ConstCharacteristicsInput.CharacteristicTypeCode;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.CharacteristicValue))
                ConstHelper.CharacteristicValue = ConstCharacteristicsInput.CharacteristicValue;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstCharacteristicsInput.SourceSystemCode;

            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_chrctrstc", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            //changed by srini -- changed from bigint to varchar
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_val", ConstHelper.OldCharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_typ_cd", ConstHelper.OldCharacteristicTypeCode, "IN", TdType.VarChar, 20));
            //changed from bigint to varchar
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_val", ConstHelper.CharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_typ_cd", ConstHelper.CharacteristicTypeCode, "IN", TdType.VarChar, 20));
            parameters = ParamObjects;
            return ConstHelper;
        }

        public static ConstituentCharacteristicsInput getDeleteCharacteristicsParameters(ARC.Donor.Data.Entities.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentCharacteristicsInput ConstHelper = new ConstituentCharacteristicsInput();
            ConstHelper.RequestType = "delete";
            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstCharacteristicsInput.MasterID;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.UserName))
                ConstHelper.UserName = ConstCharacteristicsInput.UserName;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.ConstType))
                ConstHelper.ConstType = ConstCharacteristicsInput.ConstType;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.Notes))
                ConstHelper.Notes = ConstCharacteristicsInput.Notes;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstCharacteristicsInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.OldCharacteristicTypeCode))
                ConstHelper.OldCharacteristicTypeCode = ConstCharacteristicsInput.OldCharacteristicTypeCode;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.OldCharacteristicValue))
                ConstHelper.OldCharacteristicValue = ConstCharacteristicsInput.OldCharacteristicValue;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstCharacteristicsInput.OldSourceSystemCode;


            ConstHelper.CharacteristicTypeCode = string.Empty;
            ConstHelper.CharacteristicValue = string.Empty;
            ConstHelper.SourceSystemCode = string.Empty;

            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_chrctrstc", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            //changed by srini -- changed from bigint to varchar
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_val", ConstHelper.OldCharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_typ_cd", ConstHelper.OldCharacteristicTypeCode, "IN", TdType.VarChar, 20));
            //changed from bigint to varchar
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_val", ConstHelper.CharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_typ_cd", ConstHelper.CharacteristicTypeCode, "IN", TdType.VarChar, 20));
            parameters = ParamObjects;
            return ConstHelper;
        }

        public static ConstituentCharacteristicsInput getUpdateCharacteristicsParameters(ARC.Donor.Data.Entities.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentCharacteristicsInput ConstHelper = new ConstituentCharacteristicsInput();
            ConstHelper.RequestType = "update";
            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstCharacteristicsInput.MasterID;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.UserName))
                ConstHelper.UserName = ConstCharacteristicsInput.UserName;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.ConstType))
                ConstHelper.ConstType = ConstCharacteristicsInput.ConstType;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.Notes))
                ConstHelper.Notes = ConstCharacteristicsInput.Notes;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstCharacteristicsInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.OldCharacteristicTypeCode))
                ConstHelper.OldCharacteristicTypeCode = ConstCharacteristicsInput.OldCharacteristicTypeCode;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.OldCharacteristicValue))
                ConstHelper.OldCharacteristicValue = ConstCharacteristicsInput.OldCharacteristicValue;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstCharacteristicsInput.OldSourceSystemCode;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.CharacteristicTypeCode))
                ConstHelper.CharacteristicTypeCode = ConstCharacteristicsInput.CharacteristicTypeCode;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.CharacteristicValue))
                ConstHelper.CharacteristicValue = ConstCharacteristicsInput.CharacteristicValue;

            if (!string.IsNullOrEmpty(ConstCharacteristicsInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstCharacteristicsInput.SourceSystemCode;

            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_chrctrstc", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            //changed by srini -- changed from bigint to varchar
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_val", ConstHelper.OldCharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_typ_cd", ConstHelper.OldCharacteristicTypeCode, "IN", TdType.VarChar, 20));
            //changed from bigint to varchar
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_val", ConstHelper.CharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_typ_cd", ConstHelper.CharacteristicTypeCode, "IN", TdType.VarChar, 20));
            parameters = ParamObjects;
            return ConstHelper;
        }
    }
}