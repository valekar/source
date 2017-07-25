using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class GroupMembership
    {
        public IList<Entities.Constituents.GroupMembership> getGroupMembership(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.GroupMembership>(SQL.Constituents.GroupMembership.getGroupMembershipSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.AllGroupMembership> getAllGroupMembership(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.AllGroupMembership>(SQL.Constituents.GroupMembership.getAllGroupMembershipSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }


        public IList<Entities.Constituents.GroupMembershipOutput> addGroupMembership(ARC.Donor.Data.Entities.Constituents.GroupMembershipInput groupMembershipInput)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.GroupMembership.addGroupMembershipParams(groupMembershipInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.GroupMembershipOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.GroupMembershipOutput> deleteGroupMembership(ARC.Donor.Data.Entities.Constituents.GroupMembershipInput groupMembershipInput)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.GroupMembership.deleteGroupMembershipParams(groupMembershipInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.GroupMembershipOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.GroupMembershipOutput> editGroupMembership(ARC.Donor.Data.Entities.Constituents.GroupMembershipInput groupMembershipInput)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.GroupMembership.editGroupMembershipParams(groupMembershipInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.GroupMembershipOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;
        }
    }
}