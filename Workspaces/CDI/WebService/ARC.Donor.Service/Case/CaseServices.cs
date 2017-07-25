using ARC.Donor.Business.Case;
using ARC.Donor.Data.Case;
using AutoMapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Case
{
    public class CaseServices: QueryLogger
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        public IList<ARC.Donor.Business.Case.CaseOutputSearchResults> getCaseSearchResults(ListCaseInputSearchModel CaseSearchData)
        {
            Mapper.CreateMap<ARC.Donor.Business.Case.CaseInputSearchModel, Data.Entities.Case.CaseInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.Case.ListCaseInputSearchModel, Data.Entities.Case.ListCaseInputSearchModel>();
            Mapper.CreateMap<Data.Entities.Case.CaseOutputSearchResults, ARC.Donor.Business.Case.CaseOutputSearchResults>();
            var Input = Mapper.Map<ARC.Donor.Business.Case.ListCaseInputSearchModel, ARC.Donor.Data.Entities.Case.ListCaseInputSearchModel>(CaseSearchData);
            ARC.Donor.Data.Case.CaseSearchData csd = new ARC.Donor.Data.Case.CaseSearchData();
            
            var searchResults = csd.getCaseSearchResults(Input);
            var result = Mapper.Map<IList<Data.Entities.Case.CaseOutputSearchResults>, IList<ARC.Donor.Business.Case.CaseOutputSearchResults>>(searchResults);
            //**** Added event to catpture Query Execution Time
             var uname = CaseSearchData.CaseInputSearchModel.FirstOrDefault();
             string UserName = "";
             if (uname != null) UserName = uname.UserName;
             OnInsertQueryLogger("Case Search", csd.Query, csd.QueryStartTime, csd.QueryEndTime, UserName);
            return result;
        }

        public IList<ARC.Donor.Business.Case.CreateCaseOutput> createCase(ARC.Donor.Business.Case.CreateCaseInput CreateCaseInput, ARC.Donor.Business.Case.SaveCaseSearchInput SaveCaseSearchInput)
        {
            Mapper.CreateMap<Business.Case.CreateCaseInput, Data.Entities.Case.CreateCaseInput>();
            var Input1 = Mapper.Map<Business.Case.CreateCaseInput, Data.Entities.Case.CreateCaseInput>(CreateCaseInput);
            Mapper.CreateMap<Business.Case.SaveCaseSearchInput, Data.Entities.Case.SaveCaseSearchInput>();
            var Input2 = Mapper.Map<Business.Case.SaveCaseSearchInput, Data.Entities.Case.SaveCaseSearchInput>(SaveCaseSearchInput);
            Data.Case.CaseSearchData gd = new Data.Case.CaseSearchData();
            var AcctLst = gd.createCase(Input1,Input2);
            Mapper.CreateMap<Data.Entities.Case.CreateCaseOutput, Business.Case.CreateCaseOutput>();
            var result = Mapper.Map<IList<Data.Entities.Case.CreateCaseOutput>, IList<Business.Case.CreateCaseOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Case.CaseTransactionDetails> getCaseTransactionDetails(int caseId)
        {
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.getCaseTransactionDetails(caseId);
            Mapper.CreateMap<Data.Entities.Case.CaseTransactionDetails, ARC.Donor.Business.Case.CaseTransactionDetails>();
            var result = Mapper.Map<IList<Data.Entities.Case.CaseTransactionDetails>, IList<Business.Case.CaseTransactionDetails>>(AcctLst);
            return result;
        }

        public IList<Business.Case.CaseLocatorDetails> getCaseLocatorDetails(int caseId)
        {
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.getCaseLocatorDetails(caseId);
            Mapper.CreateMap<Data.Entities.Case.CaseLocatorDetails, ARC.Donor.Business.Case.CaseLocatorDetails>();
            var result = Mapper.Map<IList<Data.Entities.Case.CaseLocatorDetails>, IList<Business.Case.CaseLocatorDetails>>(AcctLst);
            return result;
        }

        public IList<Business.Case.CaseNotesDetails> getCaseNotesDetails(int caseId)
        {
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.getCaseNotesDetails(caseId);
            Mapper.CreateMap<Data.Entities.Case.CaseNotesDetails, ARC.Donor.Business.Case.CaseNotesDetails>();
            var result = Mapper.Map<IList<Data.Entities.Case.CaseNotesDetails>, IList<Business.Case.CaseNotesDetails>>(AcctLst);
            return result;
        }

        public IList<Business.Case.CasePreferenceDetails> getCasePreferenceDetails(int caseId)
        {
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.getCasePreferenceDetails(caseId);
            Mapper.CreateMap<Data.Entities.Case.CasePreferenceDetails, ARC.Donor.Business.Case.CasePreferenceDetails>();
            var result = Mapper.Map<IList<Data.Entities.Case.CasePreferenceDetails>, IList<Business.Case.CasePreferenceDetails>>(AcctLst);
            return result;
        }

        public IList<Business.Case.CreateCaseOutput> updateCase(ARC.Donor.Business.Case.CreateCaseInput CreateCaseInput)
        {
            Mapper.CreateMap<Business.Case.CreateCaseInput, Data.Entities.Case.CreateCaseInput>();
            var input = Mapper.Map<Business.Case.CreateCaseInput, Data.Entities.Case.CreateCaseInput>(CreateCaseInput);
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.updateCase(input);
            Mapper.CreateMap<Data.Entities.Case.CreateCaseOutput, Business.Case.CreateCaseOutput>();
            var result = Mapper.Map<IList<Data.Entities.Case.CreateCaseOutput>, IList<Business.Case.CreateCaseOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Case.DeleteCaseOutput> deleteCase(ARC.Donor.Business.Case.DeleteCaseInput DeleteCaseInput)
        {
            Mapper.CreateMap<Business.Case.DeleteCaseInput, Data.Entities.Case.DeleteCaseInput>();
            var input = Mapper.Map<Business.Case.DeleteCaseInput, Data.Entities.Case.DeleteCaseInput>(DeleteCaseInput);
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.deleteCase(input);
            Mapper.CreateMap<Data.Entities.Case.DeleteCaseOutput, Business.Case.DeleteCaseOutput>();
            var result = Mapper.Map<IList<Data.Entities.Case.DeleteCaseOutput>, IList<Business.Case.DeleteCaseOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Case.CaseNotesOutput> addCaseNotes(ARC.Donor.Business.Case.CaseNotesInput CaseNotesInput)
        {
            Mapper.CreateMap<Business.Case.CaseNotesInput, Data.Entities.Case.CaseNotesInput>();
            var Input = Mapper.Map<Business.Case.CaseNotesInput, Data.Entities.Case.CaseNotesInput>(CaseNotesInput);
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.addCaseNotes(Input);
            Mapper.CreateMap<Data.Entities.Case.CaseNotesOutput, Business.Case.CaseNotesOutput>();
            var result = Mapper.Map<IList<Data.Entities.Case.CaseNotesOutput>, IList<Business.Case.CaseNotesOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Case.CaseNotesOutput> updateCaseNotes(ARC.Donor.Business.Case.CaseNotesInput CaseNotesInput)
        {
            Mapper.CreateMap<Business.Case.CaseNotesInput, Data.Entities.Case.CaseNotesInput>();
            var Input = Mapper.Map<Business.Case.CaseNotesInput, Data.Entities.Case.CaseNotesInput>(CaseNotesInput);
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.updateCaseNotes(Input);
            Mapper.CreateMap<Data.Entities.Case.CaseNotesOutput, Business.Case.CaseNotesOutput>();
            var result = Mapper.Map<IList<Data.Entities.Case.CaseNotesOutput>, IList<Business.Case.CaseNotesOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Case.CaseNotesOutput> deleteCaseNotes(ARC.Donor.Business.Case.CaseNotesInput CaseNotesInput)
        {
            Mapper.CreateMap<Business.Case.CaseNotesInput, Data.Entities.Case.CaseNotesInput>();
            var Input = Mapper.Map<Business.Case.CaseNotesInput, Data.Entities.Case.CaseNotesInput>(CaseNotesInput);
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.deleteCaseNotes(Input);
            Mapper.CreateMap<Data.Entities.Case.CaseNotesOutput, Business.Case.CaseNotesOutput>();
            var result = Mapper.Map<IList<Data.Entities.Case.CaseNotesOutput>, IList<Business.Case.CaseNotesOutput>>(AcctLst);
            return result;
        }

        /*Add,Update and Delete Services for Case Locator*/
        public IList<Business.Case.CaseLocatorOutput> addCaseLocator(ARC.Donor.Business.Case.CaseLocatorInput CaseLocatorInput)
        {
            Mapper.CreateMap<Business.Case.CaseLocatorInput, Data.Entities.Case.CaseLocatorInput>();
            var Input = Mapper.Map<Business.Case.CaseLocatorInput, Data.Entities.Case.CaseLocatorInput>(CaseLocatorInput);
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.addCaseLocator(Input);
            Mapper.CreateMap<Data.Entities.Case.CaseLocatorOutput, Business.Case.CaseLocatorOutput>();
            var result = Mapper.Map<IList<Data.Entities.Case.CaseLocatorOutput>, IList<Business.Case.CaseLocatorOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Case.CaseLocatorOutput> updateCaseLocator(ARC.Donor.Business.Case.CaseLocatorInput CaseLocatorInput)
        {
            Mapper.CreateMap<Business.Case.CaseLocatorInput, Data.Entities.Case.CaseLocatorInput>();
            var Input = Mapper.Map<Business.Case.CaseLocatorInput, Data.Entities.Case.CaseLocatorInput>(CaseLocatorInput);
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.updateCaseLocator(Input);
            Mapper.CreateMap<Data.Entities.Case.CaseLocatorOutput, Business.Case.CaseLocatorOutput>();
            var result = Mapper.Map<IList<Data.Entities.Case.CaseLocatorOutput>, IList<Business.Case.CaseLocatorOutput>>(AcctLst);
            return result;
        }

        public IList<Business.Case.CaseLocatorOutput> deleteCaseLocator(ARC.Donor.Business.Case.CaseLocatorInput CaseLocatorInput)
        {
            Mapper.CreateMap<Business.Case.CaseLocatorInput, Data.Entities.Case.CaseLocatorInput>();
            var Input = Mapper.Map<Business.Case.CaseLocatorInput, Data.Entities.Case.CaseLocatorInput>(CaseLocatorInput);
            Data.Case.CaseSearchData csd = new Data.Case.CaseSearchData();
            var AcctLst = csd.deleteCaseLocator(Input);
            Mapper.CreateMap<Data.Entities.Case.CaseLocatorOutput, Business.Case.CaseLocatorOutput>();
            var result = Mapper.Map<IList<Data.Entities.Case.CaseLocatorOutput>, IList<Business.Case.CaseLocatorOutput>>(AcctLst);
            return result;
        }
    }
}
