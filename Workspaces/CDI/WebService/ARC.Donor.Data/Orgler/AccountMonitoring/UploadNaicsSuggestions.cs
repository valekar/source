using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Orgler.AccountMonitoring;

namespace ARC.Donor.Data.Orgler.AccountMonitoring
{
 public class UploadNaicsSuggestions
    {
        /* Method name: insertNaicsSuggestionUploadDetails
        * Input Parameters: An object of UploadNAICSSuggestionsInput class which has the list of values and transaction key.
        * Output Parameters: NULL
        * Purpose: This method is used to execute the stored procedure for uploading naics suggestions */
        public async Task insertNaicsSuggestionUploadDetails(UploadNAICSSuggestionsInput nsu, List<NAICSSuggestionsJsonFileDetailsHelper> ch, long trans_key)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudOutput = SQL.Orgler.AccountMonitoring.UploadNaicsSuggestions.insertUploadNAICSSuggestionsRecords(nsu,trans_key);
            //execute the SP query using the statetment and the parameters retrieved above.
           await rep.ExecuteStoredProcedureAsync(crudOutput.strSPQuery, crudOutput.parameters);
           
        }
        /* Method name: getNaicsSuggestionsUploadTransKeyDetails
     * Input Parameters: User ID to store the trasaction creations.
     * Output Parameters: Transaction key after creating new transactions
     * Purpose: This methods creates new trasactions for uploading naics suggestions */
        public long getNaicsSuggestionsUploadTransKeyDetails(string userId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            long strTransactionKey = -1;
            try
            {
                CrudOperationOutput crudOutput = new CrudOperationOutput();
                crudOutput = SQL.Orgler.AccountMonitoring.UploadNaicsSuggestions.postNaicsSuggestionsUploadCreateTrans(userId);
                //execute the SP query using the statetment and the parameters retrieved above.
                var naicsSuggestionsUploadCreateTransOutput = rep.ExecuteStoredProcedure<UploadCreateTransOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
                strTransactionKey = naicsSuggestionsUploadCreateTransOutput[0] != null ? naicsSuggestionsUploadCreateTransOutput[0].transKey : strTransactionKey;
                return strTransactionKey;
            }
            catch (Exception e)
            {
                return strTransactionKey;
            }
        }
        /* Method name: updateNaicsSuggestionUploadDetails
        * Input Parameters: Null
        * Output Parameters: Null
        * Purpose: This methods updates naics suggestions */
        public async Task  updateNaicsSuggestionUploadDetails()
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            crudOutput = SQL.Orgler.AccountMonitoring.UploadNaicsSuggestions.UpdateNAICSSuggestionsParameter();
            await  rep.ExecuteStoredProcedureAsync(crudOutput.strSPQuery, crudOutput.parameters);
        }

        public IList<Entities.Orgler.AccountMonitoring.UploadMetric> getUploadMetricResults(int NoOfRecords, int PageNumber, long transactionKey)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            //Instantiate an object of the Repository class
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQL.Orgler.AccountMonitoring.UploadNaicsSuggestions.getUploadMetricSQL(NoOfRecords, PageNumber, transactionKey);

            //execute the query using the statetment and the parameters retrieved above. 
            var UploadMetricOutput = rep.ExecuteStoredProcedure<Entities.Orgler.AccountMonitoring.UploadMetric>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters).ToList();

            //return the results back to service
            return UploadMetricOutput;
        }
    }
}
