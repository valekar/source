using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

using Orgler.Models.TopAccount;
using Orgler.Models.Common;
using Orgler.ViewModels;
using Orgler.Exceptions;
using Orgler.Services;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Security.Principal;
using Orgler.Security;

namespace Orgler.Controllers
{
    [Authorize]
    public class TopAccountServiceController : BaseController
    {
        JavaScriptSerializer serializer;

        //Search Method
        [HandleException]
        [HttpPost]
        /* Method name: Search
        * Input Parameters: An object of SearchInput class
        * Purpose: This method search results for new account */
        [TabLevelSecurity("topaccount_tb_access", "RW", "R")]
        public async Task<ActionResult> Search(TopOrgsSearchInput searchInput)
        {
            TopAccountViewModel viewModel = new TopAccountViewModel();
            viewModel.SearchInput = searchInput;

            string res = string.Empty;

            res = (new JavaScriptSerializer()).Serialize(viewModel);

            //Only if search input is valid
            if (viewModel.boolValidSearch)
            {
                string url = BaseURL + "api/accountmonitoring/toporgssearch/";
                res = await InvokeWebService.PostResourceAsync(url, Token, searchInput, ClientID);
                checkExceptions(res);

                if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
                {
                    viewModel.objSearchResults = (new JavaScriptSerializer()).Deserialize<List<TopAccountSearchResults>>(res);
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
                        TopAccountSearchResults search = new TopAccountSearchResults();
                        search.listNAICSCodesAndDesc = naicsdetails;
                        viewModel.objSearchResults[j].listNAICSCodesAndDesc = search.listNAICSCodesAndDesc;
                    }


                    viewModel.objFRSearchResults = viewModel.objSearchResults.Where(x => x.line_of_service_cd == "FR").ToList();
                    viewModel.objBIOSearchResults = viewModel.objSearchResults.Where(x => x.line_of_service_cd == "BIO").ToList();
                    viewModel.objPHSSSearchResults = viewModel.objSearchResults.Where(x => x.line_of_service_cd == "PHSS").ToList();
                    res = (new JavaScriptSerializer()).Serialize(viewModel);
                }
            }

            return handleTrivialHttpRequests(res);
        }

        /* Method name: AddNAICSCode
         * Input Parameters: An object of AddNAICSCodeInput class
         * Purpose: This method Add Naics Code*/
        [TabLevelSecurity("topaccount_tb_access", "RW")]
        public async Task<ActionResult> AddNAICSCode(AddNAICSCodeInput addInput)
        {
            //addInput.usr_nm = "Keerthana";
            IPrincipal p = System.Web.HttpContext.Current.User;
            addInput.usr_nm = Orgler.Security.Extentions.GetUserName(p);
            string url = BaseURL + "api/accountmonitoring/addnaicscode/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, addInput, ClientID);
            return handleTrivialHttpRequests(res);
        }
        /* Method name: SubmitNAICSCodeUpdates
         * Input Parameters: An object of UpdateNAICSCodes class
         * Purpose: This method Updates Naics Code*/
        [TabLevelSecurity("topaccount_tb_access", "RW")]
        public async Task<ActionResult> SubmitNAICSCodeUpdates(UpdateNAICSCodes updateInput)
        {
            //updateInput.usr_nm = "Keerthana";
            IPrincipal p = System.Web.HttpContext.Current.User;
            updateInput.usr_nm = Orgler.Security.Extentions.GetUserName(p);
            string url = BaseURL + "api/accountmonitoring/naicsstatuschange/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, updateInput, ClientID);
            return handleTrivialHttpRequests(res);
        }
        /* Method name: EditNAICSCode
        * Input Parameters: An object of EditNAICSCodeInput class
        * Purpose: This method Edits Naics Code*/
        [TabLevelSecurity("topaccount_tb_access", "RW")]
        public async Task<ActionResult> EditNAICSCode(EditNAICSCodeInput editInput)
        {
            //editInput.usr_nm = "Keerthana";
            IPrincipal p = System.Web.HttpContext.Current.User;
            editInput.usr_nm = Orgler.Security.Extentions.GetUserName(p);
            string url = BaseURL + "api/accountmonitoring/updatenaicsstatus/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, editInput, ClientID);
            return handleTrivialHttpRequests(res);
        }
        /* Method name: ConfirmAccount
        * Input Parameters: An object of ConfirmAccountInput class
        * Purpose: This method Confirms the accounts*/
        [TabLevelSecurity("topaccount_tb_access", "RW")]
        public async Task<ActionResult> ConfirmAccount(ConfirmAccountInput input)
        {
            //input.usr_nm = "Keerthana";
            IPrincipal p = System.Web.HttpContext.Current.User;
            input.usr_nm = Orgler.Security.Extentions.GetUserName(p);
            string url = BaseURL + "api/accountmonitoring/confirmaccount/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, input, ClientID);
            return handleTrivialHttpRequests(res);
        }
        /* Method name: getMasterNAICSDetails
        * Input Parameters: An object of GetMasterNAICSDetailsInput class
        * Purpose: This method gets the master naics details*/
        [TabLevelSecurity("topaccount_tb_access", "RW", "R")]
        public async Task<ActionResult> getMasterNAICSDetails(GetMasterNAICSDetailsInput input)
        {
            string url = BaseURL + "api/accountmonitoring/getorgnaicsdetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, input, ClientID);
            return handleTrivialHttpRequests(res);
        }

