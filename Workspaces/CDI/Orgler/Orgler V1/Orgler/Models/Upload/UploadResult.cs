using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Upload
{
    //Class to store the upload results
    public class UploadResult
    {
        public List<AffiliationUploadResult> invalidRecords { get; set; }
        public List<AffiliationUploadInput> validRecords { get; set; }
        public List<EosiUploadResult> invalidRecordsEosi { get; set; }
        public List<EosiUploadInput> validRecordsEosi { get; set; }
        public List<EoUploadResult> invalidRecordsEo { get; set; }
        public List<EoUploadInput> validRecordsEo { get; set; }
        public string strUploadResult { get; set; }
        public string strUploadId { get; set; }
        public string strUploadFileName { get; set; }
        public int intFileSize { get; set; }
        public string strDocExtention { get; set; }
        public bool boolSuccess { get; set; }
        public bool boolFailure { get; set; }
        public int intSuccessCount { get; set; }
        public int intErrorCount { get; set; }
        public int intWarningCount { get; set; }
    }
}