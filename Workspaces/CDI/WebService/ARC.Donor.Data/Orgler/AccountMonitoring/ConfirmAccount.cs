using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Orgler.AccountMonitoring;
using ARC.Donor.Data.Entities;

namespace ARC.Donor.Data.Orgler.AccountMonitoring
{
    public class ConfirmAccount
    {
        /* Method name: confirmAccount
        * Input Parameters: An object of ConfirmAccountInput class which has the master id of the account which needs to be confirmed along with the user name
        * Output Parameters: A list of TransactionResult class from which we can derive the transaction key as well as the status of the transaction
        * Purpose: This method is used to confirm(close) the master for stewarding */
        public IList<Entities.Orgler.AccountMonitoring.TransactionResult> confirmAccount(ARC.Donor.Data.Entities.Orgler.AccountMonitoring.ConfirmAccountInput confirmAccountInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudOperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudOperationOutput=SQLQueries.Orgler.AccountMonitoring.ConfirmAccount.confirmAccountParameter(confirmAccountInput);

            //execute the SP query using the statetment and the parameters retrieved above. 
            var TransactionOutput = rep.ExecuteStoredProcedure<Entities.Orgler.AccountMonitoring.TransactionResult>(crudOperationOutput.strSPQuery, crudOperationOutput.parameters).ToList();

            //return the results back to service
            return TransactionOutput;
        }
    }
}
