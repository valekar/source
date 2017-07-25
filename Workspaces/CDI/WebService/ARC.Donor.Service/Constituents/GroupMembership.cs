using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class GroupMembership
    {
        
        public IList<Business.Constituents.GroupMembership> getConstituentGroupMembership(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.GroupMembership gd = new Data.Constituents.GroupMembership();
            var AcctLst = gd.getGroupMembership(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.GroupMembership, Business.Constituents.GroupMembership>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.GroupMembership>, IList<Business.Constituents.GroupMembership>>(AcctLst);
            return result;
        }


        public IList<Business.Constituents.AllGroupMembership> getAllGroupMembership(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.GroupMembership gd = new Data.Constituents.GroupMembership();
            var AcctLst = gd.getAllGroupMembership(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.AllGroupMembership, Business.Constituents.AllGroupMembership>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.AllGroupMembership>, IList<Business.Constituents.AllGroupMembership>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.GroupMembershipOutput> addGroupMembership(ARC.Donor.Business.Constituents.GroupMembershipInput groupInput)
        {
            Mapper.CreateMap<Business.Constituents.GroupMembershipInput, Data.Entities.Constituents.GroupMembershipInput>();
            var Input = Mapper.Map<Business.Constituents.GroupMembershipInput, Data.Entities.Constituents.GroupMembershipInput>(groupInput);
            Data.Constituents.GroupMembership gd = new Data.Constituents.GroupMembership();
            var AcctLst = gd.addGroupMembership(Input);
            Mapper.CreateMap<Data.Entities.Constituents.GroupMembershipOutput, Business.Constituents.GroupMembershipOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.GroupMembershipOutput>, IList<Business.Constituents.GroupMembershipOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.GroupMembershipOutput> editGroupMembership(ARC.Donor.Business.Constituents.GroupMembershipInput groupInput)
        {
            Mapper.CreateMap<Business.Constituents.GroupMembershipInput, Data.Entities.Constituents.GroupMembershipInput>();
            var Input = Mapper.Map<Business.Constituents.GroupMembershipInput, Data.Entities.Constituents.GroupMembershipInput>(groupInput);
            Data.Constituents.GroupMembership gd = new Data.Constituents.GroupMembership();
            var AcctLst = gd.editGroupMembership(Input);
            Mapper.CreateMap<Data.Entities.Constituents.GroupMembershipOutput, Business.Constituents.GroupMembershipOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.GroupMembershipOutput>, IList<Business.Constituents.GroupMembershipOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.GroupMembershipOutput> deleteGroupMembership(ARC.Donor.Business.Constituents.GroupMembershipInput groupInput)
        {
            Mapper.CreateMap<Business.Constituents.GroupMembershipInput, Data.Entities.Constituents.GroupMembershipInput>();
            var Input = Mapper.Map<Business.Constituents.GroupMembershipInput, Data.Entities.Constituents.GroupMembershipInput>(groupInput);
            Data.Constituents.GroupMembership gd = new Data.Constituents.GroupMembership();
            var AcctLst = gd.deleteGroupMembership(Input);
            Mapper.CreateMap<Data.Entities.Constituents.GroupMembershipOutput, Business.Constituents.GroupMembershipOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.GroupMembershipOutput>, IList<Business.Constituents.GroupMembershipOutput>>(AcctLst);
            return result;
        }
    }
}