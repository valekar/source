using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Upload
{
    public class MsgPrefUploadDetails
    {
        //msg Pref upload 
        public async Task<IList<string>> validateMasterIdDetails(MsgPrefValidationInput dncvi)
        {
            Repository rep = new Repository();
            var _inputMasterIds = (from item in dncvi._masterIds
                                   where !string.IsNullOrEmpty(item)
                                   select item).ToList();
            var _masterIds = await rep.ExecuteSqlQueryAsync<string>(SQL.Upload.DncUploadValidationSQL.getMasterIdValidationSQL(_inputMasterIds));
            return _masterIds;

        }


        public async Task<IList<string>> validateSourceSystemIds(MsgPrefValidationInput _input)
        {
            Repository rep = new Repository();


            var _inputSourceSystemIDs = (from item in _input._sourceSystemId
                                         where !string.IsNullOrEmpty(item)
                                         select item).ToList();

            var _validSources = await rep.ExecuteSqlQueryAsync<string>
                (SQL.Upload.DncUploadValidationSQL.getSourceSystemIdSQLValidation(_inputSourceSystemIDs));
            return _validSources;
        }

        public async Task<IList<string>> validateSourceSystemCodes(MsgPrefValidationInput _input)
        {
            Repository rep = new Repository();
            var _inputSourceSystemCDs = (from item in _input._sourceSystemCode
                                         where !string.IsNullOrEmpty(item)
                                         select item).ToList();



            var _validSources = await rep.ExecuteSqlQueryAsync<string>
                (SQL.Upload.DncUploadValidationSQL.getSourceSystemCodeSQLValidation(_inputSourceSystemCDs));
            return _validSources;
        }


        public IList<MsgPrefRefCode> getMsgPrefRefCodes()
        {
            Repository rep = new Repository();
            return rep.ExecuteSqlQuery<MsgPrefRefCode>(SQL.Upload.MsgPrefUploadSQLs.getMsgPrefRefCode()).ToList();
        }


        public long getMsgPrefUploadTransKeyDetails(string userId)
        {
            Repository rep = new Repository();
            long strTransactionKey = -1;

            try
            {
                CrudOperationOutput crudOutput;
                crudOutput = SQL.Upload.MsgPrefUploadSQLs.msgPrefUploadCreateTrans(userId);
                var dncUploadCreateTransOutput = rep.ExecuteStoredProcedure<UploadCreateTransOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
                strTransactionKey = dncUploadCreateTransOutput[0] != null ? dncUploadCreateTransOutput[0].transKey : strTransactionKey;
                return strTransactionKey;
            }
            catch (Exception e)
            {
                return strTransactionKey;
            }
        }



        public async Task insertMsgPrefUploadDetails(MsgPrefUploadParams gm, string username, long trans_key, long max_seq_key)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
            crudOutput = SQL.Upload.MsgPrefUploadSQLs.insertMsgPrefUploadRecords(gm, username, trans_key, max_seq_key);

            await rep.ExecuteStoredProcedureAsync(crudOutput.strSPQuery, crudOutput.parameters);
        }


        public long getMsgPrefSeqKeyDetails()
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<long> listSeqKey = new List<long>();
            long strMaxSeqKey = -1;

            try
            {
                listSeqKey = rep.ExecuteSqlQuery<Int64>(SQL.Upload.MsgPrefUploadSQLs.getMaxSeqKeySQL()).ToList();
                strMaxSeqKey = listSeqKey[0] != null ? listSeqKey[0] : strMaxSeqKey;
                return strMaxSeqKey;
            }
            catch (Exception e)
            {
                return strMaxSeqKey;
            }
        }

    }
}
