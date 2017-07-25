using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARC.Donor.Business;
using ARC.Donor.Service;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ARC.Donor.Business.Upload;
using System.Web.Script.Serialization;
using System.Web.Configuration;
using System.Diagnostics;
using NLog;
using System.Web.Http.Description;
using ARC.Donor.Business.Transaction;

namespace DonorWebservice.Controllers
{
    [Authorize]
    [RoutePrefix("api/Upload")]
    public class UploadController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        /// <summary>
        /// Used for validating a group memberhsip record.Pass the group membership record as a list
        /// </summary>
        /// <param name="ListGroupMembershipUploadInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("groupmembershipvalidate")]
        [ResponseType(typeof(GroupMembershipValidationOutput))]
        public IHttpActionResult PostGroupMembershipValidate(ListGroupMembershipUploadInput ListGroupMembershipUploadInput)
        {
            List<string> masterIds = null;
            List<string> chapterCodes = null;
            List<string> groupCodes = null;

            try
            {
                ARC.Donor.Service.Upload.UploadValidationServices ts = new ARC.Donor.Service.Upload.UploadValidationServices();
                if (ListGroupMembershipUploadInput != null)
                {
                    if (ListGroupMembershipUploadInput.GroupMembershipUploadInputList != null)
                    {
                        masterIds = ListGroupMembershipUploadInput.GroupMembershipUploadInputList.GroupBy(x => x.cnst_mstr_id).Select(y => y.First().cnst_mstr_id).ToList();
                        chapterCodes = ListGroupMembershipUploadInput.GroupMembershipUploadInputList.GroupBy(x => x.chpt_cd).Select(y => y.First().chpt_cd).ToList();
                        groupCodes = ListGroupMembershipUploadInput.GroupMembershipUploadInputList.GroupBy(x => x.grp_cd).Select(y => y.First().grp_cd).ToList();
                    }
                    else
                    {
                        //If GroupMembershipUploadInputList is null - instantiate the lists
                        masterIds = new List<string>();
                        chapterCodes = new List<string>();
                        groupCodes = new List<string>();
                    }
                }
                else{ //If ListGroupMembershipUploadInput is null - instantiate the lists
                        masterIds = new List<string>();
                        chapterCodes = new List<string>();
                        groupCodes = new List<string>();
                }

                GroupMembershipValidationInput gmvi = new GroupMembershipValidationInput();
                gmvi._masterIds = masterIds;
                gmvi._chapterCodes = chapterCodes;
                gmvi._groupCodes = groupCodes;

                if (ListGroupMembershipUploadInput != null)
                {
                    if (ListGroupMembershipUploadInput.UploadedFileInputInfo != null)
                    {
                        gmvi.uploadedFileName = ListGroupMembershipUploadInput.UploadedFileInputInfo.fileName;
                        gmvi.uploadedFileExtension = ListGroupMembershipUploadInput.UploadedFileInputInfo.fileExtension;
                        gmvi.uploadedFileSize = ListGroupMembershipUploadInput.UploadedFileInputInfo.intFileSize;
                    }
                    else
                    { //If ListGroupMembershipUploadInput.UploadedFileInputInfo is null - instantiate the variables to be the minimal ones
                        gmvi.uploadedFileName = string.Empty;
                        gmvi.uploadedFileExtension = string.Empty;
                        gmvi.uploadedFileSize = -1;
                    }
                }
                else
                {//If ListGroupMembershipUploadInput is null - instantiate the variables to be the minimal ones
                    gmvi.uploadedFileName = string.Empty;
                    gmvi.uploadedFileExtension = string.Empty;
                    gmvi.uploadedFileSize = -1;
                }

                var searchResults = ts.validateGroupMembershipUploadDetails(gmvi, ListGroupMembershipUploadInput);

                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: PostGroupMembershipValidate : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("groupmembershipupload")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostGroupMembershipUpload(GroupMembershipUploadDetails GroupMembershipUploadDetails)
        {
            try
            {
                ARC.Donor.Service.Upload.UploadValidationServices ts = new ARC.Donor.Service.Upload.UploadValidationServices();
                var output = ts.uploadGroupMembershipDetails(GroupMembershipUploadDetails);
                return Ok(output);
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: PostGroupMembershipUpload : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }

        }
        /// <summary>
        /// Search records that have been uploaded into the system using group membehsip references
        /// </summary>
        /// <param name="listUploadInputList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("listsearch")]
        [ResponseType(typeof(IList<ListUploadOutput>))]
        public IHttpActionResult getListUploadSearch(ListOfListUploadSearchInput listUploadInputList)
        {
            try
            {
                ARC.Donor.Service.Upload.ListUploadSearchServices listUploadServices = new ARC.Donor.Service.Upload.ListUploadSearchServices();
                var results = listUploadServices.getListUploadSearchResults(listUploadInputList);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: getListUploadSearch : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }

        }
        /// <summary>
        /// Get the group membership reference data 
        /// </summary>
        /// <returns></returns>
        [Route("getGroupMembershipReferenceData")]
        [ResponseType(typeof(IList<GroupMembershipReference>))]
        public IHttpActionResult GetGroupMembershipReferenceData()
        {
            try
            {
                ARC.Donor.Service.Upload.GroupMembershipReferenceServices grs = new ARC.Donor.Service.Upload.GroupMembershipReferenceServices();

                return Ok(grs.GetGroupMembershipReferenceData());
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: GetGroupMembershipReferenceData : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [Route("postNewGroupMembershipReferenceRecord")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostNewGroupMembershipReferenceRecord([FromBody] ARC.Donor.Business.Upload.GroupMembershipReferenceInsertData postGrpMbrshpRefRecord)
        {
            try
            {
                ARC.Donor.Service.Upload.GroupMembershipReferenceServices grs = new ARC.Donor.Service.Upload.GroupMembershipReferenceServices();
                var searchResults = grs.postNewGroupMembershipReferenceRecord(postGrpMbrshpRefRecord);

                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: PostNewGroupMembershipReferenceRecord : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [Route("postDeleteMembershipReferenceRecord")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDeleteMembershipReferenceRecord([FromBody] GroupMembershipDeleteReferenceParam deletegroupRefParam)
        {
            try
            {
                ARC.Donor.Service.Upload.GroupMembershipReferenceServices grs = new ARC.Donor.Service.Upload.GroupMembershipReferenceServices();
                var searchResults = grs.postDeleteGroupMembershipReferenceRecord(deletegroupRefParam);

                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: PostDeleteMembershipReferenceRecord : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }

        }

        [Route("postEditMembershipReferenceRecord")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult postEditMembershipReferenceRecord([FromBody] GroupMembershipEditReferenceParam editgroupRefParam)
        {
            try
            {
                ARC.Donor.Service.Upload.GroupMembershipReferenceServices grs = new ARC.Donor.Service.Upload.GroupMembershipReferenceServices();
                var searchResults = grs.postEditGroupMembershipReferenceRecord(editgroupRefParam);

                return Ok(searchResults);

            }
           
             catch (Exception ex)
            {
                _msg = "ERROR UploadController :: postEditMembershipReferenceRecord : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }

        }

        /// <summary>
        /// Get the upload details of the transaction id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetUploadDetailsTransExportDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionUploadDetails>))]
        public IHttpActionResult GetUploadDetailsTransExportDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getUploadDetailsTransactionDetails(2000, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: GetUploadDetailsTransExportDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// validate the uploded email record. pass the record as a list of email records
        /// </summary>
        /// <param name="ListEmailOnlyUploadInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostEmailUploadDetailsValidate")]
        [ResponseType(typeof(EmailOnlyUploadValidationOutput))]
        public IHttpActionResult PostEmailOnlyDetailsValidate(ListEmailOnlyUploadInput ListEmailOnlyUploadInput)
        {
            List<string> chapterCodes = null;
            List<string> groupCodes = null;
            List<string> emailAddresses = null;


            try
            {
                ARC.Donor.Service.Upload.EmailOnlyUploadValidationServices ts = new ARC.Donor.Service.Upload.EmailOnlyUploadValidationServices();

                if (ListEmailOnlyUploadInput != null)
                {
                    if (ListEmailOnlyUploadInput.EmailOnlyUploadInputList != null)
                    {
                        //First get all the distinct chapterCodes and groupCodes
                        chapterCodes = ListEmailOnlyUploadInput.EmailOnlyUploadInputList.GroupBy(x => x.chpt_cd).Select(y => y.First().chpt_cd).ToList();
                        groupCodes = ListEmailOnlyUploadInput.EmailOnlyUploadInputList.GroupBy(x => x.grp_cd).Select(y => y.First().grp_cd).ToList();
                        emailAddresses = ListEmailOnlyUploadInput.EmailOnlyUploadInputList.GroupBy(x => x.cnst_email_addr).Select(y => y.First().cnst_email_addr).ToList();
                    }
                    else
                    {
                        //If GroupMembershipUploadInputList is null - instantiate the lists
                        chapterCodes = new List<string>();
                        groupCodes = new List<string>();
                        emailAddresses = new List<string>();
                    }
                }
                else
                { //If ListGroupMembershipUploadInput is null - instantiate the lists
                    chapterCodes = new List<string>();
                    groupCodes = new List<string>();
                    emailAddresses = new List<string>();
                }

                EmailOnlyUploadValidationInput emvi = new EmailOnlyUploadValidationInput();
                emvi._chapterCodes = chapterCodes;
                emvi._groupCodes = groupCodes;
                emvi._emailAddress = emailAddresses;

                if (ListEmailOnlyUploadInput != null)
                {
                    if (ListEmailOnlyUploadInput.UploadedEmailFileInputInfo != null)
                    {
                        emvi.uploadedFileName = ListEmailOnlyUploadInput.UploadedEmailFileInputInfo.fileName;
                        emvi.uploadedFileExtension = ListEmailOnlyUploadInput.UploadedEmailFileInputInfo.fileExtension;
                        emvi.uploadedFileSize = ListEmailOnlyUploadInput.UploadedEmailFileInputInfo.intFileSize;
                    }
                    else
                    { //If ListGroupMembershipUploadInput.UploadedFileInputInfo is null - instantiate the variables to be the minimal ones
                        emvi.uploadedFileName = string.Empty;
                        emvi.uploadedFileExtension = string.Empty;
                        emvi.uploadedFileSize = -1;
                    }
                }
                else
                {//If ListGroupMembershipUploadInput is null - instantiate the variables to be the minimal ones
                    emvi.uploadedFileName = string.Empty;
                    emvi.uploadedFileExtension = string.Empty;
                    emvi.uploadedFileSize = -1;
                }

                var searchResults = ts.validateEmailOnlyUploadDetails(emvi, ListEmailOnlyUploadInput);

                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: PostEmailOnlyDetailsValidate : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("PostEmailOnlyUploadData")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostEmailOnlyUploadData(EmailOnlyUploadDetails emailOnlyUploadDetails)
        {
            try
            {
                ARC.Donor.Service.Upload.EmailOnlyUploadValidationServices ts = new ARC.Donor.Service.Upload.EmailOnlyUploadValidationServices();
                var output = ts.PostEmaiOnlyUploadData(emailOnlyUploadDetails);
                return Ok(output);
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: PostEmailOnlyUploadData : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
           
        }
        /// <summary>
        /// Validate the name and email upload record
        /// </summary>
        /// <param name="ListNameAndEmailUploadInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("NameAndEmailValidate")]
        [ResponseType(typeof(NameAndEmailUploadValidationOutput))]
        public IHttpActionResult PostNameEmailDetailsValidate(ListNameAndEmailUploadInput ListNameAndEmailUploadInput)
        {
            List<string> chapterCodes = null;
            List<string> groupCodes = null;
            List<string> emailAddresses = null;

            try
            {
                ARC.Donor.Service.Upload.NameAndEmailUploadValidationServices ts = new ARC.Donor.Service.Upload.NameAndEmailUploadValidationServices();

                if (ListNameAndEmailUploadInput != null)
                {
                    if (ListNameAndEmailUploadInput.NameAndEmailUploadInputList != null)
                    {
                        //First get all the distinct chapterCodes and groupCodes
                        chapterCodes = ListNameAndEmailUploadInput.NameAndEmailUploadInputList.GroupBy(x => x.chpt_cd).Select(y => y.First().chpt_cd).ToList();
                        groupCodes = ListNameAndEmailUploadInput.NameAndEmailUploadInputList.GroupBy(x => x.grp_cd).Select(y => y.First().grp_cd).ToList();
                        emailAddresses = ListNameAndEmailUploadInput.NameAndEmailUploadInputList.GroupBy(x => x.cnst_email_addr).Select(y => y.First().cnst_email_addr).ToList();
                    }
                    else
                    {
                        //If NameAndEmailUploadInputList is null - instantiate the lists
                        chapterCodes = new List<string>();
                        groupCodes = new List<string>();
                        emailAddresses = new List<string>();
                    }
                }
                else
                { //If ListNameAndEmailUploadInput is null - instantiate the lists
                    chapterCodes = new List<string>();
                    groupCodes = new List<string>();
                    emailAddresses = new List<string>();
                }

                NameAndEmailUploadValidationInput emvi = new NameAndEmailUploadValidationInput();
                emvi._chapterCodes = chapterCodes;
                emvi._groupCodes = groupCodes;
                emvi._emailAddresses = emailAddresses;

                if (ListNameAndEmailUploadInput != null)
                {
                    if (ListNameAndEmailUploadInput.UploadedNameAndEmailFileInputInfo != null)
                    {
                        emvi.uploadedFileName = ListNameAndEmailUploadInput.UploadedNameAndEmailFileInputInfo.fileName;
                        emvi.uploadedFileExtension = ListNameAndEmailUploadInput.UploadedNameAndEmailFileInputInfo.fileExtension;
                        emvi.uploadedFileSize = ListNameAndEmailUploadInput.UploadedNameAndEmailFileInputInfo.intFileSize;
                    }
                    else
                    { //If ListNameAndEmailUploadInput.UploadedFileInputInfo is null - instantiate the variables to be the minimal ones
                        emvi.uploadedFileName = string.Empty;
                        emvi.uploadedFileExtension = string.Empty;
                        emvi.uploadedFileSize = -1;
                    }
                }
                else
                {//If ListNameAndEmailUploadInput is null - instantiate the variables to be the minimal ones
                    emvi.uploadedFileName = string.Empty;
                    emvi.uploadedFileExtension = string.Empty;
                    emvi.uploadedFileSize = -1;
                }

                var searchResults = ts.validateNameAndEmailUploadDetails(emvi, ListNameAndEmailUploadInput);

                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: PostNameEmailDetailsValidate : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("NameAndEmailUpload")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostNameAndEmailUploadData(NameAndEmailUploadDetails emailAndEmailUploadDetails)
        {
            try
            {
                ARC.Donor.Service.Upload.NameAndEmailUploadValidationServices ts = new ARC.Donor.Service.Upload.NameAndEmailUploadValidationServices();
                var output = ts.PostNameAndEmailUploadData(emailAndEmailUploadDetails);
                return Ok(output);
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: PostNameAndEmailUploadData : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }

        }

        /// <summary>
        /// validate the DNC record. Pass the parameters as List of DNCs
        /// </summary>
        /// <param name="listDncUploadInput"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("dncvalidate")]
        [ResponseType(typeof(DncValidationOutput))]
        public IHttpActionResult PostDncValidate(ListDncUploadInput listDncUploadInput)
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
                ARC.Donor.Service.Upload.DncUploadValidation ts = new ARC.Donor.Service.Upload.DncUploadValidation();

               
                DncValidationInput dncValidationInput = new DncValidationInput();


                if (listDncUploadInput != null)
                {
                    if (listDncUploadInput.dncUploadInputList != null)
                    {  
                        _addressLine1 = listDncUploadInput.dncUploadInputList.GroupBy(x => x.cnst_addr_line1).Select(y => y.First().cnst_addr_line1).ToList();
                        _addressLine2 = listDncUploadInput.dncUploadInputList.GroupBy(x => x.cnst_addr_line2).Select(y => y.First().cnst_addr_line2).ToList();
                        _city = listDncUploadInput.dncUploadInputList.GroupBy(x => x.cnst_addr_city).Select(y => y.First().cnst_addr_city).ToList();
                        _state = listDncUploadInput.dncUploadInputList.GroupBy(x => x.cnst_addr_state).Select(y => y.First().cnst_addr_state).ToList();
                        _zip = listDncUploadInput.dncUploadInputList.GroupBy(x => x.cnst_addr_zip5).Select(y => y.First().cnst_addr_zip5).ToList();
                        _emailAddress = listDncUploadInput.dncUploadInputList.GroupBy(x => x.cnst_email_addr).Select(y => y.First().cnst_email_addr).ToList();
                        _phoneNumber = listDncUploadInput.dncUploadInputList.GroupBy(x => x.cnst_phn_num).Select(y => y.First().cnst_phn_num).ToList();

                        _sourceSystemCode = listDncUploadInput.dncUploadInputList.GroupBy(x => x.arc_srcsys_cd).Select(y => y.First().arc_srcsys_cd).ToList();
                        _sourceSystemId = listDncUploadInput.dncUploadInputList.GroupBy(x => x.cnst_srcsys_id).Select(y => y.First().cnst_srcsys_id).ToList();
                        _masterId = listDncUploadInput.dncUploadInputList.GroupBy(x => x.init_mstr_id).Select(y => y.First().init_mstr_id).ToList();
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


               
                dncValidationInput._addressLine1 = _addressLine1;
                dncValidationInput._addressLine2 = _addressLine2;
                dncValidationInput._city = _city;
                dncValidationInput._zip = _zip;
                dncValidationInput._state = _state;
                dncValidationInput._emailAddress = _emailAddress;
                dncValidationInput._phoneNumber = _phoneNumber;

                dncValidationInput._sourceSystemCode = _sourceSystemCode;
                dncValidationInput._sourceSystemId = _sourceSystemId;
                dncValidationInput._masterIds = _masterId;


                if (listDncUploadInput != null)
                {
                    if (listDncUploadInput.dncFileInfo != null)
                    {
                        dncValidationInput.uploadedFileName = listDncUploadInput.dncFileInfo.fileName;
                        dncValidationInput.uploadedFileExtension = listDncUploadInput.dncFileInfo.fileExtension;
                        dncValidationInput.uploadedFileSize = listDncUploadInput.dncFileInfo.intFileSize;
                    }
                    else
                    { //If ListGroupMembershipUploadInput.UploadedFileInputInfo is null - instantiate the variables to be the minimal ones
                        dncValidationInput.uploadedFileName = string.Empty;
                        dncValidationInput.uploadedFileExtension = string.Empty;
                        dncValidationInput.uploadedFileSize = -1;
                    }
                }
                else
                {//If ListGroupMembershipUploadInput is null - instantiate the variables to be the minimal ones
                    dncValidationInput.uploadedFileName = string.Empty;
                    dncValidationInput.uploadedFileExtension = string.Empty;
                    dncValidationInput.uploadedFileSize = -1;
                }

                var results = ts.validateDncRecords(dncValidationInput, listDncUploadInput);
                //var result = ts.validateRecords(listDncUploadInput);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: PostDncValidate : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("postdncupload")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDncUpload(DncUploadDetails dncUploadDetails)
        {

            try
            {

                ARC.Donor.Service.Upload.DncUploadServices ts = new ARC.Donor.Service.Upload.DncUploadServices();

                //Does not care for the returned value
                var output = ts.dncUploadData(dncUploadDetails);
                return Ok(output);

            }
            catch (Exception ex)
            {
                _msg = "ERROR UploadController :: PostDncUpload : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        
        
    }
}