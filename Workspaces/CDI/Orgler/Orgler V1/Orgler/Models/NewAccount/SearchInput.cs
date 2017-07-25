using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.NewAccount
{
    //Input model for the New Account search
    public class SearchInput
    {
        public string los { get; set; }
        public string createdDateFrom { get; set; }
        public string createdDateTo { get; set; }
        public string masteringType { get; set; }
        public string enterpriseOrgAssociation { get; set; }
        public string naicsStatus { get; set; }
        public List<string> listRuleKeyword { get; set; }
        public List<string> listNaicsCodes { get; set; }
        public string enterpriseOrgId { get; set; }
        public string srch_user_name { get; set; }
        public string AnswerSetLimit { get; set; }
    }
}