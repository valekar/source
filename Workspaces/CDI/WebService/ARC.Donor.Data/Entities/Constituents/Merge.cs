using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Constituents
{
    /* Entity classes to retrieve the Master details */
    public class MasterDetailsInput
    {
        public string ConstituentType { get; set; }
        public List<string> MasterId { get; set; }
    }

    /* cnst_mstr data */
    public class MasterData
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_dsp_id { get; set; }
    }

    /* cnst_prsn_nm or cnst_org_nm data */
    public class MasterNameData
    {
        public string cnst_mstr_id { get; set; }
        public string name { get; set; }
        public string arc_srcsys_cd { get; set; }
    }

    /* cnst_addr data */
    public class MasterAddressData
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_addr_line1_addr { get; set; }
        public string cnst_addr_line2_addr { get; set; }
        public string cnst_addr_city_nm { get; set; }
        public string cnst_addr_state_cd { get; set; }
        public string cnst_addr_zip_5_cd { get; set; }
        public string arc_srcsys_cd { get; set; }
    }

    /* cnst_phn_typ2 data */
    public class MasterPhoneData
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_phn_num { get; set; }
        public string arc_srcsys_cd { get; set; }
    }

    /* cnst_email_typ2 data */
    public class MasterEmailData
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_email_addr { get; set; }
        public string arc_srcsys_cd { get; set; }
    }

    /* cnst_mstr_external_brid data */
    public class MasterBridgeData
    {
        public string cnst_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string counter { get; set; }
    }

    /* cnst_birth data */
    public class MasterBirthData
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_birth_mth_num { get; set; }
        public string cnst_birth_dy_num { get; set; }
        public string cnst_birth_yr_num { get; set; }
        public string arc_srcsys_cd { get; set; }
    }

    /* cnst_death data */
    public class MasterDeathData
    {
        public string cnst_mstr_id { get; set; }
        public string death_date { get; set; }
        public string arc_srcsys_cd { get; set; }
    }

    /* Compare Output Classes */
    public class CompareOutput
    {
        public string header { get; set; }
        public string MasterId1 { get; set; }
        public string Detail1 { get; set; }
        public string MasterId2 { get; set; }
        public string Detail2 { get; set; }
        public string MasterId3 { get; set; }
        public string Detail3 { get; set; }
        public string MasterId4 { get; set; }
        public string Detail4 { get; set; }
        public string MasterId5 { get; set; }
        public string Detail5 { get; set; }
    }

    /* Merge Input Classes */
    public class MergeInput
    {
        public List<string> MasterIds { get; set; }
        public string ConstituentType { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }
        public string CaseNumber { get; set; }
        public string PreferredMasterIdForLn { get; set; }

        public MergeInput()
        {
            CaseNumber = string.Empty;
            PreferredMasterIdForLn = string.Empty;
        }
    }

    //Class for master merge SP output
    public class MergeSPOutput
    {
        public string o_transaction_key { get; set; }
        public string o_outputMessage { get; set; }
    }

    /* Merge Conflict Input Classes */
    public class MergeConflictInput
    {
        public List<string> MasterIds { get; set; }
        public string ConstituentType { get; set; }
        public string InternalSourceSystemGroupId { get; set; }
        public string TrustedSource { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }
        public string CaseNumber { get; set; }
        public string PreferredMasterIdForLn { get; set; }

        public MergeConflictInput()
        {
            CaseNumber = string.Empty;
            PreferredMasterIdForLn = string.Empty;
        }
    }
}
