using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class Summary
    {
        /* Method name:getTagsDetails
        * Input Parameters: Enterprise org id whose tags needs to be known.
        * Output Parameters: A list of TagOutputModel class  
        * Purpose: This method get the tags details of particular enterprise */
        public IList<Business.Orgler.EnterpriseOrgs.SummaryOutputModel> getSummaryDetails(int NoOfRecs, int PageNum, string enterpriseOrgId)
        {
            //Instantiate the data layer object for tags functionality
            Data.Orgler.EnterpriseOrgs.Summary gd = new Data.Orgler.EnterpriseOrgs.Summary();

            //call the data layer method to find the tags of an enterprise from database.
            var SummaryLst = gd.getSummaryDetails(NoOfRecs, PageNum, enterpriseOrgId);

            //call data layer method to get the bridge count details for the specific enterprise
            var BridgeCountLst = gd.getBridgeCountDetails(enterpriseOrgId);

            var finalSummaryResults = convertSummartMapperToOutput(SummaryLst, BridgeCountLst);

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.SummaryOutputModel, Business.Orgler.EnterpriseOrgs.SummaryOutputModel>();
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.BridgeCount, Business.Orgler.EnterpriseOrgs.BridgeCount>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.SummaryOutputModel>, IList<Business.Orgler.EnterpriseOrgs.SummaryOutputModel>>(finalSummaryResults);
            return result;
        }

        public IList<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.SummaryOutputModel> convertSummartMapperToOutput(IList<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.SummaryMapper> listSummaryMapper, IList<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCountOutputModel> listBridgeCount)
        {
            List<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.SummaryOutputModel> listSummaryResults = new List<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.SummaryOutputModel>();
            foreach(ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.SummaryMapper mapper in listSummaryMapper)
            {
                ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.SummaryOutputModel output = new Data.Entities.Orgler.EnterpriseOrgs.SummaryOutputModel();

                string strLobCount = string.Empty;
                List<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCount> ltLobBrid = new List<Data.Entities.Orgler.EnterpriseOrgs.BridgeCount>();
                
                if (!string.IsNullOrWhiteSpace(mapper.fr_cnt) && mapper.fr_cnt.ToString() != "0")
                {
                    ltLobBrid.Add(new ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCount { line_of_service_cd = "FR", los_cnt = "FR(" + mapper.fr_cnt.ToString().Trim() + ")" });
                    strLobCount = strLobCount == string.Empty ? "FR(" + mapper.fr_cnt.ToString().Trim() + ")"
                                                : strLobCount + ", FR(" + mapper.fr_cnt.ToString().Trim() + ")";
                }
                if (!string.IsNullOrWhiteSpace(mapper.frchpt_cnt) && mapper.frchpt_cnt.ToString() != "0")
                {
                    ltLobBrid.Add(new ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCount { line_of_service_cd = "FRCHPT", los_cnt = "FRCHPT(" + mapper.frchpt_cnt.ToString().Trim() + ")" });
                    strLobCount = strLobCount == string.Empty ? "FRCHPT(" + mapper.frchpt_cnt.ToString().Trim() + ")"
                                                : strLobCount + ", FRCHPT(" + mapper.frchpt_cnt.ToString().Trim() + ")";
                }
                if (!string.IsNullOrWhiteSpace(mapper.bio_cnt) && mapper.bio_cnt.ToString() != "0")
                {
                    ltLobBrid.Add(new ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCount { line_of_service_cd = "BIO", los_cnt = "BIO(" + mapper.bio_cnt.ToString().Trim() + ")" });
                    strLobCount = strLobCount == string.Empty ? "BIO(" + mapper.bio_cnt.ToString().Trim() + ")"
                                                : strLobCount + ", BIO(" + mapper.bio_cnt.ToString().Trim() + ")";
                }
                if (!string.IsNullOrWhiteSpace(mapper.phss_cnt) && mapper.phss_cnt.ToString() != "0")
                {
                    ltLobBrid.Add(new ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCount { line_of_service_cd = "PHSS", los_cnt = "HS(" + mapper.phss_cnt.ToString().Trim() + ")" });
                    strLobCount = strLobCount == string.Empty ? "HS(" + mapper.phss_cnt.ToString().Trim() + ")"
                                                : strLobCount + ", HS(" + mapper.phss_cnt.ToString().Trim() + ")";
                }
                if (!string.IsNullOrWhiteSpace(mapper.eosi_cnt) && mapper.eosi_cnt.ToString() != "0")
                {
                    ltLobBrid.Add(new ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCount { line_of_service_cd = "EOSI", los_cnt = "EOSI(" + mapper.eosi_cnt.ToString().Trim() + ")" });
                    strLobCount = strLobCount == string.Empty ? "EOSI(" + mapper.eosi_cnt.ToString().Trim() + ")"
                                                : strLobCount + ", EOSI(" + mapper.eosi_cnt.ToString().Trim() + ")";
                }

                string strSourceSystemCount = string.Empty;
                if (listBridgeCount != null)
                { 
                    if(listBridgeCount.Count > 0)
                    {
                        //Complete Source System counts
                        foreach(ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCountOutputModel brid in listBridgeCount)
                        {
                            strSourceSystemCount = strSourceSystemCount == string.Empty ? brid.arc_srcsys_cd.ToString().Trim() + "(" + brid.brid_cnt.ToString().Trim() + ")"
                                                : strSourceSystemCount + ", " + brid.arc_srcsys_cd.ToString().Trim() + "(" + brid.brid_cnt.ToString().Trim() + ")";
                        }

                        //Source System Counts at LOB
                        foreach(ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCount lob in ltLobBrid)
                        {
                            List<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCountOutputModel> subBridgeCountList = new List<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCountOutputModel>();
                            subBridgeCountList = listBridgeCount.Where(x => x.line_of_service_cd == lob.line_of_service_cd).ToList();
                           
                            if(subBridgeCountList.Count > 0)
                            {
                                //Complete Source System counts
                                string strSourceSystemCountSubString = string.Empty;
                                foreach (ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.BridgeCountOutputModel brid in subBridgeCountList)
                                {
                                    strSourceSystemCountSubString = strSourceSystemCountSubString == string.Empty ? brid.arc_srcsys_cd.ToString().Trim() + "(" + brid.brid_cnt.ToString().Trim() + ")"
                                                        : strSourceSystemCountSubString + ", " + brid.arc_srcsys_cd.ToString().Trim() + "(" + brid.brid_cnt.ToString().Trim() + ")";
                                }
                                lob.srcsys_cnt = strSourceSystemCountSubString;
                            }
                        }
                    }
                }

                output.ent_org_id = mapper.ent_org_id;
                output.ent_org_name = mapper.ent_org_name;
                output.ent_org_src_cd = mapper.ent_org_src_cd;
                output.nk_ent_org_id = mapper.nk_ent_org_id;
                output.ent_org_dsc = mapper.ent_org_dsc;
                output.lob_cnt = strLobCount;
                output.srcsys_cnt = strSourceSystemCount;
                output.created_by = mapper.created_by;
                output.created_at = mapper.created_at;
                output.last_modified_by = mapper.last_modified_by;
                output.last_modified_at = mapper.last_modified_at;
                output.last_modified_by_all = mapper.last_modified_by_all;
                output.last_modified_at_all = mapper.last_modified_at_all;
                output.last_reviewed_by = mapper.last_reviewed_by;
                output.last_reviewed_at = mapper.last_reviewed_at;
                output.data_stwrd_usr = mapper.data_stwrd_usr;
                output.fr_rcnt_patrng_dt = mapper.fr_rcnt_patrng_dt;
                output.fr_totl_dntn_cnt = mapper.fr_totl_dntn_cnt;
                output.fr_totl_dntn_val = mapper.fr_totl_dntn_val;
                output.fr_rcncy_scr = mapper.fr_rcncy_scr;
                output.fr_freq_scr = mapper.fr_freq_scr;
                output.fr_dntn_scr = mapper.fr_dntn_scr;
                output.fr_totl_rfm_scr = mapper.fr_totl_rfm_scr;
                output.bio_rcnt_patrng_dt = mapper.bio_rcnt_patrng_dt;
                output.bio_totl_dntn_cnt = mapper.bio_totl_dntn_cnt;
                output.bio_totl_dntn_val = mapper.bio_totl_dntn_val;
                output.bio_rcncy_scr = mapper.bio_rcncy_scr;
                output.bio_freq_scr = mapper.bio_freq_scr;
                output.bio_dntn_scr = mapper.bio_dntn_scr;
                output.bio_totl_rfm_scr = mapper.bio_totl_rfm_scr;
                output.hs_rcnt_patrng_dt = mapper.hs_rcnt_patrng_dt;
                output.hs_totl_dntn_cnt = mapper.hs_totl_dntn_cnt;
                output.hs_totl_dntn_val = mapper.hs_totl_dntn_val;
                output.hs_rcncy_scr = mapper.hs_rcncy_scr;
                output.hs_freq_scr = mapper.hs_freq_scr;
                output.hs_dntn_scr = mapper.hs_dntn_scr;
                output.hs_totl_rfm_scr = mapper.hs_totl_rfm_scr;
                output.mstr_cnt = mapper.mstr_cnt;
                output.brid_cnt = mapper.brid_cnt;
                output.lt_brid_cnt = ltLobBrid;

                listSummaryResults.Add(output);
            }

            return listSummaryResults;
        }
    }
}
