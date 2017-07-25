using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs
{
    /* Name: GetMasterNAICSDetailsOutput
    * Purpose: This class is the output model for getting all the NAICS Details for a particular master */
    public class GetNAICSDetailsOutput
    {
        public string ent_org_id { get; set; }
        public string cnst_mstr_id { get; set; }
        public string sts { get; set; }
        public string naics_cd { get; set; }
        public string naics_indus_title { get; set; }
        public string naics_indus_dsc { get; set; }
        public string rule_keywrd { get; set; }
        public string conf_weightg { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
    }
}
