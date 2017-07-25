using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities
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
}
