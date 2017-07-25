using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public class DoNotContact
    {
        public IList<Entities.Constituents.DoNotContact> getDoNotContact(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.DoNotContact>(SQL.Constituents.DoNotContact.getDoNotContactSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.AllDoNotContact> getAllDoNotContacts(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.AllDoNotContact>(SQL.Constituents.DoNotContact.getDNCShowDetailsSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.DoNotContactOutput> addDoNotContact(ARC.Donor.Data.Entities.Constituents.DoNotContactInput dncInput)
        {
            Repository rep = new Repository();

            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.DoNotContact.addDncParamters(dncInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.DoNotContactOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.DoNotContactOutput> editDoNotContact(ARC.Donor.Data.Entities.Constituents.DoNotContactInput dncInput)
        {
            Repository rep = new Repository();

            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.DoNotContact.editDncParamters(dncInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.DoNotContactOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.DoNotContactOutput> deleteDoNotContact(ARC.Donor.Data.Entities.Constituents.DoNotContactInput dncInput)
        {
            Repository rep = new Repository();

            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.DoNotContact.deleteDncParamters(dncInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.DoNotContactOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;
        }
    }   
}
