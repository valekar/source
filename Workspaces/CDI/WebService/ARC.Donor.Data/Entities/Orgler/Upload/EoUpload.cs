using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.Upload
{
    public class EoUploadInput
    {
        public Int64 strSeqKey { get; set; }
        public string strEnterpriseOrgId { get; set; }
        public string strEnterpriseOrgName { get; set; }
        public string strCharacteristics1Code { get; set; }
        public string strCharacteristics1Value { get; set; }
        public string strCharacteristics2Code { get; set; }
        public string strCharacteristics2Value { get; set; }
        public string strCharacteristics3Code { get; set; }
        public string strCharacteristics3Value { get; set; }
        public string strTransformCondition1Type1 { get; set; }
        public string strTransformCondition1String1 { get; set; }
        public string strTransformCondition1Type2 { get; set; }
        public string strTransformCondition1String2 { get; set; }
        public string strTransformCondition1Type3 { get; set; }
        public string strTransformCondition1String3 { get; set; }
        public string strTransformCondition2Type1 { get; set; }
        public string strTransformCondition2String1 { get; set; }
        public string strTransformCondition2Type2 { get; set; }
        public string strTransformCondition2String2 { get; set; }
        public string strTransformCondition2Type3 { get; set; }
        public string strTransformCondition2String3 { get; set; }
        public string strTransformCondition3Type1 { get; set; }
        public string strTransformCondition3String1 { get; set; }
        public string strTransformCondition3Type2 { get; set; }
        public string strTransformCondition3String2 { get; set; }
        public string strTransformCondition3Type3 { get; set; }
        public string strTransformCondition3String3 { get; set; }
        public string strTag1 { get; set; }
        public string strTag2 { get; set; }
        public string strTag3 { get; set; }
        public string strAction { get; set; }
        public string strTransKey { get; set; }
    }

    public class EoUploadOutput
    {
        public string o_message { get; set; }
    }
}
