using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Threading;

namespace ARC.Donor.Service.Orgler.Upload
{
    public class EoUpload
    {
        public async Task<Business.Orgler.Upload.EoUploadValidationOutput> validateEoUpload(Business.Orgler.Upload.EoUploadValidationInput validateInput)
        {
            Business.Orgler.Upload.EoUploadValidationOutput output = new Business.Orgler.Upload.EoUploadValidationOutput();
            output.strEnterpriseOrgIdValid = new List<string>();
            output.strEnterpriseOrgNameValid = new List<string>();
            output.strCharacteristicTypeValid = new List<string>();
            output.strTagValid = new List<string>();
            
            Data.Orgler.Upload.UploadValidation dataLayer = new Data.Orgler.Upload.UploadValidation();
            //Validate Ent org ids
            if (validateInput.strEnterpriseOrgIdInput.Count > 0)
            {
                var entRes = await dataLayer.getValidEnterpriseIds(validateInput.strEnterpriseOrgIdInput);
                output.strEnterpriseOrgIdValid = (List<string>)entRes;
            }

            //Validate Ent org name
            if (validateInput.strEnterpriseOrgNameInput.Count > 0)
            {
                var entRes = await dataLayer.getValidEnterpriseNames(validateInput.strEnterpriseOrgNameInput);
                output.strEnterpriseOrgNameValid = (List<string>)entRes;
            }

            //Validate Characteristic Types
            if (validateInput.strCharacteristicTypeInput.Count > 0)
            {
                var charRes = await dataLayer.getValidCharacteristicType(validateInput.strCharacteristicTypeInput);
                output.strCharacteristicTypeValid = (List<string>)charRes;
            }

            //Validate Tags
            if (validateInput.strTagInput.Count > 0)
            {
                var tagRes = await dataLayer.getValidTags(validateInput.strTagInput);
                output.strTagValid = (List<string>)tagRes;
            }

            return output;
        }

        public async Task<Business.Orgler.Upload.TransOutput> insertEo(List<Business.Orgler.Upload.EoUploadInput> input, string strUserName)
        {
            //Create the transaction
            Data.Entities.Orgler.Upload.TransInput transInput = new Data.Entities.Orgler.Upload.TransInput();
            transInput.typ = "eo_eo_upl";
            transInput.subTyp = "eo_eo_upl";
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
                //Instantiate the data layer object for Eos functionality
                Data.Orgler.Upload.EoUpload data = new Data.Orgler.Upload.EoUpload();

                if (input != null)
                {
                    if (input.Count() > 0)
                    {
                        int stageKey = 0;
                        Int64 seqKey = 0;
                        seqKey = data.getMaxSeqKey();

                        Task<IList<Data.Entities.Orgler.Upload.EoUploadOutput>>[] taskList = new Task<IList<Data.Entities.Orgler.Upload.EoUploadOutput>>[input.Count()];
                        foreach (Business.Orgler.Upload.EoUploadInput flexRecord in input)
                        {
                            var incr_max_key = Interlocked.Increment(ref seqKey);
                            Data.Entities.Orgler.Upload.EoUploadInput flexInput = new Data.Entities.Orgler.Upload.EoUploadInput();
                            flexInput.strEnterpriseOrgId = flexRecord.strEnterpriseOrgId;
                            flexInput.strEnterpriseOrgName = flexRecord.strEnterpriseOrgName;
                            flexInput.strCharacteristics1Code = flexRecord.strCharacteristics1Code;
                            flexInput.strCharacteristics1Value = flexRecord.strCharacteristics1Value;
                            flexInput.strCharacteristics2Code = flexRecord.strCharacteristics2Code;
                            flexInput.strCharacteristics2Value = flexRecord.strCharacteristics2Value;
                            flexInput.strCharacteristics3Code = flexRecord.strCharacteristics3Code;
                            flexInput.strCharacteristics3Value = flexRecord.strCharacteristics3Value;
                            flexInput.strTransformCondition1Type1 = flexRecord.strTransformCondition1Type1;
                            flexInput.strTransformCondition1String1 = flexRecord.strTransformCondition1String1;
                            flexInput.strTransformCondition1Type2 = flexRecord.strTransformCondition1Type2;
                            flexInput.strTransformCondition1String2 = flexRecord.strTransformCondition1String2;
                            flexInput.strTransformCondition1Type3 = flexRecord.strTransformCondition1Type3;
                            flexInput.strTransformCondition1String3 = flexRecord.strTransformCondition1String3;
                            flexInput.strTransformCondition2Type1 = flexRecord.strTransformCondition2Type1;
                            flexInput.strTransformCondition2String1 = flexRecord.strTransformCondition2String1;
                            flexInput.strTransformCondition2Type2 = flexRecord.strTransformCondition2Type2;
                            flexInput.strTransformCondition2String2 = flexRecord.strTransformCondition2String2;
                            flexInput.strTransformCondition2Type3 = flexRecord.strTransformCondition2Type3;
                            flexInput.strTransformCondition2String3 = flexRecord.strTransformCondition2String3;
                            flexInput.strTransformCondition3Type1 = flexRecord.strTransformCondition3Type1;
                            flexInput.strTransformCondition3String1 = flexRecord.strTransformCondition3String1;
                            flexInput.strTransformCondition3Type2 = flexRecord.strTransformCondition3Type2;
                            flexInput.strTransformCondition3String2 = flexRecord.strTransformCondition3String2;
                            flexInput.strTransformCondition3Type3 = flexRecord.strTransformCondition3Type3;
                            flexInput.strTransformCondition3String3 = flexRecord.strTransformCondition3String3;
                            flexInput.strTag1 = flexRecord.strTag1;
                            flexInput.strTag2 = flexRecord.strTag2;
                            flexInput.strTag3 = flexRecord.strTag3;
                            flexInput.strAction = flexRecord.strAction;
                            flexInput.strTransKey = strTransKey;
                            flexInput.strSeqKey = incr_max_key;
                            taskList[stageKey] = Task.Run(() => data.insertEo(flexInput, strUserName));

                            stageKey = stageKey + 1;
                        }

                        Task.WaitAll(taskList);

                        foreach (Task t in taskList)
                        {
                            var result = ((Task<IList<Data.Entities.Orgler.Upload.EoUploadOutput>>)t).Result;
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
