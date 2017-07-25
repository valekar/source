using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class Email
    {
        public IList<Entities.Constituents.Email> getConstituentEmail(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.Email>(SQL.Constituents.Email.getEmailSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentEmailOutput> addConstituentEmail(ARC.Donor.Data.Entities.Constituents.ConstituentEmailInput ConstEmailInput)
        {
            Repository rep = new Repository();
            string RequestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Email.getAddEmailParameters(ConstEmailInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentEmailOutput>(strSPQuery, listParam).ToList();
             return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentEmailOutput> deleteConstituentEmail(ARC.Donor.Data.Entities.Constituents.ConstituentEmailInput ConstEmailInput)
        {
            Repository rep = new Repository();
            string RequestType = "delete";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Email.getDeleteEmailParameters(ConstEmailInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentEmailOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentEmailOutput> updateConstituentEmail(ARC.Donor.Data.Entities.Constituents.ConstituentEmailInput ConstEmailInput)
        {
            Repository rep = new Repository();
            string RequestType = "update";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Email.getUpdateEmailParameters(ConstEmailInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentEmailOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }
    }
}