using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ARC.Donor.Service.Upload;
using System.Web.Script.Serialization;

namespace ARC.Donor.Service.Admin
{
    public class AdminServices:QueryLogger
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        public IList<ARC.Donor.Business.Admin.Admin> getTabLevelSecurity(Business.Admin.AdminTabSecurityInput adminInput)
        {
            Data.Admin.Admin ad = new Data.Admin.Admin();
            var accList = ad.getTabSecurityDetails(adminInput.NoOfRecs,adminInput.PageNum);
            Mapper.CreateMap<Data.Entities.Admin.Admin, Donor.Business.Admin.Admin>();
            var result = Mapper.Map<IList<Data.Entities.Admin.Admin>, IList<Donor.Business.Admin.Admin>>(accList);

            OnInsertQueryLogger("Admin tabSecurity", ad.Query, ad.QueryStartTime, ad.QueryEndTime, adminInput.loggedInUser);

            return result;

        }


        public ARC.Donor.Business.Admin.AdminTransOutput insertTabLevelSecurity(Business.Admin.AdminPostInput adminInput)
        {
            //Mapper.CreateMap<Data.Entities.Admin.Admin, Donor.Business.Admin.Admin>();
            Mapper.CreateMap<Donor.Business.Admin.Admin, Data.Entities.Admin.Admin>();
            Mapper.CreateMap<Donor.Data.Entities.Admin.AdminTransOutput, Donor.Business.Admin.AdminTransOutput>();


            Donor.Data.Entities.Admin.Admin input = Mapper.Map<Donor.Business.Admin.Admin, Donor.Data.Entities.Admin.Admin>(adminInput.adminInput);

            Data.Admin.Admin ad = new Data.Admin.Admin();
            var accResult = ad.insertTabLevelSecurity(input);
            OnInsertQueryLogger("Admin tabSecurity Insert", ad.Query, ad.QueryStartTime, ad.QueryEndTime, adminInput.loggedInUser);
            ARC.Donor.Business.Admin.AdminTransOutput result =
                Mapper.Map<Donor.Data.Entities.Admin.AdminTransOutput, Donor.Business.Admin.AdminTransOutput>(accResult);


            if (result.o_transOutput.Equals("Success"))
            {
                //writeToJSON();
            }
            return result;
        }

        public ARC.Donor.Business.Admin.AdminTransOutput editTabLevelSecurity(Business.Admin.AdminPostInput adminInput)
        {
            //Mapper.CreateMap<Data.Entities.Admin.Admin, Donor.Business.Admin.Admin>();
            Mapper.CreateMap<Donor.Business.Admin.Admin, Data.Entities.Admin.Admin>();
            Mapper.CreateMap<Donor.Data.Entities.Admin.AdminTransOutput,Donor.Business.Admin.AdminTransOutput>();


            Donor.Data.Entities.Admin.Admin input = Mapper.Map<Donor.Business.Admin.Admin,Donor.Data.Entities.Admin.Admin>(adminInput.adminInput);

             Data.Admin.Admin ad = new Data.Admin.Admin();
             var accResult = ad.editTabLevelSecurity(input);
             OnInsertQueryLogger("Admin tabSecurity Edit", ad.Query, ad.QueryStartTime, ad.QueryEndTime, adminInput.loggedInUser);
             ARC.Donor.Business.Admin.AdminTransOutput result = 
                 Mapper.Map<Donor.Data.Entities.Admin.AdminTransOutput, Donor.Business.Admin.AdminTransOutput>(accResult);


             if (result.o_transOutput.Equals("Success"))
             {
                 //writeToJSON();
             }
             return result;
        }


        public ARC.Donor.Business.Admin.AdminTransOutput deleteTabLevelSecurity(Business.Admin.AdminPostInput adminInput)
        {
            //Mapper.CreateMap<Data.Entities.Admin.Admin, Donor.Business.Admin.Admin>();
            Mapper.CreateMap<Donor.Business.Admin.Admin, Data.Entities.Admin.Admin>();
            Mapper.CreateMap<Donor.Data.Entities.Admin.AdminTransOutput, Donor.Business.Admin.AdminTransOutput>();


            Donor.Data.Entities.Admin.Admin input = Mapper.Map<Donor.Business.Admin.Admin, Donor.Data.Entities.Admin.Admin>(adminInput.adminInput);

            Data.Admin.Admin ad = new Data.Admin.Admin();
            var accResult = ad.deleteTabLevelSecurity(input);
            OnInsertQueryLogger("Admin tabSecurity Delete", ad.Query, ad.QueryStartTime, ad.QueryEndTime, adminInput.loggedInUser);
            ARC.Donor.Business.Admin.AdminTransOutput result =
                Mapper.Map<Donor.Data.Entities.Admin.AdminTransOutput, Donor.Business.Admin.AdminTransOutput>(accResult);


            if (result.o_transOutput.Equals("Success"))
            {
               // writeToJSON();
            }

            return result;

        }


