using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public class ConstituentSearchResults: Entities.QueryLogger
    {
        public IList<Entities.Constituents.ConstituentOutputSearchResults> getConstituentSearchResults(string master_id)
        {
            Repository rep = new Repository();
            string Qry = SQL.ConstituentSearchSQL.getConstituentSearchSQL(master_id);
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.ConstituentOutputSearchResults>(Qry).ToList();
            this._EndTime = DateTime.Now;
            return AcctLst;
        }
    }

    public class ConstSearchDat : Entities.QueryLogger
    {
        public IList<Entities.Constituents.ConstituentOutputSearchResults> getConstituentSearchResults(ListConstituentInputSearchModel searchInput)
        {
            Repository rp = new Repository();
            
            //Check if masters are of same constituent type
            Boolean boolCheckTypes = false;
            List<string> listType = new List<string>();
            foreach (ConstituentInputSearchModel cnst in searchInput.ConstituentInputSearchModel)
            {
                listType.Add(cnst.type);
            }
            
            if(listType.Distinct().Count() > 1)
            {
                boolCheckTypes = true;
            }

            if (!boolCheckTypes)
            {
                string Qry = SQL.ConstituentSQL.getConstituentAdvSearchResultsSQL(searchInput);
                this._Query = Qry;
                this._StartTime = DateTime.Now;
                var SearchResults = rp.ExecuteSqlQuery<Entities.Constituents.ConstituentOutputSearchResults>(Qry).ToList();
                this._EndTime = DateTime.Now;
                // retrieve the external bridge counts for the list of master ids.
                String strResultMasterIdsCSV = string.Empty;
                Dictionary<string, string> dictSourceSystemCounts = new Dictionary<string, string>();
                if (SearchResults != null)
                {
                    if (SearchResults.Count > 0)
                    {
                        foreach (Entities.Constituents.ConstituentOutputSearchResults cnstreslt in SearchResults)
                        {
                            strResultMasterIdsCSV += (string.IsNullOrEmpty(strResultMasterIdsCSV) ? cnstreslt.constituent_id : ", " + cnstreslt.constituent_id);
                            dictSourceSystemCounts.Add(cnstreslt.constituent_id, string.Empty);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(strResultMasterIdsCSV))
                {
                    var BridgeCount = rp.ExecuteSqlQuery<Entities.Constituents.BridgeCount>(SQL.ConstituentSQL.getBridgeCount(strResultMasterIdsCSV)).ToList();
                    dictSourceSystemCounts = getBridgeCountData(BridgeCount, dictSourceSystemCounts);
                    foreach (Entities.Constituents.ConstituentOutputSearchResults drResult in SearchResults)
                    {
                        drResult.source_system_count = dictSourceSystemCounts[drResult.constituent_id];
                    }
                }

                return SearchResults;
            }
            return null;
        }

        //method to retrieve the bridge and their counts for the master ids
        public Dictionary<string, string> getBridgeCountData(IList<Entities.Constituents.BridgeCount> dsResultCount, Dictionary<string, string> dictSourceSystemCounts)
        {
            // update the dictionary of counts with the result set
            foreach (Entities.Constituents.BridgeCount drResultCount in dsResultCount)
            {
                string strMasterIdResultCount = drResultCount.cnst_mstr_id;
                string strMasterIdCount = drResultCount.arc_srcsys_cd + " (" + drResultCount.counter + ")";
                dictSourceSystemCounts[strMasterIdResultCount] += (string.IsNullOrEmpty(dictSourceSystemCounts[strMasterIdResultCount]) ?
                                                                    strMasterIdCount : ", " + strMasterIdCount);
            }
            return dictSourceSystemCounts;
        }
    }
}
