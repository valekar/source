using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ARC.Donor.Data.Entities.Case;
using System.Reflection;

namespace ARC.Donor.Data.Case
{
    public class CaseSearchData:Entities.QueryLogger
    {
        public IList<Entities.Case.CaseOutputSearchResults> getCaseSearchResults(ListCaseInputSearchModel searchInput)
        {
            Repository rp = new Repository();
            string Qry = SQL.CaseSQL.getCaseAdvSearchResultsSQL(searchInput);
            this._Query = Qry;
            this._StartTime = DateTime.Now;
            var SearchResults = rp.ExecuteSqlQuery<Entities.Case.CaseOutputSearchResults>(Qry).ToList();
            this._EndTime = DateTime.Now;
            return SearchResults;
        }

        public IList<Entities.Case.CreateCaseOutput> createCase(ARC.Donor.Data.Entities.Case.CreateCaseInput _CreateCaseInput, ARC.Donor.Data.Entities.Case.SaveCaseSearchInput _SaveCaseSearchInput)
        {
            Repository rep = new Repository();
            string RequestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.CaseSQL.getCreateCaseParameters(_CreateCaseInput, RequestType, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Case.CreateCaseOutput>(strSPQuery, listParam).ToList();

            //Chiranjib 13/04/2016 - saveCaseSearch called asynchronously in background thread without blocking main current thread
            if (!string.IsNullOrEmpty(AcctLst.ElementAt(0).o_case_seq.ToString()))
            {
                int varCount = 0;
                foreach(PropertyInfo property in _SaveCaseSearchInput.GetType().GetProperties())
                {
                    if(property.GetValue(_SaveCaseSearchInput, null) != null)  // If the values of any of the parameters is not null then Save Case Search.
                    {
                        varCount++;
                    }
                }
                if (varCount > 1) //the count is always 1 because Const Type will not be null. Hence if it is greater than one then other parameters are present
                {
                    Task.Run(async () =>
                    {
                        await saveCaseSearch(_SaveCaseSearchInput, AcctLst.ElementAt(0).o_case_seq);
                    }).ConfigureAwait(false);              
                }
                
            }
            return AcctLst;
        }

        //Chiranjib 13/04/2016 - Called asynchronously in background thread without blocking main current thread
        public async Task<IList<Entities.Case.SaveCaseSearchOutput>> saveCaseSearch(ARC.Donor.Data.Entities.Case.SaveCaseSearchInput SaveCaseSearchInput,Int64? case_key)
        {
                Repository rep = new Repository();
                string strSPQuery = string.Empty;
                List<object> listParam = new List<object>();
                SQL.CaseSQL.getSaveCaseSearchParameters(SaveCaseSearchInput, case_key, out strSPQuery, out listParam);
                var AcctLst = await rep.ExecuteStoredProcedureAsync<Entities.Case.SaveCaseSearchOutput>(strSPQuery, listParam);
                return (System.Collections.Generic.IList<ARC.Donor.Data.Entities.Case.SaveCaseSearchOutput>)AcctLst;
        }

         /* Methods to get case details
        * Input Parameters : Case Sequence Number
        * Output Parameter : Case Transaction details
        */
        public IList<CaseTransactionDetails> getCaseTransactionDetails(int caseId)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Case.CaseTransactionDetails>(SQL.CaseSQL.getCaseTransactionDetailsSQL(caseId)).ToList();
            return SearchResults;
        }

        public IList<CaseLocatorDetails> getCaseLocatorDetails(int caseId)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Case.CaseLocatorDetails>(SQL.CaseSQL.getCaseLocatorDetailsSQL(caseId)).ToList();
            return SearchResults;
        }

        public IList<CaseNotesDetails> getCaseNotesDetails(int caseId)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Case.CaseNotesDetails>(SQL.CaseSQL.getCaseNotesDetailsSQL(caseId)).ToList();
            return SearchResults;
        }

        public IList<CasePreferenceDetails> getCasePreferenceDetails(int caseId)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Case.CasePreferenceDetails>(SQL.CaseSQL.getCasePreferenceDetailsSQL(caseId)).ToList();
            return SearchResults;
        }

        public IList<Entities.Case.CreateCaseOutput> updateCase(ARC.Donor.Data.Entities.Case.CreateCaseInput CreateCaseInput)
        {
             Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.CaseSQL.getUpdateCaseParameters(CreateCaseInput, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Case.CreateCaseOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Case.DeleteCaseOutput> deleteCase(ARC.Donor.Data.Entities.Case.DeleteCaseInput DeleteCaseInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.CaseSQL.getDeleteCaseParameters(DeleteCaseInput, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Case.DeleteCaseOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Case.CaseNotesOutput> addCaseNotes(ARC.Donor.Data.Entities.Case.CaseNotesInput CaseNotesInput)
        {
             Repository rep = new Repository();
            string strSPQuery = string.Empty;
            string request_action = "add";
            List<object> listParam = new List<object>();
            SQL.CaseSQL.getAddCaseNotesParameters(CaseNotesInput, request_action,out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Case.CaseNotesOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Case.CaseNotesOutput> updateCaseNotes(ARC.Donor.Data.Entities.Case.CaseNotesInput CaseNotesInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            string request_action = "edit";
            List<object> listParam = new List<object>();
            SQL.CaseSQL.getUpdateCaseNotesParameters(CaseNotesInput, request_action, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Case.CaseNotesOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Case.CaseNotesOutput> deleteCaseNotes(ARC.Donor.Data.Entities.Case.CaseNotesInput CaseNotesInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            string request_action = "delete";
            List<object> listParam = new List<object>();
            SQL.CaseSQL.getDeleteCaseNotesParameters(CaseNotesInput, request_action, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Case.CaseNotesOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        /*Add,Update and Delete Data for Case Locator*/
        public IList<Entities.Case.CaseLocatorOutput> addCaseLocator(ARC.Donor.Data.Entities.Case.CaseLocatorInput CaseLocatorInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            string request_action = "insert";
            List<object> listParam = new List<object>();
            SQL.CaseSQL.getWriteCaseLocatorParameters(CaseLocatorInput, request_action, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Case.CaseLocatorOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Case.CaseLocatorOutput> updateCaseLocator(ARC.Donor.Data.Entities.Case.CaseLocatorInput CaseLocatorInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            string request_action = "update";
            List<object> listParam = new List<object>();
            SQL.CaseSQL.getWriteCaseLocatorParameters(CaseLocatorInput, request_action, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Case.CaseLocatorOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Case.CaseLocatorOutput> deleteCaseLocator(ARC.Donor.Data.Entities.Case.CaseLocatorInput CaseLocatorInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            string request_action = "delete";
            List<object> listParam = new List<object>();
            SQL.CaseSQL.getWriteCaseLocatorParameters(CaseLocatorInput, request_action, out strSPQuery, out listParam);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Case.CaseLocatorOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }
    }
}
