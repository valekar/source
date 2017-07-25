using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Orgler;
using ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Orgler.EnterpriseOrgs
{
    public class Transformations
    {
        public IList<Entities.Orgler.EnterpriseOrgs.TransformationMapper> getTransformationSQLResults(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Transformations.getTransformationSQL(NoOfRecords, PageNumber, enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var TransformationDetailsOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.TransformationMapper>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return TransformationDetailsOutput;
        }
        public IList<Entities.Orgler.EnterpriseOrgs.CDITransformOutputModel> getCDITransformationSQLResults(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository();
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Transformations.getCDITransformationSQL(NoOfRecords, PageNumber, enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var TransformationDetailsOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.CDITransformOutputModel>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return TransformationDetailsOutput;
        }
        public IList<Entities.Orgler.EnterpriseOrgs.StuartTransformOutputModel> getStuartTransformationSQLResults(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository();
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Transformations.getStuartTransformationSQL(NoOfRecords, PageNumber, enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var StuartTransformationOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.StuartTransformOutputModel>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return StuartTransformationOutput;
        }

        public IList<Entities.Orgler.idCounter> getEnterpriseTransformationCountSQLResults(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository();
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Transformations.getEnterpriseTransformationCountSQL(enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var TransformationCountOutput = rep.ExecuteStoredProcedure<Entities.Orgler.idCounter>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return TransformationCountOutput;
        }

        /* Purpose: This method is used to add/remove/delete transformation rules for an enterprise */
        public IList<Entities.Orgler.EnterpriseOrgs.TransformationUpdateOutput> getTransformationUpdate(TransformationUpdateFormatInput input)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Transformations.getTransformationUpdateSQL(input);

            //execute the query using the statetment and the parameters retrieved above. 
            var TransformationOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.TransformationUpdateOutput>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return TransformationOutput;
        }

        //To get the possible affiliation count for the rules configured
        public IList<Entities.Orgler.EnterpriseOrgs.TransformationSmokeTestSummary> getSmokeTestAffiliationCount(string strFilter, string ent_org_id)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput.strSPQuery = Data.SQL.Orgler.EnterpriseOrgs.Transformations.getSmokeTestAffiliationCountSQL(strFilter, ent_org_id);
            crudoperationOutput.parameters = new List<object>();

            //execute the query using the statetment and the parameters retrieved above. 
            var TransformationOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.TransformationSmokeTestSummary>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return TransformationOutput;
        }

        /* Purpose: This method is used to get the potential affiliations for the configured transformation rules */
        public IList<Entities.Orgler.EnterpriseOrgs.TransformationSmokeTestResults> smokeTestTransformationsExport(string strFilter, int recCount, string ent_org_id)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput.strSPQuery = Data.SQL.Orgler.EnterpriseOrgs.Transformations.getSmokeTestAffiliationExportSQL(strFilter, recCount, ent_org_id);
            crudoperationOutput.parameters = new List<object>();
            
            //execute the query using the statetment and the parameters retrieved above. 
            var TransformationOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.TransformationSmokeTestResults>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return TransformationOutput;
        }
    }
}
