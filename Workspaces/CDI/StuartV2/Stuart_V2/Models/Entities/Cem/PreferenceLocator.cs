using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Cem
{
    public class PreferenceLocator
    {
        public string cnst_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string line_of_service_cd { get; set; }
        public string pref_loc_typ { get; set; }
        public string pref_loc_id { get; set; }
        public string notes { get; set; }
        public string status { get; set; }
        public string created_by { get; set; }
        public string created_ts { get; set; }
        public string cnst_pref_loc_strt_ts { get; set; }
        public string cnst_pref_loc_end_ts { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string is_previous { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string dw_trans_ts { get; set; }
        public string transNotes { get; set; }
        public string inactive_ind { get; set; }
        public string trans_status { get; set; }
        public string distinct_records_count { get; set; }
        public string transaction_key { get; set; }


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
            if (o is PreferenceLocator)
            {
                PreferenceLocator myO = (PreferenceLocator)o;
                if (myO.pref_loc_id.Equals(this.pref_loc_id) && myO.pref_loc_typ.Equals(this.pref_loc_typ) && 
                    myO.line_of_service_cd.Equals(this.line_of_service_cd))
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.pref_loc_id.GetHashCode();
        }

    }

    public class PreferenceLocatorInput
    {
        public Int64 masterId { get; set; }
        public string constituentType { get; set; }
        public string lineOfService { get; set; }
        public string notes { get; set; }
        public string createdBy { get; set; }
        public string userId { get; set; }
        public string oldSourceSystemCode { get; set; }
        public string oldPrefLocType { get; set; }
        public Int64 oldPrefLocId { get; set; }
        public string newSourceSystemCode { get; set; }
        public string newPrefLocType { get; set; }
        public Int64 newPrefLocId { get; set; }
        public Int64 caseNo { get; set; }


        public PreferenceLocatorInput()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            this.masterId = 0;
            this.constituentType = string.Empty;
            this.lineOfService = string.Empty;
            this.notes = string.Empty;
            this.createdBy = p.GetUserName();
            this.userId = p.GetUserName();
            this.oldSourceSystemCode = string.Empty;
            this.oldPrefLocType = string.Empty;
            this.oldPrefLocId = 0;
            this.newSourceSystemCode = string.Empty;
            this.newPrefLocType = string.Empty;
            this.newPrefLocId = 0;
            this.caseNo = 0;            
        }

    }



    public class PreferenceLocatorOutput
    {
        public string outputMessage { get; set; }
        public long transactionKey { get; set; }
    }
}