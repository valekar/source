using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stuart_V2.Models.Entities.Cem;
using System.Threading.Tasks;
using System.Web.Configuration;
using Stuart_V2.Models.Entities.Upload;
using System.Web.Script.Serialization;
using System.IO;
using OfficeOpenXml;
using Stuart_V2.Models;
using Stuart_V2.Exceptions;
using Newtonsoft.Json;
using Stuart_V2.Models.Entities.Admin;
using Stuart_V2.Models.Entities;

namespace Stuart_V2.Controllers.MVC
{
    [RouteArea("CemUploadNative")]
    [RoutePrefix("")]
    [Authorize]
    public class CemUploadNativeController  : BaseController
    {
        // GET: CemUpload
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult setDncParams(CemDncParams cemDncParams)
        {
            TempData["CemDncParams"] = cemDncParams;
            var result = new JsonResult();
            result.Data = "CemDncParams";
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }


        [HandleException]
        [HttpPost()]
        //[TabLevelSecurity("upload_tb_access","RW")]
        public async Task<JsonResult> dncUploadData()
        {

            DncValidationOutput uploadedFormData = (DncValidationOutput)TempData["CemUploadedRecords"];

            if (uploadedFormData.dncValidList == null) return null;

            List<ChapterUploadFileDetailsHelper> UploadJsonDetails = (List<ChapterUploadFileDetailsHelper>)TempData["UploadJsonDetails"];

            ListDncUploadInput listDncUploadInput = new ListDncUploadInput();
            listDncUploadInput.dncUploadInputList = uploadedFormData.dncValidList;

            DncUploadDetails dncUploadDetails = new DncUploadDetails();
            dncUploadDetails.ListDncUploadInput = listDncUploadInput;
            dncUploadDetails.ListChapterUploadFileDetailsInput = UploadJsonDetails;

            if (dncUploadDetails == null) return null;

            string url = BaseURL + "api/Upload/postdncupload/";

            string res = await Models.Resource.PostResourceAsync(url, Token, dncUploadDetails, ClientID);

            return processResultForUploadInsertion(res, "DNC Upload");
        }


