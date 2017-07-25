using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities
{
    public class PickListData
    {
        public string picklist_typ { get; set; }
        public string picklist_typ_key { get; set; }
        public string picklist_typ_cd { get; set; }
        public string picklist_typ_dsc { get; set; }
        public string ref_order { get; set; }
        public string dw_trans_ts { get; set; }
    }

    public class DropdownData
    {
        public string id { get; set; }
        public string value { get; set; }
    }

    public class GroupTypeData
    {
        public string GroupKey { get; set; }
        public string GroupDescription { get; set; }
        public string UserName { get; set; }
        public string CreatedDate { get; set; }
        public string RowStatCode { get; set; }
    }

    public class SubGroupTypeData
    {
        public string SubGroupKey { get; set; }
        public string GroupKeyMap { get; set; }
        public string SubGroupDescription { get; set; }
        public string UserName { get; set; }
        public string CreatedDate { get; set; }
        public string RowStatCode { get; set; }
    }


    public class GroupMembershipReferenceDataWriteOutput
    {
        public string groupCode { get; set; }
        public string groupName { get; set; }
        public long? groupKey { get; set; }
        public string transOutput { get; set; }
        public long? transKey { get; set; }
    }


    public class PicklistDataHelper
    {
        public string picklist_typ { get; set; }
        public string picklist_typ_key { get; set; }
        public string picklist_typ_cd { get; set; }
        public string picklist_typ_dsc { get; set; }
        public string ref_order { get; set; }
        public string dw_trans_ts { get; set; }

        public PicklistDataHelper()
        {

        }

        public PicklistDataHelper(string grpName)
        {
            this.picklist_typ = grpName;
            this.dw_trans_ts = DateTime.Now.ToString();
        }

    }
}
