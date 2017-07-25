using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class Name
    {
        public IList<Entities.Constituents.PersonName> getConstituentPersonName(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.PersonName>(SQL.Constituents.Name.getPersonNameSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.OrgName> getConstituentOrgName(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.OrgName>(SQL.Constituents.Name.getOrgNameSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentPersonNameOutput> addConstituentPersonName(ARC.Donor.Data.Entities.Constituents.ConstituentPersonNameInput ConstNameInput)
        {
            Repository rep = new Repository();
            string RequestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.AddPersonName.getAddPersonNameParameters(ConstNameInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentPersonNameOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentPersonNameOutput> deleteConstituentPersonName(ARC.Donor.Data.Entities.Constituents.ConstituentPersonNameInput ConstNameInput)
        {
            Repository rep = new Repository();
            string RequestType = "delete";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.DeletePersonName.getDeletePersonNameParameters(ConstNameInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentPersonNameOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentPersonNameOutput> updateConstituentPersonName(ARC.Donor.Data.Entities.Constituents.ConstituentPersonNameInput ConstNameInput)
        {
            Repository rep = new Repository();
            string RequestType = "Update";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.UpdatePersonName.getUpdatePersonNameParameters(ConstNameInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentPersonNameOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        /*Add,Update and Delete Data for Org Name*/
        public IList<Entities.Constituents.ConstituentOrgNameOutput> addConstituentOrgName(ARC.Donor.Data.Entities.Constituents.ConstituentOrgNameInput ConstNameInput)
        {
            Repository rep = new Repository();
            string RequestType = "insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.WriteOrgName.getWriteOrgNameParameters(ConstNameInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentOrgNameOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentOrgNameOutput> deleteConstituentOrgName(ARC.Donor.Data.Entities.Constituents.ConstituentOrgNameInput ConstNameInput)
        {
            Repository rep = new Repository();
            string RequestType = "delete";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.WriteOrgName.getWriteOrgNameParameters(ConstNameInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentOrgNameOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentOrgNameOutput> updateConstituentOrgName(ARC.Donor.Data.Entities.Constituents.ConstituentOrgNameInput ConstNameInput)
        {
            Repository rep = new Repository();
            string RequestType = "update";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.WriteOrgName.getWriteOrgNameParameters(ConstNameInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentOrgNameOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }
    }
}