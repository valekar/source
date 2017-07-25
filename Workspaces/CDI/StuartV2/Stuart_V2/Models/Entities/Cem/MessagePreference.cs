using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Cem
{

    public class MessagePrefParams
    {
        public string loggedInUserName { get; set; }

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
        // public List<string> _invalidStrMasterIds { get; set; }

        public List<bool> _invalidCommChannels { get; set; }
        public List<bool> _invalidLineOfServiceCodes { get; set; }

        //added later
        public List<bool> _invalidMsgPrefType { get; set; }
        public List<bool> _invalidMsgPrefValue { get; set; }

        public DncUploadedFileInfo dncUploadedFileOutputInfo { get; set; }

        public List<MsgPrefUploadInput> msgPrefInvalidList { get; set; }
        public List<MsgPrefUploadInput> msgPrefValidList { get; set; }


        public MsgPrefValidationOutput()
        {
            msgPrefInvalidList = new List<MsgPrefUploadInput>();
            msgPrefValidList = new List<MsgPrefUploadInput>();
        }
    }

    public class MsgPrefUploadedFileInfo
    {
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public double intFileSize { get; set; }
    }


    public class ListMsgPrefUploadInput
    {
        public List<MsgPrefUploadInput> msgPrefUploadInputList { get; set; }
        public MsgPrefUploadedFileInfo msgPrefFileInfo { get; set; }
    }


    public class MsgPrefUploadDetails
    {
        public ListMsgPrefUploadInput ListMsgPrefUploadInput { get; set; }
        public List<Stuart_V2.Models.Entities.Upload.ChapterUploadFileDetailsHelper> ListChapterUploadFileDetailsInput { get; set; }
        public string userEmailId { get; set; }

        public MsgPrefUploadDetails()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            userEmailId = p.GetUserEmail(); 
        }

    }





    public class MsgPrefUploadInput
    {
        public string init_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string msg_prefc_typ { get; set; }
        public string msg_prefc_val { get; set; }

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
        public string cnst_extn_phn_num { get; set; }
        public string cnst_phn_num { get; set; }
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

    // for CEM Surfacing
    public class MessagePreference
    {
        public string msg_pref_mstr_id { get; set; }
        public string init_mstr_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string line_of_service_cd { get; set; }
        public string comm_chan { get; set; }
        public string msg_pref_typ { get; set; }
        public string msg_pref_val { get; set; }
        public string msg_pref_exp_dt { get; set; }
        public string comm_typ { get; set; }
        public string msg_pref_strt_ts { get; set; }
        public string msg_pref_end_ts { get; set; }
        public string transaction_key { get; set; }
        public string user_id { get; set; }
        public string notes { get; set; }
        public string status { get; set; }
        public string srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string dw_trans_ts { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string unique_trans_key { get; set; }
        public string is_previous { get; set; }
        public string transNotes { get; set; }
        public string distinct_records_count { get; set; }


        public MessagePreference()
        {
            this.distinct_records_count = "0";
        }

        public override bool Equals(Object o)
        {
            if (o == null)
            {
                return false;
            }
            if (this == o)
            {
                return true;
            }
            if (o is MessagePreference)
            {
                MessagePreference myO = (MessagePreference)o;
                if (myO.msg_pref_mstr_id.Equals(this.msg_pref_mstr_id) && myO.arc_srcsys_cd.Equals(this.arc_srcsys_cd) && myO.comm_chan.Equals(this.comm_chan)
                     && myO.line_of_service_cd.Equals(this.line_of_service_cd) && myO.msg_pref_typ.Equals(this.msg_pref_typ) &&
                     myO.msg_pref_val.Equals(this.msg_pref_val))
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.msg_pref_mstr_id.GetHashCode();
        }
    }


    public class MessagePreferenceInput
    {
        public string i_req_typ { get; set; }
        public Int64 i_mstr_id { get; set; }
        public string i_cnst_typ { get; set; }
        public Int64 i_case_num_seq { get; set; }
        public string i_notes { get; set; }
        public string i_created_by { get; set; }
        public string i_user_id { get; set; }
        public string i_msg_pref_exp_dt { get; set; }
        public string i_bk_arc_srcsys_cd { get; set; }
        public string i_bk_msg_pref_typ { get; set; }
        public string i_bk_msg_pref_val { get; set; }
        public string i_bk_msg_pref_comm_chan { get; set; }
        public string i_bk_msg_pref_comm_chan_typ { get; set; }
        public string i_bk_line_of_service { get; set; }
        public string i_new_arc_srcsys_cd { get; set; }
        public string i_new_msg_pref_typ { get; set; }
        public string i_new_msg_pref_val { get; set; }
        public string i_new_msg_pref_comm_chan { get; set; }
        public string i_new_msg_pref_comm_chan_typ { get; set; }
        public string i_new_line_of_service { get; set; }

        public MessagePreferenceInput()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            this.i_user_id = p.GetUserName();
        }
    }


}