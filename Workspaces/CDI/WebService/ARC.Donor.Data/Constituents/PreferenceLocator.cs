using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{

    public class PreferenceLocator
    {
        public IList<Entities.Constituents.PreferenceLocator> getPreferenceLocator(int NoOfRecs, int PageNum, string masterId)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.PreferenceLocator>(SQL.Constituents.PreferenceLocator.getPreferenceLocatorSQL(NoOfRecs, PageNum, masterId)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.AllPreferenceLocator> getAllPreferenceLocators(int NoOfRecs, int PageNum, string masterId)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.AllPreferenceLocator>(SQL.Constituents.PreferenceLocator.getAllPreferenceLocatorSQL(NoOfRecs, PageNum, masterId)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.PreferenceLocatorOptions> getPreferenceLocatorOptions(int NoOfRecs, int PageNum, string masterId)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.PreferenceLocatorOptions>
                (SQL.Constituents.PreferenceLocator.getPreferenceLocatorOptionsSQL(NoOfRecs, PageNum, masterId)).ToList();
            return AcctLst;
        }


        public IList<Entities.Constituents.PreferenceLocatorOutput> addPreferenceLocator(ARC.Donor.Data.Entities.Constituents.PreferenceLocatorInput prefLocatorInput)
        {
            Repository rep = new Repository();
            
            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.PreferenceLocator.addPrefLocatorParamters(prefLocatorInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.PreferenceLocatorOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;
                  
        }

        public IList<Entities.Constituents.PreferenceLocatorOutput> deletePreferenceLocator(ARC.Donor.Data.Entities.Constituents.PreferenceLocatorInput prefLocatorInput)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.PreferenceLocator.deletePrefLocatorParamters(prefLocatorInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.PreferenceLocatorOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.PreferenceLocatorOutput> editPreferenceLocator(ARC.Donor.Data.Entities.Constituents.PreferenceLocatorInput prefLocatorInput)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.PreferenceLocator.editPrefLocatorParamters(prefLocatorInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.PreferenceLocatorOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;
        }
    }
    
}
