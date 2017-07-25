using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Orgler.Upload;
using ARC.Donor.Data.Entities;

namespace ARC.Donor.Data.Orgler.Upload
{
    public class AffiliationUpload
    {
        public async Task<IList<AffiliationUploadOutput>> insertAffiliation(AffiliationUploadInput input)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQLQueries.Orgler.Upload.AffiliationUpload.insertAffiliationSQL(input);

            //execute the query using the statetment and the parameters retrieved above. 
            var AffiliationOutput = await rep.ExecuteStoredProcedureAsync<AffiliationUploadOutput>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters);

            //return the results back to service
            return (IList<AffiliationUploadOutput>)AffiliationOutput;
        }
    }
}
