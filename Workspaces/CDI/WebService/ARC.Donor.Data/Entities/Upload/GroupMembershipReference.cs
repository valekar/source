using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Upload
{
    public class GroupMembershipReference
    {
        public long? grp_key {get;set;}
        public string grp_cd {get;set;}
        public string grp_nm {get;set;}
        public string grp_typ {get;set;}
        public string sub_grp_typ {get;set;}
        public string grp_assgnmnt_mthd {get;set;}
        public string grp_owner {get;set;}
        public long? trans_key {get;set;}
        public string user_id {get;set;}
        public string dw_srcsys_trans_ts {get;set;}
        public string row_stat_cd {get;set;}
        public string appl_src_cd {get;set;}
        public int? load_id {get;set;}
    }

    public class GroupMembershipReferenceInsertData
    {
        public string groupCode { get; set; }
        public string groupName { get; set; }
        public string groupType { get; set; }
        public string subGroupType { get; set; }
        public string groupAssignmentMethod { get; set; }
        public string groupOwnerMail { get; set; }
        public string LoggedInUser { get; set; }
    }

    public class GroupMembershipReferenceDataSPOutput
    {
        public long? groupKey { get;set;}
        public string transOutput { get;set;}
        public long? transKey { get; set; }
    }

    public class GroupMembershipEditReferenceParam
    {
        public string groupCode { get; set; }
        public string groupName { get; set; }
        public string groupType { get; set; }
        public string subGroupType { get; set; }
        public string assignmentMethod { get; set; }
        public string groupOwnerMail { get; set; }
        public long? groupKey { get; set; }
        public string LoggedInUser { get; set; }
    }

    public class GroupMembershipDeleteReferenceParam
    {
        public long? groupKey { get; set; }
        public string LoggedInUser { get; set; }
    }

    public class GroupMembershipReferenceDataWriteOutput
    {
        public string groupCode { get; set; }
        public string groupName { get; set; }
        public long? groupKey { get; set; }
        public string transOutput { get; set; }
        public long? transKey { get; set; }
    }
}
