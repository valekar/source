using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Orgler.Upload;
using ARC.Donor.Data.Entities;

namespace ARC.Donor.Data.Orgler.Upload
{
    public class EosiUpload
    {
        public async Task<IList<EosiUploadOutput>> insertEosi(EosiUploadInput input, string strUserName)
        {
            //Instantiate an object of the Repository class as well as of type CrudOperationOutput
            Repository rep = new Repository("TDOrglerEF");
            CrudOperationOutput crudoperationOutput = new CrudOperationOutput();

            //Get the query and the parameter list in the crud object by calling the respective method from the sql layer
            crudoperationOutput = Data.SQLQueries.Orgler.Upload.EosiUpload.insertEosiSQL(input, strUserName);

            //execute the query using the statetment and the parameters retrieved above. 
            var EosiOutput = await rep.ExecuteStoredProcedureAsync<EosiUploadOutput>(crudoperationOutput.strSPQuery, crudoperationOutput.parameters);

            //return the results back to service
            return (IList<EosiUploadOutput>)EosiOutput;
        }

        public long getMaxSeqKey()
        {
            Repository rep = new Repository("TDOrglerEF");
            string strSPQuery = string.Empty;
            List<long> listSeqKey = new List<long>();
            long strMaxSeqKey = -1;

            try
            {
                listSeqKey = rep.ExecuteSqlQuery<Int64>(Data.SQLQueries.Orgler.Upload.EosiUpload.getMaxSeqKeySQL()).ToList();
                strMaxSeqKey = listSeqKey[0] != null ? listSeqKey[0] : strMaxSeqKey;
                return strMaxSeqKey;
            }
            catch (Exception e)
            {
                return strMaxSeqKey;
            }
        }
    }
}
