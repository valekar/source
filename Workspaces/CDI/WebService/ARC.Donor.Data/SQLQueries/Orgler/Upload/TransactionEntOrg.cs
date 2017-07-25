using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Orgler.Upload;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQLQueries.Orgler.Upload
{
    public class TransactionEntOrg
    {
        public static CrudOperationOutput insertTransactionEntOrg(TransactionEntOrgInput input)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crud = new CrudOperationOutput();

            //populate the count of number of input parameters
            int intNumberOfInputParameters = 4;

            //create a list of all the output parameters
            List<string> listOutputParameters = new List<string> { "o_trans_out" };

            //create the SP query query using the input parameter count and output parameters and populate it to the crud object
            crud.strSPQuery = SPHelper.createSPQuery("arc_orgler_macs.sp_ld_trans_ent_org", intNumberOfInputParameters, listOutputParameters);

            //create a list of parameters that have to be passed to the procedure
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_key", input.transKey, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_id", input.entOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_trans_ent_org_note", input.transNotes, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_appl_src_cd", input.applSrcCd, "IN", TdType.VarChar, 4));

            //populate the parameters to the crud object's parameter property
            crud.parameters = ParamObjects;

            //return the crud object to the calling method
            return crud;
        }
    }
}
