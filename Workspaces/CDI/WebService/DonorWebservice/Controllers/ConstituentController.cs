using ARC.Donor.Business;
using ARC.Donor.Business.Constituent;
using ARC.Donor.Business.Constituents;
using ARC.Donor.Service;
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
    //[Authorize]
    [RoutePrefix("api/constituent")]
    public class ConstituentController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        ConstituentController()
        {
            
        }
        /// <summary>
        /// This is used to get simple search results
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("search")]
        [ResponseType(typeof(IList<ConstituentOutputSearchResults>))]
        public IHttpActionResult GetConstituentResults(ConstituentInputSearchModel postData)
        {
            try
            {
                ConstituentServices constituentServices = new ConstituentServices();
                constituentServices.InsertQueryLogEvent += constituentServices_InsertQueryLog;
                var results = constituentServices.getConstituentSearchResults(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentResults : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        private void constituentServices_InsertQueryLog(object sender, QueryLogEventArgs e)
        {
            var qry = new ClientValidation.QueryTimeLogger();
            qry.UserName = e.UserName;
            qry.Query = e.Query;
            qry.StartTime = e.StartTime;
            qry.EndTime = e.EndTime;
            qry.Action = e.Action;
            Models.QueryLogger.InsertQuery(qry);
        }
        /// <summary>
        /// This is used to get advanced search results by passing various parameters
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("advsearch")]
        [ResponseType(typeof(IList<ConstituentOutputSearchResults>))]
        public IHttpActionResult GetConstituentAdvSearchResults( ListConstituentInputSearchModel postData)
        {
           try
            {
                ConstituentServices constituentServices = new ConstituentServices();
                constituentServices.InsertQueryLogEvent += constituentServices_InsertQueryLog;
                var results = constituentServices.getConstituentAdvSearchResults(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentAdvSearchResults : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* Create, Delete and Update API's for Person Name*/

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("addconstituentpersonname")]
        public IHttpActionResult PostAddConstPersonName([FromBody] ARC.Donor.Business.Constituents.ConstituentPersonNameInput ConstNameInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("PersonName", "Add", ConstNameInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Name p = new ARC.Donor.Service.Constituents.Name();
                var searchResults = p.addConstituentPersonName(ConstNameInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddConstPersonName : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("deleteconstituentpersonname")]
        public IHttpActionResult PostDeleteConstPersonName([FromBody] ARC.Donor.Business.Constituents.ConstituentPersonNameInput ConstNameInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("PersonName", "Delete", ConstNameInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.Name p = new ARC.Donor.Service.Constituents.Name();
                    var searchResults = p.deleteConstituentPersonName(ConstNameInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostDeleteConstPersonName : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("updateconstituentpersonname")]
        public IHttpActionResult PostUpdateConstPersonName([FromBody]  ARC.Donor.Business.Constituents.ConstituentPersonNameInput ConstNameInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("PersonName", "Edit", ConstNameInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.Name p = new ARC.Donor.Service.Constituents.Name();
                    var searchResults = p.updateConstituentPersonName(ConstNameInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUpdateConstPersonName : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* Create, Delete and Update API's for Org Name*/
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("addconstituentorgname")]
        public IHttpActionResult PostAddConstOrgName([FromBody] ARC.Donor.Business.Constituents.ConstituentOrgNameInput ConstNameInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("OrgName", "Add", ConstNameInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Name p = new ARC.Donor.Service.Constituents.Name();
                var searchResults = p.addConstituentOrgName(ConstNameInput);
                return Ok(searchResults);
                    }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddConstOrgName : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("deleteconstituentorgname")]
        public IHttpActionResult PostDeleteConstOrgName([FromBody] ARC.Donor.Business.Constituents.ConstituentOrgNameInput ConstNameInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("OrgName", "Delete", ConstNameInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Name p = new ARC.Donor.Service.Constituents.Name();
                var searchResults = p.deleteConstituentOrgName(ConstNameInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: deleteconstituentorgname : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("updateconstituentorgname")]
        public IHttpActionResult PostUpdateConstOrgName([FromBody]  ARC.Donor.Business.Constituents.ConstituentOrgNameInput ConstNameInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("OrgName", "Edit", ConstNameInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Name p = new ARC.Donor.Service.Constituents.Name();
                var searchResults = p.updateConstituentOrgName(ConstNameInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUpdateConstOrgName : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* Create, Delete and Update API's for Phone*/
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("addconstituentphone")]
        public IHttpActionResult PostAddConstPhone([FromBody]  ARC.Donor.Business.Constituents.ConstituentPhoneInput ConstPhoneInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Phone", "Add", ConstPhoneInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Phone p = new ARC.Donor.Service.Constituents.Phone();
                var searchResults = p.addConstituentPhone(ConstPhoneInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddConstPhone : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("deleteconstituentphone")]
        public IHttpActionResult PostDeleteConstPhone([FromBody] ARC.Donor.Business.Constituents.ConstituentPhoneInput ConstPhoneInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Phone", "Delete", ConstPhoneInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Phone p = new ARC.Donor.Service.Constituents.Phone();
                var searchResults = p.deleteConstituentPhone(ConstPhoneInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostDeleteConstPhone : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("updateconstituentphone")]
        public IHttpActionResult PostUpdateConstPhone([FromBody] ARC.Donor.Business.Constituents.ConstituentPhoneInput ConstPhoneInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Phone", "Edit", ConstPhoneInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Phone p = new ARC.Donor.Service.Constituents.Phone();
                var searchResults = p.updateConstituentPhone(ConstPhoneInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUpdateConstPhone : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* Create, Delete and Update API's for Email*/
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("addconstituentemail")]
        public IHttpActionResult PostAddConstEmail([FromBody]  ARC.Donor.Business.Constituents.ConstituentEmailInput ConstEmailInput)
        {
            try
            {

                Boolean boolMandatoryCheck = checkMandatoryInputs("Email", "Add", ConstEmailInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.Email e = new ARC.Donor.Service.Constituents.Email();
                    var searchResults = e.addConstituentEmail(ConstEmailInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
                return Ok("Error");
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddConstEmail : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("deleteconstituentemail")]
        public IHttpActionResult PostDeleteConstEmail([FromBody] ARC.Donor.Business.Constituents.ConstituentEmailInput ConstEmailInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Email", "Delete", ConstEmailInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.Email e = new ARC.Donor.Service.Constituents.Email();
                    var searchResults = e.deleteConstituentEmail(ConstEmailInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
                return Ok("Error");
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostDeleteConstEmail : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("updateconstituentemail")]
        public IHttpActionResult PostUpdateConstEmail([FromBody] ARC.Donor.Business.Constituents.ConstituentEmailInput ConstEmailInput)
        {
            try
            {

                Boolean boolMandatoryCheck = checkMandatoryInputs("Email", "Edit", ConstEmailInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.Email e = new ARC.Donor.Service.Constituents.Email();
                    var searchResults = e.updateConstituentEmail(ConstEmailInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
                return Ok("Error");
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUpdateConstEmail : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* Create, Delete and Update API's for Birth*/
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("addconstituentbirth")]
        public IHttpActionResult PostAddConstBirth([FromBody] ARC.Donor.Business.Constituents.ConstituentBirthInput ConstBirthInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Birth", "Add", ConstBirthInput);
                if (boolMandatoryCheck)
                {
                    bool boolIsValidDate = IsValidDate(Convert.ToInt32(ConstBirthInput.NewBirthYearNumber), Convert.ToInt32(ConstBirthInput.NewBirthMonthNumber), Convert.ToInt32(ConstBirthInput.NewBirthDayNumber));
                    if (boolIsValidDate == true)
                    {
                        ARC.Donor.Service.Constituents.Birth b = new ARC.Donor.Service.Constituents.Birth();
                        var searchResults = b.addConstituentBirth(ConstBirthInput);
                        return Ok(searchResults);
                    }
                    else
                    {
                        return Ok("Invalid value in one of the Birth fields");
                    }
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddConstBirth : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("deleteconstituentbirth")]
        public IHttpActionResult PostDeleteConstBirth([FromBody] ARC.Donor.Business.Constituents.ConstituentBirthInput ConstBirthInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Birth", "Delete", ConstBirthInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Birth b = new ARC.Donor.Service.Constituents.Birth();
                var searchResults = b.deleteConstituentBirth(ConstBirthInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostDeleteConstBirth : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("updateconstituentbirth")]
        public IHttpActionResult PostUpdateConstBirth([FromBody] ARC.Donor.Business.Constituents.ConstituentBirthInput ConstBirthInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Birth", "Edit", ConstBirthInput);
                if (boolMandatoryCheck)
                {
                    bool boolIsValidDate = IsValidDate(Convert.ToInt32(ConstBirthInput.NewBirthYearNumber), Convert.ToInt32(ConstBirthInput.NewBirthMonthNumber), Convert.ToInt32(ConstBirthInput.NewBirthDayNumber));
                    if (boolIsValidDate == true)
                    {
                        ARC.Donor.Service.Constituents.Birth b = new ARC.Donor.Service.Constituents.Birth();
                        var searchResults = b.updateConstituentBirth(ConstBirthInput);
                        return Ok(searchResults);
                    }
                    else
                    {
                        return Ok("Invalid value in one of the Birth fields");
                    }
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUpdateConstBirth : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* Create, Delete and Update API's for Death*/
        [HttpPost]
        [Route("addconstituentdeath")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostAddConstDeath([FromBody] ARC.Donor.Business.Constituents.ConstituentDeathInput ConstDeathInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Death", "Add", ConstDeathInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Death d = new ARC.Donor.Service.Constituents.Death();
                var searchResults = d.addConstituentDeath(ConstDeathInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddConstDeath : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("deleteconstituentdeath")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDeleteConstDeath([FromBody] ARC.Donor.Business.Constituents.ConstituentDeathInput ConstDeathInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Death", "Delete", ConstDeathInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Death d = new ARC.Donor.Service.Constituents.Death();
                var searchResults = d.deleteConstituentDeath(ConstDeathInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostDeleteConstDeath : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("updateconstituentdeath")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUpdateConstDeath([FromBody] ARC.Donor.Business.Constituents.ConstituentDeathInput ConstDeathInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Death", "Edit", ConstDeathInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Death d = new ARC.Donor.Service.Constituents.Death();
                var searchResults = d.updateConstituentDeath(ConstDeathInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUpdateConstDeath : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* Create, Delete and Update API's for Address*/
        [HttpPost]
        [Route("addconstituentaddress")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostAddConstAddress([FromBody] ARC.Donor.Business.Constituents.ConstituentAddressInput ConstAddressInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Address", "Add", ConstAddressInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Address a = new ARC.Donor.Service.Constituents.Address();
                var searchResults = a.addConstituentAddress(ConstAddressInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddConstAddress : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("deleteconstituentaddress")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDeleteConstAddress([FromBody] ARC.Donor.Business.Constituents.ConstituentAddressInput ConstAddressInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Address", "Delete", ConstAddressInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Address a = new ARC.Donor.Service.Constituents.Address();
                var searchResults = a.deleteConstituentAddress(ConstAddressInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostDeleteConstAddress : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("updateconstituentaddress")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUpdateConstAddress([FromBody] ARC.Donor.Business.Constituents.ConstituentAddressInput ConstAddressInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Address", "Edit", ConstAddressInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Address a = new ARC.Donor.Service.Constituents.Address();
                var searchResults = a.updateConstituentAddress(ConstAddressInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUpdateConstAddress : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* Create, Delete and Update API's for Contact Preference*/
        [HttpPost]
        [Route("addconstituentcontactprefc")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostAddConstContactPreference([FromBody] ARC.Donor.Business.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("ContactPreference", "Add", ConstContactPrefcInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.ContactPreference cp = new ARC.Donor.Service.Constituents.ContactPreference();
                cp.InsertQueryLogEvent += constituentServices_InsertQueryLog;
                var searchResults = cp.addConstituentContactPrefc(ConstContactPrefcInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddConstContactPreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("deleteconstituentcontactprefc")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDeleteConstContactPreference([FromBody] ARC.Donor.Business.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("ContactPreference", "Delete", ConstContactPrefcInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.ContactPreference cp = new ARC.Donor.Service.Constituents.ContactPreference();
                var searchResults = cp.deleteConstituentContactPrefc(ConstContactPrefcInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostDeleteConstContactPreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("updateconstituentcontactprefc")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUpdateConstContactPreference([FromBody] ARC.Donor.Business.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("ContactPreference", "Edit", ConstContactPrefcInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.ContactPreference cp = new ARC.Donor.Service.Constituents.ContactPreference();
                var searchResults = cp.updateConstituentContactPrefc(ConstContactPrefcInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUpdateConstContactPreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }



        /* Create, Delete and Update API's for Chatacteristics*/
        [HttpPost]
        [Route("addconstituentcharacteristics")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostAddConstCharacteristics([FromBody] ARC.Donor.Business.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Characteristics", "Add", ConstCharacteristicsInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Characteristicscs ch = new ARC.Donor.Service.Constituents.Characteristicscs();
                var searchResults = ch.addConstituentCharacteristics(ConstCharacteristicsInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddConstCharacteristics : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("deleteconstituentcharacteristics")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDeleteConstCharacteristics([FromBody] ARC.Donor.Business.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Characteristics", "Delete", ConstCharacteristicsInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Characteristicscs ch = new ARC.Donor.Service.Constituents.Characteristicscs();
                var searchResults = ch.deleteConstituentCharacteristics(ConstCharacteristicsInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostDeleteConstCharacteristics : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("updateconstituentcharacteristics")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUpdateConstCharacteristics([FromBody] ARC.Donor.Business.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Characteristics", "Edit", ConstCharacteristicsInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.Characteristicscs ch = new ARC.Donor.Service.Constituents.Characteristicscs();
                var searchResults = ch.updateConstituentCharacteristics(ConstCharacteristicsInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUpdateConstCharacteristics : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /*Create and Delete API for Org Affiliators*/
        [HttpPost]
        [Route("addorgaffiliators")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostAddOrgAffiliators([FromBody] ARC.Donor.Business.Constituents.OrgAffiliatorsInput OrgAffiliatorsInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("OrgAffiliator", "Add", OrgAffiliatorsInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.OrgAffiliators af = new ARC.Donor.Service.Constituents.OrgAffiliators();
                var searchResults = af.addOrgAffiliators(OrgAffiliatorsInput);
                return Ok(searchResults);}
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddOrgAffiliators : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("deleteorgaffiliators")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDeleteOrgAffiliators([FromBody] ARC.Donor.Business.Constituents.OrgAffiliatorsInput OrgAffiliatorsInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("OrgAffiliator", "Delete", OrgAffiliatorsInput);
                if (boolMandatoryCheck)
                {
                ARC.Donor.Service.Constituents.OrgAffiliators af = new ARC.Donor.Service.Constituents.OrgAffiliators();
                var searchResults = af.deleteOrgAffiliators(OrgAffiliatorsInput);
                return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostDeleteOrgAffiliators : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


        /*
         * The below methods are get api's calls to each section of details
         * */
        /// <summary>
        /// Get constituent master details, used in constituent tab.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [Route("GetConstituentMasterDetails/{id}")]
        //[Authorize]
        [ResponseType(typeof(IList<ConstituentMaster>))]
        public IHttpActionResult GetConstituentMasterDetails(string id)
        {

            try
            {
                ARC.Donor.Service.Constituents.ConstituentMaster arc = new ARC.Donor.Service.Constituents.ConstituentMaster();
                return Ok(arc.getConstituentMasterDetails(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentMasterDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This api is used to get the individual details of a constituent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetConstituentPersonName/{id}")]
        //[Authorize]
        [ResponseType(typeof(IList<PersonName>))]
        public IHttpActionResult GetConstituentPersonName(string id)
        {

            try
            {
                ARC.Donor.Service.Constituents.Name arc = new ARC.Donor.Service.Constituents.Name();
                return Ok(arc.getConstituentPersonName(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentPersonName : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the organization details of a constituent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetConstituentOrgName/{id}")]
        //[Authorize]
        [ResponseType(typeof(IList<OrgName>))]
        public IHttpActionResult GetConstituentOrgName(string id)
        {

            try
            {
                ARC.Donor.Service.Constituents.Name arc = new ARC.Donor.Service.Constituents.Name();
                return Ok(arc.getConstituentOrgName(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentOrgName : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the address details of a constituent... pass master id as string ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetConstituentAddress/{id}")]
        //[Authorize]
        [ResponseType(typeof(IList<Address>))]
        public IHttpActionResult GetConstituentAddress(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.Address arc = new ARC.Donor.Service.Constituents.Address();
                var res = arc.getConstituentAddress(10, 1, id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentAddress : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get email details of a constituent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetConstituentEmail/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<Email>))]
        public IHttpActionResult GetConstituentEmail(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.Email arc = new ARC.Donor.Service.Constituents.Email();
                return Ok(arc.getConstituentEmail(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentEmail : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This API is used to get the phone details of constituent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetConstituentPhone/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<Phone>))]
        public IHttpActionResult GetConstituentPhone(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.Phone arc = new ARC.Donor.Service.Constituents.Phone();
                return Ok(arc.getConstituentPhone(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentPhone : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// ARC best summary details about a searched master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetARCBest/{id}")]
        // [Authorize]
        [ResponseType(typeof(IList<ARCBest>))]
        public IHttpActionResult GetARCBest(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.ARCBest arc = new ARC.Donor.Service.Constituents.ARCBest();
                return Ok(arc.getARCBest(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetARCBest : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
      /// <summary>
      /// Birth details of a constituent 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        [Route("getconstituentbirth/{id}")]
        [ResponseType(typeof(IList<Birth>))]
        public IHttpActionResult GetConstituentBirth(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.Birth arc = new ARC.Donor.Service.Constituents.Birth();
                return Ok(arc.getConstituentBirth(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentBirth : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Use this to get the characteristics details of a master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentcharacteristics/{id}")]
        [ResponseType(typeof(IList<Characteristics>))]
        public IHttpActionResult GetConstituentCharacteristics(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.Characteristicscs arc = new ARC.Donor.Service.Constituents.Characteristicscs();
                return Ok(arc.getConstituentCharacteristics(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentCharacteristics : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Not used anymore
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentcontactpreference/{id}")]
        public IHttpActionResult GetConstituentContactPreference(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.ContactPreference arc = new ARC.Donor.Service.Constituents.ContactPreference();
                arc.InsertQueryLogEvent += constituentServices_InsertQueryLog;
                return Ok(arc.getConstituentContactPreference(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentContactPreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the demise details of a constituent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentdeath/{id}")]
        [ResponseType(typeof(IList<Death>))]
        public IHttpActionResult GetConstituentDeath(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.Death arc = new ARC.Donor.Service.Constituents.Death();
                return Ok(arc.getConstituentDeath(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentDeath : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// External bridge details of a master. Gives you the sources from which a master id got the info from
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentexternalbridge/{id}")]
        [ResponseType(typeof(IList<ExternalBridge>))]
        public IHttpActionResult GetConstituentExternalBridge(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.ExternalBridge arc = new ARC.Donor.Service.Constituents.ExternalBridge();
                return Ok(arc.getConstituentExternalBridge(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentExternalBridge : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This section is moved to CEM functionality
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentgroupmembership/{id}")]
        [ResponseType(typeof(IList<GroupMembership>))]
        public IHttpActionResult GetConstituentGroupMembership(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.GroupMembership arc = new ARC.Donor.Service.Constituents.GroupMembership();
                return Ok(arc.getConstituentGroupMembership(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentGroupMembership : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Internal bridge details of constituent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentinternalbridge/{id}")]
        [ResponseType(typeof(IList<InternalBridge>))]
        public IHttpActionResult GetConstituentInternalBridge(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.InternalBridge arc = new ARC.Donor.Service.Constituents.InternalBridge();
                return Ok(arc.getConstituentInternalBridge(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentInternalBridge : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Mastering details of a master id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentmasteringattempts/{id}")]
        [ResponseType(typeof(IList<MasteringAttempts>))]
        public IHttpActionResult GetConstituentMasteringAttempts(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.MasteringAttempts arc = new ARC.Donor.Service.Constituents.MasteringAttempts();
                return Ok(arc.getConstituentMasteringAttempts(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentMasteringAttempts : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Merge history of a constituent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentmergehistory/{id}")]
        [ResponseType(typeof(IList<MergeHistory>))]
        public IHttpActionResult GetConstituentMergeHistory(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.MergeHistory arc = new ARC.Donor.Service.Constituents.MergeHistory();
                return Ok(arc.getConstituentMergeHistory(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentMergeHistory : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Not used in constituent tab. But this api can be used to get the relationship of a constituent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentrelationship/{id}")]
        [ResponseType(typeof(IList<Relationship>))]
        public IHttpActionResult GetConstituentRelationship(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.Relationship arc = new ARC.Donor.Service.Constituents.Relationship();
                return Ok(arc.getConstituentRelationship(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentRelationship : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// get the summary details of a master id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentsummary/{id}")]
        [ResponseType(typeof(IList<Summary>))]
        public IHttpActionResult GetConstituentSummary(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.Summary arc = new ARC.Donor.Service.Constituents.Summary();
                return Ok(arc.getConstituentSummary(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentSummary : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// All the transaction history related to a master id can retrieved
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituenttransactionhistory/{id}")]
        [ResponseType(typeof(IList<TransactionHistory>))]
        public IHttpActionResult GetConstituentTransactionHistory(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.TransactionHistory arc = new ARC.Donor.Service.Constituents.TransactionHistory();
                return Ok(arc.getConstituentTransactionHistory(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentTransactionHistory : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Not used in constituent tab.This is used to get private info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentprivateinformation/{id}")]
        [ResponseType(typeof(IList<Private>))]
        public IHttpActionResult GetConstituentPrivateInformation(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.Private arc = new ARC.Donor.Service.Constituents.Private();
                return Ok(arc.getConstituentPrivateInformation(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentPrivateInformation : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the anonymous info of a master id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentanonymousinformation/{id}")]
        //[Authorize]
        [ResponseType(typeof(IList<Anonymous>))]
        public IHttpActionResult GetConstituentAnonymousInformation(string id)
        {

            try
            {
                ARC.Donor.Service.Constituents.Anonymous arc = new ARC.Donor.Service.Constituents.Anonymous();
                return Ok(arc.getConstituentAnonymousInformation(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentAnonymousInformation : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get all the old master ids that were previously mapped to a master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentoldmasters/{id}")]
        [ResponseType(typeof(IList<OldMaster>))]
        public IHttpActionResult GetConstituentOldMasters(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.OldMaster arc = new ARC.Donor.Service.Constituents.OldMaster();
                return Ok(arc.getConstituentOldMasters(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentOldMasters : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the affiliator of a master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getconstituentorgaffiliators/{id}")]
        [ResponseType(typeof(IList<OrgAffiliators>))]
        public IHttpActionResult GetConstituentOrgAffiliators(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.OrgAffiliators arc = new ARC.Donor.Service.Constituents.OrgAffiliators();
                return Ok(arc.getConstituentOrgAffiliators(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentOrgAffiliators : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        
        /*API methods for show sectional details*/
        /// <summary>
        /// Get more name details of a master
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(IList<PersonNameDetailsOutput>))]
        [Route("showpersonnamedetails")]
        public IHttpActionResult PostShowPersonNameDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.PersonNameDetails pd = new ARC.Donor.Service.Constituents.PersonNameDetails();
                var searchResults = pd.showPersonNameDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowPersonNameDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// get more org name details of a master id 
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showorgnamedetails")]
        [ResponseType(typeof(IList<OrgNameDetailsOutput>))]
        public IHttpActionResult PostShowOrgNameDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.OrgNameDetails od = new ARC.Donor.Service.Constituents.OrgNameDetails();
                var searchResults = od.showOrgNameDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowOrgNameDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Retrieve more details of an address
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showaddressdetails")]
        [ResponseType(typeof(IList<AddressDetailsOutput>))]
        public IHttpActionResult PostShowAddressDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.AddressDetails od = new ARC.Donor.Service.Constituents.AddressDetails();
                var searchResults = od.showAddressDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowAddressDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get more phone details about a masterid 
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showphonedetails")]
        [ResponseType(typeof(IList<PhoneDetailsOutput>))]
        public IHttpActionResult PostShowPhoneDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.PhoneDetails od = new ARC.Donor.Service.Constituents.PhoneDetails();
                var searchResults = od.showPhoneDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowPhoneDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get more email details of a constituent
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showemaildetails")]
        [ResponseType(typeof(IList<EmailDetailsOutput>))]
        public IHttpActionResult PostShowEmailDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.EmailDetails od = new ARC.Donor.Service.Constituents.EmailDetails();
                var searchResults = od.showEmailDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowEmailDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get more details of birth of a constituent 
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showbirthdetails")]
        [ResponseType(typeof(IList<BirthDetailsOutput>))]
        public IHttpActionResult PostShowBirthDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.BirthDetails od = new ARC.Donor.Service.Constituents.BirthDetails();
                var searchResults = od.showBirthDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowBirthDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the death details of a constituent
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showdeathdetails")]
        [ResponseType(typeof(IList<DeathDetailsOutput>))]
        public IHttpActionResult PostShowDeathDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.DeathDetails od = new ARC.Donor.Service.Constituents.DeathDetails();
                var searchResults = od.showDeathDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowDeathDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Moved to CEM
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showcontactpreferencedetails")]
        public IHttpActionResult PostShowContactPreferenceDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.ContactPreferenceDetails od = new ARC.Donor.Service.Constituents.ContactPreferenceDetails();
                var searchResults = od.showContactPreferenceDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowContactPreferenceDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This is used to get more details of characteristics of a master id
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showcharacteristicsdetails")]
        [ResponseType(typeof(IList<CharacteristicsDetailsOutput>))]
        public IHttpActionResult PostShowCharacteristicsDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.CharacteristicsDetails od = new ARC.Donor.Service.Constituents.CharacteristicsDetails();
                var searchResults = od.showCharacteristicsDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowCharacteristicsDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// External details of a constituent 
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showexternalbridgedetails")]
        [ResponseType(typeof(IList<ExternalBridgeDetailsOutput>))]
        public IHttpActionResult PostShowExternalBridgeDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.ExternalBridgeDetails od = new ARC.Donor.Service.Constituents.ExternalBridgeDetails();
                var searchResults = od.showExternalBridgeDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowExternalBridgeDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get more details of internal bridge
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showinternalbridgedetails")]
        [ResponseType(typeof(IList<InternalBridgeDetailsOutput>))]
        public IHttpActionResult PostShowInternalBridgeDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.InternalBridgeDetails od = new ARC.Donor.Service.Constituents.InternalBridgeDetails();
                var searchResults = od.showInternalBridgeDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowInternalBridgeDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Master metrics of a master id 
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showmastermetricdetails")]
        [ResponseType(typeof(IList<MasterMetricsDetailsOutput>))]
        public IHttpActionResult PostShowMasterMetricsDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.MasterMetricsDetails od = new ARC.Donor.Service.Constituents.MasterMetricsDetails();
                var searchResults = od.showMasterMetricsDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowMasterMetricsDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// FSA details of masterid.. not used in constituent tab
        /// </summary>
        /// <param name="ShowDetailsInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("showfsarelationshipdetails")]
        [ResponseType(typeof(IList<FSARelationshipDetailsOutput>))]
        public IHttpActionResult PostShowFsaRelationshipDetails([FromBody] ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.FSARelationshipDetails od = new ARC.Donor.Service.Constituents.FSARelationshipDetails();
                var searchResults = od.showFSARelationshipDetails(ShowDetailsInput);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostShowFsaRelationshipDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* API method for the merge compare functionality */
        [HttpPost]
        [Route("getcomparedata")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult GetCompareData([FromBody] ARC.Donor.Business.Constituents.MasterDetailsInput MasterDetailsInput)
        {
            try
            {
                //Check if atleast one master id and atmost 5 distinct master ids are provided for compare
                if (MasterDetailsInput.MasterId.Distinct().ToList().Count > 0 & MasterDetailsInput.MasterId.Distinct().ToList().Count <= 5)
                {
                    ARC.Donor.Service.Constituents.ConstituentMerge c = new ARC.Donor.Service.Constituents.ConstituentMerge();
                    var results = c.getCompareData(MasterDetailsInput);
                    return Ok(results);
                }
                else
                {
                    if (MasterDetailsInput.MasterId.Distinct().ToList().Count < 1)
                        return Ok("Please provide atleast one master to compare");
                    if (MasterDetailsInput.MasterId.Distinct().ToList().Count > 5)
                        return Ok("Please choose 5 or less masters to compare");
                }
                return Ok("Error");
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetCompareData : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* API methods to submit merge requests */
        [HttpPost]
        [Route("mergemaster")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostMergeMasters([FromBody] ARC.Donor.Business.Constituents.MergeInput MergeInput)
        {
            try
            {
                //Check if atleast two master id's are given as part of the merge request
                if (MergeInput.MasterIds.Distinct().ToList().Count > 1)
                {
                    ARC.Donor.Service.Constituents.ConstituentMerge c = new ARC.Donor.Service.Constituents.ConstituentMerge();
                    var searchResults = c.mergeMasters(MergeInput);
                    return Ok(searchResults);
                }
                else
                {
                    if (MergeInput.MasterIds.Distinct().ToList().Count <= 1)
                        return Ok("Please provide atleast two masters as part of merge request");
                }
                return Ok("Error");
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostMergeMasters : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* API methods to submit unmerge requests */
        [HttpPost]
        [Route("unmerge")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUnmerge([FromBody] ARC.Donor.Business.Constituents.UnmergeInput UnmergeInput)
        {
            try
            {
                List<string> listConstType = UnmergeInput.UnmergeRequestDetails.AsEnumerable().Select(x => x.ConstituentType).Distinct().ToList();
                List<string> listSourcesystemid = UnmergeInput.UnmergeRequestDetails.AsEnumerable().Select(x => x.SourceSystemId).Distinct().ToList();

                //Check if atleast one brige is given as part of the unmerge request
                //Also check if all the bridges corresponds to the same constituent type
                if (listConstType.Count == 1 && listSourcesystemid.Count > 0)
                {
                    ARC.Donor.Service.Constituents.Unmerge c = new ARC.Donor.Service.Constituents.Unmerge();
                    var searchResults = c.unmerge(UnmergeInput);
                    return Ok(searchResults);
                }
                else
                {
                    if (listConstType.Count > 1)
                        return Ok("Please ensure that only individuals or only organizations are submitted as part of a single unmerge request");
                    if (listSourcesystemid.Count < 1)
                        return Ok("Please provide atleast one brige as part of unmerge request");
                }
                return Ok("Error");
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUnmerge : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        //CEM Web Services 
        /// <summary>
        /// Get the DNC records of masterid 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getConstituentDNCrecords/{id}")]
        [ResponseType(typeof(IList<ConstituentDNC>))]
        public IHttpActionResult GetConstituentDNCRecords(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.ConstituentDNC arc = new ARC.Donor.Service.Constituents.ConstituentDNC();
                return Ok(arc.getConstituentDNCrecords(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetConstituentDNCRecords : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Get the prferred communication channel of a constituent 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getPreferredCommunicationChannel/{id}")]
        [ResponseType(typeof(IList<PreferredComChannel>))]
        public IHttpActionResult GetPreferredCommunicationChannel(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.PreferredComChannel arc = new ARC.Donor.Service.Constituents.PreferredComChannel();
                return Ok(arc.getPreferredComChannel(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetPreferredCommunicationChannel : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Get the preferred locator of a master id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getPreferredLocator/{id}")]
        [ResponseType(typeof(IList<PreferredLocator>))]
        public IHttpActionResult GetPreferredLocator(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.PreferredLocator arc = new ARC.Donor.Service.Constituents.PreferredLocator();
                return Ok(arc.getPreferredLocator(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetPreferredLocator : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the message prefernce of a master id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getMessagePreferences/{id}")]
        [ResponseType(typeof(IList<MessagePreference>))]
        public IHttpActionResult GetMessagePreferences(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.MessagePreference arc = new ARC.Donor.Service.Constituents.MessagePreference();
                return Ok(arc.getMessagePreference(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: GetMessagePreferences : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("addchannelpreference")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostAddChannelPreference(ARC.Donor.Business.Constituents.PreferredComChannelInput preferredChannelInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("ChannelPreference", "Add", preferredChannelInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.PreferredComChannel prefComChannel = new ARC.Donor.Service.Constituents.PreferredComChannel();
                    var searchResults = prefComChannel.addPreferredComChannel(preferredChannelInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch(Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddChannelPreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("updatechannelpreference")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUpdateChannelPreference(ARC.Donor.Business.Constituents.PreferredComChannelInput preferredChannelInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("ChannelPreference", "Edit", preferredChannelInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.PreferredComChannel prefComChannel = new ARC.Donor.Service.Constituents.PreferredComChannel();
                    var searchResults = prefComChannel.updatePreferredComChannel(preferredChannelInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUpdateChannelPreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("inactivatechannelpreference")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostInactivateChannelPreference(ARC.Donor.Business.Constituents.PreferredComChannelInput preferredChannelInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("ChannelPreference", "Delete", preferredChannelInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.PreferredComChannel prefComChannel = new ARC.Donor.Service.Constituents.PreferredComChannel();
                    var searchResults = prefComChannel.inactivatePreferredComChannel(preferredChannelInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostInactivateChannelPreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("addlocatorpreference")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostAddLocatorPreference(ARC.Donor.Business.Constituents.PreferredLocatorInput preferredLocatorInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("ChannelPreference", "Add", preferredLocatorInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.PreferredLocator prefComChannel = new ARC.Donor.Service.Constituents.PreferredLocator();
                    var searchResults = prefComChannel.addPreferredLocator(preferredLocatorInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostAddLocatorPreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }



        [HttpPost]
        [Route("updatelocatorpreference")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUpdateLocatorPreference(ARC.Donor.Business.Constituents.PreferredLocatorInput preferredLocatorInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("ChannelPreference", "Edit", preferredLocatorInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.PreferredLocator prefComChannel = new ARC.Donor.Service.Constituents.PreferredLocator();
                    var searchResults = prefComChannel.updatePreferredLocator(preferredLocatorInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostUpdateLocatorPreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


        [HttpPost]
        [Route("inactivatelocatorpreference")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostInactivateLocatorPreference(ARC.Donor.Business.Constituents.PreferredLocatorInput preferredLocatorInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("ChannelPreference", "Delete", preferredLocatorInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.PreferredLocator prefComChannel = new ARC.Donor.Service.Constituents.PreferredLocator();
                    var searchResults = prefComChannel.inactivatePreferredLocator(preferredLocatorInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: PostInactivateLocatorPreference : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Get alternate ids of a master id
        /// </summary>
        /// <param name="altInput"></param>
        /// <returns></returns>
        [Route("GetSourceSystemAlternateIds")]
        [HttpPost]
        [ResponseType(typeof(IList<AlternateIds>))]
        public IHttpActionResult GetAlternateIds(AlternateIdsInput altInput)
        {
            try
            {
                ARC.Donor.Service.Constituents.AlternateIds arc = new ARC.Donor.Service.Constituents.AlternateIds();
                return Ok(arc.getSourceSystemAlternateIds(50, 1, altInput));
            }
            catch (Exception ex)
            {
                return Ok("Error");
            }
        }

        //[HttpPost]
        //[Route("addmessagepreference")]
        //public IHttpActionResult PostAddMesssagePreference(ARC.Donor.Business.Constituents.MessagePreference preferredLocatorInput)
        //{
        //    try
        //    {
        //        Boolean boolMandatoryCheck = checkMandatoryInputs("ChannelPreference", "Add", preferredLocatorInput);
        //        if (boolMandatoryCheck)
        //        {
        //           // ARC.Donor.Service.Constituents.PreferredLocator prefComChannel = new ARC.Donor.Service.Constituents.PreferredLocator();
        //            //var searchResults = prefComChannel.addPreferredLocator(preferredLocatorInput);
        //            //return Ok(searchResults);
        //        }
        //        else
        //        {
        //            return Ok("Please provide the necessary inputs");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok("Error");
        //    }
        //}


        private Boolean checkMandatoryInputs(string strRequestType, string strActionType, object InputObj)
        {
            Boolean boolMandatoryCheck = true;
            //Dictionary to hold the list of columns which are mandatory while 
            Dictionary<string, Dictionary<string, List<string>>> dictMandatoryLibrary = new Dictionary<string, Dictionary<string, List<string>>>()
            {
                {"PersonName", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"MasterID", "FirstName", "LastName", "SourceSystemCode", "NameTypeCode"}},
                    {"Edit", new List<string>() {"MasterID", "OldSourceSystemCode", "OldNameTypeCode", "FirstName", "LastName", "SourceSystemCode", "NameTypeCode"}},
                    {"Delete", new List<string>() {"MasterID", "OldSourceSystemCode", "OldNameTypeCode"}}}
                },
                {"OrgName", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"MasterID", "OrgName", "SourceSystemCode", "OrgNameTypeCode"}},
                    {"Edit", new List<string>() {"MasterID", "OldSourceSystemCode", "OldOrgNameTypeCode", "OrgName", "SourceSystemCode", "OrgNameTypeCode"}},
                    {"Delete", new List<string>() {"MasterID", "OldSourceSystemCode", "OldOrgNameTypeCode"}}}
                },
                {"Address", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"MasterID", "AddressLine1", "City", "Zip5", "SourceSystemCode", "AddressTypeCode"}},
                    {"Edit", new List<string>() {"MasterID", "OldSourceSystemCode", "OldAddressTypeCode", "AddressLine1", "City", "Zip5", "SourceSystemCode", "AddressTypeCode"}},
                    {"Delete", new List<string>() {"MasterID", "OldSourceSystemCode", "OldAddressTypeCode"}}}
                },
                {"Email", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"MasterID", "NewEmailAddress", "SourceSystemCode", "EmailTypeCode"}},
                    {"Edit", new List<string>() {"MasterID", "OldSourceSystemCode", "OldEmailTypeCode", "NewEmailAddress", "SourceSystemCode", "EmailTypeCode"}},
                    {"Delete", new List<string>() {"MasterID", "OldSourceSystemCode", "OldEmailTypeCode"}}}
                },
                {"Phone", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"MasterID", "PhoneNumber", "SourceSystemCode", "PhoneTypeCode"}},
                    {"Edit", new List<string>() {"MasterID", "OldSourceSystemCode", "OldPhoneTypeCode", "PhoneNumber", "SourceSystemCode", "PhoneTypeCode"}},
                    {"Delete", new List<string>() {"MasterID", "OldSourceSystemCode", "OldPhoneTypeCode"}}}
                },
                {"Birth", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"MasterID", "NewBirthDayNumber", "NewBirthMonthNumber", "NewBirthYearNumber", "SourceSystemCode"}},
                    {"Edit", new List<string>() {"MasterID", "OldSourceSystemCode", "NewBirthDayNumber", "NewBirthMonthNumber", "NewBirthYearNumber", "SourceSystemCode"}},
                    {"Delete", new List<string>() {"MasterID", "OldSourceSystemCode"}}}
                },
                {"Death", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"MasterID", "NewDeathDate", "NewDeceasedCode", "SourceSystemCode"}},
                    {"Edit", new List<string>() {"MasterID", "OldSourceSystemCode", "NewDeathDate", "NewDeceasedCode", "SourceSystemCode"}},
                    {"Delete", new List<string>() {"MasterID", "OldSourceSystemCode"}}}
                },
                {"ContactPreference", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"MasterID", "ContactPrefcValue", "SourceSystemCode", "ContactPrefcTypeCode"}},
                    {"Edit", new List<string>() {"MasterID", "OldContactPrefcValue", "OldSourceSystemCode", "OldContactPrefcTypeCode", "ContactPrefcValue", "SourceSystemCode", "ContactPrefcTypeCode"}},
                    {"Delete", new List<string>() {"MasterID", "OldContactPrefcValue", "OldSourceSystemCode", "OldContactPrefcTypeCode"}}}
                },
                {"Characteristics", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"MasterID", "CharacteristicValue", "SourceSystemCode", "CharacteristicTypeCode"}},
                    {"Edit", new List<string>() {"MasterID", "OldCharacteristicValue", "OldSourceSystemCode", "OldCharacteristicTypeCode", "CharacteristicValue", "SourceSystemCode", "CharacteristicTypeCode"}},
                    {"Delete", new List<string>() {"MasterID", "OldCharacteristicValue", "OldSourceSystemCode", "OldCharacteristicTypeCode"}}}
                },
                {"GroupMembership", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"MasterID", "GroupKey", "AssignmentMethod"}},
                    {"Edit", new List<string>() {"MasterID", "OldGroupKey", "OldAssignmentMethod", "AssignmentMethod"}},
                    {"Delete", new List<string>() {"MasterID", "OldGroupKey", "OldAssignmentMethod"}}}
                },
                {"OrgAffiliator", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"mstr_id", "new_ent_org_id"}},
                    {"Delete", new List<string>() {"mstr_id", "bk_ent_org_id"}}}
                },
                {"ChannelPreference", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"mstr_id", "los_cd", "pref_chan"}},
                    {"Edit", new List<String>() {"mstr_id", "los_cd", "pref_chan"}},
                    {"Delete", new List<string>(){"mstr_id"}}}
                },
                {"LocatorPreference", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"cnst_mstr_id", "los_cd", "pref_loc_typ", "pref_loc_id" }},
                    {"Edit", new List<String>() {"cnst_mstr_id", "los_cd", "pref_loc_typ", "pref_loc_id" }},
                    {"Delete", new List<string>(){"cnst_mstr_id"}}}
                }

            };

            //Fetch the mandatory fields for the inputted details type and action types
            List<string> listMandatoryColumns = new List<string>();
            foreach (KeyValuePair<string, Dictionary<string, List<string>>> keyValuePair in dictMandatoryLibrary)
            {
                if(keyValuePair.Key == strRequestType)
                {
                    foreach(KeyValuePair<string, List<string>> innerkeyValuePair in keyValuePair.Value)
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

        public static bool IsValidDate(int year, int month, int day)
        {
            if (month < 1 || month > 12)
                return false;
            DateTime Birthdate;
            try
            {
                Birthdate = new DateTime(year, month, day);

                if (Birthdate > DateTime.Today)
                    return false;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }

            return day > 0 && day <= DateTime.DaysInMonth(year, month);
        }

        /* API methods to submit merge conflicts */
        [HttpPost]
        [Route("mergeconflicts")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostMergeConflicts([FromBody] ARC.Donor.Business.Constituents.MergeConflictInput MergeInput)
        {
            try
            {
                //Check if atleast two master id's are given as part of the merge request
                if (MergeInput.MasterIds.Distinct().ToList().Count > 1)
                {
                    ARC.Donor.Service.Constituents.ConstituentMerge c = new ARC.Donor.Service.Constituents.ConstituentMerge();
                    var searchResults = c.submitMergeConflicts(MergeInput);
                    return Ok(searchResults);
                }
                else
                {
                    if (MergeInput.MasterIds.Distinct().ToList().Count <= 1)
                        return Ok("Please provide atleast two masters as part of merge request");
                }
                return Ok("Error");
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: mergeconflicts : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get individaul master details linked with the given master id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetOrgRelationship/{id}")]
        [ResponseType(typeof(IList<OrgRelationship>))]
       public IHttpActionResult getOrgRelationship(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.OrgRelationship orgRel = new ARC.Donor.Service.Constituents.OrgRelationship();
                return Ok(orgRel.getOrgConstituentRelationship(10,1,id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR ConstituentController :: OrgRelationship : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

    }
}
