using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public class OrgNaics
    {
        public IList<Entities.Constituents.OrgNaics> getOrgNaics(int NoOfRecs, int PageNum, string id)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            var NaicsLst = rep.ExecuteSqlQuery<Entities.Constituents.OrgNaics>(SQL.Constituents.OrgNaics.getOrgNaicsSQL(NoOfRecs, PageNum, id)).ToList();
            return NaicsLst;
        }

        /* Method name: naicsStatusChange
          * Input Parameters: An object of NAICSStatusChangeInput class which has the list of values for submitting values for status change of naics codes for a master
          * Output Parameters: An object of TransactionResult class which contains the SP query and the parameters required for execution.
          * Purpose: This method is used to change the status(approve, reject or add) of naics codes for a single master */
        public IList<Entities.Orgler.AccountMonitoring.TransactionResult> naicsStatusChange(NAICSStatusUpdateInput naicsStatusCangeInput)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crud = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crud = SQL.Constituents.OrgNaics.getNAICSStatusChangeCodeParameter(naicsStatusCangeInput);

            //execute the SP query using the statetment and the parameters retrieved above. 
            var TransactionOutput = rep.ExecuteStoredProcedure<Entities.Orgler.AccountMonitoring.TransactionResult>(crud.strSPQuery, crud.parameters).ToList();
            return TransactionOutput;
        }
    }
}
