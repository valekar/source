using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;


namespace ARC.Donor.Data.SQL
{
    public class CaseSQL
    {
        public static string getCaseAdvSearchResultsSQL(ListCaseInputSearchModel listSearchInput)
        {
            string strCaseSearchQuery = " select * from dw_stuart_vws.strx_case ";
            string strWhereClause = " where 1=1 ";

            string strWherePartClause = string.Empty;
            List<CaseInputSearchModel> listCaseInput = new List<CaseInputSearchModel>();
            foreach (CaseInputSearchModel srch in listSearchInput.CaseInputSearchModel)
            {
                string strPartWhereClause = string.Empty;
                if (!string.IsNullOrEmpty(srch.CaseId)) strPartWhereClause = string.IsNullOrEmpty(strPartWhereClause) ? strPartWhereClause += " (case_key = '" + srch.CaseId + "') " : strPartWhereClause += " (case_key = '" + srch.CaseId + "') ";
                if (!string.IsNullOrEmpty(srch.CaseName)) strPartWhereClause = string.IsNullOrEmpty(strPartWhereClause) ? strPartWhereClause += " (case_nm LIKE '%" + srch.CaseName + "%') " : strPartWhereClause += " and (case_nm LIKE '%" + srch.CaseName + "%') ";
                if (!string.IsNullOrEmpty(srch.ReferenceId)) strPartWhereClause = string.IsNullOrEmpty(strPartWhereClause) ? strPartWhereClause += " (ref_id = '" + srch.ReferenceId + "') " : strPartWhereClause += " and (ref_id = '" + srch.ReferenceId + "') ";
                if (!string.IsNullOrEmpty(srch.ReferenceSource)) strPartWhereClause = string.IsNullOrEmpty(strPartWhereClause) ? strPartWhereClause += " (ref_src_key = '" + srch.ReferenceSource + "') " : strPartWhereClause += " and (ref_src_key = '" + srch.ReferenceSource + "') ";
                if (!string.IsNullOrEmpty(srch.CaseType)) strPartWhereClause = string.IsNullOrEmpty(strPartWhereClause) ? strPartWhereClause += " (typ_key = '" + srch.CaseType + "') " : strPartWhereClause += " and (typ_key = '" + srch.CaseType + "') ";
                if (!string.IsNullOrEmpty(srch.CaseStatus)) strPartWhereClause = string.IsNullOrEmpty(strPartWhereClause) ? strPartWhereClause += " (status = '" + srch.CaseStatus + "') " : strPartWhereClause += " and (status = '" + srch.CaseStatus + "') ";
                if (!string.IsNullOrEmpty(srch.ConstituentName)) strPartWhereClause = string.IsNullOrEmpty(strPartWhereClause) ? strPartWhereClause += " (cnst_nm LIKE '%" + srch.ConstituentName + "%') " : strPartWhereClause += " and (cnst_nm LIKE '%" + srch.ConstituentName + "%') ";
                if (!string.IsNullOrEmpty(srch.UserName)) strPartWhereClause = string.IsNullOrEmpty(strPartWhereClause) ? strPartWhereClause += " ( crtd_by_usr_id = '" + srch.UserName + "') " : strPartWhereClause += " and (crtd_by_usr_id = '" + srch.UserName + "') ";
                if (!string.IsNullOrEmpty(srch.ReportedDateFrom)) strPartWhereClause = string.IsNullOrEmpty(strPartWhereClause) ? strPartWhereClause += " (report_dt >= '" + srch.ReportedDateFrom + "' (DATE, FORMAT 'mm/dd/yyyy')) " : strPartWhereClause += " and (report_dt >= '" + srch.ReportedDateFrom + "' (DATE, FORMAT 'mm/dd/yyyy')) ";
                if (!string.IsNullOrEmpty(srch.ReportedDateTo)) strPartWhereClause = string.IsNullOrEmpty(strPartWhereClause) ? strPartWhereClause += " (report_dt <= '" + srch.ReportedDateTo + "' (DATE, FORMAT 'mm/dd/yyyy')) " : strPartWhereClause += " and (report_dt <= '" + srch.ReportedDateTo + "' (DATE, FORMAT 'mm/dd/yyyy')) ";
                if (!string.IsNullOrEmpty(srch.UserId)) strPartWhereClause = string.IsNullOrEmpty(strPartWhereClause) ? strPartWhereClause += " (crtd_by_usr_id = '" + srch.UserId + "') " : strPartWhereClause += " and (crtd_by_usr_id = '" + srch.UserId + "') ";
                if (string.IsNullOrEmpty(strWherePartClause))
                {
                    strWherePartClause = "( " + strPartWhereClause + ")";
                }
                else
                {
                    strWherePartClause = strWherePartClause + " or ( " + strPartWhereClause + ")";
                }
             

            }
            if (strWherePartClause != string.Empty)
                strWhereClause += " and ( " + strWherePartClause + " ) and row_stat_cd <> 'L' ";

            string strPartitionByClause = " qualify row_number() over (partition by case_key order by create_ts desc) <=1";
            string strOrderByClause = " order by create_ts desc";
            //add where clause to the transaction query
            strCaseSearchQuery += strWhereClause + strPartitionByClause; // +strOrderByClause;

            strCaseSearchQuery = " select top 100 * from ( " + strCaseSearchQuery + " ) A " + strOrderByClause;
            //else
            //{
            //    strCaseSearchQuery = strCaseSearchQuery + " where 1 <> 1;";
            //}




            return string.Format(strCaseSearchQuery);
        }

