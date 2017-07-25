using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NLog;
using System.Web.Script.Serialization;
using ARC.Donor.Service.Upload;

namespace ARC.Donor.Service.Orgler.Admin
{
  public  class UserProfile
    {

        public IList<ARC.Donor.Business.Orgler.Admin.UserProfile> getUserProfile(ARC.Donor.Business.Orgler.Admin.AdminTabSecurityInput adminInput)
        {
            //Map the various buisness objects and data layer objects using the Mapper class          
            Mapper.CreateMap<Data.Entities.Orgler.Admin.UserProfile, Business.Orgler.Admin.UserProfile>();

            //Instantiate the data layer object for confirm functionality
            Data.Orgler.Admin.UserProfile userProfile = new Data.Orgler.Admin.UserProfile();

            //call the data layer method to get NAICS details from the database
            var userProfileDetails = userProfile.getUserProfileDetails(adminInput.NoOfRecs,adminInput.PageNum);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.Admin.UserProfile>, IList<Business.Orgler.Admin.UserProfile>>(userProfileDetails);

            //return the results back to the controller
            return result;
        }
        public IList<ARC.Donor.Business.Orgler.Admin.UserProfileOutput> insertUserProfile(Business.Orgler.Admin.UserProfile userProfileInput)
        {

            Mapper.CreateMap<Business.Orgler.Admin.UserProfile, Data.Entities.Orgler.Admin.UserProfile>();
            var Input = Mapper.Map<Business.Orgler.Admin.UserProfile, Data.Entities.Orgler.Admin.UserProfile>(userProfileInput);
            ARC.Donor.Data.Orgler.Admin.UserProfile userprofile = new ARC.Donor.Data.Orgler.Admin.UserProfile();
           var newUserProfile= userprofile.insertUserProfileDetails(Input);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.Admin.UserProfileOutput>, IList<Business.Orgler.Admin.UserProfileOutput>>(newUserProfile);

            //return the results back to the controller
            return result;

        }
        public IList<ARC.Donor.Business.Orgler.Admin.UserProfileOutput> editUserProfile(ARC.Donor.Business.Orgler.Admin.AdminPostInput adminInput)
        {

            Mapper.CreateMap<Business.Orgler.Admin.UserProfile,Data.Entities.Orgler.Admin.UserProfile>();
            Mapper.CreateMap<Data.Entities.Orgler.Admin.UserProfileOutput, Business.Orgler.Admin.UserProfileOutput>();

            var Input = Mapper.Map<Business.Orgler.Admin.UserProfile, Data.Entities.Orgler.Admin.UserProfile>(adminInput.adminInput);
            ARC.Donor.Data.Orgler.Admin.UserProfile userprofile = new ARC.Donor.Data.Orgler.Admin.UserProfile();
            var newUserProfile = userprofile.editUserProfileDetails(Input);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.Admin.UserProfileOutput>, IList<Business.Orgler.Admin.UserProfileOutput>>(newUserProfile);
            if (result[0].o_transOutput.Contains("Success"))
            {
                //writeToJSON(adminInput.adminInput, "Update");
            }
            //return the results back to the controller
            return result;

        }
        public IList<ARC.Donor.Business.Orgler.Admin.UserProfileOutput> deleteUserProfile(ARC.Donor.Business.Orgler.Admin.AdminPostInput adminInput)
        {
            Mapper.CreateMap<Business.Orgler.Admin.UserProfile, Data.Entities.Orgler.Admin.UserProfile>();
            Mapper.CreateMap<Data.Entities.Orgler.Admin.UserProfileOutput, Business.Orgler.Admin.UserProfileOutput>();

            var Input = Mapper.Map<Business.Orgler.Admin.UserProfile, Data.Entities.Orgler.Admin.UserProfile>(adminInput.adminInput);
            ARC.Donor.Data.Orgler.Admin.UserProfile userprofile = new ARC.Donor.Data.Orgler.Admin.UserProfile();
            var newUserProfile = userprofile.deleteUserProfile(Input);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.Admin.UserProfileOutput>, IList<Business.Orgler.Admin.UserProfileOutput>>(newUserProfile);

            //return the results back to the controller
           
            if (result[0].o_transOutput.Contains("Success"))
            {
                //writeToJSON(adminInput.adminInput, "Delete");
            }

            return result;
        }

