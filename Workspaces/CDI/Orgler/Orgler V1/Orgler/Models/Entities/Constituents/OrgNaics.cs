using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orgler.Models.Entities.Constituent
{

    public class NAICSStatusUpdateInput
    {
        public string cnst_mstr_id { get; set; }

        public List<string> approved_naics_codes { get; set; }
        public List<string> rejected_naics_codes { get; set; }
        public List<string> added_naics_codes { get; set; }
        public string cnst_org_nm { get; set; }
        public string usr_nm { get; set; }
    }

    public class OrgNaics
    {
        public string cnst_mstr_id { get; set; }
        public string sts { get; set; }
        public string naics_cd { get; set; }
        public string naics_indus_title { get; set; }
        public string naics_indus_dsc { get; set; }
        public string conf_weightg { get; set; }
        public string rule_keywrd { get; set; }
        public string manual_sts { get; set; }

        public OrgNaics()
        {
            manual_sts = "Action";
        }
    }
}
