using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Upload
{
    public class EoUploadResult
    {
        public string strEnterpriseOrgId { get; set; }
        public string strEnterpriseOrgIdFlag { get; set; }
        public string strEnterpriseOrgName { get; set; }
        public string strEnterpriseOrgNameFlag { get; set; }
        public string strCharacteristics1Code { get; set; }
        public string strCharacteristics1CodeFlag { get; set; }
        public string strCharacteristics1Value { get; set; }
        public string strCharacteristics1ValueFlag { get; set; }
        public string strCharacteristics2Code { get; set; }
        public string strCharacteristics2CodeFlag { get; set; }
        public string strCharacteristics2Value { get; set; }
        public string strCharacteristics2ValueFlag { get; set; }
        public string strCharacteristics3Code { get; set; }
        public string strCharacteristics3CodeFlag { get; set; }
        public string strCharacteristics3Value { get; set; }
        public string strCharacteristics3ValueFlag { get; set; }
        public string strTransformCondition1Type1 { get; set; }
        public string strTransformCondition1Type1Flag { get; set; }
        public string strTransformCondition1String1 { get; set; }
        public string strTransformCondition1String1Flag { get; set; }
        public string strTransformCondition1Type2 { get; set; }
        public string strTransformCondition1Type2Flag { get; set; }
        public string strTransformCondition1String2 { get; set; }
        public string strTransformCondition1String2Flag { get; set; }
        public string strTransformCondition1Type3 { get; set; }
        public string strTransformCondition1Type3Flag { get; set; }
        public string strTransformCondition1String3 { get; set; }
        public string strTransformCondition1String3Flag { get; set; }
        public string strTransformCondition2Type1 { get; set; }
        public string strTransformCondition2Type1Flag { get; set; }
        public string strTransformCondition2String1 { get; set; }
        public string strTransformCondition2String1Flag { get; set; }
        public string strTransformCondition2Type2 { get; set; }
        public string strTransformCondition2Type2Flag { get; set; }
        public string strTransformCondition2String2 { get; set; }
        public string strTransformCondition2String2Flag { get; set; }
        public string strTransformCondition2Type3 { get; set; }
        public string strTransformCondition2Type3Flag { get; set; }
        public string strTransformCondition2String3 { get; set; }
        public string strTransformCondition2String3Flag { get; set; }
        public string strTransformCondition3Type1 { get; set; }
        public string strTransformCondition3Type1Flag { get; set; }
        public string strTransformCondition3String1 { get; set; }
        public string strTransformCondition3String1Flag { get; set; }
        public string strTransformCondition3Type2 { get; set; }
        public string strTransformCondition3Type2Flag { get; set; }
        public string strTransformCondition3String2 { get; set; }
        public string strTransformCondition3String2Flag { get; set; }
        public string strTransformCondition3Type3 { get; set; }
        public string strTransformCondition3Type3Flag { get; set; }
        public string strTransformCondition3String3 { get; set; }
        public string strTransformCondition3String3Flag { get; set; }
        public string strTag1 { get; set; }
        public string strTag1Flag { get; set; }
        public string strTag2 { get; set; }
        public string strTag2Flag { get; set; }
        public string strTag3 { get; set; }
        public string strTag3Flag { get; set; }
        public string strAction { get; set; }
        public string strActionFlag { get; set; }

        public EoUploadResult() 
        {
            strEnterpriseOrgIdFlag = "1";
            strEnterpriseOrgNameFlag = "1";
            strCharacteristics1CodeFlag = "1";
            strCharacteristics1ValueFlag = "1";
            strCharacteristics2CodeFlag = "1";
            strCharacteristics2ValueFlag = "1";
            strCharacteristics3CodeFlag = "1";
            strCharacteristics3ValueFlag = "1";
            strTransformCondition1Type1Flag = "1";
            strTransformCondition1String1Flag = "1";
            strTransformCondition1Type2Flag = "1";
            strTransformCondition1String2Flag = "1";
            strTransformCondition1Type3Flag = "1";
            strTransformCondition1String3Flag = "1";
            strTransformCondition2Type1Flag = "1";
            strTransformCondition2String1Flag = "1";
            strTransformCondition2Type2Flag = "1";
            strTransformCondition2String2Flag = "1";
            strTransformCondition2Type3Flag = "1";
            strTransformCondition2String3Flag = "1";
            strTransformCondition3Type1Flag = "1";
            strTransformCondition3String1Flag = "1";
            strTransformCondition3Type2Flag = "1";
            strTransformCondition3String2Flag = "1";
            strTransformCondition3Type3Flag = "1";
            strTransformCondition3String3Flag = "1";
            strTag1Flag = "1";
            strTag2Flag = "1";
            strTag3Flag = "1";
            strActionFlag = "1";
        }
    }

    public class EoUploadValidationInput
    {
        //Input ids
        public List<string> strEnterpriseOrgIdInput { get; set; }
        public List<string> strEnterpriseOrgNameInput { get; set; }
        public List<string> strCharacteristicTypeInput { get; set; }
        public List<string> strTagInput { get; set; }
    }

    public class EoUploadValidationOutput
    {
        //Valid ids
        public List<string> strEnterpriseOrgIdValid { get; set; }
        public List<string> strEnterpriseOrgNameValid { get; set; }
        public List<string> strCharacteristicTypeValid { get; set; }
        public List<string> strTagValid { get; set; }
    }
}