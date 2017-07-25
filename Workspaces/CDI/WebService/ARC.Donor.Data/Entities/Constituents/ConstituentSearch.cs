using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Constituents
{
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
    }

    public class ListConstituentInputSearchModel
    {
        public List<ConstituentInputSearchModel> ConstituentInputSearchModel { get; set; }
        public string AnswerSetLimit { get; set; }
    }

    public class ConstituentOutputSearchResults
    {
        //public string name { get; set; }
        //public Int64 constituent_id { get; set; }
        //public string sourceSystem { get; set; }
        //public string email_address { get; set; }
        //public string phone_number { get; set; }
        //public string addr_line_1 { get; set; }
        //public string addr_line_2 { get; set; }
        //public string city { get; set; }
        //public string state_cd { get; set; }
        //public string zip { get; set; }
        //public int lnId { get; set; }
        //public string sourceSystem { get; set; }
        //public int lnId { get; set; }

        public string name { get; set; }
        public string ent_org_name { get; set; }
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
        public string ent_has_eo { get; set; }
        public string has_dsp_id { get; set; }
    }

    public class BridgeCount
    {
        public string cnst_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string counter { get; set; }
    }
}
