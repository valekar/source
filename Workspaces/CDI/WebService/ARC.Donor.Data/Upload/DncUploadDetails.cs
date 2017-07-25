using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Upload
{
    public class DncUploadDetails
    {

        //dnc upload 
        public async Task<IList<string>> validateMasterIdDetails(DncValidationInput dncvi)
        {
            Repository rep = new Repository();
            var _inputMasterIds = (from item in dncvi._masterIds
                                   where !string.IsNullOrEmpty(item)
                                   select item).ToList();
            var _masterIds = await rep.ExecuteSqlQueryAsync<string>(SQL.Upload.DncUploadValidationSQL.getMasterIdValidationSQL(_inputMasterIds));
            return _masterIds;

        }


        public async Task<IList<string>> validateSourceSystemIds(DncValidationInput _input)
        {
            Repository rep = new Repository();


            var _inputSourceSystemIDs = (from item in _input._sourceSystemId
                                         where !string.IsNullOrEmpty(item)
                                         select item).ToList();

            var _validSources = await rep.ExecuteSqlQueryAsync<string>
                (SQL.Upload.DncUploadValidationSQL.getSourceSystemIdSQLValidation(_inputSourceSystemIDs));
            return _validSources;
        }

        public async Task<IList<string>> validateSourceSystemCodes(DncValidationInput _input)
        {
            Repository rep = new Repository();
            var _inputSourceSystemCDs = (from item in _input._sourceSystemCode
                                         where !string.IsNullOrEmpty(item)
                                         select item).ToList();



            var _validSources = await rep.ExecuteSqlQueryAsync<string>
                (SQL.Upload.DncUploadValidationSQL.getSourceSystemCodeSQLValidation(_inputSourceSystemCDs));
            return _validSources;
        }


        public async Task<IList<string>> validateCommChannels(DncValidationInput _input)
        {
            Repository rep = new Repository();
            var _inputCommChannels = (from item in _input._commChannels
                                      where !string.IsNullOrEmpty(item) 
                                      select item).ToList();
            List<string> commChannels = new List<string>();
            //await rep.ExecuteSqlQueryAsync<string>
              //      (SQL.Upload.DncValidation.getCommChannelSQLValidation());
          //  commChannels.Add("All");
           // commChannels.Add("Email");
            // commChannels.Add("Phone");
            // commChannels.Add("Mail");
           // commChannels.Add("Text"); 

           // var validCommChannels =  (from item in _inputCommChannels
             //                where commChannels.Contains(item)
               //             select item).ToList();
            var validCommChannels = await rep.ExecuteSqlQueryAsync<string>
                   (SQL.Upload.DncUploadValidationSQL.getCommChannelSQLValidation());

            

            return validCommChannels;
        }


        public async Task<IList<string>> validateLineOfService(DncValidationInput _input)
        {
            Repository rep = new Repository();
            var _inputLineOfService = (from item in _input._lineOfServiceCodes
                                      where !string.IsNullOrEmpty(item)
                                      select item).ToList();
            List<string> lineOfSericeList = new List<string>();
                //await rep.ExecuteSqlQueryAsync<string>
                    //(SQL.Upload.DncValidation.getLineOfServiceSQLValidation());
            //lineOfSericeList.Add("All");
           // lineOfSericeList.Add("FR");
           // lineOfSericeList.Add("PHSS");
           // lineOfSericeList.Add("Bio");

           // var validLineOfServices = (from item in _inputLineOfService
               //                      where lineOfSericeList.Contains(item)
                //                     select item).ToList();
            var validLineOfServices  = await rep.ExecuteSqlQueryAsync<string>
                    (SQL.Upload.DncUploadValidationSQL.getLineOfServiceSQLValidation());
            return validLineOfServices;
        }


        public long getDncUploadTransKeyDetails(string userId)
        {
            Repository rep = new Repository();
            long strTransactionKey = -1;

            try
            {
                CrudOperationOutput crudOutput;
                crudOutput = SQL.Upload.DncUploadSQL.dncUploadCreateTrans(userId);
                var dncUploadCreateTransOutput = rep.ExecuteStoredProcedure<UploadCreateTransOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
                strTransactionKey = dncUploadCreateTransOutput[0] != null ? dncUploadCreateTransOutput[0].transKey : strTransactionKey;
                return strTransactionKey;
            }
            catch (Exception e)
            {
                return strTransactionKey;
            }
        }


        public long getDncSeqKeyDetails()
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<long> listSeqKey = new List<long>();
            long strMaxSeqKey = -1;

            try
            {
                listSeqKey = rep.ExecuteSqlQuery<Int64>(SQL.Upload.DncUploadSQL.getMaxSeqKeySQL()).ToList();
                strMaxSeqKey = listSeqKey[0] != null ? listSeqKey[0] : strMaxSeqKey;
                return strMaxSeqKey;
            }
            catch (Exception e)
            {
                return strMaxSeqKey;
            }
        }

        public async Task insertDncUploadDetails(DncUploadParams gm, string username, long trans_key,long max_seq_key)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
            crudOutput = SQL.Upload.DncUploadSQL.insertDncUploadRecords(gm, username, trans_key,max_seq_key);
            
            await rep.ExecuteStoredProcedureAsync(crudOutput.strSPQuery, crudOutput.parameters);
        }


    }
}
