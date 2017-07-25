using ARC.Donor.Data.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Upload
{
    public class ListUpload : Entities.QueryLogger
    {
        public IList<ListUploadOutput> getListUploadDetails(ListOfListUploadSearchInput listUploadInputList)
        {
            Repository rep = new Repository();
            
            List<string> grpCdList = new List<string>();

            List<ListUploadSearch> listUploadSearchList = listUploadInputList.ListUploadSearchInput;
            foreach (ListUploadSearch listUploadSearch in listUploadSearchList)
            {
                grpCdList.Add(listUploadSearch.grp_cd);
            }
            string Qry = SQL.Upload.ListUpload.getListUploadSQL(grpCdList,listUploadInputList.answerLimit);
             this._Query = Qry;
            this._StartTime = DateTime.Now;
            var AcctLst = rep.ExecuteSqlQuery<ListUploadOutput>(Qry).ToList();
             this._EndTime = DateTime.Now;
            return AcctLst;

        }
    }
}
