using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Constituents;

namespace ARC.Donor.Data.Constituents
{
    public class ConstituentMerge
    {
        /* Method to get the master level details
         * Input Parameters : Constituent Type, List of Master Ids
         * Output Parameter : Dictionary of master level data
         */
        public IList<CompareOutput> getCompareData(MasterDetailsInput masterDetailsInsert)
        {
            Repository rep = new Repository();
            //Pick up the distinct master ids
            masterDetailsInsert.MasterId = masterDetailsInsert.MasterId.Distinct().ToList();

            List<CompareOutput> listCompareOutput = new List<CompareOutput>();

            var resultMasterLst = rep.ExecuteSqlQuery<MasterData>(SQL.Constituents.Compare.getMasterSQL(masterDetailsInsert.MasterId)).ToList();
            var resultPersonNameLst = rep.ExecuteSqlQuery<MasterNameData>(SQL.Constituents.Compare.getMasterPersonNameSQL(masterDetailsInsert.MasterId)).ToList();
            var resultOrgNameLst = rep.ExecuteSqlQuery<MasterNameData>(SQL.Constituents.Compare.getMasterOrgNameSQL(masterDetailsInsert.MasterId)).ToList();
            var resultAddressLst = rep.ExecuteSqlQuery<MasterAddressData>(SQL.Constituents.Compare.getMasterAddressSQL(masterDetailsInsert.MasterId)).ToList();
            var resultPhoneLst = rep.ExecuteSqlQuery<MasterPhoneData>(SQL.Constituents.Compare.getMasterPhoneSQL(masterDetailsInsert.MasterId)).ToList();
            var resultEmailLst = rep.ExecuteSqlQuery<MasterEmailData>(SQL.Constituents.Compare.getMasterEmailSQL(masterDetailsInsert.MasterId)).ToList();
            var resultBridgeCountLst = rep.ExecuteSqlQuery<MasterBridgeData>(SQL.Constituents.Compare.getMasterBridgeCountSQL(masterDetailsInsert.MasterId)).ToList();
            var resultBirthLst = rep.ExecuteSqlQuery<MasterBirthData>(SQL.Constituents.Compare.getMasterBirthSQL(masterDetailsInsert.MasterId)).ToList();
            var resultDeathLst = rep.ExecuteSqlQuery<MasterDeathData>(SQL.Constituents.Compare.getMasterDeathSQL(masterDetailsInsert.MasterId)).ToList();
            var resultNameLst = (masterDetailsInsert.ConstituentType == "IN") ? resultPersonNameLst : resultOrgNameLst;

            //Consolidate into a single object

            Dictionary<string, Dictionary<String, Dictionary<String, List<String>>>> dictCompareResult = new Dictionary<string, Dictionary<String, Dictionary<String, List<String>>>>();
            // add an entry to the results per master id supplied
            foreach (string strMasterId in masterDetailsInsert.MasterId)
            {
                // initialize a default dictionary that can be added to all masters
                Dictionary<String, Dictionary<String, List<String>>> dictLocatorDetails = new Dictionary<String, Dictionary<String, List<String>>>();
                dictLocatorDetails.Add("Dsp Id", new Dictionary<String, List<String>>());
                dictLocatorDetails.Add("Bridge Presence", new Dictionary<String, List<String>>());
                dictLocatorDetails.Add("Name", new Dictionary<String, List<String>>());
                dictLocatorDetails.Add("Address", new Dictionary<String, List<String>>());
                dictLocatorDetails.Add("Phone", new Dictionary<String, List<String>>());
                dictLocatorDetails.Add("Email", new Dictionary<String, List<String>>());
                if (masterDetailsInsert.ConstituentType == "IN")
                {
                    dictLocatorDetails.Add("Birth", new Dictionary<String, List<String>>());
                    dictLocatorDetails.Add("Death", new Dictionary<String, List<String>>());
                }

                dictCompareResult.Add(strMasterId, dictLocatorDetails);
            }

            // retrieve and add dsp id entries to the dictionary
            if (resultMasterLst.Count > 0)
            {
                foreach (MasterData drMasterData in resultMasterLst)
                {
                    string strMasterId = drMasterData.cnst_mstr_id.ToString();
                    string strDspIdEntry = (drMasterData.cnst_dsp_id != null ? drMasterData.cnst_dsp_id.ToString() : string.Empty);
                    Dictionary<string, List<string>> dictSourceSystemAndDetails = dictCompareResult[strMasterId]["Dsp Id"];
                    if (!dictSourceSystemAndDetails.ContainsKey("dsp_id"))
                    {
                        dictSourceSystemAndDetails.Add("dsp_id", new List<String>());
                    }
                    dictSourceSystemAndDetails["dsp_id"].Add(strDspIdEntry);
                }
            }

            // retrieve and add name entries to the dictionary
            if (resultNameLst.Count > 0)
            {
                foreach (MasterNameData drMasterNameData in resultNameLst)
                {
                    //remove special characters
                    drMasterNameData.name = !string.IsNullOrEmpty(drMasterNameData.name) ? getValidString(drMasterNameData.name.ToString()) : string.Empty;

                    string strMasterId = drMasterNameData.cnst_mstr_id.ToString();
                    string strNameEntry = drMasterNameData.name.ToString();
                    Dictionary<string, List<string>> dictSourceSystemAndDetails = dictCompareResult[strMasterId]["Name"];
                    if (!dictSourceSystemAndDetails.ContainsKey(drMasterNameData.arc_srcsys_cd.ToString()))
                    {
                        dictSourceSystemAndDetails.Add(drMasterNameData.arc_srcsys_cd.ToString(), new List<String>());
                    }
                    dictSourceSystemAndDetails[drMasterNameData.arc_srcsys_cd.ToString()].Add(strNameEntry);
                }
            }

            // retrieve and add address entries to the dictionary
            if (resultAddressLst.Count > 0)
            {
                foreach (MasterAddressData drMasterAddressData in resultAddressLst)
                {
                    //remove special characters
                    drMasterAddressData.cnst_addr_line1_addr = !string.IsNullOrEmpty(drMasterAddressData.cnst_addr_line1_addr) ? getValidString(drMasterAddressData.cnst_addr_line1_addr.ToString()) : string.Empty;
                    drMasterAddressData.cnst_addr_line2_addr = !string.IsNullOrEmpty(drMasterAddressData.cnst_addr_line2_addr) ? getValidString(drMasterAddressData.cnst_addr_line2_addr.ToString()) : string.Empty;
                    drMasterAddressData.cnst_addr_city_nm = !string.IsNullOrEmpty(drMasterAddressData.cnst_addr_city_nm) ? getValidString(drMasterAddressData.cnst_addr_city_nm.ToString()) : string.Empty;
                    drMasterAddressData.cnst_addr_state_cd = !string.IsNullOrEmpty(drMasterAddressData.cnst_addr_state_cd) ? getValidString(drMasterAddressData.cnst_addr_state_cd.ToString()) : string.Empty;
                    drMasterAddressData.cnst_addr_zip_5_cd = !string.IsNullOrEmpty(drMasterAddressData.cnst_addr_zip_5_cd) ? getValidString(drMasterAddressData.cnst_addr_zip_5_cd.ToString()) : string.Empty;

                    string strMasterId = drMasterAddressData.cnst_mstr_id.ToString();
                    string strAddressEntry = (!string.IsNullOrEmpty(drMasterAddressData.cnst_addr_line1_addr) ? drMasterAddressData.cnst_addr_line1_addr.ToString() + ", " : string.Empty) +
                                            (!string.IsNullOrEmpty(drMasterAddressData.cnst_addr_line2_addr) ? drMasterAddressData.cnst_addr_line2_addr.ToString() + ", " : string.Empty) +
                                            (!string.IsNullOrEmpty(drMasterAddressData.cnst_addr_city_nm) ? drMasterAddressData.cnst_addr_city_nm.ToString() + ", " : string.Empty) +
                                            (!string.IsNullOrEmpty(drMasterAddressData.cnst_addr_state_cd) ? drMasterAddressData.cnst_addr_state_cd.ToString() + ", " : string.Empty) +
                                            (!string.IsNullOrEmpty(drMasterAddressData.cnst_addr_zip_5_cd) ? drMasterAddressData.cnst_addr_zip_5_cd.ToString() : string.Empty);
                    strAddressEntry = strAddressEntry.TrimEnd(' ');
                    strAddressEntry = strAddressEntry.TrimEnd(',');
                    Dictionary<string, List<string>> dictSourceSystemAndDetails = dictCompareResult[strMasterId]["Address"];
                    if (!dictSourceSystemAndDetails.ContainsKey(drMasterAddressData.arc_srcsys_cd.ToString()))
                    {
                        dictSourceSystemAndDetails.Add(drMasterAddressData.arc_srcsys_cd.ToString(), new List<String>());
                    }
                    dictSourceSystemAndDetails[drMasterAddressData.arc_srcsys_cd.ToString()].Add(strAddressEntry);
                }
            }

            // retrieve and add email entries to the dictionary
            if (resultEmailLst.Count > 0)
            {
                foreach (MasterEmailData drMasterEmailData in resultEmailLst)
                {
                    //remove special characters
                    drMasterEmailData.cnst_email_addr = !string.IsNullOrEmpty(drMasterEmailData.cnst_email_addr) ? getValidString(drMasterEmailData.cnst_email_addr.ToString()) : string.Empty;

                    string strMasterId = drMasterEmailData.cnst_mstr_id.ToString();
                    string strEmailEntry = drMasterEmailData.cnst_email_addr.ToString();
                    Dictionary<string, List<string>> dictSourceSystemAndDetails = dictCompareResult[strMasterId]["Email"];
                    if (!dictSourceSystemAndDetails.ContainsKey(drMasterEmailData.arc_srcsys_cd.ToString()))
                    {
                        dictSourceSystemAndDetails.Add(drMasterEmailData.arc_srcsys_cd.ToString(), new List<String>());
                    }
                    dictSourceSystemAndDetails[drMasterEmailData.arc_srcsys_cd.ToString()].Add(strEmailEntry);
                }
            }

            // retrieve and add phone entries to the dictionary
            if (resultPhoneLst.Count > 0)
            {
                foreach (MasterPhoneData drMasterPhoneData in resultPhoneLst)
                {
                    string strMasterId = drMasterPhoneData.cnst_mstr_id.ToString();
                    string strPhoneEntry = (drMasterPhoneData.cnst_phn_num != null ? drMasterPhoneData.cnst_phn_num.ToString() : string.Empty);
                    Dictionary<string, List<string>> dictSourceSystemAndDetails = dictCompareResult[strMasterId]["Phone"];
                    if (!dictSourceSystemAndDetails.ContainsKey(drMasterPhoneData.arc_srcsys_cd.ToString()))
                    {
                        dictSourceSystemAndDetails.Add(drMasterPhoneData.arc_srcsys_cd.ToString(), new List<String>());
                    }
                    dictSourceSystemAndDetails[drMasterPhoneData.arc_srcsys_cd.ToString()].Add(strPhoneEntry);
                }
            }

            // retrieve and add bridge count details to the dictionary
            Dictionary<string, string> dictBridgeCounts = new Dictionary<string, string>();
            foreach (string s in masterDetailsInsert.MasterId)
            {
                dictBridgeCounts.Add(s, string.Empty);
            }
            dictBridgeCounts = getBridgeCountData(resultBridgeCountLst, dictBridgeCounts);

            foreach (KeyValuePair<string, string> kvPair in dictBridgeCounts)
            {
                string strMasterId = kvPair.Key.ToString();
                string strBridgeString = kvPair.Value.ToString();
                Dictionary<string, List<string>> dictSourceSystemAndDetails = dictCompareResult[strMasterId]["Bridge Presence"];
                if (!dictSourceSystemAndDetails.ContainsKey("bridge_count"))
                {
                    dictSourceSystemAndDetails.Add("bridge_count", new List<String>());
                }
                dictSourceSystemAndDetails["bridge_count"].Add(strBridgeString);
            }

            // retrieve and add birth entries to the dictionary
            if (resultBirthLst.Count > 0 && masterDetailsInsert.ConstituentType == "IN")
            {
                foreach (MasterBirthData drMasterBirthData in resultBirthLst)
                {
                    string strMasterId = drMasterBirthData.cnst_mstr_id.ToString();
                    string strBirthDayEntry = (drMasterBirthData.cnst_birth_dy_num != null ? drMasterBirthData.cnst_birth_dy_num.ToString() : string.Empty);
                    string strBirthMonthEntry = (drMasterBirthData.cnst_birth_mth_num != null ? drMasterBirthData.cnst_birth_mth_num.ToString() : string.Empty);
                    string strBirthYearEntry = (drMasterBirthData.cnst_birth_yr_num != null ? drMasterBirthData.cnst_birth_yr_num.ToString() : string.Empty);
                    string strBirthDate = string.Empty;

                    if (strBirthDayEntry != string.Empty)
                    {
                        int intBirthDayEntry;
                        if (int.TryParse(strBirthDayEntry, out intBirthDayEntry) == true)
                        {
                            if (intBirthDayEntry > 0 && intBirthDayEntry <= 31)
                            {
                                strBirthDate = string.IsNullOrEmpty(strBirthDate) ? strBirthDayEntry : strBirthDate + " " + strBirthDayEntry;
                            }
                        }
                    }
                    if (strBirthMonthEntry != string.Empty)
                    {
                        int intBirthMonthEntry;
                        if (int.TryParse(strBirthMonthEntry, out intBirthMonthEntry) == true)
                        {
                            if (intBirthMonthEntry > 0 && intBirthMonthEntry <= 12)
                            {
                                DateTime dtDate = new DateTime(2000, intBirthMonthEntry, 1);
                                string sMonthFullName = dtDate.ToString("MMMM");
                                strBirthDate = string.IsNullOrEmpty(strBirthDate) ? sMonthFullName : strBirthDate + " " + sMonthFullName;
                            }
                        }
                    }
                    if (strBirthYearEntry != string.Empty)
                    {
                        int intBirthMonthEntry;
                        if (int.TryParse(strBirthYearEntry, out intBirthMonthEntry) == true)
                        {
                            strBirthDate = string.IsNullOrEmpty(strBirthDate) ? strBirthYearEntry : strBirthDate + " " + strBirthYearEntry;
                        }
                    }

                    Dictionary<string, List<string>> dictSourceSystemAndDetails = dictCompareResult[strMasterId]["Birth"];
                    if (!dictSourceSystemAndDetails.ContainsKey(drMasterBirthData.arc_srcsys_cd.ToString()))
                    {
                        dictSourceSystemAndDetails.Add(drMasterBirthData.arc_srcsys_cd.ToString(), new List<String>());
                    }
                    dictSourceSystemAndDetails[drMasterBirthData.arc_srcsys_cd.ToString()].Add(strBirthDate);
                }
            }

            // retrieve and add death entries to the dictionary
            if (resultDeathLst.Count > 0 && masterDetailsInsert.ConstituentType == "IN")
            {
                foreach (MasterDeathData drMasterDeathData in resultDeathLst)
                {
                    string strMasterId = drMasterDeathData.cnst_mstr_id.ToString();
                    string strDeathEntry = (drMasterDeathData.death_date != null ? drMasterDeathData.death_date.ToString() : string.Empty);
                    if (strDeathEntry != string.Empty)
                    {
                        strDeathEntry = DateTime.Parse(strDeathEntry).ToString("MM/dd/yyyy");
                    }
                    Dictionary<string, List<string>> dictSourceSystemAndDetails = dictCompareResult[strMasterId]["Death"];
                    if (!dictSourceSystemAndDetails.ContainsKey(drMasterDeathData.arc_srcsys_cd.ToString()))
                    {
                        dictSourceSystemAndDetails.Add(drMasterDeathData.arc_srcsys_cd.ToString(), new List<String>());
                    }
                    dictSourceSystemAndDetails[drMasterDeathData.arc_srcsys_cd.ToString()].Add(strDeathEntry);
                }
            }

            //Transpose the entries
            CompareOutput newCompareOutput = new CompareOutput();
            newCompareOutput.header = "Master Id";
            newCompareOutput.Detail1 = null;
            newCompareOutput.Detail2 = null;
            newCompareOutput.Detail3 = null;
            newCompareOutput.Detail4 = null;
            newCompareOutput.Detail5 = null;
            newCompareOutput.MasterId1 = null;
            newCompareOutput.MasterId2 = null;
            newCompareOutput.MasterId3 = null;
            newCompareOutput.MasterId4 = null;
            newCompareOutput.MasterId5 = null;
            listCompareOutput.Add(newCompareOutput);

            // setting initial column index to 1
            int intColumnIndex = 1;

            // process and retrieve details
            foreach (KeyValuePair<string, Dictionary<String, Dictionary<String, List<String>>>> kvPair in dictCompareResult)
            {
                int Detailscount = 1;
                Dictionary<string, Dictionary<String, List<String>>> dictCompareValues = kvPair.Value;
                foreach (KeyValuePair<string, Dictionary<String, List<String>>> kvInnerPair in dictCompareValues)
                {
                    // retrieve the label text to be shown against this type
                    Dictionary<String, List<String>> dictSourceSystemCodesAndDetails = kvInnerPair.Value;
                    string stringLiteral = string.Empty;
                    if (dictSourceSystemCodesAndDetails != null)
                    {
                        foreach (KeyValuePair<string, List<String>> kvSourceAndDetail in dictSourceSystemCodesAndDetails)
                        {
                            if (stringLiteral == string.Empty)
                                stringLiteral += kvSourceAndDetail.Key + Environment.NewLine;
                            else
                                stringLiteral += Environment.NewLine + kvSourceAndDetail.Key + Environment.NewLine;
                            foreach (String strDetail in kvSourceAndDetail.Value)
                            {
                                stringLiteral += strDetail + Environment.NewLine;
                            }

                        }
                    }
                    stringLiteral = System.Web.HttpUtility.HtmlDecode(stringLiteral);

                    // retrieve the type
                    string strDetailType = kvInnerPair.Key;

                    // retrieve the type's data row, if it exists, else create a new one
                    CompareOutput compareOutput = new CompareOutput();
                    int intArrayIndex = 0;
                    int SelectedArrayIndex = Detailscount;
                    foreach (CompareOutput drCompareOutput in listCompareOutput)
                    {
                        if (drCompareOutput.header.ToString() == strDetailType)
                        {
                            compareOutput = drCompareOutput;
                            SelectedArrayIndex = intArrayIndex;
                        }
                        intArrayIndex += 1;
                    }
                    if (string.IsNullOrEmpty(compareOutput.header))
                    {
                        compareOutput.header = strDetailType;
                        listCompareOutput.Add(compareOutput);
                    }
                    if (strDetailType == "Dsp Id")
                    {
                        if (dictSourceSystemCodesAndDetails != null)
                        {
                            if (dictSourceSystemCodesAndDetails.ContainsKey("dsp_id"))
                            {
                                populateDetails(intColumnIndex, listCompareOutput, dictSourceSystemCodesAndDetails["dsp_id"][0], SelectedArrayIndex);
                            }
                            else
                            {
                                populateDetails(intColumnIndex, listCompareOutput, string.Empty, SelectedArrayIndex);
                            }
                        }
                    }
                    else if (strDetailType == "Bridge Presence")
                    {
                        if (dictSourceSystemCodesAndDetails != null)
                        {
                            if (dictSourceSystemCodesAndDetails.ContainsKey("bridge_count"))
                            {
                                populateDetails(intColumnIndex, listCompareOutput, dictSourceSystemCodesAndDetails["bridge_count"][0], SelectedArrayIndex);
                            }
                            else
                            {
                                populateDetails(intColumnIndex, listCompareOutput, string.Empty, SelectedArrayIndex);
                            }
                        }
                    }
                    else
                    {
                        populateDetails(intColumnIndex, listCompareOutput, stringLiteral, SelectedArrayIndex);
                    }
                    Detailscount += 1;
                }

                //Populate the master id field
                int intArrayNum = 0;
                foreach (CompareOutput CompareOutput in listCompareOutput)
                {
                    if (intColumnIndex == 1)
                        listCompareOutput[intArrayNum].MasterId1 = kvPair.Key;
                    else if (intColumnIndex == 2)
                        listCompareOutput[intArrayNum].MasterId2 = kvPair.Key;
                    else if (intColumnIndex == 3)
                        listCompareOutput[intArrayNum].MasterId3 = kvPair.Key;
                    else if (intColumnIndex == 4)
                        listCompareOutput[intArrayNum].MasterId4 = kvPair.Key;
                    else if (intColumnIndex == 5)
                        listCompareOutput[intArrayNum].MasterId5 = kvPair.Key;
                    intArrayNum += 1;
                }
                // incrementing column index
                intColumnIndex++;
            }

            return listCompareOutput;
        }

