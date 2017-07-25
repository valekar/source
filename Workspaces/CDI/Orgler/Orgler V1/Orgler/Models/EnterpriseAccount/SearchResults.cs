using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.EnterpriseAccount
{
    public class EnterpriseAccountSearchResults
    {
        public string ent_org_id { get; set; }
        public string ent_org_name { get; set; }
        public string ent_org_src_cd { get; set; }
        public string nk_ent_org_id { get; set; }
        public string affil_vsblty { get; set; }
        public string trans_key { get; set; }
        public string Tags { get; set; }
        public List<listString> listNAICSCodes { get; set; }
        public List<listString> listNAICSDesc { get; set; }
        public string created_by { get; set; }
        public string created_at { get; set; }
        public string last_modified_by { get; set; }
        public string last_modified_at { get; set; }
        public string last_modified_by_all { get; set; }
        public string last_modified_at_all { get; set; }
        public string last_reviewed_by { get; set; }
        public string last_reviewed_at { get; set; }
        public string data_stwrd_usr { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string load_id { get; set; }
        public string o_outputMessage { get; set; }
        public string transaction_key { get; set; }
        public string ent_has_eo { get; set; }
        public string has_dsp_id { get; set; }
    }
    public class listString
    {
        public string strText { get; set; }
        public string status { get; set; }
    }
}