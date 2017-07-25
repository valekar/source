using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Login;
using ARC.Donor.Data.Entities;

namespace ARC.Donor.Data.Login
{
    public class UserTabLevelSecurity
    {
        /* Method to get the log the user logged in history into the Login History table
         * Input Parameters : LoginHistoryInput object
         * Output Parameter : NA
         */
        public async Task<string> addUserTabLevelSecurity(ARC.Donor.Data.Entities.Login.UserTabLevelSecurity tabLevelSecurityInput)
        {
           Repository rep = new Repository();
           CrudOperationOutput crudOutput =  SQL.Login.UserTabLevelSecurity.addUserTabLevelSecurityParameters(tabLevelSecurityInput);

           var repList = await rep.ExecuteStoredProcedureAsync<UserTabLevelSecuritytOutput>(crudOutput.strSPQuery, crudOutput.parameters);
           return null;
        }
    }
}
