using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ARC.Donor.Data.Upload
{
    public class EmailOnlyUploadDetailsData
    {
        public async Task<IList<ChapterCodeValidationOutput>> validateChapterCodeDetails(EmailOnlyUploadValidationInput emvi)
        {
            Repository rep = new Repository();
           /* if (emvi._chapterCodes != null || emvi._chapterCodes.Count != 0)
            {
                if (!emvi._chapterCodes.All(x => x.Equals("")))
                {*/
                    var _validChapterCodes = await rep.ExecuteSqlQueryAsync<ChapterCodeValidationOutput>(SQL.Upload.UploadValidation.getChapterCodeValidationSQL(emvi._chapterCodes));
                    return _validChapterCodes;
                /*}
                else
                    return new List<ChapterCodeValidationOutput>();
            }
            else
            {//If the incoming list of chapter codes is null or they do not contain any data 
                return new List<ChapterCodeValidationOutput>();
            }*/
        }

        public async Task<IList<GroupCodeValidationOutput>> validateGroupCodeDetails(EmailOnlyUploadValidationInput emvi)
        {
            Repository rep = new Repository();
            /*if (emvi._groupCodes != null || emvi._groupCodes.Count != 0)
            {
                if (!emvi._groupCodes.All(x => x.Equals("")))
                {*/
                    var _validGroupCodes = await rep.ExecuteSqlQueryAsync<GroupCodeValidationOutput>(SQL.Upload.UploadValidation.getGroupCodeValidationSQL(emvi._groupCodes));
                    return _validGroupCodes;
                /*}
                else
                    return new List<GroupCodeValidationOutput>();
            }
            else
            {
                return new List<GroupCodeValidationOutput>();
            }*/
        }

        public long getEmailOnlyUploadTransKeyDetails(string userId)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            long strTransactionKey = -1;
           
            try
            {
                CrudOperationOutput crudOutput = new CrudOperationOutput();
                crudOutput = SQL.Upload.EmailOnlyUploadSQLDetails.postEmailUploadCreateTrans(userId);
               // SQL.Upload.EmailOnlyUploadSQLDetails.postEmailUploadCreateTrans(userId, out strSPQuery, out listParam);
                var emailOnlyUploadCreateTransOutput = rep.ExecuteStoredProcedure<UploadCreateTransOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
                //rep.ExecuteStoredProcedure<UploadCreateTransOutput>(strSPQuery, listParam).ToList();
                strTransactionKey = emailOnlyUploadCreateTransOutput[0] != null ? emailOnlyUploadCreateTransOutput[0].transKey : strTransactionKey;
                return strTransactionKey;
            }
            catch (Exception e)
            {
                return strTransactionKey;
            }
        }

        public long getEmailOnlyUploadSeqKeyDetails()
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<long> listSeqKey = new List<long>();
            long strMaxSeqKey = -1;

            try
            {
                listSeqKey = rep.ExecuteSqlQuery<Int64>(SQL.Upload.UploadDetails.getMaxSeqKeySQL()).ToList();
                strMaxSeqKey = listSeqKey[0] != null ? listSeqKey[0] : strMaxSeqKey;
                return strMaxSeqKey;
            }
            catch (Exception e)
            {
                return strMaxSeqKey;
            }
        }

        public long getEmailOnlyUploadGrpSeqKeyDetails()
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<long> listGrpSeqKey = new List<long>();
            long strMaxGrpSeqKey = -1;

            try
            {
                listGrpSeqKey = rep.ExecuteSqlQuery<Int64>(SQL.Upload.UploadDetails.getMaxGroupSeqKeySQL()).ToList();
                strMaxGrpSeqKey = listGrpSeqKey[0] != null ? listGrpSeqKey[0] : strMaxGrpSeqKey;
                return strMaxGrpSeqKey;
            }
            catch (Exception e)
            {
                return strMaxGrpSeqKey;
            }
        }


        public List<ComUnitKeyOutput> getEmailOnlyUploadUnitKeyDetails(EmailOnlyUploadDetails gm)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<ComUnitKeyOutput> listUnitKey = new List<ComUnitKeyOutput>();
          /*  ComUnitKeyOutput strUnitKey = new ComUnitKeyOutput()
            {
                nk_ecode = string.Empty,
                unit_key = -1
            };*/

            try
            {
                //if (gm != null || gm.ListEmailOnlyUploadDetails.EmailOnlyUploadInputList.Count !=0)
                //{
                    listUnitKey = rep.ExecuteSqlQuery<ComUnitKeyOutput>(SQL.Upload.EmailOnlyUploadSQLDetails.getComUnitKeySQL(gm)).ToList();
                    return listUnitKey;
               /* }
                else
                {
                    return new List<ComUnitKeyOutput>()
                    {
                        new ComUnitKeyOutput() {
                            nk_ecode = string.Empty,
                            unit_key = -1                  
                        }
                    };
                }*/
            }
            catch (Exception e)
            {
                return listUnitKey;
            }

        }

        public async Task insertEmailOnlyUploadDetails(EmailOnlyUploadInput gm, List<ChapterUploadFileDetailsHelper> ch, long max_seq_key, long max_grp_seq_key, List<ComUnitKeyOutput> com_unit_key_lst, long trans_key)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();

            CrudOperationOutput crudOutput = new CrudOperationOutput();
            crudOutput = SQL.Upload.EmailOnlyUploadSQLDetails.insertEmailOnlyUploadRecords(gm, ch, ++max_seq_key, ++max_grp_seq_key, 
                com_unit_key_lst, trans_key);

            //SQL.Upload.EmailOnlyUploadSQLDetails.insertEmailOnlyUploadRecords(gm, ch, ++max_seq_key, ++max_grp_seq_key, 
              //  com_unit_key_lst, trans_key, out strSPQuery, out listParam);
            await rep.ExecuteStoredProcedureAsync(crudOutput.strSPQuery, crudOutput.parameters);
        }

    }
}
