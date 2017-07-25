using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
   public class OrgEmailDomain
    {
        public IList<Entities.Constituents.OrgEmailDomain> getOrgEmailDomain(int NoOfRecs, int PageNum, string id)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            var EmailLst = rep.ExecuteSqlQuery<Entities.Constituents.OrgEmailDomain>(SQL.Constituents.OrgEmailDomain.getOrgEmailSQL(NoOfRecs, PageNum, id)).ToList();
            return EmailLst;
        }

        public IList<Entities.Constituents.OrgEmailDomainOutput> addOrgEmailDomainMapping(ARC.Donor.Data.Entities.Constituents.OrgEmailDomainAddInput orgEmailDomainAddInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudOperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudOperationOutput = SQL.Constituents.OrgEmailDomain.getAddOrgEmailDomainMappingParameters(orgEmailDomainAddInput);

            //execute the SP query using the statetment and the parameters retrieved above. 
            var TransactionOutput = rep.ExecuteStoredProcedure<Entities.Constituents.OrgEmailDomainOutput>(crudOperationOutput.strSPQuery, crudOperationOutput.parameters).ToList();

            //return the results back to service
            return TransactionOutput;
        }

        public IList<Entities.Constituents.OrgEmailDomainOutput> deleteOrgEmailDomainMapping(ARC.Donor.Data.Entities.Constituents.OrgEmailDomainDeleteInput orgEmailDomainDeleteInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudOperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudOperationOutput = SQL.Constituents.OrgEmailDomain.getDeleteOrgEmailDomainMappingParameters(orgEmailDomainDeleteInput);

            //execute the SP query using the statetment and the parameters retrieved above. 
            var TransactionOutput = rep.ExecuteStoredProcedure<Entities.Constituents.OrgEmailDomainOutput>(crudOperationOutput.strSPQuery, crudOperationOutput.parameters).ToList();

            //return the results back to service
            return TransactionOutput;
        }
    }
}
