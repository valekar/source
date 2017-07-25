using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Threading;

namespace ARC.Donor.Service.Orgler.Upload
{
    public class EosiUpload
    {
        public async Task<Business.Orgler.Upload.EosiUploadValidationOutput> validateEosiUpload(Business.Orgler.Upload.EosiUploadValidationInput validateInput)
        {
            Business.Orgler.Upload.EosiUploadValidationOutput output = new Business.Orgler.Upload.EosiUploadValidationOutput();
            output.strEnterpriseOrgIdValid = new List<string>();
            output.strMasterIdValid = new List<string>();
            output.strNaicsCodeValid = new List<string>();
            output.strCharacteristicTypeValid = new List<string>();

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
                output.strMasterIdValid = (List<string>)masRes;
            }

            //Validate Naics codes
            if (validateInput.strNaicsCodeInput.Count > 0)
            {
                var naicsRes = await dataLayer.getValidNaicsCode(validateInput.strNaicsCodeInput);
                output.strNaicsCodeValid = (List<string>)naicsRes;
            }

            //Validate Characteristic Types
            if (validateInput.strCharacteristicTypeInput.Count > 0)
            {
                var charRes = await dataLayer.getValidCharacteristicType(validateInput.strCharacteristicTypeInput);
                output.strCharacteristicTypeValid = (List<string>)charRes;
            }
            return output;
        }

        public async Task<Business.Orgler.Upload.TransOutput> insertEosi(List<Business.Orgler.Upload.EosiUploadInput> input, string strUserName)
        {
            //Create the transaction
            Data.Entities.Orgler.Upload.TransInput transInput = new Data.Entities.Orgler.Upload.TransInput();
            transInput.typ = "eo_eosi_upl";
            transInput.subTyp = "eo_eosi_upl";
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
                //Instantiate the data layer object for Eosis functionality
                Data.Orgler.Upload.EosiUpload data = new Data.Orgler.Upload.EosiUpload();

                if (input != null)
                {
                    if (input.Count() > 0)
                    {
                        int stageKey = 0;
                        Int64 seqKey = 0;
                        seqKey = data.getMaxSeqKey();

                        Task<IList<Data.Entities.Orgler.Upload.EosiUploadOutput>>[] taskList = new Task<IList<Data.Entities.Orgler.Upload.EosiUploadOutput>>[input.Count()];
                        foreach (Business.Orgler.Upload.EosiUploadInput flexRecord in input)
                        {
                            var incr_max_key = Interlocked.Increment(ref seqKey);
                            Data.Entities.Orgler.Upload.EosiUploadInput flexInput = new Data.Entities.Orgler.Upload.EosiUploadInput();
                            flexInput.strEnterpriseOrgId = flexRecord.strEnterpriseOrgId;
                            flexInput.strMasterId = flexRecord.strMasterId;
                            flexInput.strSourceSystemCode = flexRecord.strSourceSystemCode.ToUpper();
                            flexInput.strSourceId = (!string.IsNullOrEmpty(flexRecord.strSourceId) ? flexRecord.strSourceId : ((!string.IsNullOrEmpty(flexRecord.strSourceSystemCode)) ? flexRecord.strSourceSystemCode.ToUpper() + incr_max_key.ToString() : string.Empty));
                            flexInput.strSecondarySourceId = flexRecord.strSecondarySourceId;
                            flexInput.strParentEnterpriseOrgId = flexRecord.strParentEnterpriseOrgId;
                            flexInput.strAltSourceCode = flexRecord.strAltSourceCode;
                            flexInput.strAltSourceId = flexRecord.strAltSourceId;
                            flexInput.strOrgName = flexRecord.strOrgName;
                            flexInput.strAddress1Street1 = flexRecord.strAddress1Street1;
                            flexInput.strAddress1Street2 = flexRecord.strAddress1Street2;
                            flexInput.strAddress1City = flexRecord.strAddress1City;
                            flexInput.strAddress1State = flexRecord.strAddress1State;
                            flexInput.strAddress1Zip = flexRecord.strAddress1Zip;
                            flexInput.strPhone1 = flexRecord.strPhone1;
                            flexInput.strPhone2 = flexRecord.strPhone2;
                            flexInput.strNaicsCode = flexRecord.strNaicsCode;
                            flexInput.strCharacteristics1Code = flexRecord.strCharacteristics1Code;
                            flexInput.strCharacteristics1Value = flexRecord.strCharacteristics1Value;
                            flexInput.strCharacteristics2Code = flexRecord.strCharacteristics2Code;
                            flexInput.strCharacteristics2Value = flexRecord.strCharacteristics2Value;
                            flexInput.strRMIndicator = flexRecord.strRMIndicator;
                            flexInput.strNotes = flexRecord.strNotes;
                            flexInput.strTransKey = strTransKey;
                            flexInput.strSeqKey = incr_max_key;
                            taskList[stageKey] = Task.Run(() => data.insertEosi(flexInput, strUserName));

                            stageKey = stageKey + 1;
                        }

                        Task.WaitAll(taskList);

                        foreach (Task t in taskList)
                        {
                            var result = ((Task<IList<Data.Entities.Orgler.Upload.EosiUploadOutput>>)t).Result;
                            if (result[0].o_message.ToLower() != "success")
                            {
                                transOut.transOutput = "stage insert failed";
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
