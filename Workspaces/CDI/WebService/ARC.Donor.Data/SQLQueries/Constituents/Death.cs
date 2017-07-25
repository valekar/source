using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class Death
    {
        public static string getDeathSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT *
        FROM DW_STUART_VWS.strx_cnst_dtl_cnst_death
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

        public static ConstituentDeathInput getAddDeathParameters(ARC.Donor.Data.Entities.Constituents.ConstituentDeathInput ConstDeathInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentDeathInput ConstHelper = new ConstituentDeathInput();
            ConstHelper.RequestType = "insert";
            if (!string.IsNullOrEmpty(ConstDeathInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstDeathInput.MasterID;

            if (!string.IsNullOrEmpty(ConstDeathInput.UserName))
                ConstHelper.UserName = ConstDeathInput.UserName;

            if (!string.IsNullOrEmpty(ConstDeathInput.ConstType))
                ConstHelper.ConstType = ConstDeathInput.ConstType;

            if (!string.IsNullOrEmpty(ConstDeathInput.Notes))
                ConstHelper.Notes = ConstDeathInput.Notes;

            if (!string.IsNullOrEmpty(ConstDeathInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstDeathInput.CaseNumber;

            ConstHelper.OldSourceSystemCode = string.Empty;
            ConstHelper.OldBestLOSInd = "0";

            if (!string.IsNullOrEmpty(ConstDeathInput.NewDeathDate.ToString()))
                ConstHelper.NewDeathDate = ConstDeathInput.NewDeathDate;

            if (!string.IsNullOrEmpty(ConstDeathInput.NewDeceasedCode.ToString()))
                ConstHelper.NewDeceasedCode = ConstDeathInput.NewDeceasedCode;

            if (!string.IsNullOrEmpty(ConstDeathInput.SourceSystemCode.ToString()))
                ConstHelper.SourceSystemCode = ConstDeathInput.SourceSystemCode;

            ConstHelper.BestLOS = ConstDeathInput.BestLOS;

            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_death", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_death_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_death_dt", ConstHelper.NewDeathDate, "IN", TdType.Date, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_deceased_cd", ConstHelper.NewDeceasedCode, "IN", TdType.Char, 1));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_death_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }

        public static ConstituentDeathInput getDeleteDeathParameters(ARC.Donor.Data.Entities.Constituents.ConstituentDeathInput ConstDeathInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentDeathInput ConstHelper = new ConstituentDeathInput();
            ConstHelper.RequestType = "delete";
            if (!string.IsNullOrEmpty(ConstDeathInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstDeathInput.MasterID;

            if (!string.IsNullOrEmpty(ConstDeathInput.UserName))
                ConstHelper.UserName = ConstDeathInput.UserName;

            if (!string.IsNullOrEmpty(ConstDeathInput.ConstType))
                ConstHelper.ConstType = ConstDeathInput.ConstType;

            if (!string.IsNullOrEmpty(ConstDeathInput.Notes))
                ConstHelper.Notes = ConstDeathInput.Notes;

            if (!string.IsNullOrEmpty(ConstDeathInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstDeathInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstDeathInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstDeathInput.OldSourceSystemCode;

            //Added by Srini
            if (string.IsNullOrEmpty(ConstDeathInput.OldBestLOSInd))
            {
                ConstHelper.OldBestLOSInd = "0";
            }
            else
            {
                ConstHelper.OldBestLOSInd = ConstDeathInput.OldBestLOSInd;
            }

            //assign empty strings for the remaining columns which are not needed
            ConstHelper.NewDeathDate = default(DateTime);
            if (ConstDeathInput.NewDeceasedCode == null)
            {
                ConstHelper.NewDeceasedCode = default(char);
            }
            else
            {
                ConstHelper.NewDeceasedCode = ConstDeathInput.NewDeceasedCode;
            }

            ConstHelper.SourceSystemCode = string.Empty;

            ConstHelper.BestLOS = ConstDeathInput.BestLOS;

            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_death", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_death_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_death_dt", ConstHelper.NewDeathDate, "IN", TdType.Date, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_deceased_cd", ConstHelper.NewDeceasedCode, "IN", TdType.Char, 1));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_death_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }

        public static ConstituentDeathInput getUpdateDeathParameters(ARC.Donor.Data.Entities.Constituents.ConstituentDeathInput ConstDeathInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentDeathInput ConstHelper = new ConstituentDeathInput();
            ConstHelper.RequestType = "update";
            if (!string.IsNullOrEmpty(ConstDeathInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstDeathInput.MasterID;

            if (!string.IsNullOrEmpty(ConstDeathInput.UserName))
                ConstHelper.UserName = ConstDeathInput.UserName;

            if (!string.IsNullOrEmpty(ConstDeathInput.ConstType))
                ConstHelper.ConstType = ConstDeathInput.ConstType;

            if (!string.IsNullOrEmpty(ConstDeathInput.Notes))
                ConstHelper.Notes = ConstDeathInput.Notes;

            if (!string.IsNullOrEmpty(ConstDeathInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstDeathInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstDeathInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstDeathInput.OldSourceSystemCode;

            //Added by Srini
            if (string.IsNullOrEmpty(ConstDeathInput.OldBestLOSInd))
            {
                ConstHelper.OldBestLOSInd = "0";
            }
            else
            {
                ConstHelper.OldBestLOSInd = ConstDeathInput.OldBestLOSInd;
            }


            if (!string.IsNullOrEmpty(ConstDeathInput.NewDeathDate.ToString()))
                ConstHelper.NewDeathDate = ConstDeathInput.NewDeathDate;

            if (!string.IsNullOrEmpty(ConstDeathInput.NewDeceasedCode.ToString()))
            {
                ConstHelper.NewDeceasedCode = ConstDeathInput.NewDeceasedCode;
            }
            else
            {
                ConstHelper.NewDeceasedCode = default(char);
            }

            if (!string.IsNullOrEmpty(ConstDeathInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstDeathInput.SourceSystemCode;

            ConstHelper.BestLOS = ConstDeathInput.BestLOS;

            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_death", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_death_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_death_dt", ConstHelper.NewDeathDate, "IN", TdType.Date, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_deceased_cd", ConstHelper.NewDeceasedCode, "IN", TdType.Char, 1));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_death_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }
    }
}