        /* Method to get the submit the merge requests
         * Input Parameters : MergeInput object
         * Output Parameter : Output variables from the Merge SP
         */
        public IList<MergeSPOutput> mergeMasters(MergeInput MergeInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.MergeMasters.getMasterMergeParameters(MergeInput, out strSPQuery, out listParam);
            var MergeLst = rep.ExecuteStoredProcedure<Entities.Constituents.MergeSPOutput>(strSPQuery, listParam).ToList();
            return MergeLst;
        }

        //Method to populate the details columns
        private static void populateDetails(int intColumnIndex, List<CompareOutput> listCompareOutput, string strDetailsValue, int intSelectedIndex)
        {
            //Populate the master id field
            if (intColumnIndex == 1)
                listCompareOutput[intSelectedIndex].Detail1 = strDetailsValue;
            else if (intColumnIndex == 2)
                listCompareOutput[intSelectedIndex].Detail2 = strDetailsValue;
            else if (intColumnIndex == 3)
                listCompareOutput[intSelectedIndex].Detail3 = strDetailsValue;
            else if (intColumnIndex == 4)
                listCompareOutput[intSelectedIndex].Detail4 = strDetailsValue;
            else if (intColumnIndex == 5)
                listCompareOutput[intSelectedIndex].Detail5 = strDetailsValue;
        }

