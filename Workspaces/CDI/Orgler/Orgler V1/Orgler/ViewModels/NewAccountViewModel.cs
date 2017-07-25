using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orgler.Models.NewAccount;

namespace Orgler.ViewModels
{
    //View Model to store the consolidated list of inputs needed for the New Account Tab
    public class NewAccountViewModel
    {
   
        public SearchInput SearchInput { get; set; }
     
        public List<SearchResults> objSearchResults { get; set; }
        public List<SearchResults> objFRSearchResults { get; set; }
        public List<SearchResults> objBIOSearchResults { get; set; }
        public List<SearchResults> objPHSSSearchResults { get; set; }

        //Validation Variables
        #region ResultsValidationRegion
        public Boolean boolValidSearch
        {
            get
            {
                if (SearchInput != null)
                {
                    return (!((SearchInput.los == "") && (SearchInput.createdDateFrom == "") && (SearchInput.createdDateTo == "")));
                }
                else
                {
                    return false;
                }
            }
        }

        public Boolean boolNoRecord
        {
            get
            {
                return (objSearchResults != null && objSearchResults.Count == 0 && boolValidSearch);
            }
        }
        #endregion ResultsValidationRegion
    }
}