using Newtonsoft.Json;
using OfficeOpenXml;
using Orgler.Exceptions;
using Orgler.Models;
using Orgler.Models.Entities.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Orgler.Controllers.MVC
{
    [Authorize]
    public class EmailOnlyNativeController : BaseController
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        public void setEmailUserUploadParams(EmailOnlyUserUploadParams emailUserParams)
        {
            TempData["EmailOnlyUserUplaodParams"] = emailUserParams;
            //var result = new JsonResult();
            ////result.Data = "FileLimitExceeded"; // Return a Excel Upload Limit Exceeded
            //result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //return result;
        }


        [HandleException]
        [HttpPost()]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> postEmailOnlyUploadData(string uploadedEmailFormData)
        {

            uploadedEmailFormData = Request.Form[0];
            if (string.IsNullOrEmpty(uploadedEmailFormData)) return null;

            List<EmailUploadJsonFileDetailsHelper> _uploadJsonDetails = (List<EmailUploadJsonFileDetailsHelper>)TempData["UploadJsonDetails"];
            EmailOnlyUploadDetails gm = new EmailOnlyUploadDetails();

            var UploadedEmailFormData = JsonConvert.DeserializeObject<ListEmailOnlyUploadInput>(uploadedEmailFormData);
            gm.ListEmailOnlyUploadDetails = UploadedEmailFormData;
            gm.ListEmailUploadJsonFileDetailsInput = _uploadJsonDetails;

            if (gm == null) return null;

            string url = BaseURL + "api/Upload/PostEmailOnlyUploadData/";

            string res = await Models.Resource.PostResourceAsync(url, Token, gm, ClientID);
            return null;
        }


        [HandleException]
        [HttpPost()]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> ValidateEmailUploadFiles()
        {

            var temp = (EmailOnlyUserUploadParams)TempData["EmailOnlyUserUplaodParams"];

            int iUploadedCnt = 0;
            string fileName = "";

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            ListEmailOnlyUploadInput lgl = new ListEmailOnlyUploadInput();

            lgl.EmailOnlyUploadInputList = new List<EmailOnlyUploadInput>();

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    EmailUploadJsonFileDetailsHelper jsonhelper = new EmailUploadJsonFileDetailsHelper();
                    var serializer = new JavaScriptSerializer();
                    string JSONString = "";

                    // Get JSON data from the File
                    if (System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"]))
                    {
                        JSONString = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["UploadStatusPath"]);
                    }

                    List<EmailUploadJsonFileDetailsHelper> UploadStatusFileDetails = new List<EmailUploadJsonFileDetailsHelper>();

                    var jsonresult = serializer.Deserialize<List<EmailUploadJsonFileDetailsHelper>>(JSONString);


                    //Populate json details to a list  if the Upload Status File is not null
                    if (jsonresult != null)
                    {
                        UploadStatusFileDetails = jsonresult;
                    }

                    string strFileName = "";
                    double intFileSize = 0;
                    string strDocType = "";
                    string strDocExtention = "";
                    int intBusinessType = 2;
                    DateTime dateUploadStart = DateTime.Now;
                    DateTime dateUploadEnd = DateTime.Now;
                    string strUploadStatus = "";
                    string strEmailMessage = "";

                    bool UploadFlag = true;

                    // get document details
                    strFileName = hpf.FileName; //file name
                    intFileSize = hpf.ContentLength;
                    strDocExtention = System.IO.Path.GetExtension(hpf.FileName);// file extention

                    // Check if document was uploaded previously by referencing against the details obtained from the json
                    if (UploadStatusFileDetails.Count > 0)
                    {
                        foreach (var item in UploadStatusFileDetails)
                        {
                            // If business type is Email Only Upload and filename/filesize is the same as in the list, then the file is a duplicate
                            if (item.BusinessType == 2 && item.FileName == strFileName && item.FileSize == intFileSize)
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
                        lgl.UploadedEmailFileInputInfo = new UploadedEmailFileInfo();
                        lgl.UploadedEmailFileInputInfo.fileName = fileName;
                        lgl.UploadedEmailFileInputInfo.fileExtension = System.IO.Path.GetExtension(hpf.FileName);
                        lgl.UploadedEmailFileInputInfo.intFileSize = hpf.ContentLength;

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
                                        .Range(ws.Dimension.Start.Column, 3 - ws.Dimension.Start.Column + 1)
                                        .ToDictionary(col => ws.Cells[headerCol, col].Value.ToString().Trim(), col => col);

                                    var keys = new[] {"Email","Remove Indicator","Notes"};

                                    //Check for the template
                                    bool check = keys.All(map.ContainsKey) && map.Keys.All(keys.Contains) && map.Keys.Count() == keys.Count();

                                    if (check)
                                    {

                                        for (int rw = 3; rw <= ws.Dimension.End.Row; rw++)
                                        {
                                            if (!ws.Cells[rw, 1, rw, 3].All(c => c.Value == null))
                                            {
                                                lgl.EmailOnlyUploadInputList.Add(new EmailOnlyUploadInput()
                                                {                                                
                                                    cnst_email_addr = (ws.Cells[rw, map["Email"]].Value ?? (Object)"").ToString(),

                                                    rm_ind = (ws.Cells[rw, map["Remove Indicator"]].Value ?? (Object)"").ToString(),
                                                    notes = (ws.Cells[rw, map["Notes"]].Value ?? (Object)"").ToString(),

                                                    chpt_cd = temp.chapterCode.ToString(),
                                                    nk_ecode = temp.chapterCode.ToString(),
                                                    grp_cd = temp.groupCode.ToString(),
                                                    grp_nm = temp.groupName.ToString(),

                                                    grp_mbrshp_strt_dt = temp.startDate.ToString(),
                                                    grp_mbrshp_end_dt = temp.endDate.ToString(),
                                                    status = "In Progress"
                                                });
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

                        if (lgl.EmailOnlyUploadInputList.Count < 2001) //Check for the uploaded list count
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

                            UploadStatusFileDetails.Add(jsonhelper);

                            TempData["UploadJsonDetails"] = UploadStatusFileDetails;
                        }
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                //If you update the path here, update in web config as well
                //DEPLOYMENT - comment this line
                string url = BaseURL + "api/Upload/PostEmailUploadDetailsValidate/";
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