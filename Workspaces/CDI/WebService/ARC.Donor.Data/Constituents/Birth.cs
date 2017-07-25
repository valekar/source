using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class Birth
    {
        public IList<Entities.Constituents.Birth> getConstituentBirth(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.Birth>(SQL.Constituents.Birth.getBirthSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentBirthOutput> addConstituentBirth(ARC.Donor.Data.Entities.Constituents.ConstituentBirthInput ConstBirthInput)
        {
            Repository rep = new Repository();
            string RequestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Birth.getAddBirthParameters(ConstBirthInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentBirthOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentBirthOutput> deleteConstituentBirth(ARC.Donor.Data.Entities.Constituents.ConstituentBirthInput ConstBirthInput)
        {
            Repository rep = new Repository();
            string RequestType = "Delete";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Birth.getDeleteBirthParameters(ConstBirthInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentBirthOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentBirthOutput> updateConstituentBirth(ARC.Donor.Data.Entities.Constituents.ConstituentBirthInput ConstBirthInput)
        {
            Repository rep = new Repository();
            string RequestType = "Update";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Birth.getUpdateBirthParameters(ConstBirthInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentBirthOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }
    }
}