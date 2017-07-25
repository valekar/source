using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class Address
    {
        public static string getAddressSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT cnst_mstr_id, cnst_srcsys_id, cnst_addr_strt_ts, arc_srcsys_cd,
		arc_srcsys_uid, addr_typ_cd, cnst_addr_end_dt, best_addr_ind,
		cnst_addr_group_ind, cnst_addr_clssfctn_ind, cnst_addr_line1_addr,
		cnst_addr_line2_addr, cnst_addr_city_nm, cnst_addr_state_cd,
		cnst_addr_zip_5_cd, cnst_addr_zip_4_cd, cnst_addr_carrier_route,
		cnst_addr_county_nm, cnst_addr_country_cd, cnst_addr_latitude,
		cnst_addr_longitude, cnst_addr_non_us_pstl_c, cnst_addr_prefd_ind,
		cnst_addr_ff_mov_ind, cnst_addr_unserv_ind, cnst_addr_undeliv_ind,
		cnst_addr_best_los_ind, trans_key, act_ind, user_id, dw_srcsys_trans_ts,
		row_stat_cd, appl_src_cd, load_id, is_previous, transaction_key,
		trans_status, inactive_ind, strx_row_stat_cd, unique_trans_key,
		assessmnt_ctg, dpv_cd, res_deliv_ind, locator_addr_key
        FROM DW_STUART_VWS.strx_cnst_dtl_cnst_addr
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

        public static ConstituentAddressInput getAddAddressParameters(ARC.Donor.Data.Entities.Constituents.ConstituentAddressInput ConstAddressInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentAddressInput ConstHelper = new ConstituentAddressInput();
            ConstHelper.RequestType = "insert";
            if (!string.IsNullOrEmpty(ConstAddressInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstAddressInput.MasterID;

            if (!string.IsNullOrEmpty(ConstAddressInput.UserName))
                ConstHelper.UserName = ConstAddressInput.UserName;

            if (!string.IsNullOrEmpty(ConstAddressInput.ConstType))
                ConstHelper.ConstType = ConstAddressInput.ConstType;

            if (!string.IsNullOrEmpty(ConstAddressInput.Notes))
                ConstHelper.Notes = ConstAddressInput.Notes;

            if (!string.IsNullOrEmpty(ConstAddressInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstAddressInput.CaseNumber;

            ConstHelper.OldSourceSystemCode = string.Empty;
            ConstHelper.OldBestLOSInd = "0";
            ConstHelper.OldAddressTypeCode = string.Empty;

            if (!string.IsNullOrEmpty(ConstAddressInput.AddressLine1))
                ConstHelper.AddressLine1 = ConstAddressInput.AddressLine1;

            if (!string.IsNullOrEmpty(ConstAddressInput.AddressLine2))
                ConstHelper.AddressLine2 = ConstAddressInput.AddressLine2;

            if (!string.IsNullOrEmpty(ConstAddressInput.City))
                ConstHelper.City = ConstAddressInput.City;
            if (!string.IsNullOrEmpty(ConstAddressInput.State))
                ConstHelper.State = ConstAddressInput.State;
            if (!string.IsNullOrEmpty(ConstAddressInput.Country))
                ConstHelper.Country = ConstAddressInput.Country;
            if (!string.IsNullOrEmpty(ConstAddressInput.Zip4))
                ConstHelper.Zip4 = ConstAddressInput.Zip4;
            if (!string.IsNullOrEmpty(ConstAddressInput.Zip5))
                ConstHelper.Zip5 = ConstAddressInput.Zip5;


            if (!string.IsNullOrEmpty(ConstAddressInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstAddressInput.SourceSystemCode;

            if (!string.IsNullOrEmpty(ConstAddressInput.AddressTypeCode))
                ConstHelper.AddressTypeCode = ConstAddressInput.AddressTypeCode;

            ConstHelper.BestLOS = ConstAddressInput.BestLOS;

            int intNumberOfInputParameters = 20;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_addr", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            //changed by srini - order of params changed
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_addr_typ_cd", ConstHelper.OldAddressTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_addr_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_line1_addr", ConstHelper.AddressLine1, "IN", TdType.VarChar, 250));
            // ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_line2_addr", ConstHelper.AddressLine2, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_city_nm", ConstHelper.City, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_state_cd", ConstHelper.State, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_county_nm", ConstHelper.Country, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_zip_4_cd", ConstHelper.Zip4, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_zip_5_cd", ConstHelper.Zip5, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_undeliv_ind", ConstHelper.UndeliveredIndicator, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_typ_cd", ConstHelper.AddressTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));
            parameters = ParamObjects;
            return ConstHelper;
        }

        public static ConstituentAddressInput getDeleteAddressParameters(ARC.Donor.Data.Entities.Constituents.ConstituentAddressInput ConstAddressInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentAddressInput ConstHelper = new ConstituentAddressInput();
            ConstHelper.RequestType = "delete";
            if (!string.IsNullOrEmpty(ConstAddressInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstAddressInput.MasterID;

            if (!string.IsNullOrEmpty(ConstAddressInput.UserName))
                ConstHelper.UserName = ConstAddressInput.UserName;

            if (!string.IsNullOrEmpty(ConstAddressInput.ConstType))
                ConstHelper.ConstType = ConstAddressInput.ConstType;

            if (!string.IsNullOrEmpty(ConstAddressInput.Notes))
                ConstHelper.Notes = ConstAddressInput.Notes;

            if (!string.IsNullOrEmpty(ConstAddressInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstAddressInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstAddressInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstAddressInput.OldSourceSystemCode;

            //added by srini
            if (string.IsNullOrEmpty(ConstAddressInput.OldBestLOSInd))
            {
                ConstHelper.OldBestLOSInd = "0";
            }
            else
            {
                ConstHelper.OldBestLOSInd = ConstAddressInput.OldBestLOSInd;
            }

            if (!string.IsNullOrEmpty(ConstAddressInput.OldAddressTypeCode))
                ConstHelper.OldAddressTypeCode = ConstAddressInput.OldAddressTypeCode;

            //assign empty strings for the remaining columns which are not needed
            ConstHelper.AddressLine1 = string.Empty;
            ConstHelper.AddressLine2 = string.Empty;
            ConstHelper.City = string.Empty;
            ConstHelper.State = string.Empty;
            ConstHelper.Country = string.Empty;
            ConstHelper.Zip4 = string.Empty;
            ConstHelper.Zip5 = string.Empty;

            ConstHelper.SourceSystemCode = string.Empty;
            ConstHelper.AddressTypeCode = string.Empty;

            ConstHelper.BestLOS = 0;

            int intNumberOfInputParameters = 20;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_addr", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            //changed by srini - order of params changed
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_addr_typ_cd", ConstHelper.OldAddressTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_addr_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_line1_addr", ConstHelper.AddressLine1, "IN", TdType.VarChar, 250));
            //ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_line2_addr", ConstHelper.AddressLine2, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_city_nm", ConstHelper.City, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_state_cd", ConstHelper.State, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_county_nm", ConstHelper.Country, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_zip_4_cd", ConstHelper.Zip4, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_zip_5_cd", ConstHelper.Zip5, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_undeliv_ind", ConstHelper.UndeliveredIndicator, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_typ_cd", ConstHelper.AddressTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));
            parameters = ParamObjects;

            return ConstHelper;
        }

        public static ConstituentAddressInput getUpdateAddressParameters(ARC.Donor.Data.Entities.Constituents.ConstituentAddressInput ConstAddressInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            ConstituentAddressInput ConstHelper = new ConstituentAddressInput();
            ConstHelper.RequestType = "update";
            if (!string.IsNullOrEmpty(ConstAddressInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstAddressInput.MasterID;

            if (!string.IsNullOrEmpty(ConstAddressInput.UserName))
                ConstHelper.UserName = ConstAddressInput.UserName;

            if (!string.IsNullOrEmpty(ConstAddressInput.ConstType))
                ConstHelper.ConstType = ConstAddressInput.ConstType;

            if (!string.IsNullOrEmpty(ConstAddressInput.Notes))
                ConstHelper.Notes = ConstAddressInput.Notes;

            if (!string.IsNullOrEmpty(ConstAddressInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstAddressInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstAddressInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstAddressInput.OldSourceSystemCode;
            //added by srini
            if (string.IsNullOrEmpty(ConstAddressInput.OldBestLOSInd))
            {
                ConstHelper.OldBestLOSInd = "0";
            }
            else
            {
                ConstHelper.OldBestLOSInd = ConstAddressInput.OldBestLOSInd;
            }


            if (!string.IsNullOrEmpty(ConstAddressInput.OldAddressTypeCode))
                ConstHelper.OldAddressTypeCode = ConstAddressInput.OldAddressTypeCode;

            if (!string.IsNullOrEmpty(ConstAddressInput.AddressLine1))
                ConstHelper.AddressLine1 = ConstAddressInput.AddressLine1;

            if (!string.IsNullOrEmpty(ConstAddressInput.AddressLine2))
                ConstHelper.AddressLine2 = ConstAddressInput.AddressLine2;

            if (!string.IsNullOrEmpty(ConstAddressInput.City))
                ConstHelper.City = ConstAddressInput.City;

            if (!string.IsNullOrEmpty(ConstAddressInput.State))
                ConstHelper.State = ConstAddressInput.State;

            if (!string.IsNullOrEmpty(ConstAddressInput.Country))
                ConstHelper.Country = ConstAddressInput.Country;

            if (!string.IsNullOrEmpty(ConstAddressInput.Zip4))
                ConstHelper.Zip4 = ConstAddressInput.Zip4;

            if (!string.IsNullOrEmpty(ConstAddressInput.Zip5))
                ConstHelper.Zip5 = ConstAddressInput.Zip5;

            if (!string.IsNullOrEmpty(ConstAddressInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstAddressInput.SourceSystemCode;

            if (!string.IsNullOrEmpty(ConstAddressInput.AddressTypeCode))
                ConstHelper.AddressTypeCode = ConstAddressInput.AddressTypeCode;

            ConstHelper.BestLOS = ConstAddressInput.BestLOS;

            int intNumberOfInputParameters = 20;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_addr", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            //changed by srini -- changed the order 
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_addr_typ_cd", ConstHelper.OldAddressTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_addr_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_line1_addr", ConstHelper.AddressLine1, "IN", TdType.VarChar, 250));
            //ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_line2_addr", ConstHelper.AddressLine2, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_city_nm", ConstHelper.City, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_state_cd", ConstHelper.State, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_county_nm", ConstHelper.State, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_zip_4_cd", ConstHelper.Zip4, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_zip_5_cd", ConstHelper.Zip5, "IN", TdType.VarChar, 250));
            // ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_undeliv_ind", ConstHelper.Zip5, "IN", TdType.VarChar, 250));
            //changed by srini made it as ByteInt
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_undeliv_ind", ConstHelper.UndeliveredIndicator, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_typ_cd", ConstHelper.AddressTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_addr_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }
    }
}