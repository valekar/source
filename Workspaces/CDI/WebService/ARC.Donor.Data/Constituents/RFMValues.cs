using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public interface IRFMValues
    {
        IList<Entities.Constituents.RFMValues> getRFMValues(int NoOfRecs, int PageNum, string id);
       // IList<Entities.Constituents.RFMValues> getRFMValues(int NoOfRecs, int PageNum, RFMValuesInput input);
    }

    public class RFMValues : IRFMValues
    {

        public IList<Entities.Constituents.RFMValues> getRFMValues(int NoOfRecs, int PageNum, string id)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            var AlternateIds = rep.ExecuteSqlQuery<Entities.Constituents.RFMValues>(SQL.Constituents.RFMValues.getRFMValuesSQL(NoOfRecs, PageNum, id)).ToList();
            return AlternateIds;
        }

        //public IList<Entities.Constituents.RFMValues> getRFMValues(int NoOfRecs, int PageNum, RFMValuesInput input)
        //{
        //    //Instantiate an object of the Repository class as well as of type CrudOperationOutput
        //    Repository rep = new Repository();
        //    string qry = SQL.Constituents.RFMValues.getRFMValuesDataSQL(NoOfRecs, PageNum, input);
        //    var AlternateIds = rep.ExecuteSqlQuery<Entities.Constituents.RFMValues>(qry).ToList();
        //    return AlternateIds;
        //}



    }
}