        [HandleException]
        [HttpPost()]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> validateFiles()
        {
            var temp = (CemDncParams)TempData["CemDncParams"];
            int iUploadedCnt = 0;
            //string fileName = "";

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            ListDncUploadInput listDncInput = new ListDncUploadInput();
            listDncInput.dncUploadInputList = new List<DncUploadInput>();
            

            sPath = WebConfigurationManager.AppSettings["UploadFilePath"];

            DateTime dateUploadStart = DateTime.Now;
            bool isFileDuplicateVar = isFileDuplicate();

            if (!isFileDuplicateVar)
            {
                Stuart_V2.Models.FileDetails fileDetails= getFileDetails();
                listDncInput.dncFileInfo = new DncUploadedFileInfo();
                listDncInput.dncFileInfo.fileName = fileDetails.name;
                listDncInput.dncFileInfo.fileExtension = fileDetails.extension;
                listDncInput.dncFileInfo.intFileSize = fileDetails.contentLength;

                //check for extension of the file
                if (!(fileDetails.extension.Equals(".xlsx") || fileDetails.extension.Equals(".xls")))
                {
                    var result = new JsonResult();
                    result.Data = "IncorrectTemplate"; // Return a FileAlread
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return result;
                }
                try
                {
                    using (var excel = new ExcelPackage(fileDetails.InputStream))
                    {
                        var ws = excel.Workbook.Worksheets["Sheet1"];

                        var lastRow = ws.Dimension.End.Row;
                        while (lastRow >= 1)
                        {
                            var range = ws.Cells[lastRow, 1, lastRow, 21];
                            if (range.Any(c => c.Value != null))
                            {
                                break; //If they contain any data
                            }
                            lastRow--;
                        }

                        if (lastRow > 2000)
                        {
                            var result = new JsonResult();
                            FileLimit fileLimit = new FileLimit();
                            fileLimit.fileName = fileDetails.name;
                            fileLimit.message = "FileLimitExceeded";
                            fileLimit.rowCount = lastRow.ToString();
                            result.Data = fileLimit;// Return a Excel Upload Limit Exceeded
                            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                            return result;
                        }
                        else
                        {
                            try
                            {
                                //Read the file into memory
                                int headerCol = 2;
                                //var temp = TempData["CemDncParams"];
                                Dictionary<string, int> map = Enumerable
                                    .Range(ws.Dimension.Start.Column, 21 - ws.Dimension.Start.Column + 1)
                                    .ToDictionary(col => ws.Cells[headerCol, col].Value.ToString().Trim(), col => col);

                                var keys = new[] {"Master Id","Source System Code","Source System Id","Line of Service Code","Communication Channel",
                             "Prefix","First Name","Middle Name","Last Name","Suffix","Organization Name","Address Line 1","Address Line 2",
                              "City","State","Zip 5","Email Address","Phone Number Extension","Phone Number","Notes","Created By"
                              
                            };

                                //Check for the template

                                temp.loggedInUserName.ToString();

                                System.Diagnostics.Debug.Write(map.Keys);
                                System.Diagnostics.Debug.Write(keys);
                                System.Diagnostics.Debug.Write(map.Keys.Count());
                                System.Diagnostics.Debug.Write(keys.Count());

                                bool check = keys.All(map.ContainsKey) && map.Keys.All(keys.Contains) && map.Keys.Count() == keys.Count();
                                if (check)
                                {
                                    for (int rw = 3; rw <= lastRow; rw++)
                                    {
                                        if (!ws.Cells[rw, 1, rw, 21].All(c => c.Value == null))
                                        {
                                            DncUploadInput input = new DncUploadInput()
                                            {
                                                init_mstr_id = (ws.Cells[rw, map["Master Id"]].Value ?? (Object)"").ToString(),
                                                arc_srcsys_cd = (ws.Cells[rw, map["Source System Code"]].Value ?? (Object)"").ToString(),
                                                cnst_srcsys_id = (ws.Cells[rw, map["Source System Id"]].Value ?? (Object)"").ToString(),
                                                line_of_service_cd = (ws.Cells[rw, map["Line of Service Code"]].Value ?? (Object)"").ToString(),
                                                comm_chan = (ws.Cells[rw, map["Communication Channel"]].Value ?? (Object)"").ToString(),
                                                prefix_nm = (ws.Cells[rw, map["Prefix"]].Value ?? (Object)"").ToString(),
                                                prsn_frst_nm = (ws.Cells[rw, map["First Name"]].Value ?? (Object)"").ToString(),
                                                prsn_mid_nm = (ws.Cells[rw, map["Middle Name"]].Value ?? (Object)"").ToString(),
                                                prsn_lst_nm = (ws.Cells[rw, map["Last Name"]].Value ?? (Object)"").ToString(),
                                                suffix_nm = (ws.Cells[rw, map["Suffix"]].Value ?? (Object)"").ToString(),
                                                cnst_org_nm = (ws.Cells[rw, map["Organization Name"]].Value ?? (Object)"").ToString(),
                                                cnst_addr_line1 = (ws.Cells[rw, map["Address Line 1"]].Value ?? (Object)"").ToString(),
                                                cnst_addr_line2 = (ws.Cells[rw, map["Address Line 2"]].Value ?? (Object)"").ToString(),
                                                cnst_addr_city = (ws.Cells[rw, map["City"]].Value ?? (Object)"").ToString(),
                                                cnst_addr_state = (ws.Cells[rw, map["State"]].Value ?? (Object)"").ToString(),
                                                cnst_addr_zip5 = (ws.Cells[rw, map["Zip 5"]].Value ?? (Object)"").ToString(),
                                                cnst_email_addr = (ws.Cells[rw, map["Email Address"]].Value ?? (Object)"").ToString(),
                                                cnst_phn_num = (ws.Cells[rw, map["Phone Number"]].Value ?? (Object)"").ToString(),
                                                cnst_extn_phn_num = (ws.Cells[rw, map["Phone Number Extension"]].Value ?? (Object)"").ToString(),
                                                notes = (ws.Cells[rw, map["Notes"]].Value ?? (Object)"").ToString(),
                                                created_by = (ws.Cells[rw, map["Created By"]].Value ?? temp.loggedInUserName).ToString(),

                                            };


                                            listDncInput.dncUploadInputList.Add(input);
                                        }
                                        else
                                        {
                                            throw new Exception();
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                var result = new JsonResult();
                                result.Data = "IncorrectTemplate"; // Return a FileAlread
                                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                return result;
                            }
                        }//end of else check of number of rows

                        iUploadedCnt = iUploadedCnt + 1; //Increase the count by 1    
                    }// end of using ExcelPackage

                }
                catch (Exception ex)
                {
                    var result = new JsonResult();
                    result.Data = "EmptyFileUploaded"; // Return a FileAlread
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return result;
                }
                 
                 if (listDncInput.dncUploadInputList.Count < 2001) //Check for the uploaded list count
                 {
                     storeUploadedFileDetails(dateUploadStart, fileDetails);
                 }

                 if (listDncInput.dncUploadInputList.Count == 0)
                 {
                     var result = new JsonResult();
                     result.Data = "EmptyFileUploaded"; // Return a FileAlread
                     result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                     return result;
                 }

            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                string url = BaseURL + "api/Upload/dncvalidate/";
                string res = await Models.Resource.PostResourceAsync(url, Token, listDncInput, ClientID);
                return handleTrivialHttpRequestsForValidation(res);
            }
            else
            {
                var result = new JsonResult();
                result.Data = "FileAlreadyUploaded"; // Return a FileAlread
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
          
        }

        private void storeUploadedFileDetails(DateTime startDate, Stuart_V2.Models.FileDetails fileDetails)
        {
            ChapterUploadFileDetailsHelper jsonhelper = new ChapterUploadFileDetailsHelper();

            DateTime endDate = DateTime.Now;
            string strUploadStatus = "Success";
            string strDocType = "Excel";
            string strEmailMessage = "The File, " + fileDetails.name + " has been uploaded to the server successfully!";
            var temp = (CemDncParams)TempData["CemDncParams"];

            var UploadJsonDetails = (List<ChapterUploadFileDetailsHelper>)TempData["UploadJsonDetails"];

            jsonhelper.UserName = temp.loggedInUserName.ToString(); 
            jsonhelper.FileName = fileDetails.name;
            jsonhelper.FileSize = fileDetails.contentLength ;
            jsonhelper.FileType = strDocType;
            jsonhelper.FileExtention = fileDetails.extension;
            jsonhelper.BusinessType = 4;
            jsonhelper.UploadStart = startDate.ToString();
            jsonhelper.UploadEnd = endDate.ToString();
            jsonhelper.UploadStatus = strUploadStatus;
            jsonhelper.TransactionKey = "";


            UploadJsonDetails.Add(jsonhelper);
            TempData["UploadJsonDetails"] = UploadJsonDetails;

        }

        private System.Web.HttpPostedFile getOneFile(){
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            return hfc[hfc.Count-1];
        }
 


        private Stuart_V2.Models.FileDetails getFileDetails(){
            System.Web.HttpPostedFile hpf = getOneFile();
            Stuart_V2.Models.FileDetails fileDetails = new Stuart_V2.Models.FileDetails();
            fileDetails.name = Path.GetFileName(hpf.FileName);
            fileDetails.extension = System.IO.Path.GetExtension(hpf.FileName);
            fileDetails.contentLength =  hpf.ContentLength;
            fileDetails.InputStream = hpf.InputStream;
            return fileDetails;
        }


      

        private bool isFileDuplicate()
        {
           // string fileName = "";
            //System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            bool UploadFlag = false;
            System.Web.HttpPostedFile hpf = getOneFile();

            if (hpf.ContentLength > 0)
            {
               
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
                   
                string strDocExtention = "";

                // get document details
                strFileName = hpf.FileName; //file name
                intFileSize = hpf.ContentLength;
                strDocExtention = System.IO.Path.GetExtension(hpf.FileName);// file extention

                // Check if document was uploaded previously by referencing against the details obtained from the json
                if (UploadDetails.Count > 0)
                {
                    foreach (var item in UploadDetails)
                    {
                        // If business type is Chapter Upload and filename/filesize is the same as in the list, then the file is a duplicate
                        if (item.BusinessType == 4 && item.FileName == strFileName && item.FileSize == intFileSize)
                        {
                            if (item.UploadStatus != "Success")
                            {
                                UploadFlag = false;
                            }
                            else
                            {
                                UploadFlag = true;
                                break;
                            }
                        }
                    }                   
                }
               // return UploadFlag;    

                //store the upload content in temporary data
                TempData["UploadJsonDetails"] = UploadDetails;
            }

            

            return UploadFlag;
        }


        private JsonResult handleTrivialHttpRequestsForValidation(string res)
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

                // if (result.dncInvalidList == null || )


                DncValidationOutput dncOP = JsonConvert.DeserializeObject<DncValidationOutput>(res);

                if (dncOP.dncInvalidList == null || dncOP.dncInvalidList.Count == 0)
                {
                    TempData["CemUploadedRecords"] = dncOP;
                }


                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }


        }

       

    }
}