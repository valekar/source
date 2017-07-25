using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ARC.Donor.Data.Entities.Locator;
using ARC.Donor.Data.Entities.LocatorDomain;
using ARC.Donor.Data.Entities.LocatorAddress;
using System.Reflection;

namespace ARC.Donor.Data.Locator
{
    public class LocatorEmailSearchData:Entities.QueryLogger
    {
        public IList<Entities.Locator.LocatorEmailOutputSearchResults> getLocatorEmailSearchResults(ListlocatorEmailInputSearchModel searchInput)
        {
            Repository rp = new Repository();
            string Qry = SQL.LocatorSQL.getLocatorEmailSearchSQL(searchInput);
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var SearchResults = rp.ExecuteSqlQuery<Entities.Locator.LocatorEmailOutputSearchResults>(Qry).ToList();
            this._EndTime = DateTime.Now;

            // retrieve the external bridge counts for the list of master ids.
            String strResultMasterIdsCSV = string.Empty;
            Dictionary<string, string> dictSourceSystemCounts = new Dictionary<string, string>();
           
            if (SearchResults != null)
            {
                if (SearchResults.Count > 0)
                {
                    var str = "";
                    foreach (Entities.Locator.LocatorEmailOutputSearchResults cnstreslt in SearchResults)
                    {
                        
                        if (str == "" || str!= cnstreslt.email_key)
                        {
                        str = cnstreslt.email_key;
                        strResultMasterIdsCSV += (string.IsNullOrEmpty(strResultMasterIdsCSV) ? cnstreslt.cnst_email_addr : ", " + cnstreslt.cnst_email_addr);
                        dictSourceSystemCounts.Add(cnstreslt.cnst_email_addr, string.Empty);
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(strResultMasterIdsCSV))
            {
                var BridgeCount = rp.ExecuteSqlQuery<Entities.Locator.BridgeCount>(SQL.LocatorSQL.getBridgeCount(strResultMasterIdsCSV)).ToList();
                dictSourceSystemCounts = getBridgeCountData(BridgeCount, dictSourceSystemCounts);
                foreach (Entities.Locator.LocatorEmailOutputSearchResults drResult in SearchResults)
                {
                    drResult.cnst_email_cnt = dictSourceSystemCounts[drResult.cnst_email_addr];
                }
            }


            return SearchResults;
        }

        public IList<Entities.Locator.LocatorEmailOutputSearchResults> getLocatorEmailDetailsByID(LocatorEmailInputSearchModel searchInput)
        {
            Repository rp = new Repository();
            string Qry = SQL.LocatorSQL.getLocatorDeatilsByIDSQL(searchInput);
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var SearchResults = rp.ExecuteSqlQuery<Entities.Locator.LocatorEmailOutputSearchResults>(Qry).ToList();
            this._EndTime = DateTime.Now;
            return SearchResults;
        }


        public IList<Entities.Locator.LocatorEmailConstOutputSearchResults> getLocatorEmailConstDetailsByID(LocatorEmailInputSearchModel searchInput)
        {
            Repository rp = new Repository();
            string Qry = SQL.LocatorSQL.getLocatorConstDeatilsByIDSQL(searchInput);
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var SearchResults = rp.ExecuteSqlQuery<Entities.Locator.LocatorEmailConstOutputSearchResults>(Qry).ToList();
            this._EndTime = DateTime.Now;
            return SearchResults;
        }

        public IList<Entities.Locator.CreateLocatorEmailOutput> updateLocatorEmail(ARC.Donor.Data.Entities.Locator.CreateLocatorEmailInput locatorEmailInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            string request_action = "update";
            List<object> listParam = new List<object>();
            SQL.LocatorSQL.getWriteLocatorEmailParameters(locatorEmailInput,out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Locator.CreateLocatorEmailOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }


        
        public Dictionary<string, string> getBridgeCountData(IList<Entities.Locator.BridgeCount> dsResultCount, Dictionary<string, string> dictSourceSystemCounts)
        {
            
            foreach (Entities.Locator.BridgeCount drResultCount in dsResultCount)
            {
                string strMasterIdResultCount = drResultCount.cnst_email_addr;
                string strMasterIdCount = drResultCount.cnst_mstr_subj_area_cd + " (" + drResultCount.cnt + ")";
                dictSourceSystemCounts[strMasterIdResultCount] += (string.IsNullOrEmpty(dictSourceSystemCounts[strMasterIdResultCount]) ?
                                                                    strMasterIdCount : ", " + strMasterIdCount);
            }
            return dictSourceSystemCounts;
        }

        
    }
    public class LocatorAddressSearchData : Entities.QueryLogger
    {
        public IList<Entities.LocatorAddress.LocatorAddressOutputSearchResults> getLocatorAddress(ListLocatorAddressInputSearchModel searchInput)
        {
            Repository rp = new Repository();
            string Qry = SQL.LocatorSQL.getLocatorAddressDataSQL(searchInput);
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var SearchResults = rp.ExecuteSqlQuery<Entities.LocatorAddress.LocatorAddressOutputSearchResults>(Qry).ToList();
            this._EndTime = DateTime.Now;
            return SearchResults;
        }

        public IList<Entities.LocatorAddress.LocatorAddressOutputSearchResults> getLocatorAddressByID(LocatorAddressInputSearchModel searchInput)
        {
            Repository rp = new Repository();
            string Qry = SQL.LocatorSQL.getLocatorAddressDataSQLById(searchInput);
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var SearchResults = rp.ExecuteSqlQuery<Entities.LocatorAddress.LocatorAddressOutputSearchResults>(Qry).ToList();
            this._EndTime = DateTime.Now;
            
            return SearchResults;
        }


        public IList<Entities.LocatorAddress.LocatorAddressOutputSearchResults> getLocatorAddressAssessmentByID(LocatorAddressInputSearchModel searchInput)
        {
            Repository rp = new Repository();
            string Qry = SQL.LocatorSQL.getLocatorAddressAssessmentDataSQLById(searchInput);
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var SearchResults = rp.ExecuteSqlQuery<Entities.LocatorAddress.LocatorAddressOutputSearchResults>(Qry).ToList();
            this._EndTime = DateTime.Now;

            return SearchResults;
        }

        public IList<Entities.LocatorAddress.LocatorAddressConstituentsOutputSearchResults> getLocatorAddressConstituentsByID(LocatorAddressInputSearchModel searchInput)
        {
            Repository rp = new Repository();
            string Qry = SQL.LocatorSQL.getLocatorAddressConstituentsDataSQLById(searchInput);            
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var SearchResults = rp.ExecuteSqlQuery<Entities.LocatorAddress.LocatorAddressConstituentsOutputSearchResults>(Qry).ToList();
             this._EndTime = DateTime.Now;

            return SearchResults;
        }

        public IList<Entities.LocatorAddress.CreateLocatorAddressOutput> updateLocatorAddress(ARC.Donor.Data.Entities.LocatorAddress.CreateLocatorAddressInput locatorAddressInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            string request_action = "update";
            List<object> listParam = new List<object>();
            SQL.LocatorSQL.getWriteLocatorAddressParameters(locatorAddressInput, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.LocatorAddress.CreateLocatorAddressOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }


    }
    public class LocatorDomainSearchData : Entities.QueryLogger
    {
        public IList<Entities.LocatorDomain.LocatorDomainOutputSearchResults> getLocatorEmailDomain(ListLocatorDomainInputSearchModel searchInput)
        {
            Repository rp = new Repository();
            string Qry = SQL.LocatorSQL.getEmailDomain_CorrectionDataSQL(searchInput);
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var SearchResults = rp.ExecuteSqlQuery<Entities.LocatorDomain.LocatorDomainOutputSearchResults>(Qry).ToList();
            this._EndTime = DateTime.Now;
            return SearchResults;
        }

        public IList<Entities.LocatorDomain.CreateLocatorDomainOutput> updateLocatorDomain(ARC.Donor.Data.Entities.LocatorDomain.ListLocatorDomainInputSearchModel locatordomainInput)
        {


            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            string request_action = "update";
            List<object> listParam = new List<object>();
            SQL.LocatorSQL.updateEmailDomainStatus(locatordomainInput, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.LocatorDomain.CreateLocatorDomainOutput>(strSPQuery, listParam);
            return AcctLst.ToList();
        }

    }

}
