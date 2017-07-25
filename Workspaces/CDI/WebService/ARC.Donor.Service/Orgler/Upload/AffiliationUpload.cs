using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ARC.Donor.Service.Orgler.Upload
{
    public class AffiliationUpload
    {
        public async Task<Business.Orgler.Upload.AffiliationUploadValidationOutput> validateAffiliationUpload(Business.Orgler.Upload.AffiliationUploadValidationInput validateInput)
        {
            Business.Orgler.Upload.AffiliationUploadValidationOutput output = new Business.Orgler.Upload.AffiliationUploadValidationOutput();
            output.strEnterpriseOrgIdValid = new List<string>();
            output.strMasterIdInputValid = new List<string>();

            Data.Orgler.Upload.UploadValidation dataLayer = new Data.Orgler.Upload.UploadValidation();
            //Validate Ent org ids
            if (validateInput.strEnterpriseOrgIdInput.Count > 0)
            {
                var entRes = await dataLayer.getValidEnterpriseIds(validateInput.strEnterpriseOrgIdInput);
                output.strEnterpriseOrgIdValid = (List<string>)entRes;
            }

            //Validate Master ids
            if (validateInput.strMasterIdInput.Count > 0)
            {
                var masRes = await dataLayer.getValidMasterIds(validateInput.strMasterIdInput);
                output.strMasterIdInputValid = (List<string>)masRes;
            }

            return output;
        }

        public async Task<Business.Orgler.Upload.TransOutput> insertAffiliation(List<Business.Orgler.Upload.AffiliationUploadInput> input, string strUserName)
        {
            //Create the transaction
            Data.Entities.Orgler.Upload.TransInput transInput = new Data.Entities.Orgler.Upload.TransInput();
            transInput.typ = "eo_affil_upl";
            transInput.subTyp = "eo_affil_upl";
            transInput.transStat= "In Progress";
            transInput.transNotes = "";
            transInput.actionType = string.Empty;
            transInput.caseSeqNum = "";
            transInput.userId = strUserName;

            //Call data layer to create transaction
            Data.Orgler.Upload.Transaction tService = new Data.Orgler.Upload.Transaction();
            IList<Data.Entities.Orgler.Upload.TransOutput> tOut = tService.insertTransaction(transInput);

            string strTransStatus = tOut.Select(x => x.transOutput).Distinct().First().ToString();
            string strTransKey = tOut.Select(x => x.transKey).Distinct().First().ToString();

            Business.Orgler.Upload.TransOutput transOut = new Business.Orgler.Upload.TransOutput();
            
            if (strTransStatus.ToLower().Contains("success"))
            {
                transOut.transKey = Convert.ToInt64(strTransKey);
                transOut.transOutput = "success";

                //Instantiate the data layer object for affiliations functionality
                Data.Orgler.Upload.AffiliationUpload data = new Data.Orgler.Upload.AffiliationUpload();
                Data.Orgler.Upload.TransactionEntOrg dataTransEnt = new Data.Orgler.Upload.TransactionEntOrg();

                if (input != null)
                {
                    if (input.Count() > 0)
                    {
                        Dictionary<string, Dictionary<string, int>> dictTransEntOrg = new Dictionary<string, Dictionary<string, int>>();

                        //Insert the upload stage table records
                        int stageKey = 0;
                        Task<IList<Data.Entities.Orgler.Upload.AffiliationUploadOutput>>[] taskList = new Task<IList<Data.Entities.Orgler.Upload.AffiliationUploadOutput>>[input.Count()];
                        foreach (Business.Orgler.Upload.AffiliationUploadInput flexRecord in input)
                        {
                            Data.Entities.Orgler.Upload.AffiliationUploadInput flexInput = new Data.Entities.Orgler.Upload.AffiliationUploadInput();
                            flexInput.strEnterpriseOrgId = flexRecord.strEnterpriseOrgId;
                            flexInput.strMasterId = flexRecord.strMasterId;
                            flexInput.strStatus = flexRecord.strStatus;
                            flexInput.strTransKey = strTransKey;

                            //Frame the input for the transaction ent org insertion
                            if (dictTransEntOrg != null)
                            {
                                if(dictTransEntOrg.ContainsKey(flexRecord.strEnterpriseOrgId))
                                {
                                    Dictionary<string, int> tempDict = new Dictionary<string, int>();
                                    tempDict = dictTransEntOrg[flexRecord.strEnterpriseOrgId];
                                    if(tempDict.ContainsKey(flexRecord.strStatus))
                                    {
                                        tempDict[flexRecord.strStatus] += 1;
                                    }
                                    else
                                    {
                                        tempDict.Add(flexRecord.strStatus, 1);
                                    }
                                    dictTransEntOrg[flexRecord.strEnterpriseOrgId] = tempDict;
                                }
                                else
                                {
                                    Dictionary<string, int> tempDict = new Dictionary<string, int>();
                                    tempDict.Add(flexRecord.strStatus, 1);
                                    dictTransEntOrg.Add(flexRecord.strEnterpriseOrgId, tempDict);
                                }
                            }

                            taskList[stageKey] = Task.Run(() => data.insertAffiliation(flexInput));

                            stageKey = stageKey + 1;
                        }

                        Task.WaitAll(taskList);

                        foreach (Task t in taskList)
                        {
                            var result = ((Task<IList<Data.Entities.Orgler.Upload.AffiliationUploadOutput>>)t).Result;
                            if (result[0].o_message.ToLower() != "success")
                            {
                                transOut.transOutput = "stage insert failed";
                            }
                        }

                        //insert the trans ent org record
                        if(dictTransEntOrg != null)
                        {
                            stageKey = 0;
                            Task<IList<ARC.Donor.Data.Entities.Orgler.Upload.TransactionEntOrgOutput>>[] taskTransEntOrg = new Task<IList<Data.Entities.Orgler.Upload.TransactionEntOrgOutput>>[dictTransEntOrg.Count];
                            foreach(KeyValuePair<string, Dictionary<string, int>> indvOuterDict in dictTransEntOrg)
                            {
                                Dictionary<string, int> innerDict = new Dictionary<string, int>();
                                innerDict = indvOuterDict.Value;

                                string strNotes = string.Empty;
                                if(innerDict.ContainsKey("Active"))
                                {
                                    strNotes += "New Affiliations(" + innerDict["Active"].ToString() + ")" ;
                                }
                                if (innerDict.ContainsKey("Inactive"))
                                {
                                    strNotes = string.IsNullOrEmpty(strNotes) ? "Inactivated Affiliations(" + innerDict["Inactive"].ToString() + ")"
                                                                    : strNotes + ", Inactivated Affiliations(" + innerDict["Inactive"].ToString() + ")";
                                }
                                ARC.Donor.Data.Entities.Orgler.Upload.TransactionEntOrgInput TransEntOrgInput = new Data.Entities.Orgler.Upload.TransactionEntOrgInput();
                                TransEntOrgInput.transKey = strTransKey;
                                TransEntOrgInput.entOrgId = indvOuterDict.Key;
                                TransEntOrgInput.applSrcCd = "ORGL";
                                TransEntOrgInput.transNotes = strNotes;

                                taskTransEntOrg[stageKey] = Task.Run(() => dataTransEnt.insertTransactionEntOrg(TransEntOrgInput));
                                stageKey += 1;
                            }

                            Task.WaitAll(taskTransEntOrg);

                            foreach(Task t in taskTransEntOrg)
                            {
                                var result = ((Task<IList<ARC.Donor.Data.Entities.Orgler.Upload.TransactionEntOrgOutput>>)t).Result;
                                if(result[0].o_trans_out.ToLower() != "success")
                                {
                                    transOut.transOutput = "transaction enterprise org insert failed";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                transOut.transOutput = "transaction failed";
            }
            return transOut;
        }
    }
}