        public static CreateCaseInput getCreateCaseParameters(ARC.Donor.Data.Entities.Case.CreateCaseInput CreateCaseInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            CreateCaseInput ConstHelper = new CreateCaseInput();

            if (!string.IsNullOrEmpty(CreateCaseInput.case_nm))
                ConstHelper.case_nm = CreateCaseInput.case_nm;

            if (!string.IsNullOrEmpty(CreateCaseInput.case_desc))
                ConstHelper.case_desc = CreateCaseInput.case_desc;

            if (!string.IsNullOrEmpty(CreateCaseInput.ref_src_desc))
                ConstHelper.ref_src_desc = CreateCaseInput.ref_src_desc;

            if (!string.IsNullOrEmpty(CreateCaseInput.ref_id))
                ConstHelper.ref_id = CreateCaseInput.ref_id;

            if (!string.IsNullOrEmpty(CreateCaseInput.typ_key_desc))
                ConstHelper.typ_key_desc = CreateCaseInput.typ_key_desc;

            if (!string.IsNullOrEmpty(CreateCaseInput.intake_chan_desc))
                ConstHelper.intake_chan_desc = CreateCaseInput.intake_chan_desc;

            if (!string.IsNullOrEmpty(CreateCaseInput.intake_owner_dept_desc))
                ConstHelper.intake_owner_dept_desc = CreateCaseInput.intake_owner_dept_desc;

            if (!string.IsNullOrEmpty(CreateCaseInput.cnst_nm))
                ConstHelper.cnst_nm = CreateCaseInput.cnst_nm;

            if (!string.IsNullOrEmpty(CreateCaseInput.crtd_by_usr_id))
                ConstHelper.crtd_by_usr_id = CreateCaseInput.crtd_by_usr_id;

            if (!string.IsNullOrEmpty(CreateCaseInput.status))
                ConstHelper.status = CreateCaseInput.status;

            if (!string.IsNullOrEmpty(CreateCaseInput.report_dt))
            {
                ConstHelper.report_dt = CreateCaseInput.report_dt;
            }
            else
            {
                ConstHelper.report_dt = null;
            }

            if (!string.IsNullOrEmpty(CreateCaseInput.attchmnt_url))
                ConstHelper.attchmnt_url = CreateCaseInput.attchmnt_url;

            int intNumberOfInputParameters = 17;
            List<string> listOutputParameters = new List<string> { "o_case_seq", "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_case", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_case_nm", ConstHelper.case_nm, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_desc", ConstHelper.case_desc, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_ref_src_dsc", ConstHelper.ref_src_desc, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_ref_id", ConstHelper.ref_id, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_typ_key_dsc", ConstHelper.typ_key_desc, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_intake_chan_desc", ConstHelper.intake_chan_desc, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_intake_owner_dept_desc", ConstHelper.intake_owner_dept_desc, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_nm", ConstHelper.cnst_nm, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_crtd_by_usr_id", ConstHelper.crtd_by_usr_id, "IN", TdType.VarChar, 100));

            ParamObjects.Add(SPHelper.createTdParameter("i_status", ConstHelper.status, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_report_dt", ConstHelper.report_dt, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_attchmnt_url", ConstHelper.attchmnt_url, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref1", null, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref2", null, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref3", null, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref4", null, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref5", null, "IN", TdType.VarChar, 1200));
            parameters = ParamObjects;

            return ConstHelper;
        }

        public static SaveCaseSearchInput getSaveCaseSearchParameters(ARC.Donor.Data.Entities.Case.SaveCaseSearchInput SaveCaseSearchInput, Int64? case_key, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            SaveCaseSearchInput ConstHelper = new SaveCaseSearchInput();

            string partSQLClause = "";

            if (!string.IsNullOrEmpty(SaveCaseSearchInput.first_name))
                partSQLClause += "first_name|" + SaveCaseSearchInput.first_name.Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.last_name))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "last_name|" + SaveCaseSearchInput.last_name.Trim() : partSQLClause += ",last_name|" + SaveCaseSearchInput.last_name.Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.address_line))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "address_line|" + SaveCaseSearchInput.address_line.Trim() : partSQLClause += ",address_line|" + SaveCaseSearchInput.address_line.Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.city))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "city|" + SaveCaseSearchInput.city.Trim() : partSQLClause += ",city|" + SaveCaseSearchInput.city.Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.state))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "state|" + SaveCaseSearchInput.state.Trim() : partSQLClause += ",state|" + SaveCaseSearchInput.state.Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.zip))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "zip|" + SaveCaseSearchInput.zip.Trim() : partSQLClause += ",zip|" + SaveCaseSearchInput.zip.Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.phone_number))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "phone_number|" + SaveCaseSearchInput.phone_number.Trim() : partSQLClause += ",phone_number|" + SaveCaseSearchInput.phone_number.Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.email_address))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "email_address|" + SaveCaseSearchInput.email_address.Trim() : partSQLClause += ",email_address|" + SaveCaseSearchInput.email_address.Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.master_id.ToString()))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "master_id|" + SaveCaseSearchInput.master_id.ToString().Trim() : partSQLClause += ",master_id|" + SaveCaseSearchInput.master_id.ToString().Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.source_system))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "source_system|" + SaveCaseSearchInput.source_system.Trim() : partSQLClause += ",source_system|" + SaveCaseSearchInput.source_system.Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.chapter_source_system))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "chapter_source_system|" + SaveCaseSearchInput.chapter_source_system.Trim() : partSQLClause += ",chapter_source_system|" + SaveCaseSearchInput.chapter_source_system.Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.source_system_id))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "source_system_id|" + SaveCaseSearchInput.source_system_id.Trim() : partSQLClause += ",source_system_id|" + SaveCaseSearchInput.source_system_id.Trim();
            if (!string.IsNullOrEmpty(SaveCaseSearchInput.constituent_type))
                partSQLClause = string.IsNullOrEmpty(partSQLClause) ? partSQLClause += "constituent_type|" + SaveCaseSearchInput.constituent_type.Trim() : partSQLClause += ",constituent_type|" + SaveCaseSearchInput.constituent_type.Trim();

            string strUserName = "StuartAdmin";
            string i_search1 = null, i_search2 = null, i_search3 = null, i_search4 = null, i_search5 = null;
            string strSearchType = "Case";

            if (!string.IsNullOrEmpty(partSQLClause))
                i_search1 = partSQLClause;

            int intNumberOfInputParameters = 8;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_save_srch", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_search1", i_search1, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_search2", i_search2, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_search3", i_search3, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_search4", i_search4, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_search5", i_search5, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_search_type", strSearchType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", strUserName, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_key", case_key, "IN", TdType.VarChar, 100));
            parameters = ParamObjects;

            return ConstHelper;
        }

        /* Method to frame the query to retrive the case transaction details */
        public static string getCaseTransactionDetailsSQL(int caseId)
        {
            string strCaseDetailsQuery = " select TOP 50 * from dw_stuart_vws.strx_case_trans where case_seq = " + caseId + ";";
            return strCaseDetailsQuery;
        }

        /* Method to frame the query to retrive the case details */
        public static string getCasePreferenceDetailsSQL(int caseId)
        {
            string strCaseDetailsQuery = " select * from dw_stuart_vws.strx_case_prefc where case_id = " + caseId + ";";
            return strCaseDetailsQuery;
        }

        /* Method to frame the query to retrive the case locator details */
        public static string getCaseLocatorDetailsSQL(int caseId)
        {
            string strCaseDetailsQuery = " select * from dw_stuart_vws.strx_srch_criteria where case_key = " + caseId + ";";
            return strCaseDetailsQuery;
        }

        /* Method to frame the query to retrive the case notes details */
        public static string getCaseNotesDetailsSQL(int caseId)
        {
            string strCaseDetailsQuery = " select * from dw_stuart_vws.strx_case_notes_mapping where case_key = " + caseId + ";";
            return strCaseDetailsQuery;
        }

        public static CreateCaseInput getUpdateCaseParameters(ARC.Donor.Data.Entities.Case.CreateCaseInput CreateCaseInput, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            CreateCaseInput ConstHelper = new CreateCaseInput();

            if (!string.IsNullOrEmpty(CreateCaseInput.case_seq.ToString()))
                ConstHelper.case_seq = CreateCaseInput.case_seq;

            if (!string.IsNullOrEmpty(CreateCaseInput.case_nm))
                ConstHelper.case_nm = CreateCaseInput.case_nm;

            if (!string.IsNullOrEmpty(CreateCaseInput.case_desc))
                ConstHelper.case_desc = CreateCaseInput.case_desc;

            if (!string.IsNullOrEmpty(CreateCaseInput.ref_src_desc))
                ConstHelper.ref_src_desc = CreateCaseInput.ref_src_desc;

            if (!string.IsNullOrEmpty(CreateCaseInput.ref_id))
                ConstHelper.ref_id = CreateCaseInput.ref_id;

            if (!string.IsNullOrEmpty(CreateCaseInput.typ_key_desc))
                ConstHelper.typ_key_desc = CreateCaseInput.typ_key_desc;

            if (!string.IsNullOrEmpty(CreateCaseInput.intake_chan_desc))
                ConstHelper.intake_chan_desc = CreateCaseInput.intake_chan_desc;

            if (!string.IsNullOrEmpty(CreateCaseInput.intake_owner_dept_desc))
                ConstHelper.intake_owner_dept_desc = CreateCaseInput.intake_owner_dept_desc;

            if (!string.IsNullOrEmpty(CreateCaseInput.cnst_nm))
                ConstHelper.cnst_nm = CreateCaseInput.cnst_nm;

            if (!string.IsNullOrEmpty(CreateCaseInput.crtd_by_usr_id))
                ConstHelper.crtd_by_usr_id = CreateCaseInput.crtd_by_usr_id;

            if (!string.IsNullOrEmpty(CreateCaseInput.status))
                ConstHelper.status = CreateCaseInput.status;

            if (!string.IsNullOrEmpty(CreateCaseInput.report_dt))
            {
                ConstHelper.report_dt = CreateCaseInput.report_dt;
            }
            else
            {
                ConstHelper.report_dt = null;
            }

            int intNumberOfInputParameters = 12;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_case_update", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq", ConstHelper.case_seq, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_nm", ConstHelper.case_nm, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_desc", ConstHelper.case_desc, "IN", TdType.VarChar, 1000));
            ParamObjects.Add(SPHelper.createTdParameter("i_ref_src_dsc", ConstHelper.ref_src_desc, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_ref_id", ConstHelper.ref_id, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_intake_chan_desc", ConstHelper.intake_chan_desc, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_intake_owner_dept_desc", ConstHelper.intake_owner_dept_desc, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_typ_key_dsc", ConstHelper.typ_key_desc, "IN", TdType.VarChar, 100));


            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_nm", ConstHelper.cnst_nm, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_crtd_by_usr_id", ConstHelper.crtd_by_usr_id, "IN", TdType.VarChar, 100));

            ParamObjects.Add(SPHelper.createTdParameter("i_status", ConstHelper.status, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_report_dt", ConstHelper.report_dt, "IN", TdType.VarChar, 100));
            parameters = ParamObjects;

            return ConstHelper;
        }

        public static DeleteCaseInput getDeleteCaseParameters(ARC.Donor.Data.Entities.Case.DeleteCaseInput DeleteCaseInput, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            DeleteCaseInput ConstHelper = new DeleteCaseInput();

            if (!string.IsNullOrEmpty(DeleteCaseInput.case_nm.ToString()))
                ConstHelper.case_nm = DeleteCaseInput.case_nm;

            int intNumberOfInputParameters = 1;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_del_case", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_case_nm", ConstHelper.case_nm, "IN", TdType.BigInt, 250));

            parameters = ParamObjects;

            return ConstHelper;
        }

        public static CaseNotesInput getAddCaseNotesParameters(ARC.Donor.Data.Entities.Case.CaseNotesInput CaseNotesInput, string request_action, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            CaseNotesInput ConstHelper = new CaseNotesInput();

            if (!string.IsNullOrEmpty(CaseNotesInput.case_id.ToString()))
                ConstHelper.case_id = CaseNotesInput.case_id;

            if (!string.IsNullOrEmpty(CaseNotesInput.notes_id.ToString()))
                ConstHelper.notes_id = CaseNotesInput.notes_id;

            if (!string.IsNullOrEmpty(CaseNotesInput.notes_text))
                ConstHelper.notes_text = CaseNotesInput.notes_text;

            if (!string.IsNullOrEmpty(CaseNotesInput.user_id))
                ConstHelper.user_id = CaseNotesInput.user_id;

            int intNumberOfInputParameters = 5;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_case_notes", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_case_id", ConstHelper.case_id, "IN", TdType.Integer, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes_id", ConstHelper.notes_id, "IN", TdType.Integer, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes_text", ConstHelper.notes_text, "IN", TdType.VarChar, 3000));
            ParamObjects.Add(SPHelper.createTdParameter("i_action", request_action, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", ConstHelper.user_id, "IN", TdType.VarChar, 50));

            parameters = ParamObjects;

            return ConstHelper;
        }

        public static CaseNotesInput getUpdateCaseNotesParameters(ARC.Donor.Data.Entities.Case.CaseNotesInput CaseNotesInput, string request_action, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            CaseNotesInput ConstHelper = new CaseNotesInput();

            if (!string.IsNullOrEmpty(CaseNotesInput.case_id.ToString()))
                ConstHelper.case_id = CaseNotesInput.case_id;

            if (!string.IsNullOrEmpty(CaseNotesInput.notes_id.ToString()))
                ConstHelper.notes_id = CaseNotesInput.notes_id;

            if (!string.IsNullOrEmpty(CaseNotesInput.notes_text))
                ConstHelper.notes_text = CaseNotesInput.notes_text;

            if (!string.IsNullOrEmpty(CaseNotesInput.user_id))
                ConstHelper.user_id = CaseNotesInput.user_id;

            int intNumberOfInputParameters = 5;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_case_notes", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_case_id", ConstHelper.case_id, "IN", TdType.Integer, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes_id", ConstHelper.notes_id, "IN", TdType.Integer, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes_text", ConstHelper.notes_text, "IN", TdType.VarChar, 3000));
            ParamObjects.Add(SPHelper.createTdParameter("i_action", request_action, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", ConstHelper.user_id, "IN", TdType.VarChar, 50));

            parameters = ParamObjects;

            return ConstHelper;
        }

        public static CaseNotesInput getDeleteCaseNotesParameters(ARC.Donor.Data.Entities.Case.CaseNotesInput CaseNotesInput, string request_action, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            CaseNotesInput ConstHelper = new CaseNotesInput();

            if (!string.IsNullOrEmpty(CaseNotesInput.case_id.ToString()))
                ConstHelper.case_id = CaseNotesInput.case_id;

            if (!string.IsNullOrEmpty(CaseNotesInput.notes_id.ToString()))
                ConstHelper.notes_id = CaseNotesInput.notes_id;

            if (!string.IsNullOrEmpty(CaseNotesInput.notes_text))
                ConstHelper.notes_text = CaseNotesInput.notes_text;

            if (!string.IsNullOrEmpty(CaseNotesInput.user_id))
                ConstHelper.user_id = CaseNotesInput.user_id;

            int intNumberOfInputParameters = 5;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_case_notes", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_case_id", ConstHelper.case_id, "IN", TdType.Integer, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes_id", ConstHelper.notes_id, "IN", TdType.Integer, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes_text", ConstHelper.notes_text, "IN", TdType.VarChar, 3000));
            ParamObjects.Add(SPHelper.createTdParameter("i_action", request_action, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", ConstHelper.user_id, "IN", TdType.VarChar, 50));

            parameters = ParamObjects;

            return ConstHelper;
        }

        /*Add,Update and Delete write parameters for case locator*/
        public static CaseLocatorInput getWriteCaseLocatorParameters(ARC.Donor.Data.Entities.Case.CaseLocatorInput CaseLocatorInput, string request_action, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            CaseLocatorInput ConstHelper = new CaseLocatorInput();

            if (!string.IsNullOrEmpty(CaseLocatorInput.case_key.ToString()))
                ConstHelper.case_key = CaseLocatorInput.case_key;

            if (!string.IsNullOrEmpty(CaseLocatorInput.locator_id.ToString()))
                ConstHelper.locator_id = CaseLocatorInput.locator_id;

            if (!string.IsNullOrEmpty(CaseLocatorInput.usr_nm))
                ConstHelper.usr_nm = CaseLocatorInput.usr_nm;

            if (!string.IsNullOrEmpty(CaseLocatorInput.locator_upd))
                ConstHelper.locator_upd = CaseLocatorInput.locator_upd;

            if (!string.IsNullOrEmpty(request_action))
                ConstHelper.req_typ = request_action;

            int intNumberOfInputParameters = 5;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_case_locator", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.req_typ, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_key", ConstHelper.case_key, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_locator_id", ConstHelper.locator_id, "IN", TdType.Integer, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.usr_nm, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_locator_upd", ConstHelper.locator_upd, "IN", TdType.VarChar, 1200));

            parameters = ParamObjects;

            return ConstHelper;
        }
    }
}
