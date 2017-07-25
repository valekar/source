using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
   public class OrgContacts
    {
        public IList<Entities.Constituents.OrgContacts> getOrgContacts(int NoOfRecs, int PageNum, string id)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            var ContactsLst = rep.ExecuteSqlQuery<Entities.Constituents.OrgContacts>(SQL.Constituents.OrgContacts.getOrgContactSQL(NoOfRecs, PageNum, id)).ToList();
            return ContactsLst;
        }
    }
}
