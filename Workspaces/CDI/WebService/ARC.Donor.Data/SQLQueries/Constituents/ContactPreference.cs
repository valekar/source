using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class ContactPreference
    {
        public static string getContactPreferenceSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT *
        FROM DW_STUART_VWS.strx_cnst_dtl_cntct_prefc
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

        public static ConstituentContactPrefcInput getAddContactPrefcParameters(ARC.Donor.Data.Entities.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentContactPrefcInput ConstHelper = new ConstituentContactPrefcInput();
            ConstHelper.RequestType = "insert";
            if (!string.IsNullOrEmpty(ConstContactPrefcInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstContactPrefcInput.MasterID;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.UserName))
                ConstHelper.UserName = ConstContactPrefcInput.UserName;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.ConstType))
                ConstHelper.ConstType = ConstContactPrefcInput.ConstType;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.Notes))
                ConstHelper.Notes = ConstContactPrefcInput.Notes;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstContactPrefcInput.CaseNumber;

            ConstHelper.OldContactPrefcValue = string.Empty;
            ConstHelper.OldSourceSystemCode = string.Empty;
            ConstHelper.OldContactPrefcTypeCode = string.Empty;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.ContactPrefcValue))
                ConstHelper.ContactPrefcValue = ConstContactPrefcInput.ContactPrefcValue;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.ContactPrefcTypeCode))
                ConstHelper.ContactPrefcTypeCode = ConstContactPrefcInput.ContactPrefcTypeCode;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstContactPrefcInput.SourceSystemCode;

            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_cntct_prefc", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cntct_prefc_val", ConstHelper.OldContactPrefcValue, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cntct_prefc_typ_cd", ConstHelper.OldContactPrefcTypeCode, "IN", TdType.VarChar, 40));
            //changed by srini - not required
            // ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cntct_prefc_val", ConstHelper.ContactPrefcValue, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
            //changed by srini -- changed to ContactPrefcTypeCode from ConstType
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cntct_prefc_typ_cd", ConstHelper.ContactPrefcTypeCode, "IN", TdType.VarChar, 40));
            parameters = ParamObjects;
            return ConstHelper;
        }

        public static ConstituentContactPrefcInput getDeleteContactPrefcParameters(ARC.Donor.Data.Entities.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentContactPrefcInput ConstHelper = new ConstituentContactPrefcInput();
            ConstHelper.RequestType = "delete";
            if (!string.IsNullOrEmpty(ConstContactPrefcInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstContactPrefcInput.MasterID;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.UserName))
                ConstHelper.UserName = ConstContactPrefcInput.UserName;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.ConstType))
                ConstHelper.ConstType = ConstContactPrefcInput.ConstType;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.Notes))
                ConstHelper.Notes = ConstContactPrefcInput.Notes;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstContactPrefcInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.OldContactPrefcValue))
                ConstHelper.OldContactPrefcValue = ConstContactPrefcInput.OldContactPrefcValue;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstContactPrefcInput.OldSourceSystemCode;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.OldContactPrefcTypeCode))
                ConstHelper.OldContactPrefcTypeCode = ConstContactPrefcInput.OldContactPrefcTypeCode;

            //assign empty strings for the remaining columns which are not needed
            ConstHelper.ContactPrefcTypeCode = string.Empty;
            ConstHelper.ContactPrefcValue = string.Empty;
            ConstHelper.SourceSystemCode = string.Empty;

            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_cntct_prefc", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cntct_prefc_val", ConstHelper.OldContactPrefcValue, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cntct_prefc_typ_cd", ConstHelper.OldContactPrefcTypeCode, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cntct_prefc_val", ConstHelper.ContactPrefcValue, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
            //changed by srini -- changed to ContactPrefcTypeCode from ConstType
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cntct_prefc_typ_cd", ConstHelper.ContactPrefcTypeCode, "IN", TdType.VarChar, 40));
            parameters = ParamObjects;
            return ConstHelper;
        }

        public static ConstituentContactPrefcInput getUpdateContactPrefcParameters(ARC.Donor.Data.Entities.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentContactPrefcInput ConstHelper = new ConstituentContactPrefcInput();
            ConstHelper.RequestType = "update";
            if (!string.IsNullOrEmpty(ConstContactPrefcInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstContactPrefcInput.MasterID;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.UserName))
                ConstHelper.UserName = ConstContactPrefcInput.UserName;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.ConstType))
                ConstHelper.ConstType = ConstContactPrefcInput.ConstType;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.Notes))
                ConstHelper.Notes = ConstContactPrefcInput.Notes;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstContactPrefcInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.OldContactPrefcValue))
                ConstHelper.OldContactPrefcValue = ConstContactPrefcInput.OldContactPrefcValue;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstContactPrefcInput.OldSourceSystemCode;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.OldContactPrefcTypeCode))
                ConstHelper.OldContactPrefcTypeCode = ConstContactPrefcInput.OldContactPrefcTypeCode;

            //assign values for the remaining columns which are to be updated
            if (!string.IsNullOrEmpty(ConstContactPrefcInput.ContactPrefcValue))
                ConstHelper.ContactPrefcValue = ConstContactPrefcInput.ContactPrefcValue;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstContactPrefcInput.SourceSystemCode;

            if (!string.IsNullOrEmpty(ConstContactPrefcInput.ContactPrefcTypeCode))
                ConstHelper.ContactPrefcTypeCode = ConstContactPrefcInput.ContactPrefcTypeCode;

            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_cntct_prefc", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cntct_prefc_val", ConstHelper.OldContactPrefcValue, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_cntct_prefc_typ_cd", ConstHelper.OldContactPrefcTypeCode, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cntct_prefc_val", ConstHelper.ContactPrefcValue, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
            //changed by srini -- changed to ContactPrefcTypeCode from ConstType
            ParamObjects.Add(SPHelper.createTdParameter("i_new_cntct_prefc_typ_cd", ConstHelper.ContactPrefcTypeCode, "IN", TdType.VarChar, 40));
            parameters = ParamObjects;
            return ConstHelper;
        }
    }
}