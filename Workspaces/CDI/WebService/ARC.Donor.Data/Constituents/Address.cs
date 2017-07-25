using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARC.Donor.Data;

namespace ARC.Donor.Data.Constituents
{
    public class Address
    {
        public IList<Entities.Constituents.Address> getConstituentAddress(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.Address>(SQL.Constituents.Address.getAddressSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentAddressOutput> addConstituentAddress(ARC.Donor.Data.Entities.Constituents.ConstituentAddressInput ConstAddressInput)
        {
            Repository rep = new Repository();
            string RequestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Address.getAddAddressParameters(ConstAddressInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentAddressOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentAddressOutput> deleteConstituentAddress(ARC.Donor.Data.Entities.Constituents.ConstituentAddressInput ConstAddressInput)
        {
            Repository rep = new Repository();
            string RequestType = "Delete";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Address.getDeleteAddressParameters(ConstAddressInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentAddressOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentAddressOutput> updateConstituentAddress(ARC.Donor.Data.Entities.Constituents.ConstituentAddressInput ConstAddressInput)
        {
            Repository rep = new Repository();
            string RequestType = "Update";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Address.getUpdateAddressParameters(ConstAddressInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentAddressOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }
    }
}