using ARC.Donor.Business;
using ARC.Donor.Business.Orgler.Upload;
using ARC.Donor.Service;
using ARC.Donor.Service.Orgler.Upload;
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
    [RoutePrefix("api/orglerupload")]
    public class OrglerUploadController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Method to validate the inputs provided as part of the Affiliation upload
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("validateaffiliationupload")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult validateAffiliationUpload(AffiliationUploadValidationInput postData)
        {
            try
            {
                log.Info("Validate Affiliation API");
                AffiliationUpload service = new AffiliationUpload();

                var results = service.validateAffiliationUpload(postData);
                return Ok(results.Result);
            }
            catch (Exception ex)
            {
                log.Info("Validate Affiliation API Error - " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Method to process the inputs provided as part of the Affiliation upload
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertaffiliationupload")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult insertAffiliationUpload(AffiliationUploadControllerInput postData)
        {
            try
            {
                log.Info("Insert Affiliation API");
                AffiliationUpload service = new AffiliationUpload();

                var results = service.insertAffiliation(postData.input, postData.strUserName);
                return Ok(results.Result);
            }
            catch (Exception ex)
            {
                log.Info("Insert Affiliation API Error - " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Method to validate the inputs provided as part of the EOSI upload
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("validateeosiupload")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult validateEosiUpload(EosiUploadValidationInput postData)
        {
            try
            {
                log.Info("Validate Eosi API");
                EosiUpload service = new EosiUpload();

                var results = service.validateEosiUpload(postData);
                return Ok(results.Result);
            }
            catch (Exception ex)
            {
                log.Info("Validate Eosi API Error - " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Method to process the inputs provided as part of the EOSI upload
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserteosiupload")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult insertEosiUpload(EosiUploadControllerInput postData)
        {
            try
            {
                log.Info("Insert Eosi API");
                EosiUpload service = new EosiUpload();

                var results = service.insertEosi(postData.input, postData.strUserName);
                return Ok(results.Result);
            }
            catch (Exception ex)
            {
                log.Info("Insert Eosi API Error - " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Method to validate the inputs provided as part of the EO upload
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("validateeoupload")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult validateEoUpload(EoUploadValidationInput postData)
        {
            try
            {
                log.Info("Validate Eo API");
                EoUpload service = new EoUpload();

                var results = service.validateEoUpload(postData);
                return Ok(results.Result);
            }
            catch (Exception ex)
            {
                log.Info("Validate Eo API Error - " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Method to process the inputs provided as part of the EO upload
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserteoupload")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult insertEoUpload(EoUploadControllerInput postData)
        {
            try
            {
                log.Info("Insert Eo API");
                EoUpload service = new EoUpload();

                var results = service.insertEo(postData.input, postData.strUserName);
                return Ok(results.Result);
            }
            catch (Exception ex)
            {
                log.Info("Insert Eo API Error - " + ex.Message);
                return Ok("Error");
            }
        }
    }
}