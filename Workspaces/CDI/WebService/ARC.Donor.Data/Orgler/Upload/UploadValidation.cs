using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Orgler.Upload;
using ARC.Donor.Data.Entities;

namespace ARC.Donor.Data.Orgler.Upload
{
    public class UploadValidation
    {
        //Validate Enterprise Ids
        public async Task<List<string>> getValidEnterpriseIds(List<string> ltInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQLQueries.Orgler.Upload.UploadValidation.getValidEnterpriseIdsSQL(ltInput);

            //execute the query using the statetment and the parameters retrieved above. 
            var resOutput = await rep.ExecuteStoredProcedureAsync<string>(crudoperationOutput.strSPQuery, new List<object>());

            //return the results back to service
            return (List<string>)resOutput;
        }

        //Validate Master Ids
        public async Task<List<string>> getValidMasterIds(List<string> ltInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQLQueries.Orgler.Upload.UploadValidation.getValidMasterIdsSQL(ltInput);

            //execute the query using the statetment and the parameters retrieved above. 
            var resOutput = await rep.ExecuteStoredProcedureAsync<string>(crudoperationOutput.strSPQuery, new List<object>());

            //return the results back to service
            return (List<string>)resOutput;
        }

        //Validate Naics Codes
        public async Task<List<string>> getValidNaicsCode(List<string> ltInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQLQueries.Orgler.Upload.UploadValidation.getValidNaicsCodeSQL(ltInput);

            //execute the query using the statetment and the parameters retrieved above. 
            var resOutput = await rep.ExecuteStoredProcedureAsync<string>(crudoperationOutput.strSPQuery, new List<object>());

            //return the results back to service
            return (List<string>)resOutput;
        }

        //Validate Characteristic Types
        public async Task<List<string>> getValidCharacteristicType(List<string> ltInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQLQueries.Orgler.Upload.UploadValidation.getValidCharacteristicTypeSQL(ltInput);

            //execute the query using the statetment and the parameters retrieved above. 
            var resOutput = await rep.ExecuteStoredProcedureAsync<string>(crudoperationOutput.strSPQuery, new List<object>());

            //return the results back to service
            return (List<string>)resOutput;
        }

        //Validate Enterprise Names
        public async Task<List<string>> getValidEnterpriseNames(List<string> ltInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQLQueries.Orgler.Upload.UploadValidation.getValidEnterpriseNamesSQL(ltInput);

            //execute the query using the statetment and the parameters retrieved above. 
            var resOutput = await rep.ExecuteStoredProcedureAsync<string>(crudoperationOutput.strSPQuery, new List<object>());

            //return the results back to service
            return (List<string>)resOutput;
        }

        //Validate Tags
        public async Task<List<string>> getValidTags(List<string> ltInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQLQueries.Orgler.Upload.UploadValidation.getValidTagsSQL(ltInput);

            //execute the query using the statetment and the parameters retrieved above. 
            var resOutput = await rep.ExecuteStoredProcedureAsync<string>(crudoperationOutput.strSPQuery, new List<object>());

            //return the results back to service
            return (List<string>)resOutput;
        }
    }
}
