using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Orgler.Upload;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQLQueries.Orgler.Upload
{
    public class Transaction
    {
        public static CrudOperationOutput insertTransaction(TransInput input)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crud = new CrudOperationOutput();

            //populate the count of number of input parameters
            int intNumberOfInputParameters = 7;

            //create a list of all the output parameters
            List<string> listOutputParameters = new List<string> { "transKey", "transOutput" };

            //create the SP query query using the input parameter count and output parameters and populate it to the crud object
            crud.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_inst_trans", intNumberOfInputParameters, listOutputParameters);

            //create a list of parameters that have to be passed to the procedure
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("typ", input.typ, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("subTyp", input.subTyp, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("actionType", DBNull.Value, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("transStat", input.transStat, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("transNotes", input.transNotes, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("userId", input.userId, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("caseSeqNum", DBNull.Value, "IN", TdType.BigInt, 0));

            //populate the parameters to the crud object's parameter property
            crud.parameters = ParamObjects;

            //return the crud object to the calling method
            return crud;
        }
    }
}
