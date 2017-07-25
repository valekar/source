using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.AccountMonitoring
{
    /* Name: UploadNAICSSuggestionsInput
   * Purpose: This class is the input model for Uploading NAICS suggestions */
    public class UploadNAICSSuggestionsInput
    {
        public int cnst_mstr_id { get; set; }
        public string cnst_org_nm { get; set; }
        public string cnst_org_addrs { get; set; }
        public string naics_cd { get; set; }
        public string naics_title { get; set; }
        public string naics_map_rule_key { get; set; }
        public string sts { get; set; }
        public string action { get; set; }
        public string user_id { get; set; }
        public int trans_key { get; set; }
    }
    /* Name: UploadNAICSSuggestionsFileInfo
 * Purpose: This class is the model for storing file details */
    public class UploadNAICSSuggestionsFileInfo
    {
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public double intFileSize { get; set; }
    }
    /* Name: ListUploadNAICSSuggestionsInput
  * Purpose: This class is the input list model for Uploading NAICS suggestions */
    public class ListUploadNAICSSuggestionsInput
    {
        public List<UploadNAICSSuggestionsInput> NAICSSuggestionsInputList { get; set; }
        public UploadNAICSSuggestionsFileInfo UploadedNAICSSuggestionsFileInputInfo { get; set; }
    }
    /* Name: NAICSSuggestionsJsonFileDetailsHelper
 * Purpose: This class is the model for storing Json file details upon Uploading NAICS suggestions */
    public class NAICSSuggestionsJsonFileDetailsHelper
    {
        public string UserName { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public string FileType { get; set; }
        public string FileExtention { get; set; }
        public int BusinessType { get; set; }
        public string UploadStart { get; set; }
        public string UploadEnd { get; set; }
        public string UploadStatus { get; set; }
        public string TransactionKey { get; set; }
    }
    /* Name: NAICSSuggestionsDetails
* Purpose: This class is the input model for storing Uploaded NAICS suggestions details*/
    public class NAICSSuggestionsDetails
    {
        public ListUploadNAICSSuggestionsInput ListUploadNAICSSuggestionsDetails { get; set; }
        public List<NAICSSuggestionsJsonFileDetailsHelper> ListNAICSSuggestionsJsonFileDetailsInput { get; set; }
    }

    /* Name: NAICSSuggestionsSubmitOutput
    * Purpose: This class is the output model for storing Uploaded NAICS suggestions details*/
    public class NAICSSuggestionsSubmitOutput
    {
        public byte insertFlag { get; set; }
        public string insertOutput { get; set; }

        public List<NAICSSuggestionsJsonFileDetailsHelper> ListNAICSSuggestionsJsonFileDetailsHelperDetailsInput { get; set; }
        public long transactionKey { get; set; }
    }
    /* Name: UploadCreateTransOutput
   * Purpose: This class is the output model for creating transactions*/
    public class UploadCreateTransOutput
    {
        public long transKey { get; set; }
        public string transOutput { get; set; }
    }

    /* Name: UploadMetric
  * Purpose: This class is the output model for getting */
    public class UploadMetric
    {
        public long trans_key { get; set; }
        public string rej_cnt { get; set; }
        public string trgt_cnt { get; set; }
    }
}
