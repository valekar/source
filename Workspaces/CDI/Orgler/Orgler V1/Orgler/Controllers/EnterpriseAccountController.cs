using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

using Orgler.Models.EnterpriseAccount;
using Orgler.Models.Common;
using Orgler.ViewModels;
using Orgler.Exceptions;
using Orgler.Services;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Security.Principal;
using Orgler.Models.Entities;
using Orgler.Security;
using OfficeOpenXml.Table.PivotTable;
using System.Drawing;
using Orgler.Models.NewAccount;
using Orgler.Models;

namespace Orgler.Controllers
{
    [Authorize]
    public class EnterpriseAccountServiceController : BaseController
    {
        JavaScriptSerializer serializer;

        //Search Method
      
        [HttpPost]
        /* Method name: Search
        * Input Parameters: An object of SearchInput class
        * Purpose: This method search results for new account */
        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        public async Task<ActionResult> Search(ListEnterpriseOrgInputSearchModel inputSearch)
        {
            EnterpriseOrgViewModel viewModel = new EnterpriseOrgViewModel();           
            string res = string.Empty;
            inputSearch.AnswerSetLimit = "10000";
            //List<RankInput> listRankInput = new List<RankInput>();
            RankInput rankInput = new RankInput();
            foreach(EnterpriseOrgInputSearchModel input in inputSearch.EnterpriseOrgInputSearchModel)
            {
               
                List<RankInput> listRankInput = new List<RankInput>();
                if (input.RankProviderInput!=null)
                {
                    foreach (string s in input.RankProviderInput)
                    {
                        rankInput = new RankInput();
                        string[] rankInfo = s.Split('-');
                        string strRankProvider = rankInfo[0].Trim();
                        string strRankYear = rankInfo[1].Trim();
                        rankInput.Provider = strRankProvider;
                        rankInput.Year = strRankYear;
                        listRankInput.Add(rankInput);
                    }
                    input.RankProvider = listRankInput;
                }
            }
           if(inputSearch.EnterpriseOrgInputSearchModel[0].RecentChanges==false)
            {
                inputSearch.EnterpriseOrgInputSearchModel[0].Username = null;
            }
            res = (new JavaScriptSerializer()).Serialize(inputSearch);

            //Only if search input is valid
            //if (searchInput)
            //{
            string url = BaseURL + "api/enterpriseorgs/enterpriseorgsearch/";
            res = await InvokeWebService.PostResourceAsync(url, Token, inputSearch, ClientID);

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                ser.MaxJsonLength = 2147483647;
                ser.Serialize(res);
                viewModel.objSearchResults = ser.Deserialize<List<EnterpriseOrgOutputSearchResults>>(res);
                // var objSearchResults = formTreeDataStructureEnterpriseOrgsSearchResult(viewModel.objSearchResults);
                // viewModel.objSearchResults = (new JavaScriptSerializer()).Deserialize<List<EnterpriseOrgOutputSearchResults>>(restree.ToString());
                res = ser.Serialize(viewModel);
            }
            if (viewModel.objSearchResults != null && viewModel.objSearchResults.Count > 0)
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                ser.MaxJsonLength = 2147483647;
                //ser.Serialize(res);
                //viewModel.objSearchResults = ser.Deserialize<List<EnterpriseOrgOutputSearchResults>>(res);
                for (int j = 0; j < viewModel.objSearchResults.Count; j++)
                {
                    List<NAICSCodesNDesc> naicsdetails = new List<NAICSCodesNDesc>();
                    for (int i = 0; i < viewModel.objSearchResults[j].listNAICSDesc.Count; i++)
                    {

                        NAICSCodesNDesc naicsCodeDesc = new NAICSCodesNDesc();
                        naicsCodeDesc.naicsCode = viewModel.objSearchResults[j].listNAICSCodes[i].strText;
                        naicsCodeDesc.naicsTitle = viewModel.objSearchResults[j].listNAICSDesc[i].strText;
                        naicsCodeDesc.status = viewModel.objSearchResults[j].listNAICSDesc[i].status;

                        naicsdetails.Add(naicsCodeDesc);
                        //  viewModel.searchResults.listNAICSCodesAndDesc.Add(naicsCodeDesc);
                    }
                    EnterpriseOrgOutputSearchResults search = new EnterpriseOrgOutputSearchResults();
                    search.listNAICSCodesAndDesc = naicsdetails;
                    viewModel.objSearchResults[j].listNAICSCodesAndDesc = search.listNAICSCodesAndDesc;
                }



                res = ser.Serialize(viewModel);

            }
            //}

