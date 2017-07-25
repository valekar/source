using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{

    public interface IAlternateIds
    {
         IList<Entities.Constituents.AlternateIds> getSourceSystemAlternateIds(int NoOfRecs, int PageNum, string id);
         IList<Entities.Constituents.AlternateIds> getSourceSystemAlternateIds(int NoOfRecs, int PageNum, AlternateIdsInput input);
    }

    public class AlternateIds : IAlternateIds
    {

        public IList<Entities.Constituents.AlternateIds> getSourceSystemAlternateIds(int NoOfRecs, int PageNum, string id)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            var AlternateIds = rep.ExecuteSqlQuery<Entities.Constituents.AlternateIds>(SQL.Constituents.AlternateIds.getSourceSystemAlternateIdSQL(NoOfRecs, PageNum, id)).ToList();
            return AlternateIds;
        }

        public IList<Entities.Constituents.AlternateIds> getSourceSystemAlternateIds(int NoOfRecs, int PageNum, AlternateIdsInput input)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository();
            string qry = SQL.Constituents.AlternateIds.getSourceSystemAlternateIdSQL(NoOfRecs, PageNum, input);
            var AlternateIds = rep.ExecuteSqlQuery<Entities.Constituents.AlternateIds>(qry).ToList();
            return AlternateIds;
        }


     
    } 
}
