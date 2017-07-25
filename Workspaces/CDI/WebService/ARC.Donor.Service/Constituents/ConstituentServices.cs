using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using ARC.Donor.Business;
using ARC.Donor.Data.Constituents;
using AutoMapper;
namespace ARC.Donor.Service
{
    public class ConstituentServices: QueryLogger
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        public IList<ARC.Donor.Business.ConstituentOutputSearchResults> getConstituentSearchResults(ConstituentInputSearchModel constituentInputSearchData)
        {
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentOutputSearchResults, ARC.Donor.Business.ConstituentOutputSearchResults>();         
            
            ARC.Donor.Data.Constituents.ConstituentSearchResults gd = new ARC.Donor.Data.Constituents.ConstituentSearchResults();
            var AcctLst = gd.getConstituentSearchResults(constituentInputSearchData.masterId.ToString());
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentOutputSearchResults>, IList<ARC.Donor.Business.ConstituentOutputSearchResults>>(AcctLst);
            //**** Added event to catpture Query Execution Time
            OnInsertQueryLogger("Constituent Search", gd.Query, gd.QueryStartTime, gd.QueryEndTime, constituentInputSearchData.LoggedInUser);
            
            return result;
        }


        public IList<ARC.Donor.Business.ConstituentOutputSearchResults> getConstituentAdvSearchResults(ListConstituentInputSearchModel searchInput)
        {
            Mapper.CreateMap<ARC.Donor.Business.ConstituentInputSearchModel, ARC.Donor.Data.Entities.Constituents.ConstituentInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.ListConstituentInputSearchModel, ARC.Donor.Data.Entities.Constituents.ListConstituentInputSearchModel>();
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentOutputSearchResults, ARC.Donor.Business.ConstituentOutputSearchResults>();
            var Input = Mapper.Map<ARC.Donor.Business.ListConstituentInputSearchModel, ARC.Donor.Data.Entities.Constituents.ListConstituentInputSearchModel>(searchInput);
            ConstSearchDat csd = new ConstSearchDat();
            var searchResults = csd.getConstituentSearchResults(Input);
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentOutputSearchResults>, IList<ARC.Donor.Business.ConstituentOutputSearchResults>>(searchResults);
            //**** Added event to catpture Query Execution Time
            var uname = searchInput.ConstituentInputSearchModel.FirstOrDefault();
            string UserName = "";
            if (uname != null) UserName = uname.LoggedInUser;

            OnInsertQueryLogger("Constituent Search", csd.Query, csd.QueryStartTime, csd.QueryEndTime, UserName);
            
            return result;
        }


    }
}