        /* Method name:  getRFMDetails
      * Input Parameters:  
      * Purpose: This method gets the RFM  details*/
        public async Task<ActionResult> getRFMDetails()
        {
            string url = BaseURL + "api/accountmonitoring/getrfmdetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, null, ClientID);
            return handleTrivialHttpRequests(res);
        }
        /* Method name: getPotentialMergesDetails
        * Input Parameters: An object of PotentialMergeInput class
        * Purpose: This method gets the Potential Merges Details*/
        [TabLevelSecurity("topaccount_tb_access", "RW", "R")]
        public async Task<ActionResult> getPotentialMergesDetails(PotentialMergeInput input)
        {
            if (string.IsNullOrWhiteSpace(input.master_id)) input.master_id = input.master_id ?? "";
            string url = BaseURL + "api/accountmonitoring/getpotentialmergedetails/" + input.master_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);
        }
        /* Method name: GetNewAccountRecentSearches
         * Input Parameters: Null
         * Purpose: This method gets the New Account Recent Searches*/
        [HttpGet]
        [TabLevelSecurity("topaccount_tb_access", "RW", "R")]
        public JsonResult GetTopAccountRecentSearches()
        {
            string strRecentSearchPath = string.Empty;
            strRecentSearchPath = ConfigurationManager.AppSettings["TopAccountRecentSearchFilePath"];
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);

            var JSON = Utilities.readJSONTFromFile<List<TopOrgsSearchInput>>(strRecentSearchPath);
            var filteredJSON = JSON;
            if (JSON != null)
                filteredJSON = JSON.Where(x => x.srch_user_name == strUserName).ToList();
            //filteredJSON = JSON.Where(x => x.srch_user_name == User.Identity.Name).ToList();
            return Json(filteredJSON, JsonRequestBehavior.AllowGet);
        }
        [TabLevelSecurity("topaccount_tb_access", "RW", "R")]
        public JsonResult GetNAICSCodes()
        {
            string strPath = string.Empty;
            strPath = ConfigurationManager.AppSettings["NAICSCodeMetadataFilePath"];

            var JSON = Utilities.readJSONTFromFile<List<NAICSCode>>(strPath);
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [TabLevelSecurity("topaccount_tb_access", "RW", "R")]
        public JsonResult GetNAICSCodesLevel2and3()
        {
            string strPath = string.Empty;
            strPath = ConfigurationManager.AppSettings["NAICSCodeLevel2and3MetadataFilePath"];

            var JSON = Utilities.readJSONTFromFile<List<NAICSCode>>(strPath);
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string InsertTopAccountRecentSearch(TopOrgsSearchInput searchInput)
        {
            string strMessage = "Error, something happened while logging the Recent Searches. Please contact administrator.";
            //Check if search input is not empty
            if (!Utilities.IsAnyPropertyNotNull(searchInput))
                return "Top Account Search Log: Nothing to log";

            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);

            searchInput.srch_user_name = strUserName;
            //searchInput.srch_user_name = User.Identity.Name;

            string strRecentSearchPath = string.Empty;
            strRecentSearchPath = ConfigurationManager.AppSettings["TopAccountRecentSearchFilePath"];

            List<TopOrgsSearchInput> ltRecentSearch = new List<TopOrgsSearchInput>();
            try
            {
                var RecentSearch = Utilities.readJSONTFromFile<List<TopOrgsSearchInput>>(strRecentSearchPath);
                var filteredRecentSearch = RecentSearch; // This is to filter out the recent searches based on the username

                if (RecentSearch != null)
                {
                    //if (!string.IsNullOrEmpty(User.Identity.Name))
                    //{
                    //    filteredRecentSearch = RecentSearch.Where(x => x.srch_user_name == User.Identity.Name).ToList();
                    //    RecentSearch.RemoveAll(x => x.srch_user_name == User.Identity.Name);
                    //}
                    filteredRecentSearch = RecentSearch.Where(x => x.srch_user_name == strUserName).ToList();
                    RecentSearch.RemoveAll(x => x.srch_user_name == strUserName);

                    if (filteredRecentSearch.Count >= 10)
                    {
                        filteredRecentSearch.RemoveRange(9, filteredRecentSearch.Count - 9); // Remove all items greater than 9, since we show only top 10 recent searches
                    }
                    filteredRecentSearch.Insert(0, searchInput);
                    RecentSearch.AddRange(filteredRecentSearch);
                    var SuccessMessage = Utilities.writeJSONIntoFile<List<TopOrgsSearchInput>>(RecentSearch, strRecentSearchPath);
                    if (SuccessMessage.ToLower() == "success")
                        strMessage = "Top Account Recent searches are logged successfully";
                    else
                        return strMessage;
                }
                else
                {
                    ltRecentSearch.Add(searchInput);
                    var SuccessMessage = Utilities.writeJSONIntoFile<List<TopOrgsSearchInput>>(ltRecentSearch, strRecentSearchPath);
                    if (SuccessMessage.ToLower() == "success")
                        strMessage = "Top Account Recent searches are logged successfully";
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

        public string InsertTopOrgsRecentNAICS(NAICSRecentUpdate recentNAICS)
        {
            string strMessage = "Error, something happened while logging the Recent Searches. Please contact administrator.";
            //Check if search input is not empty
            if (!Utilities.IsAnyPropertyNotNull(recentNAICS))
                return "Top Orgs NAICS Update Log: Nothing to log";
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
            NAICSRecentData naicsRecentData = new NAICSRecentData();

            List<NAICSRecentData> ltRecentNAICSUpdate = new List<NAICSRecentData>();

            if (recentNAICS.added_naics_codes != null && recentNAICS.added_naics_titles != null)
            {
                for (int i = 0; i < recentNAICS.added_naics_codes.Count; i++)
                {
                    naicsRecentData = new NAICSRecentData();
                    naicsRecentData.naics_codes = recentNAICS.added_naics_codes[i];
                    naicsRecentData.naics_titles = recentNAICS.added_naics_titles[i];
                    naicsRecentData.naics_status = "";
                    naicsRecentData.usr_nm = strUserName;
                    ltRecentNAICSUpdate.Add(naicsRecentData);
                }
            }
            if (recentNAICS.approved_naics_codes != null && recentNAICS.approved_naics_titles != null)
            {
                for (int i = 0; i < recentNAICS.approved_naics_codes.Count; i++)
                {
                    naicsRecentData = new NAICSRecentData();
                    naicsRecentData.naics_codes = recentNAICS.approved_naics_codes[i];
                    naicsRecentData.naics_titles = recentNAICS.approved_naics_titles[i];
                    naicsRecentData.naics_status = "";
                    naicsRecentData.usr_nm = strUserName;
                    ltRecentNAICSUpdate.Add(naicsRecentData);
                }
            }
            if (recentNAICS.rejected_naics_codes != null && recentNAICS.rejected_naics_titles != null)
            {
                for (int i = 0; i < recentNAICS.rejected_naics_codes.Count; i++)
                {
                    naicsRecentData = new NAICSRecentData();
                    naicsRecentData.naics_codes = recentNAICS.rejected_naics_codes[i];
                    naicsRecentData.naics_titles = recentNAICS.rejected_naics_titles[i];
                    naicsRecentData.naics_status = "";
                    naicsRecentData.usr_nm = strUserName;
                    ltRecentNAICSUpdate.Add(naicsRecentData);
                }
            }

            recentNAICS.usr_nm = strUserName;
            //searchInput.srch_user_name = User.Identity.Name;

            string strRecentNAICSUpdatePath = string.Empty;
            strRecentNAICSUpdatePath = ConfigurationManager.AppSettings["TopOrgsRecentNAICSUpdateFilePath"];


            try
            {
                var RecentNAICSUpdate = Utilities.readJSONTFromFile<List<NAICSRecentData>>(strRecentNAICSUpdatePath);
                var filteredRecentNAICSUpdate = RecentNAICSUpdate; // This is to filter out the recent searches based on the username

                if (RecentNAICSUpdate != null)
                {
                    //if (!string.IsNullOrEmpty(User.Identity.Name))
                    //{
                    //    filteredRecentSearch = RecentSearch.Where(x => x.srch_user_name == User.Identity.Name).ToList();
                    //    RecentSearch.RemoveAll(x => x.srch_user_name == User.Identity.Name);
                    //}
                    filteredRecentNAICSUpdate = RecentNAICSUpdate.Where(x => x.usr_nm == strUserName).Distinct().ToList();
                    RecentNAICSUpdate.RemoveAll(x => x.usr_nm == strUserName);
                    int counter = 0;
                    foreach (NAICSRecentData n in ltRecentNAICSUpdate)
                    {
                        filteredRecentNAICSUpdate.Insert(counter, n);
                        counter++;
                    }

                    if (filteredRecentNAICSUpdate.Count >= 11)
                    {
                        filteredRecentNAICSUpdate.RemoveRange(10, filteredRecentNAICSUpdate.Count - 10); // Remove all items greater than 9, since we show only top 10 recent searches
                    }
                    //filteredRecentNAICSUpdate.Insert(0, naicsRecentData);
                    RecentNAICSUpdate.AddRange(filteredRecentNAICSUpdate);
                    var SuccessMessage = Utilities.writeJSONIntoFile<List<NAICSRecentData>>(RecentNAICSUpdate, strRecentNAICSUpdatePath);
                    if (SuccessMessage.ToLower() == "success")
                        strMessage = "Top Orgs Recent NAICS Update are logged successfully";
                    else
                        return strMessage;
                }
                else
                {
                    //ltRecentNAICSUpdate.Add(naicsRecentData);
                    var SuccessMessage = Utilities.writeJSONIntoFile<List<NAICSRecentData>>(ltRecentNAICSUpdate, strRecentNAICSUpdatePath);
                    if (SuccessMessage.ToLower() == "success")
                        strMessage = "Top Orgs Recent NAICS Update are logged successfully";
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

        public JsonResult GetTopOrgsRecentNAICSUpdate()
        {
            string strRecentSearchPath = string.Empty;
            strRecentSearchPath = ConfigurationManager.AppSettings["TopOrgsRecentNAICSUpdateFilePath"];

            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);

            var JSON = Utilities.readJSONTFromFile<List<NAICSRecentData>>(strRecentSearchPath).Distinct();
            var filteredJSON = JSON;
            if (JSON != null)
                filteredJSON = JSON.Where(x => x.usr_nm == strUserName).GroupBy(x => x.naics_codes).Select(y => y.First()).ToList();
            //filteredJSON = JSON.Where(x => x.srch_user_name == User.Identity.Name).ToList();
            return Json(filteredJSON, JsonRequestBehavior.AllowGet);
        }

        //*************************************Download NAICS Suggestions*****************************************
        [HandleException]
        [HttpPost]
        /* Method name: DownloadNAICSSuggestionsExcel
        * Input Parameters: An object of SearchInput class
        * Purpose: This method Download NAICSSuggestions in Excel file*/
        [TabLevelSecurity("topaccount_tb_access", "RW", "R")]
        public async Task<ActionResult> DownloadNAICSSuggestionsExcel(TopOrgsSearchInput searchInput)
        {
            TopAccountViewModel viewModel = new TopAccountViewModel();
            viewModel.SearchInput = searchInput;
            string strFileName = @"NAICSSuggestions_Template.xlsx";
            string FilePath = HttpContext.Server.MapPath("~/App/Shared/Files/" + strFileName);
            FileInfo file = new FileInfo(FilePath);
            string res = string.Empty;
            // res = (new JavaScriptSerializer()).Serialize(viewModel);
            string url = BaseURL + "api/accountmonitoring/toporgssearch/";
            searchInput.AnswerSetLimit = "2000";
            res = await InvokeWebService.PostResourceAsync(url, Token, searchInput, ClientID);
            checkExceptions(res);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                List<TopAccountSearchResults> searchList = JsonConvert.DeserializeObject<List<TopAccountSearchResults>>(res);
                List<ExportSearchResults> exportResult = new List<ExportSearchResults>();
                foreach (TopAccountSearchResults srch in searchList)
                {
                    if ((srch.listNAICSCodes != null) && (searchInput.listRuleKeyword != null))
                    {
                        if ((srch.listNAICSCodes.Count > 0) && (searchInput.listRuleKeyword.Count > 0))
                        {
                            for (int i = 0; i < srch.listNAICSCodes.Count; i++)
                            {
                                string NAICS_Code = (!string.IsNullOrEmpty(srch.listNAICSCodes[i].strText) ? srch.listNAICSCodes[i].strText : "");
                                string NAICS_Title = (!string.IsNullOrEmpty(srch.listNAICSDesc[i].strText) ? srch.listNAICSDesc[i].strText : "");
                                string NAICS_Keyword = (!string.IsNullOrEmpty(srch.listNAICSRuleKeyword[i].strText) ? srch.listNAICSRuleKeyword[i].strText : "");
                                string NAICS_Status = (!string.IsNullOrEmpty(srch.listNAICSDesc[i].status) ? srch.listNAICSDesc[i].status : "");
                                ExportSearchResults exp = createExportSearchResults(srch, NAICS_Code, NAICS_Title, NAICS_Keyword, NAICS_Status);
                                exportResult.Add(exp);
                            }
                        }
                    }

                    else
                    {
                        if (srch.listNAICSCodes.Count > 0)
                        {
                            for (int i = 0; i < srch.listNAICSCodes.Count; i++)
                            {
                                string NAICS_Code = (!string.IsNullOrEmpty(srch.listNAICSCodes[i].strText) ? srch.listNAICSCodes[i].strText : "");
                                string NAICS_Title = (!string.IsNullOrEmpty(srch.listNAICSDesc[i].strText) ? srch.listNAICSDesc[i].strText : "");
                                string NAICS_Keyword = (!string.IsNullOrEmpty(srch.listNAICSRuleKeyword[i].strText) ? srch.listNAICSRuleKeyword[i].strText : "");
                                string NAICS_Status = (!string.IsNullOrEmpty(srch.listNAICSDesc[i].status) ? srch.listNAICSDesc[i].status : "");
                                ExportSearchResults exp = createExportSearchResults(srch, NAICS_Code, NAICS_Title, NAICS_Keyword, NAICS_Status);
                                exportResult.Add(exp);
                            }
                        }
                        else
                        {
                            string NAICS_Code = "";
                            string NAICS_Title = "";
                            string NAICS_Keyword = "";
                            string NAICS_Status = "";
                            ExportSearchResults exp = createExportSearchResults(srch, NAICS_Code, NAICS_Title, NAICS_Keyword, NAICS_Status);
                            exportResult.Add(exp);
                        }
                    }


                }
                // var exportData = exportResult.OrderBy(x => x.NAICS_Title).ThenBy(x => x.NAICS_Match_Keyword).ToList();
                var exportData = exportResult.ToList();
                worksheet.Cells["A2"].LoadFromCollection(exportData, false);
                return new FileContentResult(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
        public ExportSearchResults createExportSearchResults(TopAccountSearchResults srch, string strNAICSCode, string strNAICSTitle, string strNAICSKeyword, string strNAICSStatus)
        {
            ExportSearchResults expRes = new ExportSearchResults();
            expRes.Master_ID = srch.master_id;
            expRes.Name = srch.name;
            expRes.Address = srch.address;
            expRes.NAICS_Code = strNAICSCode;
            expRes.NAICS_Title = strNAICSTitle;
            expRes.NAICS_Match_Keyword = strNAICSKeyword;
            expRes.NAICS_Status = strNAICSStatus;
            expRes.Action = "";
            return expRes;
        }

        /* ************************ Stuart Details methods ************************ */
        /* Method name: GetConstituentAddress
            * Input Parameters: An object of StuartDetails.DetailsInput class
            * Purpose: This method Gets Constituent Address*/
        [HandleException]
        [Route("GetConstituentAddress/{id}")]
        [TabLevelSecurity("topaccount_tb_access", "RW", "R")]
        public async Task<JsonResult> GetConstituentAddress(StuartDetails.DetailsInput input)
        {

            if (string.IsNullOrWhiteSpace(input.cnst_mstr_id))
                return Json("NoID");

            string url = BaseURL + "api/Constituent/GetConstituentAddress/" + input.cnst_mstr_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                serializer = new JavaScriptSerializer();
                List<StuartDetails.Address> list = serializer.Deserialize<List<StuartDetails.Address>>(res);
                List<StuartDetails.Address> listWithActiveInd = new List<StuartDetails.Address>();
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        listWithActiveInd = list.Where(x => x.act_ind == "1").ToList();
                        foreach (StuartDetails.Address indv in listWithActiveInd)
                        {
                            indv.sel_arc_srcsys_cd = input.arc_srcsys_cd;
                            indv.sel_cnst_srcsys_id = input.cnst_srcsys_id;
                        }
                    }
                }
                Process<StuartDetails.Address> process = new Process<StuartDetails.Address>();
                List<StuartDetails.Address> convertedList = process.convertRecords(listWithActiveInd, "Address");

                return Json(convertedList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        /* Method name: GetConstituentPhone
         * Input Parameters: An object of StuartDetails.DetailsInput class
         * Purpose: This method Gets Constituent Phone numbers*/
        [HandleException]
        [Route("GetConstituentPhone/{id}")]
        [TabLevelSecurity("topaccount_tb_access", "RW", "R")]
        public async Task<JsonResult> GetConstituentPhone(StuartDetails.DetailsInput input)
        {

            if (string.IsNullOrWhiteSpace(input.cnst_mstr_id))
                return Json("NoID");
            serializer = new JavaScriptSerializer();
            string url = BaseURL + "api/Constituent/GetConstituentPhone/" + input.cnst_mstr_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<StuartDetails.Phone> list = serializer.Deserialize<List<StuartDetails.Phone>>(res);
                List<StuartDetails.Phone> listWithActiveInd = new List<StuartDetails.Phone>();

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        listWithActiveInd = list.Where(x => x.act_ind == "1").ToList();
                        foreach (StuartDetails.Phone indv in listWithActiveInd)
                        {
                            indv.sel_arc_srcsys_cd = input.arc_srcsys_cd;
                            indv.sel_cnst_srcsys_id = input.cnst_srcsys_id;
                        }
                    }
                }
                Process<StuartDetails.Phone> process = new Process<StuartDetails.Phone>();
                List<StuartDetails.Phone> convertedList = process.convertRecords(listWithActiveInd, "Phone");
                return Json(convertedList, JsonRequestBehavior.AllowGet);
            }


            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        /* Method name: GetConstituentOrgName
        * Input Parameters: An object of StuartDetails.DetailsInput class
        * Purpose: This method Gets Constituent Org Name*/
        [HandleException]
        [Route("GetConstituentOrgName/{id}")]
        [TabLevelSecurity("topaccount_tb_access", "RW", "R")]
        public async Task<JsonResult> GetConstituentOrgName(StuartDetails.DetailsInput input)
        {

            if (string.IsNullOrWhiteSpace(input.cnst_mstr_id))
                return Json("NoID");
            serializer = new JavaScriptSerializer();
            string url = BaseURL + "api/Constituent/GetConstituentOrgName/" + input.cnst_mstr_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<StuartDetails.OrgName> nameList = serializer.Deserialize<List<StuartDetails.OrgName>>(res);
                List<StuartDetails.OrgName> nameListWithActiveInd = new List<StuartDetails.OrgName>();
                if (nameList != null)
                {
                    if (nameList.Count > 0)
                    {
                        nameListWithActiveInd = nameList.Where(x => x.act_ind == "1").ToList();
                        foreach (StuartDetails.OrgName indv in nameListWithActiveInd)
                        {
                            indv.sel_arc_srcsys_cd = input.arc_srcsys_cd;
                            indv.sel_cnst_srcsys_id = input.cnst_srcsys_id;
                        }
                    }
                }

                Process<StuartDetails.OrgName> process = new Process<StuartDetails.OrgName>();
                List<StuartDetails.OrgName> convertedNameList = process.convertRecords(nameListWithActiveInd, "Name");
                return Json(convertedNameList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }


        class Process<T>
        {
            private string type;
            private string trans_status;
            private string is_previous;
            private string arc_srcsys_cd;
            private string transaction_key;
            private string user_id;
            private string transNotes;
            private string inactive_ind;
            public Process()
            {
                this.type = string.Empty;
                this.trans_status = string.Empty;
                this.is_previous = string.Empty;
                this.arc_srcsys_cd = string.Empty;
                this.transaction_key = string.Empty;
                this.user_id = string.Empty;
                this.transNotes = string.Empty;
                this.inactive_ind = string.Empty;
            }

            public List<T> convertRecords(List<T> records, string type)
            {
                List<T> newRecords = new List<T>();

                if (type == "Name")
                {
                    foreach (T record in records)
                    {
                        Object objectRecord = record;
                        StuartDetails.OrgName orgName = (StuartDetails.OrgName)objectRecord;
                        this.trans_status = orgName.trans_status != null ? orgName.trans_status : string.Empty;
                        this.is_previous = orgName.is_previous != null ? orgName.is_previous : "0";
                        this.arc_srcsys_cd = orgName.arc_srcsys_cd != null ? orgName.arc_srcsys_cd : string.Empty;
                        this.user_id = orgName.user_id != null ? orgName.user_id : string.Empty;
                        this.transaction_key = orgName.transaction_key != null ? orgName.transaction_key : string.Empty;
                        this.inactive_ind = orgName.inactive_ind != null ? orgName.inactive_ind : string.Empty;
                        orgName.transNotes = getMessage();
                    }
                }
                else if (type == "Address")
                {
                    foreach (T record in records)
                    {
                        Object objectRecord = record;
                        StuartDetails.Address obj = (StuartDetails.Address)objectRecord;
                        this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                        this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                        this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                        this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                        this.transaction_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                        this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                        obj.transNotes = getMessage();
                    }
                }
                else if (type == "Phone")
                {
                    foreach (T record in records)
                    {
                        Object objectRecord = record;
                        StuartDetails.Phone obj = (StuartDetails.Phone)objectRecord;
                        this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                        this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                        this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                        this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                        this.transaction_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                        this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                        obj.transNotes = getMessage();


                        if (!obj.transNotes.ToLower().Contains("deleted"))
                        {
                            newRecords.Add(record);
                        }

                    }
                    records = newRecords;
                }
                return records;
            }


            private string getMessage()
            {

                transNotes = string.Empty;

                if (inactive_ind.Equals("-1") && !arc_srcsys_cd.Equals("CDIM"))
                {
                    if (type.Equals("FSARelationship"))
                    {
                        transNotes = "Only active chapter relationships can be edited/removed.";
                    }
                    else
                    {
                        transNotes = "Inactive/Soft-Deleted records cannot be edited.";
                    }
                }
                else if (arc_srcsys_cd.Equals("CDIM") && !type.Equals("FSARelationship"))
                {
                    //if (type.Equals(Constant.CONSTANTS["CHARACTERISTICS"]))
                    //{
                    //    transNotes = "BB Records cannot be edited.";
                    //}
                    //else
                    //{
                    if (trans_status.Equals("In Progress") || inactive_ind.Equals("-1"))
                    {
                        transNotes = "This LN Record is soft deleted.";
                    }
                    else
                    {
                        transNotes = "LN Records cannot be edited.";
                    }
                    //}
                }
                else if (trans_status.Equals("Waiting for approval") || trans_status.Equals("In Progress"))
                {
                    if (is_previous.Equals("1"))
                    {
                        transNotes = "Transaction #" + transaction_key + ". Previous record in a change request by " + user_id;
                    }
                    else
                    {
                        transNotes = "Transaction #" + transaction_key + ". New record in a change request by " + user_id;
                    }
                }




                return transNotes;
            }
        }

        /* ************************ Generic method to capture exceptions ************************ */
        private JsonResult handleTrivialHttpRequests1(string res)
        {
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
                var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        /* Code by naga */
        private JsonResult handleTrivialHttpRequests(string res)
        {
            if (!res.Equals("") && res != null)
            {
                // var result = JsonConvert.DeserializeObject<WebServiceStatus>(res);   
                // var JObj =(new JavaScriptSerializer()).DeserializeObject(res) as Dictionary<string,object>;
                //object x = null;
                //var result = new WebServiceStatus();
                //foreach (var k in JObj)
                //{
                //    if (JObj.TryGetValue(k.Key, out x))
                //    {
                //        if (x.GetType() == typeof(WebServiceStatus))
                //        {
                //            result = (WebServiceStatus)x;
                //            //t.HasValue = true;
                //        }
                //    }
                //}
                var JObj = (new JavaScriptSerializer()).DeserializeObject(res);
                if (JObj is Array)
                {
                    var results = (new JavaScriptSerializer()).DeserializeObject(res);
                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                var result = JsonConvert.DeserializeObject<WebServiceStatus>(res);
                if (result != null && result.Status != null)
                {
                    var status = result.Status;

                    if (status.ToLower().Contains("timedout"))
                    {
                        throw new CustomExceptionHandler(Json("TimedOut"));
                    }
                    else if (status.ToLower().Contains("error"))
                    {
                        throw new CustomExceptionHandler(Json("DatabaseError"));
                    }
                    else if (status.ToLower().Contains("unauthorized"))
                    {
                        throw new CustomExceptionHandler(Json("Unauthorized"));
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result1, JsonRequestBehavior.AllowGet);
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

        // ********************** Upload NAICS Suggestions function****************************************
        /* Method name: UploadNAICSSuggestions
       * Input Parameters: An object of ListUploadNAICSSuggestionsInput class
       * Purpose: This method Upload NAICS Suggestions excel file data to the server*/
        [TabLevelSecurity("topaccount_tb_access", "RW")]
        public async Task<ActionResult> UploadNAICSSuggestions(ListUploadNAICSSuggestionsInput searchInput)
        {
            NAICSSuggestionsJsonFileDetailsHelper jsonhelper = new NAICSSuggestionsJsonFileDetailsHelper();
            int iUploadedCnt = 0;
            string fileName = "";
            string res = string.Empty;
            List<string> listFileNames = new List<string>();
            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);
            // string strUserName = "dixit.jain";

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            ListUploadNAICSSuggestionsInput lgl = new ListUploadNAICSSuggestionsInput();
            lgl.NAICSSuggestionsInputList = new List<UploadNAICSSuggestionsInput>();
            List<NAICSSuggestionsJsonFileDetailsHelper> UploadStatusFileDetails = new List<NAICSSuggestionsJsonFileDetailsHelper>();
            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    var serializer = new JavaScriptSerializer();

                    string strFileName = "";
                    double intFileSize = 0;
                    string strDocType = "";
                    string strDocExtention = "";
                    DateTime dateUploadStart = DateTime.Now;
                    DateTime dateUploadEnd = DateTime.Now;
                    bool UploadFlag = true;
                    if (listFileNames.Contains(hpf.FileName.ToString()))
                    {
                        UploadFlag = false;
                    }
                    else
                    {
                        listFileNames.Add(hpf.FileName.ToString());
                    }
                    // get document details
                    strFileName = hpf.FileName; //file name
                    intFileSize = hpf.ContentLength;
                    strDocExtention = System.IO.Path.GetExtension(hpf.FileName);// file extention

                    if (UploadFlag)    // If the file is not a duplicate, perform below actions
                    {
                        fileName = Path.GetFileName(hpf.FileName);
                        UploadNAICSSuggestionsFileInfo uploadFileinput = new UploadNAICSSuggestionsFileInfo();
                        uploadFileinput.fileName = fileName;
                        uploadFileinput.intFileSize = hpf.ContentLength;
                        uploadFileinput.fileExtension = System.IO.Path.GetExtension(hpf.FileName);
                        using (var excel = new ExcelPackage(hpf.InputStream))
                        {
                            ExcelWorksheet workSheet = excel.Workbook.Worksheets.First();
                            var ws = excel.Workbook.Worksheets[workSheet.Name];
                            var lastRow = ws.Dimension.End.Row;
                            while (lastRow >= 1)
                            {
                                var range = ws.Cells[lastRow, 1, lastRow, 8];
                                if (range.Any(c => c.Value != null))
                                {
                                    break; //If they contain any data
                                }
                                lastRow--;
                            }

                            if (lastRow > 2000)
                            {
                                var result = new JsonResult();
                                result.Data = "FileLimitExceeded||" + lastRow + "||" + fileName; // Return a Excel Upload Limit Exceeded
                                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                return result;
                            }

                            else
                            {
                                try
                                {
                                    //Read the file into memory
                                    int headerCol = 1;

                                    Dictionary<string, int> map = Enumerable
                                        .Range(ws.Dimension.Start.Column, 8 - ws.Dimension.Start.Column + 1)
                                        .ToDictionary(col => ws.Cells[headerCol, col].Value.ToString().Trim(), col => col);

                                    var keys = new[] { "Master ID", "Name", "Address", "NAICS Code", "NAICS Title", "NAICS Match Keyword", "NAICS Status", "Action" };

                                    //Check for the template
                                    bool check = keys.All(map.ContainsKey) && map.Keys.All(keys.Contains) && map.Keys.Count() == keys.Count();

                                    if (check)
                                    {

                                        for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                                        {
                                            if (!ws.Cells[rw, 1, rw, 8].All(c => c.Value == null))
                                            {
                                                lgl.NAICSSuggestionsInputList.Add(new UploadNAICSSuggestionsInput()
                                                {

                                                    cnst_mstr_id = (ws.Cells[rw, map["Master ID"]].Value ?? (Object)"").ToString(),
                                                    cnst_org_nm = (ws.Cells[rw, map["Name"]].Value ?? (Object)"").ToString(),
                                                    cnst_org_addrs = (ws.Cells[rw, map["Address"]].Value ?? (Object)"").ToString(),
                                                    naics_cd = (ws.Cells[rw, map["NAICS Code"]].Value ?? (Object)"").ToString(),
                                                    naics_title = (ws.Cells[rw, map["NAICS Title"]].Value ?? (Object)"").ToString(),
                                                    naics_map_rule_key = (ws.Cells[rw, map["NAICS Match Keyword"]].Value ?? (Object)"").ToString(),
                                                    sts = (ws.Cells[rw, map["NAICS Status"]].Value ?? (Object)"").ToString(),
                                                    action = (ws.Cells[rw, map["Action"]].Value ?? (Object)"").ToString(),
                                                    user_id = strUserName, //User.Identity.Name,
                                                });

                                            }
                                        }
                                    }
                                    else
                                    {
                                        var result = new JsonResult();
                                        result.Data = "Invalid Data"; // Return a FileAlread
                                        result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                        return result;
                                    }
                                }
                                catch (Exception e)
                                {
                                    var result = new JsonResult();
                                    result.Data = "Invalid Data"; // Return a FileAlread
                                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                    return result;
                                }
                            }
                            iUploadedCnt = iUploadedCnt + 1; //Increase the count by 1
                            if (lgl.NAICSSuggestionsInputList.Count < 2001) //Check for the uploaded list count
                            {
                                dateUploadEnd = DateTime.Now;
                                strDocType = "Excel";
                                jsonhelper.FileName = strFileName;
                                jsonhelper.FileSize = intFileSize;
                                jsonhelper.FileType = strDocType;
                                jsonhelper.FileExtention = strDocExtention;
                                jsonhelper.UploadStart = dateUploadStart.ToString();
                                jsonhelper.UploadEnd = dateUploadEnd.ToString();
                                jsonhelper.TransactionKey = "";
                                UploadStatusFileDetails.Add(jsonhelper);
                                TempData["UploadJsonDetails"] = UploadStatusFileDetails;
                            }
                            if (lgl.NAICSSuggestionsInputList.Count == 0)
                            {
                                var result = new JsonResult();
                                result.Data = "EmptyFileUploaded"; // Return a FileAlread
                                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                return result;
                            }
                        }
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                List<NAICSSuggestionsJsonFileDetailsHelper> _uploadJsonDetails = (List<NAICSSuggestionsJsonFileDetailsHelper>)TempData["UploadJsonDetails"];
                NAICSSuggestionsDetails gm = new NAICSSuggestionsDetails();
                gm.ListUploadNAICSSuggestionsDetails = lgl;
                gm.ListNAICSSuggestionsJsonFileDetailsInput = _uploadJsonDetails;
                if (gm == null) return null;
                var result = new JsonResult();
                string url = BaseURL + "api/accountmonitoring/uploadnaicssuggestions/";
                res = await InvokeWebService.PostResourceAsync(url, Token, gm, ClientID);

                NAICSSuggestionsSubmitOutput _uploadJsonDetailsOutput = JsonConvert.DeserializeObject<NAICSSuggestionsSubmitOutput>(res);
                Mail em = new Mail();
                string strEmailMessage = string.Empty;
                string strSubject = string.Empty;
                if (res != null)
                {
                    checkExceptions(res);
                    if (_uploadJsonDetailsOutput.insertOutput.ToLower() == "rejections")
                    {
                        strSubject = "Upload Naics Suggestions - Warning";
                        strEmailMessage = "The File, " + gm.ListNAICSSuggestionsJsonFileDetailsInput[0].FileName + " has been uploaded to the server. One or more records from the uploaded file have been rejected. Transaction key generated for this upload is " + _uploadJsonDetailsOutput.transactionKey + ". You can review the rejections using the Transaction tab of the Orgler web application.";
                    }
                    else if (_uploadJsonDetailsOutput.insertOutput.ToLower() == "none")
                    {
                        strSubject = "Upload Naics Suggestions - Warning";
                        strEmailMessage = "The File, " + gm.ListNAICSSuggestionsJsonFileDetailsInput[0].FileName + " has been uploaded to the server, but no records were processed. Transaction key generated for this upload is " + _uploadJsonDetailsOutput.transactionKey + ". ";
                    }
                    if (_uploadJsonDetailsOutput.insertOutput.ToLower() == "success")
                    {
                        strSubject = "Upload Naics Suggestions - Success";
                        strEmailMessage = "The File, " + gm.ListNAICSSuggestionsJsonFileDetailsInput[0].FileName + " has been uploaded to the server successfully!. Transaction key generated for this upload is " + _uploadJsonDetailsOutput.transactionKey + ". ";
                    }
                }
                else
                {
                    strSubject = "Upload Naics Suggestions - Failure";
                    strEmailMessage = "The File, " + gm.ListNAICSSuggestionsJsonFileDetailsInput[0].FileName + " has failed to upload.";
                }

                //input.ListUploadNAICSSuggestionsDetails.NAICSSuggestionsInputList[0].user_id = "dixit.jain";
                string uploadedUserDomain = (System.Configuration.ConfigurationManager.AppSettings["UploadedUserDomain"] ?? "").ToString();
                em.sendUploadStatusMail(strSubject, strEmailMessage, gm.ListUploadNAICSSuggestionsDetails.NAICSSuggestionsInputList[0].user_id + uploadedUserDomain);
                /* if (res != null)
                {
                    NAICSSuggestionsSubmitOutput _uploadJsonDetailsOutput = JsonConvert.DeserializeObject<NAICSSuggestionsSubmitOutput>(res);                   
                    Mail em = new Mail();
                    string strEmailMessage = "The File, " + gm.ListNAICSSuggestionsJsonFileDetailsInput[0].FileName + " has been uploaded to the server successfully!. Transaction key generated for this upload is " + _uploadJsonDetailsOutput.transactionKey + ". ";
                    //input.ListUploadNAICSSuggestionsDetails.NAICSSuggestionsInputList[0].user_id = "dixit.jain";
                    string uploadedUserDomain = (System.Configuration.ConfigurationManager.AppSettings["UploadedUserDomain"] ?? "").ToString();
                    em.sendUploadStatusMail(strEmailMessage, gm.ListUploadNAICSSuggestionsDetails.NAICSSuggestionsInputList[0].user_id + uploadedUserDomain);
                } */
            }
            return Json(res, JsonRequestBehavior.AllowGet);

        }
    }
}