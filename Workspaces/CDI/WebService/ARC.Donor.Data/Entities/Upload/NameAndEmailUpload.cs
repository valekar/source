using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Upload
{

    public class NameAndEmailUploadValidationInput
    {
        public List<string> _chapterCodes { get; set; }
        public List<string> _groupNames { get; set; }
        public List<string> _groupCodes { get; set; }
        public string uploadedFileName { get; set; }
        public string uploadedFileExtension { get; set; }
        public double uploadedFileSize { get; set; }
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

   

    public class NameAndEmailUploadDetails
    {
        public ListNameAndEmailUploadInput ListNameAndEmailUploadDetails { get; set; }
        public List<ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
    }

    public class NameAndEmailUploadValidationOutput
    {
        public UploadedNameAndEmailFileInfo UploadedNameAndEmailFileOutputInfo { get; set; }

        public ListNameAndEmailUploadInput NameAndEmailUploadInvalidList { get; set; }
        public ListNameAndEmailUploadInput NameAndEmailUploadValidList { get; set; }

        public List<ChapterCodeValidationOutput> _validChapterCodes { get; set; }
        public List<GroupCodeValidationOutput> _validGroupCodes { get; set; }

        public List<string> _invalidChapterCodes { get; set; }
        public List<string> _invalidGroupCodes { get; set; }
        //invalid email addresses
        public List<string> _invalidEmailAddresses { get; set; }

        public NameAndEmailUploadValidationOutput()
        {
            _invalidEmailAddresses = new List<string>();
        }

    }

    public class NameAndEmailUploadSubmitOutput
    {
        public byte insertFlag { get; set; }
        public string insertOutput { get; set; }

        public List<ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
        public long transactionKey { get; set; }
    }
}
