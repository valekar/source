using Newtonsoft.Json.Linq;
using Orgler.Models.Entities.Case;
using Orgler.Models.Entities.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Orgler.Models;
using OfficeOpenXml;
using Orgler.Models.Entities.Constituents;
using Newtonsoft.Json;
using Orgler.Models.Entities.Transaction;
using Orgler.Exceptions;

namespace Orgler.Controllers.MVC
{
    [Authorize]
    public class TransactionNativeController : BaseController
    {
        // GET: Case
        public ActionResult Index()
        {
            return View();
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> AdvSearch(ListTransactionSearchInputModel postData)
        {

                string url = BaseURL + "api/Transaction/TransactionSearch";
                string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
                return handleTrivialHttpRequests(res);
            
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("transaction_tb_access", "RW")]
        public async Task<JsonResult> AssociateCase(TransactionCaseAssociationInput postData)
        {

                string url = BaseURL + "api/Transaction/UpdateTransactionCaseAssociation";
                string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
                return handleTrivialHttpRequests(res);
            
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("transaction_tb_access", "RW")]
        public async Task<JsonResult> CreateTransaction(JObject postData)
        {

                    string url = BaseURL + "api/Case/createcase";
                    string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("is_approver", "1")]
        public async Task<JsonResult> PostUpdateTransactionStatus(TransactionStatusUpdateInput postData)
        {

                    string url = BaseURL + "api/Transaction/UpdateTransactionStatus";
                    string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("is_approver", "1")]
        public async Task<JsonResult> PostUnmergeTransactionResearch(TransactionStatusUpdateInput postData)
        {
            //Dummy Server Call Check
            string url = BaseURL + "api/Transaction/UnmergeTransactionResearch";
            string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("is_approver", "1")]
        public async Task<JsonResult> getMergeResearchResultData(MasterDetailsInput masterInput)
        {

                string url = BaseURL + "api/constituent/getcomparedata/";
                string res = await Models.Resource.PostResourceAsync(url, Token, masterInput, ClientID);
                return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("getconstituentbirth/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> getconstituentbirth(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetBirthTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
                    
        }

        [HandleException]
        [Route("gettransorgaffiliator/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransorgaffiliator(string id)
        {
                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetOrgAffiliatorTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransunmergerequest/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransunmergerequest(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetUnmergeRequestLogTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransunmergeprocess/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransunmergeprocess(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetUnmergeProcessLogTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransdeath/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransdeath(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetDeathTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettranspersonname/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettranspersonname(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetPersonNameTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransorgname/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransorgname(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetOrgNameTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransaddress/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransaddress(string id)
        {

                
                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetAddressTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransaffiliatorhierarchy/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransaffiliatorhierarchy(string id)
        {


                try
                {
                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetAffiliatorHierarchyTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    var result = (new JavaScriptSerializer()).DeserializeObject(res);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    // log.Info(ex.Message + ((ex.InnerException != null) ? ex.InnerException.ToString() : ""));
                }
                return Json("Error");
            
        }

        [HandleException]
        [Route("gettransaffiliatortags/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransaffiliatortags(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetAffiliatorTagsTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettranscharacteristics/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettranscharacteristics(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetCharacteristicsTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettranscontactpref/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettranscontactpref(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetContactPreferenceTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransemail/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransemail(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetEmailTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransemailupload/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransemailupload(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetUploadDetailsTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }


        [HandleException]
        [Route("gettransallupload/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransallupload(string id)
        {

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Transaction/GetUploadDetailsTransactionDetails/" + id;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransmerge/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransmerge(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetMergeTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransgroupmembership/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransgroupmembership(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetGroupMembershipTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransorgtransform/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransorgtransform(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetOrgTransformationsTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("gettransphone/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<JsonResult> gettransphone(string id)
        {

                    if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
                    string url = BaseURL + "api/Transaction/GetPhoneTransactionDetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("transactionDetailsExport/{id}")]
        public async Task<FileContentResult> transactionDetailsExport(ExportDetailInput ExportDetailInput)
        {
            JavaScriptSerializer serializer;
            serializer = new JavaScriptSerializer();

            string url = "";
            string url2 = "";
            switch (ExportDetailInput.sub_trans_typ_dsc.ToUpper())
            {
                case "DATE OF BIRTH": url = BaseURL + "api/Transaction/GetBirthTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "UNMERGE": url = BaseURL + "api/Transaction/GetUnmergeRequestLogTransactionDetails/" + ExportDetailInput.trans_key;
                    url2 = BaseURL + "api/Transaction/GetUnmergeProcessLogTransactionDetails/" + ExportDetailInput.trans_key;
                    break;
                case "ORGANIZATION AFFILIATOR": url = BaseURL + "api/Transaction/GetOrgAffiliatorTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "DATE OF DEATH": url = BaseURL + "api/Transaction/GetDeathTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "PERSON NAME": url = BaseURL + "api/Transaction/GetPersonNameTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "ORGANIZATION NAME": url = BaseURL + "api/Transaction/GetOrgNameTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "ADDRESS": url = BaseURL + "api/Transaction/GetAddressTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "AFFILIATOR HIERARCHY": url = BaseURL + "api/Transaction/GetAffiliatorHierarchyTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "AFFILIATOR TAGS": url = BaseURL + "api/Transaction/GetAffiliatorTagsTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "AFFILIATOR UPLOAD": url = BaseURL + "api/Transaction/GetAffiliatorTagsUploadTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "CHARACTERISTICS": url = BaseURL + "api/Transaction/GetCharacteristicsTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "CONTACT PREFERENCE": url = BaseURL + "api/Transaction/GetContactPreferenceTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "EMAIL": url = BaseURL + "api/Transaction/GetEmailTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "NAME AND EMAIL UPLOAD":
                case "EMAIL-ONLY UPLOAD":
                case "GROUP MEMBERSHIP UPLOAD": url = BaseURL + "api/Transaction/GetUploadDetailsTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "GROUP MEMBERSHIP": url = BaseURL + "api/Transaction/GetGroupMembershipTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "ORGANIZATION TRANSFORMATIONS": url = BaseURL + "api/Transaction/GetOrgTransformationsTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "MERGE": url = BaseURL + "api/Transaction/GetMergeTransactionDetails/" + ExportDetailInput.trans_key; break;
                case "PHONE": url = BaseURL + "api/Transaction/GetPhoneTransactionDetails/" + ExportDetailInput.trans_key; break;
            }

            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            string res2 = null;
            //if (ExportDetailInput.sub_trans_typ_dsc == "Person Name")
            //{
            //    res2 = await Models.Resource.GetResourceAsync(url2, Token, ClientID);
            //}

            if (ExportDetailInput.sub_trans_typ_dsc == "Unmerge")
            {
                res2 = await Models.Resource.GetResourceAsync(url2, Token, ClientID);
            }

            using (ExcelPackage package = new ExcelPackage())
            {
                int wsRow = 1;
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("TransactionDetails");
                switch (ExportDetailInput.sub_trans_typ_dsc.ToUpper())
                {
                    case "DATE OF BIRTH": List<TransactionBirth> listDOB = serializer.Deserialize<List<TransactionBirth>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listDOB, true);
                        break;
                    case "UNMERGE":
                        int intNextPointer = 2;
                        List<TransactionUnmergeRequestLog> listUnmergeProcessLog = serializer.Deserialize<List<TransactionUnmergeRequestLog>>(res);
                        List<TransactionUnmergeProcessLog> listUnmergeRequestLog = serializer.Deserialize<List<TransactionUnmergeProcessLog>>(res2);
                        worksheet.Cells["A1"].Value = "Unmerge Request Log";
                        worksheet.Cells["A2"].LoadFromCollection(listUnmergeRequestLog, true);
                        intNextPointer += listUnmergeRequestLog.Count + 1;
                        string strExcelSectionHeaderCurrentPointerValue = "A" + intNextPointer.ToString();
                        string strExcelSectionBlockCurrentPointerValue = "A" + (intNextPointer+1).ToString();

                        worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Unmerge Process Log";
                        worksheet.Cells[strExcelSectionBlockCurrentPointerValue].LoadFromCollection(listUnmergeProcessLog, true);

                        //worksheet.Cells["A2"].LoadFromCollection(listUnmergeProcessLog, true);
                        break;
                    case "ORGANIZATION AFFILIATOR":
                        List<TransactionOrgAffiliator> listOrgAffil = serializer.Deserialize<List<TransactionOrgAffiliator>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listOrgAffil, true);
                        break;
                    case "DATE OF DEATH": List<TransactionDeath> listDOD = serializer.Deserialize<List<TransactionDeath>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listDOD, true);
                        break;
                    case "PERSON NAME": List<TransactionPersonName> listNames = serializer.Deserialize<List<TransactionPersonName>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listNames, true);
                        break;
                    case "ORGANIZATION NAME": List<TransactionOrgName> listOrgNames = serializer.Deserialize<List<TransactionOrgName>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listOrgNames, true);
                        break;
                    case "ADDRESS": List<TransactionAddress> listAddress = serializer.Deserialize<List<TransactionAddress>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listAddress, true);
                        break;
                    case "AFFILIATOR HIERARCHY": List<TransactionAffiliatorHierarchy> listAffilHierarchy = serializer.Deserialize<List<TransactionAffiliatorHierarchy>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listAffilHierarchy, true);
                        break;
                    case "AFFILIATOR TAGS": List<TransactionAffiliatorTags> listAffTags = serializer.Deserialize<List<TransactionAffiliatorTags>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listAffTags, true);
                        break;
                    case "AFFILIATOR UPLOAD": List<TransactionAffiliatorTagsUpload> listAffUpload = serializer.Deserialize<List<TransactionAffiliatorTagsUpload>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listAffUpload, true);
                        break;
                    case "CHARACTERISTICS": List<TransactionCharacteristics> listCharacteristics = serializer.Deserialize<List<TransactionCharacteristics>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listCharacteristics, true);
                        break;
                    case "CONTACT PREFERENCE": List<TransactionContactPreference> listContactPreference = serializer.Deserialize<List<TransactionContactPreference>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listContactPreference, true);
                        break;
                    case "EMAIL": List<TransactionEmail> listEmails = serializer.Deserialize<List<TransactionEmail>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listEmails, true);
                        break;
                    case "NAME AND EMAIL UPLOAD":
                    case "EMAIL-ONLY UPLOAD":
                    case "GROUP MEMBERSHIP UPLOAD": List<TransactionUploadDetails> listUpload = serializer.Deserialize<List<TransactionUploadDetails>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listUpload, true);
                        break;
                    case "GROUP MEMBERSHIP": List<TransactionGroupMembership> listGroupMembership = serializer.Deserialize<List<TransactionGroupMembership>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listGroupMembership, true);
                        break;
                    case "ORGANIZATION TRANSFORMATIONS": List<TransactionOrgTransformations> listOrgTransform = serializer.Deserialize<List<TransactionOrgTransformations>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listOrgTransform, true);
                        break;
                    case "MERGE": List<TransactionMerge> listMerge = serializer.Deserialize<List<TransactionMerge>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listMerge, true);
                        break;
                    case "PHONE": List<TransactionPhone> listPhones = serializer.Deserialize<List<TransactionPhone>>(res);
                        worksheet.Cells["A2"].LoadFromCollection(listPhones, true);
                        break;
                }
                return new FileContentResult(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

        private JsonResult handleTrivialHttpRequests(string res)
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


    }
}