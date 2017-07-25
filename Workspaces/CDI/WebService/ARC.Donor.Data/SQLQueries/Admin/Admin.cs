using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Admin
{
    public class Admin
    {
        public static string getAdminTabSecuritySQL(int NoOfRecords, int PageNumber)
        {
            return string.Format(Qry, NoOfRecords,PageNumber,(((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }
        static readonly string Qry = @"SELECT * FROM dw_stuart_vws.strx_usr_prfl order by usr_nm";


        public static CrudOperationOutput tabLevelSecurityProcParams(ARC.Donor.Data.Entities.Admin.Admin adminInput,string actionType)
        {
            CrudOperationOutput crudOutput = new CrudOperationOutput();

            List<string> listOutputParameters = new List<string> { "o_transOutput" };
            int intNumberOfInputParameters = 20;
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_usr_prfl", intNumberOfInputParameters, listOutputParameters);
            var paramObjects = new List<object>();
            paramObjects.Add(SPHelper.createTdParameter("i_user_id", adminInput.usr_nm, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_group_name", adminInput.grp_nm, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_email_address", adminInput.email_address, "IN", TdType.VarChar, 100));
            paramObjects.Add(SPHelper.createTdParameter("i_telephone_number", adminInput.telephone_number, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_constituent_tb_access", adminInput.constituent_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_account_tb_access", adminInput.account_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_transaction_tb_access", adminInput.transaction_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_case_tb_access", adminInput.case_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_admin_tb_access", adminInput.admin_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_enterprise_orgs_tb_access", adminInput.enterprise_orgs_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_reference_data_tb_access", adminInput.reference_data_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_upload_tb_access", adminInput.upload_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_report_tb_access", adminInput.report_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_utitlity_tb_access", adminInput.utitlity_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_locator_tab_access", adminInput.locator_tab_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_help_tb_access", adminInput.help_tb_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_domn_corctn_tb_access", adminInput.domn_corctn_access, "IN", TdType.VarChar, 15));
            paramObjects.Add(SPHelper.createTdParameter("i_has_merge_unmerge_access", adminInput.has_merge_unmerge_access, "IN", TdType.BigInt, 10));
            paramObjects.Add(SPHelper.createTdParameter("i_is_approver", adminInput.is_approver, "IN", TdType.BigInt, 10));
            paramObjects.Add(SPHelper.createTdParameter("i_action", actionType, "IN", TdType.VarChar, 100));

            crudOutput.parameters = paramObjects;

            return crudOutput;

        }



    }
}
