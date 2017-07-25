using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs;
using NLog;

namespace ARC.Donor.Data.Orgler.EnterpriseOrgs
{
    public class Search
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        /* Method name: getEnterpriseOrgSearchResults
         * Input Parameters: An object of ListEnterpriseOrgInputSearchModel class
         * Output Parameters: A list of EnterpriseOrgOutputSearchResults class which is a mapper class for the search results
         * Purpose: This method calls the various components of the Data layer to execute the search for enterprise org and returns the results back to te service */
        //public IList<EnterpriseOrgMapper> getEnterpriseOrgSearchResults(ListEnterpriseOrgInputSearchModel listEnterpriseOrgSearchInput)
        //{
        //    //Instantiate an object of the Repository class
        //    Repository rep = new Repository("TDOrglerEF");

        //    //Get the query for enterprise org search based on the input provided
        //    string Qry = Data.SQL.Orgler.EnterpriseOrgs.SearchSQL.getEnterpriseOrgSearchResultsSQL(listEnterpriseOrgSearchInput);

        //    //execute the search query and return back the results
        //    var SearchResults = rep.ExecuteSqlQuery<EnterpriseOrgMapper>(Qry).ToList();
        //    return SearchResults;
        //}

        //added by supriya
        public IList<EnterpriseOrgMapper> getEnterpriseOrgSearchResults(ListEnterpriseOrgInputSearchModel listEnterpriseOrgSearchInput)
        {
            //Instantiate an object of the Repository class
            Repository rep = new Repository("TDOrglerEF");

            //Get the query for enterprise org search based on the input provided
            string Qry = Data.SQL.Orgler.EnterpriseOrgs.SearchSQL.getEnterpriseOrgSearchResultsSQL(listEnterpriseOrgSearchInput);

            //execute the search query and return back the results
            var SearchResults = rep.ExecuteSqlQuery<EnterpriseOrgMapper>(Qry).ToList();
            return SearchResults;
        }
    }
}
