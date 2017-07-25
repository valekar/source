using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Login;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Login
{
    class LogUserHistory
    {
        public static void getLoginHistoryParameters(LoginHistoryInput LoginHistoryInput, out string strSPQuery, out List<object> parameters)
        {
            int intNumberOfInputParameters = 4;
            List<string> listOutputParameters = new List<string>();

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.inst_login_hst", intNumberOfInputParameters, listOutputParameters);

            string strUserName = "<not available>";
            string strActiveDirectoryGroupName = string.Empty;
            string strLoginStatus = string.Empty;
            DateTime dateTime = DateTime.Now;
            TdTimestamp tdTimestamp = new TdTimestamp(dateTime);

            if (!string.IsNullOrEmpty(LoginHistoryInput.UserName))
                strUserName = LoginHistoryInput.UserName;
            if (!string.IsNullOrEmpty(LoginHistoryInput.ActiveDirectoryGroupName))
                strActiveDirectoryGroupName = LoginHistoryInput.ActiveDirectoryGroupName;
            if (!string.IsNullOrEmpty(LoginHistoryInput.LoginStatus))
                strLoginStatus = LoginHistoryInput.LoginStatus;

            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("usrNm", strUserName, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("actDrctryGroupNm", strActiveDirectoryGroupName, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("loginAttemptTs", tdTimestamp, "IN", TdType.Timestamp, 3));
            ParamObjects.Add(SPHelper.createTdParameter("loginStat", strLoginStatus, "IN", TdType.VarChar, 20));

            parameters = ParamObjects;
        }
    }
}
