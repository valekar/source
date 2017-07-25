using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Upload
{
    public class EmailOnlyUploadInput
    {
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

    public class UploadedEmailFileInfo
    {
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public double intFileSize { get; set; }
    }

    public class ListEmailOnlyUploadInput
    {
        public List<EmailOnlyUploadInput> EmailOnlyUploadInputList { get; set; }
        public UploadedEmailFileInfo UploadedEmailFileInputInfo { get; set; }
    }

    public class EmailOnlyUploadValidationInput
    {
        public List<string> _chapterCodes { get; set; }
        public List<string> _groupNames { get; set; }
        public List<string> _groupCodes { get; set; }

        public List<string> _emailAddress { get; set; } 

        public string uploadedFileName { get; set; }
        public string uploadedFileExtension { get; set; }
        public double uploadedFileSize { get; set; }
    }


    public class EmailOnlyUploadDetails
    {
        public ListEmailOnlyUploadInput ListEmailOnlyUploadDetails { get; set; }
        public List<ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
        public string userEmailId { get; set; }
    }

    public class EmailOnlyUploadValidationOutput
    {
        public UploadedEmailFileInfo UploadedEmailFileOutputInfo { get; set; }

        public ListEmailOnlyUploadInput EmailUploadInvalidList { get; set; }
        public ListEmailOnlyUploadInput EmailUploadValidList { get; set; }

        public List<ChapterCodeValidationOutput> _validChapterCodes { get; set; }
        public List<GroupCodeValidationOutput> _validGroupCodes { get; set; }

        public List<string> _invalidChapterCodes { get; set; }
        public List<string> _invalidGroupCodes { get; set; }

        //invalid email addresses
        public List<string> _invalidEmailAddresses { get; set; }
    }

    public class EmailOnlyUploadSubmitOutput
    {
        public byte insertFlag { get; set; }
        public string insertOutput { get; set; }

        public List<ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
        public long transactionKey { get; set; }
    }

    public class ComUnitKeyOutput
    {
        public string nk_ecode { get; set; }
        public int unit_key { get; set; }
    }


}
