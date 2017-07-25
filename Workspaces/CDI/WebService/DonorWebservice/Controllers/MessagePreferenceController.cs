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
    [RoutePrefix("api/Message")]
    public class MessagePreferenceController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        /// <summary>
        /// Get the message preference of a constituent by passing the master id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetMessagePreference/{id}")]
        [ResponseType(typeof(IList<MessagePreference>))]
        public IHttpActionResult getMessagePreference(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.MessagePreference arc = new ARC.Donor.Service.Constituents.MessagePreference();
                return Ok(arc.getMessagePreference(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR MessagePreferenceController :: getMessagePreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get all the details of message preferences of a constituent by passing master id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetAllMessagePreference/{id}")]
        [ResponseType(typeof(IList<AllMessagePreference>))]
        public IHttpActionResult getAllMessagePreference(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.MessagePreference arc = new ARC.Donor.Service.Constituents.MessagePreference();
                return Ok(arc.getAllMessagePreference(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR MessagePreferenceController :: getAllMessagePreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the drop down options of message preferences 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetMessagePreferenceOptions/{id}")]
        [ResponseType(typeof(IList<MessagePreferenceOptions>))]
        public IHttpActionResult getMessagePreferenceOptions(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.MessagePreference arc = new ARC.Donor.Service.Constituents.MessagePreference();
                return Ok(arc.getMessagePreferenceOptions(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR MessagePreferenceController :: getMessagePreferenceOptions : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


        [HttpPost]
        [Route("AddMessagePreference")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult addMessagePreference([FromBody] ARC.Donor.Business.Constituents.MessagePreferenceInput msgprefInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("MessagePreference", "Add", msgprefInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.MessagePreference p = new ARC.Donor.Service.Constituents.MessagePreference();
                    var searchResults = p.addMessagePreference(msgprefInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR MessagePreferenceController :: addMessagePreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


        [HttpPost]
        [Route("EditMessagePreference")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult editMessagePreference([FromBody] ARC.Donor.Business.Constituents.MessagePreferenceInput msgprefInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("MessagePreference", "Edit", msgprefInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.MessagePreference p = new ARC.Donor.Service.Constituents.MessagePreference();
                    var searchResults = p.editMessagePreference(msgprefInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR MessagePreferenceController :: editMessagePreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("DeleteMessagePreference")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult deleteMessagePreference([FromBody] ARC.Donor.Business.Constituents.MessagePreferenceInput msgprefInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("MessagePreference", "Delete", msgprefInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.MessagePreference p = new ARC.Donor.Service.Constituents.MessagePreference();
                    var searchResults = p.deleteMessagePreference(msgprefInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR MessagePreferenceController :: deleteMessagePreference : " + ex.Message;
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
                {"MessagePreference", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"i_mstr_id", "i_cnst_typ", "i_new_arc_srcsys_cd","i_new_msg_pref_typ", "i_new_msg_pref_val", 
                        "i_new_msg_pref_comm_chan","i_new_msg_pref_comm_chan_typ","i_new_line_of_service","i_user_id","i_msg_pref_exp_dt"}},
                    {"Edit", new List<string>() {"i_mstr_id", "i_cnst_typ", "i_new_arc_srcsys_cd","i_new_msg_pref_typ", "i_new_msg_pref_val", 
                        "i_new_msg_pref_comm_chan","i_new_msg_pref_comm_chan_typ","i_new_line_of_service","i_user_id","i_msg_pref_exp_dt",
                    "i_bk_arc_srcsys_cd","i_bk_msg_pref_typ","i_bk_msg_pref_val","i_bk_msg_pref_comm_chan","i_bk_msg_pref_comm_chan_typ","i_bk_line_of_service"}},
                    {"Delete", new List<string>()  {"i_mstr_id", "i_cnst_typ", "i_user_id","i_bk_arc_srcsys_cd","i_bk_msg_pref_typ",
                        "i_bk_msg_pref_val","i_bk_msg_pref_comm_chan","i_bk_msg_pref_comm_chan_typ","i_bk_line_of_service"}}
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
