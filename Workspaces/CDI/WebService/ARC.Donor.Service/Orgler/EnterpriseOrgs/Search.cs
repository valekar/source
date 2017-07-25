using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ARC.Donor.Business.Orgler.EnterpriseOrgs;

namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class Search
    {
        /* Method name: getNewAccountSearchResults
        * Input Parameters: An object of NewAccountsInputModel class
        * Output Parameters: A list of NewAccountsOutputModel class  
        * Purpose: This method gets the search results for new account */
        public IList<EnterpriseOrgOutputSearchResults> getEnterpriseOrgSearchResults(ListEnterpriseOrgInputSearchModel enterpriseOrgInputData)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<ARC.Donor.Business.Orgler.EnterpriseOrgs.ListEnterpriseOrgInputSearchModel, Data.Entities.Orgler.EnterpriseOrgs.ListEnterpriseOrgInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.Orgler.EnterpriseOrgs.EnterpriseOrgInputSearchModel, Data.Entities.Orgler.EnterpriseOrgs.EnterpriseOrgInputSearchModel>();
            Mapper.CreateMap<ARC.Donor.Business.Orgler.EnterpriseOrgs.RankInput, Data.Entities.Orgler.EnterpriseOrgs.RankInput>();
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.EnterpriseOrgOutputSearchResults, ARC.Donor.Business.Orgler.EnterpriseOrgs.EnterpriseOrgOutputSearchResults>();
            Mapper.CreateMap<ARC.Donor.Data.Entities.Orgler.listString, ARC.Donor.Business.Orgler.listString>();      

            //map the input from buisness object to the data layer object
            var Input = Mapper.Map<ARC.Donor.Business.Orgler.EnterpriseOrgs.ListEnterpriseOrgInputSearchModel, ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.ListEnterpriseOrgInputSearchModel>(enterpriseOrgInputData);

            //Instantiate the data layer object for search functionality
            ARC.Donor.Data.Orgler.EnterpriseOrgs.Search csd = new ARC.Donor.Data.Orgler.EnterpriseOrgs.Search();

            //call the data layer method to get search results from DB
            var searchResults = csd.getEnterpriseOrgSearchResults(Input);

            var convertedResults = convertNewAccountMapperToOutputModel(searchResults);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.EnterpriseOrgOutputSearchResults>, IList<Business.Orgler.EnterpriseOrgs.EnterpriseOrgOutputSearchResults>>(convertedResults);

            return result;
        }

        /* Method name: convertNewAccountMapperToOutputModel
        * Input Parameters: A list of NewAccountSearchMapper class
        * Output Parameters: A list of NewAccountsOutputModel class 
        * Purpose: This method is used to convert the DB results in the specific class for new account search results*/
        public IList<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.EnterpriseOrgOutputSearchResults> convertNewAccountMapperToOutputModel(
                            IList<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.EnterpriseOrgMapper> listEnterpriseOrgMapper
                            )
        {
            //Instantiate a list of output class
            List<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.EnterpriseOrgOutputSearchResults> listEnterpriseOrgSearchResults = new List<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.EnterpriseOrgOutputSearchResults>();
            Dictionary<string, List<ARC.Donor.Data.Entities.Orgler.listString>> dictEntOrgNAICS = new Dictionary<string, List<ARC.Donor.Data.Entities.Orgler.listString>>();
            Dictionary<string, List<ARC.Donor.Data.Entities.Orgler.listString>> dictEntOrgDescription = new Dictionary<string, List<ARC.Donor.Data.Entities.Orgler.listString>>();
            Dictionary<string, List<ARC.Donor.Data.Entities.Orgler.listString>> dictTags = new Dictionary<string, List<ARC.Donor.Data.Entities.Orgler.listString>>();
            List<string> listEnterpriseOrgIds = new List<string>();
            foreach (ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.EnterpriseOrgMapper entOrgMapper in listEnterpriseOrgMapper)
            {
                if (!listEnterpriseOrgIds.Contains(entOrgMapper.ent_org_id))
                {
                    listEnterpriseOrgIds.Add(entOrgMapper.ent_org_id);
                    ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.EnterpriseOrgOutputSearchResults entOrgSearchOutput = new ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.EnterpriseOrgOutputSearchResults();

                    entOrgSearchOutput.ent_org_id = entOrgMapper.ent_org_id;
                    entOrgSearchOutput.ent_org_name = entOrgMapper.ent_org_name;
                    entOrgSearchOutput.ent_org_src_cd = entOrgMapper.ent_org_src_cd;
                    entOrgSearchOutput.nk_ent_org_id = entOrgMapper.nk_ent_org_id;
                    entOrgSearchOutput.affil_cnt = entOrgMapper.affil_cnt;
                    entOrgSearchOutput.transformation_cnt = entOrgMapper.transformation_cnt;
                    entOrgSearchOutput.created_at = entOrgMapper.created_at;
                    entOrgSearchOutput.created_by = entOrgMapper.created_by;
                    entOrgSearchOutput.last_modified_at = entOrgMapper.last_modified_at;
                    entOrgSearchOutput.last_modified_at_all = entOrgMapper.last_modified_at_all;
                    entOrgSearchOutput.last_modified_by = entOrgMapper.last_modified_by;
                    entOrgSearchOutput.last_modified_by_all = entOrgMapper.last_modified_by_all;
                    entOrgSearchOutput.last_reviewed_at = entOrgMapper.last_reviewed_at;
                    entOrgSearchOutput.last_reviewed_by = entOrgMapper.last_reviewed_by;
                    entOrgSearchOutput.trans_key = entOrgMapper.trans_key;
                    entOrgSearchOutput.data_stwrd_usr = entOrgMapper.data_stwrd_usr;
                    entOrgSearchOutput.dw_srcsys_trans_ts = entOrgMapper.dw_srcsys_trans_ts;
                    entOrgSearchOutput.row_stat_cd = entOrgMapper.row_stat_cd;
                    entOrgSearchOutput.load_id = entOrgMapper.load_id;

                    entOrgSearchOutput.parent_ent_org_id = entOrgMapper.parent_ent_org_id;

                    string strSourceSystemCount = string.Empty;
                    if(!string.IsNullOrWhiteSpace(entOrgMapper.fr_cnt) && entOrgMapper.fr_cnt.ToString() != "0")
                    {
                        strSourceSystemCount = strSourceSystemCount == string.Empty ? "FR(" + entOrgMapper.fr_cnt.ToString().Trim() + ")"
                                                    : strSourceSystemCount + ", FR(" + entOrgMapper.fr_cnt.ToString().Trim() + ")";
                    }
                    if (!string.IsNullOrWhiteSpace(entOrgMapper.frchpt_cnt) && entOrgMapper.frchpt_cnt.ToString() != "0")
                    {
                        strSourceSystemCount = strSourceSystemCount == string.Empty ? "FRCHPT(" + entOrgMapper.frchpt_cnt.ToString().Trim() + ")"
                                                    : strSourceSystemCount + ", FRCHPT(" + entOrgMapper.frchpt_cnt.ToString().Trim() + ")";
                    }
                    if (!string.IsNullOrWhiteSpace(entOrgMapper.bio_cnt) && entOrgMapper.bio_cnt.ToString() != "0")
                    {
                        strSourceSystemCount = strSourceSystemCount == string.Empty ? "BIO(" + entOrgMapper.bio_cnt.ToString().Trim() + ")"
                                                    : strSourceSystemCount + ", BIO(" + entOrgMapper.bio_cnt.ToString().Trim() + ")";
                    }
                    if (!string.IsNullOrWhiteSpace(entOrgMapper.phss_cnt) && entOrgMapper.phss_cnt.ToString() != "0")
                    {
                        strSourceSystemCount = strSourceSystemCount == string.Empty ? "HS(" + entOrgMapper.phss_cnt.ToString().Trim() + ")"
                                                    : strSourceSystemCount + ", HS(" + entOrgMapper.phss_cnt.ToString().Trim() + ")";
                    }
                    if (!string.IsNullOrWhiteSpace(entOrgMapper.eosi_cnt) && entOrgMapper.eosi_cnt.ToString() != "0")
                    {
                        strSourceSystemCount = strSourceSystemCount == string.Empty ? "EOSI(" + entOrgMapper.eosi_cnt.ToString().Trim() + ")"
                                                    : strSourceSystemCount + ", EOSI(" + entOrgMapper.eosi_cnt.ToString().Trim() + ")";
                    }

                    entOrgSearchOutput.srcsys_cnt = strSourceSystemCount;

                    listEnterpriseOrgSearchResults.Add(entOrgSearchOutput);
                }
                    
                    string strNAICSCode = entOrgMapper.naics_cd;
                    string strNAICSDescription = entOrgMapper.naics_indus_title;
                    if (!string.IsNullOrEmpty(strNAICSCode))
                    {
                        if (dictEntOrgNAICS.ContainsKey(entOrgMapper.ent_org_id))
                        {
                            bool boolAlreadyPresent = false;
                            List<ARC.Donor.Data.Entities.Orgler.listString> listNAICSCode = dictEntOrgNAICS[entOrgMapper.ent_org_id];
                            List<ARC.Donor.Data.Entities.Orgler.listString> listNAICSDesc = dictEntOrgDescription[entOrgMapper.ent_org_id];
                            foreach (ARC.Donor.Data.Entities.Orgler.listString s in listNAICSCode)
                            {
                                if (s.strText == strNAICSCode) boolAlreadyPresent = true;
                            }
                            if (boolAlreadyPresent == false)
                            {
                                listNAICSCode.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = strNAICSCode, status = entOrgMapper.sts });
                                dictEntOrgNAICS[entOrgMapper.ent_org_id] = listNAICSCode;
                                listNAICSDesc.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = strNAICSDescription, status = entOrgMapper.sts });
                                dictEntOrgDescription[entOrgMapper.ent_org_id] = listNAICSDesc;
                            }
                        }
                        else
                        {
                            List<ARC.Donor.Data.Entities.Orgler.listString> listNAICSCode = new List<ARC.Donor.Data.Entities.Orgler.listString>();
                            List<ARC.Donor.Data.Entities.Orgler.listString> listNAICSDesc = new List<ARC.Donor.Data.Entities.Orgler.listString>();
                            listNAICSCode.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = strNAICSCode, status = entOrgMapper.sts });
                            listNAICSDesc.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = strNAICSDescription, status = entOrgMapper.sts });
                            dictEntOrgNAICS.Add(entOrgMapper.ent_org_id, listNAICSCode);
                            dictEntOrgDescription.Add(entOrgMapper.ent_org_id, listNAICSDesc);
                        }
                    }
                    
                    string strTag = entOrgMapper.tag;
                    if (!string.IsNullOrEmpty(strTag))
                    {
                        if (dictTags.ContainsKey(entOrgMapper.ent_org_id))
                        {
                            bool boolAlreadyPresent = false;
                            List<ARC.Donor.Data.Entities.Orgler.listString> listTags = dictTags[entOrgMapper.ent_org_id];
                            foreach (ARC.Donor.Data.Entities.Orgler.listString s in listTags)
                            {
                                if (s.strText == strTag) boolAlreadyPresent = true;
                            }
                            if (boolAlreadyPresent == false)
                            {
                                listTags.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = strTag, status = string.Empty });
                                dictTags[entOrgMapper.ent_org_id] = listTags;
                            }
                        }
                        else
                        {
                            List<ARC.Donor.Data.Entities.Orgler.listString> listTags = new List<ARC.Donor.Data.Entities.Orgler.listString>();
                            listTags.Add(new ARC.Donor.Data.Entities.Orgler.listString { strText = strTag, status = string.Empty });
                            dictTags.Add(entOrgMapper.ent_org_id, listTags);
                        }
                    }
                    
               // }
            }
            foreach (ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.EnterpriseOrgOutputSearchResults output in listEnterpriseOrgSearchResults)
            {
                if (dictTags.ContainsKey(output.ent_org_id)) output.listTags = dictTags[output.ent_org_id];
                if (dictEntOrgNAICS.ContainsKey(output.ent_org_id)) output.listNAICSCodes = dictEntOrgNAICS[output.ent_org_id];
                if (dictEntOrgDescription.ContainsKey(output.ent_org_id)) output.listNAICSDesc = dictEntOrgDescription[output.ent_org_id];
            }
            return listEnterpriseOrgSearchResults;
            

        }
    }
}
