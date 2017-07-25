using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.SQL.Upload
{
    class ListUpload
    {
        static readonly string listUploadSearchQry = "SELECT TOP {1} * FROM dw_stuart_vws.strx_upld_srch WHERE grp_cd IN ({0});";

        public static string getListUploadSQL(List<string> grpCdList,string answerLimit)
        {
            string grpCds = "";
            foreach (string grdCd in grpCdList)
            {
                grpCds += '\''+grdCd+'\''+ ",";
            }
            grpCds = grpCds.Remove(grpCds.Length -1);
            return string.Format(listUploadSearchQry, grpCds, answerLimit);
        }

    }
}
