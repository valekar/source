using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Cem
{
    public class GroupMembership
    {
        public string grp_key { get; set; }
        public string cnst_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string line_of_service_cd { get; set; }
        public string grp_mbrshp_eff_strt_dt { get; set; }
        public string grp_mbrshp_eff_end_dt { get; set; }
        public string assgnmnt_mthd { get; set; }
        //public string created_dt { get; set; }
        //public string created_by { get; set; }
        public string grp_strt_ts { get; set; }
        public string grp_end_dt { get; set; }
        public string grp_cd { get; set; }
        public string grp_nm { get; set; }
        public string grp_typ { get; set; }
        public string grp_owner { get; set; }
        public string trans_key { get; set; }
        public string act_ind { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
        public string transNotes { get; set; }
        //for Missy
        public string distinct_records_count { get; set; }

        public GroupMembership()
        {
            this.transNotes = string.Empty;
        }


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
            if (o is GroupMembership)
            {
                GroupMembership myO = (GroupMembership)o;
                if (myO.grp_key.Equals(this.grp_key) && myO.cnst_mstr_id.Equals(this.cnst_mstr_id)
                     && myO.line_of_service_cd.Equals(this.line_of_service_cd))
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

    public class ConstituentChapterGroupInput
    {
        public string RequestType { get; set; }
        public Int64 MasterID { get; set; }
        public string UserName { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public Int64? CaseNumber { get; set; }
        public Int64? OldGroupKey { get; set; }
        public string OldAssignmentMethod { get; set; }
        public Int64? GroupKey { get; set; }
        public string AssignmentMethod { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }

        public ConstituentChapterGroupInput()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            UserName = p.GetUserName(); //p.Identity.Name;
            ConstType = string.Empty;
           // Notes = "This is a test";
           // CaseNumber = 0;
            OldAssignmentMethod = string.Empty;
            AssignmentMethod = string.Empty;
        }
    }

    public class ConstituentChapterGroupOutput
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }

    //changed Group membership
    public class GroupMembershipInput
    {
        public string i_req_typ { get; set; }
        public string i_mstr_id { get; set; }
        public string i_line_of_service_cd { get; set; }
        public string i_grp_mbrshp_eff_strt_dt { get; set; }
        public string i_grp_mbrshp_eff_end_dt { get; set; }
        public string i_user_id { get; set; }
        public string i_cnst_typ { get; set; }
        public string i_notes { get; set; }
        public string i_case_seq_num { get; set; }
        public string i_bk_group_key { get; set; }
        public string i_bk_assgnmnt_mthd { get; set; }
        public string i_bk_line_of_service_cd { get; set; }
        public string i_bk_arc_srcsys_cd { get; set; }
        public string i_new_group_key { get; set; }
        public string i_new_assgnmnt_mthd { get; set; }
        public string i_new_line_of_service_cd { get; set; }
        public string i_new_arc_srcsys_cd { get; set; }

        public GroupMembershipInput()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            this.i_user_id = p.GetUserName();
        }
    }

    public class GroupMembershipOutput
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }

    }
}