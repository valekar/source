using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Orgler.EnterpriseOrgs
{
    public class RFM
    {
        public IList<Entities.Orgler.EnterpriseOrgs.RFMDetails> getRFMDetails(int NoOfRecs, int PageNum, string strEntOrgId)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crud = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crud = SQL.Orgler.EnterpriseOrgs.RFM.getRFMSQL(NoOfRecs, PageNum, strEntOrgId);

            //execute the search query and return back the results
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.RFMDetails>(crud.strSPQuery, crud.parameters).ToList();
            return AcctLst;
        }
    }
}
