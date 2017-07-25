using ARC.Donor.Business;
using ARC.Donor.Business.Constituents;
using ARC.Donor.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;
using System.Web.Http.Description;

namespace DonorWebservice.Controllers
{
    //[Authorize]
    [RoutePrefix("api/org")]
    public class OrgController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        OrgController()
        {

        }
        /// <summary>
        /// This method is used to Get Email Domain for Organizations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetEmailDomain/{id}")]
        [ResponseType(typeof(IList<OrgEmailDomain>))]
        public IHttpActionResult GetEmailDomain(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.OrgEmailDomain arc = new ARC.Donor.Service.Constituents.OrgEmailDomain();
                return Ok(arc.getOrgEmailDomain(50, 1, id));
            }
            catch (Exception ex)
            {
                log.Info("GetEmailDomain API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method is used to Get NAICS information for Organizations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetOrgNAICS/{id}")]
        [ResponseType(typeof(IList<OrgNaics>))]
        public IHttpActionResult GetOrgNAICS(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.OrgNaics arc = new ARC.Donor.Service.Constituents.OrgNaics();
                return Ok(arc.getOrgNAICS(50, 1, id));
            }
            catch (Exception ex)
            {
                log.Info("GetOrgNAICS API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method is used to Get Contacts information for Organizations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetOrgContacts/{id}")]
        [ResponseType(typeof(IList<OrgContacts>))]
        public IHttpActionResult GetOrgContacts(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.OrgContacts arc = new ARC.Donor.Service.Constituents.OrgContacts();
                return Ok(arc.getOrgContacts(50, 1, id));
            }
            catch (Exception ex)
            {
                log.Info("GetOrgContacts API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method is used to Get Contacts Export information for Organizations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetOrgContactsExport/{id}")]
        [ResponseType(typeof(IList<OrgContacts>))]
        public IHttpActionResult GetOrgContactsExport(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.OrgContacts arc = new ARC.Donor.Service.Constituents.OrgContacts();
                return Ok(arc.getOrgContacts(2000, 1, id));
            }
            catch (Exception ex)
            {
                log.Info("GetOrgContactsExport API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method is used to Get Source System AlternateIds information for Organizations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetSourceSystemAlternateIds/{id}")]
        [ResponseType(typeof(IList<AlternateIds>))]
        public IHttpActionResult GetSourceSystemAlternateIds(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.AlternateIds arc = new ARC.Donor.Service.Constituents.AlternateIds();
                return Ok(arc.getSourceSystemAlternateIds(50, 1, id));
            }
            catch (Exception ex)
            {
                log.Info("GetSourceSystemAlternateIds API Error - " + ex.Message);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("naicsstatuschange")]
        [ApiExplorerSettings(IgnoreApi = true)]
        /* Method name: NAICSStatusChange
         * Input Parameters: An object of NAICSStatusChangeInput class
         * Purpose: This method updates(approve/reject/add) the status of the NAICS codes for a particular master */
        public IHttpActionResult NAICSStatusChange(NAICSStatusUpdateInput postData)
        {
            try
            {
                ARC.Donor.Service.Constituents.OrgNaics naicsServices = new ARC.Donor.Service.Constituents.OrgNaics();
                var results = naicsServices.naicsStatusChange(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("NAICSStatusChange API Error - " + ex.Message);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("addconstituentemaildomain")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostAddConstEmailDomain([FromBody]  ARC.Donor.Business.Constituents.OrgEmailDomainAddInput OrgEmailDomainAddInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("EmailDomain", "Add", OrgEmailDomainAddInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.OrgEmailDomain p = new ARC.Donor.Service.Constituents.OrgEmailDomain();
                    var searchResults = p.addOrgEmailDomainMapping(OrgEmailDomainAddInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                log.Info("AddConstEmailDomain API Error - " + ex.Message);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("deleteconstituentemaildomain")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDeleteConstEmailDomain([FromBody] ARC.Donor.Business.Constituents.OrgEmailDomainDeleteInput OrgEmailDomainDeleteInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("EmailDomain", "Delete", OrgEmailDomainDeleteInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.OrgEmailDomain p = new ARC.Donor.Service.Constituents.OrgEmailDomain();
                    var searchResults = p.deleteOrgEmailDomainMapping(OrgEmailDomainDeleteInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                log.Info("DeleteConstEmailDomain API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method is used to Get Source System AlternateIds information for Organizations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetRFMValues/{id}")]
        [ResponseType(typeof(IList<RFMValues>))]
        public IHttpActionResult GetRFMValues(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.GetRFMValues arc = new ARC.Donor.Service.Constituents.GetRFMValues();
                return Ok(arc.getGetRFMValues(50, 1, id));
            }
            catch (Exception ex)
            {
                log.Info("GetRFMValues API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        private Boolean checkMandatoryInputs(string strRequestType, string strActionType, object InputObj)
        {
            Boolean boolMandatoryCheck = true;
            //Dictionary to hold the list of columns which are mandatory while 
            Dictionary<string, Dictionary<string, List<string>>> dictMandatoryLibrary = new Dictionary<string, Dictionary<string, List<string>>>()
            {
                {"PersonName", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"MasterID", "EmailDomain", "UserName"}},
                    {"Delete", new List<string>() {"MasterID", "EmailDomain", "UserName"}}}
                }

            };

            //Fetch the mandatory fields for the inputted details type and action types
            List<string> listMandatoryColumns = new List<string>();
            foreach (KeyValuePair<string, Dictionary<string, List<string>>> keyValuePair in dictMandatoryLibrary)
            {
                if (keyValuePair.Key == strRequestType)
                {
                    foreach (KeyValuePair<string, List<string>> innerkeyValuePair in keyValuePair.Value)
                    {
                        if (innerkeyValuePair.Key == strActionType)
                        {
                            listMandatoryColumns = innerkeyValuePair.Value;
                        }
                    }
                }
            }

            //Check if the mandatory columns are present in the inputted object
            foreach (string columnName in listMandatoryColumns)
            {
                //Check if the mandatory columns are null or empty
                if (InputObj.GetType().GetProperty(columnName).GetValue(InputObj) == null)
                {
                    boolMandatoryCheck = false;
                }
                else if (string.IsNullOrEmpty(InputObj.GetType().GetProperty(columnName).GetValue(InputObj).ToString()))
                {
                    boolMandatoryCheck = false;
                }
                //Check if valid numbers are provided to the not nullable fields
                else if (InputObj.GetType().GetProperty(columnName).GetValue(InputObj).ToString() == "0")
                {
                    boolMandatoryCheck = false;
                }
            }

            return boolMandatoryCheck;
        }
    }
}