            return handleTrivialHttpRequests(res);
        }

        /* Method name: Search
       * Input Parameters: An object of SearchInput class
       * Purpose: This method insert latest search results for EnterpriseOrg */
        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        [HttpPost]
        public string InsertEnterpriseOrgRecentSearch(ListEnterpriseOrgInputSearchModel searchInput)
        {
            string strMessage = "Error, something happened while logging the Recent Searches. Please contact administrator.";
            //Check if search input is not empty
            if (!Utilities.IsAnyPropertyNotNull(searchInput))
                return "Enterprise Orgs Search Log : Nothing to log";

            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
           // searchInput.EnterpriseOrgInputSearchModel[0].Username= strUserName;           
            List<ListEnterpriseOrgInputSearchModel> ltRecentSearch = new List<ListEnterpriseOrgInputSearchModel>();
            try
            {
                string strRecentSearchPath = ConfigurationManager.AppSettings["EnterpriseOrgsRecentSearchFilePath"];
                var RecentSearch = Utilities.readJSONTFromFile<List<ListEnterpriseOrgInputSearchModel>>(strRecentSearchPath);
                var filteredRecentSearch = RecentSearch; // This is to filter out the recent searches based on the username

                if (RecentSearch != null)
                {
                    if (!string.IsNullOrEmpty(strUserName))
                    {
                        filteredRecentSearch = RecentSearch.Where(x => x.EnterpriseOrgInputSearchModel[0].Username == strUserName).ToList();
                        RecentSearch.RemoveAll(x => x.EnterpriseOrgInputSearchModel[0].Username == strUserName);
                    } 
                    if (filteredRecentSearch.Count >= 10)
                    {
                        filteredRecentSearch.RemoveRange(9, filteredRecentSearch.Count - 9); // Remove all items greater than 9, since we show only top 10 recent searches
                    }
                    searchInput.EnterpriseOrgInputSearchModel[0].Username = strUserName;
                    filteredRecentSearch.Insert(0, searchInput);
                    RecentSearch.AddRange(filteredRecentSearch);
                    var SuccessMessage = Utilities.writeJSONIntoFile<List<ListEnterpriseOrgInputSearchModel>>(RecentSearch, strRecentSearchPath);
                    if (SuccessMessage.ToLower() == "success")
                        strMessage = "Enterprise Orgs Recent searches are logged successfully";
                    else
                        return strMessage;
                }
                else
                {
                    ltRecentSearch.Add(searchInput);
                    var SuccessMessage = Utilities.writeJSONIntoFile<List<ListEnterpriseOrgInputSearchModel>>(ltRecentSearch, strRecentSearchPath);
                    if (SuccessMessage.ToLower() == "success")
                        strMessage = "Enterprise Orgs Recent searches are logged successfully";
                    else
                        return strMessage;
                }
            }
            catch (Exception ex)
            {
                strMessage = "Erorr, " + ex.Message;
            }
            return strMessage;
        }
        /* Method name: GetEnterpriseOrgsRecentSearches
        * Input Parameters: Null
        * Purpose: This method gets the EnterpriseOrgs Recent Searches*/
        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        [HttpGet]
        public JsonResult GetEnterpriseOrgsRecentSearches()
        {            
            string strRecentSearchPath = ConfigurationManager.AppSettings["EnterpriseOrgsRecentSearchFilePath"];
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
           // string strUserName = "dixit.jain";

            var JSON = Utilities.readJSONTFromFile<List<ListEnterpriseOrgInputSearchModel>>(strRecentSearchPath);
            var filteredJSON = JSON;
            if (JSON != null)
            {
                filteredJSON = JSON.Where(x => x.EnterpriseOrgInputSearchModel[0].Username == strUserName).ToList();             
                
            }
            return Json(filteredJSON, JsonRequestBehavior.AllowGet);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<ActionResult> Create(CreateEnterpriseOrgInputModel createInput)
        {

            IPrincipal p = System.Web.HttpContext.Current.User;
            createInput.user_id = Orgler.Security.Extentions.GetUserName(p);
            string url = BaseURL + "api/enterpriseorgs/createenterpriseorg/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, createInput, ClientID);
            return handleTrivialHttpRequests(res);
        }

        /* Method name: Edit Enterprise Account
         * Input Parameters: OrgID
         * Purpose: This method Creates Enterprise Account*/
        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<ActionResult> Edit(string enterpriseOrgId)
        {
            //addInput.usr_nm = "Keerthana";
            IPrincipal p = System.Web.HttpContext.Current.User;
            // createInput.user_id = Orgler.Security.Extentions.GetUserName(p);
            string url = BaseURL + "api/enterpriseorgs/getsummarydetails/" + enterpriseOrgId;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);
        }
        /* Method name: Edit Enterprise Account
        * Input Parameters: OrgID
        * Purpose: This method Creates Enterprise Account*/
        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<ActionResult> EditEnterpriseOrg(EditEnterpriseOrgInputModel enterpriseEditInput)
        {
            //addInput.usr_nm = "Keerthana";
            IPrincipal p = System.Web.HttpContext.Current.User;
            enterpriseEditInput.user_id = Orgler.Security.Extentions.GetUserName(p);
            string url = BaseURL + "api/enterpriseorgs/editenterpriseorg/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, enterpriseEditInput, ClientID);
            return handleTrivialHttpRequests(res);
        }
        /* Method name: Delete Enterprise Orgs
        * Input Parameters: OrgID
        * Purpose: This method Deletes Enterprise Orgs*/
        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<ActionResult> DeleteEnterpriseOrg(DeleteEnterpriseOrgInputModel enterpriseEditInput)
        {
            //addInput.usr_nm = "Keerthana";
            IPrincipal p = System.Web.HttpContext.Current.User;
            // createInput.user_id = Orgler.Security.Extentions.GetUserName(p);
            enterpriseEditInput.user_id = p.Identity.Name;
            string url = BaseURL + "api/enterpriseorgs/deleteenterpriseorg/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, enterpriseEditInput, ClientID);
            return handleTrivialHttpRequests(res);
        }
        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        public JsonResult GetNAICSCodesLevel2and3()
        {
            string strPath = string.Empty;
            strPath = ConfigurationManager.AppSettings["NAICSCodeLevel2and3MetadataFilePath"];

            var JSON = Utilities.readJSONTFromFile<List<NAICSCode>>(strPath);
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

        /* Keerthana - 12-April-2017 Method to get the menu preferences for details stored in a file */
        [HttpGet]
        public JsonResult GetMenuJson()
        {
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
            var readJSON = Utility.readJSONTFromFile<IList<MenuPreferences>>(EntOrgMenuPrefFilePath);
            var userPrefJSON = readJSON.Where(x => x.UserName == strUserName).Select(x => x).FirstOrDefault();
            //return null;
            return Json(userPrefJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string SaveMenuJSON(MenuPreferences MenuPref)
        {
            string outputMessage = string.Empty;
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
            MenuPref.UserName = strUserName;
            try
            {
                var readJSON = Utility.readJSONTFromFile<IList<MenuPreferences>>(EntOrgMenuPrefFilePath);
                if (readJSON != null)
                {
                    var userMenuPref = readJSON.Where(x => x.UserName == strUserName).Select(s => s).FirstOrDefault();
                    if (userMenuPref != null)
                    {
                        readJSON.Remove(userMenuPref);
                    }
                }
                readJSON.Add(MenuPref);
                if (readJSON != null)
                    outputMessage = Utility.writeJSONToFile(readJSON, EntOrgMenuPrefFilePath);

                return outputMessage;
            }
            catch (Exception ex)
            {
                outputMessage = "Failed: " + ex;
                return outputMessage;
            }
        }

        /* ***************************** Details Individual Sections ***************************** */
        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        public async Task<JsonResult> GetEntOrgDetailDetails(string ent_org_id)
        {
            string url = BaseURL + "api/enterpriseorgs/getsummarydetails/" + ent_org_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            string strRes = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                List<SummaryOutputModel> JObj = JsonConvert.DeserializeObject<List<SummaryOutputModel>>(res);
                strRes = (new JavaScriptSerializer()).Serialize(JObj);
            }
            else
            {
                strRes = res;
            }
            return handleTrivialHttpRequests(strRes);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        public async Task<JsonResult> GetTransformationDetailDetails(string ent_org_id)
        {
            string url = BaseURL + "api/enterpriseorgs/gettransformationdetails/" + ent_org_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            string strRes = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                TransformationModel JObjRes = new TransformationModel();
                JObjRes.output = new List<TransformationOutputModel>();
                JObjRes.manual_affil_cnt = "0";
                TransformationModel JObj = JsonConvert.DeserializeObject<TransformationModel>(res);
                strRes = (new JavaScriptSerializer()).Serialize(JObj);
            }
            else
            {
                strRes = res;
            }
            return handleTrivialHttpRequests(strRes);
        }
        
        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        public async Task<JsonResult> GetTagsDetails(string ent_org_id)
        {
            string url = BaseURL + "api/enterpriseorgs/gettagsdetails/" + ent_org_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            string strRes = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                List<TagOutputModel> JObj = JsonConvert.DeserializeObject<List<TagOutputModel>>(res);
                strRes = (new JavaScriptSerializer()).Serialize(JObj);
            }
            else
            {
                strRes = res;
            }
            return handleTrivialHttpRequests(strRes);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        public async Task<JsonResult> GetEntOrgHierarchyDetails(string ent_org_id)
        {
            string url = BaseURL + "api/enterpriseorgs/gethierarchydetails/" + ent_org_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            string strRes = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                List<OrganizationHierarchyOutputModel> JObj = JsonConvert.DeserializeObject<List<OrganizationHierarchyOutputModel>>(res);
                strRes = (new JavaScriptSerializer()).Serialize(JObj);
            }
            else
            {
                strRes = res;
            }
            return handleTrivialHttpRequests(strRes);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        public async Task<JsonResult> GetMasterBridgeDetails(AffiliatedMasterBridgeInput input)
        {
            string url = BaseURL + "api/enterpriseorgs/getaffiliatedmasterbridgeresults/";
            string res = string.Empty;
            input.AffiliationLimit = ConfigurationManager.AppSettings["AffiliationMasterBridgeInitialLimit"];
            res = await InvokeWebService.PostResourceAsync(url, Token, input, ClientID);

            //string url = BaseURL + "api/enterpriseorgs/getaffiliatedmasterbridgeresults/" + ent_org_id;
            //string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID, 180);
            string strRes = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                AffiliatedMasterBridgeOutput JObjRes = new AffiliatedMasterBridgeOutput();
                JObjRes.lt_affil_res = new List<AffiliatedMasterBridgeOutputModel>();
                JObjRes.summary_info = new AffiliatedMasterBridgeSummary();
                JObjRes = JsonConvert.DeserializeObject<AffiliatedMasterBridgeOutput>(res); 
                List<AffiliatedMasterBridgeOutputModel> JObj = new List<AffiliatedMasterBridgeOutputModel>();
                JObj = JObjRes.lt_affil_res;
                
                //State Code Name Mapping Dictionary
                Dictionary<string, string> dictStateCodeNameMap = new Dictionary<string,string>();
                dictStateCodeNameMap.Add("AL", "Alabama");
                dictStateCodeNameMap.Add("AK", "Alaska");
                dictStateCodeNameMap.Add("AZ", "Arizona");
                dictStateCodeNameMap.Add("AR", "Arkansas");
                dictStateCodeNameMap.Add("CA", "California");
                dictStateCodeNameMap.Add("CO", "Colorado");
                dictStateCodeNameMap.Add("CT", "Connecticut");
                dictStateCodeNameMap.Add("DE", "Delaware");
                dictStateCodeNameMap.Add("DC", "District of Columbia");
                dictStateCodeNameMap.Add("FL", "Florida");
                dictStateCodeNameMap.Add("GA", "Georgia");
                dictStateCodeNameMap.Add("HI", "Hawaii");
                dictStateCodeNameMap.Add("ID", "Idaho");
                dictStateCodeNameMap.Add("IL", "Illinois");
                dictStateCodeNameMap.Add("IN", "Indiana");
                dictStateCodeNameMap.Add("IA", "Iowa");
                dictStateCodeNameMap.Add("KS", "Kansas");
                dictStateCodeNameMap.Add("KY", "Kentucky");
                dictStateCodeNameMap.Add("LA", "Louisiana");
                dictStateCodeNameMap.Add("ME", "Maine");
                dictStateCodeNameMap.Add("MD", "Maryland");
                dictStateCodeNameMap.Add("MA", "Massachusetts");
                dictStateCodeNameMap.Add("MI", "Michigan");
                dictStateCodeNameMap.Add("MN", "Minnesota");
                dictStateCodeNameMap.Add("MS", "Mississippi");
                dictStateCodeNameMap.Add("MO", "Missouri");
                dictStateCodeNameMap.Add("MT", "Montana");
                dictStateCodeNameMap.Add("NE", "Nebraska");
                dictStateCodeNameMap.Add("NV", "Nevada");
                dictStateCodeNameMap.Add("NH", "New Hampshire");
                dictStateCodeNameMap.Add("NJ", "New Jersey");
                dictStateCodeNameMap.Add("NM", "New Mexico");
                dictStateCodeNameMap.Add("NY", "New York");
                dictStateCodeNameMap.Add("NC", "North Carolina");
                dictStateCodeNameMap.Add("ND", "North Dakota");
                dictStateCodeNameMap.Add("OH", "Ohio");
                dictStateCodeNameMap.Add("OK", "Oklahoma");
                dictStateCodeNameMap.Add("OR", "Oregon");
                dictStateCodeNameMap.Add("PA", "Pennsylvania");
                dictStateCodeNameMap.Add("RI", "Rhode Island");
                dictStateCodeNameMap.Add("SC", "South Carolina");
                dictStateCodeNameMap.Add("SD", "South Dakota");
                dictStateCodeNameMap.Add("TN", "Tennessee");
                dictStateCodeNameMap.Add("TX", "Texas");
                dictStateCodeNameMap.Add("UT", "Utah");
                dictStateCodeNameMap.Add("VT", "Vermont");
                dictStateCodeNameMap.Add("VA", "Virginia");
                dictStateCodeNameMap.Add("WA", "Washington");
                dictStateCodeNameMap.Add("WV", "West Virginia");
                dictStateCodeNameMap.Add("WI", "Wisconsin");
                dictStateCodeNameMap.Add("WY", "Wyoming");
                dictStateCodeNameMap.Add("AS", "American Samoa");
                dictStateCodeNameMap.Add("GU", "Guam");
                dictStateCodeNameMap.Add("MP", "North Mariana Islands");
                dictStateCodeNameMap.Add("PR", "Puerto Rico");
                dictStateCodeNameMap.Add("VI", "Virgin Islands");
                dictStateCodeNameMap.Add("AA", "Armed Forces Americas");
                dictStateCodeNameMap.Add("AE", "Armed Forces Eur.,Mid.East,Africa,Canada");
                dictStateCodeNameMap.Add("AP", "Armed Forces Pacific");
                dictStateCodeNameMap.Add("AB", "Alberta");
                dictStateCodeNameMap.Add("BC", "British Columbia");
                dictStateCodeNameMap.Add("MB", "Manitoba");
                dictStateCodeNameMap.Add("NB", "New Brunswick");
                dictStateCodeNameMap.Add("NF", "Newfoundland");
                dictStateCodeNameMap.Add("NT", "Northwest Territories");
                dictStateCodeNameMap.Add("NS", "Nova Scotia");
                dictStateCodeNameMap.Add("NU", "Nunavut");
                dictStateCodeNameMap.Add("ON", "Ontario");
                dictStateCodeNameMap.Add("PE", "Prince Edward Island");
                dictStateCodeNameMap.Add("QC", "Quebec");
                dictStateCodeNameMap.Add("SK", "Saskatchewan");
                dictStateCodeNameMap.Add("YT", "Yukon Territory");
                dictStateCodeNameMap.Add("FM", "Federated States of Micronesia");
                dictStateCodeNameMap.Add("MH", "Marshall Islands");
                dictStateCodeNameMap.Add("PW", "Palau Island");

                //Standardize the state and city columns
                foreach (AffiliatedMasterBridgeOutputModel ind in JObj)
                {
                    if (string.IsNullOrEmpty(ind.mstr_state))
                        ind.mstr_state = "Unknown";
                    if (string.IsNullOrEmpty(ind.mstr_city))
                        ind.mstr_city = "Unknown";
                    if (dictStateCodeNameMap.ContainsKey(ind.mstr_state))
                        ind.mstr_state = dictStateCodeNameMap[ind.mstr_state];
                }

                //External Source Flag
                List<string> lt = new List<string>();
                //Internal Source Flag
                List<string> ltInternal = new List<string>();
                foreach (var x in JObj.Select(t => new { t.cnst_mstr_id, t.line_of_service_cd }).Distinct())
                {
                    if (x.line_of_service_cd == "EOSI")
                        lt.Add(x.cnst_mstr_id);
                    else
                        ltInternal.Add(x.cnst_mstr_id);
                }

                //Master Count and Bridge Count
                Dictionary<string, string> ltSt = new Dictionary<string, string>();
                Dictionary<Dictionary<string, string>, string> ltCity = new Dictionary<Dictionary<string, string>, string>();
                
                var stGroupRes = from t in JObj
                                 group t by t.mstr_state into grp
                                 select new { state = grp.Key, mstrCnt = grp.Select(t1 => t1.cnst_mstr_id).Distinct().Count(), bridCnt = grp.Count() };

                var cityGroupRes = from t in JObj
                                   group t by new { t.mstr_state, t.mstr_city } into grp
                                   select new { city = grp.Key, mstrCnt = grp.Select(t1 => t1.cnst_mstr_id).Distinct().Count(), bridCnt = grp.Count() };

                foreach(var x in stGroupRes)
                {
                    ltSt.Add(x.state, "Master (" + x.mstrCnt + "), Bridge (" + x.bridCnt + ")|");
                }

                foreach (var x in cityGroupRes)
                {
                    Dictionary<string, string> temp = new Dictionary<string, string>();
                    temp.Add(x.city.mstr_state, x.city.mstr_city);
                    ltCity.Add(temp, "Master (" + x.mstrCnt + "), Bridge (" + x.bridCnt + ")");
                }
                foreach (AffiliatedMasterBridgeOutputModel a in JObj)
                {
                    //Master Count and Bridge Count
                    foreach (KeyValuePair<Dictionary<string, string>, string> keyValue in ltCity)
                    {
                        if (keyValue.Key.ContainsKey(a.mstr_state) && keyValue.Key.ContainsValue(a.mstr_city))
                        {
                            a.mstr_city += " - " + ltCity[keyValue.Key];
                        }
                    }

                    //Master Count and Bridge Count
                    if (ltSt.ContainsKey(a.mstr_state))
                        a.mstr_state += " - " + ltSt[a.mstr_state];

                    //Counts
                    if (lt.Contains(a.cnst_mstr_id))
                    {
                        //External Source Flag
                        a.extn_sys_flag = "1";
                        
                        //External only
                        if (!ltInternal.Contains(a.cnst_mstr_id))
                            a.prospect_cnt = 1;
                        else
                        {
                            //External and contains other source records as well
                            if (a.intrnl_prod_sys == "Active") //Active
                                a.act_valid_cnt = 1;
                            else //Dormant
                                a.inact_valid_cnt = 1;
                        }
                    }
                    else
                    {
                        //External Source Flag
                        a.extn_sys_flag = "0";

                        //Internal only and contains other source records as well
                        if (a.intrnl_prod_sys == "Active") //Active
                            a.act_invalid_cnt = 1;
                        else //Dormant
                            a.inact_invalid_cnt = 1;
                    }
                }

                JObjRes.lt_affil_res = JObj;
                JavaScriptSerializer ser = new JavaScriptSerializer();
                ser.MaxJsonLength = 2147483647;

                strRes = ser.Serialize(JObjRes);
            }
            else
            {
                strRes = res;
            }
            return handleTrivialHttpRequests(strRes);
        }

        [HandleException]
        [HttpPost]
        //Get method to export the details
        public async Task<FileContentResult> ExportDetails(ExportDetailsInput input)
        {
            string url = BaseURL + "api/enterpriseorgs/getaffiliatedmasterbridgeexportresults/" + input.strEntOrgId;

            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID, 180);
            //List<AffiliatedMasterBridgeExportOutputModel> listExport = new List<AffiliatedMasterBridgeExportOutputModel>();
            //listExport.Add(new AffiliatedMasterBridgeExportOutputModel {ent_org_id = "2", cnst_mstr_id = "10", rec_typ = "MASTER", name = "fairfax", address = "Address1", extrn_flg = 1, intrnl_flg = 0, concatenated_val = "fairfax - Address1"});
            //listExport.Add(new AffiliatedMasterBridgeExportOutputModel { ent_org_id = "2", cnst_mstr_id = "10", rec_typ = "BRIDGE", arc_srcsys_cd = "EOSI", line_of_service_cd ="EOSI", name = "fairfax", address = "AddressB1", extrn_flg = 1, intrnl_flg = 0, concatenated_val = "EOSI - fairfax - AddressB1" });
            //listExport.Add(new AffiliatedMasterBridgeExportOutputModel { ent_org_id = "2", cnst_mstr_id = "20", rec_typ = "MASTER", name = "fairfax", address = "Address2", extrn_flg = 0, intrnl_flg = 0, concatenated_val = "fairfax - Address2"});
            //listExport.Add(new AffiliatedMasterBridgeExportOutputModel { ent_org_id = "2", cnst_mstr_id = "30", rec_typ = "MASTER", name = "fairfax", address = "Address3", extrn_flg = 1, intrnl_flg = 1, concatenated_val = "fairfax - Address3" });
            //listExport.Add(new AffiliatedMasterBridgeExportOutputModel { ent_org_id = "2", cnst_mstr_id = "30", rec_typ = "BRIDGE", arc_srcsys_cd = "AGGD", line_of_service_cd = "EOSI", name = "fairfax", address = "AddressB3", extrn_flg = 1, intrnl_flg = 0, concatenated_val = "AGGD - fairfax - AddressB3"});
            //listExport.Add(new AffiliatedMasterBridgeExportOutputModel { ent_org_id = "2", cnst_mstr_id = "30", rec_typ = "BRIDGE", arc_srcsys_cd = "TAFS", line_of_service_cd = "FR", name = "fairfax", address = "AddressB31", extrn_flg = 0, intrnl_flg = 1, concatenated_val = "TAFS - fairfax - AddressB31" });

            //Calling the service
            var result = (dynamic)null;
            string strFilePath = string.Empty;
            strFilePath = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AffiliationBridgesExportTemplatePath"]);

            FileInfo newFile = new FileInfo(strFilePath);
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorkbook workbook = package.Workbook;

                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                //worksheet.Cells["A2"].LoadFromCollection(result, false);

                if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
                {
                    List<AffiliatedMasterBridgeExportOutputModel> JObj = JsonConvert.DeserializeObject<List<AffiliatedMasterBridgeExportOutputModel>>(res);
                    worksheet.Cells["A2"].LoadFromCollection(JObj, false);
                }
                ExcelWorksheet pivotSheet = workbook.Worksheets.Add("PivotSummary");
                pivotSheet.View.ShowGridLines = false;

                //Consolidated Values
                pivotSheet.Cells[2, 1].Value = "Caterories";
                pivotSheet.Cells[2, 2].Value = "Master Count";
                pivotSheet.Cells[2, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                pivotSheet.Cells[2, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                Color colFromHexFill = System.Drawing.ColorTranslator.FromHtml("#5b9bd5");
                Color colFromHexText = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                pivotSheet.Cells[2, 1].Style.Fill.BackgroundColor.SetColor(colFromHexFill);
                pivotSheet.Cells[2, 2].Style.Fill.BackgroundColor.SetColor(colFromHexFill);
                pivotSheet.Cells[2, 1].Style.Font.Color.SetColor(colFromHexText);
                pivotSheet.Cells[2, 2].Style.Font.Color.SetColor(colFromHexText);

                pivotSheet.Cells[3, 1].Value = "Active Validated";
                pivotSheet.Cells[3, 2].Formula = "SUM(Table1[Active Validated])";
                pivotSheet.Cells[4, 1].Value = "Inactive Validated";
                pivotSheet.Cells[4, 2].Formula = "SUM(Table1[Inactive Validated])";
                pivotSheet.Cells[5, 1].Value = "Prospect/Opportunity";
                pivotSheet.Cells[5, 2].Formula = "SUM(Table1[Prospect])";
                pivotSheet.Cells[6, 1].Value = "Active Unvalidated";
                pivotSheet.Cells[6, 2].Formula = "SUM(Table1[Active Unvalidated])";
                pivotSheet.Cells[7, 1].Value = "Inactive Unvalidated";
                pivotSheet.Cells[7, 2].Formula = "SUM(Table1[Inactive Unvalidated])";

                pivotSheet.Cells["A2:B7"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                pivotSheet.Cells["A2:B7"].Style.Border.Top.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#5b9bd5"));
                pivotSheet.Cells["A2:B7"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                pivotSheet.Cells["A2:B7"].Style.Border.Bottom.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#5b9bd5"));
                pivotSheet.Cells["A2:B7"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                pivotSheet.Cells["A2:B7"].Style.Border.Right.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#5b9bd5"));
                pivotSheet.Cells["A2:B7"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                pivotSheet.Cells["A2:B7"].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#5b9bd5"));

                var dataRange = worksheet.Cells[worksheet.Dimension.Address.ToString()];
                //dataRange.AutoFitColumns();
                var pivotTable = pivotSheet.PivotTables.Add(pivotSheet.Cells["A10"], dataRange, "MasterGrouping");
                pivotTable.MultipleFieldFilters = true;
                pivotTable.RowGrandTotals = false;
                pivotTable.ColumGrandTotals = true;
                pivotTable.Compact = true;
                pivotTable.CompactData = true;
                pivotTable.GridDropZones = false;
                pivotTable.Outline = false;
                pivotTable.OutlineData = false;
                pivotTable.ShowError = true;
                pivotTable.ErrorCaption = "[error]";
                pivotTable.ShowHeaders = true;
                pivotTable.UseAutoFormatting = true;
                pivotTable.ApplyWidthHeightFormats = true;
                pivotTable.ShowDrill = true;
                pivotTable.FirstDataCol = 2;
                pivotTable.RowHeaderCaption = "Row Labels";
                pivotTable.DataOnRows = false;
                pivotTable.DataCaption = "";

                if (input.strSectionName == "MasterBridgeLocation")
                {
                    var stateField = pivotTable.Fields["State"];
                    pivotTable.RowFields.Add(stateField);
                    stateField.Sort = OfficeOpenXml.Table.PivotTable.eSortType.Ascending;

                    var cityField = pivotTable.Fields["City"];
                    pivotTable.RowFields.Add(cityField);
                    cityField.Sort = OfficeOpenXml.Table.PivotTable.eSortType.Ascending;
                }

                var masterIdField = pivotTable.Fields["Master ID"];
                masterIdField.Name = "Master ID";
                pivotTable.RowFields.Add(masterIdField);
                masterIdField.Sort = OfficeOpenXml.Table.PivotTable.eSortType.Descending;
                
                var concatenatedValueField = pivotTable.Fields["Concatenated Value"];
                pivotTable.RowFields.Add(concatenatedValueField);
                concatenatedValueField.Sort = OfficeOpenXml.Table.PivotTable.eSortType.Ascending;

                var externalField = pivotTable.Fields["Master Type Indicator"];
                if (input.strSectionName == "MasterBridgeLocation")
                    pivotTable.DataFields.Add(externalField).Function = DataFieldFunctions.Sum;
                else
                    pivotTable.DataFields.Add(externalField).Function = DataFieldFunctions.Max;
                pivotTable.DataFields[0].Name = "Master Type";

                var internalField = pivotTable.Fields["DSP Verified Indicator"];
                if (input.strSectionName == "MasterBridgeLocation")
                    pivotTable.DataFields.Add(internalField).Function = DataFieldFunctions.Sum;
                else
                    pivotTable.DataFields.Add(internalField).Function = DataFieldFunctions.Max;
                pivotTable.DataFields[1].Name = "DSP Verified";

                //Apply conditional formatting for the Master ID column in pivot table
                ExcelAddress _formatRangeAddress = new ExcelAddress("A10:A1048576");
                //Fill GREEN color if value contains VERIFIED word in it
                string _statement = "IF(AND(C10=0,B10=1,NOT(ISNUMBER(SEARCH(\" - BRIDGE - \",A10))),NOT(ISNUMBER(SEARCH(\" - ARCBEST - \",A10)))),1,0)";
                var _cond1 = pivotSheet.ConditionalFormatting.AddExpression(_formatRangeAddress);
                _cond1.Style.Font.Color.Color = Color.Red;
                _cond1.Formula = _statement;
                string _statement1 = "IF(AND(C10=0,B10=0,NOT(ISNUMBER(SEARCH(\" - BRIDGE - \",A10))),NOT(ISNUMBER(SEARCH(\" - ARCBEST - \",A10)))),1,0)";
                var _cond2 = pivotSheet.ConditionalFormatting.AddExpression(_formatRangeAddress);
                _cond2.Style.Font.Color.Color = Color.Orange;
                _cond2.Formula = _statement1;

                return new FileContentResult(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            return null;
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        public async Task<JsonResult> GetNaicsCodeStewDetails(string ent_org_id)
        {
            string url = BaseURL + "api/enterpriseorgs/getnaicsdetails/" + ent_org_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            string strRes = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                List<GetNAICSDetailsOutput> JObj = JsonConvert.DeserializeObject<List<GetNAICSDetailsOutput>>(res);
                strRes = (new JavaScriptSerializer()).Serialize(JObj);
            }
            else
            {
                strRes = res;
            }
            return handleTrivialHttpRequests(strRes);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        public async Task<JsonResult> GetRankingStewDetails(string ent_org_id)
        {
            string url = BaseURL + "api/enterpriseorgs/getrankingdetails/" + ent_org_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            string strRes = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                List<RankingOutputModel> JObj = JsonConvert.DeserializeObject<List<RankingOutputModel>>(res);
                strRes = (new JavaScriptSerializer()).Serialize(JObj);
            }
            else
            {
                strRes = res;
            }
            return handleTrivialHttpRequests(strRes);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        public async Task<JsonResult> GetTransactionHistoryDetails(string ent_org_id)
        {
            string url = BaseURL + "api/enterpriseorgs/gettransactionhistorydetails/" + ent_org_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            string strRes = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                List<TransactionHistoryOutputModel> JObj = JsonConvert.DeserializeObject<List<TransactionHistoryOutputModel>>(res);
                strRes = (new JavaScriptSerializer()).Serialize(JObj);
            }
            else
            {
                strRes = res;
            }
            return handleTrivialHttpRequests(strRes);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R", "RW")]
        public async Task<JsonResult> GetRFMDetails(string ent_org_id)
        {
            string url = BaseURL + "api/enterpriseorgs/getrfmdetails/" + ent_org_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            string strRes = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                List<RFMDetails> JObj = JsonConvert.DeserializeObject<List<RFMDetails>>(res);
                strRes = (new JavaScriptSerializer()).Serialize(JObj);
            }
            else
            {
                strRes = res;
            }
            return handleTrivialHttpRequests(strRes);
        }

        /* ******************************* Controller methods corresponding to actions performed under details section ******************************* */
        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<JsonResult> GetTagDDList()
        {
            //Place the API call to process the request
            string serviceUrl = BaseURL + "api/enterpriseorgs/gettagddlist";
            string serviceResult = await InvokeWebService.GetResourceAsync(serviceUrl, Token, ClientID);
            return handleTrivialHttpRequests(serviceResult);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<JsonResult> UpdateTags(TagUpdateInputModel input)
        {
            //Get the user name and embed to the service input for processing the actions
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
            input.user_nm = strUserName;
            string strRes = string.Empty;

            //Place the API call to process the request
            string serviceUrl = BaseURL + "api/enterpriseorgs/updatetags";
            string serviceResult = await InvokeWebService.PostResourceAsync(serviceUrl, Token, input, ClientID);
            //if (!serviceResult.ToLower().Contains("error") && !serviceResult.ToLower().Contains("timedout"))
            //{
            //    List<TagUpdateOutputModel> JObj = JsonConvert.DeserializeObject<List<TagUpdateOutputModel>>(serviceResult);
            //    TagUpdateOutputModel outObj = new TagUpdateOutputModel();
            //    outObj = JObj[0];
            //    strRes = (new JavaScriptSerializer()).Serialize(outObj);
            //}
            //else
            //{
            //    strRes = serviceResult;
            //}
            return handleTrivialHttpRequests(serviceResult);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<JsonResult> UpdateTransformations(TransformationUpdateInput input)
        {
            //Get the user name and embed to the service input for processing the actions
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
            input.userId = strUserName;
            string strRes = string.Empty;

            //Place the API call to process the request
            string serviceUrl = BaseURL + "api/enterpriseorgs/updatetransformations";
            string serviceResult = await InvokeWebService.PostResourceAsync(serviceUrl, Token, input, ClientID);
            return handleTrivialHttpRequests(serviceResult);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<JsonResult> SmokeTestingCount(TransformationUpdateInput input)
        {
            //Get the user name and embed to the service input for processing the actions
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
            input.userId = strUserName;
            string strRes = string.Empty;

            //Place the API call to process the request
            string serviceUrl = BaseURL + "api/enterpriseorgs/smoketesttransformations";
            string serviceResult = await InvokeWebService.PostResourceAsync(serviceUrl, Token, input, ClientID);
            return handleTrivialHttpRequests(serviceResult);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<FileContentResult> SmokeTestingExportTransformationOrgDetails(TransformationUpdateInput input)
        {
            //Get the user name and embed to the service input for processing the actions
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
            input.userId = strUserName;
            string strRes = string.Empty;

            //Place the API call to process the request
            string serviceUrl = BaseURL + "api/enterpriseorgs/smoketesttransformationsexport";
            string serviceResult = await InvokeWebService.PostResourceAsync(serviceUrl, Token, input, ClientID);
            
            string strFilePath = string.Empty;
            strFilePath = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["TransformationSmokeTestResultsTemplatePath"]);

            FileInfo newFile = new FileInfo(strFilePath);
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorkbook workbook = package.Workbook;

                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                if (!serviceResult.ToLower().Contains("error") && !serviceResult.ToLower().Contains("timedout"))
                {
                    List<TransformationSmokeTestResults> JObj = JsonConvert.DeserializeObject<List<TransformationSmokeTestResults>>(serviceResult);
                    worksheet.Cells["A2"].LoadFromCollection(JObj, false);
                }
                return new FileContentResult(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            return null;
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<JsonResult> UpdateAffiliations(OrgAffiliatorsInput input)
        {
            //Get the user name and embed to the service input for processing the actions
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
            input.usr_nm = strUserName;
            string strRes = string.Empty;

            //Place the API call to process the request
            string serviceUrl = string.Empty;
            if(input.req_typ == "Add")
                serviceUrl = BaseURL + "api/Constituent/addorgaffiliators";
            else
                serviceUrl = BaseURL + "api/Constituent/deleteorgaffiliators";

            string serviceResult = await InvokeWebService.PostResourceAsync(serviceUrl, Token, input, ClientID);
            return handleTrivialHttpRequests(serviceResult);
        }

        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<JsonResult> UpdateHierarchy(HierarchyUpdateInput input)
        {
            //Get the user name and embed to the service input for processing the actions
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
            input.userid = strUserName;
            string strRes = string.Empty;

            //Place the API call to process the request
            string serviceUrl = BaseURL + "api/enterpriseorgs/updatehierarchy";
            string serviceResult = await InvokeWebService.PostResourceAsync(serviceUrl, Token, input, ClientID);
            return handleTrivialHttpRequests(serviceResult);
        }

        /* ************************ Generic method to capture exceptions ************************ */
        private JsonResult handleTrivialHttpRequests(string res)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = 2147483647;
            var JObj = ser.DeserializeObject(res);
            if (JObj is Array)
            {
                var results = ser.DeserializeObject(res);
                return new JsonResult()
                {
                    Data = results,
                    MaxJsonLength = 2147483647,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                }; 
                //return Json(results, JsonRequestBehavior.AllowGet);
            }
            if (!res.Equals("") && res != null)
            {
                if (res.ToLower().Contains("timedout"))
                {
                    throw new CustomExceptionHandler(Json("TimedOut"));
                }
                if (res.ToLower().Contains("error"))
                {
                    throw new CustomExceptionHandler(Json("DatabaseError"));
                }
                if (res.ToLower().Contains("unauthorized"))
                {
                    throw new CustomExceptionHandler(Json("Unauthorized"));
                }
                var result = ser.DeserializeObject(res);

                //return Json(result, JsonRequestBehavior.AllowGet);
                return new JsonResult()
                {
                    Data = result,
                    MaxJsonLength = 2147483647,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                }; 
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        private void checkExceptions(string res)
        {
            var JObj = (new JavaScriptSerializer()).DeserializeObject(res);
            if (JObj is Array)
            {
                var results = (new JavaScriptSerializer()).DeserializeObject(res);
                //return Json(results, JsonRequestBehavior.AllowGet);
            }
            if (res.ToLower().Contains("timedout"))
            {
                throw new CustomExceptionHandler(Json("TimedOut"));
            }
            if (res.ToLower().Contains("error"))
            {
                throw new CustomExceptionHandler(Json("DatabaseError"));
            }
            if (res.ToLower().Contains("unauthorized"))
            {
                throw new CustomExceptionHandler(Json("Unauthorized"));
            }
        }
        public JsonResult GetFortuneRankProvider()
        {
            string strPath = string.Empty;
            strPath = ConfigurationManager.AppSettings["FortuneRankProviderFilePath"];

            var JSON = Utilities.readJSONTFromFile<List<string>>(strPath);
            JSON.Sort();
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChapterSystemData()
        {
            string strPath = string.Empty;
            strPath = ConfigurationManager.AppSettings["ChapterSystemDataFilePath"];

            var JSON = Utilities.readJSONTFromFile<List<string>>(strPath);
            JSON.Sort();
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [HandleException]
        [TabLevelSecurity("enterprise_orgs_tb_access", "R","RW")]
        public async Task<JsonResult> GetTagsData()
        {
            //string strPath = string.Empty;
            //strPath = ConfigurationManager.AppSettings["TagsDataFilePath"];

            //var JSON = Utilities.readJSONTFromFile<List<string>>(strPath);
            //JSON.Sort();
            //return Json(JSON, JsonRequestBehavior.AllowGet);

            string url = BaseURL + "api/enterpriseorgs/gettagddlist/";
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            string strRes = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                List<TagDDList> JObj = JsonConvert.DeserializeObject<List<TagDDList>>(res);
                List<string> tagList = new List<string>();
                foreach (TagDDList tags in JObj)
                {                    
                    tagList.Add(tags.tag);

                }
                strRes = (new JavaScriptSerializer()).Serialize(tagList);
            }
            else
            {
                strRes = res;
            }
            return handleTrivialHttpRequests(strRes);
        }
        /* Method name: formTreeDataStructureEnterpriseOrgsSearchResult
        * Input Parameters: An object of EnterpriseOrgOutputSearchResults class
        * Output Parameters: A list of EnterpriseOrgOutputSearchResults class  
        * Purpose: This method uses all the naics codes and forms a tree hierachy data structure */
        public IList<EnterpriseOrgOutputSearchResults> formTreeDataStructureEnterpriseOrgsSearchResult(IList<EnterpriseOrgOutputSearchResults> listResult)
        {
            //create a list of OrgsSearchResult class
            List<EnterpriseOrgOutputSearchResults> result = new List<EnterpriseOrgOutputSearchResults>();

            //create a dictionary of search result and the object. This is used to store all the search result from the db
            Dictionary<string, EnterpriseOrgOutputSearchResults> alreadyRead = new Dictionary<string, EnterpriseOrgOutputSearchResults>();

            //for each record from db
            foreach (EnterpriseOrgOutputSearchResults resultData in listResult)
            {

                //populate the dictionary for this search result
                 alreadyRead[resultData.ent_org_id] = resultData;
            }

            //for each naics code in the dictionary
            foreach (EnterpriseOrgOutputSearchResults resultData in alreadyRead.Values)
            {
                //get the parent id
                EnterpriseOrgOutputSearchResults aParent;
                string strParentId = resultData.parent_ent_org_id;

                //if parent is present , then append the current search result as the parent's child
                if(!string.IsNullOrEmpty(strParentId))
                {
                    if (alreadyRead.TryGetValue(strParentId, out aParent))
                    {
                        aParent.AddSubResultData(resultData);
                    }
                    else
                    {
                        result.Add(resultData);
                    }
                }
                else
                {
                    result.Add(resultData);
                }
            }

            //return back the result
            return result;
        }


        [HandleException]
        [Route("GetEnterpriseCharacteristics/{id}")]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW", "R")]
        public async Task<JsonResult> GetEnterpriseCharacteristics(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/enterpriseorgs/GetOrgCharacteristics/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                serializer = new JavaScriptSerializer();
                List<Characteristics> characterList = serializer.Deserialize<List<Characteristics>>(res);
                Process<Characteristics> process = new Process<Characteristics>();
                List<Characteristics> convertedCharacterList = process.convertRecords(characterList, Constant.CONSTANTS["ENT_ORG_CHARACTERISTICS"]);
                return Json(convertedCharacterList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HandleException]
        [Route("GetAllEnterpriseCharacteristics/{id}")]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW", "R")]
        public async Task<JsonResult> GetAllEnterpriseCharacteristics(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/enterpriseorgs/GetOrgAllCharacteristics/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);


        }


        /* Create, Delete and Update API's for Characteristics*/
        [HttpPost]
        [HandleException]
        [Route("AddCharacteristics")]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<JsonResult> AddCharacteristics(OrgCharacteristicsInput ConstCharacteristicsInput)
        {

            string url = BaseURL + "api/enterpriseorgs/AddOrgCharacteristics/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstCharacteristicsInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [Route("DeleteCharacteristics")]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<JsonResult> DeleteCharacteristics(OrgCharacteristicsInput ConstCharacteristicsInput)
        {

            string url = BaseURL + "api/enterpriseorgs/DeleteOrgCharacteristics/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstCharacteristicsInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }

        [HandleException]
        [HttpPost]
        [Route("EditCharacteristics")]
        [TabLevelSecurity("enterprise_orgs_tb_access", "RW")]
        public async Task<JsonResult> EditCharacteristics(OrgCharacteristicsInput ConstCharacteristicsInput)
        {

            string url = BaseURL + "api/enterpriseorgs/EditOrgCharacteristics/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstCharacteristicsInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }

    }
}