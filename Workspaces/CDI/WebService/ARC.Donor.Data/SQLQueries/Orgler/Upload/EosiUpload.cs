using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Orgler.Upload;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQLQueries.Orgler.Upload
{
    public class EosiUpload
    {
        public static CrudOperationOutput insertEosiSQL(EosiUploadInput input, string strUserName)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crud = new CrudOperationOutput();

            //populate the count of number of input parameters
            int intNumberOfInputParameters = 26;

            //create a list of all the output parameters
            List<string> listOutputParameters = new List<string> { "o_message" };

            //create the SP query query using the input parameter count and output parameters and populate it to the crud object
            crud.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.sp_ld_eosi_upld", intNumberOfInputParameters, listOutputParameters);

            //Standardize the column values
            dynamic strEnterpriseOrgId;
            if (string.IsNullOrEmpty(input.strEnterpriseOrgId))
                strEnterpriseOrgId = DBNull.Value;
            else
                strEnterpriseOrgId = input.strEnterpriseOrgId;
            dynamic strMasterId;
            if (string.IsNullOrEmpty(input.strMasterId))
                strMasterId = DBNull.Value;
            else
                strMasterId = input.strMasterId;
            dynamic strSourceId;
            if (string.IsNullOrEmpty(input.strSourceId))
                strSourceId = DBNull.Value;
            else
                strSourceId = input.strSourceId;
            dynamic strSecondarySourceId;
            if (string.IsNullOrEmpty(input.strSecondarySourceId))
                strSecondarySourceId = DBNull.Value;
            else
                strSecondarySourceId = input.strSecondarySourceId;
            dynamic strParentEnterpriseOrgId;
            if (string.IsNullOrEmpty(input.strParentEnterpriseOrgId))
                strParentEnterpriseOrgId = DBNull.Value;
            else
                strParentEnterpriseOrgId = input.strParentEnterpriseOrgId;
            dynamic strAltSourceCd;
            if (string.IsNullOrEmpty(input.strAltSourceCode))
                strAltSourceCd = DBNull.Value;
            else
                strAltSourceCd = input.strAltSourceCode;
            dynamic strAltSourceId;
            if (string.IsNullOrEmpty(input.strAltSourceId))
                strAltSourceId = DBNull.Value;
            else
                strAltSourceId = input.strAltSourceId;

            //create a list of parameters that have to be passed to the procedure
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_stg_eosi_upld_key", input.strSeqKey, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_id", strEnterpriseOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_mstr_id", strMasterId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_arc_srcsys_cd", input.strSourceSystemCode, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_srcsys_id", strSourceId, "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_srcsys_scndry_id", strSecondarySourceId, "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_prnt_ent_org_id", strParentEnterpriseOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_alt_appl_src_cd", strAltSourceCd, "IN", TdType.VarChar, 4));
            ParamObjects.Add(SPHelper.createTdParameter("i_alt_srcsys_cnst_uid", strAltSourceId, "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_org_nm", (!string.IsNullOrEmpty(input.strOrgName) ? input.strOrgName : string.Empty), "IN", TdType.VarChar, 150));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr1_street1", (!string.IsNullOrEmpty(input.strAddress1Street1) ? input.strAddress1Street1 : string.Empty), "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr1_street2", (!string.IsNullOrEmpty(input.strAddress1Street2) ? input.strAddress1Street2 : string.Empty), "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr1_city", (!string.IsNullOrEmpty(input.strAddress1City) ? input.strAddress1City : string.Empty), "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr1_state", (!string.IsNullOrEmpty(input.strAddress1State) ? input.strAddress1State : string.Empty), "IN", TdType.Char, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_addr1_zip", (!string.IsNullOrEmpty(input.strAddress1Zip) ? input.strAddress1Zip : string.Empty), "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_phn1_num", (!string.IsNullOrEmpty(input.strPhone1) ? input.strPhone1 : string.Empty), "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_phn2_num", (!string.IsNullOrEmpty(input.strPhone2) ? input.strPhone2 : string.Empty), "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_naics_cd", (!string.IsNullOrEmpty(input.strNaicsCode) ? input.strNaicsCode : string.Empty), "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_chrctrstc1_typ_cd", (!string.IsNullOrEmpty(input.strCharacteristics1Code) ? input.strCharacteristics1Code : string.Empty), "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_chrctrstc1_val", (!string.IsNullOrEmpty(input.strCharacteristics1Value) ? input.strCharacteristics1Value : string.Empty), "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_chrctrstc2_typ_cd", (!string.IsNullOrEmpty(input.strCharacteristics2Code) ? input.strCharacteristics2Code : string.Empty), "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_chrctrstc2_val", (!string.IsNullOrEmpty(input.strCharacteristics2Value) ? input.strCharacteristics2Value : string.Empty), "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_rm_ind", (!string.IsNullOrEmpty(input.strRMIndicator) ? ((input.strRMIndicator == "0" || input.strRMIndicator == "1") ? input.strRMIndicator : "0") : "0"), "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", (!string.IsNullOrEmpty(input.strNotes) ? input.strNotes : string.Empty), "IN", TdType.VarChar, 1000));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", (!string.IsNullOrEmpty(strUserName) ? strUserName : string.Empty), "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_key", input.strTransKey, "IN", TdType.BigInt, 0));
            
            //populate the parameters to the crud object's parameter property
            crud.parameters = ParamObjects;

            //return the crud object to the calling method
            return crud;
        }

        public static string getMaxSeqKeySQL()
        {
            return "SELECT COALESCE(MAX(stg_eosi_upld_key),0) FROM arc_orgler_vws.bz_eosi_upld;";
        }
    }
}
