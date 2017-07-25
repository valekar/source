using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class Relationship
    {
        public IList<Entities.Constituents.Relationship> getConstituentRelationship(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.Relationship>(SQL.Constituents.Relationship.getRelationshipSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

    }


    public class OrgRelationship
    {
        public IList<Entities.Constituents.OrgRelationship> getConstituentOrgRelationship(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var AccList = rep.ExecuteSqlQuery<Entities.Constituents.OrgRelationship>(SQL.Constituents.OrgRelationship.getOrgRelationship(NoOfRecs, PageNum, id)).ToList();
            return AccList;
        }

    }
}