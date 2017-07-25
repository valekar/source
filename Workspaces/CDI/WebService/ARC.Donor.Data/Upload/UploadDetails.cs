using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Upload
{
    public class UploadDetails
    {

        public async Task<IList<ChapterCodeValidationOutput>> validateChapterCodeDetails(GroupMembershipValidationInput gmvi)
        {
            Repository rep = new Repository();
           /* if (gmvi._chapterCodes != null || gmvi._chapterCodes.Count != 0)
            {
                if (!gmvi._chapterCodes.All(x => x.Equals("")))
                {*/
                    var _validChapterCodes = await rep.ExecuteSqlQueryAsync<ChapterCodeValidationOutput>(SQL.Upload.UploadValidation.getChapterCodeValidationSQL(gmvi._chapterCodes));
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

        public async Task<IList<GroupCodeValidationOutput>> validateGroupCodeDetails(GroupMembershipValidationInput gmvi)
        {
            Repository rep = new Repository();
           /* if (gmvi._groupCodes != null || gmvi._groupCodes.Count != 0)
            {
                if (!gmvi._groupCodes.All(x => x.Equals("")))
                {*/
                    var _validGroupCodes = await rep.ExecuteSqlQueryAsync<GroupCodeValidationOutput>(SQL.Upload.UploadValidation.getGroupCodeValidationSQL(gmvi._groupCodes));
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

        public async Task<IList<string>> validateMasterIdDetails(GroupMembershipValidationInput gmvi)
        {
            Repository rep = new Repository();
           /* if (gmvi._masterIds != null || gmvi._masterIds.Count != 0)
            {*/
                var _inputMasterIds = (from item in gmvi._masterIds
                                       where !string.IsNullOrEmpty(item)
                                       select item).ToList();
              /*  if (!gmvi._masterIds.All(x => x.Equals("")))
                {*/
                    var _masterIds = await rep.ExecuteSqlQueryAsync<string>(SQL.Upload.UploadValidation.getMasterIdValidationSQL(_inputMasterIds));
                    return _masterIds;
                /*}
                else
                    return gmvi._masterIds;
            }
            else
            {
                return new List<string>(); //If the incoming list of master ids is null or they do not contain any data - instantiate the list and send back
            }*/
        }

        public long getGroupMembershipTransKeyDetails(GroupMembershipUploadDetails gm)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            long strTransactionKey = -1;

            try
            {
                //SQL.Upload.UploadDetails.postGroupMembershipCreateTrans(gm, out strSPQuery, out listParam);
                crudOutput = SQL.Upload.UploadDetails.postGroupMembershipCreateTrans(gm);
                var GroupMembershipCreateTransOutput = rep.ExecuteStoredProcedure<UploadCreateTransOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
                strTransactionKey = GroupMembershipCreateTransOutput[0] != null ? GroupMembershipCreateTransOutput[0].transKey : strTransactionKey;
                return strTransactionKey;
            }
            catch (Exception e)
            {
                return strTransactionKey;
            }
        }

        public long getGroupMembershipSeqKeyDetails()
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

        public long getGroupMembershipGrpSeqKeyDetails()
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


        public List<ComUnitKeyOutput> getGroupMembershipUnitKeyDetails(GroupMembershipUploadDetails gm)
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
                /*if (gm != null || gm.ListGroupMembershipUploadDetailsInput.GroupMembershipUploadInputList.Count != 0)
                {*/
                    listUnitKey = rep.ExecuteSqlQuery<ComUnitKeyOutput>(SQL.Upload.UploadDetails.getComUnitKeySQL(gm)).ToList();
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


        public async Task insertGroupMembershipUploadDetails(GroupMembershipUploadInput gm, List<ChapterUploadFileDetailsHelper> ch, long max_seq_key, long max_grp_seq_key, List<ComUnitKeyOutput> com_unit_key_lst, long trans_key)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
           //SQL.Upload.UploadDetails.insertGroupMembershipRecords(gm, ch, ++max_seq_key, ++max_grp_seq_key, com_unit_key_lst, trans_key, out strSPQuery, out listParam);
            crudOutput = SQL.Upload.UploadDetails.insertGroupMembershipRecords(gm, ch, ++max_seq_key, ++max_grp_seq_key, com_unit_key_lst, trans_key);
            await rep.ExecuteStoredProcedureAsync(crudOutput.strSPQuery, crudOutput.parameters);
        }

    }
}
