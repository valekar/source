using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class Relationship
    {
        public IList<Business.Constituents.Relationship> getConstituentRelationship(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.Relationship gd = new Data.Constituents.Relationship();
            var AcctLst = gd.getConstituentRelationship(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.Relationship, Business.Constituents.Relationship>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.Relationship>, IList<Business.Constituents.Relationship>>(AcctLst);
            return result;
        }
    }


    public class OrgRelationship
    {
        public IList<Business.Constituents.OrgRelationship> getOrgConstituentRelationship(int NoOfRecs, int PageNum, string org_mstr_id)
        {
            Mapper.CreateMap<Data.Entities.Constituents.OrgRelationship, Business.Constituents.OrgRelationship>();
            Data.Constituents.OrgRelationship gd = new Data.Constituents.OrgRelationship();
            var AccList = gd.getConstituentOrgRelationship(NoOfRecs, PageNum, org_mstr_id);
            var result = Mapper.Map<IList<Data.Entities.Constituents.OrgRelationship>, IList<Business.Constituents.OrgRelationship>>(AccList);
            return result;
        }
    }
}