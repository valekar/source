using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teradata.Client.Provider;
using System.Collections;
using System.Data;


namespace ARC.Donor.Data.SQL.Constituents
{
    //code for Name related API's
    //class for GET API
    public class Name
    {
        public static string getPersonNameSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(QryPerson, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string QryPerson = @"SELECT cnst_mstr_id, locator_prsn_nm_key,cnst_srcsys_id, cnst_prsn_seq, cnst_prsn_nm_typ_cd,
		cnst_nm_strt_dt, arc_srcsys_cd, appl_src_cd, line_of_service_cd,
		cnst_prsn_nm_end_dt, best_prsn_nm_ind, cnst_prsn_first_nm, cnst_prsn_middle_nm,
		cnst_prsn_last_nm, cnst_prsn_prefix_nm, cnst_prsn_suffix_nm,
		cnst_prsn_full_nm, cnst_prsn_nick_nm, cnst_prsn_mom_maiden_nm,
		cnst_alias_out_saltn_nm, cnst_alias_in_saltn_nm, cnst_prsn_nm_best_los_ind,
		trans_key, act_ind, user_id, dw_srcsys_trans_ts, row_stat_cd,
		load_id, is_previous, transaction_key, trans_status, inactive_ind,assessmnt_ctg,
		strx_row_stat_cd, unique_trans_key
        FROM DW_STUART_VWS.strx_cnst_dtl_cnst_prsn_nm
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

        public static string getOrgNameSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(QryOrg, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string QryOrg = @"SELECT   cnst_mstr_id, cnst_srcsys_id, cnst_org_nm_typ_cd, arc_srcsys_cd, cnst_org_nm_strt_dt, cnst_org_nm_seq, 
            cnst_org_nm_end_dt, best_org_nm_ind, cnst_org_nm, cln_cnst_org_nm, cnst_org_nm_best_los_ind, 
            trans_key, act_ind, user_id, dw_srcsys_trans_ts, row_stat_cd, appl_src_cd, load_id, is_previous, 
            transaction_key, trans_status, inactive_ind, strx_row_stat_cd, unique_trans_key
            FROM     dw_stuart_vws.strx_cnst_dtl_cnst_org_nm 
            WHERE    cnst_mstr_id = {2}
            AND      ( 
                              trans_status NOT IN ('Rejected') 
                     OR       trans_status IS NULL) 
            AND      (( 
                                       trans_status IN ('Reject') 
                              AND      unique_trans_key IS NOT NULL 
                              AND      unique_trans_key <> '') 
                     OR       ( 
                                       trans_status IN ('Processed') 
                              AND      strx_row_stat_cd = 'F' 
                              AND      unique_trans_key IS NOT NULL 
                              AND      unique_trans_key <> '') 
                     OR       ( 
                                       trans_status NOT IN ('Reject', 
                                                            'Processed')) 
                     OR       trans_status IS NULL) 
            ORDER BY transaction_key, 
                     load_id;";

    }

    

    
    //class for Add Name API
    public class AddPersonName
    {
        public static ConstituentPersonNameInput getAddPersonNameParameters(ARC.Donor.Data.Entities.Constituents.ConstituentPersonNameInput ConstNameInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {            
            //Get the Full name, ouside salutation and inside salutation names from the ones that the user has entered
            string strFullAndSalutationName = string.Empty;
            string strSalutationInName = string.Empty;

            //Full Name and Outside Salutation Name = Prefix + First name + Middle Name + Last Name + Suffix Name 
            strFullAndSalutationName += !string.IsNullOrEmpty(ConstNameInput.PrefixName) ? ConstNameInput.PrefixName : string.Empty;
            strFullAndSalutationName += !string.IsNullOrEmpty(ConstNameInput.FirstName) ? " " + ConstNameInput.FirstName : string.Empty;
            strFullAndSalutationName += !string.IsNullOrEmpty(ConstNameInput.MiddleName) ? " " + ConstNameInput.MiddleName : string.Empty;
            strFullAndSalutationName += !string.IsNullOrEmpty(ConstNameInput.LastName) ? " " + ConstNameInput.LastName : string.Empty;
            strFullAndSalutationName += !string.IsNullOrEmpty(ConstNameInput.SuffixName) ? " " + ConstNameInput.SuffixName : string.Empty;

            //Inside Salutation Name =  Prefix + Last 
            strSalutationInName += !string.IsNullOrEmpty(ConstNameInput.PrefixName) ? ConstNameInput.PrefixName : string.Empty;
            strSalutationInName += !string.IsNullOrEmpty(ConstNameInput.LastName) ? ConstNameInput.LastName : string.Empty;

            //Helper record tpo have cleaner version of the data from the input
            ConstituentPersonNameInput ConstHelper = new ConstituentPersonNameInput();
            ConstHelper.RequestType = "insert";
            if (!string.IsNullOrEmpty(ConstNameInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstNameInput.MasterID;

            if (!string.IsNullOrEmpty(ConstNameInput.UserName))
                ConstHelper.UserName = ConstNameInput.UserName;

            if (!string.IsNullOrEmpty(ConstNameInput.ConstType))
                ConstHelper.ConstType = ConstNameInput.ConstType;

            if (!string.IsNullOrEmpty(ConstNameInput.Notes))
                ConstHelper.Notes = ConstNameInput.Notes;

            if (!string.IsNullOrEmpty(ConstNameInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstNameInput.CaseNumber;

            ConstHelper.OldSourceSystemCode = string.Empty;
            ConstHelper.OldBestLOSInd = 0;
            ConstHelper.OldNameTypeCode = string.Empty;

            if (!string.IsNullOrEmpty(ConstNameInput.FirstName))
                ConstHelper.FirstName = ConstNameInput.FirstName;

            if (!string.IsNullOrEmpty(ConstNameInput.LastName))
                ConstHelper.LastName = ConstNameInput.LastName;

            if (!string.IsNullOrEmpty(ConstNameInput.MiddleName))
                ConstHelper.MiddleName = ConstNameInput.MiddleName;

            if (!string.IsNullOrEmpty(ConstNameInput.NickName))
                ConstHelper.NickName = ConstNameInput.NickName;

            if (!string.IsNullOrEmpty(ConstNameInput.PrefixName))
                ConstHelper.PrefixName = ConstNameInput.PrefixName;

            if (!string.IsNullOrEmpty(ConstNameInput.SuffixName))
                ConstHelper.SuffixName = ConstNameInput.SuffixName;

            if (!string.IsNullOrEmpty(ConstNameInput.MaidenName))
                ConstHelper.MaidenName = ConstNameInput.MaidenName;

            ConstHelper.FullName = strFullAndSalutationName;
            ConstHelper.InSalutationName = strSalutationInName;
            ConstHelper.OutSalutationName = strFullAndSalutationName;

            if (!string.IsNullOrEmpty(ConstNameInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstNameInput.SourceSystemCode;

            if (!string.IsNullOrEmpty(ConstNameInput.NameTypeCode))
                ConstHelper.NameTypeCode = ConstNameInput.NameTypeCode;

            ConstHelper.BestLOS = ConstNameInput.BestLOS;

            int intNumberOfInputParameters = 22;
            List<string> listOutputParameters = new List<string>{"o_outputMessage","transaction_key"};

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_prsn_nm", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_prsn_nm_typ_cd", ConstHelper.OldNameTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_prsn_nm_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_first_nm", ConstHelper.FirstName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_middle_nm", ConstHelper.MiddleName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_last_nm", ConstHelper.LastName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_prefix_nm", ConstHelper.PrefixName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_suffix_nm", ConstHelper.SuffixName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_nick_nm", ConstHelper.NickName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_mom_maiden_nm", ConstHelper.MaidenName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_full_nm", ConstHelper.FullName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_alias_in_saltn_nm", ConstHelper.InSalutationName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_alias_out_saltn_nm", ConstHelper.OutSalutationName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_nm_typ_cd", ConstHelper.NameTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_nm_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstNameInput;
        }
    }

    //class for Delete Name API
    public class DeletePersonName
    {
        public static ConstituentPersonNameInput getDeletePersonNameParameters(ARC.Donor.Data.Entities.Constituents.ConstituentPersonNameInput ConstNameInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            ConstituentPersonNameInput ConstHelper = new ConstituentPersonNameInput();
            ConstHelper.RequestType = "delete";
            if (!string.IsNullOrEmpty(ConstNameInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstNameInput.MasterID;

            if (!string.IsNullOrEmpty(ConstNameInput.UserName))
                ConstHelper.UserName = ConstNameInput.UserName;

            if (!string.IsNullOrEmpty(ConstNameInput.ConstType))
                ConstHelper.ConstType = ConstNameInput.ConstType;

            if (!string.IsNullOrEmpty(ConstNameInput.Notes))
                ConstHelper.Notes = ConstNameInput.Notes;

            if (!string.IsNullOrEmpty(ConstNameInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstNameInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstNameInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstNameInput.OldSourceSystemCode;

            ConstHelper.OldBestLOSInd = ConstNameInput.OldBestLOSInd;

            if (!string.IsNullOrEmpty(ConstNameInput.OldNameTypeCode))
                ConstHelper.OldNameTypeCode = ConstNameInput.OldNameTypeCode;

            //assign empty strings for the remaining columns which are not needed
            ConstHelper.FirstName = string.Empty;
            ConstHelper.LastName = string.Empty;
            ConstHelper.MiddleName = string.Empty;
            ConstHelper.NickName = string.Empty;
            ConstHelper.PrefixName = string.Empty;
            ConstHelper.SuffixName = string.Empty;
            ConstHelper.MaidenName = string.Empty;
            ConstHelper.FullName = string.Empty;
            ConstHelper.InSalutationName = string.Empty;
            ConstHelper.OutSalutationName = string.Empty;
            ConstHelper.SourceSystemCode = string.Empty;
            ConstHelper.NameTypeCode = string.Empty;

            ConstHelper.BestLOS = 0;

            int intNumberOfInputParameters = 22;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_prsn_nm", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_prsn_nm_typ_cd", ConstHelper.OldNameTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_prsn_nm_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_first_nm", ConstHelper.FirstName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_middle_nm", ConstHelper.MiddleName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_last_nm", ConstHelper.LastName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_prefix_nm", ConstHelper.PrefixName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_suffix_nm", ConstHelper.SuffixName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_nick_nm", ConstHelper.NickName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_mom_maiden_nm", ConstHelper.MaidenName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_full_nm", ConstHelper.FullName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_alias_in_saltn_nm", ConstHelper.InSalutationName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_alias_out_saltn_nm", ConstHelper.OutSalutationName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_nm_typ_cd", ConstHelper.NameTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_nm_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstNameInput;
        }
    }

    //class for Update Name API
    public class UpdatePersonName
    {
        public static ConstituentPersonNameInput getUpdatePersonNameParameters(ARC.Donor.Data.Entities.Constituents.ConstituentPersonNameInput ConstNameInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Get the Full name, ouside salutation and inside salutation names from the ones that the user has entered
            string strFullAndSalutationName = string.Empty;
            string strSalutationInName = string.Empty;

            //Full Name and Outside Salutation Name = Prefix + First name + Middle Name + Last Name + Suffix Name 
            strFullAndSalutationName += !string.IsNullOrEmpty(ConstNameInput.PrefixName) ? ConstNameInput.PrefixName : string.Empty;
            strFullAndSalutationName += !string.IsNullOrEmpty(ConstNameInput.FirstName) ? " " + ConstNameInput.FirstName : string.Empty;
            strFullAndSalutationName += !string.IsNullOrEmpty(ConstNameInput.MiddleName) ? " " + ConstNameInput.MiddleName : string.Empty;
            strFullAndSalutationName += !string.IsNullOrEmpty(ConstNameInput.LastName) ? " " + ConstNameInput.LastName : string.Empty;
            strFullAndSalutationName += !string.IsNullOrEmpty(ConstNameInput.SuffixName) ? " " + ConstNameInput.SuffixName : string.Empty;

            //Inside Salutation Name =  Prefix + Last 
            strSalutationInName += !string.IsNullOrEmpty(ConstNameInput.PrefixName) ? ConstNameInput.PrefixName : string.Empty;
            strSalutationInName += !string.IsNullOrEmpty(ConstNameInput.LastName) ? ConstNameInput.LastName : string.Empty;

            ConstituentPersonNameInput ConstHelper = new ConstituentPersonNameInput();
            ConstHelper.RequestType = "update";
            if (!string.IsNullOrEmpty(ConstNameInput.MasterID.ToString()))
                ConstHelper.MasterID = ConstNameInput.MasterID;

            if (!string.IsNullOrEmpty(ConstNameInput.UserName))
                ConstHelper.UserName = ConstNameInput.UserName;

            if (!string.IsNullOrEmpty(ConstNameInput.ConstType))
                ConstHelper.ConstType = ConstNameInput.ConstType;

            if (!string.IsNullOrEmpty(ConstNameInput.Notes))
                ConstHelper.Notes = ConstNameInput.Notes;

            if (!string.IsNullOrEmpty(ConstNameInput.CaseNumber.ToString()))
                ConstHelper.CaseNumber = ConstNameInput.CaseNumber;

            if (!string.IsNullOrEmpty(ConstNameInput.OldSourceSystemCode))
                ConstHelper.OldSourceSystemCode = ConstNameInput.OldSourceSystemCode;

            ConstHelper.OldBestLOSInd = ConstNameInput.OldBestLOSInd;

            if (!string.IsNullOrEmpty(ConstNameInput.OldNameTypeCode))
                ConstHelper.OldNameTypeCode = ConstNameInput.OldNameTypeCode;

            if (!string.IsNullOrEmpty(ConstNameInput.FirstName))
                ConstHelper.FirstName = ConstNameInput.FirstName;

            if (!string.IsNullOrEmpty(ConstNameInput.LastName))
                ConstHelper.LastName = ConstNameInput.LastName;

            if (!string.IsNullOrEmpty(ConstNameInput.MiddleName))
                ConstHelper.MiddleName = ConstNameInput.MiddleName;

            if (!string.IsNullOrEmpty(ConstNameInput.NickName))
                ConstHelper.NickName = ConstNameInput.NickName;

            if (!string.IsNullOrEmpty(ConstNameInput.PrefixName))
                ConstHelper.PrefixName = ConstNameInput.PrefixName;

            if (!string.IsNullOrEmpty(ConstNameInput.SuffixName))
                ConstHelper.SuffixName = ConstNameInput.SuffixName;

            if (!string.IsNullOrEmpty(ConstNameInput.MaidenName))
                ConstHelper.MaidenName = ConstNameInput.MaidenName;

            ConstHelper.FullName = strFullAndSalutationName;
            ConstHelper.InSalutationName = strSalutationInName;
            ConstHelper.OutSalutationName = strFullAndSalutationName;

            if (!string.IsNullOrEmpty(ConstNameInput.SourceSystemCode))
                ConstHelper.SourceSystemCode = ConstNameInput.SourceSystemCode;

            if (!string.IsNullOrEmpty(ConstNameInput.NameTypeCode))
                ConstHelper.NameTypeCode = ConstNameInput.NameTypeCode;

            ConstHelper.BestLOS = ConstNameInput.BestLOS;

            int intNumberOfInputParameters = 22;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_prsn_nm", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_prsn_nm_typ_cd", ConstHelper.OldNameTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_prsn_nm_best_los_ind", ConstHelper.OldBestLOSInd, "IN", TdType.ByteInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_first_nm", ConstHelper.FirstName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_middle_nm", ConstHelper.MiddleName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_last_nm", ConstHelper.LastName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_prefix_nm", ConstHelper.PrefixName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_suffix_nm", ConstHelper.SuffixName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_nick_nm", ConstHelper.NickName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_mom_maiden_nm", ConstHelper.MaidenName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_full_nm", ConstHelper.FullName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_alias_in_saltn_nm", ConstHelper.InSalutationName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_alias_out_saltn_nm", ConstHelper.OutSalutationName, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_nm_typ_cd", ConstHelper.NameTypeCode, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_prsn_nm_best_los_ind", ConstHelper.BestLOS, "IN", TdType.ByteInt, 250));

            parameters = ParamObjects;

            return ConstNameInput;
        }
    }

    public class WriteOrgName
    {
        public static ConstituentOrgNameInput getWriteOrgNameParameters(ARC.Donor.Data.Entities.Constituents.ConstituentOrgNameInput ConstNameInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            if(RequestType=="insert")
            {

                ConstituentOrgNameInput ConstHelper = new ConstituentOrgNameInput();

                ConstHelper.RequestType = RequestType;

                if (!string.IsNullOrEmpty(ConstNameInput.MasterID.ToString()))
                    ConstHelper.MasterID = ConstNameInput.MasterID;

                if (!string.IsNullOrEmpty(ConstNameInput.UserName))
                    ConstHelper.UserName = ConstNameInput.UserName;

                if (!string.IsNullOrEmpty(ConstNameInput.ConstType))
                    ConstHelper.ConstType = ConstNameInput.ConstType;

                if (!string.IsNullOrEmpty(ConstNameInput.Notes))
                    ConstHelper.Notes = ConstNameInput.Notes;

                if (!string.IsNullOrEmpty(ConstNameInput.CaseNumber.ToString()))
                    ConstHelper.CaseNumber = ConstNameInput.CaseNumber;

                ConstHelper.OldOrgNameTypeCode = string.Empty;
                ConstHelper.OldSourceSystemCode = string.Empty;
                ConstHelper.OldOrgNameBestLOSInd = 0;

                if (!string.IsNullOrEmpty(ConstNameInput.OrgName))
                    ConstHelper.OrgName = ConstNameInput.OrgName;
                if (!string.IsNullOrEmpty(ConstNameInput.OrgNameBestLOS.ToString()))
                    ConstHelper.OrgNameBestLOS = ConstNameInput.OrgNameBestLOS;
                else
                    ConstHelper.OrgNameBestLOS = 0;
                if (!string.IsNullOrEmpty(ConstNameInput.OrgNameTypeCode))
                    ConstHelper.OrgNameTypeCode = ConstNameInput.OrgNameTypeCode;
                if (!string.IsNullOrEmpty(ConstNameInput.SourceSystemCode))
                    ConstHelper.SourceSystemCode = ConstNameInput.SourceSystemCode;

                int intNumberOfInputParameters = 13;
                List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_transaction_key" };

                strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_org_nm", intNumberOfInputParameters, listOutputParameters);

                var ParamObjects = new List<object>();
                ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
                ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 10));
                ParamObjects.Add(SPHelper.createTdParameter("i_bk_org_nm_typ_cd", ConstHelper.OldOrgNameTypeCode, "IN", TdType.VarChar, 5));
                ParamObjects.Add(SPHelper.createTdParameter("i_bk_org_nm_best_los_ind", ConstHelper.OldOrgNameBestLOSInd, "IN", TdType.ByteInt, 0));

                ParamObjects.Add(SPHelper.createTdParameter("i_new_org_nm", ConstHelper.OrgName, "IN", TdType.VarChar, 150));
                ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
                ParamObjects.Add(SPHelper.createTdParameter("i_new_org_nm_typ_cd", ConstHelper.OrgNameTypeCode, "IN", TdType.VarChar, 5));
                ParamObjects.Add(SPHelper.createTdParameter("i_new_org_nm_best_los_ind", ConstHelper.OrgNameBestLOS, "IN", TdType.ByteInt, 0));

                parameters = ParamObjects;

                return ConstNameInput;
            }
            else if(RequestType=="update")
            {
                ConstituentOrgNameInput ConstHelper = new ConstituentOrgNameInput();

                ConstHelper.RequestType = RequestType;

                if (!string.IsNullOrEmpty(ConstNameInput.MasterID.ToString()))
                    ConstHelper.MasterID = ConstNameInput.MasterID;

                if (!string.IsNullOrEmpty(ConstNameInput.UserName))
                    ConstHelper.UserName = ConstNameInput.UserName;

                if (!string.IsNullOrEmpty(ConstNameInput.ConstType))
                    ConstHelper.ConstType = ConstNameInput.ConstType;

                if (!string.IsNullOrEmpty(ConstNameInput.Notes))
                    ConstHelper.Notes = ConstNameInput.Notes;

                if (!string.IsNullOrEmpty(ConstNameInput.CaseNumber.ToString()))
                    ConstHelper.CaseNumber = ConstNameInput.CaseNumber;

                if (!string.IsNullOrEmpty(ConstNameInput.OldOrgNameTypeCode))
                    ConstHelper.OldOrgNameTypeCode = ConstNameInput.OldOrgNameTypeCode;
                if (!string.IsNullOrEmpty(ConstNameInput.OldSourceSystemCode))
                    ConstHelper.OldSourceSystemCode = ConstNameInput.OldSourceSystemCode;
                if (!string.IsNullOrEmpty(ConstNameInput.OldOrgNameBestLOSInd.ToString()))
                    ConstHelper.OldOrgNameBestLOSInd = ConstNameInput.OldOrgNameBestLOSInd;
                else
                    ConstHelper.OldOrgNameBestLOSInd = 0;

                if (!string.IsNullOrEmpty(ConstNameInput.OrgName))
                    ConstHelper.OrgName = ConstNameInput.OrgName;
                if (!string.IsNullOrEmpty(ConstNameInput.OrgNameBestLOS.ToString()))
                    ConstHelper.OrgNameBestLOS = ConstNameInput.OrgNameBestLOS;
                else
                    ConstHelper.OrgNameBestLOS = 0;
                if (!string.IsNullOrEmpty(ConstNameInput.OrgNameTypeCode))
                    ConstHelper.OrgNameTypeCode = ConstNameInput.OrgNameTypeCode;
                if (!string.IsNullOrEmpty(ConstNameInput.SourceSystemCode))
                    ConstHelper.SourceSystemCode = ConstNameInput.SourceSystemCode;

                int intNumberOfInputParameters = 13;
                List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_transaction_key" };

                strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_org_nm", intNumberOfInputParameters, listOutputParameters);

                var ParamObjects = new List<object>();
                ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
                ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 10));
                ParamObjects.Add(SPHelper.createTdParameter("i_bk_org_nm_typ_cd", ConstHelper.OldOrgNameTypeCode, "IN", TdType.VarChar, 5));
                ParamObjects.Add(SPHelper.createTdParameter("i_bk_org_nm_best_los_ind", ConstHelper.OldOrgNameBestLOSInd, "IN", TdType.ByteInt, 0));

                ParamObjects.Add(SPHelper.createTdParameter("i_new_org_nm", ConstHelper.OrgName, "IN", TdType.VarChar, 150));
                ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
                ParamObjects.Add(SPHelper.createTdParameter("i_new_org_nm_typ_cd", ConstHelper.OrgNameTypeCode, "IN", TdType.VarChar, 5));
                ParamObjects.Add(SPHelper.createTdParameter("i_new_org_nm_best_los_ind", ConstHelper.OrgNameBestLOS, "IN", TdType.ByteInt, 0));

                parameters = ParamObjects;

                return ConstNameInput;
            }
            else
            {
                ConstituentOrgNameInput ConstHelper = new ConstituentOrgNameInput();

                ConstHelper.RequestType = RequestType;

                if (!string.IsNullOrEmpty(ConstNameInput.MasterID.ToString()))
                    ConstHelper.MasterID = ConstNameInput.MasterID;

                if (!string.IsNullOrEmpty(ConstNameInput.UserName))
                    ConstHelper.UserName = ConstNameInput.UserName;

                if (!string.IsNullOrEmpty(ConstNameInput.ConstType))
                    ConstHelper.ConstType = ConstNameInput.ConstType;

                if (!string.IsNullOrEmpty(ConstNameInput.Notes))
                    ConstHelper.Notes = ConstNameInput.Notes;

                if (!string.IsNullOrEmpty(ConstNameInput.CaseNumber.ToString()))
                    ConstHelper.CaseNumber = ConstNameInput.CaseNumber;

                if (!string.IsNullOrEmpty(ConstNameInput.OldOrgNameTypeCode))
                    ConstHelper.OldOrgNameTypeCode = ConstNameInput.OldOrgNameTypeCode;
                if (!string.IsNullOrEmpty(ConstNameInput.OldSourceSystemCode))
                    ConstHelper.OldSourceSystemCode = ConstNameInput.OldSourceSystemCode;
                if (!string.IsNullOrEmpty(ConstNameInput.OldOrgNameBestLOSInd.ToString()))
                    ConstHelper.OldOrgNameBestLOSInd = ConstNameInput.OldOrgNameBestLOSInd;
                else
                    ConstHelper.OldOrgNameBestLOSInd = 0;


                 ConstHelper.OrgName = string.Empty;    
                ConstHelper.OrgNameBestLOS = 0;
                ConstHelper.OrgNameTypeCode = string.Empty;
                ConstHelper.SourceSystemCode = string.Empty;

                int intNumberOfInputParameters = 13;
                List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_transaction_key" };

                strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_loc_dtl_org_nm", intNumberOfInputParameters, listOutputParameters);

                var ParamObjects = new List<object>();
                ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.RequestType, "IN", TdType.VarChar, 100));
                ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.MasterID, "IN", TdType.BigInt, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.UserName, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.ConstType, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.Notes, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.CaseNumber, "IN", TdType.BigInt, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_bk_arc_srcsys_cd", ConstHelper.OldSourceSystemCode, "IN", TdType.VarChar, 10));
                ParamObjects.Add(SPHelper.createTdParameter("i_bk_org_nm_typ_cd", ConstHelper.OldOrgNameTypeCode, "IN", TdType.VarChar, 5));
                ParamObjects.Add(SPHelper.createTdParameter("i_bk_org_nm_best_los_ind", ConstHelper.OldOrgNameBestLOSInd, "IN", TdType.ByteInt, 0));

                ParamObjects.Add(SPHelper.createTdParameter("i_new_org_nm", ConstHelper.OrgName, "IN", TdType.VarChar, 150));
                ParamObjects.Add(SPHelper.createTdParameter("i_new_arc_srcsys_cd", ConstHelper.SourceSystemCode, "IN", TdType.VarChar, 10));
                ParamObjects.Add(SPHelper.createTdParameter("i_new_org_nm_typ_cd", ConstHelper.OrgNameTypeCode, "IN", TdType.VarChar, 5));
                ParamObjects.Add(SPHelper.createTdParameter("i_new_org_nm_best_los_ind", ConstHelper.OrgNameBestLOS, "IN", TdType.ByteInt, 0));

                parameters = ParamObjects;

                return ConstNameInput;
            }
        }
    }

}