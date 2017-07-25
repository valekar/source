using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Orgler.EnterpriseOrgs
{
    public class Characteristics
    {
        public string ent_org_id { get; set; }
        //this is for details section
        public string cnst_mstr_id { get; set; }
        public string cnst_chrctrstc_typ_cd { get; set; }
        public string cnst_chrctrstc_val { get; set; }
        public string cnst_chrctrstc_strt_dt { get; set; }
        public string cnst_chrctrstc_end_dt { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string cnst_chrctrstc_typ_cnfdntl_ind { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
    }

    public class OrgCharacteristicsInput
    {
        public string RequestType { get; set; }
        public Int64 EntOrgID { get; set; }
        public string UserName { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public Int64? CaseNumber { get; set; }
        public string OldCharacteristicValue { get; set; }
        public string OldCharacteristicTypeCode { get; set; }
        public string CharacteristicValue { get; set; }
        public string CharacteristicTypeCode { get; set; }

        public OrgCharacteristicsInput()
        {
            UserName = string.Empty;
            ConstType = string.Empty;
            Notes = string.Empty;
            CaseNumber = 0;
            OldCharacteristicValue = string.Empty;
            OldCharacteristicTypeCode = string.Empty;
            CharacteristicValue = string.Empty;
            CharacteristicTypeCode = string.Empty;
        }
    }

    public class OrgCharacteristicsOutput
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }

}