        public void insertLoginHistory(ARC.Donor.Business.Orgler.Admin.LoginHistoryInput LoginHistoryInput)
        {
            Mapper.CreateMap<Business.Orgler.Admin.LoginHistoryInput, Data.Entities.Orgler.Admin.LoginHistoryInput>();
            var Input = Mapper.Map<Business.Orgler.Admin.LoginHistoryInput, Data.Entities.Orgler.Admin.LoginHistoryInput>(LoginHistoryInput);
            Data.Orgler.Admin.UserProfile cm = new ARC.Donor.Data.Orgler.Admin.UserProfile();

            var AcctLst = cm.insertLoginHistory(Input);
        }


        //private void writeToJSON(ARC.Donor.Business.Orgler.Admin.UserProfile adminInput, string type)
        //{
        //    var serializer = new JavaScriptSerializer();
        //    List<Business.Orgler.Admin.TabSecurityData> tabLevelSecurityListData =
        //        Utility.readJSONTFromFile<List<Business.Orgler.Admin.TabSecurityData>>(System.Configuration.ConfigurationManager.AppSettings["TabLevelSecurityOrglerConfigPath"]);

        //    List<Business.Orgler.Admin.TabSecurityData> newLevelSecurityListData = new List<Business.Orgler.Admin.TabSecurityData>();

        //    if (type == "Update")
        //    {
        //        foreach (Business.Orgler.Admin.TabSecurityData input in tabLevelSecurityListData)
        //        {
        //            // I did not add other params(grp_nm,email etc) because we would not be able to remove the newly created user
        //            if (input.usr_nm.Equals(adminInput.usr_nm))
        //            {
        //                Business.Orgler.Admin.TabSecurityData newTabSecurityData = new Business.Orgler.Admin.TabSecurityData();

        //                newTabSecurityData.usr_nm = adminInput.usr_nm;
        //                newTabSecurityData.grp_nm = adminInput.grp_nm;
        //                newTabSecurityData.email_address = adminInput.email_address;
        //                newTabSecurityData.telephone_number = adminInput.telephone_number;
        //                newTabSecurityData.newaccount_tb_access = adminInput.newaccount_tb_access;
        //                newTabSecurityData.topaccount_tb_access = adminInput.topaccount_tb_access;
        //                newTabSecurityData.enterprise_orgs_tb_access = adminInput.enterprise_orgs_tb_access;
        //                newTabSecurityData.constituent_tb_access = adminInput.constituent_tb_access;
        //                newTabSecurityData.transaction_tb_access = adminInput.transaction_tb_access;                       
        //                newTabSecurityData.admin_tb_access = adminInput.admin_tb_access;                       
        //                newTabSecurityData.help_tb_access = adminInput.help_tb_access;                       
        //                newTabSecurityData.has_merge_unmerge_access = adminInput.has_merge_unmerge_access;
        //                newTabSecurityData.is_approver = adminInput.is_approver;
        //                newLevelSecurityListData.Add(newTabSecurityData);
        //            }
        //            else
        //            {
        //                newLevelSecurityListData.Add(input);
        //            }
        //        }
        //    }
        //    else if (type == "Delete")
        //    {
        //        /* Business.Admin.TabSecurityData found =  tabLevelSecurityListData.Find(x => x.usr_nm.Equals(adminInput.usr_nm));
        //         if (found != null)
        //         {
        //             tabLevelSecurityListData.Remove(found);
        //             newLevelSecurityListData = tabLevelSecurityListData;
        //         }*/
        //        foreach (Business.Orgler.Admin.TabSecurityData input in tabLevelSecurityListData)
        //        {

        //            if (input.usr_nm.Equals(adminInput.usr_nm))
        //            {
        //                // do nothing
        //            }
        //            else
        //            {
        //                newLevelSecurityListData.Add(input);
        //            }
        //        }

        //    }
        //    else if (type == "Insert")
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
