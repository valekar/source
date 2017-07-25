using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Upload
{
    public class MsgPrefUploadParams
    {
        public string init_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string msg_prefc_typ { get; set; }
        public string msg_prefc_val { get; set; }

        public string msg_prefc_submissn_typ { get; set; }

        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string comm_typ { get; set; }
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
        public string msg_pref_exp_ts { get; set; }

        public string created_by { get; set; }

        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string notes { get; set; }
        public string load_id { get; set; }
        //org name
        public string cnst_org_nm { get; set; }
    }


    public class MsgPrefUploadDetails
    {
        public ListMsgPrefUploadInput ListMsgPrefUploadInput { get; set; }
        public List<ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
    }


    public class ListMsgPrefUploadInput
    {
        public List<MsgPrefUploadParams> msgPrefUploadInputList { get; set; }
        public MsgPrefUploadedFileInfo msgPrefFileInfo { get; set; }
    }

    public class ListMsgPrefUploadOutput
    {
        public List<MsgPrefUploadParams> listMsgPrefUploadOutput { get; set; }
    }


    public class MsgPrefUploadedFileInfo
    {
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public double intFileSize { get; set; }
    }


    public class MsgPrefValidationInput
    {
        public List<string> _addressLine1 { get; set; }
        public List<string> _addressLine2 { get; set; }
        public List<string> _city { get; set; }
        public List<string> _state { get; set; }
        public List<string> _zip { get; set; }
        public List<string> _emailAddress { get; set; }
        public List<string> _phoneNumber { get; set; }

        public List<string> _sourceSystemCode { get; set; }
        public List<string> _sourceSystemId { get; set; }
        public List<string> _masterIds { get; set; }
        public List<string> _commChannels { get; set; }
        public List<string> _lineOfServiceCodes { get; set; }
        public List<string> _msgPrefTypes { get; set; }
        public List<string> _msgPrefValues { get; set; }

        public string uploadedFileName { get; set; }
        public string uploadedFileExtension { get; set; }
        public double uploadedFileSize { get; set; }
    }


    public class MsgPrefValidationOutput
    {
        public List<bool> _invalidFirstName { get; set; }
        public List<bool> _invalidLastName { get; set; }
        public List<bool> _invalidAddressLine1 { get; set; }
        public List<bool> _invaldAddressLine2 { get; set; }
        public List<bool> _invalidCity { get; set; }
        public List<bool> _invalidState { get; set; }
        public List<bool> _invalidZip { get; set; }
        public List<bool> _invalidEmailAddresses { get; set; }
        public List<bool> _invalidPhoneNumber { get; set; }

        public List<bool> _invalidSourceSystemCode { get; set; }
        public List<bool> _invalidSourceSystemId { get; set; }

        public List<bool> _invalidMasterIds { get; set; }


        public List<bool> _invalidCommChannels { get; set; }
        public List<bool> _invalidLineOfServiceCodes { get; set; }
        public List<bool> _invalidMsgPrefTypes { get; set; }
        public List<bool> _invalidMsgPrefValues { get; set; }

        public MsgPrefUploadedFileInfo msgPrefUploadedFileOutputInfo { get; set; }

        public List<MsgPrefUploadParams> dncInvalidList { get; set; }
        public List<MsgPrefUploadParams> dncValidList { get; set; }


        public MsgPrefValidationOutput()
        {
            dncInvalidList = new List<MsgPrefUploadParams>();
            dncValidList = new List<MsgPrefUploadParams>();
        }
    }

    public class MsgPrefSubmitOutput
    {
        public byte insertFlag { get; set; }
        public string insertOutput { get; set; }

        public List<ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
        public long transactionKey { get; set; }
    }



    public class MsgPrefRefCode
    {
        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string msg_prefc_typ { get; set; }
        public string msg_prefc_val { get; set; }
        public string comm_typ { get; set; }
    }

}
