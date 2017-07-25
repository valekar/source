using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Orgler.EnterpriseOrgs
{
    public class Summary
    {
        /* Method name: getOrgNAICSDetails
        * Input Parameters: An object containing the Master id for which the naics details need to be retrieved
        * Output Parameters: A list of GetMasterNAICSDetailsOutput class which gives the details of naics codes related to a specific master .
        * Purpose: This method gets all the NAICS details for an input master id */
        public IList<Entities.Orgler.EnterpriseOrgs.SummaryMapper> getSummaryDetails(int NoOfRecs, int PageNum, string strEntOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crud = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crud = SQL.Orgler.EnterpriseOrgs.Summary.getSummarySQL(NoOfRecs, PageNum, strEntOrgId);

            //execute the search query and return back the results
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.SummaryMapper>(crud.strSPQuery, crud.parameters).ToList();
            return AcctLst;
        }

        public IList<Entities.Orgler.EnterpriseOrgs.BridgeCountOutputModel> getBridgeCountDetails(string strEntOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crud = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crud = SQL.Orgler.EnterpriseOrgs.Summary.getBridgeCountSQL(strEntOrgId);

            //execute the search query and return back the results
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.BridgeCountOutputModel>(crud.strSPQuery, crud.parameters).ToList();
            return AcctLst;
        }
    }
}
