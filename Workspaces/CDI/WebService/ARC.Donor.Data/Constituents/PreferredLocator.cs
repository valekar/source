using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public class PreferredLocator
    {
        public IList<Entities.Constituents.PreferredLocator> getPreferredLocator(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.PreferredLocator>(SQL.Constituents.PreferredLocator.getPreferredLocatorSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }


        public IList<Entities.Constituents.PreferredLocatorOutput> addPreferredLocator(Entities.Constituents.PreferredLocatorInput preferredLocatorInput)
        {
            string requestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.PreferredLocator.addPreferredLocatorSQL(preferredLocatorInput, requestType, out strSPQuery, out listParam);
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.PreferredLocatorOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.PreferredLocatorOutput> updatePreferredLocator(Entities.Constituents.PreferredLocatorInput preferredLocatorInput)
        {
            string requestType = "Update";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.PreferredLocator.updatePreferredLocatorSQL(preferredLocatorInput, requestType, out strSPQuery, out listParam);
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.PreferredLocatorOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }


        public IList<Entities.Constituents.PreferredLocatorOutput> inactivatePreferredLocator(Entities.Constituents.PreferredLocatorInput preferredLocatorInput)
        {
            string requestType = "Delete";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.PreferredLocator.inactivatePreferredLocatorSQL(preferredLocatorInput, requestType, out strSPQuery, out listParam);
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.PreferredLocatorOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }
    }
}
