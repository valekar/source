using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Upload
{
    public class AffiliationUploadInput
    {
        public string strEnterpriseOrgId { get; set; }
        public string strMasterId { get; set; }
        public string strStatus { get; set; }
        public string strTransKey { get; set; }
    }

    public class AffiliationUploadControllerInput
    {
        public List<AffiliationUploadInput> input { get; set; }
        public string strUserName { get; set; }
        public string strUploadFileName { get; set; }
        public int intFileSize { get; set; }
        public string strDocExtention { get; set; }
        
    }
}