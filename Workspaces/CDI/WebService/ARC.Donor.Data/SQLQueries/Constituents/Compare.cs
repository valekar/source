using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class Compare
    {
        /* Method to frame the query to retrive the master level details from cnst_mstr */
        public static string getMasterSQL(List<string> listMasterIds)
        {
            string strCommaSeparatedMasterIds = string.Empty;
            foreach (string s in listMasterIds)
            {
                strCommaSeparatedMasterIds = strCommaSeparatedMasterIds == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedMasterIds + ", " + s + "";
            }
            string strCompareDetailsQuery = string.Empty;
            strCompareDetailsQuery += " select cnst_mstr_id,cnst_dsp_id from arc_mdm_vws.bz_cnst_mstr where cnst_mstr_id in (" + strCommaSeparatedMasterIds + ") ;";
            return strCompareDetailsQuery;
        }

        /* Method to frame the query to retrive the master level person name details from cnst_prsn_nm */
        public static string getMasterPersonNameSQL(List<string> listMasterIds)
        {
            string strCommaSeparatedMasterIds = string.Empty;
            foreach (string s in listMasterIds)
            {
                strCommaSeparatedMasterIds = strCommaSeparatedMasterIds == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedMasterIds + ", " + s + "";
            }
            string strCompareDetailsQuery = string.Empty;
            strCompareDetailsQuery += " select distinct cnst_mstr_id, cnst_prsn_full_nm as name, arc_srcsys_cd from arc_mdm_vws.bzal_cnst_prsn_nm where cnst_mstr_id in (" + strCommaSeparatedMasterIds + ") ;";
            return strCompareDetailsQuery;
        }

        /* Method to frame the query to retrive the master level organization name details from cnst_org_nm */
        public static string getMasterOrgNameSQL(List<string> listMasterIds)
        {
            string strCommaSeparatedMasterIds = string.Empty;
            foreach (string s in listMasterIds)
            {
                strCommaSeparatedMasterIds = strCommaSeparatedMasterIds == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedMasterIds + ", " + s + "";
            }
            string strCompareDetailsQuery = string.Empty;
            strCompareDetailsQuery += " select distinct cnst_mstr_id, cnst_org_nm as name, arc_srcsys_cd from arc_mdm_vws.bzal_cnst_org_nm where cnst_mstr_id in (" + strCommaSeparatedMasterIds + ") ;";
            return strCompareDetailsQuery;
        }

        /* Method to frame the query to retrive the master level address details from cnst_addr */
        public static string getMasterAddressSQL(List<string> listMasterIds)
        {
            string strCommaSeparatedMasterIds = string.Empty;
            foreach (string s in listMasterIds)
            {
                strCommaSeparatedMasterIds = strCommaSeparatedMasterIds == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedMasterIds + ", " + s + "";
            }
            string strCompareDetailsQuery = string.Empty;
            strCompareDetailsQuery += " select distinct cnst_mstr_id, cnst_addr_line1_addr,cnst_addr_line2_addr,cnst_addr_city_nm,cnst_addr_state_cd,cnst_addr_zip_5_cd, arc_srcsys_cd from arc_mdm_vws.bzal_cnst_addr where cnst_mstr_id in (" + strCommaSeparatedMasterIds + ") ;";
            return strCompareDetailsQuery;
        }

        /* Method to frame the query to retrive the master level phone details from cnst_phn_typ2 */
        public static string getMasterPhoneSQL(List<string> listMasterIds)
        {
            string strCommaSeparatedMasterIds = string.Empty;
            foreach (string s in listMasterIds)
            {
                strCommaSeparatedMasterIds = strCommaSeparatedMasterIds == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedMasterIds + ", " + s + "";
            }
            string strCompareDetailsQuery = string.Empty;
            strCompareDetailsQuery += " select distinct cnst_mstr_id, cnst_phn_num, arc_srcsys_cd from arc_mdm_vws.bzal_cnst_phn_typ2 where cnst_mstr_id in (" + strCommaSeparatedMasterIds + ") ;";
            return strCompareDetailsQuery;
        }

        /* Method to frame the query to retrive the master level email details from cnst_email_typ2 */
        public static string getMasterEmailSQL(List<string> listMasterIds)
        {
            string strCommaSeparatedMasterIds = string.Empty;
            foreach (string s in listMasterIds)
            {
                strCommaSeparatedMasterIds = strCommaSeparatedMasterIds == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedMasterIds + ", " + s + "";
            }
            string strCompareDetailsQuery = string.Empty;
            strCompareDetailsQuery += " select distinct cnst_mstr_id, cnst_email_addr, arc_srcsys_cd from arc_mdm_vws.bzal_cnst_email_typ2 where cnst_mstr_id in (" + strCommaSeparatedMasterIds + ") ;";
            return strCompareDetailsQuery;
        }

        /* Method to frame the query to retrive the master level bridge count details from cnst_mstr_external_brid */
        public static string getMasterBridgeCountSQL(List<string> listMasterIds)
        {
            string strCommaSeparatedMasterIds = string.Empty;
            foreach (string s in listMasterIds)
            {
                strCommaSeparatedMasterIds = strCommaSeparatedMasterIds == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedMasterIds + ", " + s + "";
            }
            string strCompareDetailsQuery = string.Empty;
            strCompareDetailsQuery += " select cnst_mstr_external_brid.cnst_mstr_id, cnst_mstr_external_brid.arc_srcsys_cd, count(*) as counter from arc_mdm_vws.bzal_cnst_mstr_external_brid cnst_mstr_external_brid where cnst_mstr_external_brid.row_stat_cd <> 'L' and cnst_mstr_external_brid.cnst_mstr_id in (" + strCommaSeparatedMasterIds + ") group by cnst_mstr_external_brid.cnst_mstr_id, cnst_mstr_external_brid.arc_srcsys_cd;";
            return strCompareDetailsQuery;
        }

        /* Method to frame the query to retrive the master level birth details from cnst_birth */
        public static string getMasterBirthSQL(List<string> listMasterIds)
        {
            string strCommaSeparatedMasterIds = string.Empty;
            foreach (string s in listMasterIds)
            {
                strCommaSeparatedMasterIds = strCommaSeparatedMasterIds == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedMasterIds + ", " + s + "";
            }
            string strCompareDetailsQuery = string.Empty;
            strCompareDetailsQuery += " select distinct cnst_mstr_id, cnst_birth_mth_num, cnst_birth_dy_num, cnst_birth_yr_num, arc_srcsys_cd from arc_mdm_vws.bzal_cnst_birth where cnst_mstr_id in (" + strCommaSeparatedMasterIds + ") ;";
            return strCompareDetailsQuery;
        }

        /* Method to frame the query to retrive the master level death details from cnst_death */
        public static string getMasterDeathSQL(List<string> listMasterIds)
        {
            string strCommaSeparatedMasterIds = string.Empty;
            foreach (string s in listMasterIds)
            {
                strCommaSeparatedMasterIds = strCommaSeparatedMasterIds == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedMasterIds + ", " + s + "";
            }
            string strCompareDetailsQuery = string.Empty;
            strCompareDetailsQuery += " select distinct cnst_mstr_id, cnst_death_dt as death_date, arc_srcsys_cd from arc_mdm_vws.bzal_cnst_death where cnst_mstr_id in (" + strCommaSeparatedMasterIds + ") ;";
            return strCompareDetailsQuery;
        }
    }

    //Class for Master Merge API
    public class MergeMasters
    {
        public static void getMasterMergeParameters(ARC.Donor.Data.Entities.Constituents.MergeInput MergeInput, out string strSPQuery, out List<object> parameters)
        {
            int intNumberOfInputParameters = 6;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_merge", intNumberOfInputParameters, listOutputParameters);

            string strCommaSeparatedMasterIds = string.Empty;
            foreach (string s in MergeInput.MasterIds)
            {
                strCommaSeparatedMasterIds = strCommaSeparatedMasterIds == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedMasterIds + ", " + s + "";
            }

            string strMasterId = string.Empty;
            string strNotes = string.Empty;
            string strCaseNumber = "0";
            string strUserName = string.Empty;
            string strConstituentType = string.Empty;
            string strPreferredMasterIdForLn = string.Empty;

            if (!string.IsNullOrEmpty(strCommaSeparatedMasterIds))
                strMasterId = strCommaSeparatedMasterIds;
            if (!string.IsNullOrEmpty(MergeInput.Notes))
                strNotes = MergeInput.Notes;
            if (!string.IsNullOrEmpty(MergeInput.CaseNumber))
                strCaseNumber = MergeInput.CaseNumber;
            if (!string.IsNullOrEmpty(MergeInput.UserName))
                strUserName = MergeInput.UserName;
            if (!string.IsNullOrEmpty(MergeInput.ConstituentType))
                strConstituentType = MergeInput.ConstituentType;
            if (!string.IsNullOrEmpty(MergeInput.PreferredMasterIdForLn))
                strPreferredMasterIdForLn = MergeInput.PreferredMasterIdForLn;

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("csv_mstr_id", strMasterId, "IN", TdType.VarChar, 500));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", strNotes, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", strCaseNumber, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", strUserName, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", strConstituentType, "IN", TdType.Char, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_prsv_cnst_mstr_id", strPreferredMasterIdForLn, "IN", TdType.Char, 15));

            parameters = ParamObjects;
        }

        public static void getMergeConflictsParameters(ARC.Donor.Data.Entities.Constituents.MergeConflictInput MergeInput, out string strSPQuery, out List<object> parameters)
        {
            int intNumberOfInputParameters = 8;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_merge_for_conflicts", intNumberOfInputParameters, listOutputParameters);

            string strCommaSeparatedMasterIds = string.Empty;
            foreach (string s in MergeInput.MasterIds)
            {
                strCommaSeparatedMasterIds = strCommaSeparatedMasterIds == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedMasterIds + ", " + s + "";
            }

            string strMasterId = string.Empty;
            string strNotes = string.Empty;
            string strCaseNumber = "0";
            string strUserName = string.Empty;
            string strConstituentType = string.Empty;
            string strTrustedSource = string.Empty;
            string strInternalSourceSystemGroupId = string.Empty;
            string strPreferredMasterIdForLn = string.Empty;

            if (!string.IsNullOrEmpty(strCommaSeparatedMasterIds))
                strMasterId = strCommaSeparatedMasterIds;
            if (!string.IsNullOrEmpty(MergeInput.Notes))
                strNotes = MergeInput.Notes;
            if (!string.IsNullOrEmpty(MergeInput.CaseNumber))
                strCaseNumber = MergeInput.CaseNumber;
            if (!string.IsNullOrEmpty(MergeInput.UserName))
                strUserName = MergeInput.UserName;
            if (!string.IsNullOrEmpty(MergeInput.ConstituentType))
                strConstituentType = MergeInput.ConstituentType;
            if (!string.IsNullOrEmpty(MergeInput.TrustedSource))
                strTrustedSource = MergeInput.TrustedSource;
            if (!string.IsNullOrEmpty(MergeInput.InternalSourceSystemGroupId))
                strInternalSourceSystemGroupId = MergeInput.InternalSourceSystemGroupId;
            if (!string.IsNullOrEmpty(MergeInput.PreferredMasterIdForLn))
                strPreferredMasterIdForLn = MergeInput.PreferredMasterIdForLn;

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_intnl_srcsys_grp_id", strInternalSourceSystemGroupId, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_trusted_source", strTrustedSource, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_csv_mstr_id", strMasterId, "IN", TdType.VarChar, 500));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", strNotes, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", strCaseNumber, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", strUserName, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", strConstituentType, "IN", TdType.Char, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_prsv_cnst_mstr_id", strPreferredMasterIdForLn, "IN", TdType.Char, 15));

            parameters = ParamObjects;
        }

    }
}
