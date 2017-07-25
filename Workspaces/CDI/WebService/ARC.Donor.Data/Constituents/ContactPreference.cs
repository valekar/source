using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class ContactPreference: Entities.QueryLogger
    {
        public IList<Entities.Constituents.ContactPreference> getConstituentContactPreference(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            string Qry = SQL.Constituents.ContactPreference.getContactPreferenceSQL(NoOfRecs, PageNum, id);
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.ContactPreference>(Qry).ToList();
            this._EndTime = DateTime.Now;
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentContactPrefcOutput> addConstituentContactPrefc(ARC.Donor.Data.Entities.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput)
        {
            Repository rep = new Repository();
            this._StartTime = DateTime.Now;
            string RequestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.ContactPreference.getAddContactPrefcParameters(ConstContactPrefcInput, RequestType, out strSPQuery, out listParam);
            this._Query = strSPQuery;
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentContactPrefcOutput>(strSPQuery, listParam).ToList();
            this._EndTime = DateTime.Now;
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentContactPrefcOutput> deleteConstituentContactPrefc(ARC.Donor.Data.Entities.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput)
        {
            Repository rep = new Repository();
            string RequestType = "Delete";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.ContactPreference.getDeleteContactPrefcParameters(ConstContactPrefcInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentContactPrefcOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentContactPrefcOutput> updateConstituentContactPrefc(ARC.Donor.Data.Entities.Constituents.ConstituentContactPrefcInput ConstContactPrefcInput)
        {
            Repository rep = new Repository();
            string RequestType = "Update";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.ContactPreference.getUpdateContactPrefcParameters(ConstContactPrefcInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentContactPrefcOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }
    }
}

