using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using Teradata.Client.Provider;
namespace ARC.Donor.Data.SQL.Orgler.Admin
{
    class UserProfile
    {
        static readonly string strUserProfileDetailsQuery = @"SELECT * FROM arc_orgler_tbls.orgler_usr_prfl ; ";
        public static CrudOperationOutput getUserProfileSQL(int NoOfRecords, int PageNumber)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for naics details
            crudOperationsOutput.strSPQuery = strUserProfileDetailsQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
          var ParamObjects = new List<object>();
          //  ParamObjects.Add(SPHelper.createTdParameter("cnst_mstr_id", userprofile., "IN", TdType.BigInt, 100));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        public static CrudOperationOutput insertUserProfileDetailsSQL(ARC.Donor.Data.Entities.Orgler.Admin.UserProfile userProfileInput)
        {

            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 17;
            List<string> listOutputParameters = new List<string> { "o_transOutput" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.orgler_usr_prfl", intNumberOfInputParameters, listOutputParameters);
            var paramObjects = new List<object>();
            string strNaicsCode = string.Empty;
            paramObjects.Add(SPHelper.createTdParameter("i_user_id", userProfileInput.usr_nm, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_group_name", userProfileInput.grp_nm, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_email_address", userProfileInput.email_address, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_telephone_number", userProfileInput.telephone_number, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter(" i_newaccount_tb_access", userProfileInput.newaccount_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_i_topaccount_tb_access", userProfileInput.topaccount_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_enterprise_orgs_tb_access", userProfileInput.enterprise_orgs_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_constituent_tb_access", userProfileInput.constituent_tb_access, "IN", TdType.VarChar, 15));          
            paramObjects.Add(SPHelper.createTdParameter("i_transaction_tb_access", userProfileInput.transaction_tb_access, "IN", TdType.VarChar, 15));            
            paramObjects.Add(SPHelper.createTdParameter("i_admin_tb_access", userProfileInput.admin_tb_access, "IN", TdType.VarChar, 15));          
            paramObjects.Add(SPHelper.createTdParameter("i_help_tb_access", userProfileInput.help_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_upload_eosi_tb_access", userProfileInput.upload_eosi_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_upload_affil_tb_access", userProfileInput.upload_affil_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_upload_eo_tb_access", userProfileInput.upload_eo_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_has_merge_unmerge_access", userProfileInput.has_merge_unmerge_access, "IN", TdType.BigInt, 10));
            paramObjects.Add(SPHelper.createTdParameter("i_is_approver", userProfileInput.is_approver, "IN", TdType.BigInt, 10));
            paramObjects.Add(SPHelper.createTdParameter("i_action","Insert", "IN", TdType.VarChar, 100));

            crudOutput.parameters = paramObjects;
            return crudOutput;


        }
        public static CrudOperationOutput userProfileCRUDSQL(ARC.Donor.Data.Entities.Orgler.Admin.UserProfile userProfileInput,string actionType)
        {

            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 17;
            List<string> listOutputParameters = new List<string> { "o_transOutput" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.orgler_usr_prfl", intNumberOfInputParameters, listOutputParameters);
            var paramObjects = new List<object>();
            //if (userProfileInput.has_merge_unmerge_access == "true")
            //{
            //    userProfileInput.has_merge_unmerge_access = "1";
            //}
            //else
            //    userProfileInput.has_merge_unmerge_access = "0";
            //if (userProfileInput.is_approver == "true")
            //{
            //    userProfileInput.is_approver = "1";
            //}
            paramObjects.Add(SPHelper.createTdParameter("i_user_id", userProfileInput.usr_nm, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_group_name", userProfileInput.grp_nm, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_email_address", userProfileInput.email_address, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_telephone_number", userProfileInput.telephone_number, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter(" i_newaccount_tb_access", userProfileInput.newaccount_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_i_topaccount_tb_access", userProfileInput.topaccount_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_enterprise_orgs_tb_access", userProfileInput.enterprise_orgs_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_constituent_tb_access", userProfileInput.constituent_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_transaction_tb_access", userProfileInput.transaction_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_admin_tb_access", userProfileInput.admin_tb_access, "IN", TdType.VarChar, 15)); 
            paramObjects.Add(SPHelper.createTdParameter("i_help_tb_access", userProfileInput.help_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_upload_eosi_tb_access", userProfileInput.upload_eosi_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_upload_affil_tb_access", userProfileInput.upload_affil_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_upload_eo_tb_access", userProfileInput.upload_eo_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_has_merge_unmerge_access", userProfileInput.has_merge_unmerge_access, "IN", TdType.BigInt, 10));
            paramObjects.Add(SPHelper.createTdParameter("i_is_approver", userProfileInput.is_approver, "IN", TdType.BigInt, 10));
            paramObjects.Add(SPHelper.createTdParameter("i_action", actionType, "IN", TdType.VarChar, 100));

            crudOutput.parameters = paramObjects;
            return crudOutput;


        }
        public static void getLoginHistoryParameters(ARC.Donor.Data.Entities.Orgler.Admin.LoginHistoryInput LoginHistoryInput, out string strSPQuery, out List<object> parameters)
        {
            int intNumberOfInputParameters = 4;
            List<string> listOutputParameters = new List<string>();

            strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.orgler_login_hst", intNumberOfInputParameters, listOutputParameters);

            string strUserName = "<not available>";
            string strActiveDirectoryGroupName = string.Empty;
            string strLoginStatus = string.Empty;
            DateTime dateTime = DateTime.Now;
            TdTimestamp tdTimestamp = new TdTimestamp(dateTime);

            if (!string.IsNullOrEmpty(LoginHistoryInput.UserName))
                strUserName = LoginHistoryInput.UserName;
            if (!string.IsNullOrEmpty(LoginHistoryInput.ActiveDirectoryGroupName))
                strActiveDirectoryGroupName = LoginHistoryInput.ActiveDirectoryGroupName;
            if (!string.IsNullOrEmpty(LoginHistoryInput.LoginStatus))
                strLoginStatus = LoginHistoryInput.LoginStatus;

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("usrNm", strUserName, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("actDrctryGroupNm", strActiveDirectoryGroupName, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("loginAttemptTs", tdTimestamp, "IN", TdType.Timestamp, 3));
            ParamObjects.Add(SPHelper.createTdParameter("loginStat", strLoginStatus, "IN", TdType.VarChar, 20));

            parameters = ParamObjects;
        }
    }
}
