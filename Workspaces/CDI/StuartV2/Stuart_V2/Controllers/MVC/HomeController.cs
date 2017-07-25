using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using Stuart_V2.Models;
using Stuart_V2.Data.Entities;
using System.Threading.Tasks;
using Stuart_V2.Models.Entities.Constituents;
using Stuart_V2.Models.Entities;
using Stuart_V2.Models.Entities.Case;
using Stuart_V2.Models.Entities.Transaction;
using Stuart_V2.Exceptions;
using System.Security.Principal;
using Stuart_V2.Models.Entities.Locator;

namespace Stuart_V2.Controllers
{
    [RouteArea("Home")]
    [RoutePrefix("")]
    [Authorize]
    [AuthRoles("Admin", "User", "Writer")]
    public class HomeController : MVC.BaseController
    {
        //private string BaseURL = "";
        private string MenuFilePath = "";
        //private string ClientID = "";
        //private string UserName = "";
        //private string Password = "";
        private int TokenExpire = 30;
        protected string Token = "";
        

        public HomeController()
        {
            //BaseURL = WebConfigurationManager.AppSettings["BaseURL"];
            MenuFilePath = WebConfigurationManager.AppSettings["MenuJSONFilePath"];
            //ClientID = WebConfigurationManager.AppSettings["ClientID"];
            //UserName = WebConfigurationManager.AppSettings["UserName"];
            //Password = WebConfigurationManager.AppSettings["Password"];
            TokenExpire = WebConfigurationManager.AppSettings["TokenExpire"] == null ? 30 : int.Parse(WebConfigurationManager.AppSettings["TokenExpire"].ToString());
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

        public ActionResult Index()
        {
            string userName = User.GetUserName();
            
            TabLevelSecurityParams tabParams = Utility.getUserPermissions(userName);
            if (tabParams.constituent_tb_access != "N")
            {
                return RedirectToAction("Index", "Constituent");
            }
            else if (tabParams.case_tb_access != "N")
            {
                return RedirectToAction("Index", "Case");
            }
            else if (tabParams.transaction_tb_access != "N")
            {
                return RedirectToAction("Index", "Transaction");
            }
          /*  else if (tabParams.upload_tb_access != "N")
            {
                return RedirectToAction("Index", "Upload");
            }
            else if (tabParams.admin_tb_access != "N")
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (tabParams.reference_data_tb_access != "N")
            {
                return RedirectToAction("Index", "Reference");
            }
            else if (tabParams.enterprise_orgs_tb_access != "N")
            {
                return RedirectToAction("Index", "Enterprise");
            }*/
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        public  ActionResult Case()
        {
            ViewBag.Message = "Case Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }

        public ActionResult Constituent()
        {
            ViewBag.Message = "Constituent section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }

        public ActionResult Transaction()
        {
            ViewBag.Message = "Transaction Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }
        public ActionResult Locator()
        {
            ViewBag.Message = "Locator Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }
        public ActionResult Account()
        {
            ViewBag.Message = "Account Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }
        public ActionResult Upload()
        {
           // await checkStuartwebToken();
            ViewBag.Message = "Upload Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }

        public ActionResult Admin()
        {
          
            ViewBag.Message = "Admin Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }
        public ActionResult Reports()
        {
            ViewBag.Message = "Reports Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }
        public ActionResult Reference()
        {
            ViewBag.Message = "Reference Section";
            TabLevelSecurityParams tb = Utility.getUserPermissions(User.GetUserName());
            return View(tb);
        }

        public ActionResult Utilities()
        {
            ViewBag.Message = "Utilities Section";
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
                List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData("CartItems", Request);

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
                    MemoryCache<ConsSearchResultsCache>.setListCacheData(newList, "CartItems", Response);
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
                MergeConflict Obj = MemoryCache<MergeConflict>.getCachedData("mergeConflicts", Request);

                if (Obj != null)
                {
                   // Obj = new MergeConflict();
                    List<locInfoResearchData> mergeConflictRecords = Obj.locInfoResearchData;

                    //get all the records from the cart
                    List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData("CartItems", Request);
                           

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

                        MemoryCache<ConsSearchResultsCache>.setListCacheData(newObj.ToList(), "CartItems", Response);
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
                    MemoryCache<MergeConflict>.setCacheData(Obj, "mergeConflicts", Response);
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
               List<UnmergeCache> cartListObj = MemoryCache<UnmergeCache>.getListCachedData("UnmergeCartItems", Request);

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
                    MemoryCache<UnmergeCache>.setListCacheData(cartListObj, "UnmergeCartItems", Response);
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
                cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData("CartItems", Request);

            }
            catch (Exception ex)
            {
            }

            ViewBag.Remove = TempData["RmvCartItem"];

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
                        if (MenuPref.PreferencesIN!=null )
                        {
                            userMenuPref.PreferencesIN = MenuPref.PreferencesIN;
                        }
                        if (MenuPref.PreferencesOR != null)
                        {
                            userMenuPref.PreferencesOR = MenuPref.PreferencesOR;
                        }
                        //readJSON.Remove(userMenuPref);
                    }
                    else
                    {
                        readJSON.Add(MenuPref);
                    }
                }   
                //readJSON.Add(MenuPref);
                if(readJSON != null)
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
                cartListObj = MemoryCache<UnmergeCache>.getListCachedData("UnmergeCartItems", Request);
            }
            catch (Exception ex)
            {
            }

            ViewBag.Remove = TempData["RmvUnmergeCartItem"];

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
                List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData("CartItems", Request);

                if (cartListObj == null)
                {
                    cartListObj = new List<ConsSearchResultsCache>();
                }
                MergeConflict mergeConflictObj = MemoryCache<MergeConflict>.getCachedData("mergeConflicts", Request);

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

                    MemoryCache<ConsSearchResultsCache>.setListCacheData(freshLst, "CartItems", Response);


                    if (removeMerConflict)
                    {
                        MemoryCache<MergeConflict>.clearCacheData("mergeConflicts", Response, Request);
                    }
                    status = "1";
                }
            }
            catch (Exception ex)
            {
                status = "0";
            }
            TempData["RmvCartItem"] = status;
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

                List<ConsSearchResultsCache> cartListObj = MemoryCache<ConsSearchResultsCache>.getListCachedData("CartItems", Request);
                if (cartListObj == null)
                {
                    cartListObj = new List<ConsSearchResultsCache>();
                }

                MergeConflict mergeConflictObj = MemoryCache<MergeConflict>.getCachedData("mergeConflicts", Request);

                if (mergeConflictObj == null)
                {
                    mergeConflictObj = new MergeConflict();
                }

                //get the merge records that are not to be removed from cart
                //List<ConsSearchResultsCache> mergeRecords = cartListObj.Except(removeRecords).ToList();
                List<ConsSearchResultsCache> mergeRecords = cartListObj.Where(w =>!removeRecords.Any(r => r.constituent_id == w.constituent_id)).ToList();

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
                    MemoryCache<ConsSearchResultsCache>.setListCacheData(mergeRecords, "CartItems", Response);
                    if (!mergeConflictIsPresent)
                    {
                        MemoryCache<MergeConflict>.clearCacheData("mergeConflicts", Response, Request);
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


                List<UnmergeCache> cartListObj = MemoryCache<UnmergeCache>.getListCachedData("UnmergeCartItems", Request);
                if (cartListObj == null)
                {
                    cartListObj = new List<UnmergeCache>();
                }

                //List<UnmergeCache> result = cartListObj.Except(removeRecords).ToList();
                List<UnmergeCache> result = cartListObj.Where(w => !removeRecords.Any(r => r.source_system_cd == w.source_system_cd && r.source_system_id == w.source_system_id)).ToList();
                if (result != null)
                {
                    MemoryCache<UnmergeCache>.setListCacheData(result, "UnmergeCartItems", Response);
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
                MemoryCache<ConsSearchResultsCache>.clearCacheData("CartItems", Response, Request);
                MemoryCache<MergeConflict>.clearCacheData("mergeConflicts", Response, Request);
                
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
                MemoryCache<UnmergeCache>.clearCacheData("UnmergeCartItems", Response, Request);
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
        public JsonResult GetLocatorEmailRecentSearches()
        {
            string LocatorEmailRecentSearchPath = WebConfigurationManager.AppSettings["LocatorEmailRecentSearchPath"];

            var JSON = Utility.readJSONTFromFile<List<ListLocatorEmailInputSearchModel>>(LocatorEmailRecentSearchPath);
            var filteredJSON = JSON;
            if (JSON != null)
                filteredJSON = JSON.Where(x => x.LocatorEmailInputSearchModel[0].LoggedInUser == User.GetUserName()).ToList();
            return Json(filteredJSON, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetLocatorAddressRecentSearches()
        {
            string LocatorAddressRecentSearchPath = WebConfigurationManager.AppSettings["LocatorAddressRecentSearchPath"];

            var JSON = Utility.readJSONTFromFile<List<ListLocatorAddressInputSearchModel>>(LocatorAddressRecentSearchPath);
            var filteredJSON = JSON;
            if (JSON != null)
                filteredJSON = JSON.Where(x => x.LocatorAddressInputSearchModel[0].LoggedInUser == User.GetUserName()).ToList();
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
            if(JSON != null)
                filteredJSON = JSON.Where(x => x.TransactionSearchInputModel[0].LoggedInUser == User.GetUserName()).ToList();
            return Json(filteredJSON, JsonRequestBehavior.AllowGet);
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
        public string WriteLocatorEmailRecentSearches(ListLocatorEmailInputSearchModel locatorModel)
        {
            string Message = "Error, something happened while logging the Recent Searches. Please contact administrator.";
            if (!Utility.IsAnyPropertyNotNull(locatorModel)) return "Locator Email Search Log: Nothing to log";

            List<ListLocatorEmailInputSearchModel> RecentSearchObj = new List<ListLocatorEmailInputSearchModel>();
            try
            {
                string LocatorEmailRecentSearchPath = WebConfigurationManager.AppSettings["LocatorEmailRecentSearchPath"];
                var RecentSearch = Utility.readJSONTFromFile<List<ListLocatorEmailInputSearchModel>>(LocatorEmailRecentSearchPath);
                var filteredRecentSearch = RecentSearch; // This is to filter out the recent searches based on the username
                if (RecentSearch != null)
                {
                    if (!User.GetUserName().Equals("") || User.GetUserName() != null)
                    {
                        filteredRecentSearch = RecentSearch.Where(x => x.LocatorEmailInputSearchModel[0].LoggedInUser == User.GetUserName()).ToList();
                        RecentSearch.RemoveAll(x => x.LocatorEmailInputSearchModel[0].LoggedInUser == User.GetUserName());
                    }

                    if (filteredRecentSearch.Count > 10)
                    {
                        filteredRecentSearch.RemoveRange(10, filteredRecentSearch.Count - 10); // Remove all items greater than 10, since we show only top 10 recent searches
                    }
                    filteredRecentSearch.Insert(0, locatorModel);
                    RecentSearch.AddRange(filteredRecentSearch);
                    var SuccessMessage = Utility.writeJSONToFile<List<ListLocatorEmailInputSearchModel>>(RecentSearch, LocatorEmailRecentSearchPath);
                    if (SuccessMessage == "SUCCESS")
                        Message = "The Locator Email Recent searches were logged successfully";
                    else
                        return Message;
                }
                else
                {
                    RecentSearchObj.Add(locatorModel);
                    var SuccessMessage = Utility.writeJSONToFile<List<ListLocatorEmailInputSearchModel>>(RecentSearchObj, LocatorEmailRecentSearchPath);
                    if (SuccessMessage == "SUCCESS")
                        Message = "The Locator Email Recent searches were logged successfully";
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
        public string WriteLocatorAddressRecentSearches(ListLocatorAddressInputSearchModel locatorModel)
        {
            string Message = "Error, something happened while logging the Recent Searches. Please contact administrator.";
            if (!Utility.IsAnyPropertyNotNull(locatorModel)) return "Locator Address Search Log: Nothing to log";

            List<ListLocatorAddressInputSearchModel> RecentSearchObj = new List<ListLocatorAddressInputSearchModel>();
            try
            {
                string LocatorAddressRecentSearchPath = WebConfigurationManager.AppSettings["LocatorAddressRecentSearchPath"];
                var RecentSearch = Utility.readJSONTFromFile<List<ListLocatorAddressInputSearchModel>>(LocatorAddressRecentSearchPath);
                var filteredRecentSearch = RecentSearch; // This is to filter out the recent searches based on the username
                if (RecentSearch != null)
                {
                    if (!User.GetUserName().Equals("") || User.GetUserName() != null)
                    {
                        filteredRecentSearch = RecentSearch.Where(x => x.LocatorAddressInputSearchModel[0].LoggedInUser == User.GetUserName()).ToList();
                        RecentSearch.RemoveAll(x => x.LocatorAddressInputSearchModel[0].LoggedInUser == User.GetUserName());
                    }

                    if (filteredRecentSearch.Count > 10)
                    {
                        filteredRecentSearch.RemoveRange(10, filteredRecentSearch.Count - 10); // Remove all items greater than 10, since we show only top 10 recent searches
                    }
                    filteredRecentSearch.Insert(0, locatorModel);
                    RecentSearch.AddRange(filteredRecentSearch);
                    var SuccessMessage = Utility.writeJSONToFile<List<ListLocatorAddressInputSearchModel>>(RecentSearch, LocatorAddressRecentSearchPath);
                    if (SuccessMessage == "SUCCESS")
                        Message = "The Locator Address Recent searches were logged successfully";
                    else
                        return Message;
                }
                else
                {
                    RecentSearchObj.Add(locatorModel);
                    var SuccessMessage = Utility.writeJSONToFile<List<ListLocatorAddressInputSearchModel>>(RecentSearchObj, LocatorAddressRecentSearchPath);
                    if (SuccessMessage == "SUCCESS")
                        Message = "The Locator Address Recent searches were logged successfully";
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

        //added by srini for getting userPermissions for tabs
        [HttpGet]
        public JsonResult GetUserPermissions()
        {
            TabLevelSecurityParams userPermissionsObj = Utility.getUserPermissions(User.GetUserName());
            return Json(userPermissionsObj, JsonRequestBehavior.AllowGet);
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


       

        //private void checkExceptions(string res)
        //{
        //    if (res.ToLower().Contains("timedout"))
        //    {
        //        throw new CustomExceptionHandler(Json("TimedOut"));
        //    }
        //    if (res.ToLower().Contains("error"))
        //    {
        //        throw new CustomExceptionHandler(Json("DatabaseError"));
        //    }
        //    if (res.ToLower().Contains("unauthorized"))
        //    {
        //        throw new CustomExceptionHandler(Json("Unauthorized"));
        //    }
        //}

        public JsonResult compareUnmerge(IList<UnmergeCache> cartList)
        {
            if (cartList.Count > 0)
            {
                List<string> indexStrings = new List<string>();
                List<string> constNames = new List<string>();
                List<string> constMasterIds=  new List<string>();
                List<string> constAddresses = new List<string>();
                List<string> sourcesystemCds = new List<string>();
                List<string> sourceSystemIds = new List<string>();


                foreach(UnmergeCache cart in cartList){
                    indexStrings.Add(cart.IndexString);
                    constNames.Add(cart.cnst_nm);
                    constAddresses.Add(cart.address);
                    constMasterIds.Add(cart.cnst_mstr_id);
                    sourcesystemCds.Add(cart.source_system_cd);
                    sourceSystemIds.Add(cart.source_system_id);

                }


               Dictionary<string, List<string>> returnData = new Dictionary<string, List<string>>();
                returnData.Add("IndexStrings", indexStrings);
                returnData.Add("ConstNames",constNames);
                returnData.Add("ConstAddresses",constAddresses);
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
        public static List<T> getListCachedData(string cookieName,HttpRequestBase Request)
        {
            if (Request != null && Request.Cookies[cookieName] != null)
            {
                var CartString = Request.Cookies[cookieName].Value.ToString();
                if (!string.IsNullOrEmpty(CartString))
                {
                    return Models.Utility.StringToObject<List<T>>(CartString);
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
                    return Models.Utility.StringToObject<T>(CartString);
                }
            }

            return default(T);


        }



        public static void setListCacheData(List<T> data, string cookieName, HttpResponseBase Response)
        {
            string cookieTimeout = WebConfigurationManager.AppSettings["StuartCartCookieTimeout"];
            var finalResult = data;
            string cartListStr = Models.Utility.ObjectToString<IList<T>>(finalResult);
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
            string cookieTimeout = WebConfigurationManager.AppSettings["StuartCartCookieTimeout"];
            string str = "";
            var finalResult = data;
            str = Models.Utility.ObjectToString<T>(finalResult);

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


    }


    //[HttpPost]
    //public string WriteLocatorEmailRecentSearches(ListLocatorEmailInputSearchModel CaseModel)
    //{
    //    string Message = "Error, something happened while logging the Recent Searches. Please contact administrator.";
    //    if (!Utility.IsAnyPropertyNotNull(CaseModel)) return "Case Search Log: Nothing to log";

    //    List<ListCaseInputSearchModel> RecentSearchObj = new List<ListCaseInputSearchModel>();
    //    try
    //    {
    //        string CaseRecentSearchPath = WebConfigurationManager.AppSettings["CaseRecentSearchPath"];
    //        var RecentSearch = Utility.readJSONTFromFile<List<ListCaseInputSearchModel>>(CaseRecentSearchPath);
    //        var filteredRecentSearch = RecentSearch; // This is to filter out the recent searches based on the username
    //        if (RecentSearch != null)
    //        {
    //            //if (!User.GetUserName().Equals("") || User.GetUserName() != null)
    //            //{
    //            //    filteredRecentSearch = RecentSearch.Where(x => x.CaseInputSearchModel[0].LoggedInUser == User.GetUserName()).ToList();
    //            //    RecentSearch.RemoveAll(x => x.CaseInputSearchModel[0].LoggedInUser == User.GetUserName());
    //            //}

    //            //if (filteredRecentSearch.Count > 10)
    //            //{
    //            //    filteredRecentSearch.RemoveRange(10, filteredRecentSearch.Count - 10); // Remove all items greater than 10, since we show only top 10 recent searches
    //            //}
    //            //filteredRecentSearch.Insert(0, CaseModel);
    //            //RecentSearch.AddRange(filteredRecentSearch);
    //            //var SuccessMessage = Utility.writeJSONToFile<List<ListCaseInputSearchModel>>(RecentSearch, CaseRecentSearchPath);
    //            //if (SuccessMessage == "SUCCESS")
    //            //    Message = "The Case Recent searches were logged successfully";
    //            //else
    //            //    return Message;
    //        }
    //        else
    //        {
    //            RecentSearchObj.Add(CaseModel);
    //            var SuccessMessage = Utility.writeJSONToFile<List<ListCaseInputSearchModel>>(RecentSearchObj, CaseRecentSearchPath);
    //            if (SuccessMessage == "SUCCESS")
    //                Message = "The Case Recent searches were logged successfully";
    //            else
    //                return Message;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Message = "Erorr, " + ex.Message;
    //    }
    //    return Message;
    //}

}