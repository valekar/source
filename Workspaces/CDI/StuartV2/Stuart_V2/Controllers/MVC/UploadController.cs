using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Stuart_V2.Models;
using OfficeOpenXml;
using Newtonsoft.Json;
using Stuart_V2.Exceptions;
using System.Web.Configuration;
using System.IO;
using Stuart_V2.Models.Entities.Upload;
using System.Dynamic;

namespace Stuart_V2.Controllers.MVC
{
    [RouteArea("UploadNative")]
    [RoutePrefix("")]
    [Authorize]
    public class UploadNativeController : BaseController
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> setGroupMembershipUploadParams(GroupMembershipUploadParams arg)
        {
            TempData["GroupMembershipUserUplaodDetails"] = arg;
            var result = new JsonResult();
            result.Data = "FileLimitExceeded"; // Return a Excel Upload Limit Exceeded
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }


        [HandleException]
        [HttpPost()]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> UploadGroupMembershipData1(string uploadedFormData)
        {

            uploadedFormData = Request.Form[0];
            if (string.IsNullOrEmpty(uploadedFormData)) return null;

            List<ChapterUploadFileDetailsHelper> _uploadJsonDetails = (List<ChapterUploadFileDetailsHelper>)TempData["UploadJsonDetails"];
            GroupMembershipUploadDetails gm = new GroupMembershipUploadDetails();

            var UploadedFormData = JsonConvert.DeserializeObject<ListGroupMembershipUploadInput>(uploadedFormData);
            gm.ListGroupMembershipUploadDetailsInput = UploadedFormData;
            gm.ListChapterUploadFileDetailsInput = _uploadJsonDetails;

            if (gm == null) return null;

            string url = BaseURL + "api/Upload/groupmembershipupload/";

            string res = await Models.Resource.PostResourceAsync(url, Token, gm, ClientID);
            return processResultForUploadInsertion(res, "Group Membership Upload");
        }

        [HandleException]
        [HttpPost()]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> UploadGroupMembershipData(ListGroupMembershipUploadInput uploadedFormData, List<string> uploadedNkecodes, string username)
        {
 
            List<ChapterUploadFileDetailsHelper> _uploadJsonDetails = (List<ChapterUploadFileDetailsHelper>)TempData["UploadJsonDetails"];

            GroupMembershipUploadDetails gm = new GroupMembershipUploadDetails();

            gm.ListGroupMembershipUploadDetailsInput = uploadedFormData;
            gm.ListChapterUploadFileDetailsInput = _uploadJsonDetails;

            string url = BaseURL + "api/Upload/groupmembershipupload/";

            string res = await Models.Resource.PostResourceAsync(url, Token, gm, ClientID);
            return processResultForUploadInsertion(res, "Group Membership Upload");
        }

