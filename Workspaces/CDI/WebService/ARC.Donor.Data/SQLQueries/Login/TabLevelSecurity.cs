using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Login;
using Teradata.Client.Provider;
using ARC.Donor.Data.Entities;

namespace ARC.Donor.Data.SQL.Login
{
    class UserTabLevelSecurity
    {
        public static CrudOperationOutput addUserTabLevelSecurityParameters(ARC.Donor.Data.Entities.Login.UserTabLevelSecurity UserTabLevelSecurity)
        {
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 20;
            List<string> listOutputParameters = new List<string> { "o_transOutput" };

            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_usr_prfl", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", UserTabLevelSecurity.usr_nm, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_group_name", UserTabLevelSecurity.grp_nm, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_email_address", UserTabLevelSecurity.email_address, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_telephone_number", UserTabLevelSecurity.telephone_number, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_constituent_tb_access", UserTabLevelSecurity.constituent_tb_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_account_tb_access", UserTabLevelSecurity.account_tb_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_transaction_tb_access", UserTabLevelSecurity.transaction_tb_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_tb_access", UserTabLevelSecurity.case_tb_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_admin_tb_access", UserTabLevelSecurity.admin_tb_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_enterprise_orgs_tb_access", UserTabLevelSecurity.enterprise_orgs_tb_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_reference_data_tb_access", UserTabLevelSecurity.reference_data_tb_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_upload_tb_access", UserTabLevelSecurity.upload_tb_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_report_tb_access", UserTabLevelSecurity.report_tb_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_utitlity_tb_access", UserTabLevelSecurity.utitlity_tb_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_locator_tab_access", UserTabLevelSecurity.locator_tab_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_help_tb_access", UserTabLevelSecurity.help_tb_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_domn_corctn_tb_access", UserTabLevelSecurity.domn_corctn_access, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_has_merge_unmerge_access", UserTabLevelSecurity.has_merge_unmerge_access, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_is_approver", UserTabLevelSecurity.is_approver, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_action", "Insert", "IN", TdType.VarChar, 100));

            crudOutput.parameters = ParamObjects;

            return crudOutput;

        }
    }
}
