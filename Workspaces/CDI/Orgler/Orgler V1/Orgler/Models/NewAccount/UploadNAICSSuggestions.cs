using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.NewAccount
{
    public class UploadNAICSSuggestionsInput
    {
        public int naics_sug_upload_id { get; set; }
        public string los_cd { get; set; }
        public string cnst_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_org_nm { get; set; }
        public string cnst_org_addrs { get; set; }
        public string cnst_mstr_result { get; set; }
        public string cnst_monitory_value { get; set; }
        public string naics_cd { get; set; }
        public string naics_title { get; set; }
        public string naics_map_rule_key { get; set; }
        public string sts { get; set; }
        public string action { get; set; }
        public string user_id { get; set; }
        public int srcsys_trans_id { get; set; }
    }

    public class UploadNAICSSuggestionsFileInfo
    {
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public double intFileSize { get; set; }
    }

    public class ListUploadNAICSSuggestionsInput
    {
        public List<UploadNAICSSuggestionsInput> NAICSSuggestionsInputList { get; set; }
        public UploadNAICSSuggestionsFileInfo UploadedNAICSSuggestionsFileInputInfo { get; set; }
    }

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

    public class NAICSSuggestionsDetails
    {
        public ListUploadNAICSSuggestionsInput ListUploadNAICSSuggestionsDetails { get; set; }
        public List<NAICSSuggestionsJsonFileDetailsHelper> ListNAICSSuggestionsJsonFileDetailsInput { get; set; }
    }


    public class NAICSSuggestionsSubmitOutput
    {
        public byte insertFlag { get; set; }
        public string insertOutput { get; set; }

        public List<NAICSSuggestionsJsonFileDetailsHelper> ListNAICSSuggestionsJsonFileDetailsHelperDetailsInput { get; set; }
        public long transactionKey { get; set; }
    }
}