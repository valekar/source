using ARC.Donor.Business;
using ARC.Donor.Business.Locator;
using ARC.Donor.Business.LocatorAddress;
using ARC.Donor.Business.LocatorDomain;
using ARC.Donor.Service;
using ARC.Donor.Service.LocatorModels;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DonorWebservice.Controllers
{

    [RoutePrefix("api/Locator")]
    public class LocatorController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        /// <summary>
        /// Post constituent parameters to get Locator Email search results.
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("locatoremailsearch")]
        [ResponseType(typeof(IList<ARC.Donor.Business.Locator.LocatorEmailOutputSearchResults>))]
        public IHttpActionResult GetLocatorEmailSearchResults(ARC.Donor.Business.Locator.ListLocatorEmailInputSearchModel postData)
        {
            try
            {
                LocatorSearchModel locatoremailServices = new LocatorSearchModel();
                locatoremailServices.InsertQueryLogEvent += locatorServices_InsertQueryLog;
                var results = locatoremailServices.getLocatorEmailSearchResults(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR LocatorController :: GetLocatorEmailSearchResults : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Post Email parameters to get email details of locator 
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("locatoremailDetailsByID")]
        [ResponseType(typeof(IList<LocatorEmailOutputSearchResults>))]
        public IHttpActionResult GetLocatorEmailDetailsByID(ARC.Donor.Business.Locator.LocatorEmailInputSearchModel postData)
        {
            try
            {
                LocatorSearchModel locatoremailServices = new LocatorSearchModel();
                locatoremailServices.InsertQueryLogEvent += locatorServices_InsertQueryLog;
                var results = locatoremailServices.getLocatorEmailDetailsByID(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR LocatorController :: GetLocatorEmailDetailsByID : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Post email parameters to get email coonstituent details of locator
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(IList<LocatorEmailConstOutputSearchResults>))]
        [Route("locatoremailconstDetailsByID")]
        public IHttpActionResult GetLocatorEmailConstDetailsByID(ARC.Donor.Business.Locator.LocatorEmailInputSearchModel postData)
        {
            try
            {
                LocatorSearchModel locatoremailServices = new LocatorSearchModel();
                locatoremailServices.InsertQueryLogEvent += locatorServices_InsertQueryLog;
                var results = locatoremailServices.getLocatorEmailConstDetailsByID(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR LocatorController :: GetLocatorEmailConstDetailsByID : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// get email domain details by passing email domain parameters
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(IList<LocatorDomainOutputSearchResults>))]
        [Route("locatoremailDomain_Details")]
        public IHttpActionResult GetLocatorDomain_Correction(ARC.Donor.Business.LocatorDomain.ListLocatorDomainInputSearchModel postData)
        {
            try
            {
                LocatorSearchModel LocatorDomainServices = new LocatorSearchModel();
                LocatorDomainServices.InsertQueryLogEvent += locatorServices_InsertQueryLog;
                var results = LocatorDomainServices.getLocatorDomain(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR LocatorController :: GetLocatorDomain_Correction : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }



        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("locatordomainUpdates")]
        public IHttpActionResult LocatordomainUpdates(ARC.Donor.Business.LocatorDomain.ListLocatorDomainInputSearchModel postData)
        {
            
            try
            {

                ARC.Donor.Service.LocatorModels.LocatorSearchModel Ls = new ARC.Donor.Service.LocatorModels.LocatorSearchModel();
                 var Results = Ls.updateLocatorDomain(postData);
                
                return Ok(Results);

            }
            catch (Exception ex)
            {
                _msg = "ERROR LocatorController :: LocatordomainUpdates : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }

        }



        /// <summary>
        /// Get the locator address details by passing address parameters
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(IList<LocatorAddressOutputSearchResults>))]
        [Route("locatoraddresssearch")]
        public IHttpActionResult GetLocatorAddressDetails(ARC.Donor.Business.LocatorAddress.ListLocatorAddressInputSearchModel postData)
        {
            try
            {
                LocatorSearchModel LocatorDomainServices = new LocatorSearchModel();
                LocatorDomainServices.InsertQueryLogEvent += locatorServices_InsertQueryLog;
                var results = LocatorDomainServices.getLocatorAddress(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR LocatorController :: GetLocatorAddressDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Post address parameters to address details of locator 
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("locatorAddressDetailsByID")]
        [ResponseType(typeof(IList<LocatorAddressOutputSearchResults>))]
        public IHttpActionResult GetLocatorAddressDetailsById(ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel postData)
        {
            try
            {
                LocatorSearchModel LocatorDomainServices = new LocatorSearchModel();
                LocatorDomainServices.InsertQueryLogEvent += locatorServices_InsertQueryLog;
                var results = LocatorDomainServices.getLocatorAddressById(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR LocatorController :: GetLocatorAddressDetailsById : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the email constituent details of locator address by passing address parameters
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("locatorAddressConstituentsDetailsByID")]
        [ResponseType(typeof(IList<LocatorAddressConstituentsOutputSearchResults>))]
        public IHttpActionResult GetLocatorAddressConstituentsDetailsById(ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel postData)
        {
            try
            {
                LocatorSearchModel LocatorDomainServices = new LocatorSearchModel();
                LocatorDomainServices.InsertQueryLogEvent += locatorServices_InsertQueryLog;
                var results = LocatorDomainServices.getLocatorAddressConstituentsById(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR LocatorController :: GetLocatorAddressConstituentsDetailsById : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Get the details of locator address assessment by passing ID as main post parameter
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("locatorAddressAssessmentDetailsByID")]
        [ResponseType(typeof(IList<LocatorAddressOutputSearchResults>))]
        public IHttpActionResult GetLocatorAddressAssessmentDetailsById(ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel postData)
        {
            try
            {
                LocatorSearchModel LocatorDomainServices = new LocatorSearchModel();
                LocatorDomainServices.InsertQueryLogEvent += locatorServices_InsertQueryLog;
                var results = LocatorDomainServices.getLocatorAddressAssessmentById(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR LocatorController :: GetLocatorAddressAssessmentDetailsById : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


        private void locatorServices_InsertQueryLog(object sender, QueryLogEventArgs e)
        {
            var qry = new ClientValidation.QueryTimeLogger();
            qry.UserName = e.UserName;
            qry.Query = e.Query;
            qry.StartTime = e.StartTime;
            qry.EndTime = e.EndTime;
            qry.Action = e.Action;
            Models.QueryLogger.InsertQuery(qry);
        }

        /*Update API for Locator*/
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("editlocatorEmail")]
        public IHttpActionResult PostUpdateEmail([FromBody] ARC.Donor.Business.Locator.CreateLocatorEmailInput CreateLocatorEmailInput)
        {
            try
            {

                    ARC.Donor.Service.LocatorModels.LocatorSearchModel Ls = new ARC.Donor.Service.LocatorModels.LocatorSearchModel();
                    var searchResults = Ls.updateLocatorEmail(CreateLocatorEmailInput);
                    return Ok(searchResults);
                
            }
            catch (Exception ex)
            {
                _msg = "ERROR LocatorController :: PostUpdateEmail : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }



        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("editlocatorAddress")]
        public IHttpActionResult PostUpdateAddress([FromBody] ARC.Donor.Business.LocatorAddress.CreateLocatorAddressInput CreateLocatorAddressInput)
        {
            try
            {

                ARC.Donor.Service.LocatorModels.LocatorSearchModel Ls = new ARC.Donor.Service.LocatorModels.LocatorSearchModel();
                var searchResults = Ls.updateLocatorAddress(CreateLocatorAddressInput);
                return Ok(searchResults);

            }
            catch (Exception ex)
            {
                _msg = "ERROR LocatorController :: PostUpdateAddress : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

    }
}