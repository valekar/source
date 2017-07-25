using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Upload
{
    public class GroupMembershipReference
    {
        public static string getGroupMembershipReferenceDataSQL()
        {
            return string.Format(strGroupMembershipReferenceDataQuery).ToString();
        }

        static readonly string strGroupMembershipReferenceDataQuery = @"select * from arc_cmm_vws.grp_ref order by grp_key asc";

        public static CrudOperationOutput postNewGroupMembershipReferenceRecord(ARC.Donor.Data.Entities.Upload.GroupMembershipReferenceInsertData groupMembershipReferenceData)
        {
           // GroupMembershipReferenceInsertData ReferenceInsertDataHelper = new GroupMembershipReferenceInsertData();
            CrudOperationOutput crudOutput = new CrudOperationOutput();
           // ReferenceInsertDataHelper = groupMembershipReferenceData;
            int intNumberOfInputParameters = 9;
            List<string> listOutputParameters = new List<string> { "groupKey", "transOutput", "transKey" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_ref_tab_grp_ref", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_key", 0, "IN", TdType.BigInt, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_cd", groupMembershipReferenceData.groupCode, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_nm", groupMembershipReferenceData.groupName, "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_typ", groupMembershipReferenceData.groupType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_sub_grp_typ", groupMembershipReferenceData.subGroupType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_assgnmnt_mthd", groupMembershipReferenceData.groupAssignmentMethod, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_owner", groupMembershipReferenceData.groupOwnerMail, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", groupMembershipReferenceData.LoggedInUser, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", "Insert", "IN", TdType.VarChar, 50));

            crudOutput.parameters = ParamObjects;
           // return ReferenceInsertDataHelper;
            return crudOutput;
        }

        public static CrudOperationOutput postEditGroupMembershipReferenceRecord(ARC.Donor.Data.Entities.Upload.GroupMembershipEditReferenceParam groupMembershipEditReferenceParam)
        {
           // GroupMembershipEditReferenceParam ReferenceEditDataHelper = new GroupMembershipEditReferenceParam();
           // ReferenceEditDataHelper = groupMembershipEditReferenceParam;
            int intNumberOfInputParameters = 9;
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            List<string> listOutputParameters = new List<string> { "groupKey", "transOutput", "transKey" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_ref_tab_grp_ref", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_key", groupMembershipEditReferenceParam.groupKey, "IN", TdType.BigInt, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_cd", groupMembershipEditReferenceParam.groupCode, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_nm", groupMembershipEditReferenceParam.groupName, "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_typ", groupMembershipEditReferenceParam.groupType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_sub_grp_typ", groupMembershipEditReferenceParam.subGroupType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_assgnmnt_mthd", groupMembershipEditReferenceParam.assignmentMethod, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_owner", groupMembershipEditReferenceParam.groupOwnerMail, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", groupMembershipEditReferenceParam.LoggedInUser, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", "Update", "IN", TdType.VarChar, 50));

            crudOutput.parameters = ParamObjects;
            //return ReferenceEditDataHelper;
            return crudOutput;
        }

        public static CrudOperationOutput postDeleteGroupMembershipReferenceRecord(ARC.Donor.Data.Entities.Upload.GroupMembershipDeleteReferenceParam groupMembershipDeleteReferenceParam)
        {
           // GroupMembershipDeleteReferenceParam ReferenceDeleteDataHelper = new GroupMembershipDeleteReferenceParam();
          //  ReferenceDeleteDataHelper = groupMembershipDeleteReferenceParam;
            int intNumberOfInputParameters = 9;
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            List<string> listOutputParameters = new List<string> { "groupKey", "transOutput", "transKey" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_ref_tab_grp_ref", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_key", groupMembershipDeleteReferenceParam.groupKey, "IN", TdType.BigInt, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_cd", null, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_nm", null, "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_typ", null, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_sub_grp_typ", null, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_assgnmnt_mthd", null, "IN", TdType.VarChar, 40));
            ParamObjects.Add(SPHelper.createTdParameter("i_grp_owner", null, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", groupMembershipDeleteReferenceParam.LoggedInUser, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", "Delete", "IN", TdType.VarChar, 50));

            crudOutput.parameters = ParamObjects;
            //return ReferenceDeleteDataHelper;
            return crudOutput;
        }
    }
}
