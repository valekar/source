using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Web.Script.Serialization;
using ARC.Donor.Service.Upload;

namespace ARC.Donor.Service.Orgler.Admin
{
    public class UserTabLevelSecurity
    {
        public void addUserTabLevelSecurity(ARC.Donor.Business.Orgler.Admin.UserTabLevelSecurity userTabLevelSecurity)
        {
            Mapper.CreateMap<ARC.Donor.Business.Orgler.Admin.UserTabLevelSecurity, Data.Entities.Orgler.Admin.UserTabLevelSecurity>();
            var Input = Mapper.Map<ARC.Donor.Business.Orgler.Admin.UserTabLevelSecurity, Data.Entities.Orgler.Admin.UserTabLevelSecurity>(userTabLevelSecurity);
            Data.Orgler.Admin.UserTabLevelSecurity cm = new Data.Orgler.Admin.UserTabLevelSecurity();

            var AcctLst = cm.addUserTabLevelSecurity(Input);
            //if (AcctLst.Status.Equals("Success"))
            //{
            //    writeToJSON(userTabLevelSecurity, "Insert");
            //}
        }
        //private void writeToJSON(ARC.Donor.Business.Orgler.Admin.UserTabLevelSecurity adminInput, string type)
        //{
        //    var serializer = new JavaScriptSerializer();
        //    List<Business.Orgler.Admin.TabSecurityData> tabLevelSecurityListData =
        //        Utility.readJSONTFromFile<List<Business.Orgler.Admin.TabSecurityData>>(System.Configuration.ConfigurationManager.AppSettings["TabLevelSecurityOrglerConfigPath"]);

        //    List<Business.Orgler.Admin.TabSecurityData> newLevelSecurityListData = new List<Business.Orgler.Admin.TabSecurityData>();
        //    if (type == "Insert")
        //    {
        //        Business.Orgler.Admin.TabSecurityData newTabSecurityData = new Business.Orgler.Admin.TabSecurityData();

        //        newTabSecurityData.usr_nm = adminInput.usr_nm;
        //        newTabSecurityData.grp_nm = adminInput.grp_nm;
        //        newTabSecurityData.email_address = adminInput.email_address;
        //        newTabSecurityData.telephone_number = adminInput.telephone_number;
        //        newTabSecurityData.newaccount_tb_access = adminInput.newaccount_tb_access;
        //        newTabSecurityData.topaccount_tb_access = adminInput.topaccount_tb_access;
        //        newTabSecurityData.enterprise_orgs_tb_access = adminInput.enterprise_orgs_tb_access;
        //        newTabSecurityData.constituent_tb_access = adminInput.constituent_tb_access;
        //        newTabSecurityData.transaction_tb_access = adminInput.transaction_tb_access;
        //        newTabSecurityData.admin_tb_access = adminInput.admin_tb_access;
        //        newTabSecurityData.help_tb_access = adminInput.help_tb_access;               
        //        newTabSecurityData.has_merge_unmerge_access = adminInput.has_merge_unmerge_access;
        //        newTabSecurityData.is_approver = adminInput.is_approver;

        //        tabLevelSecurityListData.Add(newTabSecurityData);
        //        newLevelSecurityListData = tabLevelSecurityListData;
        //    }


        //    var serializedResult = serializer.Serialize(newLevelSecurityListData);
        //    string jpath = (System.Configuration.ConfigurationManager.AppSettings["TabLevelSecurityOrglerConfigPath"] ?? "").ToString();

        //    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
        //        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));

        //}
    }
}
