using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
namespace ARC.Donor.Data.Orgler.EnterpriseOrgs
{
    public class Hierarchy
    {
        public IList<Entities.Orgler.EnterpriseOrgs.OrgHierarchyOutputModel> getHierarchySQLResults(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Hierarchy.getHierarchySQL(NoOfRecords, PageNumber, enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var HierarchyOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.OrgHierarchyOutputModel>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return HierarchyOutput;
        }

        public IList<Entities.Orgler.EnterpriseOrgs.OrganizationHierarchyOutputModel> getHierarchyResults(string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Hierarchy.getHierarchyTreeSQL(enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var HierarchyOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.OrganizationHierarchyOutputModel>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return HierarchyOutput;
        }

        //Method to place updates on the EO hierarchy
        public IList<Entities.Orgler.EnterpriseOrgs.HierarchyUpdateOutput> updateHierarchy(Entities.Orgler.EnterpriseOrgs.HierarchyUpdateInput input)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository();
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Hierarchy.getHierarchyUpdateSQL(input);

            //execute the query using the statetment and the parameters retrieved above. 
            var HierarchyOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.HierarchyUpdateOutput>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return HierarchyOutput;
        }
    }
}
