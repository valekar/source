using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Upload
{
    public class ListUploadSearch
    {
        public string grp_cd { get; set; }
    }

    public class ListOfListUploadSearchInput
    {
        public List<ListUploadSearch> ListUploadSearchInput { get; set; }
        public string answerLimit { get; set; }
    }

    public class ListUploadOutput
    {
        public string grp_cd { get; set; }
        public string grp_nm { get; set; }
        public string grp_info { get; set; }
        public string trans_key { get; set; }
        public string trans_stat { get; set; }
        public string user_id { get; set; }
        public string trans_create_ts { get; set; }
        public string upld_typ { get; set; }
    }
}