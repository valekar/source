using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using OfficeOpenXml;
using Orgler.Models.Upload;
using System.Configuration;
using Newtonsoft.Json.Serialization;
using System.Web.Script.Serialization;

namespace Orgler.Services
{
    public class ExcelServiceHelpers
    {
        //Method to read the number of rows present in the Excel uploaded
        public int getExcelRowCount(HttpPostedFile file, int intRow, string strRefType)
        {
            return getExcelRowCount(new MemoryStream(ReadFully(file.InputStream)), intRow, strRefType);
        }

        //Method to read the number of rows present in the Excel uploaded
        public bool checkIfTemplateIsValidHelper(HttpPostedFile file, int intRow, string strRefType)
        {
            return checkIfTemplateIsValid(new MemoryStream(ReadFully(file.InputStream)), intRow, strRefType);
        }

        //Method to check if duplicate file is present
        public bool checkIfUploadFileDuplicatesHelper(HttpPostedFile file, int intBusinessType)
        {
            bool boolDuplicatePresence = false;
            string strFileName = file.FileName;
            int intFileSize = file.ContentLength;
            string strExtn = Path.GetExtension(file.FileName);

            //Get the Upload Status file path froxm config
            string strUploadStatusFilePath = ConfigurationManager.AppSettings["UploadStatusPath"];

            string strJSONUploadStatus = string.Empty;

            //Check for the existance of the Upload Status file and read the file
            if(File.Exists(strUploadStatusFilePath))
            {
                strJSONUploadStatus = File.ReadAllText(strUploadStatusFilePath);

                //Deserialize
                List<UploadStat> listUploadStatus = new List<UploadStat>();
                listUploadStatus = (new JavaScriptSerializer()).Deserialize<List<UploadStat>>(strJSONUploadStatus);

                int intFileCount = listUploadStatus.Where(x => (x.BusinessType == intBusinessType && x.FileName == strFileName && x.FileSize == intFileSize && x.UploadStatus == "Success")).Count();
                if (intFileCount > 0)
                    boolDuplicatePresence = true;
                else
                    boolDuplicatePresence = false;
            }

            return boolDuplicatePresence;
        }

