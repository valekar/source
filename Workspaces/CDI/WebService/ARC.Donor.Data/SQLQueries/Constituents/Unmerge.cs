using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;
using ARC.Donor.Data.Entities.Constituents;

namespace ARC.Donor.Data.SQL.Constituents
{
    class Unmerge
    {
        public static void getUnmergeParameters(UnmergeInput UnmergeInput, out string strSPQuery, out List<object> parameters)
        {
            int intNumberOfInputParameters = 13;
            List<string> listOutputParameters = new List<string> { "o_transaction_key", "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_unmerge", intNumberOfInputParameters, listOutputParameters);
            
            string strNotes = string.Empty;
            string strCaseNumber = "0";
            string strUserName = string.Empty;
            string strMasterGroup1 = null;
            string strMasterGroup2 = null;
            string strMasterGroup3 = null;
            string strMasterGroup4 = null;
            string strMasterGroup5 = null;
            int intPersistenceInd1 = 0;
            int intPersistenceInd2 = 0;
            int intPersistenceInd3 = 0;
            int intPersistenceInd4 = 0;
            int intPersistenceInd5 = 0;

            if (!string.IsNullOrEmpty(UnmergeInput.Notes))
                strNotes = UnmergeInput.Notes;
            if (!string.IsNullOrEmpty(UnmergeInput.CaseNumber))
                strCaseNumber = UnmergeInput.CaseNumber;
            if (!string.IsNullOrEmpty(UnmergeInput.UserName))
                strUserName = UnmergeInput.UserName;

            List<UnMasterRequest> listUnMasterRequest = new List<UnMasterRequest>();
            listUnMasterRequest = UnmergeInput.UnmergeRequestDetails;

            Dictionary<int, List<UnMasterRequest>> dictUnMasterRequest = new Dictionary<int, List<UnMasterRequest>>();
            //dictionary to hold
            Dictionary<int, int> dictPersistence = new Dictionary<int, int>();
            if (listUnMasterRequest != null)
            {
                foreach (UnMasterRequest unMasterRequest in listUnMasterRequest)
                {
                    if (dictUnMasterRequest.ContainsKey(unMasterRequest.MasterGroup))
                    {
                        dictUnMasterRequest[unMasterRequest.MasterGroup].Add(unMasterRequest);
                    }
                    else
                    {
                        List<UnMasterRequest> listTemp = new List<UnMasterRequest>();
                        listTemp.Add(unMasterRequest);
                        dictUnMasterRequest.Add(unMasterRequest.MasterGroup, listTemp);

                        dictPersistence.Add(unMasterRequest.MasterGroup, unMasterRequest.intPersistence);
                    }
                }
            }

            if (dictUnMasterRequest != null)
            {
                int intMasterGroupNum = 1;
                foreach (KeyValuePair<int, List<UnMasterRequest>> kvOuterPair in dictUnMasterRequest)
                {
                    string strOuterCSV = string.Empty;
                    List<UnMasterRequest> listUnMaster = kvOuterPair.Value;
                    foreach (UnMasterRequest unMasterRequest in listUnMaster)
                    {
                        string strInnerCSV = string.Empty;
                        strInnerCSV += unMasterRequest.MasterId;
                        strInnerCSV += "|";
                        strInnerCSV += unMasterRequest.SourceSystemCode;
                        strInnerCSV += "|";
                        strInnerCSV += unMasterRequest.SourceSystemId;
                        strInnerCSV += "|";
                        strInnerCSV += unMasterRequest.ConstituentType;
                        strOuterCSV = string.IsNullOrEmpty(strOuterCSV) ? strInnerCSV : strOuterCSV + "," + strInnerCSV;
                    }
                    if (intMasterGroupNum == 1)
                        strMasterGroup1 = strOuterCSV;
                    else if (intMasterGroupNum == 2)
                        strMasterGroup2 = strOuterCSV;
                    else if (intMasterGroupNum == 3)
                        strMasterGroup3 = strOuterCSV;
                    else if (intMasterGroupNum == 4)
                        strMasterGroup4 = strOuterCSV;
                    else if (intMasterGroupNum == 5)
                        strMasterGroup5 = strOuterCSV;
                    intMasterGroupNum += 1;
                }
            }

            //populate persistence indicator for all 5 groups
            if (dictPersistence != null)
            {
                int intPersistenceNum = 1;
                foreach (KeyValuePair<int, int> kvOuterPair in dictPersistence)
                {
                    if (intPersistenceNum == 1)
                        intPersistenceInd1 = kvOuterPair.Value;
                    else if (intPersistenceNum == 2)
                        intPersistenceInd2 = kvOuterPair.Value;
                    else if (intPersistenceNum == 3)
                        intPersistenceInd3 = kvOuterPair.Value;
                    else if (intPersistenceNum == 4)
                        intPersistenceInd4 = kvOuterPair.Value;
                    else if (intPersistenceNum == 5)
                        intPersistenceInd5 = kvOuterPair.Value;
                    intPersistenceNum += 1;
                }
            }

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", strUserName, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", strNotes, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", strCaseNumber, "IN", TdType.BigInt, 0));

            ParamObjects.Add(SPHelper.createTdParameter("i_master_group1", strMasterGroup1, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_master_group2", strMasterGroup2, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_master_group3", strMasterGroup3, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_master_group4", strMasterGroup4, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_master_group5", strMasterGroup5, "IN", TdType.VarChar, 1200));

            ParamObjects.Add(SPHelper.createTdParameter("i_persistence_ind1", intPersistenceInd1, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_persistence_ind2", intPersistenceInd2, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_persistence_ind3", intPersistenceInd3, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_persistence_ind4", intPersistenceInd4, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_persistence_ind5", intPersistenceInd5, "IN", TdType.ByteInt, 0));

            parameters = ParamObjects;
        }
    }
}
