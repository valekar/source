using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Upload
{
    public class AffiliationUploadResult
    {
        public string strEnterpriseOrgId { get; set; }
        public string strEnterpriseOrgIdFlag { get; set; } //Flag to indicate invalid enterprise ids
        public string strMasterId { get; set; }
        public string strMasterIdFlag { get; set; } //Flag to indicate invalid master ids
        public string strStatus { get; set; }
    }

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
}