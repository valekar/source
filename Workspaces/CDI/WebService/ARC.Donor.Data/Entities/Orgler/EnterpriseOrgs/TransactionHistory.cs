using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs
{
    public class TransactionHistoryOutputModel
    {
        public string trans_key { get; set; }
        public string trans_typ_dsc { get; set; }
        public string sub_trans_typ_dsc { get; set; }
        public string sub_trans_actn_typ { get; set; }
        public string trans_stat { get; set; }
        public string trans_note { get; set; }
        public string ent_org_id { get; set; }
        public string user_id { get; set; }
        public string trans_create_ts { get; set; }
        public string trans_last_modified_ts { get; set; }
        public string approved_by { get; set; }
        public string approved_dt { get; set; }
    }
}
