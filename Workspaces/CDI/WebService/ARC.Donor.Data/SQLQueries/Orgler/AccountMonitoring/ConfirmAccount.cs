using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;
using ARC.Donor.Data.Entities.Orgler.AccountMonitoring;
using ARC.Donor.Data.Entities;

namespace ARC.Donor.Data.SQLQueries.Orgler.AccountMonitoring
{
    public class ConfirmAccount
    {
        /* Method name: confirmAccountParameter
        * Input Parameters: An object of ConfirmAccountInput class which has the master id of the account which needs to be confirmed along with the user name
        * Output Parameters: An object of CrudOperationOutput class which contains the SP query and the parameters required for execution.
        * Purpose: This method is used to confirm(close) the master for stewarding */
        public static CrudOperationOutput confirmAccountParameter(ConfirmAccountInput confirmAccountInput)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crud = new CrudOperationOutput();

            //populate the count of number of input parameters
            int intNumberOfInputParameters = 2;

            //create a list of all the output parameters
            List<string> listOutputParameters = new List<string> { "o_trans_key", "o_output_message" };

            //create the SP query query using the input parameter count and output parameters and populate it to the crud object
            crud.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.orgler_confirm_acc", intNumberOfInputParameters, listOutputParameters);

            //create a list of parameters that have to be passed to the procedure
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_mstr_id", confirmAccountInput.master_id, "IN", TdType.BigInt, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", confirmAccountInput.usr_nm, "IN", TdType.VarChar, 100));

            //populate the parameters to the crud object's parameter property
            crud.parameters = ParamObjects;

            //return the crud object to the calling method
            return crud;
        }
    }
}
