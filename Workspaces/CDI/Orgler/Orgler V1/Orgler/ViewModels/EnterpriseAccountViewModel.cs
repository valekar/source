using Orgler.Models.EnterpriseAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.ViewModels
{
    public class EnterpriseOrgViewModel
    {
        public List<ListEnterpriseOrgInputSearchModel> SearchInput { get; set; }
        public List<EnterpriseOrgOutputSearchResults> objSearchResults { get; set; }      

       
    }
}