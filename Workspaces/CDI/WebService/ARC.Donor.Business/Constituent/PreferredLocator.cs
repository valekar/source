using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Constituents
{
    public class PreferredLocator
    {
        public string line_of_service_cd { get; set; }
        public string pref_loc_typ { get; set; }
        public string pref_loc_id { get; set; }
    }

    public class PreferredLocatorInput
    {
        public string cnst_master_id { get; set; }
        public string cnst_typ { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string los_cd { get; set; }
        public string pref_loc_typ { get; set; }
        public string pref_loc_id { get; set; }
        public string notes { get; set; }
        public string created_by { get; set; }
        public string user_id { get; set; }
    }

    public class PreferredLocatorOutput
    {
        public string output_msg { get; set; }
        public string trans_key { get; set; }
    }
}
