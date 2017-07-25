using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Orgler.Exceptions;
using OfficeOpenXml;
using System.Security.Principal;
using Orgler.Security;
using Orgler.Models.Entities.Constituents;
using Orgler.Services;
using Orgler.Data.Entities.Constituents;
using Orgler.Models;
using Orgler.Models.Entities.Constituent;
using Orgler.Models.Common;
using Newtonsoft.Json;

namespace Orgler.Controllers
{
    [RouteArea("Test")]
    [RoutePrefix("")]
    [Authorize]
    public class TestController : BaseController
    {
        //  public static sealed TestController testInstance = new TestController();

        JavaScriptSerializer serializer;

        public TestController()
        {

            //Constant consts = Constant.getInstance();
            serializer = new JavaScriptSerializer();

        }


        // GET: Test
        public ActionResult Index()
        {

            return View();
        }


        ////This method implements the logging mechanism when the user attempts to login
        //public async Task<JsonResult> insertLoginHistory(LoginHistoryInput LoginHistoryInput)
        //{
        //    string url = BaseURL + "api/Login/insertloginhistory/";
        //    string res = await Models.Resource.PostResourceAsync(url, Token, LoginHistoryInput, ClientID);
        //    var result = (new JavaScriptSerializer()).DeserializeObject(res);
        //    return Json(result);
        //}

        ////This method implements the logging mechanism when the user attempts to login
        //public async Task<JsonResult> insertTabLevelSecurity(TabLevelSecurityParams TablevelSecurity)
        //{
        //    string url = BaseURL + "api/Login/addUserTabLevelSecurity/";
        //    string res = await Models.Resource.PostResourceAsync(url, Token, TablevelSecurity, ClientID);
        //    var result = (new JavaScriptSerializer()).DeserializeObject(res);
        //    return Json(result);
        //}

