using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class Affiliations
    {

        /* Method name: getAffiliatedMasterBridgeDetails
      * Input Parameters: Enterprise org id whose affiliation needs to be known.
      * Output Parameters: A list of AffiliationOutputModel class  
      * Purpose: This method get the affiliations of particular enterprise */
        public Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutput getAffiliatedMasterBridgeDetails(ARC.Donor.Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeInput input)
        {
            //Instantiate the data layer object for affiliations functionality
            Data.Orgler.EnterpriseOrgs.Affiliations gd = new Data.Orgler.EnterpriseOrgs.Affiliations();

            List<Data.Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeSummary> affilSmryList = new List<Data.Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeSummary>();
            List<Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutputModel> affilResList = new List<Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutputModel>();
            ARC.Donor.Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutput ltRes = new Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutput();
            ltRes.lt_affil_res = new List<Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutputModel>();
            ltRes.summary_info = new Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeSummary();

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutputModel, Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutputModel>();
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeSummary, Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeSummary>();

            if (input.strLoadType == "initial")
            {
                //call the data layer method to find the affiliation summary information for the enterprise
                var AffiliationSummary = gd.getAffilatedMasterBridgeSummaryResults(input.ent_org_id);
                var AffiliationOrgTypeSummary = gd.getAffilatedMasterBridgeOrgTypesResult(input.ent_org_id);

                //Check for the record count and then add the limit of the affiliation query
                if (AffiliationSummary != null)
                {
                    affilSmryList = AffiliationSummary.ToList();

                    //check for the existence of atleast one record
                    if (affilSmryList.Count > 0)
                    {
                        ltRes.summary_info = Mapper.Map<Data.Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeSummary, Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeSummary>(affilSmryList[0]);
                        ltRes.summary_info.str_concat_org_typ_cnt = string.Empty;
                        //If the bridge count is more than 2K, then apply the limit
                        if (Convert.ToInt32(affilSmryList[0].total_brid_cnt) <= Convert.ToInt32(input.AffiliationLimit))
                        {
                            input.AffiliationLimit = "0";
                        }

                        //Org Type Consolidation
                        List<Data.Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOrgTypeSummary> affilOrgTypSmryList = new List<Data.Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOrgTypeSummary>();
                        if (AffiliationOrgTypeSummary != null)
                        {
                            affilOrgTypSmryList = AffiliationOrgTypeSummary.ToList();
                            string strOrgTypeCount = string.Empty;
                            foreach (Data.Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOrgTypeSummary org_typ in affilOrgTypSmryList)
                            {
                                strOrgTypeCount = (strOrgTypeCount == string.Empty) ? org_typ.eosi_org_typ.ToString().Trim() + " - (" + org_typ.org_cnt.ToString().Trim() + ")"
                                                        : strOrgTypeCount + ", " + org_typ.eosi_org_typ.ToString().Trim() + " - (" + org_typ.org_cnt.ToString().Trim() + ")";
                            }
                            ltRes.summary_info.str_concat_org_typ_cnt = strOrgTypeCount;
                        }
                    }
                }
            }
            else
            {
                input.AffiliationLimit = "0";
            }

            //call the data layer method to find the affiliations of an enterprise from database.
            var AffiliationLst = gd.getAffiliatedMasterBridgeSQLResults(Convert.ToInt32(input.AffiliationLimit), input.ent_org_id);

            //map the output from data layer to the business layer
            var tempResult = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutputModel>, IList<Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeOutputModel>>(AffiliationLst);
            ltRes.lt_affil_res = tempResult.ToList();

            return ltRes;
        }


        /* Method name: getAffiliationDetails
       * Input Parameters: Enterprise org id whose affiliation needs to be known.
       * Output Parameters: A list of AffiliationOutputModel class  
       * Purpose: This method get the affiliations of particular enterprise */
        public IList<Business.Orgler.EnterpriseOrgs.AffiliationsOutputModel> getAffiliationDetails(int NoOfRecs, int PageNum, string enterpriseOrgId)
        {
            //Instantiate the data layer object for affiliations functionality
            Data.Orgler.EnterpriseOrgs.Affiliations gd = new Data.Orgler.EnterpriseOrgs.Affiliations();

            //call the data layer method to find the affiliations of an enterprise from database.
            var AffiliationLst = gd.getAffiliationSQLResults(NoOfRecs, PageNum, enterpriseOrgId);

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.AffiliationsOutputModel, Business.Orgler.EnterpriseOrgs.AffiliationsOutputModel>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.AffiliationsOutputModel>, IList<Business.Orgler.EnterpriseOrgs.AffiliationsOutputModel>>(AffiliationLst);
            return result;
        }
        /* Method name: getBridgeDetails
       * Input Parameters:  Enterprise org id whose bridge needs to be known.
       * Output Parameters: A list of BridgeOutputModel class  
       * Purpose: This method finds the bridge of an enterprise. */
        public IList<Business.Orgler.EnterpriseOrgs.BridgeOutputModel> getBridgeDetails(int NoOfRecs, int PageNum, string enterpriseOrgId)
        {   //Instantiate the data layer object for affiliations functionality
            Data.Orgler.EnterpriseOrgs.Affiliations gd = new Data.Orgler.EnterpriseOrgs.Affiliations();


            //call the data layer method to find the bridge of an enterprise from database.
            var BridgeLst = gd.getBridgeSQLResults(NoOfRecs, PageNum, enterpriseOrgId);

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.BridgeOutputModel, Business.Orgler.EnterpriseOrgs.BridgeOutputModel>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.BridgeOutputModel>, IList<Business.Orgler.EnterpriseOrgs.BridgeOutputModel>>(BridgeLst);
            return result;
        }

        public IList<Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeExportOutputModel> getAffiliatedMasterBridgeExportDetails(int NoOfRecs, int PageNum, string enterpriseOrgId)
        {
            //Instantiate the data layer object for affiliations functionality
            Data.Orgler.EnterpriseOrgs.Affiliations gd = new Data.Orgler.EnterpriseOrgs.Affiliations();

            //call the data layer method to find the affiliations of an enterprise from database.
            var AffiliationLst = gd.getAffiliatedMasterBridgeExportSQLResults(NoOfRecs, PageNum, enterpriseOrgId);

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeExportOutputModel, Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeExportOutputModel>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeExportOutputModel>, IList<Business.Orgler.EnterpriseOrgs.AffiliatedMasterBridgeExportOutputModel>>(AffiliationLst);
            return result;
        }
    }
}
