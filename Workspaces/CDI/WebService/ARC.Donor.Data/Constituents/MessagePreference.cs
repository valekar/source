using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public class MessagePreference
    {
        public IList<Entities.Constituents.MessagePreference> getMessagePreference(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.MessagePreference>(SQL.Constituents.MessagePreference.getMessagePreferenceSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.AllMessagePreference> getAllMessagePreference(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.AllMessagePreference>(SQL.Constituents.MessagePreference.getAllMessagePreferenceSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.MessagePreferenceOptions> getMessagePreferenceOptions(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.MessagePreferenceOptions>(SQL.Constituents.MessagePreference.getMessagePreferenceOptionsSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.MessagePreferenceOutput> addMessagePreference(ARC.Donor.Data.Entities.Constituents.MessagePreferenceInput msgPrefInput)
        {
            Repository rep = new Repository();

            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.MessagePreference.addMsgPrefParamters(msgPrefInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.MessagePreferenceOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;

        }

        public IList<Entities.Constituents.MessagePreferenceOutput> deleteMessagePreference(ARC.Donor.Data.Entities.Constituents.MessagePreferenceInput msgPrefInput)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.MessagePreference.deleteMsgPrefParamters(msgPrefInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.MessagePreferenceOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.MessagePreferenceOutput> editMessagePreference(ARC.Donor.Data.Entities.Constituents.MessagePreferenceInput msgPrefInput)
        {
            Repository rep = new Repository();
            CrudOperationOutput crudOutput;
            crudOutput = SQL.Constituents.MessagePreference.editMsgPrefParamters(msgPrefInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.MessagePreferenceOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;
        }
    }
}
