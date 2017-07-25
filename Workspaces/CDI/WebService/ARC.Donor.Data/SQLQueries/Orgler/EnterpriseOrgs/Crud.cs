using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using Teradata.Client.Provider;
using ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs;

namespace ARC.Donor.Data.SQL.Orgler.EnterpriseOrgs
{

    public class Crud
    {
        public static string getEnterpriseOrgSQL(int NoOfRecords, int PageNumber, string ent_org_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Convert.ToInt64(ent_org_id))
                     /*(((PageNumber - 1) * int.Parse(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt32(NoOfRecords)).ToString() */
                     );
        }
        static readonly string Qry = @"SELECT ent_org_id, ent_org_name, ent_org_src_cd, nk_ent_org_id,
		trans_key, created_by, cast(created_at as timestamp(0)) as created_at, last_modified_by,
		last_modified_at, last_modified_by_all, last_modified_at_all,		
		last_reviewed_by, last_reviewed_at, data_stwrd_usr,
		dw_srcsys_trans_ts, row_stat_cd, load_id	
        FROM arc_orgler_vws.bz_ent_org_srch
        WHERE ent_org_id = {2}";

        public static CreateEnterpriseOrgInputModel getCreateEnterpriseOrgParameters(ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.CreateEnterpriseOrgInputModel EntOrgInput, out string strSPQuery, out List<object> parameters)
        {
            CreateEnterpriseOrgInputModel EntOrgHelper = new CreateEnterpriseOrgInputModel();

            
            if (!string.IsNullOrEmpty(EntOrgInput.ent_org_name.ToString()))
                EntOrgHelper.ent_org_name = EntOrgInput.ent_org_name;
            if (!string.IsNullOrEmpty(EntOrgInput.ent_org_src_cd))
                EntOrgHelper.ent_org_src_cd = EntOrgInput.ent_org_src_cd;
            if (!string.IsNullOrEmpty(EntOrgInput.nk_ent_org_id))
                EntOrgHelper.nk_ent_org_id = EntOrgInput.nk_ent_org_id;
            if (!string.IsNullOrEmpty(EntOrgInput.user_id.ToString()))
                EntOrgHelper.user_id = EntOrgInput.user_id;
            if (!string.IsNullOrEmpty(EntOrgInput.ent_org_dsc))
                EntOrgHelper.ent_org_dsc = EntOrgInput.ent_org_dsc;

            int intNumberOfInputParameters = 5;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_ent_org_id" };
            strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.create_ent_org", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_name", EntOrgHelper.ent_org_name, "IN", TdType.VarChar, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_dsc", EntOrgHelper.ent_org_dsc, "IN", TdType.VarChar, 2000));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_src_cd", EntOrgHelper.ent_org_src_cd, "IN", TdType.VarChar, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_nk_ent_org_id", EntOrgHelper.nk_ent_org_id, "IN", TdType.VarChar, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", EntOrgHelper.user_id, "IN", TdType.VarChar, 20));
           
            parameters = ParamObjects;
            return EntOrgHelper;

        }
        public static EditEnterpriseOrgInputModel getUpdateEnterpriseOrgParameters(ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.EditEnterpriseOrgInputModel EntOrgInput, out string strSPQuery, out List<object> parameters)
        {

            EditEnterpriseOrgInputModel EntOrgHelper = new EditEnterpriseOrgInputModel();
            if (!string.IsNullOrEmpty(EntOrgInput.ent_org_name.ToString()))
                EntOrgHelper.ent_org_name = EntOrgInput.ent_org_name;
            if (!string.IsNullOrEmpty(EntOrgInput.ent_org_src_cd))
                EntOrgHelper.ent_org_src_cd = EntOrgInput.ent_org_src_cd;
            if (!string.IsNullOrEmpty(EntOrgInput.nk_ent_org_id))
                EntOrgHelper.nk_ent_org_id = EntOrgInput.nk_ent_org_id;
            if (!string.IsNullOrEmpty(EntOrgInput.user_id))
                EntOrgHelper.user_id = EntOrgInput.user_id;
            if (!string.IsNullOrEmpty(EntOrgInput.ent_org_id.ToString()))
                EntOrgHelper.ent_org_id = EntOrgInput.ent_org_id;
            if (!string.IsNullOrEmpty(EntOrgInput.ent_org_dsc))
                EntOrgHelper.ent_org_dsc = EntOrgInput.ent_org_dsc;




            int intNumberOfInputParameters = 6;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_transaction_key" };

            strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.edit_ent_org", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_id", EntOrgHelper.ent_org_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_name", EntOrgHelper.ent_org_name, "IN", TdType.VarChar, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_src_cd", EntOrgHelper.ent_org_src_cd, "IN", TdType.VarChar, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_nk_ent_org_id", EntOrgHelper.nk_ent_org_id, "IN", TdType.VarChar, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_dsc", EntOrgHelper.ent_org_dsc, "IN", TdType.VarChar, 2000));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", EntOrgHelper.user_id, "IN", TdType.VarChar, 20));
            

            parameters = ParamObjects;

            return EntOrgHelper;
        }
        public static DeleteEnterpriseOrgInputModel getDeleteEnterpriseOrgParameters(ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgInputModel EntOrgInput, out string strSPQuery, out List<object> parameters)
        {

            DeleteEnterpriseOrgInputModel EntOrgHelper = new DeleteEnterpriseOrgInputModel();
         
            if (!string.IsNullOrEmpty(EntOrgInput.ent_org_id.ToString()))
                EntOrgHelper.ent_org_id = EntOrgInput.ent_org_id;

            if (!string.IsNullOrEmpty(EntOrgInput.user_id))
                EntOrgHelper.user_id = EntOrgInput.user_id;

            
            int intNumberOfInputParameters = 2;
            List<string> listOutputParameters = new List<string> { "o_outputMessage","o_transaction_key" };

            strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.del_ent_org", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();
           
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_id", EntOrgHelper.ent_org_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", EntOrgHelper.user_id, "IN", TdType.VarChar, 20));

            parameters = ParamObjects;

            return EntOrgHelper;
        }

    }




}
