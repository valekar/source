using ARC.Donor.Business;
using ARC.Donor.Business.Case;
using ARC.Donor.Service;
using ARC.Donor.Service.Case;
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
    [RoutePrefix("api/case")]
    public class CaseController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        ///  
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        /// <summary>
        /// Constituents Case Advance Search ......
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("advcasesearch")]
         [ResponseType(typeof(IList<CaseOutputSearchResults>))]
        public IHttpActionResult GetCaseAdvSearchResults(ListCaseInputSearchModel postData)
        {
            try
            {
                CaseServices caseServices = new CaseServices();
                caseServices.InsertQueryLogEvent += constituentServices_InsertQueryLog;
                var results = caseServices.getCaseSearchResults(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: GetCaseAdvSearchResults : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

         [ApiExplorerSettings(IgnoreApi = true)]
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
        /// Get Transaction Details......
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        [Route("transactiondetails/{caseId}")]
        [ResponseType(typeof(IList<CaseTransactionDetails>))]
        public IHttpActionResult GetCaseTransactionDetails(int caseId)
        {
            try
            {
                CaseServices caseServices = new CaseServices();
                var results = caseServices.getCaseTransactionDetails(caseId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: GetCaseTransactionDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get the Locator details for case tab
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        [Route("locatordetails/{caseId}")]
        [ResponseType(typeof(IList<CaseLocatorDetails>))]
        public IHttpActionResult GetCaseLocatorDetails(int caseId)
        {
            try
            {
                CaseServices caseServices = new CaseServices();
                var results = caseServices.getCaseLocatorDetails(caseId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: GetCaseLocatorDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get case notes details
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        [Route("notesdetails/{caseId}")]       
        [ResponseType(typeof(IList<CaseNotesDetails>))]
        public IHttpActionResult GetCaseNotesDetails(int caseId)
        {
            try
            {
                CaseServices caseServices = new CaseServices();
                var results = caseServices.getCaseNotesDetails(caseId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: GetCaseNotesDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get preference detais for case
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        [Route("preferencedetails/{caseId}")]
        [ResponseType(typeof(IList<CasePreferenceDetails>))]
        public IHttpActionResult GetCasePreferenceDetails(int caseId)
        {
            try
            {
                CaseServices caseServices = new CaseServices();
                var results = caseServices.getCasePreferenceDetails(caseId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: GetCasePreferenceDetails : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /*Create API for Case*/
        [HttpPost]
        [Route("createcase")]
        [ApiExplorerSettings(IgnoreApi = true)]
        //public IHttpActionResult PostCreateCase(JObject jsonObject)
        public IHttpActionResult PostCreateCase([FromBody] ARC.Donor.Business.Case.CompositeCreateCaseInput compositeCreateCaseInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Case", "Create", compositeCreateCaseInput.CreateCaseInput);
                if (boolMandatoryCheck)
                {
                    var CreateCaseInput = compositeCreateCaseInput.CreateCaseInput;
                    var SaveCaseSearchInput = compositeCreateCaseInput.SaveCaseSearchInput;
                    ARC.Donor.Service.Case.CaseServices cs = new ARC.Donor.Service.Case.CaseServices();
                    var searchResults = cs.createCase(CreateCaseInput, SaveCaseSearchInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: PostCreateCase : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /*Update API for Case*/
        [HttpPost]
        [Route("editcase")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUpdateCase([FromBody] ARC.Donor.Business.Case.CreateCaseInput CreateCaseInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Case", "Edit", CreateCaseInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Case.CaseServices cs = new ARC.Donor.Service.Case.CaseServices();
                    var searchResults = cs.updateCase(CreateCaseInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: PostUpdateCase : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /*Delete API for Case*/
        [HttpPost]
        [Route("deletecase")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDeleteCase([FromBody] ARC.Donor.Business.Case.DeleteCaseInput DeleteCaseInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Case", "Delete", DeleteCaseInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Case.CaseServices cs = new ARC.Donor.Service.Case.CaseServices();
                    var searchResults = cs.deleteCase(DeleteCaseInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: PostDeleteCase : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /*Add,Update and Delete API for Notes*/
        [HttpPost]
        [Route("addcasenotes")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostAddCaseNotes([FromBody] ARC.Donor.Business.Case.CaseNotesInput CaseNotesInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Notes", "Add", CaseNotesInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Case.CaseServices cs = new ARC.Donor.Service.Case.CaseServices();
                    var searchResults = cs.addCaseNotes(CaseNotesInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: PostAddCaseNotes : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("editcasenotes")]
        public IHttpActionResult PostUpdateCaseNotes([FromBody] ARC.Donor.Business.Case.CaseNotesInput CaseNotesInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Notes", "Edit", CaseNotesInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Case.CaseServices cs = new ARC.Donor.Service.Case.CaseServices();
                    var searchResults = cs.updateCaseNotes(CaseNotesInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: PostUpdateCaseNotes : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("deletecasenotes")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDeleteCaseNotes([FromBody] ARC.Donor.Business.Case.CaseNotesInput CaseNotesInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Notes", "Delete", CaseNotesInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Case.CaseServices cs = new ARC.Donor.Service.Case.CaseServices();
                    var searchResults = cs.deleteCaseNotes(CaseNotesInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: PostDeleteCaseNotes : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /*Add,Update and Delete API for Locator*/
        [HttpPost]
        [Route("addcaselocator")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostAddCaseLocator([FromBody] ARC.Donor.Business.Case.CaseLocatorInput CaseLocatorInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Locator", "Add", CaseLocatorInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Case.CaseServices cs = new ARC.Donor.Service.Case.CaseServices();
                    var searchResults = cs.addCaseLocator(CaseLocatorInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: PostAddCaseLocator : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("editcaselocator")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUpdateCaseLocator([FromBody] ARC.Donor.Business.Case.CaseLocatorInput CaseLocatorInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Locator", "Edit", CaseLocatorInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Case.CaseServices cs = new ARC.Donor.Service.Case.CaseServices();
                    var searchResults = cs.updateCaseLocator(CaseLocatorInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: PostUpdateCaseLocator : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("deletecaselocator")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDeleteCaseLocator([FromBody] ARC.Donor.Business.Case.CaseLocatorInput CaseLocatorInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Locator", "Delete", CaseLocatorInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Case.CaseServices cs = new ARC.Donor.Service.Case.CaseServices();
                    var searchResults = cs.deleteCaseLocator(CaseLocatorInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR CaseController :: PostDeleteCaseLocator : " + ex.Message;
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
                {"Notes", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"case_id","notes_text"}},
                    {"Edit", new List<string>() {"case_id","notes_id","notes_text"}},
                    {"Delete", new List<string>() {"case_id", "notes_id"}}}
                },
                {"Locator", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"case_key","locator_upd"}},
                    {"Edit", new List<string>() {"case_key","locator_id","locator_upd"}},
                    {"Delete", new List<string>() {"case_key", "locator_id"}}}
                },

                {"Case", new Dictionary<string, List<string>>() {
                    {"Create", new List<string>() {"case_nm","typ_key_desc","intake_owner_dept_desc"}},
                    {"Edit", new List<string>() {"case_seq"}},
                    {"Delete", new List<string>() {"case_nm"}}}
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
                else if(string.IsNullOrEmpty(InputObj.GetType().GetProperty(columnName).GetValue(InputObj).ToString()))
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