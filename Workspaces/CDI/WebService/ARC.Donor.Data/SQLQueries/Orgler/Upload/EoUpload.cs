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
    public class EoUpload
    {
        public static CrudOperationOutput insertEoSQL(EoUploadInput input, string strUserName)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crud = new CrudOperationOutput();

            //populate the count of number of input parameters
            int intNumberOfInputParameters = 33;

            //create a list of all the output parameters
            List<string> listOutputParameters = new List<string> { "o_message" };

            //create the SP query query using the input parameter count and output parameters and populate it to the crud object
            crud.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.sp_ld_eo_upld", intNumberOfInputParameters, listOutputParameters);

            //Standardize the column values
            dynamic strEnterpriseOrgId;
            if (string.IsNullOrEmpty(input.strEnterpriseOrgId))
                strEnterpriseOrgId = DBNull.Value;
            else
                strEnterpriseOrgId = input.strEnterpriseOrgId;
            dynamic strEnterpriseOrgName;
            if (string.IsNullOrEmpty(input.strEnterpriseOrgName))
                strEnterpriseOrgName = DBNull.Value;
            else
                strEnterpriseOrgName = input.strEnterpriseOrgName;

            Dictionary<string, string> dictTransformationRuleMapping = new Dictionary<string, string>();
            dictTransformationRuleMapping.Add("contains", "C");
            dictTransformationRuleMapping.Add("does not contain", "D");
            dictTransformationRuleMapping.Add("exact match", "E");
            dictTransformationRuleMapping.Add("starts with", "F");

            //create a list of parameters that have to be passed to the procedure
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_eo_upld_key", input.strSeqKey, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_id", strEnterpriseOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_name", strEnterpriseOrgName, "IN", TdType.VarChar, 200));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_chrctrstc1_typ_cd", (!string.IsNullOrEmpty(input.strCharacteristics1Code) ? input.strCharacteristics1Code : string.Empty), "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_chrctrstc1_val", (!string.IsNullOrEmpty(input.strCharacteristics1Value) ? input.strCharacteristics1Value : string.Empty), "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_chrctrstc2_typ_cd", (!string.IsNullOrEmpty(input.strCharacteristics2Code) ? input.strCharacteristics2Code : string.Empty), "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_chrctrstc2_val", (!string.IsNullOrEmpty(input.strCharacteristics2Value) ? input.strCharacteristics2Value : string.Empty), "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_chrctrstc3_typ_cd", (!string.IsNullOrEmpty(input.strCharacteristics3Code) ? input.strCharacteristics3Code : string.Empty), "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_chrctrstc3_val", (!string.IsNullOrEmpty(input.strCharacteristics3Value) ? input.strCharacteristics3Value : string.Empty), "IN", TdType.VarChar, 5000));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn1_typ_cd1", (!string.IsNullOrEmpty(input.strTransformCondition1Type1) ? dictTransformationRuleMapping[input.strTransformCondition1Type1.ToLower()] : string.Empty), "IN", TdType.VarChar, 5));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn1_str1", (!string.IsNullOrEmpty(input.strTransformCondition1String1) ? input.strTransformCondition1String1 : string.Empty), "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn1_typ_cd2", (!string.IsNullOrEmpty(input.strTransformCondition1Type2) ? dictTransformationRuleMapping[input.strTransformCondition1Type2.ToLower()] : string.Empty), "IN", TdType.VarChar, 5));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn1_str2", (!string.IsNullOrEmpty(input.strTransformCondition1String2) ? input.strTransformCondition1String2 : string.Empty), "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn1_typ_cd3", (!string.IsNullOrEmpty(input.strTransformCondition1Type3) ? dictTransformationRuleMapping[input.strTransformCondition1Type3.ToLower()] : string.Empty), "IN", TdType.VarChar, 5));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn1_str3", (!string.IsNullOrEmpty(input.strTransformCondition1String3) ? input.strTransformCondition1String3 : string.Empty), "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn2_typ_cd1", (!string.IsNullOrEmpty(input.strTransformCondition2Type1) ? dictTransformationRuleMapping[input.strTransformCondition2Type1.ToLower()] : string.Empty), "IN", TdType.VarChar, 5));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn2_str1", (!string.IsNullOrEmpty(input.strTransformCondition2String1) ? input.strTransformCondition2String1 : string.Empty), "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn2_typ_cd2", (!string.IsNullOrEmpty(input.strTransformCondition2Type2) ? dictTransformationRuleMapping[input.strTransformCondition2Type2.ToLower()] : string.Empty), "IN", TdType.VarChar, 5));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn2_str2", (!string.IsNullOrEmpty(input.strTransformCondition2String2) ? input.strTransformCondition2String2 : string.Empty), "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn2_typ_cd3", (!string.IsNullOrEmpty(input.strTransformCondition2Type3) ? dictTransformationRuleMapping[input.strTransformCondition2Type3.ToLower()] : string.Empty), "IN", TdType.VarChar, 5));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn2_str3", (!string.IsNullOrEmpty(input.strTransformCondition2String3) ? input.strTransformCondition2String3 : string.Empty), "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn3_typ_cd1", (!string.IsNullOrEmpty(input.strTransformCondition3Type1) ? dictTransformationRuleMapping[input.strTransformCondition3Type1.ToLower()] : string.Empty), "IN", TdType.VarChar, 5));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn3_str1", (!string.IsNullOrEmpty(input.strTransformCondition3String1) ? input.strTransformCondition3String1 : string.Empty), "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn3_typ_cd2", (!string.IsNullOrEmpty(input.strTransformCondition3Type2) ? dictTransformationRuleMapping[input.strTransformCondition3Type2.ToLower()] : string.Empty), "IN", TdType.VarChar, 5));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn3_str2", (!string.IsNullOrEmpty(input.strTransformCondition3String2) ? input.strTransformCondition3String2 : string.Empty), "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn3_typ_cd3", (!string.IsNullOrEmpty(input.strTransformCondition3Type3) ? dictTransformationRuleMapping[input.strTransformCondition3Type3.ToLower()] : string.Empty), "IN", TdType.VarChar, 5));
            ParamObjects.Add(SPHelper.createTdParameter("i_transform_condn3_str3", (!string.IsNullOrEmpty(input.strTransformCondition3String3) ? input.strTransformCondition3String3 : string.Empty), "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_tag1", (!string.IsNullOrEmpty(input.strTag1) ? input.strTag1 : string.Empty), "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_tag2", (!string.IsNullOrEmpty(input.strTag2) ? input.strTag2 : string.Empty), "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_tag3", (!string.IsNullOrEmpty(input.strTag3) ? input.strTag3 : string.Empty), "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_action", (!string.IsNullOrEmpty(input.strAction) ? input.strAction : string.Empty), "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", (!string.IsNullOrEmpty(strUserName) ? strUserName : string.Empty), "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_key", input.strTransKey, "IN", TdType.BigInt, 0));
            
            //populate the parameters to the crud object's parameter property
            crud.parameters = ParamObjects;

            //return the crud object to the calling method
            return crud;
        }

        public static string getMaxSeqKeySQL()
        {
            return "SELECT COALESCE(MAX(eo_upld_key),0) FROM arc_orgler_vws.bz_eo_upld;";
        }
    }
}
