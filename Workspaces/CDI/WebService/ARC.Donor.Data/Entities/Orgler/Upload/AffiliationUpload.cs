using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.Upload
{
    public class AffiliationUploadInput
    {
        public string strEnterpriseOrgId { get; set; }
        public string strMasterId { get; set; }
        public string strStatus { get; set; }
        public string strTransKey { get; set; }
    }

    public class AffiliationUploadOutput
    {
        public string o_message { get; set; }
    }
}
