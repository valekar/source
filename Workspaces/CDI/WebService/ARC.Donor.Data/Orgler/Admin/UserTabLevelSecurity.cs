using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Orgler.Admin
{
    public class UserTabLevelSecurity
    {
        /* Method to get the log the user logged in history into the Login History table
         * Input Parameters : LoginHistoryInput object
         * Output Parameter : NA
         */
        public async Task<string> addUserTabLevelSecurity(ARC.Donor.Data.Entities.Orgler.Admin.UserTabLevelSecurity tabLevelSecurityInput)
        {
            //Repository rep = new Repository("TDOrglerEF");
           // CrudOperationOutput crudoperationOutput = new CrudOperationOutput();
            //string strSPQuery = string.Empty;
            //List<object> listParam = new List<object>();
            //SQL.Orgler.Admin.UserTabLevelSecurity.getAddUserTabLevelSecurityParameters(tabLevelSecurityInput, out strSPQuery, out listParam);

            //var repList = await rep.ExecuteStoredProcedureInputTypeAsync(typeof(string), strSPQuery, listParam);
            //return null;
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.Admin.UserTabLevelSecurity.insertUserProfileDetailsSQL(tabLevelSecurityInput);

            //execute the query using the statetment and the parameters retrieved above. 
            var createUserProfileOutput = rep.ExecuteStoredProcedure<Entities.Orgler.Admin.UserProfileOutput>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return null;
        }
    }
}
