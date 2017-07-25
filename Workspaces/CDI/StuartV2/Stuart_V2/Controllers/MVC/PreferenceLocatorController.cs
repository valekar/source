using Newtonsoft.Json;
using Stuart_V2.Exceptions;
using Stuart_V2.Models;
using Stuart_V2.Models.Entities.Cem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Stuart_V2.Controllers.MVC
{
    [RouteArea("preference")]
    [RoutePrefix("")]
    [Authorize]
    public class PreferenceLocatorController : BaseController
    {


        // GET: PreferenceLocator
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HandleException]
        [Route("locator/{masterId}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getPreferenceLocator(string masterId)
        {
            if (string.IsNullOrWhiteSpace(masterId)) return Json("NoID");
            string url = BaseURL + "api/Preference/GetPreferenceLocator/" + masterId;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            JavaScriptSerializer serializer = serializer = new JavaScriptSerializer();
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<PreferenceLocator> prefLocList = serializer.Deserialize<List<PreferenceLocator>>(res);
                ProcessCEM<PreferenceLocator> process = new ProcessCEM<PreferenceLocator>();
                List<PreferenceLocator> convertedPrefLocList = process.convertRecords(prefLocList, "PREF_LOCATOR");
                return Json(convertedPrefLocList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [HandleException]
        [Route("locator/all/{masterId}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getAllPreferenceLocator(string masterId)
        {
            if (string.IsNullOrWhiteSpace(masterId)) return Json("NoID");
            string url = BaseURL + "api/Preference/GetAllPreferenceLocator/" + masterId;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HttpPost]
        [HandleException]
        [Route("locator")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addPreferenceLocator(PreferenceLocatorInput prefLocInput)
        {
            string url = BaseURL + "api/Preference/AddPreferenceLocator";
            string res = await Models.Resource.PostResourceAsync(url, Token, prefLocInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);
        }


        [HttpPost]
        [HandleException]
        [Route("locator/edit")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> editPreferenceLocator(PreferenceLocatorInput prefLocInput)
        {
            string url = BaseURL + "api/Preference/EditPreferenceLocator";
            string res = await Models.Resource.PostResourceAsync(url, Token, prefLocInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);
        }

        [HttpPost]
        [HandleException]
        [Route("locator/delete")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deletePreferenceLocator(PreferenceLocatorInput prefLocInput)
        {
            string url = BaseURL + "api/Preference/DeletePreferenceLocator";
            string res = await Models.Resource.PostResourceAsync(url, Token, prefLocInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [Route("dropdowns/{id}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getPreferenceLocatorDropDowns(string id)
        {


            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Preference/GetPreferenceLocatorOptions/" + id;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);


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


        //private JsonResult handleTrivialHttpRequests(string res)
        //{

        //    if (!res.Equals("") && res != null)
        //    {
        //        if (res.ToLower().Contains("timedout"))
        //        {
        //            throw new CustomExceptionHandler(Json("TimedOut"));
        //        }
        //        if (res.ToLower().Contains("error"))
        //        {
        //            throw new CustomExceptionHandler(Json("DatabaseError"));
        //        }
        //        if (res.ToLower().Contains("unauthorized"))
        //        {
        //            throw new CustomExceptionHandler(Json("Unauthorized"));
        //        }
        //        var result = (new JavaScriptSerializer()).DeserializeObject(res);
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json("", JsonRequestBehavior.AllowGet);
        //    }
        //}

       
    }


    class ProcessCEM<T>
    {
        private string status {get;set;}
        private string trans_key {get;set;}
        private string is_previous {get;set;}
        private string row_stat_cd {get;set;}
        private string transNotes {get;set;}
        private string trans_status {get;set;}
        private string inactive_ind {get;set;}
        private string arc_srcsys_cd {get;set;}
        private string user_id { get; set; }


        public ProcessCEM()
        {
            this.transNotes = "";
        }

        public List<T> convertRecords(List<T> records, string type)
        {
            List<T> newRecords = new List<T>();

            if (type.Equals("PREF_LOCATOR"))
            {
                HashSet<PreferenceLocator> distinctRecords = new HashSet<PreferenceLocator>();
                int max = -1;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    PreferenceLocator prefLoc = (PreferenceLocator)objectRecord;
                    this.status = prefLoc.status;
                    this.trans_key = prefLoc.transaction_key;
                    this.is_previous = prefLoc.is_previous;
                    this.row_stat_cd = prefLoc.row_stat_cd;
                    this.trans_status = prefLoc.trans_status;
                    this.inactive_ind = prefLoc.inactive_ind;
                    this.arc_srcsys_cd = prefLoc.arc_srcsys_cd;
                    this.user_id = prefLoc.user_id;
                    prefLoc.transNotes = getTransNotes();
                    distinctRecords.Add(prefLoc);
                   
                    int c = distinctRecords.Where(x => !x.transNotes.ToLower().Contains("deleted") && x.cnst_pref_loc_end_ts.Contains("9999")).ToList().Count;
                    if (max < c )
                    {
                        max = c;
                    }
                    

                }

                foreach (T record in records)
                {
                    Object obj = record;
                    PreferenceLocator pref = (PreferenceLocator)obj;
                    pref.distinct_records_count = max + "";
                }              
            }
            else if (type.Equals("MESSAGE_PREFERENCE"))
            {
                HashSet<MessagePreference> distinctRecords = new HashSet<MessagePreference>();
                int max = -1;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    MessagePreference msgPref = (MessagePreference)objectRecord;
                    this.status = msgPref.status;
                    this.trans_key = msgPref.transaction_key;
                    this.is_previous = msgPref.is_previous;
                    this.row_stat_cd = msgPref.row_stat_cd;
                    this.trans_status = msgPref.trans_status;
                    this.inactive_ind = msgPref.inactive_ind;
                    this.arc_srcsys_cd = msgPref.arc_srcsys_cd;
                    this.user_id = msgPref.user_id;
                    msgPref.transNotes = getTransNotes();

                    distinctRecords.Add(msgPref);
                    int c = distinctRecords.Where(x => !x.transNotes.ToLower().Contains("deleted") && x.msg_pref_end_ts.Contains("9999")).ToList().Count;

                    if (max < c)
                    {
                        max = c;
                    }
                    
                }

                foreach (T record in records)
                {
                    Object obj = record;
                    MessagePreference pref = (MessagePreference)obj;
                    pref.distinct_records_count = max + "";
                }     
            }
            else if (type.Equals("DO_NOT_CONTACT"))
            {
                HashSet<DoNotContact> distinctRecords = new HashSet<DoNotContact>();
                int max = -1;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    DoNotContact dnc = (DoNotContact)objectRecord;
                    this.trans_key = dnc.transaction_key;
                    this.is_previous = dnc.is_previous;
                    this.row_stat_cd = dnc.strx_row_stat_cd;
                    this.trans_status = dnc.trans_status;
                    this.inactive_ind = dnc.inactive_ind;
                    this.user_id = dnc.user_id;
                    dnc.transNotes = getTransNotes();

                    distinctRecords.Add(dnc);

                    int c = distinctRecords.Where(x => !x.transNotes.ToLower().Contains("deleted") && x.cnst_dnc_end_ts.Contains("9999")).ToList().Count;

                    if (max < c)
                    {
                        max = c;
                    }
                    
                }

                foreach (T record in records)
                {
                    Object obj = record;
                    DoNotContact pref = (DoNotContact)obj;
                    pref.distinct_records_count = max + "";
                }
            }
            else if (type.Equals("GROUP_MEMBERSHIP"))
            {
                HashSet<string> distinctRecords = new HashSet<string>();
                int max = 0;
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    GroupMembership obj = (GroupMembership)objectRecord;
                    this.trans_status = obj.trans_status != null ? obj.trans_status : string.Empty;
                    this.is_previous = obj.is_previous != null ? obj.is_previous : "0";
                    this.arc_srcsys_cd = obj.arc_srcsys_cd != null ? obj.arc_srcsys_cd : string.Empty;
                    this.user_id = obj.user_id != null ? obj.user_id : string.Empty;
                    this.trans_key = obj.transaction_key != null ? obj.transaction_key : string.Empty;
                    this.inactive_ind = obj.inactive_ind != null ? obj.inactive_ind : string.Empty;
                    obj.transNotes = getTransNotes();
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
                                myObj.distinct_records_count = max + "";
                            }
                        }
                    }
                }

                // we neeed to change the code for this. Performance issue
                foreach (T record in records)
                {
                    Object objectRecord = record;
                    GroupMembership myObj = (GroupMembership)objectRecord;
                    myObj.distinct_records_count = max + "";
                }
            }

            //return the modified records
            return records;

        }
            private string getTransNotes()
            {
                this.transNotes = "";
                if (!String.IsNullOrEmpty(this.inactive_ind)) 
                {
                    if (this.inactive_ind.Equals("-1") && !this.arc_srcsys_cd.Equals("CDIM"))
                    {
                        this.transNotes = "Inactive/Soft-Deleted records cannot be edited.";
                    }
                }

                else if(!String.IsNullOrEmpty(this.trans_status))
                {
                    if (this.trans_status.Equals("Waiting for approval") || this.trans_status.Equals("In Progress"))
                    {
                        if (is_previous.Equals("1"))
                        {
                            this.transNotes = "Transaction #" + this.trans_key + ". Previous record in a change request by " + this.user_id;
                        }
                        else
                        {
                            this.transNotes = "Transaction #" + this.trans_key + ". New record in a change request by " + this.user_id;
                        }
                    }
                }
                    
                   

                return this.transNotes;
            }

    }
}