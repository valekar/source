using ARC.Donor.Business.Locator;
using ARC.Donor.Business.LocatorDomain;
using ARC.Donor.Business.LocatorAddress;
using ARC.Donor.Data.Locator;
using AutoMapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ARC.Donor.Service.LocatorModels
{
    public class LocatorSearchModel: QueryLogger
    {
        #region Locator Email
        public IList<ARC.Donor.Business.Locator.LocatorEmailOutputSearchResults> getLocatorEmailSearchResults(ListLocatorEmailInputSearchModel LocatorEmailSearchData)
        {
            Mapper.CreateMap<ARC.Donor.Business.Locator.LocatorEmailInputSearchModel, Data.Entities.Locator.LocatorEmailInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.Locator.ListLocatorEmailInputSearchModel, Data.Entities.Locator.ListlocatorEmailInputSearchModel>();
            Mapper.CreateMap<Data.Entities.Locator.LocatorEmailOutputSearchResults, ARC.Donor.Business.Locator.LocatorEmailOutputSearchResults>();

            var Input = Mapper.Map<ARC.Donor.Business.Locator.ListLocatorEmailInputSearchModel, 
                                   ARC.Donor.Data.Entities.Locator.ListlocatorEmailInputSearchModel>(LocatorEmailSearchData);           
            ARC.Donor.Data.Locator.LocatorEmailSearchData csd = new ARC.Donor.Data.Locator.LocatorEmailSearchData();            

            var searchResults = csd.getLocatorEmailSearchResults(Input);

            var result = Mapper.Map<IList<Data.Entities.Locator.LocatorEmailOutputSearchResults>,
                IList<ARC.Donor.Business.Locator.LocatorEmailOutputSearchResults>>(searchResults);
            //**** Added event to catpture Query Execution Time
            var uname = LocatorEmailSearchData.LocatorEmailInputSearchModel.FirstOrDefault();
            string UserName = "";
            //if (uname != null) UserName = uname.UserName;
            OnInsertQueryLogger("Locator Search", csd.Query, csd.QueryStartTime, csd.QueryEndTime, UserName);
            return result;
        }

        public IList<ARC.Donor.Business.Locator.LocatorEmailOutputSearchResults> getLocatorEmailDetailsByID(LocatorEmailInputSearchModel LocatorEmailSearchData)
        {
            Mapper.CreateMap<ARC.Donor.Business.Locator.LocatorEmailInputSearchModel, Data.Entities.Locator.LocatorEmailInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.Locator.ListLocatorEmailInputSearchModel, Data.Entities.Locator.ListlocatorEmailInputSearchModel>();
            Mapper.CreateMap<Data.Entities.Locator.LocatorEmailOutputSearchResults, ARC.Donor.Business.Locator.LocatorEmailOutputSearchResults>();

            var Input = Mapper.Map<ARC.Donor.Business.Locator.LocatorEmailInputSearchModel,
                                   ARC.Donor.Data.Entities.Locator.LocatorEmailInputSearchModel>(LocatorEmailSearchData);
            ARC.Donor.Data.Locator.LocatorEmailSearchData csd = new ARC.Donor.Data.Locator.LocatorEmailSearchData();

            var searchResults = csd.getLocatorEmailDetailsByID(Input);

            var result = Mapper.Map<IList<Data.Entities.Locator.LocatorEmailOutputSearchResults>,
                IList<ARC.Donor.Business.Locator.LocatorEmailOutputSearchResults>>(searchResults);               
            string UserName = "";
            //if (uname != null) UserName = uname.UserName;
            OnInsertQueryLogger("LocatorEmail Search", csd.Query, csd.QueryStartTime, csd.QueryEndTime, UserName);
            return result;
        }

        public IList<ARC.Donor.Business.Locator.LocatorEmailConstOutputSearchResults> getLocatorEmailConstDetailsByID(LocatorEmailInputSearchModel LocatorEmailSearchData)
        {
            Mapper.CreateMap<ARC.Donor.Business.Locator.LocatorEmailInputSearchModel, Data.Entities.Locator.LocatorEmailInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.Locator.ListLocatorEmailInputSearchModel, Data.Entities.Locator.ListlocatorEmailInputSearchModel>();
            Mapper.CreateMap<Data.Entities.Locator.LocatorEmailConstOutputSearchResults, ARC.Donor.Business.Locator.LocatorEmailConstOutputSearchResults>();

            var Input = Mapper.Map<ARC.Donor.Business.Locator.LocatorEmailInputSearchModel,
                                   ARC.Donor.Data.Entities.Locator.LocatorEmailInputSearchModel>(LocatorEmailSearchData);
            ARC.Donor.Data.Locator.LocatorEmailSearchData csd = new ARC.Donor.Data.Locator.LocatorEmailSearchData();

            var searchResults = csd.getLocatorEmailConstDetailsByID(Input);

            var result = Mapper.Map<IList<Data.Entities.Locator.LocatorEmailConstOutputSearchResults>,
                IList<ARC.Donor.Business.Locator.LocatorEmailConstOutputSearchResults>>(searchResults);
            string UserName = "";
            //if (uname != null) UserName = uname.UserName;
            OnInsertQueryLogger("LocatorEmailConst Search", csd.Query, csd.QueryStartTime, csd.QueryEndTime, UserName);
            return result;
        }

        public IList<Business.Locator.CreateLocatorEmailOutput> updateLocatorEmail(ARC.Donor.Business.Locator.CreateLocatorEmailInput LocatorEmailInput)
        {
            Mapper.CreateMap<Business.Locator.CreateLocatorEmailInput, Data.Entities.Locator.CreateLocatorEmailInput>();
            var Input = Mapper.Map<Business.Locator.CreateLocatorEmailInput, Data.Entities.Locator.CreateLocatorEmailInput>(LocatorEmailInput);
            Data.Locator.LocatorEmailSearchData csd = new Data.Locator.LocatorEmailSearchData();
            var AcctLst = csd.updateLocatorEmail(Input);
            Mapper.CreateMap<Data.Entities.Locator.CreateLocatorEmailOutput, Business.Locator.CreateLocatorEmailOutput>();
            var result = Mapper.Map<IList<Data.Entities.Locator.CreateLocatorEmailOutput>, IList<Business.Locator.CreateLocatorEmailOutput>>(AcctLst);
            return result;
        }

        #endregion

        #region Locator Domain Correction

        public IList<ARC.Donor.Business.LocatorDomain.LocatorDomainOutputSearchResults> getLocatorDomain(ListLocatorDomainInputSearchModel LocatorDomainSearchData)
        {
            Mapper.CreateMap<ARC.Donor.Business.LocatorDomain.LocatorDomainInputSearchModel, Data.Entities.LocatorDomain.LocatorDomainInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.LocatorDomain.ListLocatorDomainInputSearchModel, Data.Entities.LocatorDomain.ListLocatorDomainInputSearchModel>();
            Mapper.CreateMap<Data.Entities.LocatorDomain.LocatorDomainOutputSearchResults, ARC.Donor.Business.LocatorDomain.LocatorDomainOutputSearchResults>();

            var Input = Mapper.Map<ARC.Donor.Business.LocatorDomain.ListLocatorDomainInputSearchModel,
                                   ARC.Donor.Data.Entities.LocatorDomain.ListLocatorDomainInputSearchModel>(LocatorDomainSearchData);


            ARC.Donor.Data.Locator.LocatorDomainSearchData csd = new ARC.Donor.Data.Locator.LocatorDomainSearchData();

            var searchResults = csd.getLocatorEmailDomain(Input);

            var result = Mapper.Map<IList<Data.Entities.LocatorDomain.LocatorDomainOutputSearchResults>,
                IList<ARC.Donor.Business.LocatorDomain.LocatorDomainOutputSearchResults>>(searchResults);
            string UserName = "";
            //if (uname != null) UserName = uname.UserName;
            OnInsertQueryLogger("LocatorEmailDomain Search", csd.Query, csd.QueryStartTime, csd.QueryEndTime, UserName);
            return result;
        }


        public IList<Business.LocatorDomain.CreateLocatorDomainOutput> updateLocatorDomain(ARC.Donor.Business.LocatorDomain.ListLocatorDomainInputSearchModel LocatorDomainInput)
        {
            Mapper.CreateMap<ARC.Donor.Business.LocatorDomain.LocatorDomainInputSearchModel, Data.Entities.LocatorDomain.LocatorDomainInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.LocatorDomain.ListLocatorDomainInputSearchModel, Data.Entities.LocatorDomain.ListLocatorDomainInputSearchModel>();
            Mapper.CreateMap<Data.Entities.LocatorDomain.LocatorDomainOutputSearchResults, ARC.Donor.Business.LocatorDomain.LocatorDomainOutputSearchResults>();

            var Input = Mapper.Map<ARC.Donor.Business.LocatorDomain.ListLocatorDomainInputSearchModel,
                                   ARC.Donor.Data.Entities.LocatorDomain.ListLocatorDomainInputSearchModel>(LocatorDomainInput);


            ARC.Donor.Data.Locator.LocatorDomainSearchData csd = new ARC.Donor.Data.Locator.LocatorDomainSearchData();

            var AcctLst = csd.updateLocatorDomain(Input);
            Mapper.CreateMap<Data.Entities.LocatorDomain.CreateLocatorDomainOutput, Business.LocatorDomain.CreateLocatorDomainOutput>();
            var result = Mapper.Map<IList<Data.Entities.LocatorDomain.CreateLocatorDomainOutput>, IList<Business.LocatorDomain.CreateLocatorDomainOutput>>(AcctLst);
            return result;
        }

        #endregion

        #region Locator Address

        public IList<ARC.Donor.Business.LocatorAddress.LocatorAddressOutputSearchResults> getLocatorAddress(ListLocatorAddressInputSearchModel LocatorAddressSearchData)
        {
            Mapper.CreateMap<ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel, Data.Entities.LocatorAddress.LocatorAddressInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.LocatorAddress.ListLocatorAddressInputSearchModel, Data.Entities.LocatorAddress.ListLocatorAddressInputSearchModel>();
            Mapper.CreateMap<Data.Entities.LocatorAddress.LocatorAddressOutputSearchResults, ARC.Donor.Business.LocatorAddress.LocatorAddressOutputSearchResults>();

            
            var Input = Mapper.Map<ARC.Donor.Business.LocatorAddress.ListLocatorAddressInputSearchModel,
                                   ARC.Donor.Data.Entities.LocatorAddress.ListLocatorAddressInputSearchModel>(LocatorAddressSearchData);

            ARC.Donor.Data.Locator.LocatorAddressSearchData csd = new ARC.Donor.Data.Locator.LocatorAddressSearchData();

           // ARC.Donor.Data.Locator.LocatorAddressSearchData csd = new ARC.Donor.Data.Locator.LocatorAddressSearchData();

            var searchResults = csd.getLocatorAddress(Input);

            var result = Mapper.Map<IList<Data.Entities.LocatorAddress.LocatorAddressOutputSearchResults>,
                IList<ARC.Donor.Business.LocatorAddress.LocatorAddressOutputSearchResults>>(searchResults);
            string UserName = "";
            //if (uname != null) UserName = uname.UserName;
            OnInsertQueryLogger("LocatorEmailDomain Search", csd.Query, csd.QueryStartTime, csd.QueryEndTime, UserName);
            return result;
        }

        
        public IList<ARC.Donor.Business.LocatorAddress.LocatorAddressOutputSearchResults> getLocatorAddressById(LocatorAddressInputSearchModel LocatorAddressSearchData)
        {
            Mapper.CreateMap<ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel, Data.Entities.LocatorAddress.LocatorAddressInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel, Data.Entities.LocatorAddress.LocatorAddressInputSearchModel>();
            Mapper.CreateMap<Data.Entities.LocatorAddress.LocatorAddressOutputSearchResults, ARC.Donor.Business.LocatorAddress.LocatorAddressOutputSearchResults>();

            var Input = Mapper.Map<ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel,
                                   ARC.Donor.Data.Entities.LocatorAddress.LocatorAddressInputSearchModel>(LocatorAddressSearchData);
            ARC.Donor.Data.Locator.LocatorAddressSearchData csd = new ARC.Donor.Data.Locator.LocatorAddressSearchData();

            var searchResults = csd.getLocatorAddressByID(Input);
            

            var result = Mapper.Map<IList<Data.Entities.LocatorAddress.LocatorAddressOutputSearchResults>,
                IList<ARC.Donor.Business.LocatorAddress.LocatorAddressOutputSearchResults>>(searchResults);

             string UserName = "";
            //if (uname != null) UserName = uname.UserName;
            OnInsertQueryLogger("LocatorAddress Search", csd.Query, csd.QueryStartTime, csd.QueryEndTime, UserName);
                      
            return result;
        }

        public IList<ARC.Donor.Business.LocatorAddress.LocatorAddressOutputSearchResults> getLocatorAddressAssessmentById(LocatorAddressInputSearchModel LocatorAddressSearchData)
        {
            Mapper.CreateMap<ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel, Data.Entities.LocatorAddress.LocatorAddressInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel, Data.Entities.LocatorAddress.LocatorAddressInputSearchModel>();
            Mapper.CreateMap<Data.Entities.LocatorAddress.LocatorAddressOutputSearchResults, ARC.Donor.Business.LocatorAddress.LocatorAddressOutputSearchResults>();

            var Input = Mapper.Map<ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel,
                                   ARC.Donor.Data.Entities.LocatorAddress.LocatorAddressInputSearchModel>(LocatorAddressSearchData);
            ARC.Donor.Data.Locator.LocatorAddressSearchData csd = new ARC.Donor.Data.Locator.LocatorAddressSearchData();

            var searchResults = csd.getLocatorAddressAssessmentByID(Input);


            var result = Mapper.Map<IList<Data.Entities.LocatorAddress.LocatorAddressOutputSearchResults>,
                IList<ARC.Donor.Business.LocatorAddress.LocatorAddressOutputSearchResults>>(searchResults);

            string UserName = "";
            //if (uname != null) UserName = uname.UserName;
            OnInsertQueryLogger("LocatorAddress Search", csd.Query, csd.QueryStartTime, csd.QueryEndTime, UserName);

            return result;
        }

        public IList<ARC.Donor.Business.LocatorAddress.LocatorAddressConstituentsOutputSearchResults> getLocatorAddressConstituentsById(LocatorAddressInputSearchModel LocatorAddressSearchData)
        {
            Mapper.CreateMap<ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel, Data.Entities.LocatorAddress.LocatorAddressInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel, Data.Entities.LocatorAddress.LocatorAddressInputSearchModel>();
            Mapper.CreateMap<Data.Entities.LocatorAddress.LocatorAddressConstituentsOutputSearchResults, ARC.Donor.Business.LocatorAddress.LocatorAddressConstituentsOutputSearchResults>();

            var Input = Mapper.Map<ARC.Donor.Business.LocatorAddress.LocatorAddressInputSearchModel,
                                   ARC.Donor.Data.Entities.LocatorAddress.LocatorAddressInputSearchModel>(LocatorAddressSearchData);
            ARC.Donor.Data.Locator.LocatorAddressSearchData csd = new ARC.Donor.Data.Locator.LocatorAddressSearchData();

            var searchResults = csd.getLocatorAddressConstituentsByID(Input);
            var result = Mapper.Map<IList<Data.Entities.LocatorAddress.LocatorAddressConstituentsOutputSearchResults>,
                IList<ARC.Donor.Business.LocatorAddress.LocatorAddressConstituentsOutputSearchResults>>(searchResults);
            string UserName = "";
            //if (uname != null) UserName = uname.UserName;
            OnInsertQueryLogger("LocatorAddressConstituents Search", csd.Query, csd.QueryStartTime, csd.QueryEndTime, UserName);
            
            return result;
        }

        public IList<Business.LocatorAddress.CreateLocatorAddressOutput> updateLocatorAddress(ARC.Donor.Business.LocatorAddress.CreateLocatorAddressInput LocatorAddressInput)
        {
            Mapper.CreateMap<Business.LocatorAddress.CreateLocatorAddressInput, Data.Entities.LocatorAddress.CreateLocatorAddressInput>();
            var Input = Mapper.Map<Business.LocatorAddress.CreateLocatorAddressInput, Data.Entities.LocatorAddress.CreateLocatorAddressInput>(LocatorAddressInput);
            Data.Locator.LocatorAddressSearchData csd = new Data.Locator.LocatorAddressSearchData();
            var AcctLst = csd.updateLocatorAddress(Input);
            Mapper.CreateMap<Data.Entities.LocatorAddress.CreateLocatorAddressOutput, Business.LocatorAddress.CreateLocatorAddressOutput>();
            var result = Mapper.Map<IList<Data.Entities.LocatorAddress.CreateLocatorAddressOutput>, IList<Business.LocatorAddress.CreateLocatorAddressOutput>>(AcctLst);
            return result;
        }

        #endregion

        private Logger log = LogManager.GetCurrentClassLogger();
        public string emailKey { get; set; }
        public string emailAddress { get; set; }
        public string emailCategory { get; set; }
        public bool emailExactMatch { get; set; }
    }


    
}