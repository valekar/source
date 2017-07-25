using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class Death
    {
        public IList<Entities.Constituents.Death> getConstituentDeath(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.Death>(SQL.Constituents.Death.getDeathSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentDeathOutput> addConstituentDeath(ARC.Donor.Data.Entities.Constituents.ConstituentDeathInput ConstDeathInput)
        {
            Repository rep = new Repository();
            string RequestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Death.getAddDeathParameters(ConstDeathInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentDeathOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentDeathOutput> deleteConstituentDeath(ARC.Donor.Data.Entities.Constituents.ConstituentDeathInput ConstDeathInput)
        {
            Repository rep = new Repository();
            string RequestType = "Delete";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Death.getDeleteDeathParameters(ConstDeathInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentDeathOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentDeathOutput> updateConstituentDeath(ARC.Donor.Data.Entities.Constituents.ConstituentDeathInput ConstDeathInput)
        {
            Repository rep = new Repository();
            string RequestType = "Update";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Death.getUpdateDeathParameters(ConstDeathInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentDeathOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }
    }
}