        [HandleException]
        [HttpPost()]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> ValidateFiles()
        {

            var temp = (GroupMembershipUploadParams)TempData["GroupMembershipUserUplaodDetails"];

            int iUploadedCnt = 0;
            string fileName = "";

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";

            sPath = WebConfigurationManager.AppSettings["UploadFilePath"];
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            ListGroupMembershipUploadInput lgl = new ListGroupMembershipUploadInput();

            lgl.GroupMembershipUploadInputList = new List<GroupMembershipUploadInput>();

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                        ChapterUploadFileDetailsHelper jsonhelper = new ChapterUploadFileDetailsHelper();
                        var serializer = new JavaScriptSerializer();
                        string JSONString = "";

                        // Get JSON data from the File
                    if (System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"]))
                        {
                        JSONString = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"]);
                        }

                        List<ChapterUploadFileDetailsHelper> UploadDetails = new List<ChapterUploadFileDetailsHelper>();

                        var jsonresult = serializer.Deserialize<List<ChapterUploadFileDetailsHelper>>(JSONString);


                        //Populate json details to a list 
                        if (jsonresult != null)
                        {
                            UploadDetails = jsonresult;
                        }

                        string strFileName = "";
                        double intFileSize = 0;
                        string strDocType = "";
                        string strDocExtention = "";
                        int intBusinessType = 1;//Convert.ToInt16(SessionUtils.dictTypeCodes["Upload Type"]["Group Membership"]);  // 1 for chapter upload
                        DateTime dateUploadStart = DateTime.Now;
                        DateTime dateUploadEnd = DateTime.Now;
                        string strUploadStatus = "";
                        string strEmailMessage = "";

                        bool UploadFlag = true;
                
                        // get document details
                        strFileName = hpf.FileName; //file name
                        intFileSize = hpf.ContentLength;
                        strDocExtention = System.IO.Path.GetExtension(hpf.FileName);// file extention


                        //check for extension of the file
                        if (!(strDocExtention.Equals(".xlsx") || strDocExtention.Equals(".xls")))
                        {
                            var result = new JsonResult();
                            result.Data = "IncorrectTemplate"; // Return a FileAlread
                            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                            return result;
                        }

                        // Check if document was uploaded previously by referencing against the details obtained from the json
                        if (UploadDetails.Count > 0)
                        {
                            foreach (var item in UploadDetails)
                            {
                                // If business type is Chapter Upload and filename/filesize is the same as in the list, then the file is a duplicate
                                if (item.BusinessType == 1 && item.FileName == strFileName && item.FileSize == intFileSize)
                                {
                                    if (item.UploadStatus != "Success")
                                    {
                                        UploadFlag = true;
                                    }
                                    else
                                    {
                                        UploadFlag = false;
                                        break;
                                    }
                                }
                            }
                        }

                        if (UploadFlag)    // If the file is not a duplicate, perform below actions
                        {
                            fileName = Path.GetFileName(hpf.FileName);
                            lgl.UploadedFileInputInfo = new UploadedFileInfo();
                            lgl.UploadedFileInputInfo.fileName = fileName;
                            lgl.UploadedFileInputInfo.fileExtension = System.IO.Path.GetExtension(hpf.FileName);
                            lgl.UploadedFileInputInfo.intFileSize = hpf.ContentLength;
                            try
                            {
                                using (var excel = new ExcelPackage(hpf.InputStream))
                                {
                                    var ws = excel.Workbook.Worksheets["Sheet1"];

                                    var lastRow = ws.Dimension.End.Row;
                                    while (lastRow >= 1)
                                    {
                                        var range = ws.Cells[lastRow, 1, lastRow, 24];
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
                                            int headerCol = 2;

                                            Dictionary<string, int> map = Enumerable
                                                .Range(ws.Dimension.Start.Column, 24 - ws.Dimension.Start.Column + 1)
                                                .ToDictionary(col => ws.Cells[headerCol, col].Value.ToString().Trim(), col => col);

                                            var keys = new[] {"Master Id","Prefix","First Name","Middle Name","Last Name","Address Line 1 A",
                                                    "Address Line 2 A","City A","State A","Zip A","Address Line 1 B","Address Line 2 B",
                                                    "City B","State B","Zip B","Phone A","Phone B","Phone C","Email A","Email B",
                                                    "Job Title","Company Name","Remove Indicator","Notes"};

                                            //Check for the template
                                            bool check = keys.All(map.ContainsKey) && map.Keys.All(keys.Contains) && map.Keys.Count() == keys.Count();

                                            if (check)
                                            {

                                                for (int rw = 3; rw <= ws.Dimension.End.Row; rw++)
                                                {
                                                    if (!ws.Cells[rw, 1, rw, 24].All(c => c.Value == null))
                                                    {
                                                        GroupMembershipUploadInput grp = new GroupMembershipUploadInput()
                                                        {
                                                            cnst_mstr_id = (ws.Cells[rw, map["Master Id"]].Value ?? (Object)"").ToString(),
                                                            cnst_prefix_nm = (ws.Cells[rw, map["Prefix"]].Value ?? (Object)"").ToString(),
                                                            cnst_first_nm = (ws.Cells[rw, map["First Name"]].Value ?? (Object)"").ToString(),
                                                            cnst_middle_nm = (ws.Cells[rw, map["Middle Name"]].Value ?? (Object)"").ToString(),
                                                            cnst_last_nm = (ws.Cells[rw, map["Last Name"]].Value ?? (Object)"").ToString(),
                                                            cnst_addr1_street1 = (ws.Cells[rw, map["Address Line 1 A"]].Value ?? (Object)"").ToString(),
                                                            cnst_addr1_street2 = (ws.Cells[rw, map["Address Line 2 A"]].Value ?? (Object)"").ToString(),
                                                            cnst_addr1_city = (ws.Cells[rw, map["City A"]].Value ?? (Object)"").ToString(),
                                                            cnst_addr1_state = (ws.Cells[rw, map["State A"]].Value ?? (Object)"").ToString(),
                                                            cnst_addr1_zip = (ws.Cells[rw, map["Zip A"]].Value ?? (Object)"").ToString(),
                                                            cnst_addr2_street1 = (ws.Cells[rw, map["Address Line 1 B"]].Value ?? (Object)"").ToString(),
                                                            cnst_addr2_street2 = (ws.Cells[rw, map["Address Line 2 B"]].Value ?? (Object)"").ToString(),
                                                            cnst_addr2_city = (ws.Cells[rw, map["City B"]].Value ?? (Object)"").ToString(),
                                                            cnst_addr2_state = (ws.Cells[rw, map["State B"]].Value ?? (Object)"").ToString(),
                                                            cnst_addr2_zip = (ws.Cells[rw, map["Zip B"]].Value ?? (Object)"").ToString(),
                                                            cnst_phn1_num = (ws.Cells[rw, map["Phone A"]].Value ?? (Object)"").ToString(),
                                                            cnst_phn2_num = (ws.Cells[rw, map["Phone B"]].Value ?? (Object)"").ToString(),
                                                            cnst_phn3_num = (ws.Cells[rw, map["Phone C"]].Value ?? (Object)"").ToString(),
                                                            cnst_email1_addr = (ws.Cells[rw, map["Email A"]].Value ?? (Object)"").ToString(),
                                                            cnst_email2_addr = (ws.Cells[rw, map["Email B"]].Value ?? (Object)"").ToString(),
                                                            job_title = (ws.Cells[rw, map["Job Title"]].Value ?? (Object)"").ToString(),
                                                            company_nm = (ws.Cells[rw, map["Company Name"]].Value ?? (Object)"").ToString(),
                                                            rm_ind = (ws.Cells[rw, map["Remove Indicator"]].Value ?? (Object)"").ToString(),

                                                            notes = (ws.Cells[rw, map["Notes"]].Value ?? (Object)"").ToString(),

                                                            chpt_cd = temp.chapterCode.ToString(),
                                                            nk_ecode = temp.chapterCode.ToString(),
                                                            grp_cd = temp.groupCode.ToString(),
                                                            grp_nm = temp.groupName.ToString(),
                                                            //created_by = temp.createdBy.ToString(),
                                                            //created_dt = temp.createdDate.ToString(),
                                                            losCode = "FR", //Hard-coded to FR
                                                            grp_mbrshp_strt_dt = temp.startDate.ToString(),
                                                            grp_mbrshp_end_dt = temp.endDate.ToString(),
                                                            status = "In Progress"
                                                        };
                                                        if (string.IsNullOrEmpty(grp.rm_ind))
                                                        {
                                                            grp.rm_ind = "0";
                                                        }
                                                        else if (grp.rm_ind.ToUpper().Equals("Y") || grp.rm_ind.Equals("1"))
                                                        {
                                                            grp.rm_ind = "1";
                                                        }
                                                        else if (grp.rm_ind.ToUpper().Equals("N") || grp.rm_ind.Equals("0"))
                                                        {
                                                            grp.rm_ind = "0";
                                                        }
                                                        else
                                                        {
                                                            grp.rm_ind = "0";
                                                        }
                                                        lgl.GroupMembershipUploadInputList.Add(grp);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var result = new JsonResult();
                                                result.Data = "IncorrectTemplate"; // Return a FileAlread
                                                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                                return result;
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            var result = new JsonResult();
                                            result.Data = "IncorrectTemplate"; // Return a FileAlread
                                            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                            return result;
                                        }
                                    }


                                    iUploadedCnt = iUploadedCnt + 1; //Increase the count by 1
                                }
                            }
                            catch (Exception ex)
                            {
                                var result = new JsonResult();
                                result.Data = "EmptyFileUploaded"; // Return a FileAlread
                                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                return result;
                            }
                            

                            if (lgl.GroupMembershipUploadInputList.Count < 2001) //Check for the uploaded list count
                            {
                                dateUploadEnd = DateTime.Now;
                                strUploadStatus = "Success";
                                strDocType = "Excel";
                                strEmailMessage = "The File, " + strFileName + " has been uploaded to the server successfully!";

                                jsonhelper.UserName = temp.loggedInUserName;// "dixit.jain";//Hard - coded for now
                                jsonhelper.FileName = strFileName;
                                jsonhelper.FileSize = intFileSize;
                                jsonhelper.FileType = strDocType;
                                jsonhelper.FileExtention = strDocExtention;
                                jsonhelper.BusinessType = intBusinessType;
                                jsonhelper.UploadStart = dateUploadStart.ToString();
                                jsonhelper.UploadEnd = dateUploadEnd.ToString();
                                jsonhelper.UploadStatus = strUploadStatus;
                                jsonhelper.TransactionKey = "";

                                UploadDetails.Add(jsonhelper);

                                TempData["UploadJsonDetails"] = UploadDetails;
                            }

                            if (lgl.GroupMembershipUploadInputList.Count == 0)
                            {
                                var result = new JsonResult();
                                result.Data = "EmptyFileUploaded"; // Return a FileAlread
                                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                return result;
                            }
                        }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                string url = BaseURL + "api/Upload/groupmembershipvalidate/";
                string res = await Models.Resource.PostResourceAsync(url, Token, lgl, ClientID);
                return handleTrivialHttpRequests(res);
            }
            else
            {
                var result = new JsonResult();
                result.Data = "FileAlreadyUploaded"; // Return a FileAlread
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
        }


        [HttpPost]
        [HandleException]
        [Route("listUploadSearch")]
        [TabLevelSecurity("upload_tb_access", "RW", "R")]
        public async Task<JsonResult> listUploadSearch(ListOfListUploadSearchInput listUploadInputList)
        {
            listUploadInputList.answerLimit = "2000";
            string url = BaseURL + "api/Upload/listsearch";
            string res = await Models.Resource.PostResourceAsync(url, Token, listUploadInputList, ClientID);
            return handleTrivialHttpRequests(res); 

        }

        [HandleException]
        [Route("exportListUploadSearch/{id}")]
        [TabLevelSecurity("upload_tb_access", "RW", "R")]
        public async Task<FileContentResult> exportListUploadSearch(ListOfListUploadSearchInput listUploadInputList)
        {
            listUploadInputList.answerLimit = "2000";
            string url = BaseURL + "api/Upload/listsearch/";
            string res = await Models.Resource.PostResourceAsync(url, Token, listUploadInputList, ClientID);
            checkExceptions(res);
            List<ListUploadOutput> result = (new JavaScriptSerializer()).Deserialize<List<ListUploadOutput>>(res);

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("List Upload Search ");
                worksheet.Cells["A1"].Value = "List Upload Search";
                worksheet.Cells["A2"].LoadFromCollection(result, true);
                return new FileContentResult(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }


        }


        [HandleException]
        [Route("getTransExportAllUpload/{id}")]
        [TabLevelSecurity("transaction_tb_access", "RW", "R")]
        public async Task<FileContentResult> getTransExportAllUpload(string id)
        {
           
            string url = BaseURL + "api/Upload/GetUploadDetailsTransExportDetails/" + id;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
          
            checkExceptions(res);
            List<Stuart_V2.Models.Entities.Transaction.TransactionUploadDetails> result = (new JavaScriptSerializer()).Deserialize<List<Stuart_V2.Models.Entities.Transaction.TransactionUploadDetails>>(res);

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Transaction Details");
                worksheet.Cells["A1"].Value = "Transaction Details Results";
                worksheet.Cells["A2"].LoadFromCollection(result, true);
                return new FileContentResult(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
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
}