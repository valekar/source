using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orgler.Models.TopAccount;

namespace Orgler.ViewModels
{
    public class TopAccountViewModel
    {

        public TopOrgsSearchInput SearchInput { get; set; }
        public List<TopAccountSearchResults> objSearchResults { get; set; }
        public List<TopAccountSearchResults> objFRSearchResults { get; set; }
        public List<TopAccountSearchResults> objBIOSearchResults { get; set; }
        public List<TopAccountSearchResults> objPHSSSearchResults { get; set; }

        //Validation Variables
        #region ResultsValidationRegion
        public Boolean boolValidSearch
        {
            get
            {
                if (SearchInput != null)
                {
                    return (!((SearchInput.los == "") && (SearchInput.naics_cd == "") && (SearchInput.rfm_scr == "")));
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
        #endregion
    }
}