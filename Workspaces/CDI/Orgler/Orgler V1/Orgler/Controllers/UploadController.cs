using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using Orgler.Exceptions;
using Orgler.Models.Upload;
using NLog;
using System.Configuration;
using Orgler.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Orgler.Security;
using IO = System.IO;

namespace Orgler.Controllers
{
    [Authorize]
    public class UploadServiceController : BaseController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        ExcelServiceHelpers excelServiceHelpers = new ExcelServiceHelpers();

        /* ************************ Affiliation Upload ************************ */
        //Method to validate the uploaded file data
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("upload_affil_tb_access", "RW")]
        public async Task<JsonResult> UploadAffiliationFiles()
        {
            //Get the uploaded Files from the server
            System.Web.HttpFileCollection FileCollection = System.Web.HttpContext.Current.Request.Files;
            log.Info("Affiliation Upload");

            UploadResult uploadResult = new UploadResult();
            string strUploadLimitKey = string.Empty;

            //Check if any file is uploaded to the server
            if (FileCollection.Count <= 0)
            {
                uploadResult.strUploadResult = "No file";
                log.Info("No file has been uploaded");
            }

            //Loop through all the file collections
            for (int iCnt = 0; iCnt <= FileCollection.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile PostedFile = FileCollection[iCnt];

                //Check for the present of the files
                if (PostedFile.ContentLength > 0)
                {
                    //Check if the uploaded file is an Excel file
                    if (PostedFile.ContentType.ToLower().Contains("officedocument.spreadsheetml"))
                    {
                        int intUploadLimit = 0;
                        uploadResult.strUploadFileName = PostedFile.FileName;
                        uploadResult.intFileSize = PostedFile.ContentLength;
                        uploadResult.strDocExtention = IO.Path.GetExtension(PostedFile.FileName);

                        intUploadLimit = Convert.ToInt32(ConfigurationManager.AppSettings["AffiliationUploadLimit"]);
                        
                        //Check the number of requests placed
                        int validRowCount = excelServiceHelpers.getExcelRowCount(PostedFile, 3, "affiliation upload");
                        bool boolValidTemplate = excelServiceHelpers.checkIfTemplateIsValidHelper(PostedFile, 2, "affiliation upload");
                        bool filePresence = excelServiceHelpers.checkIfUploadFileDuplicatesHelper(PostedFile, 6);

                        //Check if the template is not a valid one
                        if (!boolValidTemplate)
                        {
                            log.Info("Invalid File format");
                            uploadResult.strUploadResult = "Invalid format";
                        }
                        else if(filePresence)
                        {
                            log.Info("File duplicates");
                            uploadResult.strUploadResult = "File duplicates";
                        }
                        else if (validRowCount <= 0)
                        {
                            uploadResult.strUploadResult = "Not even one valid";
                        }
                        else if (validRowCount > intUploadLimit)
                        {
                            uploadResult.strUploadResult = "Exceeds Limit -" + validRowCount.ToString();
                        }
                        else
                        {
                            List<AffiliationUploadResult> affilRes = new List<AffiliationUploadResult>();
                            List<AffiliationUploadInput> listValidRecords = new List<AffiliationUploadInput>();
                            
                            //Read the excel input and perform basic validations
                            affilRes = excelServiceHelpers.getAffiliationResults(PostedFile, 3);
                            
                            if (affilRes.Count > 0)
                            {
                                //Create service input using valid ids
                                AffiliationUploadValidationInput validInput = new AffiliationUploadValidationInput();
                                List<string> ltEnterpriseId = new List<string>();
                                List<string> ltMasterId = new List<string>();
                                if(affilRes.Count > 0)
                                {
                                    listValidRecords = affilRes.Where(x => (x.strEnterpriseOrgIdFlag != "0" && x.strMasterIdFlag != "0")).Select(x => new AffiliationUploadInput { strEnterpriseOrgId = x.strEnterpriseOrgId, strMasterId = x.strMasterId, strStatus = x.strStatus, strTransKey = "" }).ToList();
                                    ltEnterpriseId = affilRes.Where(x => x.strEnterpriseOrgIdFlag != "0").Select(x => x.strEnterpriseOrgId).ToList();
                                    ltMasterId = affilRes.Where(x => x.strMasterIdFlag != "0").Select(x => x.strMasterId).ToList();
                                }
                                validInput.strEnterpriseOrgIdInput = ltEnterpriseId;
                                validInput.strMasterIdInput = ltMasterId;

                                //Call Service to get the valid ids
                                string url = BaseURL + "api/orglerupload/validateaffiliationupload/";
                                string res = await InvokeWebService.PostResourceAsync(url, Token, validInput, ClientID);
                                //Validate the ids
                                if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
                                {
                                    AffiliationUploadValidationOutput serviceOutput = new AffiliationUploadValidationOutput();
                                    serviceOutput = JsonConvert.DeserializeObject<AffiliationUploadValidationOutput>(res);
                                
                                    foreach(AffiliationUploadResult indv in affilRes)
                                    {
                                        //Check for valid enterprise id
                                        if (indv.strEnterpriseOrgIdFlag != "0" && !string.IsNullOrEmpty(indv.strEnterpriseOrgId))
                                        {
                                            if (!serviceOutput.strEnterpriseOrgIdValid.Contains(indv.strEnterpriseOrgId))
                                                indv.strEnterpriseOrgIdFlag = "0";
                                        }

                                        //Check for valid master id
                                        if (indv.strMasterIdFlag != "0" && !string.IsNullOrEmpty(indv.strMasterId))
                                        {
                                            if (!serviceOutput.strMasterIdInputValid.Contains(indv.strMasterId))
                                                indv.strMasterIdFlag = "0";
                                        }
                                    }
                                }
                                else
                                {
                                    uploadResult.boolFailure = true;
                                }

                                //Check for any errorneous record
                                int intErrorneousRecCount = 0;
                                intErrorneousRecCount = affilRes.Where(x => x.strEnterpriseOrgIdFlag == "0" || x.strMasterIdFlag == "0").Count();
                                if (intErrorneousRecCount > 0)
                                {
                                    uploadResult.intErrorCount = intErrorneousRecCount;
                                    uploadResult.invalidRecords = affilRes.Where(x => x.strEnterpriseOrgIdFlag == "0" || x.strMasterIdFlag == "0")
                                        .OrderBy(x => x.strEnterpriseOrgId).ThenBy(x => x.strMasterId).Take(100).ToList();
                                }
                                else
                                {
                                    uploadResult.boolSuccess = true;
                                    //uploadResult.validRecords = listValidRecords;
                                }
                            }
                            else
                                uploadResult.strUploadResult = "Not even one valid";
                        }
                    }
                    else
                    {
                        log.Info("Invalid File format");
                        uploadResult.strUploadResult = "Invalid format";
                    }
                }
            }

            //Frame the exception string based on the resultant set
            string strExceptionString = "Success";
            if (uploadResult.boolFailure)
                strExceptionString = "databaseerror";

            return handleTrivialHttpRequests<UploadResult>(strExceptionString, uploadResult);

        }

        //Method to post the file to the server
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("upload_affil_tb_access", "RW")]
        public async Task<JsonResult> SubmitAffiliationFiles()
        {
            AffiliationUploadControllerInput inputValue = new AffiliationUploadControllerInput();
            //Get the uploaded Files from the server
            System.Web.HttpFileCollection FileCollection = System.Web.HttpContext.Current.Request.Files;
            log.Info("Eosi Upload");
            //Loop through all the file collections
            for (int iCnt = 0; iCnt <= FileCollection.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile PostedFile = FileCollection[iCnt];

                inputValue.strUploadFileName = PostedFile.FileName;
                inputValue.intFileSize = PostedFile.ContentLength;
                inputValue.strDocExtention = IO.Path.GetExtension(PostedFile.FileName);

                //Check for the present of the files
                if (PostedFile.ContentLength > 0)
                {
                    //Check if the uploaded file is an Excel file
                    if (PostedFile.ContentType.ToLower().Contains("officedocument.spreadsheetml"))
                    {
                        List<AffiliationUploadResult> affilRes = new List<AffiliationUploadResult>();
                        List<AffiliationUploadInput> listValidRecords = new List<AffiliationUploadInput>();
                        affilRes = excelServiceHelpers.getAffiliationResults(PostedFile, 3);
                        listValidRecords = affilRes.Where(x => (x.strEnterpriseOrgIdFlag != "0" && x.strMasterIdFlag != "0")).Select(x => new AffiliationUploadInput { strEnterpriseOrgId = x.strEnterpriseOrgId, strMasterId = x.strMasterId, strStatus = x.strStatus, strTransKey = "" }).ToList();
                        inputValue.input = listValidRecords;
                    }
                }
            }
            inputValue.strUserName = User.GetUserName();

            string strUpldStartTime = DateTime.Now.ToString();
            string url = BaseURL + "api/orglerupload/insertaffiliationupload/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, inputValue, ClientID, 180);
            string strRes = string.Empty;
            TransOutput JObj = new TransOutput();
            Int64 strTransKey = 0;
            string strTransOut = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                JObj = JsonConvert.DeserializeObject<TransOutput>(res);
                strRes = (new JavaScriptSerializer()).Serialize(JObj);
                strTransKey = JObj.transKey;
                strTransOut = JObj.transOutput.ToString();
            }
            else
            {
                strRes = res;
            }
            
            List<UploadStat> listStat = new List<UploadStat>();
            //Get the Upload Status file path froxm config
            string strUploadStatusFilePath = ConfigurationManager.AppSettings["UploadStatusPath"];

            string strJSONUploadStatus = string.Empty;

            //Check for the existance   of the Upload Status file and read the file
            if (IO.File.Exists(strUploadStatusFilePath))
            {
                strJSONUploadStatus = IO.File.ReadAllText(strUploadStatusFilePath);

                //Deserialize
                listStat = (new JavaScriptSerializer()).Deserialize<List<UploadStat>>(strJSONUploadStatus);
            }

            //New Upload Status record
            UploadStat newUpldSts = new UploadStat();
            newUpldSts.BusinessType = 6;
            newUpldSts.FileType = "Excel";
            newUpldSts.UploadStart = strUpldStartTime;
            newUpldSts.UploadEnd = DateTime.Now.ToString();
            newUpldSts.UploadStatus = "Success";
            newUpldSts.TransactionKey = strTransKey.ToString();
            newUpldSts.FileName = inputValue.strUploadFileName;
            newUpldSts.FileSize = inputValue.intFileSize;
            newUpldSts.FileExtention = inputValue.strDocExtention;
            newUpldSts.UserName = inputValue.strUserName;
            newUpldSts.UserEmail = User.GetUserEmail();

            listStat.Add(newUpldSts);

            UploadSubmitOutput listRes = new UploadSubmitOutput();
            listRes.insertFlag = 1;
            listRes.insertOutput = "Success";
            listRes.transactionKey = strTransKey;
            listRes.ListUploadFileDetailsInput = listStat;

            
            //If there is an error with the Web API
            if (res.ToLower().Contains("error") || res.ToLower().Contains("timedout"))
            {
                listRes.insertFlag = 2;
                listRes.insertOutput = "Failure";
            }

            //If there is an error with the Web API
            if (!strTransOut.ToLower().Contains("success"))
            {
                listRes.insertFlag = 0;
                listRes.insertOutput = "Failure";
            }

            return processResultForUploadInsertion((new JavaScriptSerializer()).Serialize(listRes), "EO - Affiliation Upload");
        }

        /* ************************ EOSI Upload ************************ */
        //Method to validate the uploaded file data
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("upload_eosi_tb_access", "RW")]
        public async Task<JsonResult> UploadEosiFiles()
        {
            //Get the uploaded Files from the server
            System.Web.HttpFileCollection FileCollection = System.Web.HttpContext.Current.Request.Files;
            log.Info("Eosi Upload");

            UploadResult uploadResult = new UploadResult();
            string strUploadLimitKey = string.Empty;

            //Check if any file is uploaded to the server
            if (FileCollection.Count <= 0)
            {
                uploadResult.strUploadResult = "No file";
                log.Info("No file has been uploaded");
            }

            //Loop through all the file collections
            for (int iCnt = 0; iCnt <= FileCollection.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile PostedFile = FileCollection[iCnt];

                //Check for the present of the files
                if (PostedFile.ContentLength > 0)
                {
                    //Check if the uploaded file is an Excel file
                    if (PostedFile.ContentType.ToLower().Contains("officedocument.spreadsheetml"))
                    {
                        int intUploadLimit = 0;
                        uploadResult.strUploadFileName = PostedFile.FileName;
                        uploadResult.intFileSize = PostedFile.ContentLength;
                        uploadResult.strDocExtention = IO.Path.GetExtension(PostedFile.FileName);

                        intUploadLimit = Convert.ToInt32(ConfigurationManager.AppSettings["EosiUploadLimit"]);

                        //Check the number of requests placed
                        int validRowCount = excelServiceHelpers.getExcelRowCount(PostedFile, 3, "eosi upload");
                        bool boolValidTemplate = excelServiceHelpers.checkIfTemplateIsValidHelper(PostedFile, 2, "eosi upload");
                        bool filePresence = excelServiceHelpers.checkIfUploadFileDuplicatesHelper(PostedFile, 7);

                        //Check if the template is not a valid one
                        if (!boolValidTemplate)
                        {
                            log.Info("Invalid File format");
                            uploadResult.strUploadResult = "Invalid format";
                        }
                        else if (filePresence)
                        {
                            log.Info("File duplicates");
                            uploadResult.strUploadResult = "File duplicates";
                        }
                        else if (validRowCount <= 0)
                        {
                            uploadResult.strUploadResult = "Not even one valid";
                        }
                        else if (validRowCount > intUploadLimit)
                        {
                            uploadResult.strUploadResult = "Exceeds Limit -" + validRowCount.ToString();
                        }
                        else
                        {
                            List<EosiUploadResult> affilRes = new List<EosiUploadResult>();
                            List<EosiUploadInput> listValidRecords = new List<EosiUploadInput>();
                            affilRes = excelServiceHelpers.getEosiResults(PostedFile, 3);
                            //listValidRecords = affilRes.Where(x => (x.strEnterpriseOrgIdFlag != "0" && x.strMasterIdFlag != "0" && x.strParentEnterpriseOrgIdFlag != "0" && x.strSourceSystemCodeFlag != "0")).Select(x => new EosiUploadInput { strEnterpriseOrgId = x.strEnterpriseOrgId, strMasterId = x.strMasterId, strOrgName = x.strOrgName, strAddress1City = x.strAddress1City, strAddress1State = x.strAddress1State, strAddress1Street1 = x.strAddress1Street1, strAddress1Street2 = x.strAddress1Street2, strAddress1Zip = x.strAddress1Zip, strCharacteristics1Code = x.strCharacteristics1Code, strCharacteristics1Value = x.strCharacteristics1Value, strCharacteristics2Code = x.strCharacteristics2Code, strCharacteristics2Value = x.strCharacteristics2Value, strNaicsCode = x.strNaicsCode, strNotes = x.strNotes, strParentEnterpriseOrgId = x.strParentEnterpriseOrgId, strPhone1 = x.strPhone1, strPhone2 = x.strPhone2, strRMIndicator = x.strRMIndicator, strSecondarySourceId = x.strSecondarySourceId, strSourceId = x.strSourceId, strSourceSystemCode = x.strSourceSystemCode, strTransKey = "", strSeqKey = 0 }).ToList();

                            if (affilRes.Count > 0)
                            {
                                //Create service input using valid ids
                                EosiUploadValidationInput validInput = new EosiUploadValidationInput();
                                List<string> ltEnterpriseId = new List<string>();
                                List<string> ltMasterId = new List<string>();
                                List<string> ltNaicsCode = new List<string>();
                                List<string> ltCharacteristicType = new List<string>();
                                if (affilRes.Count > 0)
                                {
                                    listValidRecords = affilRes.Where(x => (x.strEnterpriseOrgIdFlag != "0" && x.strMasterIdFlag != "0" && x.strParentEnterpriseOrgIdFlag != "0" && x.strSourceSystemCodeFlag != "0" && x.strSecondarySourceIdFlag != "0" && x.strSourceIdFlag != "0" && x.strNaicsCodeFlag != "0" && x.strCharacteristics1CodeFlag != "0" && x.strCharacteristics2CodeFlag != "0" && x.strCharacteristics1ValueFlag != "0" && x.strCharacteristics2ValueFlag != "0")).Select(x => new EosiUploadInput { strEnterpriseOrgId = x.strEnterpriseOrgId, strMasterId = x.strMasterId, strOrgName = x.strOrgName, strAddress1City = x.strAddress1City, strAddress1State = x.strAddress1State, strAddress1Street1 = x.strAddress1Street1, strAddress1Street2 = x.strAddress1Street2, strAddress1Zip = x.strAddress1Zip, strCharacteristics1Code = x.strCharacteristics1Code, strCharacteristics1Value = x.strCharacteristics1Value, strCharacteristics2Code = x.strCharacteristics2Code, strCharacteristics2Value = x.strCharacteristics2Value, strNaicsCode = x.strNaicsCode, strNotes = x.strNotes, strParentEnterpriseOrgId = x.strParentEnterpriseOrgId, strPhone1 = x.strPhone1, strPhone2 = x.strPhone2, strRMIndicator = x.strRMIndicator, strSecondarySourceId = x.strSecondarySourceId, strSourceId = x.strSourceId, strSourceSystemCode = x.strSourceSystemCode, strTransKey = "", strSeqKey = 0 }).ToList();
                                    ltEnterpriseId = affilRes.Where(x => x.strEnterpriseOrgIdFlag != "0" && !string.IsNullOrEmpty(x.strEnterpriseOrgId)).Select(x => x.strEnterpriseOrgId).ToList();
                                    ltMasterId = affilRes.Where(x => x.strMasterIdFlag != "0" && !string.IsNullOrEmpty(x.strMasterId)).Select(x => x.strMasterId).ToList();
                                    ltNaicsCode = affilRes.Where(x => x.strNaicsCodeFlag != "0" && !string.IsNullOrEmpty(x.strNaicsCode)).Select(x => x.strNaicsCode).ToList();
                                    ltCharacteristicType = affilRes.Where(x => (x.strCharacteristics1CodeFlag != "0" && !string.IsNullOrEmpty(x.strCharacteristics1Code))).Select(x => x.strCharacteristics1Code).ToList();
                                    ltCharacteristicType.AddRange(affilRes.Where(x => (x.strCharacteristics2CodeFlag != "0" && !string.IsNullOrEmpty(x.strCharacteristics2Code))).Select(x => x.strCharacteristics2Code).ToList());
                                }
                                validInput.strEnterpriseOrgIdInput = ltEnterpriseId;
                                validInput.strMasterIdInput = ltMasterId;
                                validInput.strNaicsCodeInput = ltNaicsCode;
                                validInput.strCharacteristicTypeInput = ltCharacteristicType;

                                //Call Service to get the valid ids
                                string url = BaseURL + "api/orglerupload/validateeosiupload/";
                                string res = await InvokeWebService.PostResourceAsync(url, Token, validInput, ClientID);
                                //Validate the ids
                                if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
                                {
                                    EosiUploadValidationOutput serviceOutput = new EosiUploadValidationOutput();
                                    serviceOutput = JsonConvert.DeserializeObject<EosiUploadValidationOutput>(res);

                                    foreach (EosiUploadResult indv in affilRes)
                                    {
                                        //Check for valid enterprise id
                                        if (indv.strEnterpriseOrgIdFlag != "0" && !string.IsNullOrEmpty(indv.strEnterpriseOrgId))
                                        {
                                            if (!serviceOutput.strEnterpriseOrgIdValid.Contains(indv.strEnterpriseOrgId))
                                                indv.strEnterpriseOrgIdFlag = "0";
                                        }

                                        //Check for valid parent enterprise id
                                        //if (indv.strParentEnterpriseOrgIdFlag != "0" && !string.IsNullOrEmpty(indv.strParentEnterpriseOrgId))
                                        //{
                                        //    if (!serviceOutput.strEnterpriseOrgIdValid.Contains(indv.strParentEnterpriseOrgId))
                                        //        indv.strParentEnterpriseOrgIdFlag = "0";
                                        //}

                                        //Check for valid master id
                                        if (indv.strMasterIdFlag != "0" && !string.IsNullOrEmpty(indv.strMasterId))
                                        {
                                            if (!serviceOutput.strMasterIdValid.Contains(indv.strMasterId))
                                                indv.strMasterIdFlag = "0";
                                        }

                                        //Check for valid naics code
                                        if (indv.strNaicsCodeFlag != "0" && !string.IsNullOrEmpty(indv.strNaicsCode))
                                        {
                                            if (!serviceOutput.strNaicsCodeValid.Contains(indv.strNaicsCode))
                                                indv.strNaicsCodeFlag = "0";
                                        }

                                        //Check for valid characteristic type 1
                                        if (indv.strCharacteristics1CodeFlag != "0" && !string.IsNullOrEmpty(indv.strCharacteristics1Code))
                                        {
                                            if (!serviceOutput.strCharacteristicTypeValid.Contains(indv.strCharacteristics1Code))
                                                indv.strCharacteristics1CodeFlag = "0";
                                        }

                                        //Check for valid characteristic type 2
                                        if (indv.strCharacteristics2CodeFlag != "0" && !string.IsNullOrEmpty(indv.strCharacteristics2Code))
                                        {
                                            if (!serviceOutput.strCharacteristicTypeValid.Contains(indv.strCharacteristics2Code))
                                                indv.strCharacteristics2CodeFlag = "0";
                                        }
                                    }
                                }
                                else
                                {
                                    uploadResult.boolFailure = true;
                                }

                                //Check for any errorneous record
                                int intErrorneousRecCount = 0;
                                intErrorneousRecCount = affilRes.Where(x => (x.strEnterpriseOrgIdFlag == "0" || x.strMasterIdFlag == "0" || x.strParentEnterpriseOrgIdFlag == "0" || x.strSourceSystemCodeFlag == "0" || x.strSourceIdFlag == "0" || x.strSecondarySourceIdFlag == "0" || x.strNaicsCodeFlag == "0" || x.strCharacteristics1CodeFlag == "0" || x.strCharacteristics2CodeFlag == "0" || x.strCharacteristics1ValueFlag == "0" || x.strCharacteristics2ValueFlag == "0")).Count();
                                if (intErrorneousRecCount > 0)
                                {
                                    uploadResult.intErrorCount = intErrorneousRecCount;
                                    uploadResult.invalidRecordsEosi = affilRes.Where(x => (x.strEnterpriseOrgIdFlag == "0" || x.strMasterIdFlag == "0" || x.strParentEnterpriseOrgIdFlag == "0" || x.strSourceSystemCodeFlag == "0" || x.strSourceIdFlag == "0" || x.strSecondarySourceIdFlag == "0" || x.strNaicsCodeFlag == "0" || x.strCharacteristics1CodeFlag == "0" || x.strCharacteristics2CodeFlag == "0" || x.strCharacteristics1ValueFlag == "0" || x.strCharacteristics2ValueFlag == "0"))
                                        .OrderBy(x => x.strMasterId).ThenBy(x => x.strSourceSystemCode).ThenBy(x => x.strSourceId).Take(100).ToList();
                                }
                                else
                                {
                                    uploadResult.boolSuccess = true;
                                    //uploadResult.validRecordsEosi = listValidRecords;
                                }
                            }
                            else
                                uploadResult.strUploadResult = "Not even one valid";
                        }
                    }
                    else
                    {
                        log.Info("Invalid File format");
                        uploadResult.strUploadResult = "Invalid format";
                    }
                }
            }

            //Frame the exception string based on the resultant set
            string strExceptionString = "Success";
            if (uploadResult.boolFailure)
                strExceptionString = "databaseerror";

            return handleTrivialHttpRequests<UploadResult>(strExceptionString, uploadResult);

        }

        //Method to post the file to the server
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("upload_eosi_tb_access", "RW")]
        public async Task<JsonResult> SubmitEosiFiles()
        {
            EosiUploadControllerInput inputValue = new EosiUploadControllerInput();

            //Get the uploaded Files from the server
            System.Web.HttpFileCollection FileCollection = System.Web.HttpContext.Current.Request.Files;
            log.Info("Eosi Upload");
            //Loop through all the file collections
            for (int iCnt = 0; iCnt <= FileCollection.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile PostedFile = FileCollection[iCnt];

                inputValue.strUploadFileName = PostedFile.FileName;
                inputValue.intFileSize = PostedFile.ContentLength;
                inputValue.strDocExtention = IO.Path.GetExtension(PostedFile.FileName);

                //Check for the present of the files
                if (PostedFile.ContentLength > 0)
                {
                    //Check if the uploaded file is an Excel file
                    if (PostedFile.ContentType.ToLower().Contains("officedocument.spreadsheetml"))
                    {
                            List<EosiUploadResult> affilRes = new List<EosiUploadResult>();
                            List<EosiUploadInput> listValidRecords = new List<EosiUploadInput>();
                            affilRes = excelServiceHelpers.getEosiResults(PostedFile, 3);
                            listValidRecords = affilRes.Where(x => (x.strEnterpriseOrgIdFlag != "0" && x.strMasterIdFlag != "0" && x.strParentEnterpriseOrgIdFlag != "0" && x.strSourceSystemCodeFlag != "0" && x.strSourceIdFlag != "0" && x.strSecondarySourceIdFlag != "0" && x.strNaicsCodeFlag != "0" && x.strCharacteristics1CodeFlag != "0" && x.strCharacteristics2CodeFlag != "0" && x.strCharacteristics1ValueFlag != "0" && x.strCharacteristics2ValueFlag != "0")).Select(x => new EosiUploadInput { strEnterpriseOrgId = x.strEnterpriseOrgId, strMasterId = x.strMasterId, strOrgName = x.strOrgName, strAddress1City = x.strAddress1City, strAddress1State = x.strAddress1State, strAddress1Street1 = x.strAddress1Street1, strAddress1Street2 = x.strAddress1Street2, strAddress1Zip = x.strAddress1Zip, strCharacteristics1Code = x.strCharacteristics1Code, strCharacteristics1Value = x.strCharacteristics1Value, strCharacteristics2Code = x.strCharacteristics2Code, strCharacteristics2Value = x.strCharacteristics2Value, strNaicsCode = x.strNaicsCode, strNotes = x.strNotes, strParentEnterpriseOrgId = x.strParentEnterpriseOrgId, strPhone1 = x.strPhone1, strPhone2 = x.strPhone2, strRMIndicator = x.strRMIndicator, strSecondarySourceId = x.strSecondarySourceId, strSourceId = x.strSourceId, strSourceSystemCode = x.strSourceSystemCode, strTransKey = "", strSeqKey = 0, strAltSourceCode = x.strAltSourceCode, strAltSourceId = x.strAltSourceId }).ToList();
                            inputValue.input = listValidRecords;
                    }
                }
            }
            inputValue.strUserName = User.GetUserName();

            string strUpldStartTime = DateTime.Now.ToString();
            string url = BaseURL + "api/orglerupload/insertEosiupload/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, inputValue, ClientID, 180);
            string strRes = string.Empty;
            TransOutput JObj = new TransOutput();
            Int64 strTransKey = 0;
            string strTransOut = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                JObj = JsonConvert.DeserializeObject<TransOutput>(res);
                strRes = (new JavaScriptSerializer()).Serialize(JObj);
                strTransKey = JObj.transKey;
                strTransOut = JObj.transOutput.ToString();
            }
            else
            {
                strRes = res;
            }

            List<UploadStat> listStat = new List<UploadStat>();
            //Get the Upload Status file path froxm config
            string strUploadStatusFilePath = ConfigurationManager.AppSettings["UploadStatusPath"];

            string strJSONUploadStatus = string.Empty;

            //Check for the existance of the Upload Status file and read the file
            if (IO.File.Exists(strUploadStatusFilePath))
            {
                strJSONUploadStatus = IO.File.ReadAllText(strUploadStatusFilePath);

                //Deserialize
                listStat = (new JavaScriptSerializer()).Deserialize<List<UploadStat>>(strJSONUploadStatus);
            }

            //New Upload Status record
            UploadStat newUpldSts = new UploadStat();
            newUpldSts.BusinessType = 7;
            newUpldSts.FileType = "Excel";
            newUpldSts.UploadStart = strUpldStartTime;
            newUpldSts.UploadEnd = DateTime.Now.ToString();
            newUpldSts.UploadStatus = "Success";
            newUpldSts.TransactionKey = strTransKey.ToString();
            newUpldSts.FileName = inputValue.strUploadFileName;
            newUpldSts.FileSize = inputValue.intFileSize;
            newUpldSts.FileExtention = inputValue.strDocExtention;
            newUpldSts.UserName = inputValue.strUserName;
            newUpldSts.UserEmail = User.GetUserEmail();

            listStat.Add(newUpldSts);

            UploadSubmitOutput listRes = new UploadSubmitOutput();
            listRes.insertFlag = 1;
            listRes.insertOutput = "Success";
            listRes.transactionKey = strTransKey;
            listRes.ListUploadFileDetailsInput = listStat;


            //If there is an error with the Web API
            if (res.ToLower().Contains("error") || res.ToLower().Contains("timedout"))
            {
                listRes.insertFlag = 2;
                listRes.insertOutput = "Failure";
            }

            //If there is an error with the Web API
            if (!strTransOut.ToLower().Contains("success"))
            {
                listRes.insertFlag = 0;
                listRes.insertOutput = "Failure";
            }

            return processResultForUploadInsertion((new JavaScriptSerializer()).Serialize(listRes), "EO - Eosi Upload");
        }

        /* ************************ EO Upload ************************ */
        //Method to validate the uploaded file data
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("upload_eo_tb_access", "RW")]
        public async Task<JsonResult> UploadEoFiles()
        {
            //Get the uploaded Files from the server
            System.Web.HttpFileCollection FileCollection = System.Web.HttpContext.Current.Request.Files;
            log.Info("Eo Upload");

            UploadResult uploadResult = new UploadResult();
            string strUploadLimitKey = string.Empty;

            //Check if any file is uploaded to the server
            if (FileCollection.Count <= 0)
            {
                uploadResult.strUploadResult = "No file";
                log.Info("No file has been uploaded");
            }

            //Loop through all the file collections
            for (int iCnt = 0; iCnt <= FileCollection.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile PostedFile = FileCollection[iCnt];

                //Check for the present of the files
                if (PostedFile.ContentLength > 0)
                {
                    //Check if the uploaded file is an Excel file
                    if (PostedFile.ContentType.ToLower().Contains("officedocument.spreadsheetml"))
                    {
                        int intUploadLimit = 0;
                        uploadResult.strUploadFileName = PostedFile.FileName;
                        uploadResult.intFileSize = PostedFile.ContentLength;
                        uploadResult.strDocExtention = IO.Path.GetExtension(PostedFile.FileName);

                        intUploadLimit = Convert.ToInt32(ConfigurationManager.AppSettings["EoUploadLimit"]);

                        //Check the number of requests placed
                        int validRowCount = excelServiceHelpers.getExcelRowCount(PostedFile, 3, "eo upload");
                        bool boolValidTemplate = excelServiceHelpers.checkIfTemplateIsValidHelper(PostedFile, 2, "eo upload");
                        bool filePresence = excelServiceHelpers.checkIfUploadFileDuplicatesHelper(PostedFile, 8);

                        //Check if the template is not a valid one
                        if (!boolValidTemplate)
                        {
                            log.Info("Invalid File format");
                            uploadResult.strUploadResult = "Invalid format";
                        }
                        else if (filePresence)
                        {
                            log.Info("File duplicates");
                            uploadResult.strUploadResult = "File duplicates";
                        }
                        else if (validRowCount <= 0)
                        {
                            uploadResult.strUploadResult = "Not even one valid";
                        }
                        else if (validRowCount > intUploadLimit)
                        {
                            uploadResult.strUploadResult = "Exceeds Limit -" + validRowCount.ToString();
                        }
                        else
                        {
                            List<EoUploadResult> affilRes = new List<EoUploadResult>();
                            List<EoUploadInput> listValidRecords = new List<EoUploadInput>();
                            affilRes = excelServiceHelpers.getEoResults(PostedFile, 3);
                            //listValidRecords = affilRes.Where(x => (x.strEnterpriseOrgIdFlag != "0" && x.strMasterIdFlag != "0" && x.strParentEnterpriseOrgIdFlag != "0" && x.strSourceSystemCodeFlag != "0")).Select(x => new EoUploadInput { strEnterpriseOrgId = x.strEnterpriseOrgId, strMasterId = x.strMasterId, strOrgName = x.strOrgName, strAddress1City = x.strAddress1City, strAddress1State = x.strAddress1State, strAddress1Street1 = x.strAddress1Street1, strAddress1Street2 = x.strAddress1Street2, strAddress1Zip = x.strAddress1Zip, strCharacteristics1Code = x.strCharacteristics1Code, strCharacteristics1Value = x.strCharacteristics1Value, strCharacteristics2Code = x.strCharacteristics2Code, strCharacteristics2Value = x.strCharacteristics2Value, strNaicsCode = x.strNaicsCode, strNotes = x.strNotes, strParentEnterpriseOrgId = x.strParentEnterpriseOrgId, strPhone1 = x.strPhone1, strPhone2 = x.strPhone2, strRMIndicator = x.strRMIndicator, strSecondarySourceId = x.strSecondarySourceId, strSourceId = x.strSourceId, strSourceSystemCode = x.strSourceSystemCode, strTransKey = "", strSeqKey = 0 }).ToList();

                            if (affilRes.Count > 0)
                            {
                                //Create service input using valid ids
                                EoUploadValidationInput validInput = new EoUploadValidationInput();
                                List<string> ltEnterpriseId = new List<string>();
                                List<string> ltEnterpriseName = new List<string>();
                                List<string> ltCharacteristicType = new List<string>();
                                List<string> ltTag = new List<string>();

                                if (affilRes.Count > 0)
                                {
                                    listValidRecords = affilRes.Where(x => (x.strEnterpriseOrgIdFlag != "0" && x.strEnterpriseOrgNameFlag != "0" && x.strCharacteristics1CodeFlag != "0" && x.strCharacteristics1ValueFlag != "0" && x.strCharacteristics2CodeFlag != "0" && x.strCharacteristics2ValueFlag != "0" && x.strCharacteristics3CodeFlag != "0" && x.strCharacteristics3ValueFlag != "0" && x.strTransformCondition1Type1Flag != "0" && x.strTransformCondition1String1Flag != "0" && x.strTransformCondition1Type2Flag != "0" && x.strTransformCondition1String2Flag != "0" && x.strTransformCondition1Type3Flag != "0" && x.strTransformCondition1String3Flag != "0" && x.strTransformCondition2Type1Flag != "0" && x.strTransformCondition2String1Flag != "0" && x.strTransformCondition2Type2Flag != "0" && x.strTransformCondition2String2Flag != "0" && x.strTransformCondition2Type3Flag != "0" && x.strTransformCondition2String3Flag != "0" && x.strTransformCondition3Type1Flag != "0" && x.strTransformCondition3String1Flag != "0" && x.strTransformCondition3Type2Flag != "0" && x.strTransformCondition3String2Flag != "0" && x.strTransformCondition3Type3Flag != "0" && x.strTransformCondition3String3Flag != "0" && x.strTag1Flag != "0" && x.strTag2Flag != "0" && x.strTag3Flag != "0"))
                                        .Select(x => new EoUploadInput { strEnterpriseOrgId = x.strEnterpriseOrgId, strEnterpriseOrgName = x.strEnterpriseOrgName, strCharacteristics1Code = x.strCharacteristics1Code, strCharacteristics1Value = x.strCharacteristics1Value, strCharacteristics2Code = x.strCharacteristics2Code, strCharacteristics2Value = x.strCharacteristics2Value, strCharacteristics3Code = x.strCharacteristics3Code, strCharacteristics3Value = x.strCharacteristics3Value, strTransformCondition1Type1 = x.strTransformCondition1Type1, strTransformCondition1String1 = x.strTransformCondition1String1, strTransformCondition1Type2 = x.strTransformCondition1Type2, strTransformCondition1String2 = x.strTransformCondition1String2, strTransformCondition1Type3 = x.strTransformCondition1Type3, strTransformCondition1String3 = x.strTransformCondition1String3, strTransformCondition2Type1 = x.strTransformCondition2Type1, strTransformCondition2String1 = x.strTransformCondition2String1, strTransformCondition2Type2 = x.strTransformCondition2Type2, strTransformCondition2String2 = x.strTransformCondition2String2, strTransformCondition2Type3 = x.strTransformCondition2Type3, strTransformCondition2String3 = x.strTransformCondition2String3, strTransformCondition3Type1 = x.strTransformCondition3Type1, strTransformCondition3String1 = x.strTransformCondition3String1, strTransformCondition3Type2 = x.strTransformCondition3Type2, strTransformCondition3String2 = x.strTransformCondition3String2, strTransformCondition3Type3 = x.strTransformCondition3Type3, strTransformCondition3String3 = x.strTransformCondition3String3, strTag1 = x.strTag1, strTag2 = x.strTag2, strTag3 = x.strTag3, strTransKey = "", strSeqKey = 0, strAction = x.strAction }).ToList();
                                    ltEnterpriseId = affilRes.Where(x => x.strEnterpriseOrgIdFlag != "0" && !string.IsNullOrEmpty(x.strEnterpriseOrgId)).Select(x => x.strEnterpriseOrgId).ToList();
                                    ltEnterpriseName = affilRes.Where(x => x.strEnterpriseOrgNameFlag != "0" && !string.IsNullOrEmpty(x.strEnterpriseOrgName)).Select(x => x.strEnterpriseOrgName).ToList();
                                    ltCharacteristicType = affilRes.Where(x => (x.strCharacteristics1CodeFlag != "0" && !string.IsNullOrEmpty(x.strCharacteristics1Code))).Select(x => x.strCharacteristics1Code).ToList();
                                    ltCharacteristicType.AddRange(affilRes.Where(x => (x.strCharacteristics2CodeFlag != "0" && !string.IsNullOrEmpty(x.strCharacteristics2Code))).Select(x => x.strCharacteristics2Code).ToList());
                                    ltCharacteristicType.AddRange(affilRes.Where(x => (x.strCharacteristics3CodeFlag != "0" && !string.IsNullOrEmpty(x.strCharacteristics3Code))).Select(x => x.strCharacteristics3Code).ToList());
                                    ltTag = affilRes.Where(x => (x.strTag1Flag != "0" && !string.IsNullOrEmpty(x.strTag1))).Select(x => x.strTag1).ToList();
                                    ltTag.AddRange(affilRes.Where(x => (x.strTag2Flag != "0" && !string.IsNullOrEmpty(x.strTag2))).Select(x => x.strTag2).ToList());
                                    ltTag.AddRange(affilRes.Where(x => (x.strTag3Flag != "0" && !string.IsNullOrEmpty(x.strTag3))).Select(x => x.strTag3).ToList());
                                }
                                validInput.strEnterpriseOrgIdInput = ltEnterpriseId;
                                validInput.strEnterpriseOrgNameInput = ltEnterpriseName;
                                validInput.strCharacteristicTypeInput = ltCharacteristicType;
                                validInput.strTagInput = ltTag;

                                //Call Service to get the valid ids
                                string url = BaseURL + "api/orglerupload/validateeoupload/";
                                string res = await InvokeWebService.PostResourceAsync(url, Token, validInput, ClientID);
                                //Validate the ids
                                if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
                                {
                                    EoUploadValidationOutput serviceOutput = new EoUploadValidationOutput();
                                    serviceOutput = JsonConvert.DeserializeObject<EoUploadValidationOutput>(res);

                                    foreach (EoUploadResult indv in affilRes)
                                    {
                                        //Check for valid enterprise id
                                        if (indv.strEnterpriseOrgIdFlag != "0" && !string.IsNullOrEmpty(indv.strEnterpriseOrgId))
                                        {
                                            if (!serviceOutput.strEnterpriseOrgIdValid.Contains(indv.strEnterpriseOrgId))
                                                indv.strEnterpriseOrgIdFlag = "0";
                                        }

                                        //Check for valid enterprise name in case of updates
                                        if (indv.strAction.ToLower() == "update" && indv.strEnterpriseOrgNameFlag != "0" && !string.IsNullOrEmpty(indv.strEnterpriseOrgName))
                                        {
                                            if (!serviceOutput.strEnterpriseOrgNameValid.Contains(indv.strEnterpriseOrgName))
                                                indv.strEnterpriseOrgNameFlag = "0";
                                        }

                                        //Check for unique enterprise name in case of inserts
                                        if (indv.strAction.ToLower() == "insert" && indv.strEnterpriseOrgNameFlag != "0" && !string.IsNullOrEmpty(indv.strEnterpriseOrgName))
                                        {
                                            if (serviceOutput.strEnterpriseOrgNameValid.Contains(indv.strEnterpriseOrgName))
                                                indv.strEnterpriseOrgNameFlag = "0";
                                        }

                                        //Check for valid characteristic type 1
                                        if (indv.strCharacteristics1CodeFlag != "0" && !string.IsNullOrEmpty(indv.strCharacteristics1Code))
                                        {
                                            if (!serviceOutput.strCharacteristicTypeValid.Contains(indv.strCharacteristics1Code))
                                                indv.strCharacteristics1CodeFlag = "0";
                                        }

                                        //Check for valid characteristic type 2
                                        if (indv.strCharacteristics2CodeFlag != "0" && !string.IsNullOrEmpty(indv.strCharacteristics2Code))
                                        {
                                            if (!serviceOutput.strCharacteristicTypeValid.Contains(indv.strCharacteristics2Code))
                                                indv.strCharacteristics2CodeFlag = "0";
                                        }

                                        //Check for valid characteristic type 3
                                        if (indv.strCharacteristics3CodeFlag != "0" && !string.IsNullOrEmpty(indv.strCharacteristics3Code))
                                        {
                                            if (!serviceOutput.strCharacteristicTypeValid.Contains(indv.strCharacteristics3Code))
                                                indv.strCharacteristics3CodeFlag = "0";
                                        }

                                        //Check for valid tag 1
                                        if (indv.strTag1Flag != "0" && !string.IsNullOrEmpty(indv.strTag1))
                                        {
                                            if (!serviceOutput.strTagValid.Contains(indv.strTag1))
                                                indv.strTag1Flag = "0";
                                        }

                                        //Check for valid tag 2
                                        if (indv.strTag2Flag != "0" && !string.IsNullOrEmpty(indv.strTag2))
                                        {
                                            if (!serviceOutput.strTagValid.Contains(indv.strTag2))
                                                indv.strTag2Flag = "0";
                                        }

                                        //Check for valid tag 3
                                        if (indv.strTag3Flag != "0" && !string.IsNullOrEmpty(indv.strTag3))
                                        {
                                            if (!serviceOutput.strTagValid.Contains(indv.strTag3))
                                                indv.strTag3Flag = "0";
                                        }
                                    }
                                }
                                else
                                {
                                    uploadResult.boolFailure = true;
                                }

                                //Check for any errorneous record
                                int intErrorneousRecCount = 0;
                                intErrorneousRecCount = affilRes.Where(x => (x.strEnterpriseOrgIdFlag == "0" || x.strEnterpriseOrgNameFlag == "0" || x.strCharacteristics1CodeFlag == "0" || x.strCharacteristics1ValueFlag == "0" || x.strCharacteristics2CodeFlag == "0" || x.strCharacteristics2ValueFlag == "0" || x.strCharacteristics3CodeFlag == "0" || x.strCharacteristics3ValueFlag == "0" || x.strTransformCondition1Type1Flag == "0" || x.strTransformCondition1String1Flag == "0" || x.strTransformCondition1Type2Flag == "0" || x.strTransformCondition1String2Flag == "0" || x.strTransformCondition1Type3Flag == "0" || x.strTransformCondition1String3Flag == "0" || x.strTransformCondition2Type1Flag == "0" || x.strTransformCondition2String1Flag == "0" || x.strTransformCondition2Type2Flag == "0" || x.strTransformCondition2String2Flag == "0" || x.strTransformCondition2Type3Flag == "0" || x.strTransformCondition2String3Flag == "0" || x.strTransformCondition3Type1Flag == "0" || x.strTransformCondition3String1Flag == "0" || x.strTransformCondition3Type2Flag == "0" || x.strTransformCondition3String2Flag == "0" || x.strTransformCondition3Type3Flag == "0" || x.strTransformCondition3String3Flag == "0" || x.strTag1Flag == "0" || x.strTag2Flag == "0" || x.strTag3Flag == "0")).Count();
                                if (intErrorneousRecCount > 0)
                                {
                                    uploadResult.intErrorCount = intErrorneousRecCount;
                                    uploadResult.invalidRecordsEo = affilRes.Where(x => (x.strEnterpriseOrgIdFlag == "0" || x.strEnterpriseOrgNameFlag == "0" || x.strCharacteristics1CodeFlag == "0" || x.strCharacteristics1ValueFlag == "0" || x.strCharacteristics2CodeFlag == "0" || x.strCharacteristics2ValueFlag == "0" || x.strCharacteristics3CodeFlag == "0" || x.strCharacteristics3ValueFlag == "0" || x.strTransformCondition1Type1Flag == "0" || x.strTransformCondition1String1Flag == "0" || x.strTransformCondition1Type2Flag == "0" || x.strTransformCondition1String2Flag == "0" || x.strTransformCondition1Type3Flag == "0" || x.strTransformCondition1String3Flag == "0" || x.strTransformCondition2Type1Flag == "0" || x.strTransformCondition2String1Flag == "0" || x.strTransformCondition2Type2Flag == "0" || x.strTransformCondition2String2Flag == "0" || x.strTransformCondition2Type3Flag == "0" || x.strTransformCondition2String3Flag == "0" || x.strTransformCondition3Type1Flag == "0" || x.strTransformCondition3String1Flag == "0" || x.strTransformCondition3Type2Flag == "0" || x.strTransformCondition3String2Flag == "0" || x.strTransformCondition3Type3Flag == "0" || x.strTransformCondition3String3Flag == "0" || x.strTag1Flag == "0" || x.strTag2Flag == "0" || x.strTag3Flag == "0"))
                                        .OrderBy(x => x.strEnterpriseOrgId).ThenBy(x => x.strEnterpriseOrgName).Take(100).ToList();
                                }
                                else
                                {
                                    uploadResult.boolSuccess = true;
                                    //uploadResult.validRecordsEo = listValidRecords;
                                }
                            }
                            else
                                uploadResult.strUploadResult = "Not even one valid";
                        }
                    }
                    else
                    {
                        log.Info("Invalid File format");
                        uploadResult.strUploadResult = "Invalid format";
                    }
                }
            }

            //Frame the exception string based on the resultant set
            string strExceptionString = "Success";
            if (uploadResult.boolFailure)
                strExceptionString = "databaseerror";

            return handleTrivialHttpRequests<UploadResult>(strExceptionString, uploadResult);

        }

        //Method to post the file to the server
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("upload_eo_tb_access", "RW")]
        public async Task<JsonResult> SubmitEoFiles()
        {
            EoUploadControllerInput inputValue = new EoUploadControllerInput();

            //Get the uploaded Files from the server
            System.Web.HttpFileCollection FileCollection = System.Web.HttpContext.Current.Request.Files;
            log.Info("Eo Upload");
            //Loop through all the file collections
            for (int iCnt = 0; iCnt <= FileCollection.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile PostedFile = FileCollection[iCnt];

                inputValue.strUploadFileName = PostedFile.FileName;
                inputValue.intFileSize = PostedFile.ContentLength;
                inputValue.strDocExtention = IO.Path.GetExtension(PostedFile.FileName);

                //Check for the present of the files
                if (PostedFile.ContentLength > 0)
                {
                    //Check if the uploaded file is an Excel file
                    if (PostedFile.ContentType.ToLower().Contains("officedocument.spreadsheetml"))
                    {
                            List<EoUploadResult> affilRes = new List<EoUploadResult>();
                            List<EoUploadInput> listValidRecords = new List<EoUploadInput>();
                            affilRes = excelServiceHelpers.getEoResults(PostedFile, 3);
                            listValidRecords = affilRes.Where(x => (x.strEnterpriseOrgIdFlag != "0" && x.strEnterpriseOrgNameFlag != "0" && x.strCharacteristics1CodeFlag != "0" && x.strCharacteristics1ValueFlag != "0" && x.strCharacteristics2CodeFlag != "0" && x.strCharacteristics2ValueFlag != "0" && x.strCharacteristics3CodeFlag != "0" && x.strCharacteristics3ValueFlag != "0" && x.strTransformCondition1Type1Flag != "0" && x.strTransformCondition1String1Flag != "0" && x.strTransformCondition1Type2Flag != "0" && x.strTransformCondition1String2Flag != "0" && x.strTransformCondition1Type3Flag != "0" && x.strTransformCondition1String3Flag != "0" && x.strTransformCondition2Type1Flag != "0" && x.strTransformCondition2String1Flag != "0" && x.strTransformCondition2Type2Flag != "0" && x.strTransformCondition2String2Flag != "0" && x.strTransformCondition2Type3Flag != "0" && x.strTransformCondition2String3Flag != "0" && x.strTransformCondition3Type1Flag != "0" && x.strTransformCondition3String1Flag != "0" && x.strTransformCondition3Type2Flag != "0" && x.strTransformCondition3String2Flag != "0" && x.strTransformCondition3Type3Flag != "0" && x.strTransformCondition3String3Flag != "0" && x.strTag1Flag != "0" && x.strTag2Flag != "0" && x.strTag3Flag != "0"))
                                .Select(x => new EoUploadInput { strEnterpriseOrgId = x.strEnterpriseOrgId, strEnterpriseOrgName = x.strEnterpriseOrgName, strCharacteristics1Code = x.strCharacteristics1Code, strCharacteristics1Value = x.strCharacteristics1Value, strCharacteristics2Code = x.strCharacteristics2Code, strCharacteristics2Value = x.strCharacteristics2Value, strCharacteristics3Code = x.strCharacteristics3Code, strCharacteristics3Value = x.strCharacteristics3Value, strTransformCondition1Type1 = x.strTransformCondition1Type1, strTransformCondition1String1 = x.strTransformCondition1String1, strTransformCondition1Type2 = x.strTransformCondition1Type2, strTransformCondition1String2 = x.strTransformCondition1String2, strTransformCondition1Type3 = x.strTransformCondition1Type3, strTransformCondition1String3 = x.strTransformCondition1String3, strTransformCondition2Type1 = x.strTransformCondition2Type1, strTransformCondition2String1 = x.strTransformCondition2String1, strTransformCondition2Type2 = x.strTransformCondition2Type2, strTransformCondition2String2 = x.strTransformCondition2String2, strTransformCondition2Type3 = x.strTransformCondition2Type3, strTransformCondition2String3 = x.strTransformCondition2String3, strTransformCondition3Type1 = x.strTransformCondition3Type1, strTransformCondition3String1 = x.strTransformCondition3String1, strTransformCondition3Type2 = x.strTransformCondition3Type2, strTransformCondition3String2 = x.strTransformCondition3String2, strTransformCondition3Type3 = x.strTransformCondition3Type3, strTransformCondition3String3 = x.strTransformCondition3String3, strTag1 = x.strTag1, strTag2 = x.strTag2, strTag3 = x.strTag3, strTransKey = "", strSeqKey = 0, strAction = x.strAction }).ToList();
                            inputValue.input = listValidRecords;
                    }
                }
            }
            inputValue.strUserName = User.GetUserName();

            string strUpldStartTime = DateTime.Now.ToString();
            string url = BaseURL + "api/orglerupload/insertEoupload/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, inputValue, ClientID, 180);
            string strRes = string.Empty;
            TransOutput JObj = new TransOutput();
            Int64 strTransKey = 0;
            string strTransOut = string.Empty;

            if (!res.ToLower().Contains("error") && !res.ToLower().Contains("timedout"))
            {
                JObj = JsonConvert.DeserializeObject<TransOutput>(res);
                strRes = (new JavaScriptSerializer()).Serialize(JObj);
                strTransKey = JObj.transKey;
                strTransOut = JObj.transOutput.ToString();
            }
            else
            {
                strRes = res;
            }

            List<UploadStat> listStat = new List<UploadStat>();
            //Get the Upload Status file path froxm config
            string strUploadStatusFilePath = ConfigurationManager.AppSettings["UploadStatusPath"];

            string strJSONUploadStatus = string.Empty;

            //Check for the existance of the Upload Status file and read the file
            if (IO.File.Exists(strUploadStatusFilePath))
            {
                strJSONUploadStatus = IO.File.ReadAllText(strUploadStatusFilePath);

                //Deserialize
                listStat = (new JavaScriptSerializer()).Deserialize<List<UploadStat>>(strJSONUploadStatus);
            }

            //New Upload Status record
            UploadStat newUpldSts = new UploadStat();
            newUpldSts.BusinessType = 8;
            newUpldSts.FileType = "Excel";
            newUpldSts.UploadStart = strUpldStartTime;
            newUpldSts.UploadEnd = DateTime.Now.ToString();
            newUpldSts.UploadStatus = "Success";
            newUpldSts.TransactionKey = strTransKey.ToString();
            newUpldSts.FileName = inputValue.strUploadFileName;
            newUpldSts.FileSize = inputValue.intFileSize;
            newUpldSts.FileExtention = inputValue.strDocExtention;
            newUpldSts.UserName = inputValue.strUserName;
            newUpldSts.UserEmail = User.GetUserEmail();

            listStat.Add(newUpldSts);

            UploadSubmitOutput listRes = new UploadSubmitOutput();
            listRes.insertFlag = 1;
            listRes.insertOutput = "Success";
            listRes.transactionKey = strTransKey;
            listRes.ListUploadFileDetailsInput = listStat;


            //If there is an error with the Web API
            if (res.ToLower().Contains("error") || res.ToLower().Contains("timedout"))
            {
                listRes.insertFlag = 2;
                listRes.insertOutput = "Failure";
            }

            //If there is an error with the Web API
            if (!strTransOut.ToLower().Contains("success"))
            {
                listRes.insertFlag = 0;
                listRes.insertOutput = "Failure";
            }

            return processResultForUploadInsertion((new JavaScriptSerializer()).Serialize(listRes), "EO - Eo Upload");
        }

        /* ************************ Generic method to capture exceptions ************************ */
        private JsonResult handleTrivialHttpRequests(string res)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = 2147483647;
            var JObj = ser.DeserializeObject(res);
            if (JObj is Array)
            {
                var results = ser.DeserializeObject(res);
                return new JsonResult()
                {
                    Data = results,
                    MaxJsonLength = 2147483647,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                }; 
            }
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
                //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                //return Json(result, JsonRequestBehavior.AllowGet);
                return new JsonResult()
                {
                    Data = res,
                    MaxJsonLength = 2147483647,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                }; 
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //Method to handle exception and return back a JSON result
        //Pass the exception string and the object which needs to be sent as JSON to the client
        private JsonResult handleTrivialHttpRequests<T>(string strExceptionString, T jsonObject)
        {
            //Check if the exception string is not empty
            if (!string.IsNullOrEmpty(strExceptionString))
            {
                //Check if the exception string has 'timedout' string
                if (strExceptionString.ToLower().Contains("timedout"))
                {
                    throw (new CustomExceptionHandler(Json("TimedOut")));
                }
                //Check if the exception string has 'databaseerror' string
                if (strExceptionString.ToLower().Contains("error"))
                {
                    throw (new CustomExceptionHandler(Json("DatabaseError")));
                }
                return new JsonResult()
                {
                    Data = jsonObject,
                    MaxJsonLength = 2147483647,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                }; 
                //return Json(jsonObject, JsonRequestBehavior.AllowGet);
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
    }
}