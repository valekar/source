using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs;
namespace ARC.Donor.Data.Orgler.EnterpriseOrgs
{
    public class Tags
    {

        public IList<Entities.Orgler.EnterpriseOrgs.TagOutputModel> getTagsSQLResults(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Tags.getTagsSQL(NoOfRecords, PageNumber, enterpriseOrgId);

            //execute the query using the statetment and the parameters retrieved above. 
            var TagsOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.TagOutputModel>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return TagsOutput;
        }

        /* Purpose: This method is used to remove/add tags to an enterprise */
        public IList<Entities.Orgler.EnterpriseOrgs.TagUpdateOutputModel> updateTags(TagUpdateInputModel input)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Tags.updateTagsSQL(input);

            //execute the query using the statetment and the parameters retrieved above. 
            var TagsOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.TagUpdateOutputModel>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return TagsOutput;
        }

        /* Purpose: This method is used to fetch all the active tags */
        public IList<Entities.Orgler.EnterpriseOrgs.TagDDList> getTagDDList()
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.EnterpriseOrgs.Tags.getTagDDListSQL();

            //execute the query using the statetment and the parameters retrieved above. 
            var TagsOutput = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.TagDDList>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return TagsOutput;
        }
    }
}
