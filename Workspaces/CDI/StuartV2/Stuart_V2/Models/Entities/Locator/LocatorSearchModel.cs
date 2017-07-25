using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Locator
{
    public class LocatorEmailInputSearchModel
    {
        public string LocEmailId { get; set; }
        public string LocEmailKey { get; set; }
        public string IntAssessCode { get; set; }
        public string ExtAssessCode { get; set; }
        public string ExactMatch { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string LoggedInUser { get; set; }
        

        public LocatorEmailInputSearchModel()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            LoggedInUser = p.GetUserName(); //p.Identity.Name;
        }
    }

    public class ListLocatorEmailInputSearchModel
    {
        public List<LocatorEmailInputSearchModel> LocatorEmailInputSearchModel { get; set; }
    }



    public class LocatorAddressInputSearchModel
    {
        public string LocAddrKey { get; set; }
        public string LocAddressLine { get; set; }
        public string LocAddressLine2 { get; set; }
        public string LocCity { get; set; }
        public string LocState { get; set; }
        public string LocZip { get; set; }
        public string LocZip4 { get; set; }
        public string LocDelType { get; set; }
        public string LocDelCode { get; set; }
        public string LocAssessCode { get; set; }
        public string code_category { get; set; }
        public string LoggedInUser { get; set; }


        public LocatorAddressInputSearchModel()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            LoggedInUser = p.GetUserName(); //p.Identity.Name;
        }
    }

    public class ListLocatorAddressInputSearchModel
    {
        public List<LocatorAddressInputSearchModel> LocatorAddressInputSearchModel { get; set; }
    }


    public class LocatorDomainInputSearchModel
    {
        public string LocValidDomain { get; set; }
        public string LocInvalidDomain { get; set; }
        public string LocStatus { get; set; }       
        public string LoggedInUser { get; set; }
        public string email_domain_map_key { get; set; }
        public string status { get; set; }


        public LocatorDomainInputSearchModel()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            LoggedInUser = p.GetUserName(); //p.Identity.Name;
        }
    }

    public class ListLocatorDomainInputSearchModel
    {
        public List<LocatorDomainInputSearchModel> LocatorDomainInputSearchModel { get; set; }
    }


}