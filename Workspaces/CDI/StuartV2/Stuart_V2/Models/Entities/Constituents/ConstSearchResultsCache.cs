using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Constituents
{
    [Serializable]
    public class ConsSearchResultsCache
    {
        public string IndexString { get; set; }
        public string constituent_id { get; set; }
        public string cnst_dsp_id { get; set; }
        public string sourceSystem { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string constituent_type { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string city { get; set; }
        public string state_cd { get; set; }
        public string zip { get; set; }
        public string request_transaction_key { get; set; }
        public string address { get; set; }


        public override bool Equals(Object o)
        {
            if (o == null)
            {
                return false;
            }
            if (this == o)
            {
                return true;
            }
            if (o is ConsSearchResultsCache)
            {
                ConsSearchResultsCache myO = (ConsSearchResultsCache)o;
                if (myO.constituent_id.Equals(this.constituent_id))
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.constituent_id.GetHashCode();
        }


    }


    [Serializable]
    public class UnmergeCache
    {
        public string IndexString { get; set; }
        public string cnst_nm { get; set; }
        public string address { get; set; }
        public string appl_src_cd { get; set; }
        public string cnst_act_ind { get; set; }
        public string cnst_mstr_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string load_id { get; set; }
        public string request_transaction_key { get; set; }
        public string row_stat_cd { get; set; }
        public string source_system_cd { get; set; }
        public string source_system_id { get; set; }
        public string srch_srcsys_id { get; set; }
        public string trans_status { get; set; }
        public string cnst_dsp_id { get; set; }
        public string constituent_type { get; set; }


        public override bool Equals(Object o)
        {
            if (o == null)
            {
                return false;
            }
            if (this == o)
            {
                return true;
            }
            if (o is UnmergeCache)
            {
                UnmergeCache myO = (UnmergeCache)o;
                if (myO.source_system_cd.Equals(this.source_system_cd) && myO.source_system_id.Equals(this.source_system_id))
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.cnst_mstr_id.GetHashCode();
        }


    }



    [Serializable]
    public class MergeConflict
    {
        public string groupID { set; get; }
        public string mergeConflictFlag {set;get;}
        public string caseKey {set;get;}
        public List<locInfoResearchData> locInfoResearchData { set; get; }

        public MergeConflict()
        {
            locInfoResearchData = new List<locInfoResearchData>();
        }

    }

    [Serializable]
    public class locInfoResearchData{
        public string addr_line {set;get; }
        public string appl_src_cd { set; get; }
        public string assessmnt_email_addr { set; get; }
        public string case_key { set; get; }
        public string case_num { set; get; }
        public string chpt_srcsys { set; get; }
        public string city { set; get; }
        public string cnst_mstr_id { set; get; }
        public string cnst_typ { set; get; }
        public string dw_srcsys_trans_ts { get; set; }
        public string email_addr { get; set; }
        public string end_dt { set; get; }
        public string ent_org_id { set; get; }
        public string ent_org_name { set; get; }
        public string ent_org_type { set; get; }
        public string ext_assessmnt_cd { set; get; }
        public string ext_hygiene_result { set; get; }
        public string f_nm { get; set; }
        public string fortune_num { get; set; }
        public string from_dt { get; set; }
        public string int_assessmnt_cd { get; set; }
        public string key_acct_bio {set;get;}
        public string key_acct_ent { set; get; }
        public string key_acct_fr { set; get; }
        public string key_acct_hs { get; set; }
        public string l_nm { get; set; }
        public string load_id { get; set; }
        public string locator_id { get; set; }
        public string naics { get; set; }
        public string org_nm { get; set; }
        public string phn_num { get; set; }
        public string row_stat_cd { get; set; }
        public string source_code { get; set; }
        public string source_id { get; set; }
        public string srch_criteria_key { get; set; }
        public string srch_typ { get; set; }
        public string srch_usr { get; set; }
        public string srcsys { get; set; }
        public string srcsys_id { get; set; }
        public string state_cd { get; set; }
        public string strt_dt { get; set; }
        public string to_dt { get; set; }
        public string trans_key { get; set; }
        public string trans_stat { get; set; }
        public string trans_typ { get; set; }
        public string user_id { get; set; }
        public string usr_nm { get; set; }
        public string zip { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (this == obj)
            {
                return true;
            }
            if (obj is locInfoResearchData)
            {
                locInfoResearchData loc = (locInfoResearchData)obj;

                if (loc.cnst_mstr_id.Equals(this.cnst_mstr_id))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.cnst_mstr_id.GetHashCode();
        }

    }
}


