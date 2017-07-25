using System;
using AutoMapper;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Business.Orgler.AccountMonitoring;

namespace ARC.Donor.Service.Orgler.AccountMonitoring
{
    public class AccountServices
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        /* Method name: getNewAccountSearchResults
         * Input Parameters: An object of NewAccountsInputModel class
         * Output Parameters: A list of NewAccountsOutputModel class  
         * Purpose: This method gets the search results for new account */
        public IList<NewAccountsOutputModel> getNewAccountSearchResults(NewAccountsInputModel newAccountInputData)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<ARC.Donor.Business.Orgler.AccountMonitoring.NewAccountsInputModel, Data.Entities.Orgler.AccountMonitoring.NewAccountsInputModel>();
            Mapper.CreateMap<Data.Entities.Orgler.listString, ARC.Donor.Business.Orgler.listString>();
            Mapper.CreateMap<Data.Entities.Orgler.AccountMonitoring.NewAccountsOutputModel, ARC.Donor.Business.Orgler.AccountMonitoring.NewAccountsOutputModel>();
            
            //map the input from buisness object to the data layer object
            var Input = Mapper.Map<ARC.Donor.Business.Orgler.AccountMonitoring.NewAccountsInputModel, ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NewAccountsInputModel>(newAccountInputData);
            
            //Instantiate the data layer object for search functionality
            ARC.Donor.Data.Orgler.AccountMonitoring.Search csd = new ARC.Donor.Data.Orgler.AccountMonitoring.Search();
            
            //call the data layer method to get search results from DB
            var searchMappingResults = csd.getNewAccountSearchResults(Input);

            //convert the mapper results to output format 
            var searchResults = convertNewAccountMapperToOutputModel(searchMappingResults);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.AccountMonitoring.NewAccountsOutputModel>, IList<ARC.Donor.Business.Orgler.AccountMonitoring.NewAccountsOutputModel>>(searchResults);

