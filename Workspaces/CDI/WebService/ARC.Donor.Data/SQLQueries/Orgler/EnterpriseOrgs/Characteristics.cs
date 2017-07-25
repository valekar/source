using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Orgler.EnterpriseOrgs
{
    public class Characteristics
    {

        public static string getOrgAllCharacteristicsSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry_2, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }
        static readonly string Qry_2 = @"Select * from arc_mdm_vws.bzal_cnst_chrctrstc where cnst_mstr_id = {2} ";

        public static string getOrgCharacteristicsSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry_1, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        

        static readonly string Qry_1 = @"SELECT *
        FROM arc_orgler_vws.ent_org_dtl_chrctrstc
        WHERE ent_org_id = {2}  
        ORDER  by transaction_key;";

        public static CrudOperationOutput addCharacteristicsParameters(OrgCharacteristicsInput orgCharInput)
        {
            string requestType = "insert";
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 8;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.ent_org_loc_dtl_chrctrstc", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_id", orgCharInput.EntOrgID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", orgCharInput.UserName, "IN", TdType.VarChar, 250));
            //ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", orgCharInput.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", orgCharInput.Notes, "IN", TdType.VarChar, 250));
            //ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", orgCharInput.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_val", orgCharInput.OldCharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_typ_cd", orgCharInput.OldCharacteristicTypeCode, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_val", orgCharInput.CharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_typ_cd", orgCharInput.CharacteristicTypeCode, "IN", TdType.VarChar, 20));

            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }


        public static CrudOperationOutput editCharacteristicsParameters(OrgCharacteristicsInput orgCharInput)
        {
            string requestType = "update";
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 8;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.ent_org_loc_dtl_chrctrstc", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_id", orgCharInput.EntOrgID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", orgCharInput.UserName, "IN", TdType.VarChar, 250));
            //ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", orgCharInput.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", orgCharInput.Notes, "IN", TdType.VarChar, 250));
            //ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", orgCharInput.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_val", orgCharInput.OldCharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_typ_cd", orgCharInput.OldCharacteristicTypeCode, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_val", orgCharInput.CharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_typ_cd", orgCharInput.CharacteristicTypeCode, "IN", TdType.VarChar, 20));
            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }


        public static CrudOperationOutput deleteCharacteristicsParameters(OrgCharacteristicsInput orgCharInput)
        {
            string requestType = "delete";
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 8;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.ent_org_loc_dtl_chrctrstc", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_id", orgCharInput.EntOrgID, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", orgCharInput.UserName, "IN", TdType.VarChar, 250));
            //ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", orgCharInput.ConstType, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", orgCharInput.Notes, "IN", TdType.VarChar, 250));
            //ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", orgCharInput.CaseNumber, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_val", orgCharInput.OldCharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_bk_chrctrstc_typ_cd", orgCharInput.OldCharacteristicTypeCode, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_val", orgCharInput.CharacteristicValue, "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_new_chrctrstc_typ_cd", orgCharInput.CharacteristicTypeCode, "IN", TdType.VarChar, 20));
            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }

    }
  
}
