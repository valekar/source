using ARC.Donor.Business;
using ARC.Donor.Business.Orgler.EnterpriseOrgs;
using ARC.Donor.Service;
using ARC.Donor.Service.Orgler.EnterpriseOrgs;
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
   // [Authorize]
    [RoutePrefix("api/enterpriseorgs")]
    public class EnterpriseOrgController : ApiController
    

    {
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        /// <summary>
        /// This is used to get the search result for Enterprise Organizatios
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("enterpriseorgsearch")]
        [ResponseType(typeof(IList<EnterpriseOrgOutputSearchResults>))]       
        public IHttpActionResult GetEnterpriseOrgSearchResults(ListEnterpriseOrgInputSearchModel postData)
        {
            try
            {
                log.Info("Search API");
                Search searchServices = new Search();

                var results = searchServices.getEnterpriseOrgSearchResults(postData);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Search API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method is used to find the affiliations of an enterprise Orgs
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getaffiliatedmasterbridgeresults")]
        [ResponseType(typeof(AffiliatedMasterBridgeOutput))]
        public IHttpActionResult GetAffiliatedMasterBridgeResults(AffiliatedMasterBridgeInput input)
        {
            try
            {
                log.Info("Affiliated Master Bridge API - " + input.ent_org_id);
                log.Info("Started at - " + DateTime.Now);
                Affiliations affiliationServices = new Affiliations();
                var results = affiliationServices.getAffiliatedMasterBridgeDetails(input);
                log.Info("Ended at - " + DateTime.Now + " consisting of '" + results.lt_affil_res.Count + "' affiliations");
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Master Bridge API - " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// This method is used to find the affiliations of an enterprise and Export in Excel
        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("getaffiliatedmasterbridgeexportresults/{enterpriseOrgId}")]
        [ResponseType(typeof(IList<AffiliatedMasterBridgeExportOutputModel>))]
        public IHttpActionResult GetAffiliatedMasterBridgeExportResults(string enterpriseOrgId)
        {
            try
            {
                log.Info("Affiliated Master Bridge - Export - API - " + enterpriseOrgId);
                log.Info("Started at - " + DateTime.Now);
                Affiliations affiliationServices = new Affiliations();
                var results = affiliationServices.getAffiliatedMasterBridgeExportDetails(10, 1, enterpriseOrgId);
                log.Info("Ended at - " + DateTime.Now + " consisting of '" + results.Count + "' affiliations");
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Master Bridge - Export - API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method is used to find the affiliations of an enterprise
        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("getaffiliationsresults/{enterpriseOrgId}")]
        [ResponseType(typeof(IList<AffiliationsOutputModel>))]
        public IHttpActionResult GetAffiliationsResults(string enterpriseOrgId)
        {
            try
            {
                Affiliations affiliationServices = new Affiliations();
                var results = affiliationServices.getAffiliationDetails(10, 1, enterpriseOrgId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Affiliations Results -API " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// This method finds the bridge of an enterprise
        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("getbridgedetails/{enterpriseOrgId}")]
        [ResponseType(typeof(IList<BridgeOutputModel>))]
        public IHttpActionResult GetBridgeDetails(string enterpriseOrgId)
        {
            try
            {
                Affiliations affiliationServices = new Affiliations();
                var results = affiliationServices.getBridgeDetails(10, 1, enterpriseOrgId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Bridge Details -API " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method get the basic details of particular enterprise
        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("getbasicenterpriseorgdetails/{enterpriseOrgId}")]
        [ResponseType(typeof(IList<GetEnterpriseOrgModel>))]
        public IHttpActionResult GetBasicEnterpriseDetails(string enterpriseOrgId)
        {
            try
            {
                Crud crudServices = new Crud();
                var results = crudServices.getEntOrg(10, 1, enterpriseOrgId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Basic Enterprise Details -API " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// This method get the hierarchy details of particular enterprise
        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("gethierarchydetails/{enterpriseOrgId}")]
        [ResponseType(typeof(IList<OrganizationHierarchyOutputModel>))]
        public IHttpActionResult GetHierarchyDetails(string enterpriseOrgId)
        {
            try
            {
                Hierarchy hierarchyServices = new Hierarchy();
                var results = hierarchyServices.getHierarchyResults(enterpriseOrgId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Hierarchy Details -API " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// This method gets the naics related details for a particular master
        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("getnaicsdetails/{enterpriseOrgId}")]
        [ResponseType(typeof(IList<GetNAICSDetailsOutput>))]
        public IHttpActionResult GetNAICSDetails(string enterpriseOrgId)
        {
            try
            {
                NAICS naicsServices = new NAICS();
                var results = naicsServices.getNAICSDetails(10, 1, enterpriseOrgId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("NAICS Details -API " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method gets the naics related details for a particular master
        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("getsummarydetails/{enterpriseOrgId}")]
        [ResponseType(typeof(IList<SummaryOutputModel>))]
        public IHttpActionResult GetSummaryDetails(string enterpriseOrgId)
        {
            try
            {
                Summary naicsServices = new Summary();
                var results = naicsServices.getSummaryDetails(10, 1, enterpriseOrgId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Summary Details -API " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method gets the naics related details for a particular master
        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("getrankingdetails/{enterpriseOrgId}")]
        [ResponseType(typeof(IList<RankingOutputModel>))]
        public IHttpActionResult GetRankingDetails(string enterpriseOrgId)
        {
            try
            {
                Ranking rankingServices = new Ranking();
                var results = rankingServices.getRankingDetails(10, 1, enterpriseOrgId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Ranking Details -API " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method gets the naics related details for a particular master
        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("gettransactionhistorydetails/{enterpriseOrgId}")]
        [ResponseType(typeof(IList<TransactionHistoryOutputModel>))]
        public IHttpActionResult GetTransactionHistoryDetails(string enterpriseOrgId)
        {
            try
            {
                TransactionHistory transactionHistoryServices = new TransactionHistory();
                var results = transactionHistoryServices.getTransactionHistoryDetails(10, 1, enterpriseOrgId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Transaction History Details -API " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method get the tags details of particular enterprise
        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("gettagsdetails/{enterpriseOrgId}")]
        [ResponseType(typeof(IList<TagOutputModel>))]
        public IHttpActionResult GetTagsDetails(string enterpriseOrgId)
        {
            try
            {
                Tags tagsServices = new Tags();
                var results = tagsServices.getTagsDetails(10, 1, enterpriseOrgId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Tags Details Details -API " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// This method creates new enterprise Organizations
        /// </summary>
        /// <param name="CreateEntOrgInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createenterpriseorg")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostCreateEnterpriseOrg([FromBody] ARC.Donor.Business.Orgler.EnterpriseOrgs.CreateEnterpriseOrgInputModel CreateEntOrgInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Crud", "Add", CreateEntOrgInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Orgler.EnterpriseOrgs.Crud c = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Crud();
                    var searchResults = c.createEntOrg(CreateEntOrgInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                log.Info("Create Enterprise Orgs -API " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// This method Edit existing enterprise Organizations
        /// </summary>
        /// <param name="EditEntOrgInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editenterpriseorg")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostUpdateEnterpriseOrg([FromBody] ARC.Donor.Business.Orgler.EnterpriseOrgs.EditEnterpriseOrgInputModel EditEntOrgInput)
        {
            try
            {
               //; Boolean boolMandatoryCheck = checkMandatoryInputs("Crud", "Edit", EditEntOrgInput);
                //if (boolMandatoryCheck)
                //{
                    ARC.Donor.Service.Orgler.EnterpriseOrgs.Crud c = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Crud();
                    var searchResults = c.updateEntOrg(EditEntOrgInput);
                    return Ok(searchResults);
                //}
                //else
                //{
                //    return Ok("Please provide the necessary inputs");
                //}
            }
            catch (Exception ex)
            {
                log.Info("Edit Enterprise Orgs -API " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method Delete existing enterprise Organizations
        /// </summary>
        /// <param name="DeleteEntOrgInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteenterpriseorg")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostDeleteEnterpriseOrg([FromBody] ARC.Donor.Business.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgInputModel DeleteEntOrgInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Crud", "Delete", DeleteEntOrgInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Orgler.EnterpriseOrgs.Crud c = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Crud();
                    var searchResults = c.deleteEntOrg(DeleteEntOrgInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                log.Info("Delete Enterprise Orgs -API " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// This method get the transformation details of particular enterprise        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("gettransformationdetails/{enterpriseOrgId}")]
        [ResponseType(typeof(TransformationModel))]
        public IHttpActionResult GetTransformationDetails(string enterpriseOrgId)
        {
            try
            {
                 Transformations transformationServices = new Transformations();
                var results = transformationServices.getTransformationDetails(10, 1, enterpriseOrgId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Transformation Details -API " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method gets the rfm related details for a particular enterprise
        /// </summary>
        /// <param name="enterpriseOrgId"></param>
        /// <returns></returns>
        [Route("getrfmdetails/{enterpriseOrgId}")]
        [ResponseType(typeof(RFMDetails))]
        public IHttpActionResult GetRFMDetails(string enterpriseOrgId)
        {
            try
            {
                RFM ser = new RFM();
                var results = ser.getRFMDetails(10, 1, enterpriseOrgId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("RFM Details -API " + ex.Message);
                return Ok("Error");
            }
        }
        /// <summary>
        /// This method get the tags details of particular enterprise
        /// </summary>       
        /// <returns></returns>
        //[Route("gettags")]
        //[ResponseType(typeof(IList<Tag>))]
        //public IHttpActionResult GetTags()
        //{
        //    try
        //    {
        //        Tags tagsServices = new Tags();
        //        var results = tagsServices.getTagsData();
        //        return Ok(results);
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Info("Tags Details Details -API " + ex.Message);
        //        return Ok("Error");
        //    }
        //}

        /* ******************************* API methods corresponding to actions performed under details section ******************************* */

        /// <summary>
        /// Method to get the list of active tags
        /// </summary>
        /// <returns></returns>
        [Route("gettagddlist")]
        [ResponseType(typeof(IList<TagDDList>))]
        public IHttpActionResult GetTagDDList()
        {
            try
            {
                ARC.Donor.Service.Orgler.EnterpriseOrgs.Tags service = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Tags();
                var serviceRes = service.getTagDDList();
                return Ok(serviceRes);
            }
            catch (Exception ex)
            {
                log.Info("Get Tags Dropdown list -API " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Method to perform updates to the tags on an enterprise organization
        /// </summary>
        /// <param name="TagUpdateInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatetags")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult UpdateTags([FromBody] ARC.Donor.Business.Orgler.EnterpriseOrgs.TagUpdateInputModel TagUpdateInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Tags", TagUpdateInput.action_type.ToString(), TagUpdateInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Orgler.EnterpriseOrgs.Tags service = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Tags();
                    var serviceRes = service.updateTags(TagUpdateInput);
                    return Ok(serviceRes);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                log.Info("Update Tags - Orgler API " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Method to perform updates to the transformations on an enterprise organizations
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatetransformations")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult UpdateTransformations([FromBody] TransformationUpdateInput input)
        {
            try
            {
                ARC.Donor.Service.Orgler.EnterpriseOrgs.Transformations service = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Transformations();
                
                //If not a delete operation, validate for the presence of the atleast one rule
                if (input.req_typ.ToLower() != "delete")
                {
                    if (input.ltCondition != null)
                    {
                        if (input.ltCondition.Count > 0)
                        {
                            int intValidCnt = 0;
                            intValidCnt = input.ltCondition.Where(x => !string.IsNullOrEmpty(x.condition_typ) && !string.IsNullOrEmpty(x.pattern_string)).Count();
                            if (intValidCnt > 0)
                            {
                                var serviceRes = service.updateTransformation(input);
                                return Ok(serviceRes);
                            }
                            else
                                return Ok("Please provide the necessary inputs");
                        }
                        else
                            return Ok("Please provide the necessary inputs");
                    }
                    else
                        return Ok("Please provide the necessary inputs");
                }
                else
                {
                    var serviceRes = service.updateTransformation(input);
                    return Ok(serviceRes);
                }
            }
            catch(Exception ex)
            {
                log.Info("Update Transformations - Orgler API " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Method to evaluate the potential affiliations which might get created for the rules configured by the user
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("smoketesttransformations")]
        public IHttpActionResult SmokeTestTransformations([FromBody] TransformationUpdateInput input)
        {
            try
            {
                ARC.Donor.Service.Orgler.EnterpriseOrgs.Transformations service = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Transformations();

                if (input.ltCondition != null)
                {
                    if (input.ltCondition.Count > 0)
                    {
                        int intValidCnt = 0;
                        intValidCnt = input.ltCondition.Where(x => !string.IsNullOrEmpty(x.condition_typ) && !string.IsNullOrEmpty(x.pattern_string)).Count();
                        if (intValidCnt > 0)
                        {
                            var serviceRes = service.smokeTestTransformations(input);
                            return Ok(serviceRes);
                        }
                        else
                            return Ok("Please provide the necessary inputs");
                    }
                    else
                        return Ok("Please provide the necessary inputs");
                }
                else
                    return Ok("Please provide the necessary inputs");
            }
            catch (Exception ex)
            {
                log.Info("Smoke Testing - Transformations - Orgler API " + ex.Message);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Method to get the organization details corresonding to the potential affiliations on the selected enterprise
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("smoketesttransformationsexport")]
        public IHttpActionResult SmokeTestTransformationsExport([FromBody] TransformationUpdateInput input)
        {
            try
            {
                ARC.Donor.Service.Orgler.EnterpriseOrgs.Transformations service = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Transformations();

                if (input.ltCondition != null)
                {
                    if (input.ltCondition.Count > 0)
                    {
                        int intValidCnt = 0;
                        intValidCnt = input.ltCondition.Where(x => !string.IsNullOrEmpty(x.condition_typ) && !string.IsNullOrEmpty(x.pattern_string)).Count();
                        if (intValidCnt > 0)
                        {
                            var serviceRes = service.smokeTestTransformationsExport(input, 2000);
                            return Ok(serviceRes);
                        }
                        else
                            return Ok("Please provide the necessary inputs");
                    }
                    else
                        return Ok("Please provide the necessary inputs");
                }
                else
                    return Ok("Please provide the necessary inputs");
            }
            catch (Exception ex)
            {
                log.Info("Smoke Testing - Transformations - Orgler API " + ex.Message);
                return Ok("Error");
            }
        }


        /// <summary>
        /// Use this to get the characteristics details of a master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetOrgCharacteristics/{id}")]
        [ResponseType(typeof(IList<ARC.Donor.Business.Orgler.EnterpriseOrgs.Characteristics>))]
        public IHttpActionResult GetOrgCharacteristics(string id)
        {
            try
            {
                ARC.Donor.Service.Orgler.EnterpriseOrgs.Characteristics arc = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Characteristics();
                return Ok(arc.getOrgCharacteristics(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR EnterpriseOrgController :: GetOrgCharacteristics : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Use this to get more characteristics details of a master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Route("GetOrgAllCharacteristics/{id}")]
        [ResponseType(typeof(IList<ARC.Donor.Business.Orgler.EnterpriseOrgs.Characteristics>))]
        public IHttpActionResult GetOrgAllCharacteristics(string id)
        {
            try
            {
                ARC.Donor.Service.Orgler.EnterpriseOrgs.Characteristics arc = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Characteristics();
                return Ok(arc.getOrgAllCharacteristics(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR EnterpriseOrgController :: GetOrgCharacteristics : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


        /* Create, Delete and Update API's for Chatacteristics*/
        [HttpPost]
        [Route("AddOrgCharacteristics")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult AddOrgCharacteristics([FromBody] ARC.Donor.Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput OrgCharInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Characteristics", "Add", OrgCharInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Orgler.EnterpriseOrgs.Characteristics ch = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Characteristics();
                    var searchResults = ch.addOrgCharacteristics(OrgCharInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR EnterpriseOrgController :: AddOrgCharacteristics : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("EditOrgCharacteristics")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult EditOrgCharacteristics([FromBody] ARC.Donor.Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput OrgCharInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Characteristics", "Edit", OrgCharInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Orgler.EnterpriseOrgs.Characteristics ch = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Characteristics();
                    var searchResults = ch.editOrgCharacteristics(OrgCharInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR EnterpriseOrgController :: EditOrgCharacteristics : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("DeleteOrgCharacteristics")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult DeleteOrgCharacteristics([FromBody] ARC.Donor.Business.Orgler.EnterpriseOrgs.OrgCharacteristicsInput OrgCharInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Characteristics", "Delete", OrgCharInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Orgler.EnterpriseOrgs.Characteristics ch = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Characteristics();
                    var searchResults = ch.deleteOrgCharacteristics(OrgCharInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR EnterpriseOrgController :: DeleteOrgCharacteristics : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Method to perform updates to the hierarchy of an enterprise organizations
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatehierarchy")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult UpdateHierarchy([FromBody] HierarchyUpdateInput input)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("Hierarchy", input.action_type.ToString(), input);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Orgler.EnterpriseOrgs.Hierarchy service = new ARC.Donor.Service.Orgler.EnterpriseOrgs.Hierarchy();

                    var serviceRes = service.updateHierarchy(input);
                    return Ok(serviceRes);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                log.Info("Update Hierarchy - Orgler API " + ex.Message);
                return Ok("Error");
            }
        }

        private Boolean checkMandatoryInputs(string strRequestType, string strActionType, object InputObj)
        {
            Boolean boolMandatoryCheck = true;
            //Dictionary to hold the list of columns which are mandatory while 
            Dictionary<string, Dictionary<string, List<string>>> dictMandatoryLibrary = new Dictionary<string, Dictionary<string, List<string>>>()
            {
               {"Crud", new Dictionary<string, List<string>>() {
                    {"Create", new List<string>() { "user_id", "ent_org_name", "ent_org_src_cd", "nk_ent_org_id"}},
                    {"Edit", new List<string>() {"ent_org_id", "ent_org_name","ent_org_src_cd","nk_ent_org_id","user_id"}},
                    {"Delete", new List<string>() {"ent_org_id","user_id"}}}
               },
               {"Tags", new Dictionary<string, List<string>>() {
                    {"Insert", new List<string>() {"ent_org_id", "tag"}},
                    {"Delete", new List<string>() {"ent_org_id", "tag"}}}
               },
               {"Hierarchy", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"superior_ent_org_key", "subodinate_ent_org_key", "rlshp_desc", "rlshp_cd"}},
                    {"Delete", new List<string>() {"superior_ent_org_key", "subodinate_ent_org_key"}}}
               },
                {"Characteristics", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"EntOrgID", "CharacteristicValue", "CharacteristicTypeCode"}},
                    {"Edit", new List<string>() {"EntOrgID", "OldCharacteristicValue",  "OldCharacteristicTypeCode", "CharacteristicValue",  "CharacteristicTypeCode"}},
                    {"Delete", new List<string>() {"EntOrgID", "OldCharacteristicValue", "OldCharacteristicTypeCode"}}}
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