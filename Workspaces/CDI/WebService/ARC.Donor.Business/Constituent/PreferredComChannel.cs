using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Constituents
{
    public class PreferredComChannel
    {
        public string pref_chan { get; set; }
        public string line_of_service_cd { get; set; }
    }

    public class PreferredComChannelInput
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_typ { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string los_cd { get; set; }
        public string pref_chan { get; set; }
        public string notes { get; set; }
        public string created_by { get; set; }
        public string user_id { get; set; }
    }

    public class PreferredComChannelOutput
    {
        public string output_msg { get; set; }
        public string trans_key { get; set; }
    }
}
