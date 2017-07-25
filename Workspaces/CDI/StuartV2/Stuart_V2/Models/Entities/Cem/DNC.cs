using Stuart_V2.Models.Entities.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Cem
{
    public class DncUploadInput
    {
        public string init_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string dnc_type { get; set; }
        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string locator_id { get; set; }
        public string prefix_nm { get; set; }
        public string prsn_frst_nm { get; set; }
        public string prsn_mid_nm { get; set; }
        public string prsn_lst_nm { get; set; }
        public string suffix_nm { get; set; }
        public string cnst_addr_line1 { get; set; }
        public string cnst_addr_line2 { get; set; }
        public string cnst_addr_city { get; set; }
        public string cnst_addr_state { get; set; }
        public string cnst_addr_zip4 { get; set; }
        public string cnst_addr_zip5 { get; set; }
        public string cnst_email_addr { get; set; }
        public string cnst_phn_num { get; set; }
        public string cnst_extn_phn_num { get; set; }
        public string created_by { get; set; }
        
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string notes { get; set; }
        public string load_id { get; set; }

        //public string status { get; set; }

        //org name
        public string cnst_org_nm { get; set; }
    }


    public class ListDncUploadInput
    {
        public List<DncUploadInput> dncUploadInputList { get; set; }
        public DncUploadedFileInfo dncFileInfo { get; set; }
    }


    public class DncUploadDetails
    {
        public ListDncUploadInput ListDncUploadInput { get; set; }
        public List<Stuart_V2.Models.Entities.Upload.ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
        public string userEmailId { get; set; }

        public DncUploadDetails()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            userEmailId = p.GetUserEmail(); 
        }
    }


    public class DncUploadedFileInfo
    {
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public double intFileSize { get; set; }
    }

    public class CemDncParams
    {
        public string loggedInUserName { get; set; }

    }


    public class FileDetails
    {
        public string name { get; set; }
        public double contentLength { get; set; }
        public string extension { get; set; }
        public Stream InputStream { get; set; }
    }



    public class DncValidationOutput
    {
        public List<bool> _invalidFirstName { get; set; }
        public List<bool> _invalidLastName { get; set; }
        public List<bool> _invalidMiddleName { get; set; }
        public List<bool> _invalidAddressLine1 { get; set; }
        public List<bool> _invaldAddressLine2 { get; set; }
        public List<bool> _invalidCity { get; set; }
        public List<bool> _invalidState { get; set; }
        public List<bool> _invalidZip { get; set; }
        public List<bool> _invalidEmailAddresses { get; set; }
        public List<bool> _invalidPhoneNumber { get; set; }

        public List<bool> _invalidSourceSystemCode { get; set; }
        public List<bool> _invalidSourceSystemId { get; set; }

        public List<bool> _invalidOrgName { get; set; }

        public List<bool> _invalidMasterIds { get; set; }
        // public List<string> _invalidStrMasterIds { get; set; }

        public List<bool> _invalidCommChannels { get; set; }
        public List<bool> _invalidLineOfServiceCodes { get; set; }


        public DncUploadedFileInfo dncUploadedFileOutputInfo { get; set; }

        public List<DncUploadInput> dncInvalidList { get; set; }
        public List<DncUploadInput> dncValidList { get; set; }


        public DncValidationOutput()
        {
            dncInvalidList = new List<DncUploadInput>();
            dncValidList = new List<DncUploadInput>();
        }
    }


    public class UploadSubmitOutput
    {
        public byte insertFlag { get; set; }
        public string insertOutput { get; set; }

        public List<ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
        public long transactionKey { get; set; }
    }




}