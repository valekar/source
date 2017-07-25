using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities.Upload
{
    public class GroupMembershipUploadInput
    {
        public string chpt_cd {get;set;}
        public string cnst_mstr_id {get;set;}
        public string cnst_prefix_nm {get;set;}
        public string cnst_first_nm {get;set;}
        public string cnst_middle_nm {get;set;}
        public string cnst_last_nm {get;set;}
        public string cnst_addr1_street1 {get;set;}
        public string cnst_addr1_street2 {get;set;}
        public string cnst_addr1_city {get;set;}
        public string cnst_addr1_state {get;set;}
        public string cnst_addr1_zip {get;set;}
        public string cnst_addr2_street1 {get;set;}
        public string cnst_addr2_street2 {get;set;}
        public string cnst_addr2_city {get;set;}
        public string cnst_addr2_state {get;set;}
        public string cnst_addr2_zip {get;set;}
        public string cnst_phn1_num {get;set;}
        public string cnst_phn2_num {get;set;}
        public string cnst_phn3_num {get;set;}
        public string cnst_email1_addr {get;set;}
        public string cnst_email2_addr {get;set;}
        public string job_title {get;set;}
        public string company_nm {get;set;}
        public string grp_cd {get;set;}
        public string grp_nm {get;set;}
        public string rm_ind {get;set;}
        public string notes {get;set;}
        public string stuart_cnst_grp_key {get;set;}
        //public string created_by {get;set;}
        //public string created_dt {get;set;}
        public string grp_mbrshp_strt_dt { get;set;}
        public string grp_mbrshp_end_dt { get; set; }
        public string losCode { get; set; }
        public string trans_key {get;set;}
        public string dw_srcsys_trans_ts {get;set;}
        public string row_stat_cd {get;set;}
        public string appl_src_cd {get;set;}
        public string load_id {get;set;}
        public string upld_typ_key {get;set;}
        public string upld_typ_dsc {get;set;}
        public string nk_ecode { get; set; }
        public string status {get;set;}

    }

    public class ListGroupMembershipUploadInput
    {
        public List<GroupMembershipUploadInput> GroupMembershipUploadInputList { get; set; }
        public UploadedFileInfo UploadedFileInputInfo { get; set; }
    }

    public class UploadedFileInfo
    {
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public double intFileSize { get; set; }
    }

    public class ChapterUploadFileDetailsHelper
    {
        public string UserName { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public string FileType { get; set; }
        public string FileExtention { get; set; }
        public int BusinessType { get; set; }
        public string UploadStart { get; set; }
        public string UploadEnd { get; set; }
        public string UploadStatus { get; set; }
        public string TransactionKey { get; set; }
    }

    public class GroupMembershipUploadDetails
    {
        public ListGroupMembershipUploadInput ListGroupMembershipUploadDetailsInput { get; set; }
        public List<ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
    }

    public class GroupMembershipSubmitOutput
    {
        public byte insertFlag { get; set; }
        public string insertOutput { get; set; }

        public List<ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
        public long transactionKey { get; set; }
    }

    public class GroupMembershipUploadParams
    {
        public string loggedInUserName { get; set; }
        public string chapterCode { get; set; }
        public string groupCode { get; set; }
        public string groupName { get; set; }
        public string losCode { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string endDate { get; set; }
        public string startDate { get; set; }
    }

    public class GroupMembershipReferenceInsertData
    {
        public string groupCode { get; set; }
        public string groupName { get; set; }
        public string groupType { get; set; }
        public string subGroupType { get; set; }
        public string groupAssignmentMethod { get; set; }
        public string groupOwnerMail { get; set; }
    }

    public class GroupMembershipDeleteReferenceParam
    {
        public long? groupKey { get; set; }
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
    }

    public class GroupMembershipReferenceDataOutput
    {
        public string groupCode { get; set; }
        public string groupName { get; set; }
        public long? groupKey { get; set; }
        public string transOutput { get; set; }
        public long? transKey { get; set; }
    }
}