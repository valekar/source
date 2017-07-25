using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using Orgler.Models.Login;
using Orgler.Models.Entities;
using Orgler.Security;
using Orgler.Models;
using Orgler.Models.Entities.Constituents;
using System.Web.Configuration;
using Orgler.Exceptions;
using Orgler.Models.Entities.Case;
using Orgler.Models.Entities.Transaction;
using System.Security.Principal;
using System.Web.Security;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Orgler.Controllers
{
    [Authorize]
    [AuthRoles("Admin", "User", "Writer")]
    public class HomeController : BaseController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        public static string OrglerCartItemsPath = WebConfigurationManager.AppSettings["OrglerCartItemsPath"];
        public static string OrglerUnmergeCartItemsPath = WebConfigurationManager.AppSettings["UnmergeOrglerCartItemsPath"];
        public static string MergeConflictsItems = WebConfigurationManager.AppSettings["MergeConflictsItemsPath"];

        public async Task<ActionResult> Index()
        {
            string userName = User.GetUserName();
            TabLevelSecurityParams tabParams = Utility.getUserPermissions(userName);
            if (TempData["FromLogin"] != null && TempData["FromLogin"].ToString() == "Y")
            {
                UpdateUserProfile usrProfile = new UpdateUserProfile();
                usrProfile.Token = Token;
                usrProfile.CleintId = ClientID;
                await usrProfile.updateUserProfile();
            }

            if (tabParams.constituent_tb_access != "N")
            {
                return RedirectToAction("Index", "Constituent");
            }
            else if (tabParams.newaccount_tb_access != "N")
            {
                return RedirectToAction("Index", "NewAccount");
            }
            else if (tabParams.topaccount_tb_access != "N")
            {
                return RedirectToAction("Index", "TopAccount");
            }
            else if (tabParams.transaction_tb_access != "N")
            {
                return RedirectToAction("Index", "Transaction");
            }
            else if (tabParams.enterprise_orgs_tb_access != "N")
            {
                return RedirectToAction("Index", "EnterpriseAccount");
            }
            else if (tabParams.upload_affil_tb_access == "RW" || tabParams.upload_eo_tb_access == "RW" || tabParams.upload_eosi_tb_access == "RW")
            {
                return RedirectToAction("Index", "Upload");
            }
            else if (tabParams.admin_tb_access != "N")
              {
                  return RedirectToAction("Index", "Admin");
              }
              
              else if (tabParams.enterprise_orgs_tb_access != "N")
              {
                  return RedirectToAction("Index", "Enterprise");
              }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
       [TabLevelSecurity("newaccount_tb_access", "RW", "R")]
        public ActionResult NewAccount()
        {
            ViewBag.Message = "New Account Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }

        public ActionResult TopAccount()
        {
            ViewBag.Message = "Top Account section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }

        public ActionResult Constituent()
        {
            ViewBag.Message = "Constituent Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            var returnUrl = TempData["returnUrl"];
            ViewBag.returnUrl = returnUrl??"";
            return View(tb);
        }
        public ActionResult Transaction()
        {
            ViewBag.Message = "Transaction Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }
        public ActionResult EnterpriseAccount()
        {
            ViewBag.Message = "Enterprise Orgs Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }
        public ActionResult Upload()
        {
            ViewBag.Message = "Upload Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }
        public ActionResult Admin()
        {
            ViewBag.Message = "Account Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }
      
        public ActionResult Enterprise()
        {
            ViewBag.Message = "Enterprise Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }
        public ActionResult Help()
        {
            ViewBag.Message = "Help Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }
        //added by srini for getting userPermissions for tabs
        [HttpGet]
        public JsonResult GetUserPermissions()
        {
            TabLevelSecurityParams userPermissionsObj = Utility.getUserPermissions(User.GetUserName());
            return Json(userPermissionsObj, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult getGroupNames()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Group Name");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult getAddressType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Address Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult getEmailType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Email Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }



        [AllowAnonymous]
        public JsonResult getPhoneType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Phone Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult getPersonNameType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Person Name Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getOrganizationNameType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Organization Name Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getCharacteristicsType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Characteristics Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getContactPreferenceType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Contact Preference Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getContactPreferenceValue()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Contact Preference Value");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getTransactionNotes()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Transaction Notes");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getAffiliatorType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Affiliator Group Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getTransactionType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Transaction Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getSourceSystemType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Source System Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getChapterSourceSystemType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Chapter Source System Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getBoseApplicationSourceSystemCode()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Bose Application Source System Code");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getReferenceSources()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Reference Sources");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getCaseTypes()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Case Types");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getIntakeChannel()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Intake Channel");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getIntakeOwnerDepartment()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Intake Owner Department");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getPreferenceRequestType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Preference Request Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getPreferenceLos()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Preference Los");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getPreferenceChannel()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Preference Channel");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getPreferenceMessage()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Preference Message");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getExternalAssessmentCode()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("External Assessment Code");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getInternalAssessmentCode()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Internal Assessment Code");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getManualEmailAssessmentCode()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Manual Email Assessment Code");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getManualAddressAssessmentCode()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Manual Address Assessment Code");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getFinalAssesmentCodeEmail()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Final Assesment Code Email");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getAddressAssessmentCode()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Address Assessment Code");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getDeliverableLocatorType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Deliverable Locator Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getDeliverabilityCode()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Deliverability Code");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getUploadType()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Upload Type");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getChapterGroupName()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("ChapterGroupName");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getChapterGroupKeyName()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("ChapterGroupKeyName");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getChapterGroupCodeName()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("ChapterGroupCodeName");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getChapterGroupAssignmentMethod()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("ChapterGroupAssignmentMethod");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getChapterGroupAssignmentMethodReference()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("ChapterGroupAssignmentMethodReference");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult getChapterCode()
        {
            List<DropdownData> listPickListData = DropdownHelper.getDropdownValues("Chapter Code");
            var JSON = listPickListData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult getGroupTypeData()
        {
            List<DropdownData> listGroupTypeData = DropdownHelper.getGroupTypeDropdownValues();
            var JSON = listGroupTypeData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult getSubGroupTypeData()
        {
            List<DropdownData> listSubGroupTypeData = DropdownHelper.getSubGroupTypeDropdownValues();
            var JSON = listSubGroupTypeData;
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }


       

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult test1()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public int AddToCart(List<ConsSearchResultsCache> paramList)
        {
            if (paramList == null) return 0;
            string Message = "Error, Items are not added to the cart, Please contact administrator.";
            int ItemsCnt = 0;
            /// List<ConsSearchResultsCache> cartListObj = new List<ConsSearchResultsCache>();
            try
            {
               // List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData("OrglerCartItems", Request);
                List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData(this.getUserName(), OrglerCartItemsPath);

                if (cartListObj == null)
                {
                    cartListObj = new List<ConsSearchResultsCache>();
                }

                // cartListObj.AddRange(paramList.Where(x => x.request_transaction_key.Equals("-1")).ToList());
                cartListObj.AddRange(paramList.ToList());

                /*HashSet<ConsSearchResultsCache> hashSetList = new HashSet<ConsSearchResultsCache>();

                foreach (ConsSearchResultsCache cc in cartListObj)
                {
                    hashSetList.Add(cc);
                }*/

                List<ConsSearchResultsCache> newList = new List<ConsSearchResultsCache>();

                newList = cartListObj.Distinct().ToList();


                if (newList != null)
                {
                    MemoryCache<ConsSearchResultsCache>.setListCacheData(newList, this.getUserName(), OrglerCartItemsPath);
                    //??MemoryCache<ConsSearchResultsCache>.setListCacheData(newList, "OrglerCartItems", Response);
                    ItemsCnt = newList.Count;
                    Message = "Items " + ItemsCnt.ToString() + " successfully added to the cart.";
                }
                return ItemsCnt;
            }
            catch (Exception ex)
            {
                Message = "Erorr, " + ex.Message;
                return ItemsCnt = 0;
            }

        }
        [AllowAnonymous]
        [HttpPost]
        public int AddToCartMerge(List<ConsSearchResultsCacheMerge> paramList)
        {
            if (paramList == null) return 0;
            string Message = "Error, Items are not added to the cart, Please contact administrator.";
            int ItemsCnt = 0;
            /// List<ConsSearchResultsCache> cartListObj = new List<ConsSearchResultsCache>();
            try
            {
                //List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData("OrglerCartItems", Request);
                List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData(this.getUserName(), OrglerCartItemsPath);

                if (cartListObj == null)
                {
                    cartListObj = new List<ConsSearchResultsCache>();
                }
                List<ConsSearchResultsCache> paramnewList = new List<ConsSearchResultsCache>();
                // cartListObj.AddRange(paramList.Where(x => x.request_transaction_key.Equals("-1")).ToList());
                foreach (ConsSearchResultsCacheMerge cc in paramList)
                {
                    ConsSearchResultsCache c = new ConsSearchResultsCache();
                    c.constituent_id = cc.pot_merge_mstr_id;
                    c.name = cc.pot_merge_mstr_nm;
                    c.addr_line_1 = cc.addr_line_1;
                    c.addr_line_2 = cc.addr_line_2;
                    c.address = cc.address;
                    c.city = cc.city;
                    c.cnst_dsp_id =cc.dsp_id;
                    c.constituent_type = "OR";
                    c.email_address = cc.email;
                    c.first_name = "";
                    c.last_name = "";
                    c.phone_number = cc.phone;
                    c.request_transaction_key = "";
                    c.sourceSystem = "";
                    c.state_cd = "";
                    c.zip = "";
                    c.IndexString = cc.IndexString;
                    paramnewList.Add(c);
                }

                cartListObj.AddRange(paramnewList.ToList());

                /*HashSet<ConsSearchResultsCache> hashSetList = new HashSet<ConsSearchResultsCache>();

                foreach (ConsSearchResultsCache cc in cartListObj)
                {
                    hashSetList.Add(cc);
                }*/

                List<ConsSearchResultsCache> newList = new List<ConsSearchResultsCache>();
               
               newList = cartListObj.Distinct().ToList();


                if (newList != null)
                {
                    MemoryCache<ConsSearchResultsCache>.setListCacheData(newList, this.getUserName(), OrglerCartItemsPath);
                    ItemsCnt = newList.Count;
                    Message = "Items " + ItemsCnt.ToString() + " successfully added to the cart.";
                }
                return ItemsCnt;
            }
            catch (Exception ex)
            {
                Message = "Erorr, " + ex.Message;
                return ItemsCnt = 0;
            }

        }
        [AllowAnonymous]
        //Srini - Merge COnflicts are stored 
        [HttpPost]
        public bool AddMergeConflictRecords(MergeConflict param)
        {
            if (param == null) return false;
            string Message = "Error, Items are not added to the cart, Please contact administrator.";
            bool flag = false;
            try
            {

                //remove the merge conflicts records from merge cart also
               // MergeConflict Obj = MemoryCache<MergeConflict>.getCachedData("mergeConflicts", Request);
                //MergeConflict Obj = MemoryCache<MergeConflict>.getCachedData(this.getUserName(), MergeConflictsItems);
                MergeConflict Obj = null;
                if (Obj != null)
                {
                    // Obj = new MergeConflict();
                    List<locInfoResearchData> mergeConflictRecords = Obj.locInfoResearchData;

                    //get all the records from the cart
                    //List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData("OrglerCartItems", Request);
                    List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData(this.getUserName(), OrglerCartItemsPath);

                    List<ConsSearchResultsCache> newObj = new List<ConsSearchResultsCache>();

                    foreach (ConsSearchResultsCache constRecord in cartListObj)
                    {
                        bool recordPresent = false;
                        foreach (locInfoResearchData locInfo in mergeConflictRecords)
                        {
                            if (locInfo.cnst_mstr_id.Equals(constRecord.constituent_id))
                            {
                                recordPresent = true;
                                break;
                            }
                        }
                        if (!recordPresent)
                        {
                            newObj.Add(constRecord);
                        }
                    }
                    // string cartListStr = Models.Utility.ObjectToString<IList<ConsSearchResultsCache>>(newObj.ToList());

                    //MemoryCache<ConsSearchResultsCache>.setListCacheData(newObj.ToList(), "OrglerCartItems", Response);
                    MemoryCache<ConsSearchResultsCache>.setListCacheData(newObj.ToList(), this.getUserName(), OrglerCartItemsPath);
                }
                Obj = new MergeConflict();
                Obj.locInfoResearchData.AddRange(param.locInfoResearchData.Where(x => !x.cnst_mstr_id.Equals("")).ToList());
                HashSet<locInfoResearchData> hashSetList = new HashSet<locInfoResearchData>();

                foreach (locInfoResearchData cc in Obj.locInfoResearchData)
                {
                    hashSetList.Add(cc);
                }

                Obj.locInfoResearchData = hashSetList.Distinct().ToList();
                Obj.caseKey = param.caseKey;
                Obj.groupID = param.groupID;
                Obj.mergeConflictFlag = param.mergeConflictFlag;

                if (Obj != null)
                {
                   // MemoryCache<MergeConflict>.setCacheData(Obj, "mergeConflicts", Response);
                    MemoryCache<MergeConflict>.setCacheData(Obj, this.getUserName(), MergeConflictsItems);
                    flag = true;
                }
                return flag;
            }
            catch (Exception ex)
            {
                Message = "Erorr, " + ex.Message;
                return flag;
            }

        }

        [AllowAnonymous]
        [HttpPost]
        // [Route("home/unmerge/add")]
        public int AddToUnmergeCart(IList<UnmergeCache> paramList)
        {
            if (paramList == null) return 0;
            string Message = "Error, Items are not added to the cart, Please contact administrator.";
            int ItemsCnt = 0;
            // List<UnmergeCache> cartListObj = new List<UnmergeCache>();
            try
            {
                //List<UnmergeCache> cartListObj = MemoryCache<UnmergeCache>.getListCachedData("UnmergeOrglerCartItems", Request);
                List<UnmergeCache> cartListObj = MemoryCache<UnmergeCache>.getListCachedData(this.getUserName(), OrglerUnmergeCartItemsPath);
                if (cartListObj == null)
                {
                    cartListObj = new List<UnmergeCache>();
                }

                cartListObj.AddRange(paramList.Where(x => x.request_transaction_key.Equals("-1")).ToList());


                HashSet<UnmergeCache> hashSetList = new HashSet<UnmergeCache>();

                foreach (UnmergeCache cc in cartListObj)
                {
                    hashSetList.Add(cc);
                }

                cartListObj = hashSetList.Distinct().ToList();

                if (cartListObj != null)
                {
                    //MemoryCache<UnmergeCache>.setListCacheData(cartListObj, "UnmergeOrglerCartItems", Response);
                    MemoryCache<UnmergeCache>.setListCacheData(cartListObj, this.getUserName(), OrglerUnmergeCartItemsPath);
                    ItemsCnt = cartListObj.Count;
                    Message = "Items " + ItemsCnt.ToString() + " successfully added to the cart.";
                }
            }
            catch (Exception ex)
            {
                Message = "Erorr, " + ex.Message;
                ItemsCnt = 0;
            }
            return ItemsCnt;

        }
        [AllowAnonymous]
        //get merge records that are added to the cart
        [HttpPost]
        public JsonResult ViewCart()
        {
            List<ConsSearchResultsCache> cartListObj = null;
            try
            {
               // cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData("OrglerCartItems", Request);
                cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData(this.getUserName(),OrglerCartItemsPath);

            }
            catch (Exception ex)
            {
            }

            ViewBag.Remove = TempData["RmvOrglerCartItem"];

            return Json(cartListObj);
        }
        [HttpPost]
        public JsonResult ViewCartMerge()
        {
            List<ConsSearchResultsCacheMerge> cartListObj = null;
            try
            {
                cartListObj = MemoryCache<ConsSearchResultsCacheMerge>.getListCachedData("OrglerCartItemsMerge", Request);

            }
            catch (Exception ex)
            {
            }

            ViewBag.Remove = TempData["RmvOrglerCartItem"];

            return Json(cartListObj);
        }
        [AllowAnonymous]
        //get merge records that are added to the cart
        [HttpPost]
        public JsonResult getMergeConflict()
        {
            MergeConflict mConflictObj = null;
            try
            {
                mConflictObj = MemoryCache<MergeConflict>.getCachedData("mergeConflicts", Request);
            }
            catch (Exception ex)
            {

            }

            // ViewBag.Remove = TempData["RmvCartItem"];

            return Json(mConflictObj);
        }

        [AllowAnonymous]
        [HttpPost]
        public string SaveMenuJSON(MenuPreferences MenuPref)
        {
            string outputMessage = "";
            try
            {
                var readJSON = Utility.readJSONTFromFile<IList<MenuPreferences>>(MenuFilePath);
                if (readJSON != null)
                {
                    var userMenuPref = readJSON.Where(x => x.UserName == this.getUserName()).Select(s => s).FirstOrDefault();
                    if (userMenuPref != null)
                    {
                        readJSON.Remove(userMenuPref);
                    }
                }
                readJSON.Add(MenuPref);
                if (readJSON != null)
                    outputMessage = Utility.writeJSONToFile(readJSON, MenuFilePath);

                return outputMessage;
            }
            catch (Exception ex)
            {
                outputMessage = "Failed: " + ex;
                return outputMessage;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        // [Route("home/unmerge/list")]
        public JsonResult ViewUnmergeCart()
        {
            List<UnmergeCache> cartListObj = null;
            try
            {
                //cartListObj = MemoryCache<UnmergeCache>.getListCachedData("UnmergeOrglerCartItems", Request);
                cartListObj = MemoryCache<UnmergeCache>.getListCachedData(this.getUserName(), OrglerUnmergeCartItemsPath);
            }
            catch (Exception ex)
            {
            }

            ViewBag.Remove = TempData["RmvUnmergeOrglerCartItem"];

            return Json(cartListObj);
        }
        [AllowAnonymous]
        [HttpGet]
        public string RemoveCartItem(string IndexString)
        {
            string status = "0";
            //List<ConsSearchResultsCache> cartListObj = new List<ConsSearchResultsCache>();
            bool removeMerConflict = false;
            // MergeConflict mergeConflictObj = new MergeConflict();
            try
            {
                //List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData("OrglerCartItems", Request);

                List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData(this.getUserName(), OrglerCartItemsPath);
                if (cartListObj == null)
                {
                    cartListObj = new List<ConsSearchResultsCache>();
                }
               // MergeConflict mergeConflictObj = MemoryCache<MergeConflict>.getCachedData("mergeConflicts", Request);
                //MergeConflict mergeConflictObj = MemoryCache<MergeConflict>.getCachedData(this.getUserName(), MergeConflictsItems);
                MergeConflict mergeConflictObj = null;
                if (mergeConflictObj == null)
                {
                    mergeConflictObj = new MergeConflict();
                }

                List<ConsSearchResultsCache> tobeRemovedRecords = cartListObj.Where(x => x.IndexString == IndexString).ToList();

                foreach (ConsSearchResultsCache x in tobeRemovedRecords)
                {
                    foreach (locInfoResearchData cc in mergeConflictObj.locInfoResearchData)
                    {
                        if (x.constituent_id.Equals(cc.cnst_mstr_id))
                        {
                            removeMerConflict = true;
                            break;
                        }
                    }
                }

                List<ConsSearchResultsCache> freshLst = cartListObj.Where(x => x.IndexString != IndexString).ToList();
                if (freshLst != null)
                {

                    //MemoryCache<ConsSearchResultsCache>.setListCacheData(freshLst, "OrglerCartItems", Response);
                    MemoryCache<ConsSearchResultsCache>.setListCacheData(freshLst, this.getUserName(), OrglerCartItemsPath);

                    if (removeMerConflict)
                    {
                        //MemoryCache<MergeConflict>.clearCacheData("mergeConflicts", Response, Request);
                        MemoryCache<MergeConflict>.clearCacheData(this.getUserName(), MergeConflictsItems);
                    }
                    status = "1";
                }
            }
            catch (Exception ex)
            {
                status = "0";
            }
            TempData["RmvOrglerCartItem"] = status;
            return status;
        }

        [AllowAnonymous]
        [HttpPost]
        // [Route("home/merge/")]
        public string RemoveMergeCartItem(List<ConsSearchResultsCache> removeRecords)
        {
            string status = "0";
            // List<ConsSearchResultsCache> cartListObj = new List<ConsSearchResultsCache>();
            //MergeConflict mergeConflictObj = new MergeConflict();
            try
            {

               // List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData("OrglerCartItems", Request);
                List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData(this.getUserName(), OrglerCartItemsPath);
                if (cartListObj == null)
                {
                    cartListObj = new List<ConsSearchResultsCache>();
                }

                //MergeConflict mergeConflictObj = MemoryCache<MergeConflict>.getCachedData("mergeConflicts", Request);
                //MergeConflict mergeConflictObj = MemoryCache<MergeConflict>.getCachedData(this.getUserName(), MergeConflictsItems);
                MergeConflict mergeConflictObj = null;
                if (mergeConflictObj == null)
                {
                    mergeConflictObj = new MergeConflict();
                }

                //get the merge records that are not to be removed from cart
                //List<ConsSearchResultsCache> mergeRecords = cartListObj.Except(removeRecords).ToList();
                List<ConsSearchResultsCache> mergeRecords = cartListObj.Where(w => !removeRecords.Any(r => r.constituent_id == w.constituent_id)).ToList();

                bool mergeConflictIsPresent = false;
                //get mergeconflicts if there are any who are also present in mergeRecords
                foreach (ConsSearchResultsCache x in mergeRecords)
                {
                    foreach (locInfoResearchData cc in mergeConflictObj.locInfoResearchData)
                    {
                        if (x.constituent_id.Equals(cc.cnst_mstr_id))
                        {
                            mergeConflictIsPresent = true;
                            break;
                        }
                    }
                }
                if (mergeRecords != null)
                {
                    //MemoryCache<ConsSearchResultsCache>.setListCacheData(mergeRecords, "OrglerCartItems", Response);
                    MemoryCache<ConsSearchResultsCache>.setListCacheData(mergeRecords, this.getUserName(), OrglerCartItemsPath);
                    if (!mergeConflictIsPresent)
                    {
                        //MemoryCache<MergeConflict>.clearCacheData("mergeConflicts", Response, Request);
                        MemoryCache<MergeConflict>.clearCacheData(this.getUserName(), MergeConflictsItems);
                    }
                    status = "success";
                }
            }
            catch
            {
                status = "failure";
            }
            return status;
        }

        [AllowAnonymous]
        [HttpPost]
        public string RemoveUnmergeCartItem(List<UnmergeCache> removeRecords)
        {

            string status = "0";
            // List<UnmergeCache> cartListObj = new List<UnmergeCache>();
            try
            {


                //List<UnmergeCache> cartListObj = MemoryCache<UnmergeCache>.getListCachedData("UnmergeOrglerCartItems", Request);
                List<UnmergeCache> cartListObj = MemoryCache<UnmergeCache>.getListCachedData(this.getUserName(), OrglerUnmergeCartItemsPath);
                if (cartListObj == null)
                {
                    cartListObj = new List<UnmergeCache>();
                }

                //List<UnmergeCache> result = cartListObj.Except(removeRecords).ToList();
                List<UnmergeCache> result = cartListObj.Where(w => !removeRecords.Any(r => r.source_system_cd == w.source_system_cd && r.source_system_id == w.source_system_id)).ToList();
                if (result != null)
                {
                    //MemoryCache<UnmergeCache>.setListCacheData(result, "UnmergeOrglerCartItems", Response);
                    MemoryCache<UnmergeCache>.setListCacheData(result, this.getUserName(), OrglerUnmergeCartItemsPath);
                    status = "success";
                }
            }
            catch (Exception ex)
            {
                status = "failed";
            }
            TempData["RmvUnmergeCartItem"] = status;
            return status;
        }

        [AllowAnonymous]
        //merge records are cleared;
        [HttpGet]
        public int ClearCart()
        {
            try
            {
               // MemoryCache<ConsSearchResultsCache>.clearCacheData("OrglerCartItems", Response, Request);
                MemoryCache<ConsSearchResultsCache>.clearCacheData(this.getUserName(), OrglerCartItemsPath);
               // MemoryCache<MergeConflict>.clearCacheData("mergeConflicts", Response, Request);
                MemoryCache<MergeConflict>.clearCacheData(this.getUserName(), MergeConflictsItems);

            }
            catch (Exception ex)
            {
                return 0;
            }
            //TempData["ClearCart"] = "Y";
            return 1;
        }

        [AllowAnonymous]
        [HttpGet]
        public int ClearUnmergeCart()
        {
            try
            {
               // MemoryCache<UnmergeCache>.clearCacheData("UnmergeOrglerCartItems", Response, Request);
                MemoryCache<UnmergeCache>.clearCacheData(this.getUserName(), OrglerUnmergeCartItemsPath);
            }
            catch (Exception ex)
            {
                return 0;
            }

            return 1;
        }

        [HttpGet]
        public JsonResult GetMenuJson()
        {
            var readJSON = Utility.readJSONTFromFile<IList<MenuPreferences>>(MenuFilePath);
            var userPrefJSON = readJSON.Where(x => x.UserName == this.getUserName()).Select(x => x).FirstOrDefault();
            //return null;
            return Json(userPrefJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCaseRecentSearches()
        {
            string CaseRecentSearchPath = WebConfigurationManager.AppSettings["CaseRecentSearchPath"];

            var JSON = Utility.readJSONTFromFile<List<ListCaseInputSearchModel>>(CaseRecentSearchPath);
            var filteredJSON = JSON;
            if (JSON != null)
                filteredJSON = JSON.Where(x => x.CaseInputSearchModel[0].LoggedInUser == User.GetUserName()).ToList();
            return Json(filteredJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetConstituentRecentSearches()
        {
            string ConstRecentSearchPath = WebConfigurationManager.AppSettings["ConstituentRecentSearchPath"];
            var JSON = Utility.readJSONTFromFile<List<ListConstituentInputSearchModel>>(ConstRecentSearchPath);
            var filteredJSON = JSON;
            if (JSON != null)
                filteredJSON = JSON.Where(x => x.ConstituentInputSearchModel[0].LoggedInUser == User.GetUserName()).ToList();
            return Json(filteredJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTransactionRecentSearches()
        {
            string transRecentSearchPath = WebConfigurationManager.AppSettings["TransactionRecentSearchPath"];
            var JSON = Utility.readJSONTFromFile<List<ListTransactionSearchInputModel>>(transRecentSearchPath);
            var filteredJSON = JSON;
            if (JSON != null)
                filteredJSON = JSON.Where(x => x.TransactionSearchInputModel[0].LoggedInUser == User.GetUserName()).ToList();
            return Json(filteredJSON, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public string WriteConstituentRecentSearches(ListConstituentInputSearchModel CaseModel)
        {
            string Message = "Error, something happened while logging the Recent Searches. Please contact administrator.";
            if (!Utility.IsAnyPropertyNotNull(CaseModel)) return "Constituent Search Log: Nothing to log";

            List<ListConstituentInputSearchModel> RecentSearchObj = new List<ListConstituentInputSearchModel>();
            try
            {
                string ConstRecentSearchPath = WebConfigurationManager.AppSettings["ConstituentRecentSearchPath"];
                var RecentSearch = Utility.readJSONTFromFile<List<ListConstituentInputSearchModel>>(ConstRecentSearchPath);
                var filteredRecentSearch = RecentSearch; // This is to filter out the recent searches based on the username

                if (RecentSearch != null)
                {
                    if (!User.GetUserName().Equals("") || User.GetUserName() != null)
                    {
                        filteredRecentSearch = RecentSearch.Where(x => x.ConstituentInputSearchModel[0].LoggedInUser == User.GetUserName()).ToList();
                        RecentSearch.RemoveAll(x => x.ConstituentInputSearchModel[0].LoggedInUser == User.GetUserName());
                    }
                    if (filteredRecentSearch.Count > 10)
                    {
                        filteredRecentSearch.RemoveRange(10, filteredRecentSearch.Count - 10); // Remove all items greater than 10, since we show only top 10 recent searches
                    }
                    filteredRecentSearch.Insert(0, CaseModel);
                    RecentSearch.AddRange(filteredRecentSearch);
                    var SuccessMessage = Utility.writeJSONToFile<List<ListConstituentInputSearchModel>>(RecentSearch, ConstRecentSearchPath);
                    if (SuccessMessage == "SUCCESS")
                        Message = "The Constituent Recent searches were logged successfully";
                    else
                        return Message;
                }
                else
                {
                    RecentSearchObj.Add(CaseModel);
                    var SuccessMessage = Utility.writeJSONToFile<List<ListConstituentInputSearchModel>>(RecentSearchObj, ConstRecentSearchPath);
                    if (SuccessMessage == "SUCCESS")
                        Message = "The Constituent Recent searches were logged successfully";
                    else
                        return Message;
                }

            }
            catch (Exception ex)
            {
                Message = "Erorr, " + ex.Message;
            }
            return Message;
        }

        [HttpPost]
        public string WriteQuickSearchResults(ConstituentOutputSearchResults ConstOutputMethod)
        {
            string QuickSearchResultsPath = WebConfigurationManager.AppSettings["QuickSearchResultsPath"];
            var SuccessMessage = Utility.writeJSONToFile<ConstituentOutputSearchResults>(ConstOutputMethod, QuickSearchResultsPath);
            return "Success";
        }

        [HttpGet]
        public JsonResult GetQuickSearchResults()
        {
            string QuickSearchResultsPath = WebConfigurationManager.AppSettings["QuickSearchResultsPath"];
            var JSON = Utility.readJSONTFromFile<ConstituentOutputSearchResults>(QuickSearchResultsPath);
            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string WriteCaseRecentSearches(ListCaseInputSearchModel CaseModel)
        {
            string Message = "Error, something happened while logging the Recent Searches. Please contact administrator.";
            if (!Utility.IsAnyPropertyNotNull(CaseModel)) return "Case Search Log: Nothing to log";

            List<ListCaseInputSearchModel> RecentSearchObj = new List<ListCaseInputSearchModel>();
            try
            {
                string CaseRecentSearchPath = WebConfigurationManager.AppSettings["CaseRecentSearchPath"];
                var RecentSearch = Utility.readJSONTFromFile<List<ListCaseInputSearchModel>>(CaseRecentSearchPath);
                var filteredRecentSearch = RecentSearch; // This is to filter out the recent searches based on the username
                if (RecentSearch != null)
                {
                    if (!User.GetUserName().Equals("") || User.GetUserName() != null)
                    {
                        filteredRecentSearch = RecentSearch.Where(x => x.CaseInputSearchModel[0].LoggedInUser == User.GetUserName()).ToList();
                        RecentSearch.RemoveAll(x => x.CaseInputSearchModel[0].LoggedInUser == User.GetUserName());
                    }
                    
                    if (filteredRecentSearch.Count > 10)
                    {
                        filteredRecentSearch.RemoveRange(10, filteredRecentSearch.Count - 10); // Remove all items greater than 10, since we show only top 10 recent searches
                    }
                    filteredRecentSearch.Insert(0, CaseModel);
                    RecentSearch.AddRange(filteredRecentSearch);
                    var SuccessMessage = Utility.writeJSONToFile<List<ListCaseInputSearchModel>>(RecentSearch, CaseRecentSearchPath);
                    if (SuccessMessage == "SUCCESS")
                        Message = "The Case Recent searches were logged successfully";
                    else
                        return Message;
                }
                else
                {
                    RecentSearchObj.Add(CaseModel);
                    var SuccessMessage = Utility.writeJSONToFile<List<ListCaseInputSearchModel>>(RecentSearchObj, CaseRecentSearchPath);
                    if (SuccessMessage == "SUCCESS")
                        Message = "The Case Recent searches were logged successfully";
                    else
                        return Message;
                }

            }
            catch (Exception ex)
            {
                Message = "Erorr, " + ex.Message;
            }
            return Message;
        }


        [HttpPost]
        public string WriteTransactionRecentSearches(ListTransactionSearchInputModel TransactionModel)
        {

            string Message = "Error, something happened while logging the Recent Searches. Please contact administrator.";
            if (!Utility.IsAnyPropertyNotNull(TransactionModel)) return "Transaction Search Log: Nothing to log";

            List<ListTransactionSearchInputModel> RecentSearchObj = new List<ListTransactionSearchInputModel>();
            try
            {
                string TransRecentSearchPath = WebConfigurationManager.AppSettings["TransactionRecentSearchPath"];
                var RecentSearch = Utility.readJSONTFromFile<List<ListTransactionSearchInputModel>>(TransRecentSearchPath);
                var filteredRecentSearch = RecentSearch; // This is to filter out the recent searches based on the username
                if (RecentSearch != null)
                {
                    if (!User.GetUserName().Equals("") || User.GetUserName() != null)
                    {
                        filteredRecentSearch = RecentSearch.Where(x => x.TransactionSearchInputModel[0].LoggedInUser == User.GetUserName()).ToList();
                        RecentSearch.RemoveAll(x => x.TransactionSearchInputModel[0].LoggedInUser == User.GetUserName());
                    }
                    if (filteredRecentSearch.Count > 10)
                    {
                        filteredRecentSearch.RemoveRange(10, filteredRecentSearch.Count - 10); // Remove all items greater than 10, since we show only top 10 recent searches
                    }
                    RecentSearch.Insert(0, TransactionModel);
                    RecentSearch.AddRange(filteredRecentSearch);
                    var SuccessMessage = Utility.writeJSONToFile<List<ListTransactionSearchInputModel>>(RecentSearch, TransRecentSearchPath);
                    if (SuccessMessage == "SUCCESS")
                        Message = "The Constituent Recent searches were logged successfully";
                    else
                        return Message;
                }
                else
                {
                    RecentSearchObj.Add(TransactionModel);
                    var SuccessMessage = Utility.writeJSONToFile<List<ListTransactionSearchInputModel>>(RecentSearchObj, TransRecentSearchPath);
                    if (SuccessMessage == "SUCCESS")
                        Message = "The Transaction Recent searches were logged successfully";
                    else
                        return Message;
                }

            }
            catch (Exception ex)
            {
                Message = "Erorr, " + ex.Message;
            }
            return Message;
        }

        [HandleException]
        public string getUserName()
        {

            return User.GetUserName();

        }




        private void checkExceptions(string res)
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
        }

        public JsonResult compareUnmerge(IList<UnmergeCache> cartList)
        {
            if (cartList.Count > 0)
            {
                List<string> indexStrings = new List<string>();
                List<string> constNames = new List<string>();
                List<string> constMasterIds = new List<string>();
                List<string> constAddresses = new List<string>();
                List<string> sourcesystemCds = new List<string>();
                List<string> sourceSystemIds = new List<string>();


                foreach (UnmergeCache cart in cartList)
                {
                    indexStrings.Add(cart.IndexString);
                    constNames.Add(cart.cnst_nm);
                    constAddresses.Add(cart.address);
                    constMasterIds.Add(cart.cnst_mstr_id);
                    sourcesystemCds.Add(cart.source_system_cd);
                    sourceSystemIds.Add(cart.source_system_id);

                }


                Dictionary<string, List<string>> returnData = new Dictionary<string, List<string>>();
                returnData.Add("IndexStrings", indexStrings);
                returnData.Add("ConstNames", constNames);
                returnData.Add("ConstAddresses", constAddresses);
                returnData.Add("constMasterIds", constMasterIds);
                returnData.Add("SourcesystemCds", sourcesystemCds);
                returnData.Add("SourcesystemIds", sourceSystemIds);

                return Json(returnData, JsonRequestBehavior.AllowGet);

            }
            else

                return null;
        }


    }

    public class MemoryCache<T>
    {
        public static List<T> getListCachedData(string cookieName, HttpRequestBase Request)
        {
            if (Request != null && Request.Cookies[cookieName] != null)
            {
                var CartString = Request.Cookies[cookieName].Value.ToString();
                if (!string.IsNullOrEmpty(CartString))
                {
                    return Models.UtilityData.StringToObject<List<T>>(CartString);
                }
            }
            return null;
        }


        public static T getCachedData(string cookieName, HttpRequestBase Request)
        {


            if (Request != null && Request.Cookies[cookieName] != null)
            {
                var CartString = Request.Cookies[cookieName].Value.ToString();
                if (!string.IsNullOrEmpty(CartString))
                {
                    return Models.UtilityData.StringToObject<T>(CartString);
                }
            }

            return default(T);
        }



        public static void setListCacheData(List<T> data, string cookieName, HttpResponseBase Response)
        {
            string cookieTimeout = WebConfigurationManager.AppSettings["OrglerCartCookieTimeout"];
            var finalResult = data;
            string cartListStr = Models.UtilityData.ObjectToString<IList<T>>(finalResult);
            if (!string.IsNullOrEmpty(cartListStr))
            {
                Response.Cookies.Remove(cookieName);
                Response.Cookies.Add(new System.Web.HttpCookie(cookieName)
                {
                    Value = cartListStr,
                    Expires = DateTime.Now.AddMinutes(Convert.ToInt32(cookieTimeout)),
                    HttpOnly = true
                });
            }
        }


        public static void setCacheData(T data, string cookieName, HttpResponseBase Response)
        {
            string cookieTimeout = WebConfigurationManager.AppSettings["OrglerCartCookieTimeout"];
            string str = "";
            var finalResult = data;
            str = Models.UtilityData.ObjectToString<T>(finalResult);

            if (!string.IsNullOrEmpty(str))
            {
                Response.Cookies.Remove(cookieName);
                Response.Cookies.Add(new System.Web.HttpCookie(cookieName)
                {
                    Value = str,
                    Expires = DateTime.Now.AddMinutes(Convert.ToInt32(cookieTimeout)),
                    HttpOnly = true
                });

            }
        }



        public static void clearCacheData(string cookieName, HttpResponseBase Response, HttpRequestBase Request)
        {
            if (Request != null && Request.Cookies[cookieName] != null)
            {
                Response.Cookies.Remove(cookieName);
                Response.Cookies.Add(new System.Web.HttpCookie(cookieName)
                {
                    Value = null,
                    Expires = DateTime.Now.AddMinutes(-10),
                    HttpOnly = true
                });
            }
        }

        public static void setListCacheData(List<T> data, string userName, string path)
        {

                OrglerCartItems<T> cartItems = new OrglerCartItems<T>(data, userName);
                MyJSON<T>.SaveOrgler(cartItems, userName, path);
           
        }

        public static void setCacheData(T data, string userName,string path)
        {
            MergeConflictsItem<T> cartItems = new MergeConflictsItem<T>(data, userName);
            MyJSON<T>.SaveMergeConflicts(cartItems, userName, path);
        }


        public static List<T> getListCachedData(string userName,string path)
        {
            var result = MyJSON<T>.ReadOrlger(userName, path);
            List<T> finalResult = new List<T>();

            if (result != null)
            {
                finalResult = result.data;
            }
            return finalResult;
        }


        public static T getCachedData(string userName,string path)
        {


            var result = MyJSON<T>.ReadMergeConflicts(userName, path);
            //var result = (T)null;
           /* var finalResult

            if (result != null)
            {
                finalResult = result.data;
            }*/
            return result;
        }


        
        public static void clearCacheData(string userName,string path)
        {
         
           List<T> nlldata = new List<T>();
           OrglerCartItems<T> data = new OrglerCartItems<T>(nlldata, userName);
           MyJSON<T>.SaveOrgler(data, userName, path);
        }



    }




    public class MyJSON<T>
    {
        //public static string OrglerCartItemsPath = WebConfigurationManager.AppSettings["OrglerCartItemsPath"];
       // public static string OrglerUnmergeCartItemsPath = WebConfigurationManager.AppSettings["UnmergeOrglerCartItemsPath"];
       // public static string MergeConflictsItems = WebConfigurationManager.AppSettings["MergeConflictsItemsPath"];


        public static string SaveOrgler(OrglerCartItems<T> data, string userName,string path)
        {
            string outputMessage = "";
            try
            {
                var readJSON = Utility.readJSONTFromFile<IList<OrglerCartItems<T>>>(path);
                if (readJSON != null)
                {
                    var userRecords = readJSON.Where(x => x.UserName == userName).Select(s => s).FirstOrDefault();
                    if (userRecords != null)
                    {
                        readJSON.Remove(userRecords);
                    }
                }
                readJSON.Add(data);
                if (readJSON != null)
                    outputMessage = Utility.writeJSONToFile(readJSON, path);

                return outputMessage;
            }
            catch (Exception ex)
            {
                outputMessage = "Failed: " + ex;
                return outputMessage;
            }
        }

      



        public static dynamic ReadOrlger(string userName,string path)
        {
            var readJSON = Utility.readJSONTFromFile<IList<OrglerCartItems<T>>>(path);
            var userPrefJSON = readJSON.Where(x => x.UserName == userName).Select(x => x).FirstOrDefault();
            //var result = (new JavaScriptSerializer()).DeserializeObject(userPrefJSON);
            return userPrefJSON;
        }


        public static string SaveMergeConflicts(MergeConflictsItem<T> data, string userName, string path)
        {
            string outputMessage = "";
            try
            {
                var readJSON = Utility.readJSONTFromFile<IList<MergeConflictsItem<T>>>(path);
                if (readJSON != null)
                {
                    var userRecords = readJSON.Where(x => x.UserName == userName).Select(s => s).FirstOrDefault();
                    if (userRecords != null)
                    {
                        readJSON.Remove(userRecords);
                    }
                }
                readJSON.Add(data);
                if (readJSON != null)
                    outputMessage = Utility.writeJSONToFile(readJSON, path);

                return outputMessage;
            }
            catch (Exception ex)
            {
                outputMessage = "Failed: " + ex;
                return outputMessage;
            }
        }


        public static dynamic ReadMergeConflicts(string userName,string path)
        {
            var readJSON = Utility.readJSONTFromFile<IList<MergeConflictsItem<T>>>(path);
            //var userPrefJSON ;
            if (readJSON.Count >= 1)
            {
                return readJSON.Where(x => x.UserName == userName).Select(x => x).FirstOrDefault();
            }

            //var result = (new JavaScriptSerializer()).DeserializeObject(userPrefJSON);
            return null;
        }
    }


    public class OrglerCartItems<T>
    {
        public string UserName { get; set; }
        public List<T> data;

        public OrglerCartItems(List<T> d,string userName)
        {
            data = d;
            this.UserName = userName;
        }
         
    }


    public class MergeConflictsItem<T>
    {
        public string UserName { get; set; }
        public T data;

        public MergeConflictsItem(T data,string UserName)
        {
            this.data = data;
            this.UserName = UserName;
        }
    }
   

}