using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARC.Donor.Business;
using ARC.Donor.Business.Transaction;
using ARC.Donor.Service;
using ARC.Donor.Service.Transaction;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using System.Web.Http.Description;

namespace DonorWebservice.Controllers
{
    [Authorize]
    [RoutePrefix("api/Transaction")]
    public class TransactionController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        /// <summary>
        /// Get the transaction details by passing relevant parameters
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TransactionSearch")]
        [ResponseType(typeof(IList<TransactionSearchOutputModel>))]
        public IHttpActionResult getTransactionSearchResults(ListTransactionSearchInputModel postData)
        {
            try
            {
                TransactionServices transactionServices = new TransactionServices();
                transactionServices.InsertQueryLogEvent +=transactionServices_InsertQueryLogEvent;
                var results = transactionServices.getTransactionSearchResults(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: getTransactionSearchResults : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        private void transactionServices_InsertQueryLogEvent(object sender, QueryLogEventArgs e)
        {
            var qry = new ClientValidation.QueryTimeLogger();
            qry.UserName = e.UserName;
            qry.Query = e.Query;
            qry.StartTime = e.StartTime;
            qry.EndTime = e.EndTime;
            qry.Action = e.Action;
            Models.QueryLogger.InsertQuery(qry);

        }
        [HttpPost]
        [Route("UpdateTransactionStatus")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUpdateTransactionStatus([FromBody] ARC.Donor.Business.Transaction.TransactionUpdate.TransactionStatusUpdateInput TransStatusUpdateInput)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionUpdateServices ts = new ARC.Donor.Service.Transaction.TransactionUpdateServices();
                var searchResults = ts.updateTransactionStatus(TransStatusUpdateInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: PostUpdateTransactionStatus : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


        [HttpPost]
        [Route("UpdateTransactionCaseAssociation")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUpdateTransactionCaseAssociation([FromBody] ARC.Donor.Business.Transaction.TransactionUpdate.TransactionCaseAssociationInput TransCaseAssociationInput)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionUpdateServices ts = new ARC.Donor.Service.Transaction.TransactionUpdateServices();
                var searchResults = ts.updateTransactionCaseAssocation(TransCaseAssociationInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: PostUpdateTransactionCaseAssociation : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the merge transaction details by passing the transaction id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetMergeTransactionDetails/{id}")]
        [ResponseType(typeof(IList<TransactionMerge>))]
        // [Authorize]
        public IHttpActionResult GetMergeTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getMergeTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetMergeTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the transaction details of email by passing the transaction id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetEmailTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionEmail>))]
        public IHttpActionResult GetEmailTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getEmailTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetEmailTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the details of address transactions by passing transaction id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetAddressTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionAddress>))]
        public IHttpActionResult GetAddressTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getAddressTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetAddressTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the birth transaction details of transaction by passing transaction key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetBirthTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionBirth>))]
        public IHttpActionResult GetBirthTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getBirthTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetBirthTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the characteristics of a transaction by passing transaction id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetCharacteristicsTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionCharacteristics>))]
        public IHttpActionResult GetCharacteristicsTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getCharacteristicsTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetCharacteristicsTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [Route("GetContactPreferenceTransactionDetails/{id}")]
        // [Authorize]

        public IHttpActionResult GetContactPreferenceTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getContactPreferenceTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetContactPreferenceTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        ///   Get the transaction details related to death by passing the transaction id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetDeathTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionDeath>))]
        public IHttpActionResult GetDeathTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getDeathTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetDeathTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get person name details of a transaction by passing the transaction id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetPersonNameTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionPersonName>))]
        public IHttpActionResult GetPersonNameTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getPersonNameTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetPersonNameTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get org name transaction details by passing the transaaction key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetOrgNameTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionOrgName>))]
        public IHttpActionResult GetOrgNameTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getOrgNameTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetOrgNameTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the transaction details of phone by passing transaction id as the parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetPhoneTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionPhone>))]
        public IHttpActionResult GetPhoneTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getPhoneTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetPhoneTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

       
        /// <summary>
        /// Get all org transformation details by passing the transaction key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetOrgTransformationsTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionOrgTransformations>))]
        public IHttpActionResult GetOrgTransformationsTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getOrgTransformationsTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetOrgTransformationsTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get org affiliator transaction details by passing the transaction key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetOrgAffiliatorTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionOrgAffiliator>))]
        public IHttpActionResult GetOrgAffiliatorTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getOrgAffiliatorTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetOrgAffiliatorTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the affiliator tag transaction details by passing transaction key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetAffiliatorTagsTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionAffiliatorTags>))]
        public IHttpActionResult GetAffiliatorTagsTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getAffiliatorTagsTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetAffiliatorTagsTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get affiliator tag upload transaction by passing transaction key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetAffiliatorTagsUploadTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionAffiliatorTagsUpload>))]
        public IHttpActionResult GetAffiliatorTagsUploadTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getAffiliatorTagsUploadTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetAffiliatorTagsUploadTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the affiliator hierarcy transaction details by passing transaction key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetAffiliatorHierarchyTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionAffiliatorHierarchy>))]
        public IHttpActionResult GetAffiliatorHierarchyTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getAffiliatorHierarchyTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetAffiliatorHierarchyTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the upload(email/group membership / name and email upload) details of a transaction by passing transaction key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetUploadDetailsTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionUploadDetails>))]
        public IHttpActionResult GetUploadDetailsTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getUploadDetailsTransactionDetails(2000, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetUploadDetailsTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


       /// <summary>
       /// Get unmerge request log transaction details by passsing trans key
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [Route("GetUnmergeRequestLogTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionUnmergeRequestLog>))]
        public IHttpActionResult GetUnmergeRequestLogTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getUnmergeRequestLogTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetUnmergeRequestLogTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get unmerge process log transaction details by passing trans key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [Route("GetUnmergeProcessLogTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionUnmergeProcessLog>))]
        public IHttpActionResult GetUnmergeProcessLogTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getUnmergeProcessLogTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetUnmergeProcessLogTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get NAICS transaction details by passing trans key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetNAICSTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionNAICS>))]
        public IHttpActionResult GetNAICSTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getNAICSTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetNAICSTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error"+ex.GetBaseException());
            }
        }
        /// <summary>
        /// Get NAICD upload transaction details by passing trans key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetNAICSUploadTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionNAICSUpload>))]
        public IHttpActionResult GetNAICSUploadTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getNAICSUploadTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetNAICSUploadTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error" + ex.GetBaseException());
            }
        }
        /// <summary>
        /// Get org email domain transaction details by passing transaction key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetOrgEmailDomainTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionOrgEmailDomain>))]
        public IHttpActionResult GetOrgEmailDomainTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getOrgEmailDomainTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetOrgEmailDomainTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error" + ex.GetBaseException());
            }
        }
        /// <summary>
        /// Get the org confirmation transaction details by passing transaction key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetOrgConfirmationTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionOrgConfirmation>))]
        public IHttpActionResult GetOrgConfirmationTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getOrgConfirmationTransactionDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetOrgConfirmationTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error" + ex.GetBaseException());
            }
        }


        //Added by srini for CEM surfacing functionality
        /// <summary>
        /// Get CEM DNC transaction details by passing transaction key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetCemDncTransDetails/{id}")]
        [ResponseType(typeof(IList<TransCemDNCDetails>))]
        public IHttpActionResult GetCemDNCTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getTransCemDNCDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetCemDNCTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error" + ex.GetBaseException());
            }
        }
        /// <summary>
        /// Get message preference transaction details by passing the trans key as the parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetCemMsgPrefTransDetails/{id}")]
        [ResponseType(typeof(IList<TransCemMsgPrefDetails>))]
        public IHttpActionResult GetCemMsgPrefTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getTransCemMsgPrefDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetCemMsgPrefTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error" + ex.GetBaseException());
            }
        }
        /// <summary>
        /// Get preferred locator transaction details by passing trans key as the parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetCemPrefLocTransDetails/{id}")]
        [ResponseType(typeof(IList<TransCemPrefLocDetails>))]
        public IHttpActionResult GetCemPrefLocTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getTransCemPrefLocDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetCemPrefLocTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error" + ex.GetBaseException());
            }
        }
        /// <summary>
        /// Get the group membership transaction details by passing trans key 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetCemGrpMembershipTransDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransCemGrpMembership>))]
        public IHttpActionResult GetCemGrpMembershipTransDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getTransCemGrpMembershipDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetCemGrpMembershipTransDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get transaction details of dnc uploaded records by passing the trans key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetDncUploadTransDetails/{id}")]
        [ResponseType(typeof(IList<TransactionDncUploadDetails>))]
        public IHttpActionResult GetDncUploadTransDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionUploadDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionUploadDetails.getTransDncUploadDetails(2000, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetDncUploadTransDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get transaction details message preference uploaded records by passing trans key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetMsgPrefUploadTransDetails/{id}")]
        [ResponseType(typeof(IList<TransactionMsgPrefUploadDetails>))]
        public IHttpActionResult GetMsgPrefUploadTransDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionUploadDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionUploadDetails.getTransMsgPrefUploadDetails(2000, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetMsgPrefUploadTransDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        // adding ends here

        /// <summary>
        /// Get EO affiliator upload transaction details by passing trans key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetEOAffiliationUploadTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionEOAffiliationUpload>))]
        public IHttpActionResult GetEOAffiliationUploadTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getTransactionEOAffiliationUpload(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetEOAffiliationUploadTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error" + ex.GetBaseException());
            }
        }
        /// <summary>
        /// Get EO site uploaded records transaction details by passing trans key 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetEOSiteUploadTransactionDetails/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<TransactionEOSiteUpload>))]
        public IHttpActionResult GetEOSiteUploadTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getTransactionEOSiteUpload(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetEOAffiliationUploadTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error" + ex.GetBaseException());
            }
        }

        /// <summary>
        /// Get EO uploaded records transaction details by passing trans key 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetEOUploadTransactionDetails/{id}")]
        [ResponseType(typeof(IList<TransactionEOUpload>))]
        public IHttpActionResult GetEOUploadTransactionDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getTransactionEOUpload(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetEOUploadTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error" + ex.GetBaseException());
            }
        }


        /// <summary>
        /// Get EO characteristics records transaction details by passing trans key 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetEOCharacteristicsTransDetails/{id}")]
        [ResponseType(typeof(IList<TransEOCharacteristics>))]
        public IHttpActionResult GetEOCharacteristicsTransDetails(string id)
        {
            try
            {
                ARC.Donor.Service.Transaction.TransactionDetails transactionDetails = new ARC.Donor.Service.Transaction.TransactionDetails();
                return Ok(transactionDetails.getTransEOCharacteristics(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR TransactionController :: GetEOCharacteristicsTransDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error" + ex.GetBaseException());
            }
        }
    }
}