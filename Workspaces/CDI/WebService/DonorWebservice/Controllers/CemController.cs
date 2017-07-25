//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace DonorWebservice.Controllers
//{
//    [Authorize]
//    [RoutePrefix("api/Preference")]
//    public class PreferenceController : ApiController
//    {

//        [Route("GetPreferenceLocator/{id}")]
//        public IHttpActionResult getPreferenceLocator(string id)
//        {
//            try
//            {
//                ARC.Donor.Service.Constituents.PreferenceLocator arc = new ARC.Donor.Service.Constituents.PreferenceLocator();
//                return Ok(arc.getPreferenceLocators(10, 1, id));
//            }
//            catch (Exception ex)
//            {
//                return Ok("Error");
//            }
//        }


//        [Route("GetPreferenceLocatorOptions/{id}")]
//        public IHttpActionResult getPreferenceLocatorOptions(string id)
//        {
//            try
//            {
//                ARC.Donor.Service.Constituents.PreferenceLocator arc = new ARC.Donor.Service.Constituents.PreferenceLocator();
//                return Ok(arc.getPreferenceLocatorsOptions(10, 1, id));
//            }
//            catch (Exception ex)
//            {
//                return Ok("Error");
//            }
//        }

//        [HttpPost]
//        [Route("AddPreferenceLocator")]
//        public IHttpActionResult addPreferenceLocator([FromBody] ARC.Donor.Business.Constituents.PreferenceLocatorInput prefLocInput)
//        {
//            try
//            {
//                Boolean boolMandatoryCheck = checkMandatoryInputs("PreferenceLocator", "Add", prefLocInput);
//                if (boolMandatoryCheck)
//                {
//                    ARC.Donor.Service.Constituents.PreferenceLocator p = new ARC.Donor.Service.Constituents.PreferenceLocator();
//                    var searchResults = p.addPreferenceLocator(prefLocInput);
//                    return Ok(searchResults);
//                }
//                else
//                {
//                    return Ok("Please provide the necessary inputs");
//                }
//            }
//            catch (Exception ex)
//            {
//                return Ok("Error");
//            }
//        }


//        [HttpPost]
//        [Route("EditPreferenceLocator")]
//        public IHttpActionResult editPreferenceLocator([FromBody] ARC.Donor.Business.Constituents.PreferenceLocatorInput prefLocInput)
//        {
//            try
//            {
//                Boolean boolMandatoryCheck = checkMandatoryInputs("PreferenceLocator", "Edit", prefLocInput);

//                if (prefLocInput.newPrefLocType.ToLower().Equals("All".ToLower()))
//                {
//                    if (prefLocInput.newPrefLocId == 0 || prefLocInput.newPrefLocId == null)
//                    {
//                        boolMandatoryCheck = true;
//                    }
//                }

//                if (boolMandatoryCheck)
//                {
//                    ARC.Donor.Service.Constituents.PreferenceLocator p = new ARC.Donor.Service.Constituents.PreferenceLocator();
//                    var searchResults = p.editPreferenceLocator(prefLocInput);
//                    return Ok(searchResults);
//                }
                
//                else
//                {
//                    return Ok("Please provide the necessary inputs");
//                }
//            }
//            catch (Exception ex)
//            {
//                return Ok("Error");
//            }
//        }

//        [HttpPost]
//        [Route("DeletePreferenceLocator")]
//        public IHttpActionResult deletePreferenceLocator([FromBody] ARC.Donor.Business.Constituents.PreferenceLocatorInput prefLocInput)
//        {
//            try
//            {
//                Boolean boolMandatoryCheck = checkMandatoryInputs("PreferenceLocator", "Delete", prefLocInput);
//                if (boolMandatoryCheck)
//                {
//                    ARC.Donor.Service.Constituents.PreferenceLocator p = new ARC.Donor.Service.Constituents.PreferenceLocator();
//                    var searchResults = p.deletePreferenceLocator(prefLocInput);
//                    return Ok(searchResults);
//                }
//                else
//                {
//                    return Ok("Please provide the necessary inputs");
//                }
//            }
//            catch (Exception ex)
//            {
//                return Ok("Error");
//            }
//        }

//         private Boolean checkMandatoryInputs(string strRequestType, string strActionType, object InputObj)
//        {
//            Boolean boolMandatoryCheck = true;
//            //Dictionary to hold the list of columns which are mandatory while 
//            Dictionary<string, Dictionary<string, List<string>>> dictMandatoryLibrary = new Dictionary<string, Dictionary<string, List<string>>>()
//            {
//                {"PreferenceLocator", new Dictionary<string, List<string>>() {
//                    {"Add", new List<string>() {"masterId", "constituentType", "lineOfService","newSourceSystemCode", "newPrefLocType", "newPrefLocId"}},
//                    {"Edit", new List<string>() {"masterId", "constituentType", "lineOfService","oldSourceSystemCode", "oldPrefLocType", "oldPrefLocId", "newSourceSystemCode", "newPrefLocType","newPrefLocId"}},
//                    {"Delete", new List<string>() {"masterId", "constituentType","lineOfService","oldSourceSystemCode", "oldPrefLocType","oldPrefLocId"}}}
//                }
//            };
//              //Fetch the mandatory fields for the inputted details type and action types
//            List<string> listMandatoryColumns = new List<string>();
//            foreach (KeyValuePair<string, Dictionary<string, List<string>>> keyValuePair in dictMandatoryLibrary)
//            {
//                if(keyValuePair.Key == strRequestType)
//                {
//                    foreach(KeyValuePair<string, List<string>> innerkeyValuePair in keyValuePair.Value)
//                    {
//                        if (innerkeyValuePair.Key == strActionType)
//                        {
//                            listMandatoryColumns = innerkeyValuePair.Value;
//                        }
//                    }
//                }
//            }

//            //Check if the mandatory columns are present in the inputted object
//            foreach (string columnName in listMandatoryColumns)
//            {
//                //Check if the mandatory columns are null or empty
//                if (InputObj.GetType().GetProperty(columnName).GetValue(InputObj) == null)
//                {
//                    boolMandatoryCheck = false;
//                }
//                else if (string.IsNullOrEmpty(InputObj.GetType().GetProperty(columnName).GetValue(InputObj).ToString()))
//                {
//                    boolMandatoryCheck = false;
//                }
//                //Check if valid numbers are provided to the not nullable fields
//                else if (InputObj.GetType().GetProperty(columnName).GetValue(InputObj).ToString() == "0")
//                {
//                    boolMandatoryCheck = false;
//                }
//                /*else
//                {
//                    if (columnName.Equals("newPrefLocType"))
//                    {

//                        ARC.Donor.Business.Constituents.PreferenceLocatorInput pref = (ARC.Donor.Business.Constituents.PreferenceLocatorInput)InputObj;
//                        if (pref.newPrefLocType.ToLower().Equals("All".ToLower()))
//                        {
//                            if (string.IsNullOrEmpty(pref.newPrefLocId.ToString()))
//                            {
//                                boolMandatoryCheck = true;
//                            }
//                        }
                            
                        
//                    }
//                }*/
//            }

//            return boolMandatoryCheck;
//         }

//    }
//}