        [HandleException]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> ConstituentARCBest(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) id = Request["id"] ?? "";
            string url = BaseURL + "api/Constituent/GetARCBest/" + id;
            //var res = await Models.Resource.GetResourceAsync<object>(url, Token, ClientID);
            //return Json(res);
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);
        }

        /* Method name: Search
       * Input Parameters: An object of ConstituentSearchModel class
       * Purpose: This method search results for constituent */
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> search(ConstituentSearchModel postData)
        {

            string url = BaseURL + "api/Constituent/search/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, postData, ClientID);
            return handleTrivialHttpRequests(res);

        }

        /* Method name: advsearch
       * Input Parameters: An object of ListConstituentInputSearchModel class
       * Purpose: This method search results for constituent */
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> advsearch(ListConstituentInputSearchModel postData)
        {
            postData.AnswerSetLimit = "50";
            string url = BaseURL + "api/Constituent/advsearch/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, postData, ClientID);
            return handleTrivialHttpRequests(res);
        }

        /* Method name: exportAdvSearchExcel
       * Input Parameters: An object of ListConstituentInputSearchModel class
       * Purpose: This method exports search result excel file data to the server */
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<FileContentResult> exportAdvSearchExcel(ListConstituentInputSearchModel postData)
        {

            postData.AnswerSetLimit = "2000";
            string url = BaseURL + "api/Constituent/advsearch/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, postData, ClientID);
            checkExceptions(res);
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Search Results");
                List<ConstituentOutputSearchResults> searchList = serializer.Deserialize<List<ConstituentOutputSearchResults>>(res);
                worksheet.Cells["A1"].LoadFromCollection(searchList, true);
                return new FileContentResult(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }


        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addconstituentpersonname(ConstituentNameInput ConstNameInput)
        {

            string url = BaseURL + "api/Constituent/addconstituentpersonname/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstNameInput, ClientID);
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteconstituentpersonname(ConstituentNameInput ConstNameInput)
        {

            string url = BaseURL + "api/Constituent/deleteconstituentpersonname/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstNameInput, ClientID);
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> updateconstituentpersonname(ConstituentNameInput ConstNameInput)
        {

            string url = BaseURL + "api/Constituent/updateconstituentpersonname/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstNameInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addconstituentorgname(ConstituentOrgNameInput ConstOrgNameInput)
        {

            string url = BaseURL + "api/Constituent/addconstituentorgname/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstOrgNameInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteconstituentorgname(ConstituentOrgNameInput ConstOrgNameInput)
        {

            string url = BaseURL + "api/Constituent/deleteconstituentorgname/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstOrgNameInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> updateconstituentorgname(ConstituentOrgNameInput ConstOrgNameInput)
        {

            string url = BaseURL + "api/Constituent/updateconstituentorgname/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstOrgNameInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }


        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addconstituentphone(ConstituentPhoneInput ConstPhoneInput)
        {

            string url = BaseURL + "api/Constituent/addconstituentphone/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstPhoneInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteconstituentphone(ConstituentPhoneInput ConstPhoneInput)
        {

            string url = BaseURL + "api/Constituent/deleteconstituentphone/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstPhoneInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> updateconstituentphone(ConstituentPhoneInput ConstPhoneInput)
        {

            string url = BaseURL + "api/Constituent/updateconstituentphone/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstPhoneInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }

        /* Create, Delete and Update API's for Email*/
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addconstituentemail(ConstituentEmailInput ConstEmailInput)
        {

            string url = BaseURL + "api/Constituent/addconstituentemail/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstEmailInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteconstituentemail(ConstituentEmailInput ConstEmailInput)
        {

            string url = BaseURL + "api/Constituent/deleteconstituentemail/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstEmailInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> updateconstituentemail(ConstituentEmailInput ConstEmailInput)
        {

            string url = BaseURL + "api/Constituent/updateconstituentemail/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstEmailInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }

        /* Create, Delete and Update API's for Birth*/
        [HttpPost]
        [HandleException]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addconstituentbirth(ConstituentBirthInput ConstBirthInput)
        {

            string url = BaseURL + "api/Constituent/addconstituentbirth/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstBirthInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteconstituentbirth(ConstituentBirthInput ConstBirthInput)
        {

            string url = BaseURL + "api/Constituent/deleteconstituentbirth/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstBirthInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> updateconstituentbirth(ConstituentBirthInput ConstBirthInput)
        {

            string url = BaseURL + "api/Constituent/updateconstituentbirth/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstBirthInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }

        /* Create, Delete and Update API's for Death*/
        [HttpPost]
        [HandleException]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addconstituentdeath(ConstituentDeathInput ConstDeathInput)
        {

            string url = BaseURL + "api/Constituent/addconstituentdeath/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstDeathInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteconstituentdeath(ConstituentDeathInput ConstDeathInput)
        {

            string url = BaseURL + "api/Constituent/deleteconstituentdeath/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstDeathInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> updateconstituentdeath(ConstituentDeathInput ConstDeathInput)
        {

            string url = BaseURL + "api/Constituent/updateconstituentdeath/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstDeathInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }

        /* Create, Delete and Update API's for Address*/
        [HttpPost]
        [HandleException]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addconstituentaddress(ConstituentAddressInput ConstAddressInput)
        {

            string url = BaseURL + "api/Constituent/addconstituentaddress/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstAddressInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteconstituentaddress(ConstituentAddressInput ConstAddressInput)
        {

            string url = BaseURL + "api/Constituent/deleteconstituentaddress/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstAddressInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
             return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> updateconstituentaddress(ConstituentAddressInput ConstAddressInput)
        {

            string url = BaseURL + "api/Constituent/updateconstituentaddress/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstAddressInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }

        /* Create, Delete and Update API's for Contact Preference*/
        [HttpPost]
        [HandleException]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addconstituentcontactprefc(ConstituentContactPrefcInput ConstContactPrefcInput)
        {

            string url = BaseURL + "api/Constituent/addconstituentcontactprefc/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstContactPrefcInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
             return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteconstituentcontactprefc(ConstituentContactPrefcInput ConstContactPrefcInput)
        {

            string url = BaseURL + "api/Constituent/deleteconstituentcontactprefc/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstContactPrefcInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> updateconstituentcontactprefc(ConstituentContactPrefcInput ConstContactPrefcInput)
        {

            string url = BaseURL + "api/Constituent/updateconstituentcontactprefc/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstContactPrefcInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }

        /* Create, Delete and Update API's for Group Membership*/
        [HttpPost]
        [HandleException]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addconstituentchaptergrp(ConstituentChapterGroupInput ConstChapterGroupInput)
        {

            string url = BaseURL + "api/Constituent/addconstituentchaptergrp/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstChapterGroupInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteconstituentchaptergrp(ConstituentChapterGroupInput ConstChapterGroupInput)
        {

            string url = BaseURL + "api/Constituent/deleteconstituentchaptergrp/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstChapterGroupInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> updateconstituentchaptergrp(ConstituentChapterGroupInput ConstChapterGroupInput)
        {

            string url = BaseURL + "api/Constituent/updateconstituentchaptergrp/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstChapterGroupInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
             return Json(result);*/
            return handleTrivialHttpRequests(res);

        }

        /* Create, Delete and Update API's for Characteristics*/
        [HttpPost]
        [HandleException]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addconstituentcharacteristics(ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {

            string url = BaseURL + "api/Constituent/addconstituentcharacteristics/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstCharacteristicsInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteconstituentcharacteristics(ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {

            string url = BaseURL + "api/Constituent/deleteconstituentcharacteristics/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstCharacteristicsInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> updateconstituentcharacteristics(ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {

            string url = BaseURL + "api/Constituent/updateconstituentcharacteristics/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, ConstCharacteristicsInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentcomparedata(MasterDetailsInput masterInput)
        {

            string url = BaseURL + "api/constituent/getcomparedata/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, masterInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("has_merge_unmerge_access", "1")]
        public async Task<JsonResult> postmergedata(Orgler.Data.Entities.Constituents.MergeInput MergeInput)
        {
            MergeInput.UserName = User.GetUserName();
            // IPrincipal p = HttpContext.Current.User;
            // MergeInput.UserName = p.Identity.Name;
            string url = BaseURL + "api/constituent/mergemaster/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, MergeInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("has_merge_unmerge_access", "1")]
        public async Task<JsonResult> postmergeconflictdata(Orgler.Data.Entities.Constituents.MergeConflictInput MergeInput)
        {

            MergeInput.UserName = User.GetUserName();
            /* if (!Models.Entities.SessionUtils.strUserName.Equals("") || Models.Entities.SessionUtils.strUserName.Length > 0)
             {
                 MergeInput.UserName = Models.Entities.SessionUtils.strUserName;
             }*/
            string url = BaseURL + "api/constituent/mergeconflicts/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, MergeInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("has_merge_unmerge_access", "1")]
        public async Task<JsonResult> postunmergedata(Orgler.Data.Entities.Constituents.UnmergeInput MergeInput)
        {
            MergeInput.UserName = User.GetUserName();
            /*  if (!Models.Entities.SessionUtils.strUserName.Equals("") || Models.Entities.SessionUtils.strUserName.Length > 0)
              {
                  MergeInput.UserName = Models.Entities.SessionUtils.strUserName;
              }*/
            string url = BaseURL + "api/constituent/unmerge/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, MergeInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addorgaffiliators(OrgAffiliatorsInput orgAffiliatorInput)
        {


            string url = BaseURL + "api/Constituent/addorgaffiliators/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, orgAffiliatorInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteorgaffiliators(OrgAffiliatorsInput orgAffiliatorInput)
        {

            string url = BaseURL + "api/Constituent/deleteorgaffiliators/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, orgAffiliatorInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }

        /*
         * The below methods are api's calls to each section of details
         * */
        [HandleException]
        [Route("GetConstituentPersonName/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetConstituentPersonName(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");

            string url = BaseURL + "api/Constituent/GetConstituentPersonName/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<Name> nameList = serializer.Deserialize<List<Name>>(res);
                Process<Name> process = new Process<Name>();
                List<Name> convertedNameList = process.convertRecords(nameList, Constant.CONSTANTS["CONST_NAME"]);
                //var result = (new JavaScriptSerializer()).DeserializeObject(res);

                return Json(convertedNameList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        [HandleException]
        [Route("GetConstituentMasterDetails/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetConstituentMasterDetails(string id)
        {


            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");

            string url = BaseURL + "api/Constituent/GetConstituentMasterDetails/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
        return Json(result);*/
            return handleTrivialHttpRequests(res);



        }

        //org name
        [HandleException]
        [Route("GetConstituentOrgName/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetConstituentOrgName(string id)
        {
            if (id == "undefined")
            {
                id = "0";
            }
            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");

            string url = BaseURL + "api/Constituent/GetConstituentOrgName/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<OrgName> nameList = serializer.Deserialize<List<OrgName>>(res);
                Process<OrgName> process = new Process<OrgName>();
                List<OrgName> convertedNameList = process.convertRecords(nameList, Constant.CONSTANTS["CONST_ORG_NAME"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedNameList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }





        }
        [HandleException]
        [Route("GetConstituentAddress/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetConstituentAddress(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/GetConstituentAddress/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<Address> list = serializer.Deserialize<List<Address>>(res);
                Process<Address> process = new Process<Address>();
                List<Address> convertedList = process.convertRecords(list, Constant.CONSTANTS["CONST_ADDRESS"]);
                //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }



        }
        [HandleException]
        [Route("GetConstituentEmail/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetConstituentEmail(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/GetConstituentEmail/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<Email> list = serializer.Deserialize<List<Email>>(res);
                Process<Email> process = new Process<Email>();
                List<Email> convertedList = process.convertRecords(list, Constant.CONSTANTS["CONST_EMAIL"]);
                //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }




        }
        [HandleException]
        [Route("GetConstituentPhone/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetConstituentPhone(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/GetConstituentPhone/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<Phone> list = serializer.Deserialize<List<Phone>>(res);
                Process<Phone> process = new Process<Phone>();
                List<Phone> convertedList = process.convertRecords(list, Constant.CONSTANTS["CONST_PHONE"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }


        }

        [HandleException]
        [Route("getconstituentbirth/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentbirth(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentbirth/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<Birth> list = serializer.Deserialize<List<Birth>>(res);
                Process<Birth> process = new Process<Birth>();
                List<Birth> convertedList = process.convertRecords(list, Constant.CONSTANTS["CONST_BIRTH"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }


        }
        [HandleException]
        [Route("getconstituentcharacteristics/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentcharacteristics(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentcharacteristics/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<Characteristics> characterList = serializer.Deserialize<List<Characteristics>>(res);
                Process<Characteristics> process = new Process<Characteristics>();
                List<Characteristics> convertedCharacterList = process.convertRecords(characterList, Constant.CONSTANTS["CHARACTERISTICS"]);
                return Json(convertedCharacterList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }


        }
        [HandleException]
        [Route("getconstituentcontactpreference/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetConstituentContactPreference(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentcontactpreference/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<ContactPreference> list = serializer.Deserialize<List<ContactPreference>>(res);
                Process<ContactPreference> process = new Process<ContactPreference>();
                List<ContactPreference> convertedList = process.convertRecords(list, Constant.CONSTANTS["CONST_PREF"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }


        }
        [HandleException]
        [Route("getconstituentdeath/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentdeath(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentdeath/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<Death> list = serializer.Deserialize<List<Death>>(res);
                Process<Death> process = new Process<Death>();
                List<Death> convertedList = process.convertRecords(list, Constant.CONSTANTS["CONST_DEATH"]);
                //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }

        }
        [HandleException]
        [Route("getconstituentexternalbridge/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentexternalbridge(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentexternalbridge/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
        return Json(result);*/
            // return handleTrivialHttpRequests(res);


            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<ExternalBridge> list = serializer.Deserialize<List<ExternalBridge>>(res);
                Process<ExternalBridge> process = new Process<ExternalBridge>();
                List<ExternalBridge> convertedList = process.convertRecords(list, Constant.CONSTANTS["CONST_EXT_BRIDGE"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }



        }
        [HandleException]
        [Route("getconstituentgroupmembership/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentgroupmembership(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentgroupmembership/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<GroupMembership> list = serializer.Deserialize<List<GroupMembership>>(res);
                Process<GroupMembership> process = new Process<GroupMembership>();
                List<GroupMembership> convertedList = process.convertRecords(list, Constant.CONSTANTS["GRP_MEMBERSHIP"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }


        }
        [HandleException]
        [Route("getconstituentinternalbridge/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentinternalbridge(string id)
        {


            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentinternalbridge/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);



        }
        [HandleException]
        [Route("getconstituentmasteringattempts/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentmasteringattempts(string id)
        {


            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentmasteringattempts/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);



        }
        [HandleException]
        [Route("getconstituentmergehistory/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentmergehistory(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentmergehistory/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [Route("getconstituentrelationship/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentrelationship(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentrelationship/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
             return Json(result);*/
            return handleTrivialHttpRequests(res);



        }
        [HandleException]
        [Route("getconstituentsummary/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentsummary(string id)
        {


            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentsummary/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);



        }
        [HandleException]
        [Route("getconstituenttransactionhistory/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituenttransactionhistory(string id)
        {


            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituenttransactionhistory/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);



        }

        [HandleException]
        [Route("getconstituentorgaffiliators/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentorgaffiliators(string id)
        {


            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentorgaffiliators/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<OrgAffiliators> list = serializer.Deserialize<List<OrgAffiliators>>(res);
                Process<OrgAffiliators> process = new Process<OrgAffiliators>();
                List<OrgAffiliators> convertedList = process.convertRecords(list, Constant.CONSTANTS["AFFILIATOR"]);
                //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }



        }
        [HandleException]
        [Route("getconstituentprivateinformation/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentprivateinformation(string id)
        {


            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentprivateinformation/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);


        }
        [HandleException]
        [Route("getconstituentoldmasters/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentoldmasters(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentoldmasters/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
             return Json(result);*/
            return handleTrivialHttpRequests(res);



        }




        [HandleException]
        [Route("getconstituentanonymousinformation/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentanonymousinformation(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/getconstituentanonymousinformation/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
              return Json(result);*/
            return handleTrivialHttpRequests(res);



        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> showbirthdetails(ShowDetailsInput showInput)
        {

            string url = BaseURL + "api/Constituent/showbirthdetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, showInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> showpersonnamedetails(ShowDetailsInput showInput)
        {

            string url = BaseURL + "api/Constituent/showpersonnamedetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, showInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> showorgnamedetails(ShowDetailsInput showInput)
        {

            string url = BaseURL + "api/Constituent/showorgnamedetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, showInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> showaddressdetails(ShowDetailsInput showInput)
        {

            string url = BaseURL + "api/Constituent/showaddressdetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, showInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> showphonedetails(ShowDetailsInput showInput)
        {

            string url = BaseURL + "api/Constituent/showphonedetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, showInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> showemaildetails(ShowDetailsInput showInput)
        {

            string url = BaseURL + "api/Constituent/showemaildetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, showInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> showdeathdetails(ShowDetailsInput showInput)
        {

            string url = BaseURL + "api/Constituent/showdeathdetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, showInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> showcontactpreferencedetails(ShowDetailsInput showInput)
        {

            string url = BaseURL + "api/Constituent/showcontactpreferencedetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, showInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> showcharacteristicsdetails(ShowDetailsInput showInput)
        {

            string url = BaseURL + "api/Constituent/showcharacteristicsdetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, showInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> showexternalbridgedetails(ShowDetailsInput showInput)
        {

            string url = BaseURL + "api/Constituent/showexternalbridgedetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, showInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> showinternalbridgedetails(ShowDetailsInput showInput)
        {
            string url = BaseURL + "api/Constituent/showinternalbridgedetails/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, showInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result);*/
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("getPotentialMergesAccountDetails/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getPotentialMergesAccountDetails(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/accountmonitoring/getpotentialmergedetails/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
        return Json(result);*/
            // return handleTrivialHttpRequests(res);


            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<PotentialMergeOutput> list = serializer.Deserialize<List<PotentialMergeOutput>>(res);
                Process<PotentialMergeOutput> process = new Process<PotentialMergeOutput>();
                List<PotentialMergeOutput> convertedList = process.convertRecords(list, Constant.CONSTANTS["CONST_POTENTIALMERGE"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }



        }

        [HandleException]
        [Route("constituentDetailsExport/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<FileContentResult> constituentDetailsExport(string id)
        {
            string url = BaseURL + "api/Constituent/GetConstituentOrgName/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            //check exceptions before proceeding further
            checkExceptions(res);
            List<OrgName> listNames = serializer.Deserialize<List<OrgName>>(res);

            url = BaseURL + "api/Constituent/GetConstituentAddress/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<Address> listAddresses = serializer.Deserialize<List<Address>>(res);

            url = BaseURL + "api/Constituent/GetConstituentPhone/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<Phone> listPhones = serializer.Deserialize<List<Phone>>(res);

            url = BaseURL + "api/Constituent/GetConstituentEmail/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<Email> listEmails = serializer.Deserialize<List<Email>>(res);

            url = BaseURL + "api/Constituent/GetARCBest/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<ARCBest> listArcBest = serializer.Deserialize<List<ARCBest>>(res);

            url = BaseURL + "api/Constituent/getconstituentbirth/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<Birth> listBirth = serializer.Deserialize<List<Birth>>(res);

            url = BaseURL + "api/Constituent/getconstituentdeath/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<Death> listDeath = serializer.Deserialize<List<Death>>(res);

            url = BaseURL + "api/Constituent/getconstituentcharacteristics/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<Characteristics> listCharacteristics = serializer.Deserialize<List<Characteristics>>(res);

            url = BaseURL + "api/Constituent/getconstituentcontactpreference/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<ContactPreference> listcontactPreference = serializer.Deserialize<List<ContactPreference>>(res);

            url = BaseURL + "api/Constituent/getconstituentexternalbridge/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<ExternalBridge> listextBridge = serializer.Deserialize<List<ExternalBridge>>(res);

            url = BaseURL + "api/Constituent/getconstituentinternalbridge/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<InternalBridge> listintBridge = serializer.Deserialize<List<InternalBridge>>(res);

            url = BaseURL + "api/Constituent/getconstituentgroupmembership/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<GroupMembership> listgroupMembership = serializer.Deserialize<List<GroupMembership>>(res);

            //For the new sections
            url = BaseURL + "api/Org/GetEmailDomain/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<OrgEmailDomain> listEmailDomain = serializer.Deserialize<List<OrgEmailDomain>>(res);

            url = BaseURL + "api/Org/GetOrgNAICS/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<OrgNaics> listNaicsCode = serializer.Deserialize<List<OrgNaics>>(res);

            url = BaseURL + "api/Org/GetOrgContactsExport/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<Org_Contacts> listContacts = serializer.Deserialize<List<Org_Contacts>>(res);

            url = BaseURL + "api/Org/GetSourceSystemAlternateIds/" + id;
            res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            checkExceptions(res);
            List<AlternateIds> listSourceSystemAlternateIds = serializer.Deserialize<List<AlternateIds>>(res);

            int intExcelCurrentPointerValue = 2;
            string strExcelSectionHeaderCurrentPointerValue = "A1";
            string strExcelBlockCurrentPointerValue = "A2";
            using (ExcelPackage package = new ExcelPackage())
            {
                int wsRow = 1;
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Constituent Details");
                worksheet.Cells["A1"].Value = "Name";
                worksheet.Cells["A2"].LoadFromCollection(listNames, true);

                intExcelCurrentPointerValue += listNames.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Addresses";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listAddresses, true);

                intExcelCurrentPointerValue += listAddresses.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Email";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listEmails, true);

                intExcelCurrentPointerValue += listEmails.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Phone";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listPhones, true);

                intExcelCurrentPointerValue += listPhones.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Email";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listEmails, true);

                intExcelCurrentPointerValue += listEmails.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Arc Best";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listArcBest, true);

                intExcelCurrentPointerValue += listArcBest.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                /*worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Birth";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listBirth, true);

                intExcelCurrentPointerValue += listBirth.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Death";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listDeath, true);

                intExcelCurrentPointerValue += listDeath.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString(); */

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Characteristics";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listCharacteristics, true);

                intExcelCurrentPointerValue += listCharacteristics.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Contact Preference";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listcontactPreference, true);

                intExcelCurrentPointerValue += listcontactPreference.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "External Bridge";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listextBridge, true);

                intExcelCurrentPointerValue += listextBridge.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Internal Bridge";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listintBridge, true);

                intExcelCurrentPointerValue += listintBridge.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Group Memberships";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listgroupMembership, true);

                //For Exporting the new section in Constituent
                intExcelCurrentPointerValue += listgroupMembership.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Email Domains";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listEmailDomain, true);

                intExcelCurrentPointerValue += listEmailDomain.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Naics Code";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listNaicsCode, true);

                intExcelCurrentPointerValue += listNaicsCode.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Contacts";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listContacts, true);

                intExcelCurrentPointerValue += listContacts.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Alternate Ids";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listSourceSystemAlternateIds, true);

                intExcelCurrentPointerValue += listSourceSystemAlternateIds.Count + 2;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                return new FileContentResult(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

        }

        //For surfacing the new section in Constituent      
        [HandleException]
        [Route("GetEmailDomains/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetEmailDomains(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");

            string url = BaseURL + "api/Org/GetEmailDomain/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<OrgEmailDomain> emailDomainList = serializer.Deserialize<List<OrgEmailDomain>>(res);
                Process<OrgEmailDomain> process = new Process<OrgEmailDomain>();
                List<OrgEmailDomain> convertedemailDomainList = process.convertRecords(emailDomainList, Constant.CONSTANTS["EMAIL_DOMAINS"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedemailDomainList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HandleException]
        [Route("GetNaicsCodes/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetNaicsCodes(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");

            string url = BaseURL + "api/Org/GetOrgNAICS/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<OrgNaics> naicsCodesList = serializer.Deserialize<List<OrgNaics>>(res);
                Process<OrgNaics> process = new Process<OrgNaics>();
                List<OrgNaics> convertednaicsCodesListList = process.convertRecords(naicsCodesList, Constant.CONSTANTS["NAICS_CODES"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertednaicsCodesListList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HandleException]
        [Route("GetContacts/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetContacts(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");

            string url = BaseURL + "api/Org/GetOrgContacts/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<Org_Contacts> contactsList = serializer.Deserialize<List<Org_Contacts>>(res);
                Process<Org_Contacts> process = new Process<Org_Contacts>();
                List<Org_Contacts> convertedcontactsList = process.convertRecords(contactsList, Constant.CONSTANTS["CONTACTS_DETAILS"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedcontactsList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        [HandleException]
        [Route("GetAlternateIds/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetAlternateIds(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");

            string url = BaseURL + "api/Org/GetSourceSystemAlternateIds/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<AlternateIds> alternateIdsList = serializer.Deserialize<List<AlternateIds>>(res);
                Process<AlternateIds> process = new Process<AlternateIds>();
                List<AlternateIds> convertedalternateIdsList = process.convertRecords(alternateIdsList, Constant.CONSTANTS["CONTACTS_DETAILS"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedalternateIdsList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        [HandleException]
        [Route("GetRFMValues/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> GetRFMValues(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");

            string url = BaseURL + "api/Org/GetRFMValues/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<RFMValues> alternateIdsList = serializer.Deserialize<List<RFMValues>>(res);
                Process<RFMValues> process = new Process<RFMValues>();
                List<RFMValues> convertedalternateIdsList = process.convertRecords(alternateIdsList, Constant.CONSTANTS["RFM_DETAILS"]);
                // var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(convertedalternateIdsList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HandleException]
        [Route("GetOrgRelationship/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getOrgRelationship(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Constituent/GetOrgRelationship/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);



        }


        /* Method name: getMasterNAICSDetails
               * Input Parameters: Master Id
               * Purpose: This method gets the master naics details*/

        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<ActionResult> getMasterNAICSDetails(NAICSStatusUpdateInput postData)
        {


            string url = BaseURL + "api/Org/GetOrgNAICS/" + postData.cnst_mstr_id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);
        }
        /* Method name: SubmitNAICSCodeUpdates
         * Input Parameters: An object of UpdateNAICSCodes class
         * Purpose: This method Updates Naics Code*/
        [TabLevelSecurity("newaccount_tb_access", "RW")]
        public async Task<ActionResult> SubmitNAICSCodeUpdates(NAICSStatusUpdateInput updateInput)
        {

            IPrincipal p = System.Web.HttpContext.Current.User;
            updateInput.usr_nm = Orgler.Security.Extentions.GetUserName(p);
            string url = BaseURL + "api/Org/naicsstatuschange/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, updateInput, ClientID);
            return handleTrivialHttpRequests(res);
        }
        public JsonResult GetNAICSCodesTreeGrid()
        {
            string strPath = string.Empty;
            strPath = ConfigurationManager.AppSettings["NAICSCodeMetadataFilePath"];

            var JSON = Utilities.readJSONTFromFile<List<NAICSCode>>(strPath);
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

        public string InsertConstituentRecentNAICS(NAICSRecentUpdate recentNAICS)
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
            strRecentNAICSUpdatePath = ConfigurationManager.AppSettings["ConstituentRecentNAICSUpdateFilePath"];


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

        public JsonResult GetConstituentRecentNAICSUpdate()
        {
            string strRecentSearchPath = string.Empty;
            strRecentSearchPath = ConfigurationManager.AppSettings["ConstituentRecentNAICSUpdateFilePath"];

            IPrincipal p = System.Web.HttpContext.Current.User;
            string strUserName = Orgler.Security.Extentions.GetUserName(p);

            var JSON = Utilities.readJSONTFromFile<List<NAICSRecentData>>(strRecentSearchPath);
            var filteredJSON = JSON;
            if (JSON != null)
                filteredJSON = JSON.Where(x => x.usr_nm == strUserName).GroupBy(x => x.naics_codes).Select(y => y.First()).ToList();
            //filteredJSON = JSON.Where(x => x.srch_user_name == User.Identity.Name).ToList();
            return Json(filteredJSON, JsonRequestBehavior.AllowGet);
        }


        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addemaildomain(OrgEmailDomainAddInput orgEmailDomainInput)
        {
            IPrincipal p = System.Web.HttpContext.Current.User;
            orgEmailDomainInput.UserName = Orgler.Security.Extentions.GetUserName(p);
            string url = BaseURL + "api/Org/addconstituentemaildomain/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, orgEmailDomainInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
           return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteemaildomain(OrgEmailDomainDeleteInput orgEmailDomainDeleteInput)
        {
            IPrincipal p = System.Web.HttpContext.Current.User;
            orgEmailDomainDeleteInput.UserName = Orgler.Security.Extentions.GetUserName(p);
            string url = BaseURL + "api/Org/deleteconstituentemaildomain/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, orgEmailDomainDeleteInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);

        }
        public JsonResult GetEmailDomainList()
        {
            string strPath = string.Empty;
            strPath = ConfigurationManager.AppSettings["EmailDomainFilePath"];

            var JSON = Utilities.readJSONTFromFile<List<string>>(strPath);
            JSON.Sort();
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

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


        private string getUserName(string UserID)
        {
            if (UserID.Contains("\\"))
            {
                int index = UserID.IndexOf("\\");
                return UserID.Substring(index + 1);
            }
            return UserID;
        }
    }





    // 
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
        private string distinct_count;
        private string pKey;
        // private string row_stat_cd;

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
            //   this.row_stat_cd = string.Empty;
        }


        public List<T> convertRecords(List<T> records, string type)
        {
            List<T> newRecords = new List<T>();
            if (type.Equals(Constant.CONSTANTS["CONST_NAME"]))
            {
                HashSet<string> distinctNameRecords = new HashSet<string>();
                int max = -1;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Name name = (Name)objectRecord;
                    this.trans_status = name.trans_status != null ? name.trans_status : string.Empty;
                    this.is_previous = name.is_previous != null ? name.is_previous : "0";
                    this.arc_srcsys_cd = name.arc_srcsys_cd != null ? name.arc_srcsys_cd : string.Empty;
                    this.user_id = name.user_id != null ? name.user_id : string.Empty;
                    this.transaction_key = name.transaction_key != null ? name.transaction_key : string.Empty;
                    this.inactive_ind = name.inactive_ind != null ? name.inactive_ind : string.Empty;

                    name.transNotes = getMessage();

                    if (name.locator_prsn_nm_key != null)
                    {
                        if (!name.locator_prsn_nm_key.Equals("") && !name.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                        {
                            distinctNameRecords.Add(name.locator_prsn_nm_key);

                            if (max < distinctNameRecords.Count)
                            {
                                max = distinctNameRecords.Count;
                                // Name myObj = (Name)(Object)records[0];
                                //myObj.distinct_count = max + "";
                            }
                        }
                    }

                }
                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Name myObj = (Name)objectRecord;
                    myObj.distinct_count = max + "";
                }

            }

            if (type.Equals(Constant.CONSTANTS["CONST_ORG_NAME"]))
            {
                HashSet<string> distinctRecords = new HashSet<string>();
                int max = -1;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    OrgName orgName = (OrgName)objectRecord;
                    this.trans_status = orgName.trans_status != null ? orgName.trans_status : string.Empty;
                    this.is_previous = orgName.is_previous != null ? orgName.is_previous : "0";
                    this.arc_srcsys_cd = orgName.arc_srcsys_cd != null ? orgName.arc_srcsys_cd : string.Empty;
                    this.user_id = orgName.user_id != null ? orgName.user_id : string.Empty;
                    this.transaction_key = orgName.transaction_key != null ? orgName.transaction_key : string.Empty;
                    this.inactive_ind = orgName.inactive_ind != null ? orgName.inactive_ind : string.Empty;
                    orgName.transNotes = getMessage();
                    if (orgName.cnst_org_nm != null)
                    {
                        if (!orgName.cnst_org_nm.Equals("") && !orgName.row_stat_cd.ToUpper().Equals("L") && !(transNotes.ToLower().Contains("deleted") && !transNotes.ToLower().Contains("soft deleted")))
                        {
                            distinctRecords.Add(orgName.cnst_org_nm);

                            if (max < distinctRecords.Count)
                            {
                                max = distinctRecords.Count;
                                // OrgName myObj = (OrgName)(Object)records[0];
                                //myObj.distinct_count = max + "";
                            }
                        }
                    }
                }

                foreach (T record in records)
                {
                    Object objectRecord = record;
                    OrgName orgName = (OrgName)objectRecord;
                    orgName.distinct_count = max + "";
                }

            }
            else if (type.Equals(Constant.CONSTANTS["CHARACTERISTICS"]))
            {
                HashSet<string> distinctRecords = new HashSet<string>();
                int max = -1;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Characteristics character = (Characteristics)objectRecord;
                    this.trans_status = character.trans_status != null ? character.trans_status : string.Empty;
                    this.is_previous = character.is_previous != null ? character.is_previous : "0";
                    this.arc_srcsys_cd = character.arc_srcsys_cd != null ? character.arc_srcsys_cd : string.Empty;
                    this.user_id = character.user_id != null ? character.user_id : string.Empty;
                    this.transaction_key = character.transaction_key != null ? character.transaction_key : string.Empty;
                    this.inactive_ind = character.inactive_ind != null ? character.inactive_ind : string.Empty;
                    character.transNotes = getMessage();

                    if (character.cnst_chrctrstc_typ_cd != null)
                    {
                        if (!character.cnst_chrctrstc_typ_cd.Equals("") && !character.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                        {
                            distinctRecords.Add(character.cnst_chrctrstc_typ_cd);

                            if (max < distinctRecords.Count)
                            {
                                max = distinctRecords.Count;
                                // Characteristics myObj = (Characteristics)(Object)records[0];
                                //myObj.distinct_count = max + "";
                            }
                        }
                    }

                }

                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Characteristics myObj = (Characteristics)objectRecord;
                    myObj.distinct_count = max + "";
                }

            }
            else if (type.Equals(Constant.CONSTANTS["CONST_BIRTH"]))
            {
                HashSet<string> distinctRecords = new HashSet<string>();
                int max = -1;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Birth obj = (Birth)objectRecord;
                    this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                    this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                    this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                    this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                    this.transaction_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                    this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                    obj.transNotes = getMessage();
                    string birthdate = obj.cnst_birth_mth_num + obj.cnst_birth_dy_num + obj.cnst_birth_yr_num;
                    if (birthdate != null)
                    {
                        if (!birthdate.Equals("") && !obj.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                        {
                            distinctRecords.Add(birthdate);

                            if (max < distinctRecords.Count)
                            {
                                max = distinctRecords.Count;
                                //Birth myObj = (Birth)(Object)records[0];
                                // myObj.distinct_count = max + "";
                            }
                        }
                    }
                }

                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Birth myObj = (Birth)objectRecord;
                    myObj.distinct_count = max + "";
                }

            }
            else if (type.Equals(Constant.CONSTANTS["CONST_ADDRESS"]))
            {
                HashSet<string> distinctRecords = new HashSet<string>();
                int max = -1;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Address obj = (Address)objectRecord;
                    this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                    this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                    this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                    this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                    this.transaction_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                    this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                    obj.transNotes = getMessage();
                    if (obj.locator_addr_key != null)
                    {
                        if (!obj.locator_addr_key.Equals("") && !obj.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                        {
                            distinctRecords.Add(obj.locator_addr_key);

                            if (max < distinctRecords.Count)
                            {
                                max = distinctRecords.Count;
                                // Address myObj = (Address)(Object)records[0];
                                // myObj.distinct_count = max + "";
                            }
                        }
                    }
                }

                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Address myObj = (Address)objectRecord;
                    myObj.distinct_count = max + "";
                }

            }
            else if (type.Equals(Constant.CONSTANTS["CONST_PREF"]))
            {
                HashSet<string> distinctRecords = new HashSet<string>();
                int max = -1;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    ContactPreference obj = (ContactPreference)objectRecord;
                    this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                    this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                    this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                    this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                    this.transaction_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                    this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                    obj.transNotes = getMessage();
                    string prefStr = obj.cntct_prefc_typ_cd + obj.cntct_prefc_val;
                    if (prefStr != null)
                    {
                        if (!prefStr.Equals("") && !obj.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                        {
                            distinctRecords.Add(prefStr);

                            if (max < distinctRecords.Count)
                            {
                                max = distinctRecords.Count;
                                //ContactPreference myObj = (ContactPreference)(Object)records[0];
                                //myObj.distinct_count = max + "";
                            }
                        }
                    }
                }

                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    ContactPreference myObj = (ContactPreference)objectRecord;
                    myObj.distinct_count = max + "";
                }

            }
            else if (type.Equals(Constant.CONSTANTS["CONST_DEATH"]))
            {
                HashSet<string> distinctDeathRecords = new HashSet<string>();
                int max = -1;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Death obj = (Death)objectRecord;
                    this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                    this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                    this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                    this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                    this.transaction_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                    this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                    obj.transNotes = getMessage();

                    if (obj.cnst_death_dt != null)
                    {
                        if (!obj.cnst_death_dt.Equals("") && !obj.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                        {
                            distinctDeathRecords.Add(obj.cnst_death_dt);

                            if (max < distinctDeathRecords.Count)
                            {
                                max = distinctDeathRecords.Count;
                                // Death myObj = (Death)(Object)records[0];
                                //  myObj.distinct_count = max + "";
                            }
                        }
                    }


                }

                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Death myObj = (Death)objectRecord;
                    myObj.distinct_count = max + "";
                }

            }
            else if (type.Equals(Constant.CONSTANTS["CONST_EMAIL"]))
            {
                HashSet<string> distinctEmails = new HashSet<string>();
                int max = 0;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Email obj = (Email)objectRecord;
                    this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                    this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                    this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                    this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                    this.transaction_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                    this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                    //this.pKey = obj.cnst_email_addr;
                    obj.transNotes = getMessage();

                    if (!obj.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                    {
                        distinctEmails.Add(obj.email_key);
                        // this.distinct_count = 

                        if (max < distinctEmails.Count)
                        {
                            max = distinctEmails.Count;
                            Email myObj = (Email)(Object)records[0];
                            myObj.distinct_count = max + "";
                        }
                    }


                }
                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Email myObj = (Email)objectRecord;
                    myObj.distinct_count = max + "";
                }
            }
            else if (type.Equals(Constant.CONSTANTS["GRP_MEMBERSHIP"]))
            {
                HashSet<string> distinctRecords = new HashSet<string>();
                int max = 0;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    GroupMembership obj = (GroupMembership)objectRecord;
                    this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                    this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                    // this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                    this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                    this.transaction_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                    this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                    obj.transNotes = getMessage();
                    string groupStr = obj.grp_cd + obj.grp_typ;
                    if (groupStr != null)
                    {
                        if (!groupStr.Equals("") && !obj.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                        {
                            distinctRecords.Add(groupStr);

                            if (max < distinctRecords.Count)
                            {
                                max = distinctRecords.Count;
                                GroupMembership myObj = (GroupMembership)(Object)records[0];
                                myObj.distinct_count = max + "";
                            }
                        }
                    }
                }

                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    GroupMembership myObj = (GroupMembership)objectRecord;
                    myObj.distinct_count = max + "";
                }
            }
            else if (type.Equals(Constant.CONSTANTS["CONST_PHONE"]))
            {
                HashSet<string> distinctRecords = new HashSet<string>();
                int max = 0;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Phone obj = (Phone)objectRecord;
                    this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                    this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                    this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                    this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                    this.transaction_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                    this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                    obj.transNotes = getMessage();

                    if (obj.locator_phn_key != null)
                    {
                        if (!obj.locator_phn_key.Equals("") && !obj.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                        {
                            distinctRecords.Add(obj.locator_phn_key);

                            if (max < distinctRecords.Count)
                            {
                                max = distinctRecords.Count;
                                Phone myObj = (Phone)(Object)records[0];
                                myObj.distinct_count = max + "";
                            }
                        }
                    }

                    if (!obj.transNotes.ToLower().Contains("deleted"))
                    {
                        newRecords.Add(record);
                    }

                }

                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Phone myObj = (Phone)objectRecord;
                    myObj.distinct_count = max + "";
                }

                records = newRecords;
            }
            else if (type.Equals(Constant.CONSTANTS["AFFILIATOR"]))
            {
                HashSet<string> distinctRecords = new HashSet<string>();
                int max = 0;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    OrgAffiliators obj = (OrgAffiliators)objectRecord;
                    this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                    this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                    // this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                    this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                    this.transaction_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                    this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                    obj.transNotes = getMessage();
                    if (obj.ent_org_id != null)
                    {
                        if (!obj.ent_org_id.Equals("") && !obj.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                        {
                            distinctRecords.Add(obj.ent_org_id);

                            if (max < distinctRecords.Count)
                            {
                                max = distinctRecords.Count;
                                // Death myObj = (Death)(Object)records[0];
                                // myObj.distinct_count = max + "";
                            }
                        }
                    }
                }


                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    OrgAffiliators myObj = (OrgAffiliators)objectRecord;
                    myObj.distinct_count = max + "";
                }

            }
            // this added for UAT request 353
            else if (type.Equals(Constant.CONSTANTS["CONST_EXT_BRIDGE"]))
            {

                foreach (T record in records)
                {
                    Object objectRecord = record;
                    ExternalBridge obj = (ExternalBridge)objectRecord;
                    this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                    //  this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                    // this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                    this.user_id = obj.user_id != null ? obj.user_id : "<username not available>";
                    this.transaction_key = obj.request_transaction_key != null ? obj.request_transaction_key : string.Empty;
                    // this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                    obj.transNotes = getMessage();
                }

            }
            else if (type.Equals(Constant.CONSTANTS["EMAIL_DOMAINS"]))
            {
                HashSet<string> distinctEmails = new HashSet<string>();
                int max = 0;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    OrgEmailDomain obj = (OrgEmailDomain)objectRecord;
                    //this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                    //this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                    //this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                    this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                    this.transaction_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                    this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                    //this.pKey = obj.cnst_email_addr;
                    obj.transNotes = getMessage();
                    if (!obj.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                    {
                        distinctEmails.Add(obj.email_domain_key);
                        // this.distinct_count = 

                        if (max < distinctEmails.Count)
                        {
                            max = distinctEmails.Count;
                            OrgEmailDomain myObj = (OrgEmailDomain)(Object)records[0];
                            myObj.distinct_count = max + "";
                        }
                    }


                }
                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    OrgEmailDomain myObj = (OrgEmailDomain)objectRecord;
                    myObj.distinct_count = max + "";
                }
            }
            //enterprise Orgs Tabs
            else if (type.Equals(Constant.CONSTANTS["ENT_ORG_CHARACTERISTICS"]))
            {
                HashSet<string> distinctRecords = new HashSet<string>();
                int max = -1;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Orgler.Models.EnterpriseAccount.Characteristics character = (Orgler.Models.EnterpriseAccount.Characteristics)objectRecord;
                    this.trans_status = character.trans_status != null ? character.trans_status : string.Empty;
                    this.is_previous = character.is_previous != null ? character.is_previous : "0";
                   
                    this.user_id = character.user_id != null ? character.user_id : string.Empty;
                    this.transaction_key = character.transaction_key != null ? character.transaction_key : string.Empty;
                    this.inactive_ind = character.inactive_ind != null ? character.inactive_ind : string.Empty;
                    character.transNotes = getMessage();

                    if (character.cnst_chrctrstc_typ_cd != null)
                    {
                        if (!character.cnst_chrctrstc_typ_cd.Equals("") && !character.row_stat_cd.ToUpper().Equals("L") && !transNotes.ToLower().Contains("deleted"))
                        {
                            distinctRecords.Add(character.cnst_chrctrstc_typ_cd);

                            if (max < distinctRecords.Count)
                            {
                                max = distinctRecords.Count;

                            }
                        }
                    }

                }

                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    Orgler.Models.EnterpriseAccount.Characteristics myObj = (Orgler.Models.EnterpriseAccount.Characteristics)objectRecord;
                    myObj.distinct_records_count = max + "";
                }
            }


            return records;
        }

        private string getDistinctCount(HashSet<string> distinctHaset)
        {
            //throw new NotImplementedException();
            int count = distinctHaset.Count;
            return count + "";
        }


        private void assignValues()
        {

        }


        private string getMessage()
        {

            transNotes = string.Empty;

            if (type.Equals(Constant.CONSTANTS["EMAIL_DOMAINS"]))
            {
                if (inactive_ind.Equals("-1"))
                {
                    transNotes = "Inactive/Soft-Deleted records cannot be edited.";
                }
            }

            else if (inactive_ind.Equals("-1") && !arc_srcsys_cd.Equals("CDIM"))
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
                if (type.Equals(Constant.CONSTANTS["CHARACTERISTICS"]))
                {
                    transNotes = "BB Records cannot be edited.";
                }
                else
                {
                    if (trans_status.Equals("In Progress") || inactive_ind.Equals("-1"))
                    {
                        transNotes = "This LN Record is soft deleted.";
                    }
                    else
                    {
                        transNotes = "LN Records cannot be edited.";
                    }
                }
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
}