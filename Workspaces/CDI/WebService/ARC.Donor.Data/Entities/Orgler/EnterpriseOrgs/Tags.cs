using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs
{
    /* Name:TagOutputModel
* Purpose: This class is the output model for tags of an enterprise. */
    public class TagOutputModel
    {

        public string ent_org_id { get; set; }
        public string tag_key { get; set; }
        public string tag { get; set; }
        public string dw_trans_ts { get; set; }
        public string start_dt { get; set; }
        public string end_dt { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string row_stat_cd { get; set; }

    }

    public class TagUpdateInputModel
    {
        public string ent_org_id { get; set; }
        public string tag { get; set; }
        public string action_type { get; set; }
        public string user_nm { get; set; }
    }

    public class TagUpdateOutputModel
    {
        public string o_outputMessage { get; set; }
        public long? o_outputTrans { get; set; }
    }

    public class TagDDList
    {
        public string tag_key { get; set; }
        public string tag { get; set; }
        public string start_dt { get; set; }
        public string end_dt { get; set; }
        public string dw_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string user_id { get; set; }
    }
}
