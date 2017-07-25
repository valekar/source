using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.EnterpriseAccount
{
    public class EnterpriseAccountSearchInput
    {
        public string enterpriseOrgId { get; set; }
        public string enterpriseOrgName { get; set; }
        public string sourceSystem { get; set; }
        public string chapterSystem { set; get; }
        public string sourceSystemId { get; set; }
        public string rankProvider { get; set; }
        public string rankPublishYear { get; set; }
        public string rank { get; set; }
        public List<string> listNaicsCodes { get; set; }
        public string enterpriseOrgType { get; set; }
        public bool isPremierFR { get; set; }
        public bool isPremierHS { get; set; }
        public bool isPremierBIO { get; set; }
        public bool isPremierENT { get; set; }
    }
    public class ListEnterpriseAccountSearchInput
    {
        public List<EnterpriseAccountSearchInput> EnterpriseAccountSearchInput { get; set; }
        public string AnswerSetLimit { get; set; }
    }
}