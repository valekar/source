using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Upload
{
    public class GroupMembershipUploadInput
    {
        public string chpt_cd { get; set; }
        public string cnst_mstr_id { get; set; }
        public string cnst_prefix_nm { get; set; }
        public string cnst_first_nm { get; set; }
        public string cnst_middle_nm { get; set; }
        public string cnst_last_nm { get; set; }
        public string cnst_addr1_street1 { get; set; }
        public string cnst_addr1_street2 { get; set; }
        public string cnst_addr1_city { get; set; }
        public string cnst_addr1_state { get; set; }
        public string cnst_addr1_zip { get; set; }
        public string cnst_addr2_street1 { get; set; }
        public string cnst_addr2_street2 { get; set; }
        public string cnst_addr2_city { get; set; }
        public string cnst_addr2_state { get; set; }
        public string cnst_addr2_zip { get; set; }
        public string cnst_phn1_num { get; set; }
        public string cnst_phn2_num { get; set; }
        public string cnst_phn3_num { get; set; }
        public string cnst_email1_addr { get; set; }
        public string cnst_email2_addr { get; set; }
        public string job_title { get; set; }
        public string company_nm { get; set; }
        public string grp_cd { get; set; }
        public string grp_nm { get; set; }
        public string rm_ind { get; set; }
        public string notes { get; set; }
        public string stuart_cnst_grp_key { get; set; }
        //public string created_by { get; set; }
        //public string created_dt { get; set; }
        public string grp_mbrshp_strt_dt { get; set; }
        public string grp_mbrshp_end_dt { get; set; }
        public string losCode { get; set; }
        public string trans_key { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string upld_typ_key { get; set; }
        public string upld_typ_dsc { get; set; }
        public string nk_ecode { get; set; }
        public string status { get; set; }
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

    public class GroupMembershipValidationInput
    {
        public List<string> _masterIds { get; set; }
        public List<string> _chapterCodes { get; set; }
        public List<string> _groupNames { get; set; }
        public List<string> _groupCodes { get; set; }
        public string uploadedFileName { get; set; }
        public string uploadedFileExtension { get; set; }
        public double uploadedFileSize { get; set; }
    }

    public class GroupMembershipValidationOutput
    {
        public UploadedFileInfo UploadedFileOutputInfo { get; set; }

        public ListGroupMembershipUploadInput GroupMembershipInvalidList { get; set; }
        public ListGroupMembershipUploadInput GroupMembershipValidList { get; set; }

        public List<string> _validMasterIds { get; set; }
        public List<ChapterCodeValidationOutput> _validChapterCodes { get; set; }
        public List<GroupCodeValidationOutput> _validGroupCodes { get; set; }

        public List<string> _invalidMasterIds { get; set; }
        public List<string> _invalidChapterCodes { get; set; }
        public List<string> _invalidGroupCodes { get; set; }
    }

    public class ChapterCodeValidationOutput
    {
        public string chpt_cd { get; set; }
        public string appl_src_cd { get; set; }
    }


    public class GroupCodeValidationOutput
    {
        public string grp_cd { get; set; }
        public string grp_nm { get; set; }
    }

    public class ChapterUploadFileDetailsHelper
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
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
        public string userEmailId { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class GroupMembershipSubmitOutput
    {
        public byte insertFlag { get; set; }
        public string insertOutput { get; set; }

        public List<ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
        public long transactionKey { get;set;}
    }

    public class UploadCreateTransOutput 
    {
        public long transKey { get; set; }
        public string transOutput { get; set; }
    }

}
