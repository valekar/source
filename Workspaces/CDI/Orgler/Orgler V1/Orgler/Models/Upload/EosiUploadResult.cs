using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Upload
{
    public class EosiUploadResult
    {
        public string strEnterpriseOrgId { get; set; }
        public string strEnterpriseOrgIdFlag { get; set; }
        public string strMasterId { get; set; }
        public string strMasterIdFlag { get; set; }
        public string strSourceSystemCode { get; set; }
        public string strSourceSystemCodeFlag { get; set; }
        public string strSourceId { get; set; }
        public string strSourceIdFlag { get; set; }
        public string strSecondarySourceId { get; set; }
        public string strSecondarySourceIdFlag { get; set; }
        public string strParentEnterpriseOrgId { get; set; }
        public string strParentEnterpriseOrgIdFlag { get; set; }
        public string strAltSourceCode { get; set; }
        public string strAltSourceId { get; set; }
        public string strOrgName { get; set; }
        public string strAddress1Street1 { get; set; }
        public string strAddress1Street2 { get; set; }
        public string strAddress1City { get; set; }
        public string strAddress1State { get; set; }
        public string strAddress1Zip { get; set; }
        public string strPhone1 { get; set; }
        public string strPhone2 { get; set; }
        public string strNaicsCode { get; set; }
        public string strNaicsCodeFlag { get; set; }
        public string strCharacteristics1Code { get; set; }
        public string strCharacteristics1CodeFlag { get; set; }
        public string strCharacteristics1Value { get; set; }
        public string strCharacteristics1ValueFlag { get; set; }
        public string strCharacteristics2Code { get; set; }
        public string strCharacteristics2CodeFlag { get; set; }
        public string strCharacteristics2Value { get; set; }
        public string strCharacteristics2ValueFlag { get; set; }
        public string strRMIndicator { get; set; }
        public string strNotes { get; set; }
    }

    public class EosiUploadValidationInput
    {
        //Input ids
        public List<string> strEnterpriseOrgIdInput { get; set; }
        public List<string> strMasterIdInput { get; set; }
        public List<string> strNaicsCodeInput { get; set; }
        public List<string> strCharacteristicTypeInput { get; set; }
    }

    public class EosiUploadValidationOutput
    {
        //Valid ids
        public List<string> strEnterpriseOrgIdValid { get; set; }
        public List<string> strMasterIdValid { get; set; }
        public List<string> strNaicsCodeValid { get; set; }
        public List<string> strCharacteristicTypeValid { get; set; }
    }
}