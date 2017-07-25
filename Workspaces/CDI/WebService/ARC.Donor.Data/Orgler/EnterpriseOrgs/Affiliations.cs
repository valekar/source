using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using Teradata.Client.Provider;
using ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs;
using ARC.Donor.Data.Entities.Orgler;
namespace ARC.Donor.Data.Orgler.EnterpriseOrgs
{
    public class Affiliations
    {
        public IList<Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutputModel> getAffiliatedMasterBridgeSQLResults(int NoOfRecords, string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();


            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Affiliations.getAffiliatedMasterBridgeSQL(NoOfRecords, enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var AffiliationOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutputModel>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return AffiliationOutput;
        }

        public IList<Entities.Orgler.EnterpriseOrgs.AffiliationsOutputModel> getAffiliationSQLResults(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();
          

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Affiliations.getAffiliationSQL(NoOfRecords, PageNumber, enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var AffiliationOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.AffiliationsOutputModel>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return AffiliationOutput;
        }
        public IList<Entities.Orgler.EnterpriseOrgs.BridgeOutputModel> getBridgeSQLResults(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Affiliations.getBridgeSQL(NoOfRecords, PageNumber, enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var BridgeOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.BridgeOutputModel>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return BridgeOutput;
        }

        public IList<Entities.Orgler.idCounter> getEnterpriseAffiliationCountSQLResults(List<string> listStringIds)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();


            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Affiliations.getEnterpriseAffiliationCountSQL( listStringIds);

            //execute the query using the statetment and the parameters retrieved above. 
            var AffiliationCountOutput = rep.ExecuteStoredProcedure<Entities.Orgler.idCounter>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return AffiliationCountOutput;
        }
        public IList<Entities.Orgler.bridgeCounter> getEnterpriseBridgeCountSQLResults(List<string> listStringIds)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Affiliations.getEnterpriseBridgeCountCountSQL(listStringIds);

            //execute the query using the statetment and the parameters retrieved above. 
            var BridgeCountOutput = rep.ExecuteStoredProcedure<Entities.Orgler.bridgeCounter>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return BridgeCountOutput;
        }

        public IList<Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeExportOutputModel> getAffiliatedMasterBridgeExportSQLResults(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Affiliations.getAffiliatedMasterBridgeExportSQL(NoOfRecords, PageNumber, enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var AffiliationOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeExportOutputModel>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return AffiliationOutput;
        }

        public IList<Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeSummary> getAffilatedMasterBridgeSummaryResults(string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Affiliations.getAffilatedMasterBridgeSummarySQL(enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var AffiliationOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeSummary>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return AffiliationOutput;
        }

        public IList<Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOrgTypeSummary> getAffilatedMasterBridgeOrgTypesResult(string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Affiliations.getAffilatedMasterBridgeOrgTypesSQL(enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var AffiliationOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOrgTypeSummary>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return AffiliationOutput;
        }

    }
}