        //Generic method to remove the special characters
        private static String getValidString(string strProcessString)
        {
            strProcessString = string.IsNullOrEmpty(strProcessString) ? string.Empty : strProcessString;
            var validXmlChars = strProcessString.Where(ch => System.Xml.XmlConvert.IsXmlChar(ch)).ToArray();
            return new string(validXmlChars);
        }

        //method to retrieve the bridge and their counts for the master ids
        private Dictionary<string, string> getBridgeCountData(List<MasterBridgeData> resultBridgeCountLst, Dictionary<string, string> dictSourceSystemCounts)
        {
            // update the dictionary of counts with the result set
            foreach (MasterBridgeData drMasterBridgeData in resultBridgeCountLst)
            {
                string strMasterIdResultCount = drMasterBridgeData.cnst_mstr_id.ToString();
                string strMasterIdCount = drMasterBridgeData.arc_srcsys_cd.ToString() + " (" + drMasterBridgeData.counter.ToString() + ")";
                dictSourceSystemCounts[strMasterIdResultCount] += (string.IsNullOrEmpty(dictSourceSystemCounts[strMasterIdResultCount]) ?
                                                                    strMasterIdCount : ", " + strMasterIdCount);
            }
            return dictSourceSystemCounts;
        }

        /* Method to get the submit the merge requests
       * Input Parameters : MergeInput object
       * Output Parameter : Output variables from the Merge SP
       */
        public IList<MergeSPOutput> submitMergeConflicts(MergeConflictInput MergeConflictInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.MergeMasters.getMergeConflictsParameters(MergeConflictInput, out strSPQuery, out listParam);
            var MergeLst = rep.ExecuteStoredProcedure<Entities.Constituents.MergeSPOutput>(strSPQuery, listParam).ToList();
            return MergeLst;
        }
    }
}
