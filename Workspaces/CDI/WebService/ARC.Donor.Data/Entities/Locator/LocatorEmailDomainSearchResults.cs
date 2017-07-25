using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.LocatorDomain
{
    public class LocatorDomainOutputSearchResults
    {
        public string email_domain_map_key { get; set; }
        public string domain_part { get; set; }
        public string poss_domain_corrctn { get; set; }
        public string sts { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string cnt { get; set; }




        public LocatorDomainOutputSearchResults()
        {

            email_domain_map_key = string.Empty;
            domain_part = string.Empty;
            sts = string.Empty;
            trans_key = string.Empty;
            user_id = string.Empty;
            dw_srcsys_trans_ts = string.Empty;
            row_stat_cd = string.Empty;
            cnt = string.Empty;
        }
    }




}