        public ARC.Donor.Business.Admin.AdminTransOutput editLoginTabLevelSecurity(Business.Admin.AdminPostInput adminInput)
        {
            //Mapper.CreateMap<Data.Entities.Admin.Admin, Donor.Business.Admin.Admin>();
            Mapper.CreateMap<Donor.Business.Admin.Admin, Data.Entities.Admin.Admin>();
            Mapper.CreateMap<Donor.Data.Entities.Admin.AdminTransOutput, Donor.Business.Admin.AdminTransOutput>();


            Donor.Data.Entities.Admin.Admin input = Mapper.Map<Donor.Business.Admin.Admin, Donor.Data.Entities.Admin.Admin>(adminInput.adminInput);

            Data.Admin.Admin ad = new Data.Admin.Admin();
            var accResult = ad.editTabLevelSecurity(input);
            OnInsertQueryLogger("Admin tabSecurity Edit", ad.Query, ad.QueryStartTime, ad.QueryEndTime, adminInput.loggedInUser);
            ARC.Donor.Business.Admin.AdminTransOutput result =
            Mapper.Map<Donor.Data.Entities.Admin.AdminTransOutput, Donor.Business.Admin.AdminTransOutput>(accResult);


            return result;
        }



        //private void writeToJSON()
        //{
        //    var serializer = new JavaScriptSerializer();
        //   // List<Business.Admin.TabSecurityData> tabLevelSecurityListData =
        //     //   Utility.readJSONTFromFile<List<Business.Admin.TabSecurityData>>(System.Configuration.ConfigurationManager.AppSettings["TabLevelSecurityConfigPath"]);
        //    Business.Admin.AdminTabSecurityInput arbitrary = new Business.Admin.AdminTabSecurityInput();
        //    var result = getTabLevelSecurity(arbitrary);

        //    List<Business.Admin.Admin> adminList = (List<Business.Admin.Admin>)result;
        //    List<Business.Admin.TabSecurityData> newLevelSecurityListData = new List<Business.Admin.TabSecurityData>();


        //    foreach (Business.Admin.Admin input in adminList)
        //    {
        //        // I did not add other params(grp_nm,email etc) because we would not be able to remove the newly created user

        //        Business.Admin.TabSecurityData newTabSecurityData = new Business.Admin.TabSecurityData();

        //        newTabSecurityData.usr_nm = input.usr_nm;
        //        newTabSecurityData.grp_nm = input.grp_nm;
        //        newTabSecurityData.email_address = input.email_address;
        //        newTabSecurityData.telephone_number = input.telephone_number;
        //        newTabSecurityData.constituent_tb_access = input.constituent_tb_access;
        //        newTabSecurityData.account_tb_access = input.account_tb_access;
        //        newTabSecurityData.transaction_tb_access = input.transaction_tb_access;
        //        newTabSecurityData.case_tb_access = input.case_tb_access;
        //        newTabSecurityData.admin_tb_access = input.admin_tb_access;
        //        newTabSecurityData.enterprise_orgs_tb_access = input.enterprise_orgs_tb_access;
        //        newTabSecurityData.reference_data_tb_access = input.reference_data_tb_access;
        //        newTabSecurityData.upload_tb_access = input.upload_tb_access;
        //        newTabSecurityData.report_tb_access = input.report_tb_access;
        //        newTabSecurityData.utitlity_tb_access = input.utitlity_tb_access;
        //        newTabSecurityData.locator_tab_access = input.locator_tab_access;
        //        newTabSecurityData.help_tb_access = input.help_tb_access;
        //        newTabSecurityData.domn_corctn_access = input.domn_corctn_access;
        //        newTabSecurityData.has_merge_unmerge_access = input.has_merge_unmerge_access;
        //        newTabSecurityData.is_approver = input.is_approver;

        //        newLevelSecurityListData.Add(newTabSecurityData);
        //    }


         
        //    var serializedResult = serializer.Serialize(newLevelSecurityListData);
        //    string jpath = (System.Configuration.ConfigurationManager.AppSettings["TabLevelSecurityConfigPath"] ?? "").ToString();

        //    if (!string.IsNullOrEmpty(jpath)) //Utility.writeJSONToFile(serializedResult, jpath); //Error Checks
        //        System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(serializedResult));

        //}

    }
}
