using ARC.Donor.Business.Constituents;
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
    [RoutePrefix("api/dnc")]
    public class DNCController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        /// <summary>
        /// This api is used to get DNC of a constituent 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetDnc/{id}")]
        [ResponseType(typeof(IList<DoNotContact>))]
        public IHttpActionResult getDNC(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.DoNotContact arc = new ARC.Donor.Service.Constituents.DoNotContact();
                return Ok(arc.getDoNotContact(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR DNCController :: getDNC : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get all the DNCs of a given id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetAllDnc/{id}")]
        [ResponseType(typeof(IList<AllDoNotContact>))]
        public IHttpActionResult getAllDNC(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.DoNotContact arc = new ARC.Donor.Service.Constituents.DoNotContact();
                return Ok(arc.getAllDoNotContacts(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR DNCController :: GetAllDnc : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("AddDnc")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult addDoNotContact([FromBody] ARC.Donor.Business.Constituents.DoNotContactInput dncInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("DoNotContact", "Add", dncInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.DoNotContact p = new ARC.Donor.Service.Constituents.DoNotContact();
                    var searchResults = p.addDoNotContact(dncInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR DNCController :: addDoNotContact : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("EditDnc")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult editDoNotContact([FromBody] ARC.Donor.Business.Constituents.DoNotContactInput dncInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("DoNotContact", "Edit", dncInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.DoNotContact p = new ARC.Donor.Service.Constituents.DoNotContact();
                    var searchResults = p.editDoNotContact(dncInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR DNCController :: editDoNotContact : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("DeleteDnc")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult deleteDoNotContact([FromBody] ARC.Donor.Business.Constituents.DoNotContactInput dncInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("DoNotContact", "Delete", dncInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.DoNotContact p = new ARC.Donor.Service.Constituents.DoNotContact();
                    var searchResults = p.deleteDoNotContact(dncInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR DNCController :: deleteDoNotContact : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        private Boolean checkMandatoryInputs(string strRequestType, string strActionType, object InputObj)
        {
            Boolean boolMandatoryCheck = true;
            //Dictionary to hold the list of columns which are mandatory while 
            Dictionary<string, Dictionary<string, List<string>>> dictMandatoryLibrary = new Dictionary<string, Dictionary<string, List<string>>>()
            {
                {"DoNotContact", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"i_mstr_id", "i_cnst_typ","i_user_id" ,
                        "i_new_cnst_dnc_line_of_service_cd","i_new_cnst_dnc_comm_chan"}},
                    {"Edit", new List<string>() {"i_mstr_id", "i_cnst_typ","i_user_id",
                        "i_new_cnst_dnc_line_of_service_cd","i_new_cnst_dnc_comm_chan",
                    "i_bk_cnst_dnc_line_of_service_cd","i_bk_cnst_dnc_comm_chan"}},
                    {"Delete", new List<string>()  {"i_mstr_id", "i_cnst_typ","i_user_id",
                    "i_bk_cnst_dnc_line_of_service_cd","i_bk_cnst_dnc_comm_chan"}}
                 }
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
