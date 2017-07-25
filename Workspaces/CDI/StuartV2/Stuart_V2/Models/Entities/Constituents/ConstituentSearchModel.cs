using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Constituents
{
    public class ConstituentSearchModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string addressLine { get; set; }
        public string city { set; get; }
        public string state { get; set; }
        public string zip { get; set; }
        public string masterId { get; set; }
        public string sourceSystem { get; set; }
        public string sourceSystemId { get; set; }
        public string chapterSystem { get; set; }
        public string emailAddress { get; set; }
        public string phone { get; set; }
        public string type { get; set; }
        public string LoggedInUser { get; set; }
    }


    public class ConstituentInputSearchModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string organization_name { get; set; }
        public string addressLine { get; set; }
        public string city { set; get; }
        public string state { get; set; }
        public string zip { get; set; }
        public string masterId { get; set; }
        public string sourceSystem { get; set; }
        public string sourceSystemId { get; set; }
        public string chapterSystem { get; set; }
        public string emailAddress { get; set; }
        public string phone { get; set; }
        public string type { get; set; }
        public string LoggedInUser { get; set; }
        public ConstituentInputSearchModel()
        {
            //System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            //LoggedInUser = p.Identity.Name;

            LoggedInUser = HttpContext.Current.User.GetUserName();
        }
    }

    public class ConstituentOutputSearchResults
    {


        public string name { get; set; }
        public string constituent_id { get; set; }
        public string cnst_dsp_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string constituent_type { get; set; }
        public string request_transaction_key { get; set; }
        public string email_address { get; set; }
        public string phone_number { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string city { get; set; }
        public string state_cd { get; set; }
        public string zip { get; set; }
        public string address { get; set; }
        public string source_system_count { get; set; }
    }


    public class ListConstituentInputSearchModel
    {

        //public ListConstituentInputSearchModel(List<ConstituentInputSearchModel> list)
        //{
        //    this.ConstituentInputSearchModel = list;
        //}
        public List<ConstituentInputSearchModel> ConstituentInputSearchModel { get; set; }
        public string AnswerSetLimit { get; set; }
    }
}