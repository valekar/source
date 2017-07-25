using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Constituents
{
    public class OrgContacts
    {
        public string org_mstr_id { get;set;}
        public string indv_mstr_id { get;set;}
        public string rlshp_typ { get;set;}
        public string name { get; set; }
        public string address { get; set; }
        public string email_address { get; set; }
        public string phone_number { get; set; }
        public string dw_srcsys_trans_ts { get;set;}
        public string row_stat_cd { get;set;}
        public string appl_src_cd { get;set;}
        public string load_id { get; set; }
    }
}
