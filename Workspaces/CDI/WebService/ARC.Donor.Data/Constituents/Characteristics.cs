using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class Characteristics
    {
        public IList<Entities.Constituents.Characteristics> getConstituentCharacteristics(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.Characteristics>(SQL.Constituents.Characteristics.getCharacteristicsSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentCharacteristicsOutput> addConstituentCharacteristics(ARC.Donor.Data.Entities.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {
            Repository rep = new Repository();
            string RequestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Characteristics.getAddCharacteristicsParameters(ConstCharacteristicsInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentCharacteristicsOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentCharacteristicsOutput> deleteConstituentCharacteristics(ARC.Donor.Data.Entities.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {
            Repository rep = new Repository();
            string RequestType = "Delete";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Characteristics.getDeleteCharacteristicsParameters(ConstCharacteristicsInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentCharacteristicsOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentCharacteristicsOutput> updateConstituentCharacteristics(ARC.Donor.Data.Entities.Constituents.ConstituentCharacteristicsInput ConstCharacteristicsInput)
        {
            Repository rep = new Repository();
            string RequestType = "Update";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Characteristics.getUpdateCharacteristicsParameters(ConstCharacteristicsInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentCharacteristicsOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }
    }
}