        //Method to read the number of rows present in the Excel uploaded
        public List<AffiliationUploadResult> getAffiliationResults(HttpPostedFile file, int intRow)
        {
            MemoryStream stream = new MemoryStream(ReadFully(file.InputStream));
            List<AffiliationUploadResult> listRes = new List<AffiliationUploadResult>();

            // open and read the XlSX file.
            using (var package = new ExcelPackage(stream))
            {
                // get the work book in the file
                ExcelWorkbook workBook = package.Workbook;

                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        // get the first worksheet
                        ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                        ExcelCellAddress excelStartAddress = currentWorksheet.Dimension.Start;
                        ExcelCellAddress excelEndAddress = currentWorksheet.Dimension.End;

                        // get the row count
                        int intFinalRow = excelEndAddress.Row;
                        for(int i = intRow; i <= intFinalRow; i++)
                        {
                            AffiliationUploadResult aff = new AffiliationUploadResult();
                            aff.strEnterpriseOrgId = currentWorksheet.Cells[i, 1].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 1].Value.ToString())) ? currentWorksheet.Cells[i, 1].Value.ToString().Trim() : "" : "";
                            aff.strMasterId = currentWorksheet.Cells[i, 2].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 2].Value.ToString())) ? currentWorksheet.Cells[i, 2].Value.ToString().Trim() : "" : "";
                            aff.strStatus = currentWorksheet.Cells[i, 3].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 3].Value.ToString())) ? currentWorksheet.Cells[i, 3].Value.ToString().Trim() : "Active" : "Active";
                            
                            //Eliminate blank rows
                            if (aff.strEnterpriseOrgId != "" || aff.strMasterId != "")
                            {
                                //Validate Enterprise ID
                                int ent = 0;
                                bool validEntId = int.TryParse(aff.strEnterpriseOrgId, out ent);
                                aff.strEnterpriseOrgIdFlag = "1";

                                if (aff.strEnterpriseOrgId == "") //Check for empty value
                                    aff.strEnterpriseOrgIdFlag = "0";
                                else if (!validEntId) //Check for valid integer
                                    aff.strEnterpriseOrgIdFlag = "0";

                                //Validate Master ID
                                int mstr = 0;
                                bool validMstrId = int.TryParse(aff.strMasterId, out mstr);
                                aff.strMasterIdFlag = "1";

                                if (aff.strMasterId == "") //Check for empty value
                                    aff.strMasterIdFlag = "0";
                                else if (!validMstrId) //Check for valid integer
                                    aff.strMasterIdFlag = "0";

                                listRes.Add(aff);
                            }
                        }
                    }
                }
            }

            return listRes;
        }

        //Method to read the number of rows present in the Excel uploaded
        public List<EosiUploadResult> getEosiResults(HttpPostedFile file, int intRow)
        {
            MemoryStream stream = new MemoryStream(ReadFully(file.InputStream));
            List<EosiUploadResult> listRes = new List<EosiUploadResult>();

            // open and read the XlSX file.
            using (var package = new ExcelPackage(stream))
            {
                // get the work book in the file
                ExcelWorkbook workBook = package.Workbook;

                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        // get the first worksheet
                        ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                        ExcelCellAddress excelStartAddress = currentWorksheet.Dimension.Start;
                        ExcelCellAddress excelEndAddress = currentWorksheet.Dimension.End;

                        // get the row count
                        int intFinalRow = excelEndAddress.Row;
                        for (int i = intRow; i <= intFinalRow; i++)
                        {
                            EosiUploadResult aff = new EosiUploadResult();
                            aff.strEnterpriseOrgId = currentWorksheet.Cells[i, 1].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 1].Value.ToString())) ? currentWorksheet.Cells[i, 1].Value.ToString().Trim() : "" : "";
                            aff.strMasterId = currentWorksheet.Cells[i, 2].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 2].Value.ToString())) ? currentWorksheet.Cells[i, 2].Value.ToString().Trim() : "" : "";
                            aff.strSourceSystemCode = currentWorksheet.Cells[i, 3].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 3].Value.ToString())) ? currentWorksheet.Cells[i, 3].Value.ToString().Trim() : "" : "";
                            aff.strSourceId = currentWorksheet.Cells[i, 4].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 4].Value.ToString())) ? currentWorksheet.Cells[i, 4].Value.ToString().Trim() : "" : "";
                            aff.strSecondarySourceId = currentWorksheet.Cells[i, 5].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 5].Value.ToString())) ? currentWorksheet.Cells[i, 5].Value.ToString().Trim() : "" : "";
                            aff.strParentEnterpriseOrgId = currentWorksheet.Cells[i, 6].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 6].Value.ToString())) ? currentWorksheet.Cells[i, 6].Value.ToString().Trim() : "" : "";
                            aff.strAltSourceCode = currentWorksheet.Cells[i, 7].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 7].Value.ToString())) ? currentWorksheet.Cells[i, 7].Value.ToString().Trim() : "" : "";
                            aff.strAltSourceId = currentWorksheet.Cells[i, 8].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 8].Value.ToString())) ? currentWorksheet.Cells[i, 8].Value.ToString().Trim() : "" : "";
                            aff.strOrgName = currentWorksheet.Cells[i, 9].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 9].Value.ToString())) ? currentWorksheet.Cells[i, 9].Value.ToString().Trim() : "" : "";
                            aff.strAddress1Street1 = currentWorksheet.Cells[i, 10].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 10].Value.ToString())) ? currentWorksheet.Cells[i, 10].Value.ToString().Trim() : "" : "";
                            aff.strAddress1Street2 = currentWorksheet.Cells[i, 11].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 11].Value.ToString())) ? currentWorksheet.Cells[i, 11].Value.ToString().Trim() : "" : "";
                            aff.strAddress1City = currentWorksheet.Cells[i, 12].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 12].Value.ToString())) ? currentWorksheet.Cells[i, 12].Value.ToString().Trim() : "" : "";
                            aff.strAddress1State = currentWorksheet.Cells[i, 13].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 13].Value.ToString())) ? currentWorksheet.Cells[i, 13].Value.ToString().Trim() : "" : "";
                            aff.strAddress1Zip = currentWorksheet.Cells[i, 14].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 14].Value.ToString())) ? currentWorksheet.Cells[i, 14].Value.ToString().Trim() : "" : "";
                            aff.strPhone1 = currentWorksheet.Cells[i, 15].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 15].Value.ToString())) ? currentWorksheet.Cells[i, 15].Value.ToString().Trim() : "" : "";
                            aff.strPhone2 = currentWorksheet.Cells[i, 16].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 16].Value.ToString())) ? currentWorksheet.Cells[i, 16].Value.ToString().Trim() : "" : "";
                            aff.strNaicsCode = currentWorksheet.Cells[i, 17].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 17].Value.ToString())) ? currentWorksheet.Cells[i, 17].Value.ToString().Trim() : "" : "";
                            aff.strCharacteristics1Code = currentWorksheet.Cells[i, 18].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 18].Value.ToString())) ? currentWorksheet.Cells[i, 18].Value.ToString().Trim() : "" : "";
                            aff.strCharacteristics1Value = currentWorksheet.Cells[i, 19].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 19].Value.ToString())) ? currentWorksheet.Cells[i, 19].Value.ToString().Trim() : "" : "";
                            aff.strCharacteristics2Code = currentWorksheet.Cells[i, 20].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 20].Value.ToString())) ? currentWorksheet.Cells[i, 20].Value.ToString().Trim() : "" : "";
                            aff.strCharacteristics2Value = currentWorksheet.Cells[i, 21].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 21].Value.ToString())) ? currentWorksheet.Cells[i, 21].Value.ToString().Trim() : "" : "";
                            aff.strRMIndicator = currentWorksheet.Cells[i, 22].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 22].Value.ToString())) ? currentWorksheet.Cells[i, 22].Value.ToString().Trim() : "0" : "0";
                            aff.strNotes = currentWorksheet.Cells[i, 23].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 23].Value.ToString())) ? currentWorksheet.Cells[i, 23].Value.ToString().Trim() : "" : "";
                            
                            //Eliminate blank rows
                            if (aff.strEnterpriseOrgId != "" || aff.strMasterId != "" || aff.strSourceSystemCode != "" || aff.strSourceId != ""
                                || aff.strSecondarySourceId != "" || aff.strParentEnterpriseOrgId != "" || aff.strAltSourceCode != "" || aff.strAltSourceId != "" || aff.strOrgName != "" || aff.strAddress1Street1 != ""
                                || aff.strAddress1Street2 != "" || aff.strAddress1City != "" || aff.strAddress1State != "" || aff.strAddress1Zip != ""
                                || aff.strPhone1 != "" || aff.strPhone2 != "" || aff.strNaicsCode != "" || aff.strCharacteristics1Code != ""
                                || aff.strCharacteristics1Value != "" || aff.strCharacteristics2Code != "" || aff.strCharacteristics2Value != "")
                            {
                                aff.strEnterpriseOrgIdFlag = "1";
                                aff.strMasterIdFlag = "1";
                                aff.strSourceIdFlag = "1";
                                aff.strSourceSystemCodeFlag = "1";
                                aff.strSecondarySourceIdFlag = "1";
                                aff.strParentEnterpriseOrgIdFlag = "1";
                                aff.strNaicsCodeFlag = "1";
                                aff.strCharacteristics1CodeFlag = "1";
                                aff.strCharacteristics2CodeFlag = "1";
                                aff.strCharacteristics1ValueFlag = "1";
                                aff.strCharacteristics2ValueFlag = "1";

                                //Validate unique id presence
                                //if (!(aff.strMasterId != "" || (aff.strSourceSystemCode != "" && aff.strSourceId != "")))
                                if (!(aff.strMasterId != "" || aff.strSourceSystemCode != ""))
                                { 
                                    aff.strMasterIdFlag = "0";
                                    //aff.strSourceIdFlag = "0";
                                    aff.strSourceSystemCodeFlag = "0";
                                }

                                //Validate Enterprise ID
                                int ent = 0;
                                bool validEntId = int.TryParse(aff.strEnterpriseOrgId, out ent);
                                if (aff.strEnterpriseOrgId != "" && !validEntId) //Check for valid integer
                                    aff.strEnterpriseOrgIdFlag = "0";

                                //Validate Master ID
                                int mstr = 0;
                                bool validMstrId = int.TryParse(aff.strMasterId, out mstr);
                                if (aff.strMasterId != "" && !validMstrId) //Check for valid integer
                                    aff.strMasterIdFlag = "0";

                                //Validate the length of the source code column
                                if (!string.IsNullOrEmpty(aff.strSourceSystemCode) && aff.strSourceSystemCode.Length != 4)
                                    aff.strSourceSystemCodeFlag = "0";

                                //Validate Parent Enterprise ID
                                //int prarentent = 0;
                                //bool validParentEntId = int.TryParse(aff.strParentEnterpriseOrgId, out prarentent);
                                //if (aff.strParentEnterpriseOrgId != "" && !validParentEntId) //Check for valid integer
                                //    aff.strParentEnterpriseOrgIdFlag = "0";

                                //Validate Characteristic Values
                                if (aff.strCharacteristics1Code != "" && aff.strCharacteristics1Value == "")
                                    aff.strCharacteristics1ValueFlag = "0";

                                if (aff.strCharacteristics2Code != "" && aff.strCharacteristics2Value == "")
                                    aff.strCharacteristics2ValueFlag = "0";

                                if (aff.strCharacteristics1Code == "" && aff.strCharacteristics1Value != "")
                                    aff.strCharacteristics1CodeFlag = "0";

                                if (aff.strCharacteristics2Code == "" && aff.strCharacteristics2Value != "")
                                    aff.strCharacteristics2CodeFlag = "0";

                                //validate the presence of source system information for providing updates to the secondary source id
                                //if (aff.strSecondarySourceId != "" && (aff.strSourceSystemCode == "" || aff.strSourceId == ""))
                                if (aff.strSecondarySourceId != "" && (aff.strSourceSystemCode == ""))
                                    aff.strSecondarySourceIdFlag = "0";

                                listRes.Add(aff);
                            }
                        }
                    }
                }
            }

            return listRes;
        }

        //Method to read the number of rows present in the Excel uploaded
        public List<EoUploadResult> getEoResults(HttpPostedFile file, int intRow)
        {
            MemoryStream stream = new MemoryStream(ReadFully(file.InputStream));
            List<EoUploadResult> listRes = new List<EoUploadResult>();

            // open and read the XlSX file.
            using (var package = new ExcelPackage(stream))
            {
                // get the work book in the file
                ExcelWorkbook workBook = package.Workbook;

                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        // get the first worksheet
                        ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                        ExcelCellAddress excelStartAddress = currentWorksheet.Dimension.Start;
                        ExcelCellAddress excelEndAddress = currentWorksheet.Dimension.End;

                        // get the row count
                        int intFinalRow = excelEndAddress.Row;
                        for (int i = intRow; i <= intFinalRow; i++)
                        {
                            EoUploadResult fileData = new EoUploadResult();
                            fileData.strEnterpriseOrgId = currentWorksheet.Cells[i, 1].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 1].Value.ToString())) ? currentWorksheet.Cells[i, 1].Value.ToString().Trim() : "" : "";
                            fileData.strEnterpriseOrgName = currentWorksheet.Cells[i, 2].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 2].Value.ToString())) ? currentWorksheet.Cells[i, 2].Value.ToString().Trim() : "" : "";
                            fileData.strCharacteristics1Code = currentWorksheet.Cells[i, 3].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 3].Value.ToString())) ? currentWorksheet.Cells[i, 3].Value.ToString().Trim() : "" : "";
                            fileData.strCharacteristics1Value = currentWorksheet.Cells[i, 4].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 4].Value.ToString())) ? currentWorksheet.Cells[i, 4].Value.ToString().Trim() : "" : "";
                            fileData.strCharacteristics2Code = currentWorksheet.Cells[i, 5].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 5].Value.ToString())) ? currentWorksheet.Cells[i, 5].Value.ToString().Trim() : "" : "";
                            fileData.strCharacteristics2Value = currentWorksheet.Cells[i, 6].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 6].Value.ToString())) ? currentWorksheet.Cells[i, 6].Value.ToString().Trim() : "" : "";
                            fileData.strCharacteristics3Code = currentWorksheet.Cells[i, 7].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 7].Value.ToString())) ? currentWorksheet.Cells[i, 7].Value.ToString().Trim() : "" : "";
                            fileData.strCharacteristics3Value = currentWorksheet.Cells[i, 8].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 8].Value.ToString())) ? currentWorksheet.Cells[i, 8].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition1Type1 = currentWorksheet.Cells[i, 9].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 9].Value.ToString())) ? currentWorksheet.Cells[i, 9].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition1String1 = currentWorksheet.Cells[i, 10].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 10].Value.ToString())) ? currentWorksheet.Cells[i, 10].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition1Type2 = currentWorksheet.Cells[i, 11].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 11].Value.ToString())) ? currentWorksheet.Cells[i, 11].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition1String2 = currentWorksheet.Cells[i, 12].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 12].Value.ToString())) ? currentWorksheet.Cells[i, 12].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition1Type3 = currentWorksheet.Cells[i, 13].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 13].Value.ToString())) ? currentWorksheet.Cells[i, 13].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition1String3 = currentWorksheet.Cells[i, 14].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 14].Value.ToString())) ? currentWorksheet.Cells[i, 14].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition2Type1 = currentWorksheet.Cells[i, 15].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 15].Value.ToString())) ? currentWorksheet.Cells[i, 15].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition2String1 = currentWorksheet.Cells[i, 16].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 16].Value.ToString())) ? currentWorksheet.Cells[i, 16].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition2Type2 = currentWorksheet.Cells[i, 17].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 17].Value.ToString())) ? currentWorksheet.Cells[i, 17].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition2String2 = currentWorksheet.Cells[i, 18].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 18].Value.ToString())) ? currentWorksheet.Cells[i, 18].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition2Type3 = currentWorksheet.Cells[i, 19].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 19].Value.ToString())) ? currentWorksheet.Cells[i, 19].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition2String3 = currentWorksheet.Cells[i, 20].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 20].Value.ToString())) ? currentWorksheet.Cells[i, 20].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition3Type1 = currentWorksheet.Cells[i, 21].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 21].Value.ToString())) ? currentWorksheet.Cells[i, 21].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition3String1 = currentWorksheet.Cells[i, 22].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 22].Value.ToString())) ? currentWorksheet.Cells[i, 22].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition3Type2 = currentWorksheet.Cells[i, 23].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 23].Value.ToString())) ? currentWorksheet.Cells[i, 23].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition3String2 = currentWorksheet.Cells[i, 24].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 24].Value.ToString())) ? currentWorksheet.Cells[i, 24].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition3Type3 = currentWorksheet.Cells[i, 25].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 25].Value.ToString())) ? currentWorksheet.Cells[i, 25].Value.ToString().Trim() : "" : "";
                            fileData.strTransformCondition3String3 = currentWorksheet.Cells[i, 26].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 26].Value.ToString())) ? currentWorksheet.Cells[i, 26].Value.ToString().Trim() : "" : "";
                            fileData.strTag1 = currentWorksheet.Cells[i, 27].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 27].Value.ToString())) ? currentWorksheet.Cells[i, 27].Value.ToString().Trim() : "" : "";
                            fileData.strTag2 = currentWorksheet.Cells[i, 28].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 28].Value.ToString())) ? currentWorksheet.Cells[i, 28].Value.ToString().Trim() : "" : "";
                            fileData.strTag3 = currentWorksheet.Cells[i, 29].Value != null ? (!string.IsNullOrEmpty(currentWorksheet.Cells[i, 29].Value.ToString())) ? currentWorksheet.Cells[i, 29].Value.ToString().Trim() : "" : "";
                            fileData.strAction = !string.IsNullOrEmpty(fileData.strEnterpriseOrgName) ? (string.IsNullOrEmpty(fileData.strEnterpriseOrgId) ? "Insert" : "") : (!string.IsNullOrEmpty(fileData.strEnterpriseOrgId) ? "Update" : "");

                            //Eliminate blank rows
                            if (fileData.strEnterpriseOrgId != "" || fileData.strEnterpriseOrgName != "" || fileData.strCharacteristics1Code != "" || fileData.strCharacteristics1Value != ""
                                || fileData.strCharacteristics2Code != "" || fileData.strCharacteristics2Value != "" || fileData.strCharacteristics3Code != "" || fileData.strCharacteristics3Value != ""
                                || fileData.strTransformCondition1Type1 != "" || fileData.strTransformCondition1String1 != "" || fileData.strTransformCondition1Type2 != "" || fileData.strTransformCondition1String2 != "" || fileData.strTransformCondition1Type3 != "" || fileData.strTransformCondition1String3 != ""
                                || fileData.strTransformCondition2Type1 != "" || fileData.strTransformCondition2String1 != "" || fileData.strTransformCondition2Type2 != "" || fileData.strTransformCondition2String2 != "" || fileData.strTransformCondition2Type3 != "" || fileData.strTransformCondition2String3 != ""
                                || fileData.strTransformCondition3Type1 != "" || fileData.strTransformCondition3String1 != "" || fileData.strTransformCondition3Type2 != "" || fileData.strTransformCondition3String2 != "" || fileData.strTransformCondition3Type3 != "" || fileData.strTransformCondition3String3 != ""
                                || fileData.strTag1 != "" || fileData.strTag2 != "" || fileData.strTag3 != "")
                            {
                                //Validate unique id presence
                                if ((string.IsNullOrEmpty(fileData.strEnterpriseOrgName) && string.IsNullOrEmpty(fileData.strEnterpriseOrgId))
                                    || (!string.IsNullOrEmpty(fileData.strEnterpriseOrgName) && !string.IsNullOrEmpty(fileData.strEnterpriseOrgId)))
                                {
                                    fileData.strEnterpriseOrgIdFlag = "0";
                                    fileData.strEnterpriseOrgNameFlag = "0";
                                }
                                //if (fileData.strAction.ToLower() == "insert") {
                                //    if (string.IsNullOrEmpty(fileData.strEnterpriseOrgName) || !string.IsNullOrEmpty(fileData.strEnterpriseOrgId))
                                //    {
                                //        fileData.strEnterpriseOrgIdFlag = "0";
                                //        fileData.strEnterpriseOrgNameFlag = "0";
                                //    }
                                //}
                                //else
                                //{
                                //    if (!(!string.IsNullOrEmpty(fileData.strEnterpriseOrgName) || !string.IsNullOrEmpty(fileData.strEnterpriseOrgId)))
                                //    {
                                //        fileData.strEnterpriseOrgIdFlag = "0";
                                //        fileData.strEnterpriseOrgNameFlag = "0";
                                //    }
                                //}

                                //Validate Enterprise ID
                                int ent = 0;
                                bool validEntId = int.TryParse(fileData.strEnterpriseOrgId, out ent);
                                if (fileData.strEnterpriseOrgIdFlag == "1" && fileData.strEnterpriseOrgId != "" && !validEntId) //Check for valid integer
                                    fileData.strEnterpriseOrgIdFlag = "0";

                                //Validate Characteristic Type Value population
                                if (fileData.strCharacteristics1Code != "" && fileData.strCharacteristics1Value == "")
                                    fileData.strCharacteristics1ValueFlag = "0";

                                if (fileData.strCharacteristics2Code != "" && fileData.strCharacteristics2Value == "")
                                    fileData.strCharacteristics2ValueFlag = "0";

                                if (fileData.strCharacteristics3Code != "" && fileData.strCharacteristics3Value == "")
                                    fileData.strCharacteristics3ValueFlag = "0";

                                if (fileData.strCharacteristics1Code == "" && fileData.strCharacteristics1Value != "")
                                    fileData.strCharacteristics1CodeFlag = "0";

                                if (fileData.strCharacteristics2Code == "" && fileData.strCharacteristics2Value != "")
                                    fileData.strCharacteristics2CodeFlag = "0";

                                if (fileData.strCharacteristics3Code == "" && fileData.strCharacteristics3Value != "")
                                    fileData.strCharacteristics3CodeFlag = "0";

                                //Validate Transformation Type Validation
                                List<string> ltTransformationTypes = new List<string> { "contains", "does not contain", "exact match", "starts with" };
                                if (fileData.strTransformCondition1Type1 != "" && !ltTransformationTypes.Contains(fileData.strTransformCondition1Type1.ToLower()))
                                    fileData.strTransformCondition1Type1Flag = "0";
                                if (fileData.strTransformCondition1Type2 != "" && !ltTransformationTypes.Contains(fileData.strTransformCondition1Type2.ToLower()))
                                    fileData.strTransformCondition1Type2Flag = "0";
                                if (fileData.strTransformCondition1Type3 != "" && !ltTransformationTypes.Contains(fileData.strTransformCondition1Type3.ToLower()))
                                    fileData.strTransformCondition1Type3Flag = "0";

                                if (fileData.strTransformCondition2Type1 != "" && !ltTransformationTypes.Contains(fileData.strTransformCondition2Type1.ToLower()))
                                    fileData.strTransformCondition2Type1Flag = "0";
                                if (fileData.strTransformCondition2Type2 != "" && !ltTransformationTypes.Contains(fileData.strTransformCondition2Type2.ToLower()))
                                    fileData.strTransformCondition2Type2Flag = "0";
                                if (fileData.strTransformCondition2Type3 != "" && !ltTransformationTypes.Contains(fileData.strTransformCondition2Type3.ToLower()))
                                    fileData.strTransformCondition2Type3Flag = "0";

                                if (fileData.strTransformCondition3Type1 != "" && !ltTransformationTypes.Contains(fileData.strTransformCondition3Type1.ToLower()))
                                    fileData.strTransformCondition3Type1Flag = "0";
                                if (fileData.strTransformCondition3Type2 != "" && !ltTransformationTypes.Contains(fileData.strTransformCondition3Type2.ToLower()))
                                    fileData.strTransformCondition3Type2Flag = "0";
                                if (fileData.strTransformCondition3Type3 != "" && !ltTransformationTypes.Contains(fileData.strTransformCondition3Type3.ToLower()))
                                    fileData.strTransformCondition3Type3Flag = "0";

                                //Validate the presence of values against the transformation condition columns
                                if (fileData.strTransformCondition1Type1 != "" && fileData.strTransformCondition1Type1Flag == "1" && fileData.strTransformCondition1String1.Length <= 1)
                                    fileData.strTransformCondition1String1Flag = "0";
                                if (fileData.strTransformCondition1Type1 == "" && fileData.strTransformCondition1String1 != "")
                                    fileData.strTransformCondition1String1Flag = "0";

                                if (fileData.strTransformCondition1Type2 != "" && fileData.strTransformCondition1Type2Flag == "1" && fileData.strTransformCondition1String2.Length <= 1)
                                    fileData.strTransformCondition1String2Flag = "0";
                                if (fileData.strTransformCondition1Type2 == "" && fileData.strTransformCondition1String2 != "")
                                    fileData.strTransformCondition1String2Flag = "0";

                                if (fileData.strTransformCondition1Type3 != "" && fileData.strTransformCondition1Type3Flag == "1" && fileData.strTransformCondition1String3.Length <= 1)
                                    fileData.strTransformCondition1String3Flag = "0";
                                if (fileData.strTransformCondition1Type3 == "" && fileData.strTransformCondition1String3 != "")
                                    fileData.strTransformCondition1String3Flag = "0";

                                if (fileData.strTransformCondition2Type1 != "" && fileData.strTransformCondition2Type1Flag == "1" && fileData.strTransformCondition2String1.Length <= 1)
                                    fileData.strTransformCondition2String1Flag = "0";
                                if (fileData.strTransformCondition2Type1 == "" && fileData.strTransformCondition2String1 != "")
                                    fileData.strTransformCondition2String1Flag = "0";

                                if (fileData.strTransformCondition2Type2 != "" && fileData.strTransformCondition2Type2Flag == "1" && fileData.strTransformCondition2String2.Length <= 1)
                                    fileData.strTransformCondition2String2Flag = "0";
                                if (fileData.strTransformCondition2Type2 == "" && fileData.strTransformCondition2String2 != "")
                                    fileData.strTransformCondition2String2Flag = "0";

                                if (fileData.strTransformCondition2Type3 != "" && fileData.strTransformCondition2Type3Flag == "1" && fileData.strTransformCondition2String3.Length <= 1)
                                    fileData.strTransformCondition2String3Flag = "0";
                                if (fileData.strTransformCondition2Type3 == "" && fileData.strTransformCondition2String3 != "")
                                    fileData.strTransformCondition2String3Flag = "0";

                                if (fileData.strTransformCondition3Type1 != "" && fileData.strTransformCondition3Type1Flag == "1" && fileData.strTransformCondition3String1.Length <= 1)
                                    fileData.strTransformCondition3String1Flag = "0";                           
                                if (fileData.strTransformCondition3Type1 == "" && fileData.strTransformCondition3String1 != "")
                                    fileData.strTransformCondition3String1Flag = "0";

                                if (fileData.strTransformCondition3Type2 != "" && fileData.strTransformCondition3Type2Flag == "1" && fileData.strTransformCondition3String2.Length <= 1)
                                    fileData.strTransformCondition3String2Flag = "0";                           
                                if (fileData.strTransformCondition3Type2 == "" && fileData.strTransformCondition3String2 != "")
                                    fileData.strTransformCondition3String2Flag = "0";

                                if (fileData.strTransformCondition3Type3 != "" && fileData.strTransformCondition3Type3Flag == "1" && fileData.strTransformCondition3String3.Length <= 1)
                                    fileData.strTransformCondition3String3Flag = "0";                           
                                if (fileData.strTransformCondition3Type3 == "" && fileData.strTransformCondition3String3 != "")
                                    fileData.strTransformCondition3String3Flag = "0";

                                //Validate for the presence of rules other than 'does not contain'
                                if ((fileData.strTransformCondition1Type1 != "" && fileData.strTransformCondition1Type1Flag == "1" && fileData.strTransformCondition1String1Flag == "1")
                                    || (fileData.strTransformCondition1Type2 != "" && fileData.strTransformCondition1Type2Flag == "1" && fileData.strTransformCondition1String2Flag == "1")
                                    || (fileData.strTransformCondition1Type3 != "" && fileData.strTransformCondition1Type3Flag == "1" && fileData.strTransformCondition1String3Flag == "1"))
                                {
                                    if(!((fileData.strTransformCondition1Type1 != "" && fileData.strTransformCondition1Type1.ToLower() != "does not contain")
                                        || (fileData.strTransformCondition1Type2 != "" && fileData.strTransformCondition1Type2.ToLower() != "does not contain")
                                        || (fileData.strTransformCondition1Type3 != "" && fileData.strTransformCondition1Type3.ToLower() != "does not contain")))
                                    {
                                        fileData.strTransformCondition1Type1Flag = "0";
                                        fileData.strTransformCondition1String1Flag = "0";
                                        fileData.strTransformCondition1Type2Flag = "0";
                                        fileData.strTransformCondition1String2Flag = "0";
                                        fileData.strTransformCondition1Type3Flag = "0";
                                        fileData.strTransformCondition1String3Flag = "0";
                                    }
                                }

                                if ((fileData.strTransformCondition2Type1 != "" && fileData.strTransformCondition2Type1Flag == "1" && fileData.strTransformCondition2String1Flag == "1")
                                    || (fileData.strTransformCondition2Type2 != "" && fileData.strTransformCondition2Type2Flag == "1" && fileData.strTransformCondition2String2Flag == "1")
                                    || (fileData.strTransformCondition2Type3 != "" && fileData.strTransformCondition2Type3Flag == "1" && fileData.strTransformCondition2String3Flag == "1"))
                                {
                                    if (!((fileData.strTransformCondition2Type1 != "" && fileData.strTransformCondition2Type1.ToLower() != "does not contain")
                                        || (fileData.strTransformCondition2Type2 != "" && fileData.strTransformCondition2Type2.ToLower() != "does not contain")
                                        || (fileData.strTransformCondition2Type3 != "" && fileData.strTransformCondition2Type3.ToLower() != "does not contain")))
                                    {
                                        fileData.strTransformCondition2Type1Flag = "0";
                                        fileData.strTransformCondition2String1Flag = "0";
                                        fileData.strTransformCondition2Type2Flag = "0";
                                        fileData.strTransformCondition2String2Flag = "0";
                                        fileData.strTransformCondition2Type3Flag = "0";
                                        fileData.strTransformCondition2String3Flag = "0";
                                    }
                                }

                                if ((fileData.strTransformCondition3Type1 != "" && fileData.strTransformCondition3Type1Flag == "1" && fileData.strTransformCondition3String1Flag == "1")
                                    || (fileData.strTransformCondition3Type2 != "" && fileData.strTransformCondition3Type2Flag == "1" && fileData.strTransformCondition3String2Flag == "1")
                                    || (fileData.strTransformCondition3Type3 != "" && fileData.strTransformCondition3Type3Flag == "1" && fileData.strTransformCondition3String3Flag == "1"))
                                {
                                    if (!((fileData.strTransformCondition3Type1 != "" && fileData.strTransformCondition3Type1.ToLower() != "does not contain")
                                        || (fileData.strTransformCondition3Type2 != "" && fileData.strTransformCondition3Type2.ToLower() != "does not contain")
                                        || (fileData.strTransformCondition3Type3 != "" && fileData.strTransformCondition3Type3.ToLower() != "does not contain")))
                                    {
                                        fileData.strTransformCondition3Type1Flag = "0";
                                        fileData.strTransformCondition3String1Flag = "0";
                                        fileData.strTransformCondition3Type2Flag = "0";
                                        fileData.strTransformCondition3String2Flag = "0";
                                        fileData.strTransformCondition3Type3Flag = "0";
                                        fileData.strTransformCondition3String3Flag = "0";
                                    }
                                }

                                listRes.Add(fileData);
                            }
                        }
                    }
                }
            }

            return listRes;
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                input.Position = 0;
                return ms.ToArray();
            }
        }

        //Method to get the number of rows in the uploaded file excluding the header
        public int getExcelRowCount(MemoryStream stream, int intRow, string strRefType)
        {
            int totalRowCount = 0;

            // open and read the XlSX file.
            using (var package = new ExcelPackage(stream))
            {
                // get the work book in the file
                ExcelWorkbook workBook = package.Workbook;

                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        // get the first worksheet
                        ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                        ExcelCellAddress excelStartAddress = currentWorksheet.Dimension.Start;
                        ExcelCellAddress excelEndAddress = currentWorksheet.Dimension.End;

                        // get the row count
                        totalRowCount = excelEndAddress.Row;
                        totalRowCount = totalRowCount - (intRow - 1);
                    }
                }
            }
            return totalRowCount;
        }

        //Method to get the number of rows in the uploaded file excluding the header
        public bool checkIfTemplateIsValid(MemoryStream stream, int intRow, string strRefType)
        {
            int validColumns = 0;
            bool boolValidTemplate = false;

            // open and read the XlSX file.
            using (var package = new ExcelPackage(stream))
            {
                // get the work book in the file
                ExcelWorkbook workBook = package.Workbook;

                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        // get the first worksheet
                        ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                        ExcelCellAddress excelStartAddress = currentWorksheet.Dimension.Start;
                        ExcelCellAddress excelEndAddress = currentWorksheet.Dimension.End;

                        List<string> listColumnNames = new List<string>();
                        listColumnNames = getExcelExportColumnNames(strRefType);

                        //Check if the first line is same as the template
                        if (listColumnNames.Count >= 0)
                        {
                            int totalValidColumns = listColumnNames.Count;
                            int ColumnNum = 1;
                            foreach (string strColName in listColumnNames)
                            {
                                validColumns += checkColumnHeader(2, ColumnNum, strColName, currentWorksheet);
                                ColumnNum += 1;
                            }

                            if (validColumns == totalValidColumns)
                                boolValidTemplate = true;
                        }
                    }
                }
            }
            return boolValidTemplate;
        }

        private int checkColumnHeader(int rowNum, int colNum, string strDefaultValue, ExcelWorksheet excelWorksheet)
        {
            int validCol = 0;
            if (excelWorksheet.Cells[rowNum, colNum].Value != null)
            {
                if (excelWorksheet.Cells[rowNum, colNum].Value.ToString() == strDefaultValue)
                {
                    validCol = 1;
                }
            }
            return validCol;
        }

        /* Upload Validation - Get the Excel Cell Header Column to validate the template file that is being uploaded
         * listRes - List of column headers present in the Reference data set bulk upload template file
        */
        public List<string> getExcelExportColumnNames(string strUploadType)
        {
            Dictionary<string, List<string>> dictResult = new Dictionary<string, List<string>>();
            dictResult.Add("affiliation upload", new List<string> { "Enterprise Organization ID", "Organization Master ID", "Active/Inactive" });
            dictResult.Add("eosi upload", new List<string> { "Enterprise Organization ID", "Master Id", "Source Code", "Source ID", "Secondary Source Id", "Parent Source ID", "Alternate Source Code", "Alternate Source ID", "Organization Name", "Address Line 1 A", "Address Line 2 A", "City A", "State A", "Zip A", "Phone A", "Phone B", "Primary NAICS code", "Characteristic Type 1", "Characteristic Value 1", "Characteristic Type 2", "Characteristic Value 2", "Remove Indicator", "Notes" });
            dictResult.Add("eo upload", new List<string> { "Enterprise Organization ID", "Enterprise Organization Name", "Characteristic Type 1", "Characteristic Value 1", "Characteristic Type 2", "Characteristic Value 2", "Characteristic Type 3", "Characteristic Value 3", "Affiliation Match Rule 1_Condition Type 1", "Affiliation Match Rule 1_String 1", "Affiliation Match Rule 1_Condition Type 2", "Affiliation Match Rule 1_String 2", "Affiliation Match Rule 1_Condition Type 3", "Affiliation Match Rule 1_String 3", "Affiliation Match Rule 2_Condition Type 1", "Affiliation Match Rule 2_String 1", "Affiliation Match Rule 2_Condition Type 2", "Affiliation Match Rule 2_String 2", "Affiliation Match Rule 2_Condition Type 3", "Affiliation Match Rule 2_String 3", "Affiliation Match Rule 3_Condition Type 1", "Affiliation Match Rule 3_String 1", "Affiliation Match Rule 3_Condition Type 2", "Affiliation Match Rule 3_String 2", "Affiliation Match Rule 3_Condition Type 3", "Affiliation Match Rule 3_String 3", "Tag_1", "Tag_2", "Tag_3" });

            List<string> listRes = new List<string>();
            listRes = dictResult[strUploadType.ToLower()];
            return listRes;
        }
    }
}