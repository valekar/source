using ARC.Donor.Business.Upload;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DonorWebservice.Controllers
{
    [Authorize]
    [RoutePrefix("api/Upload/message/preference")]
    public class MsgPrefUploadController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        /// <summary>
        /// This is used to validate a record if the record is valid or not.Pass the relevant parameters for verifying
        /// </summary>
        /// <param name="listMsgPrefUploadInput"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("validate")]
        [ResponseType(typeof(MsgPrefValidationOutput))]
        public IHttpActionResult PostMsgPrefValidate(ListMsgPrefUploadInput listMsgPrefUploadInput)
        {

            List<string> _addressLine1 = null;
            List<string> _addressLine2 = null;
            List<string> _city = null;
            List<string> _state = null;
            List<string> _zip = null;
            List<string> _emailAddress = null;
            List<string> _phoneNumber = null;

            List<string> _sourceSystemCode = null;
            List<string> _sourceSystemId = null;
            List<string> _masterId = null;

            try
            {
                ARC.Donor.Service.Upload.MsgPrefValidation ts = new ARC.Donor.Service.Upload.MsgPrefValidation();


                MsgPrefValidationInput msgPrefValidationInput = new MsgPrefValidationInput();


                if (listMsgPrefUploadInput != null)
                {
                    if (listMsgPrefUploadInput.msgPrefUploadInputList != null)
                    {
                        _addressLine1 = listMsgPrefUploadInput.msgPrefUploadInputList.GroupBy(x => x.cnst_addr_line1).Select(y => y.First().cnst_addr_line1).ToList();
                        _addressLine2 = listMsgPrefUploadInput.msgPrefUploadInputList.GroupBy(x => x.cnst_addr_line2).Select(y => y.First().cnst_addr_line2).ToList();
                        _city = listMsgPrefUploadInput.msgPrefUploadInputList.GroupBy(x => x.cnst_addr_city).Select(y => y.First().cnst_addr_city).ToList();
                        _state = listMsgPrefUploadInput.msgPrefUploadInputList.GroupBy(x => x.cnst_addr_state).Select(y => y.First().cnst_addr_state).ToList();
                        _zip = listMsgPrefUploadInput.msgPrefUploadInputList.GroupBy(x => x.cnst_addr_zip5).Select(y => y.First().cnst_addr_zip5).ToList();
                        _emailAddress = listMsgPrefUploadInput.msgPrefUploadInputList.GroupBy(x => x.cnst_email_addr).Select(y => y.First().cnst_email_addr).ToList();
                        _phoneNumber = listMsgPrefUploadInput.msgPrefUploadInputList.GroupBy(x => x.cnst_phn_num).Select(y => y.First().cnst_phn_num).ToList();

                        _sourceSystemCode = listMsgPrefUploadInput.msgPrefUploadInputList.GroupBy(x => x.arc_srcsys_cd).Select(y => y.First().arc_srcsys_cd).ToList();
                        _sourceSystemId = listMsgPrefUploadInput.msgPrefUploadInputList.GroupBy(x => x.cnst_srcsys_id).Select(y => y.First().cnst_srcsys_id).ToList();
                        _masterId = listMsgPrefUploadInput.msgPrefUploadInputList.GroupBy(x => x.init_mstr_id).Select(y => y.First().init_mstr_id).ToList();
                    }
                    else
                    {
                        _addressLine1 = new List<string>();
                        _addressLine2 = new List<string>();
                        _city = new List<string>();
                        _state = new List<string>();
                        _zip = new List<string>();
                        _emailAddress = new List<string>();
                        _phoneNumber = new List<string>();
                        _sourceSystemCode = new List<string>();
                        _sourceSystemId = new List<string>();
                        _masterId = new List<string>();

                    }
                }
                else
                {
                    _addressLine1 = new List<string>();
                    _addressLine2 = new List<string>();
                    _city = new List<string>();
                    _state = new List<string>();
                    _zip = new List<string>();
                    _emailAddress = new List<string>();
                    _phoneNumber = new List<string>();
                    _sourceSystemCode = new List<string>();
                    _sourceSystemId = new List<string>();
                    _masterId = new List<string>();
                }



                msgPrefValidationInput._addressLine1 = _addressLine1;
                msgPrefValidationInput._addressLine2 = _addressLine2;
                msgPrefValidationInput._city = _city;
                msgPrefValidationInput._zip = _zip;
                msgPrefValidationInput._state = _state;
                msgPrefValidationInput._emailAddress = _emailAddress;
                msgPrefValidationInput._phoneNumber = _phoneNumber;

                msgPrefValidationInput._sourceSystemCode = _sourceSystemCode;
                msgPrefValidationInput._sourceSystemId = _sourceSystemId;
                msgPrefValidationInput._masterIds = _masterId;


                if (listMsgPrefUploadInput != null)
                {
                    if (listMsgPrefUploadInput.msgPrefFileInfo != null)
                    {
                        msgPrefValidationInput.uploadedFileName = listMsgPrefUploadInput.msgPrefFileInfo.fileName;
                        msgPrefValidationInput.uploadedFileExtension = listMsgPrefUploadInput.msgPrefFileInfo.fileExtension;
                        msgPrefValidationInput.uploadedFileSize = listMsgPrefUploadInput.msgPrefFileInfo.intFileSize;
                    }
                    else
                    { //If ListGroupMembershipUploadInput.UploadedFileInputInfo is null - instantiate the variables to be the minimal ones
                        msgPrefValidationInput.uploadedFileName = string.Empty;
                        msgPrefValidationInput.uploadedFileExtension = string.Empty;
                        msgPrefValidationInput.uploadedFileSize = -1;
                    }
                }
                else
                {//If ListGroupMembershipUploadInput is null - instantiate the variables to be the minimal ones
                    msgPrefValidationInput.uploadedFileName = string.Empty;
                    msgPrefValidationInput.uploadedFileExtension = string.Empty;
                    msgPrefValidationInput.uploadedFileSize = -1;
                }

                var results = ts.validateMsgPrefRecords(msgPrefValidationInput, listMsgPrefUploadInput);
                //var result = ts.validateRecords(listDncUploadInput);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR MsgPrefUploadController :: PostMsgPrefValidate : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("PostMsgPrefUpload")]
        public IHttpActionResult PostMsgPrefUpload(MsgPrefUploadDetails msgPrefUploadDetails)
        {
            try
            {
                ARC.Donor.Service.Upload.MsgPrefUploadServices ts = new ARC.Donor.Service.Upload.MsgPrefUploadServices();

                //Does not care for the returned value
                var output = ts.msgPrefUploadData(msgPrefUploadDetails);
                return Ok(output);
            }
            catch (Exception ex)
            {
                _msg = "ERROR MsgPrefUploadController :: PostMsgPrefUpload : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
            


        }

    }
}
