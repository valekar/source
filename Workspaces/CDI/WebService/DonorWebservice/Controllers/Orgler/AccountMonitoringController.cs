using ARC.Donor.Business;
using ARC.Donor.Business.Orgler.AccountMonitoring;
using ARC.Donor.Service;
using ARC.Donor.Service.Orgler.AccountMonitoring;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;

namespace DonorWebservice.Controllers.Orgler
{
  [Authorize]
    [RoutePrefix("api/accountmonitoring")]
    public class AccountMonitoringController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
/// <summary>
/// Displays all the New Accounts as per users input
/// </summary>
/// <param name="postData"></param>
/// <returns></returns>
        [HttpPost]
        [Route("newaccountssearch")]
        [ResponseType(typeof(IList<NewAccountsOutputModel>))]
        /* Method name: GetNewAccountSearchResults
         * Input Parameters: An object of NewAccountsInputModel class
         * Purpose: This method gets the search results for new account */
        public IHttpActionResult GetNewAccountSearchResults(NewAccountsInputModel postData)
        {
            try
            {
                log.Info("Search API");
                AccountServices accServices = new AccountServices();


                var results = accServices.getNewAccountSearchResults(postData);               
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Search API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Displays all the Top Organization as per users input
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("toporgssearch")]
        [ResponseType(typeof(IList<TopOrgsOutputModel>))]
        /* Method name: GetTopOrgsSearchResults
         * Input Parameters: An object of TopOrgsInputModel class
         * Purpose: This method gets the search results for top organization */
        public IHttpActionResult GetTopOrgsSearchResults(TopOrgsInputModel postData)
        {
            try
            {                
                AccountServices accServices = new AccountServices();
                var results = accServices.getTopOrgsSearchResults(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Top Orgs API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method gets the potential merge details for a master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getpotentialmergedetails/{id}")]
        [ResponseType(typeof(IList<PotentialMergeOutput>))]
        /* Method name: getPotentialMergeDetails
         * Input Parameters: Master Id
         * Purpose: This method gets the potential merge details for a master */
        public IHttpActionResult getPotentialMergeDetails(string id)
        {
            try
            {
                PotentialMergeServices mergeServices = new PotentialMergeServices();
                var results = mergeServices.getMergeResults(10, 1, id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Potential Merge API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method gets the naics related details for a particular master
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getorgnaicsdetails")]
        [ResponseType(typeof(IList<GetMasterNAICSDetailsOutput>))]
        /* Method name: GetOrgNAICSDetails
        * Input Parameters: An object of ConfirmAccountInput class
        * Purpose: This method gets the naics related details for a particular master */
        public IHttpActionResult GetOrgNAICSDetails(GetMasterNAICSDetailsInput postData)
        {
            try
            {
                NAICS naicsServices = new NAICS();
                var results = naicsServices.getOrgNAICSDetails(10, 1, postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Naics Get API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method gets all the naics codes in hierchial(tree-view) data structure
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getallnaicscodes")]
        [ResponseType(typeof(IList<NAICSCode>))]
        /* Method name: GetOrgNAICSDetails
        * Input Parameters: NA
        * Purpose: This method gets all the naics codes in hierchial(tree-view) data structure */
        public IHttpActionResult GetALLNAICSCodes()
        {
            try
            {
                NAICS naicsServices = new NAICS();
                var results = naicsServices.getALLNAICSCodes();
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Naics tree Get API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method updates(approve/reject/add) the status of the NAICS codes for a particular master
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("naicsstatuschange")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ResponseType(typeof(IList<TransactionResult>))]
        /* Method name: NAICSStatusChange
         * Input Parameters: An object of NAICSStatusChangeInput class
         * Purpose: This method updates(approve/reject/add) the status of the NAICS codes for a particular master */
        public IHttpActionResult NAICSStatusChange(NAICSStatusChangeInput postData)
        {
            try
            {
                NAICS naicsServices = new NAICS();
                var results = naicsServices.naicsStatusChange(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Naics Aprove,Reject - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method confirms an account for data stewarding
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("confirmaccount")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ResponseType(typeof(IList<TransactionResult>))]
        /* Method name: ConfirmAccount
         * Input Parameters: An object of ConfirmAccountInput class
         * Purpose: This method confirms an account for data stewarding */
        public IHttpActionResult ConfirmAccount(ConfirmAccountInput postData)
        {
            try
            {
                ConfirmAccount confirmAccountServices = new ConfirmAccount();
                var results = confirmAccountServices.confirmAccount(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Confirm Account API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method uploads bulk Naics Suggesions for New Accounts
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        /* Method name: UploadNAICSSuggestions
        * Input Parameters: An object of NAICSSuggestionsDetails class
        * Purpose: This method uploads Naics Suggesions */
        [HttpPost]
        [Route("uploadnaicssuggestions")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult UploadNAICSSuggestions(NAICSSuggestionsDetails postData)
        {
            try
            {
                UploadNaicsSuggestions naicsServices = new UploadNaicsSuggestions();
                var results = naicsServices.PostNaicsSuggestionsUploadData(postData);
                if(results.insertOutput == "Success")
                {
                    naicsServices.UpdateNaicsSuggestionsUploadData(postData);
                    string strMessage = naicsServices.getUploadStatus(10, 1,results.transactionKey);
                    results.insertOutput = strMessage;
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("NAICS Upload API Error - " + ex.Message);
                return Ok("Error");
            }
        }
    }
}