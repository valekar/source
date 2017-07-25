﻿using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Upload
{
    public class NameAndEmailUploadDetailsData
    {
        public async Task<IList<ChapterCodeValidationOutput>> validateChapterCodeDetails(NameAndEmailUploadValidationInput emvi)
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

        public async Task<IList<GroupCodeValidationOutput>> validateGroupCodeDetails(NameAndEmailUploadValidationInput emvi)
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

        public long getNameAndEmailUploadTransKeyDetails(string userId)
        {
            Repository rep = new Repository();
            long strTransactionKey = -1;

            try
            {
                CrudOperationOutput crudOutput ;
                crudOutput = SQL.Upload.NameAndEmailUploadSQLDetails.postNameAndEmailUploadCreateTrans(userId);
                //SQL.Upload.EmailOnlyUploadSQLDetails.postEmailUploadCreateTrans(userId);
                //var emailOnlyUploadCreateTransOutput = rep.ExecuteStoredProcedure<UploadCreateTransOutput>(strSPQuery, listParam).ToList();
                var emailOnlyUploadCreateTransOutput = rep.ExecuteStoredProcedure<UploadCreateTransOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
                strTransactionKey = emailOnlyUploadCreateTransOutput[0] != null ? emailOnlyUploadCreateTransOutput[0].transKey : strTransactionKey;
                return strTransactionKey;
            }
            catch (Exception e)
            {
                return strTransactionKey;
            }
        }

        public long getNameAndEmailUploadSeqKeyDetails()
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

        public long getNameAndEmailUploadGrpSeqKeyDetails()
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


        public List<ComUnitKeyOutput> getNameAndEmailUploadUnitKeyDetails(NameAndEmailUploadDetails gm)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<ComUnitKeyOutput> listUnitKey = new List<ComUnitKeyOutput>();
           /* ComUnitKeyOutput strUnitKey = new ComUnitKeyOutput()
            {
                nk_ecode = string.Empty,
                unit_key = -1
            };*/

            try
            {
              /*  if (gm != null || gm.ListNameAndEmailUploadDetails.NameAndEmailUploadInputList.Count != 0)
                {*/
                    listUnitKey = rep.ExecuteSqlQuery<ComUnitKeyOutput>(SQL.Upload.NameAndEmailUploadSQLDetails.getComUnitKeySQL(gm)).ToList();
                    return listUnitKey;
                /*}
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

        public async Task insertNameAndEmailUploadDetails(NameAndEmailUploadInput gm, List<ChapterUploadFileDetailsHelper> ch, long max_seq_key, long max_grp_seq_key, List<ComUnitKeyOutput> com_unit_key_lst, long trans_key)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
            crudOutput = SQL.Upload.NameAndEmailUploadSQLDetails.insertNameAndEmailUploadRecords(gm, ch, ++max_seq_key, ++max_grp_seq_key, com_unit_key_lst, trans_key);

           // SQL.Upload.NameAndEmailUploadSQLDetails.insertNameAndEmailUploadRecords(gm, ch, ++max_seq_key, ++max_grp_seq_key, com_unit_key_lst, trans_key, out strSPQuery, out listParam);
            await rep.ExecuteStoredProcedureAsync(crudOutput.strSPQuery, crudOutput.parameters);
        }

    }
}
