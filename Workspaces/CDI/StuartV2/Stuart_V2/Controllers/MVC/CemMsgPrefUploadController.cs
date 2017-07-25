using Newtonsoft.Json;
using OfficeOpenXml;
using Stuart_V2.Exceptions;
using Stuart_V2.Models;
using Stuart_V2.Models.Entities.Cem;
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
    [RouteArea("upload/message/preference")]
    [RoutePrefix("")]
    [Authorize]
    public class MessagePrefUploadNativeController : BaseController
    {
        // GET: CemMsgPrefUpload
        public ActionResult Index()
        {
            return View();
        }

        [Route("add/params")]
        public JsonResult setMsgPrefParams(UploadParams msgPrefParams)
        {
            TempData["MessagePrefParams"] = msgPrefParams;
            var result = new JsonResult();
            result.Data = "MessagePrefParams";
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }


        [HandleException]
        [Route("add/data")]
        [HttpPost]
        //[TabLevelSecurity("upload_tb_access","RW")]
        public async Task<JsonResult> msgPrefUploadData()
        {

            MsgPrefValidationOutput uploadedFormData = (MsgPrefValidationOutput)TempData["MsgPrefUploadedRecords"];

            if (uploadedFormData.msgPrefValidList == null) return null;

            List<ChapterUploadFileDetailsHelper> UploadJsonDetails = (List<ChapterUploadFileDetailsHelper>)TempData["UploadJsonDetails"];

            ListMsgPrefUploadInput listMsgPrefUploadInput = new ListMsgPrefUploadInput();
            listMsgPrefUploadInput.msgPrefUploadInputList = uploadedFormData.msgPrefValidList;

            MsgPrefUploadDetails msgPrefUploadDetails = new MsgPrefUploadDetails();
            msgPrefUploadDetails.ListMsgPrefUploadInput = listMsgPrefUploadInput;
            msgPrefUploadDetails.ListChapterUploadFileDetailsInput = UploadJsonDetails;

            if (msgPrefUploadDetails == null) return null;

            string url = BaseURL + "api/Upload/message/preference/PostMsgPrefUpload/";

            string res = await Models.Resource.PostResourceAsync(url, Token, msgPrefUploadDetails, ClientID);

            return processResultForUploadInsertion(res, "Message Preference Upload");
        }


        [HandleException]
        [Route("validate")]
        [HttpPost]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> validateFiles()
        {
            var temp = (UploadParams)TempData["MessagePrefParams"];
            int iUploadedCnt = 0;
            //string fileName = "";

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            ListMsgPrefUploadInput listmsgPrefInput = new ListMsgPrefUploadInput();
            listmsgPrefInput.msgPrefUploadInputList = new List<MsgPrefUploadInput>();


            sPath = WebConfigurationManager.AppSettings["UploadFilePath"];

            DateTime dateUploadStart = DateTime.Now;
            int intBusinessType = 5;
            bool isFileDuplicateVar = Utility.isFileDuplicate(TempData, intBusinessType);

            if (!isFileDuplicateVar)
            {
                Stuart_V2.Models.FileDetails fileDetails = Utility.getFileDetails();
                listmsgPrefInput.msgPrefFileInfo = new MsgPrefUploadedFileInfo();
                listmsgPrefInput.msgPrefFileInfo.fileName = fileDetails.name;
                listmsgPrefInput.msgPrefFileInfo.fileExtension = fileDetails.extension;
                listmsgPrefInput.msgPrefFileInfo.intFileSize = fileDetails.contentLength;

                //check for extension of the file
                if (!(fileDetails.extension.Equals(".xlsx") || fileDetails.extension.Equals(".xls")))
                {
                    var result = new JsonResult();
                    FileLimit fileLimit = new FileLimit();
                    fileLimit.fileName = fileDetails.name;
                    fileLimit.message = "IncorrectTemplate";
                    //fileLimit.rowCount = lastRow.ToString();
                    result.Data = fileLimit; // Return a FileAlread
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
                            var range = ws.Cells[lastRow, 1, lastRow, 23];
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
                                    .Range(ws.Dimension.Start.Column, 23 - ws.Dimension.Start.Column + 1)
                                    .ToDictionary(col => ws.Cells[headerCol, col].Value.ToString().Trim(), col => col);

                                var keys = new[] {"Master Id","Source System Code","Source System Id","Line of Service Code","Communication Channel",
                                "Message Preference Type", "Message Preference Value" ,"Prefix","First Name","Middle Name","Last Name","Suffix","Organization Name",
                                "Address Line 1","Address Line 2","City","State","Zip 5","Email Address","Phone Number Extension","Phone Number","Notes","Created By"
                              
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
                                        if (!ws.Cells[rw, 1, rw, 23].All(c => c.Value == null))
                                        {
                                            MsgPrefUploadInput input = new MsgPrefUploadInput()
                                            {
                                                init_mstr_id = (ws.Cells[rw, map["Master Id"]].Value ?? (Object)"").ToString(),
                                                arc_srcsys_cd = (ws.Cells[rw, map["Source System Code"]].Value ?? (Object)"").ToString(),
                                                cnst_srcsys_id = (ws.Cells[rw, map["Source System Id"]].Value ?? (Object)"").ToString(),

                                                line_of_service_cd = (ws.Cells[rw, map["Line of Service Code"]].Value ?? (Object)"").ToString(),
                                                comm_chan = (ws.Cells[rw, map["Communication Channel"]].Value ?? (Object)"").ToString(),
                                                msg_prefc_typ = (ws.Cells[rw, map["Message Preference Type"]].Value ?? (Object)"").ToString(),
                                                msg_prefc_val = (ws.Cells[rw, map["Message Preference Value"]].Value ?? (Object)"").ToString(),

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

                                                msg_pref_exp_ts = temp.expirationDate.ToString(),

                                            };


                                            listmsgPrefInput.msgPrefUploadInputList.Add(input);
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
                                FileLimit fileLimit = new FileLimit();
                                fileLimit.fileName = fileDetails.name;
                                fileLimit.message = "IncorrectTemplate";
                                //fileLimit.rowCount = lastRow.ToString();
                                result.Data = fileLimit; // Return a FileAlread
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
                    FileLimit fileLimit = new FileLimit();
                    fileLimit.fileName = fileDetails.name;
                    fileLimit.message = "EmptyFileUploaded";
                    result.Data = fileLimit;
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return result;
                }
              
                if (listmsgPrefInput.msgPrefUploadInputList.Count < 2001) //Check for the uploaded list count
                {
                    Utility.storeUploadedFileDetails(dateUploadStart, fileDetails, TempData, intBusinessType);
                }

                if (listmsgPrefInput.msgPrefUploadInputList.Count == 0)
                {
                    var result = new JsonResult();
                    FileLimit fileLimit = new FileLimit();
                    fileLimit.fileName = fileDetails.name;
                    fileLimit.message = "EmptyFileUploaded";
                    result.Data = fileLimit;
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return result;
                }

            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {

                string url = BaseURL + "api/Upload/message/preference/validate/";
                string res = await Models.Resource.PostResourceAsync(url, Token, listmsgPrefInput, ClientID);
                return handleTrivialHttpRequestsForValidation(res);
            }
            else
            {
                var result = new JsonResult();
                FileLimit fileLimit = new FileLimit();
                //fileLimit.fileName = fileDetails.name;
                fileLimit.message = "FileAlreadyUploaded";
                result.Data = fileLimit; // Return a FileAlread
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }

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


                MsgPrefValidationOutput msgPrefOP = JsonConvert.DeserializeObject<MsgPrefValidationOutput>(res);

                if (msgPrefOP.msgPrefInvalidList == null || msgPrefOP.msgPrefInvalidList.Count == 0)
                {
                    TempData["MsgPrefUploadedRecords"] = msgPrefOP;
                }


                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

       
       

    }// end of class
}