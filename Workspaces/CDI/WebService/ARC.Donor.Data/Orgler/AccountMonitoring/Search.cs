using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Orgler.AccountMonitoring;
using NLog;

namespace ARC.Donor.Data.Orgler.AccountMonitoring
{
    public class Search
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        /* Method name: getNewAccountSearchResults
         * Input Parameters: An object of NewAccountsInputModel class
         * Output Parameters: A list of NewAccountSearchMapper class which is a mapper class for the search results
         * Purpose: This method calls the various components of the Data layer to execute the search for new accounts and returns the results back to te service */
        public IList<NewAccountSearchMapper> getNewAccountSearchResults(NewAccountsInputModel newAccountInputModel)
        {
            //Instantiate an object of the Repository class
            Repository rp = new Repository("TDOrglerEF");

            //Get the query for new account search based on the input provided
            string Qry = Data.SQL.Orgler.AccountMonitoring.SearchSQL.getNewAccountSearchResultsSQL(newAccountInputModel);

            //execute the search query and return back the results
            var SearchResults = rp.ExecuteSqlQuery<NewAccountSearchMapper>(Qry).ToList();
            return SearchResults;
        }
        /* Method name: getTopOrgsSearchResults
         * Input Parameters: An object of TopOrgsInputModel class
         * Output Parameters: A list of TopOrgsMapper class which is a mapper class for the search results
         * Purpose: This method calls the various components of the Data layer to execute the search for organization and returns the results back to te service */
        public IList<TopOrgsMapper> getTopOrgsSearchResults(TopOrgsInputModel topOrgsInputModel)
        {
            //Instantiate an object of the Repository class
            Repository rp = new Repository("TDOrglerEF");

            //Get the query for top organization search based on the input provided
            string Qry = Data.SQL.Orgler.AccountMonitoring.SearchSQL.getTopOrgsSearchResultsSQL(topOrgsInputModel);

            //execute the search query and return back the results
            var SearchResults = rp.ExecuteSqlQuery<TopOrgsMapper>(Qry).ToList();
            return SearchResults;
        }
    }
}