            //return the results back to the controller
            return result;
            
        }

        /* Method name: convertNewAccountMapperToOutputModel
        * Input Parameters: A list of NewAccountSearchMapper class
        * Output Parameters: A list of NewAccountsOutputModel class 
        * Purpose: This method is used to convert the DB results in the specific class for new account search results*/
        public IList<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NewAccountsOutputModel> convertNewAccountMapperToOutputModel(IList<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NewAccountSearchMapper> listNewAccountMapper)
        {
            //Instantiate a list of output class
            List<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NewAccountsOutputModel> listNewAccountOutputMapper = new List<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NewAccountsOutputModel>();
            //for each mapper record
            foreach (ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NewAccountSearchMapper newAccountMapper in listNewAccountMapper)
            {
                string strAddress = string.Empty;

                //create lists for various NAICS strings
                List<ARC.Donor.Data.Entities.Orgler.listString> listNAICSString = new List<ARC.Donor.Data.Entities.Orgler.listString>();
                List<ARC.Donor.Data.Entities.Orgler.listString> listNAICSTitleString = new List<ARC.Donor.Data.Entities.Orgler.listString>();
                List<ARC.Donor.Data.Entities.Orgler.listString> listNAICSRuleKeyword = new List<ARC.Donor.Data.Entities.Orgler.listString>();

                //concatinate the address components
                if (!string.IsNullOrEmpty(newAccountMapper.addr_line_1))
                    strAddress = newAccountMapper.addr_line_1;
                if (!string.IsNullOrEmpty(newAccountMapper.addr_line_2))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + ", " + newAccountMapper.addr_line_2 : newAccountMapper.addr_line_2;
                if (!string.IsNullOrEmpty(newAccountMapper.city))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + ", " + newAccountMapper.city : newAccountMapper.city;
                if (!string.IsNullOrEmpty(newAccountMapper.state))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + ", " + newAccountMapper.state : newAccountMapper.state;
                if (!string.IsNullOrEmpty(newAccountMapper.zip))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + ", " + newAccountMapper.zip : newAccountMapper.zip;

                //populate the output model object using the mapper objects
                ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NewAccountsOutputModel newAccountsOutputModel = new ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NewAccountsOutputModel();
                newAccountsOutputModel.source_system_code = newAccountMapper.arc_srcsys_cd;
                newAccountsOutputModel.line_of_service_cd = newAccountMapper.line_of_service_cd;
                newAccountsOutputModel.source_system_id = newAccountMapper.cnst_srcsys_id;
                newAccountsOutputModel.master_id = newAccountMapper.cnst_mstr_id;
                newAccountsOutputModel.mastering_result = newAccountMapper.mstrng_typ;
                newAccountsOutputModel.lexis_nexis_id = newAccountMapper.ln_id;
                newAccountsOutputModel.monetary_value = newAccountMapper.mntry_val;
                newAccountsOutputModel.rfm_score = newAccountMapper.rfm_scr;
                newAccountsOutputModel.has_potential_merge = newAccountMapper.pot_merge_ind == "1" ? true : false;
                newAccountsOutputModel.has_potential_unmerge = newAccountMapper.pot_unmerge_ind == "1" ? true : false;
                newAccountsOutputModel.pot_unmerge_rsn = newAccountMapper.pot_unmerge_rsn;
                newAccountsOutputModel.status = newAccountMapper.confirm_ind;
                newAccountsOutputModel.line_of_service_cd = newAccountMapper.line_of_service_cd;
                newAccountsOutputModel.ent_org_id = newAccountMapper.ent_org_id;
                newAccountsOutputModel.ent_org_name = newAccountMapper.ent_org_name;
                newAccountsOutputModel.name = newAccountMapper.name;
                newAccountsOutputModel.address = strAddress;
                newAccountsOutputModel.phone = newAccountMapper.phone;
                newAccountsOutputModel.email = newAccountMapper.email_address;
                newAccountsOutputModel.mstr_metric_ts = newAccountMapper.mstr_metric_ts;

                //populate the various NAICS components in lists
                if (!string.IsNullOrEmpty(newAccountMapper.naics_cd1)) listNAICSString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = newAccountMapper.naics_cd1, status = string.Empty });
                if (!string.IsNullOrEmpty(newAccountMapper.naics_cd2)) listNAICSString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = newAccountMapper.naics_cd2, status = string.Empty });
                if (!string.IsNullOrEmpty(newAccountMapper.naics_cd3)) listNAICSString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = newAccountMapper.naics_cd3, status = string.Empty });
                newAccountsOutputModel.listNAICSCodes = listNAICSString;

                if (!string.IsNullOrEmpty(newAccountMapper.naics_indus_title1)) listNAICSTitleString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = newAccountMapper.naics_indus_title1, status = newAccountMapper.sts1 });
                if (!string.IsNullOrEmpty(newAccountMapper.naics_indus_title2)) listNAICSTitleString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = newAccountMapper.naics_indus_title2, status = newAccountMapper.sts2 });
                if (!string.IsNullOrEmpty(newAccountMapper.naics_indus_title3)) listNAICSTitleString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = newAccountMapper.naics_indus_title3, status = newAccountMapper.sts3 });
                newAccountsOutputModel.listNAICSDesc = listNAICSTitleString;

                if (!string.IsNullOrEmpty(newAccountMapper.rule_keywrd1)) listNAICSRuleKeyword.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = newAccountMapper.rule_keywrd1, status = string.Empty });
                if (!string.IsNullOrEmpty(newAccountMapper.rule_keywrd2)) listNAICSRuleKeyword.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = newAccountMapper.rule_keywrd2, status = string.Empty });
                if (!string.IsNullOrEmpty(newAccountMapper.rule_keywrd3)) listNAICSRuleKeyword.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = newAccountMapper.rule_keywrd3, status = string.Empty });
                newAccountsOutputModel.listNAICSRuleKeyword = listNAICSRuleKeyword;

                //add the output model to the output list
                listNewAccountOutputMapper.Add(newAccountsOutputModel);
            }
            //return back the list 
            return listNewAccountOutputMapper;
        }
        /* Method name: getTopOrgsSearchResults
        * Input Parameters: An object of TopOrgsInputModel class
        * Output Parameters: A list of TopOrgsOutputModel class  
        * Purpose: This method gets the search results for top organization */
        public IList<TopOrgsOutputModel> getTopOrgsSearchResults(TopOrgsInputModel topOrgsInputData)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<ARC.Donor.Business.Orgler.AccountMonitoring.TopOrgsInputModel, Data.Entities.Orgler.AccountMonitoring.TopOrgsInputModel>();
            Mapper.CreateMap<Data.Entities.Orgler.listString, ARC.Donor.Business.Orgler.listString>();
            Mapper.CreateMap<Data.Entities.Orgler.AccountMonitoring.TopOrgsOutputModel, ARC.Donor.Business.Orgler.AccountMonitoring.TopOrgsOutputModel>();

            //map the input from business object to the data layer object
            var Input = Mapper.Map<ARC.Donor.Business.Orgler.AccountMonitoring.TopOrgsInputModel, ARC.Donor.Data.Entities.Orgler.AccountMonitoring.TopOrgsInputModel>(topOrgsInputData);

            //Instantiate the data layer object for search functionality
            ARC.Donor.Data.Orgler.AccountMonitoring.Search csd = new ARC.Donor.Data.Orgler.AccountMonitoring.Search();

            //call the data layer method to get search results from DB
            var searchMappingResults = csd.getTopOrgsSearchResults(Input);

            //convert the mapper results to output format 
            var searchResults = convertTopOrgsMapperToOutputModel(searchMappingResults);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.AccountMonitoring.TopOrgsOutputModel>, IList<ARC.Donor.Business.Orgler.AccountMonitoring.TopOrgsOutputModel>>(searchResults);

            //return the results back to the controller
            return result;

        }

        /* Method name: convertTopOrgsMapperToOutputModel
        * Input Parameters: A list of TopOrgsMapper class
        * Output Parameters: A list of TopOrgsOutputModel class 
        * Purpose: This method is used to convert the DB results in the specific class for top organization search results*/
        public IList<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.TopOrgsOutputModel> convertTopOrgsMapperToOutputModel(IList<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.TopOrgsMapper> listTopOrgsMapper)
        {
            //Instantiate a list of output class
            List<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.TopOrgsOutputModel> listTopOrgsOutputMapper = new List<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.TopOrgsOutputModel>();
            //for each mapper record
            foreach (ARC.Donor.Data.Entities.Orgler.AccountMonitoring.TopOrgsMapper topOrgsMapper in listTopOrgsMapper)
            {
                string strAddress = string.Empty;

                //create lists for various NAICS strings
                List<ARC.Donor.Data.Entities.Orgler.listString> listNAICSString = new List<ARC.Donor.Data.Entities.Orgler.listString>();
                List<ARC.Donor.Data.Entities.Orgler.listString> listNAICSTitleString = new List<ARC.Donor.Data.Entities.Orgler.listString>();
                List<ARC.Donor.Data.Entities.Orgler.listString> listNAICSRuleKeyword = new List<ARC.Donor.Data.Entities.Orgler.listString>();

                //concatenate the address components
                if (!string.IsNullOrEmpty(topOrgsMapper.addr_line_1))
                    strAddress = topOrgsMapper.addr_line_1;
                if (!string.IsNullOrEmpty(topOrgsMapper.addr_line_2))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + ", " + topOrgsMapper.addr_line_2 : topOrgsMapper.addr_line_2;
                if (!string.IsNullOrEmpty(topOrgsMapper.city))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + ", " + topOrgsMapper.city : topOrgsMapper.city;
                if (!string.IsNullOrEmpty(topOrgsMapper.state))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + ", " + topOrgsMapper.state : topOrgsMapper.state;
                if (!string.IsNullOrEmpty(topOrgsMapper.zip))
                    strAddress = !string.IsNullOrEmpty(strAddress) ? strAddress + ", " + topOrgsMapper.zip : topOrgsMapper.zip;

                //populate the output model object using the mapper objects
                ARC.Donor.Data.Entities.Orgler.AccountMonitoring.TopOrgsOutputModel topOrgsOutputModel = new ARC.Donor.Data.Entities.Orgler.AccountMonitoring.TopOrgsOutputModel();

                topOrgsOutputModel.line_of_service_cd = topOrgsMapper.line_of_service_cd;

                topOrgsOutputModel.master_id = topOrgsMapper.cnst_mstr_id;

                topOrgsOutputModel.lexis_nexis_id = topOrgsMapper.ln_id;
                topOrgsOutputModel.monetary_value = topOrgsMapper.mntry_val;
                topOrgsOutputModel.freq_value = topOrgsMapper.freq_val;
                topOrgsOutputModel.rfm_score = topOrgsMapper.rfm_scr;
                topOrgsOutputModel.has_potential_merge = topOrgsMapper.pot_merge_ind == "1" ? true : false;
                topOrgsOutputModel.has_potential_unmerge = topOrgsMapper.pot_unmerge_ind == "1" ? true : false;
                topOrgsOutputModel.pot_unmerge_rsn = topOrgsMapper.pot_unmerge_rsn;
                topOrgsOutputModel.status = topOrgsMapper.confirm_ind;
                topOrgsOutputModel.line_of_service_cd = topOrgsMapper.line_of_service_cd;
                topOrgsOutputModel.ent_org_id = topOrgsMapper.ent_org_id;
                topOrgsOutputModel.ent_org_name = topOrgsMapper.ent_org_name;
                topOrgsOutputModel.name = topOrgsMapper.name;
                topOrgsOutputModel.address = strAddress;
                topOrgsOutputModel.phone = topOrgsMapper.phone;
                topOrgsOutputModel.email = topOrgsMapper.email_address;
                topOrgsOutputModel.mst_rcnt_patrng_dt = topOrgsMapper.mst_rcnt_patrng_dt;
                //populate the various NAICS components in lists
                if (!string.IsNullOrEmpty(topOrgsMapper.naics_cd1)) listNAICSString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = topOrgsMapper.naics_cd1, status = string.Empty });
                if (!string.IsNullOrEmpty(topOrgsMapper.naics_cd2)) listNAICSString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = topOrgsMapper.naics_cd2, status = string.Empty });
                if (!string.IsNullOrEmpty(topOrgsMapper.naics_cd3)) listNAICSString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = topOrgsMapper.naics_cd3, status = string.Empty });
                topOrgsOutputModel.listNAICSCodes = listNAICSString;

                if (!string.IsNullOrEmpty(topOrgsMapper.naics_indus_title1)) listNAICSTitleString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = topOrgsMapper.naics_indus_title1, status = topOrgsMapper.sts1 });
                if (!string.IsNullOrEmpty(topOrgsMapper.naics_indus_title2)) listNAICSTitleString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = topOrgsMapper.naics_indus_title2, status = topOrgsMapper.sts2 });
                if (!string.IsNullOrEmpty(topOrgsMapper.naics_indus_title3)) listNAICSTitleString.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = topOrgsMapper.naics_indus_title3, status = topOrgsMapper.sts3 });
                topOrgsOutputModel.listNAICSDesc = listNAICSTitleString;

                if (!string.IsNullOrEmpty(topOrgsMapper.rule_keywrd1)) listNAICSRuleKeyword.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = topOrgsMapper.rule_keywrd1, status = string.Empty });
                if (!string.IsNullOrEmpty(topOrgsMapper.rule_keywrd2)) listNAICSRuleKeyword.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = topOrgsMapper.rule_keywrd2, status = string.Empty });
                if (!string.IsNullOrEmpty(topOrgsMapper.rule_keywrd3)) listNAICSRuleKeyword.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = topOrgsMapper.rule_keywrd3, status = string.Empty });
                topOrgsOutputModel.listNAICSRuleKeyword = listNAICSRuleKeyword;

                //add the output model to the output list
                listTopOrgsOutputMapper.Add(topOrgsOutputModel);
            }
            //return back the list 
            return listTopOrgsOutputMapper;
        }
    }
}
