using Newtonsoft.Json;
using OfficeOpenXml;
using Stuart_V2.Exceptions;
using Stuart_V2.Models;
using Stuart_V2.Models.Entities.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Stuart_V2.Controllers.MVC
{
    [RouteArea("NameAndEmailNative")]
    [RoutePrefix("")]
    public class NameAndEmailNativeController : BaseController
    {
        // GET: NameAndEmailUpload
        public ActionResult Index()
        {
            return View();
        }

        public void setNameAndEmailUserUploadParams(NameAndEmailUserUploadParams nameAndEmailUserParams)
        {
            TempData["NameAndEmailUserUplaodParams"] = nameAndEmailUserParams;
            //var result = new JsonResult();
            ////result.Data = "FileLimitExceeded"; // Return a Excel Upload Limit Exceeded
            //result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //return result;
        }


        [HandleException]
        [HttpPost()]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> postNameAndEmailUploadData(string uploadedNameAndEmailFormData)
        {

            uploadedNameAndEmailFormData = Request.Form[0];
            if (string.IsNullOrEmpty(uploadedNameAndEmailFormData)) return null;

            List<ChapterUploadFileDetailsHelper> _uploadJsonDetails = (List<ChapterUploadFileDetailsHelper>)TempData["UploadJsonDetails"];
            NameAndEmailUploadDetails gm = new NameAndEmailUploadDetails();

            var UploadedNameAndEmailFormData = JsonConvert.DeserializeObject<ListNameAndEmailUploadInput>(uploadedNameAndEmailFormData);
            gm.ListNameAndEmailUploadDetails = UploadedNameAndEmailFormData;
            gm.ListChapterUploadFileDetailsInput = _uploadJsonDetails;

            if (gm == null) return null;

            string url = BaseURL + "api/Upload/NameAndEmailUpload/";

            string res = await Models.Resource.PostResourceAsync(url, Token, gm, ClientID);

            return processResultForUploadInsertion(res, "Name and Email Upload");
        }


        [HandleException]
        [HttpPost()]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> ValidateNameAndEmailUploadFiles()
        {

            var temp = (NameAndEmailUserUploadParams)TempData["NameAndEmailUserUplaodParams"];

            int iUploadedCnt = 0;
            string fileName = "";

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            ListNameAndEmailUploadInput lgl = new ListNameAndEmailUploadInput();

            lgl.NameAndEmailUploadInputList = new List<NameAndEmailUploadInput>();

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

                    List<ChapterUploadFileDetailsHelper> UploadStatusFileDetails = new List<ChapterUploadFileDetailsHelper>();

                    var jsonresult = serializer.Deserialize<List<ChapterUploadFileDetailsHelper>>(JSONString);


                    //Populate json details to a list  if the Upload Status File is not null
                    if (jsonresult != null)
                    {
                        UploadStatusFileDetails = jsonresult;
                    }

                    string strFileName = "";
                    double intFileSize = 0;
                    string strDocType = "";
                    string strDocExtention = "";
                    int intBusinessType = 3;
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
                    if(!(strDocExtention.Equals(".xlsx") ||strDocExtention.Equals(".xls"))){
                        var result = new JsonResult();
                        result.Data = "IncorrectTemplate"; // Return a FileAlread
                        result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return result;
                    }



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
                        lgl.UploadedNameAndEmailFileInputInfo = new UploadedNameAndEmailFileInfo();
                        lgl.UploadedNameAndEmailFileInputInfo.fileName = fileName;
                        lgl.UploadedNameAndEmailFileInputInfo.fileExtension = System.IO.Path.GetExtension(hpf.FileName);
                        lgl.UploadedNameAndEmailFileInputInfo.intFileSize = hpf.ContentLength;
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
                                            .Range(ws.Dimension.Start.Column, 8 - ws.Dimension.Start.Column + 1)
                                            .ToDictionary(col => ws.Cells[headerCol, col].Value.ToString().Trim(), col => col);

                                        var keys = new[] { "Master Id", "Prefix", "First Name", "Middle Name", "Last Name", "Email", "Remove Indicator", "Notes" };

                                        //Check for the template
                                        bool check = keys.All(map.ContainsKey) && map.Keys.All(keys.Contains) && map.Keys.Count() == keys.Count();

                                        if (check)
                                        {

                                            for (int rw = 3; rw <= ws.Dimension.End.Row; rw++)
                                            {
                                                if (!ws.Cells[rw, 1, rw, 8].All(c => c.Value == null))
                                                {
                                                    NameAndEmailUploadInput input = new NameAndEmailUploadInput()
                                                    {
                                                        cnst_mstr_id = (ws.Cells[rw, map["Master Id"]].Value ?? (Object)"").ToString(),
                                                        cnst_prefix_nm = (ws.Cells[rw, map["Prefix"]].Value ?? (Object)"").ToString(),
                                                        cnst_first_nm = (ws.Cells[rw, map["First Name"]].Value ?? (Object)"").ToString(),
                                                        cnst_middle_nm = (ws.Cells[rw, map["Middle Name"]].Value ?? (Object)"").ToString(),
                                                        cnst_last_nm = (ws.Cells[rw, map["Last Name"]].Value ?? (Object)"").ToString(),
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


                                                    };
                                                    if (string.IsNullOrEmpty(input.rm_ind))
                                                    {
                                                        input.rm_ind = "0";
                                                    }
                                                    else if (input.rm_ind.ToUpper().Equals("Y") || input.rm_ind.Equals("1"))
                                                    {
                                                        input.rm_ind = "1";
                                                    }
                                                    else if (input.rm_ind.ToUpper().Equals("N") || input.rm_ind.Equals("0"))
                                                    {
                                                        input.rm_ind = "0";
                                                    }
                                                    else
                                                    {
                                                        input.rm_ind = "0";
                                                    }

                                                    lgl.NameAndEmailUploadInputList.Add(input);
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
                      

                        if (lgl.NameAndEmailUploadInputList.Count < 2001) //Check for the uploaded list count
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
                        if (lgl.NameAndEmailUploadInputList.Count == 0)
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
                //If you update the path here, update in web config as well
                
                string url = BaseURL + "api/Upload/NameAndEmailValidate/";
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