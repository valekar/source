using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Orgler.Admin
{
     class UserTabLevelSecurity
    {
        public static void getAddUserTabLevelSecurityParameters(ARC.Donor.Data.Entities.Orgler.Admin.UserTabLevelSecurity UserTabLevelSecurity, out string strSPQuery, out List<object> parameters)
        {
            int intNumberOfInputParameters = 17;
            List<string> listOutputParameters = new List<string> { "o_transOutput" };

            strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.orgler_usr_prfl", intNumberOfInputParameters, listOutputParameters);

            //string strHasMergeUnmergeAccess = "0";
            //string strIsApprover = "0";
            //if (UserTabLevelSecurity.has_merge_unmerge_access == "1")
            //{
            //    strHasMergeUnmergeAccess = "1";
            //}
            //if (UserTabLevelSecurity.is_approver == true)
            //{
            //    strIsApprover = "1";
            //}
            var paramObjects = new List<object>();
            paramObjects.Add(SPHelper.createTdParameter("i_user_id", UserTabLevelSecurity.usr_nm, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_group_name", UserTabLevelSecurity.grp_nm, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_email_address", UserTabLevelSecurity.email_address, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_telephone_number", UserTabLevelSecurity.telephone_number, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter(" i_newaccount_tb_access", UserTabLevelSecurity.newaccount_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_i_topaccount_tb_access", UserTabLevelSecurity.topaccount_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_enterprise_orgs_tb_access", UserTabLevelSecurity.enterprise_orgs_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_constituent_tb_access", UserTabLevelSecurity.constituent_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_transaction_tb_access", UserTabLevelSecurity.transaction_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_admin_tb_access", UserTabLevelSecurity.admin_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_help_tb_access", UserTabLevelSecurity.help_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_upload_eosi_tb_access", UserTabLevelSecurity.upload_eosi_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_upload_affil_tb_access", UserTabLevelSecurity.upload_affil_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_upload_eo_tb_access", UserTabLevelSecurity.upload_eo_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_has_merge_unmerge_access", UserTabLevelSecurity.has_merge_unmerge_access, "IN", TdType.BigInt, 10));
            paramObjects.Add(SPHelper.createTdParameter("i_is_approver", UserTabLevelSecurity.is_approver, "IN", TdType.BigInt, 10));
            paramObjects.Add(SPHelper.createTdParameter("i_action", "Insert", "IN", TdType.VarChar, 100));

            parameters = paramObjects;

        }
        public static CrudOperationOutput insertUserProfileDetailsSQL(ARC.Donor.Data.Entities.Orgler.Admin.UserTabLevelSecurity userProfileInput)
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
            //else userProfileInput.has_merge_unmerge_access = "0";
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
            paramObjects.Add(SPHelper.createTdParameter("i_action", "Insert", "IN", TdType.VarChar, 100));

            crudOutput.parameters = paramObjects;
            return crudOutput;


        }
    }
}
