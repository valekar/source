using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Upload
{
    public class GroupMembershipReferenceDetails
    {
        public IList<Entities.Upload.GroupMembershipReference> getGroupMembershipReferenceData()
        {
            Repository rep = new Repository();
            var reference_data = rep.ExecuteSqlQuery<Entities.Upload.GroupMembershipReference>(SQL.Upload.GroupMembershipReference.getGroupMembershipReferenceDataSQL()).ToList();
            return reference_data;
        }

        public Entities.Upload.GroupMembershipReferenceDataSPOutput postNewGroupMembershipReferenceRecord(GroupMembershipReferenceInsertData inputReferenceData)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
            //SQL.Upload.GroupMembershipReference.postNewGroupMembershipReferenceRecord(inputReferenceData, out strSPQuery, out listParam);
            crudOutput = SQL.Upload.GroupMembershipReference.postNewGroupMembershipReferenceRecord(inputReferenceData);
            var groupMembershipReferenceInsertOuput = rep.ExecuteStoredProcedure<Entities.Upload.GroupMembershipReferenceDataSPOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return groupMembershipReferenceInsertOuput[0];
        }

        public Entities.Upload.GroupMembershipReferenceDataSPOutput postEditGroupMembershipReferenceRecord(GroupMembershipEditReferenceParam groupMembershipEditReferenceParam)
        {
            Repository rep = new Repository();
           // SQL.Upload.GroupMembershipReference.postEditGroupMembershipReferenceRecord(groupMembershipEditReferenceParam, out strSPQuery, out listParam);
            CrudOperationOutput crudOutput  = SQL.Upload.GroupMembershipReference.postEditGroupMembershipReferenceRecord(groupMembershipEditReferenceParam);
            var groupMembershipReferenceEditOuput = rep.ExecuteStoredProcedure<Entities.Upload.GroupMembershipReferenceDataSPOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return groupMembershipReferenceEditOuput[0];
        }

        public Entities.Upload.GroupMembershipReferenceDataSPOutput postDeleteGroupMembershipReferenceRecord(GroupMembershipDeleteReferenceParam groupMembershipDeleteReferenceParam)
        {
            Repository rep = new Repository();

            CrudOperationOutput crudOutput = SQL.Upload.GroupMembershipReference.postDeleteGroupMembershipReferenceRecord(groupMembershipDeleteReferenceParam);
            var groupMembershipReferenceDeleteOuput = rep.ExecuteStoredProcedure<Entities.Upload.GroupMembershipReferenceDataSPOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return groupMembershipReferenceDeleteOuput[0];
        }
    }
}
