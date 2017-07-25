using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using ARC.Donor.Data.SQL.Orgler.Admin;
namespace ARC.Donor.Data.Orgler.Admin
{
    public class UserProfile
    {
        public IList<Entities.Orgler.Admin.UserProfile> getUserProfileDetails(int NoOfRecs, int PageNum)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crud = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crud = SQL.Orgler.Admin.UserProfile.getUserProfileSQL(NoOfRecs, PageNum);

            //execute the search query and return back the results
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Orgler.Admin.UserProfile>(crud.strSPQuery, crud.parameters).ToList();
            return AcctLst;
        }
        public IList<Entities.Orgler.Admin.UserProfileOutput> insertUserProfileDetails(Data.Entities.Orgler.Admin.UserProfile userProfileInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.Admin.UserProfile.insertUserProfileDetailsSQL(userProfileInput);

            //execute the query using the statetment and the parameters retrieved above. 
            var createUserProfileOutput = rep.ExecuteStoredProcedure<Entities.Orgler.Admin.UserProfileOutput>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return createUserProfileOutput;
        }
        public IList<Entities.Orgler.Admin.UserProfileOutput> editUserProfileDetails(Data.Entities.Orgler.Admin.UserProfile userProfileInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.Admin.UserProfile.userProfileCRUDSQL(userProfileInput,"Update");

            //execute the query using the statetment and the parameters retrieved above. 
            var createUserProfileOutput = rep.ExecuteStoredProcedure<Entities.Orgler.Admin.UserProfileOutput>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return createUserProfileOutput;
        }

        public IList<Entities.Orgler.Admin.UserProfileOutput> deleteUserProfile(Data.Entities.Orgler.Admin.UserProfile userProfileInput)
        {
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.Admin.UserProfile.userProfileCRUDSQL(userProfileInput, "Delete");

            //execute the query using the statetment and the parameters retrieved above. 
            var createUserProfileOutput = rep.ExecuteStoredProcedure<Entities.Orgler.Admin.UserProfileOutput>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return createUserProfileOutput;

        }
        /* Method to get the log the user logged in history into the Login History table
        * Input Parameters : LoginHistoryInput object
        * Output Parameter : NA
        */
        public async Task insertLoginHistory(Entities.Orgler.Admin.LoginHistoryInput LoginHistoryInput)
        {

            Repository rep = new Repository("TDOrglerEF");
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Orgler.Admin.UserProfile.getLoginHistoryParameters(LoginHistoryInput, out strSPQuery, out listParam);

            await rep.ExecuteStoredProcedureAsync(strSPQuery, listParam);
            // return null;


        }
    }
}
