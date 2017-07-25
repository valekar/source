using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Orgler.AccountMonitoring
{
    /* Name: NAICSStatusChangeInput
    * Purpose: This class is the input model for changing the status of the naics codes for a particular master*/
    public class NAICSStatusChangeInput
    {
        public string cnst_mstr_id { get; set; }
      
        public List<string> approved_naics_codes { get; set; }
        public List<string> rejected_naics_codes { get; set; }
        public List<string> added_naics_codes { get; set; }
        public string cnst_org_nm { get; set; }
        public string usr_nm { get; set; }
    }

    /* Name: GetMasterNAICSDetailsInput
    * Purpose: This class is the input model for getting all the NAICS Details for a particular master */
    public class GetMasterNAICSDetailsInput
    {
        public string cnst_mstr_id { get; set; }
        public string source_system_id { get; set; }
        public string source_system_code { get; set; }
        public string naics_cd { get; set; }
    }

    /* Name: GetMasterNAICSDetailsOutput
    * Purpose: This class is the output model for getting all the NAICS Details for a particular master */
    public class GetMasterNAICSDetailsOutput
    {
        public string cnst_mstr_id { get; set; }
        public string sts { get; set; }
        public string naics_cd { get; set; }
        public string naics_indus_title { get; set; }
        public string naics_indus_dsc { get; set; }
        public string conf_weightg { get; set; }
        public string rule_keywrd { get; set; }
        public string manual_sts { get; set; }

        public GetMasterNAICSDetailsOutput()
        {
            manual_sts = "Action";
        }
    }

    /* Name: NAICSCode
    * Purpose: This class is the output class(created one for hierarchy) for getting all NAICS codes in heirachy format */
    public class NAICSCode
    {
        public string naics_key { get; set; }
        public string naics_cd { get; set; }
        public string naics_indus_title { get; set; }
        public string naics_lvl { get; set; }
        public string parent_naics_cd { get; set; }
        public List<NAICSCode> children = new List<NAICSCode>();
        public List<NAICSCode> GetSubNAICSCode()
        {
            return children;
        }
        public void AddSubNAICSCode(NAICSCode ci)
        {
            children.Add(ci);
        }
    }
}
