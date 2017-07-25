using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities.Upload
{
    public class NameAndEmailUserUploadParams
    {
        public string loggedInUserName { get; set; }
        public string chapterCode { get; set; }
        public string groupCode { get; set; }
        public string groupName { get; set; }
        public string endDate { get; set; }
        public string startDate { get; set; }
    }

    public class NameAndEmailUploadInput
    {

        public string cnst_mstr_id { get; set; }
        public string cnst_prefix_nm { get; set; }
        public string cnst_first_nm { get; set; }
        public string cnst_middle_nm { get; set; }
        public string cnst_last_nm { get; set; }
        public string chpt_cd { get; set; }
        public string cnst_email_addr { get; set; }
        public string grp_cd { get; set; }
        public string grp_nm { get; set; }
        public string rm_ind { get; set; }
        public string notes { get; set; }
        public string grp_mbrshp_strt_dt { get; set; }
        public string grp_mbrshp_end_dt { get; set; }
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

    public class UploadedNameAndEmailFileInfo
    {
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public double intFileSize { get; set; }
    }

    public class ListNameAndEmailUploadInput
    {
        public List<NameAndEmailUploadInput> NameAndEmailUploadInputList { get; set; }
        public UploadedNameAndEmailFileInfo UploadedNameAndEmailFileInputInfo { get; set; }
    }

    public class NameAndEmailUploadJsonFileDetailsHelper
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

    public class NameAndEmailUploadDetails
    {
        public ListNameAndEmailUploadInput ListNameAndEmailUploadDetails { get; set; }
        public List<NameAndEmailUploadJsonFileDetailsHelper> ListNameAndEmailUploadJsonFileDetailsInput { get; set; }
    }

    public class NameAndEmailSubmitOutput
    {
        public byte insertFlag { get; set; }
        public string insertOutput { get; set; }

        public List<NameAndEmailUploadJsonFileDetailsHelper> ListNameAndEmailUploadJsonFileDetailsInput { get; set; }
        public long transactionKey { get; set; }
    }
}