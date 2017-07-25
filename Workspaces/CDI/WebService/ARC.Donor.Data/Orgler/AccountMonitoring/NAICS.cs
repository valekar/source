using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Orgler.AccountMonitoring;
using ARC.Donor.Data.Entities;


namespace ARC.Donor.Data.Orgler.AccountMonitoring
{
    public class NAICS
    {
        /* Method name: getOrgNAICSDetails
        * Input Parameters: An object containing the Master id for which the naics details need to be retrieved
        * Output Parameters: A list of GetMasterNAICSDetailsOutput class which gives the details of naics codes related to a specific master .
        * Purpose: This method gets all the NAICS details for an input master id */
        public IList<Entities.Orgler.AccountMonitoring.GetMasterNAICSDetailsOutput> getOrgNAICSDetails(int NoOfRecs, int PageNum, GetMasterNAICSDetailsInput getNAICSInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crud = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crud= SQL.Orgler.AccountMonitoring.NAICS.getNAICSDetails(NoOfRecs, PageNum, getNAICSInput);

            //execute the search query and return back the results
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Orgler.AccountMonitoring.GetMasterNAICSDetailsOutput>(crud.strSPQuery, crud.parameters).ToList();
            return AcctLst;
        }

        /* Method name: getAllNAICSCodes
        * Input Parameters: NA
        * Output Parameters: An object of NAICSCodeMapper class which contains the results from DB.
        * Purpose: This method gets all the NAICS details in hierachy */
        public IList<NAICSCodeMapper> getAllNAICSCodes()
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudOperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudOperationOutput = SQL.Orgler.AccountMonitoring.NAICS.getALLNAICSCodes();

            //execute the search query and return back the results
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Orgler.AccountMonitoring.NAICSCodeMapper>(crudOperationOutput.strSPQuery, crudOperationOutput.parameters).ToList();
            return AcctLst;
        }

        /* Method name: naicsStatusChange
        * Input Parameters: An object of NAICSStatusChangeInput class which has the list of values for submitting values for status change of naics codes for a master
        * Output Parameters: An object of TransactionResult class which contains the SP query and the parameters required for execution.
        * Purpose: This method is used to change the status(approve, reject or add) of naics codes for a single master */
        public IList<Entities.Orgler.AccountMonitoring.TransactionResult> naicsStatusChange(ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSStatusChangeInput naicsStatusCangeInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crud = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crud = SQL.Orgler.AccountMonitoring.NAICS.getNAICSStatusChangeCodeParameter(naicsStatusCangeInput);

            //execute the SP query using the statetment and the parameters retrieved above. 
            var TransactionOutput = rep.ExecuteStoredProcedure<Entities.Orgler.AccountMonitoring.TransactionResult>(crud.strSPQuery, crud.parameters).ToList();
            return TransactionOutput;
        }
    }
}
