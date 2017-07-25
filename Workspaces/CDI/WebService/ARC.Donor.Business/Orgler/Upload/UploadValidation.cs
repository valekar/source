using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Orgler.Upload
{
    public class AffiliationUploadValidationInput
    {
        //Input ids
        public List<string> strEnterpriseOrgIdInput { get; set; }
        public List<string> strMasterIdInput { get; set; }
    }

    public class AffiliationUploadValidationOutput
    {
        //Valid ids
        public List<string> strEnterpriseOrgIdValid { get; set; }
        public List<string> strMasterIdInputValid { get; set; }
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
