using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Login;
using System.Collections;

namespace ARC.Donor.Data.Login
{
    public class LogUserHistory
    {
        /* Method to get the log the user logged in history into the Login History table
         * Input Parameters : LoginHistoryInput object
         * Output Parameter : NA
         */
        public async Task insertLoginHistory(LoginHistoryInput LoginHistoryInput)
        {

            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Login.LogUserHistory.getLoginHistoryParameters(LoginHistoryInput, out strSPQuery, out listParam);
            
            await rep.ExecuteStoredProcedureAsync(strSPQuery, listParam);
           // return null;

           
        }
    }
}
