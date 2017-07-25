using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.Constituents
{
    public class Phone
    {
        public IList<Entities.Constituents.Phone> getConstituentPhone(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.Phone>(SQL.Constituents.Phone.getPhoneSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentPhoneOutput> addConstituentPhone(ARC.Donor.Data.Entities.Constituents.ConstituentPhoneInput ConstPhoneInput)
        {
            Repository rep = new Repository();
            string RequestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Phone.getAddPhoneParameters(ConstPhoneInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentPhoneOutput>(strSPQuery, listParam).ToList();
            //var AcctLst = rep.ExecuteSqlQuerySP<Entities.Constituents.AddConstNameInput>("dw_stuart_macs.strx_loc_dtl_prsn_nm", SQL.Constituents.AddName.getAddNameParameters(ConstNameInput, RequestType)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentPhoneOutput> deleteConstituentPhone(ARC.Donor.Data.Entities.Constituents.ConstituentPhoneInput ConstPhoneInput)
        {
            Repository rep = new Repository();
            string RequestType = "delete";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Phone.getDeletePhoneParameters(ConstPhoneInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentPhoneOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.ConstituentPhoneOutput> updateConstituentPhone(ARC.Donor.Data.Entities.Constituents.ConstituentPhoneInput ConstPhoneInput)
        {
            Repository rep = new Repository();
            string RequestType = "Update";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.Phone.getUpdatePhoneParameters(ConstPhoneInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.ConstituentPhoneOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }
    }
}