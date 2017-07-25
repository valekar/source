using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Orgler.EnterpriseOrgs
{
    public class Characteristics
    {
        public IList<Entities.Orgler.EnterpriseOrgs.Characteristics> getOrgCharacteristics(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var AcctLst = rep.ExecuteSqlQuery<Entities.Orgler.EnterpriseOrgs.Characteristics>(SQL.Orgler.EnterpriseOrgs.Characteristics.getOrgCharacteristicsSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Orgler.EnterpriseOrgs.Characteristics> getOrgAllCharacteristics(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var AcctLst = rep.ExecuteSqlQuery<Entities.Orgler.EnterpriseOrgs.Characteristics>(SQL.Orgler.EnterpriseOrgs.Characteristics.getOrgAllCharacteristicsSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }


        public IList<Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput> addOrgCharacteristics(Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsInput orgCharInput)
        {
            Repository rep = new Repository("TDOrglerEF");

            CrudOperationOutput crudOutput;
            crudOutput = SQL.Orgler.EnterpriseOrgs.Characteristics.addCharacteristicsParameters(orgCharInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;

        }


        public IList<Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput> editOrgCharacteristics(Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsInput orgCharInput)
        {
            Repository rep = new Repository("TDOrglerEF");

            CrudOperationOutput crudOutput;
            crudOutput = SQL.Orgler.EnterpriseOrgs.Characteristics.editCharacteristicsParameters(orgCharInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;

        }

        public IList<Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput> deleteOrgCharacteristics(Data.Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsInput orgCharInput)
        {
            Repository rep = new Repository("TDOrglerEF");

            CrudOperationOutput crudOutput;
            crudOutput = SQL.Orgler.EnterpriseOrgs.Characteristics.deleteCharacteristicsParameters(orgCharInput);
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.OrgCharacteristicsOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
            return AcctLst;

        }

    }
}
