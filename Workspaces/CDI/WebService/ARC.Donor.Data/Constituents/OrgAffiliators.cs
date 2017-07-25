using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public class OrgAffiliators
    {
        public IList<Entities.Constituents.OrgAffiliators> getConstituentOrgAffiliators(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.OrgAffiliators>(SQL.Constituents.OrgAffiliators.getOrgAffiliatorsSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        /*Create and Delete Data Access for OrgAffiliators*/
        public IList<Entities.Constituents.OrgAffiliatorsOutput> addOrgAffiliators(ARC.Donor.Data.Entities.Constituents.OrgAffiliatorsInput OrgAffiliatorsInput)
        {
            Repository rep = new Repository();
            string RequestType = "insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.OrgAffiliators.getWriteOrgAffiliatorsParameters(OrgAffiliatorsInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.OrgAffiliatorsOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.OrgAffiliatorsOutput> deleteOrgAffiliators(ARC.Donor.Data.Entities.Constituents.OrgAffiliatorsInput OrgAffiliatorsInput)
        {
            Repository rep = new Repository();
            string RequestType = "delete";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.OrgAffiliators.getWriteOrgAffiliatorsParameters(OrgAffiliatorsInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.OrgAffiliatorsOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }
    }
}
