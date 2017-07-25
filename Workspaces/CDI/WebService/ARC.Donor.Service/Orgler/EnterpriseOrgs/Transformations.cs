using ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Orgler.EnterpriseOrgs
{
    public class Transformations
    {
        public Business.Orgler.EnterpriseOrgs.TransformationModel getTransformationDetails(int NoOfRecs, int PageNum, string enterpriseOrgId)
        {
            //Instantiate the data layer object for transformation functionality
            Data.Orgler.EnterpriseOrgs.Transformations gd = new Data.Orgler.EnterpriseOrgs.Transformations();

            //call the data layer method to find the transformation of an enterprise 
            var TransformationLst = gd.getTransformationSQLResults(NoOfRecs, PageNum, enterpriseOrgId);

            //call the data layer method to find the CDI Transformation of an enterprise 
            var CDITransformationLst = gd.getCDITransformationSQLResults(NoOfRecs, PageNum, enterpriseOrgId);

            //call the data layer method to find the Stuart Transformation of an enterprise 
            var StuartTransformationLst = gd.getStuartTransformationSQLResults(NoOfRecs, PageNum, enterpriseOrgId);
            //   Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.TransformationMapper, Business.Orgler.EnterpriseOrgs.TransformationMapper>();

            //call the data layer method to find the Stuart Transformation of an enterprise 
            var AffiliationCount = gd.getEnterpriseTransformationCountSQLResults(NoOfRecs, PageNum, enterpriseOrgId);

            ARC.Donor.Business.Orgler.EnterpriseOrgs.TransformationModel transformOutput = new Business.Orgler.EnterpriseOrgs.TransformationModel();
            transformOutput.output = new List<Business.Orgler.EnterpriseOrgs.TransformationOutputModel>();
            transformOutput.manual_affil_cnt = "0";
            if (AffiliationCount != null)
            {
                int affilCnt = 0;
                var affilCntRes = AffiliationCount.Where(t => t.id == "0");
                if (affilCntRes.ToList().Count > 0)
                    affilCnt = AffiliationCount.Where(t => t.id == "0").Select(t => t.cnt).First();
                transformOutput.manual_affil_cnt = affilCnt.ToString();
            }

            List<Data.Entities.Orgler.EnterpriseOrgs.TransformationOutputModel> transformationOutput = new List<Data.Entities.Orgler.EnterpriseOrgs.TransformationOutputModel>();
            foreach(ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.TransformationMapper TransformationOutput in TransformationLst)
            {
                TransformationOutputModel transformationOutputModel = new TransformationOutputModel();
                transformationOutputModel.ent_org_id = TransformationOutput.ent_org_id;
                transformationOutputModel.ent_org_branch = TransformationOutput.ent_org_branch;
                transformationOutputModel.act_ind = TransformationOutput.act_ind;
                transformationOutputModel.dw_srcsys_trans_ts = TransformationOutput.dw_srcsys_trans_ts;
                transformationOutputModel.org_nm_transform_strt_dt = TransformationOutput.org_nm_transform_strt_dt;
                transformationOutputModel.org_nm_transform_end_dt = TransformationOutput.org_nm_transform_end_dt;
                transformationOutputModel.transaction_key = TransformationOutput.transaction_key;
                transformationOutputModel.is_previous = TransformationOutput.is_previous;
                transformationOutputModel.mstr_cnt = "0";

                //string strCurrentConditional = string.Empty;
                
                if (TransformationOutput.cdim_transform_id == "0")
                {
                    ////Check for the existence of previous record for the same transaction id in replica table
                    //string strActualTransformId = string.Empty;
                    //strActualTransformId = TransformationLst.Where(x => (x.transaction_key == TransformationOutput.transaction_key && x.cdim_transform_id != "0" && x.is_previous == "1")).Select(x => x.cdim_transform_id).First().ToString();
                    //if (!string.IsNullOrEmpty(strActualTransformId))
                    //{
                    //    transformationOutputModel.cdim_transform_id = strActualTransformId;
                    //}
                    //else
                    //{
                        transformationOutputModel.cdim_transform_id = "(S) " + TransformationOutput.strx_transform_id;
                    //}
                    
                    //iterate through output i.e result of Stuart query 
                    int inc_key = 0;
                    foreach (ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.StuartTransformOutputModel stuartOutput in StuartTransformationLst)
                    {
                        string strCurrentConditional = string.Empty;
                        if (stuartOutput.strx_transform_id == TransformationOutput.strx_transform_id)
                        {
                            inc_key += 1;
                            string strDetailType = stuartOutput.transform_condn_typ_cd;
                            string strDetailTypeDescription = string.Empty;
                            if (inc_key > 1)
                                strDetailTypeDescription += " AND";
                            string strPatternString = stuartOutput.pattern_match_string;
                            switch (strDetailType)
                            {
                                case "C": strDetailTypeDescription += " Contains "; break;
                                case "D": strDetailTypeDescription += " Does not Contain "; break;
                                case "E": strDetailTypeDescription += " Exact Match "; break;
                                case "F": strDetailTypeDescription += " Starts With "; break;
                            }

                            //if (strCurrentConditional == string.Empty)
                            //{
                                strCurrentConditional = strDetailTypeDescription + "\"" + strPatternString + "\"";
                            //}
                            //else
                            //{
                            //    strCurrentConditional += " AND " + strDetailTypeDescription + "\"" + strPatternString + "\"";
                            //}
                            transformationOutputModel.conditional = strCurrentConditional;
                            TransformationOutputModel transformationOutputModelTemp = (TransformationOutputModel)transformationOutputModel.Clone();
                            transformationOutput.Add(transformationOutputModelTemp);
                        }
                    }
                    //transformationOutput.Add(transformationOutputModel);
                }
                else
                {
                    if (AffiliationCount != null)
                    {
                        int affilCnt = 0;
                        var affilCntRes = AffiliationCount.Where(t => t.id == TransformationOutput.cdim_transform_id);
                        if (affilCntRes.ToList().Count > 0)
                            affilCnt = AffiliationCount.Where(t => t.id == TransformationOutput.cdim_transform_id).Select(t => t.cnt).First();
                        transformationOutputModel.mstr_cnt = affilCnt.ToString();
                    }

                    transformationOutputModel.cdim_transform_id = TransformationOutput.cdim_transform_id;
                    int inc_key = 0;
                    foreach (ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.CDITransformOutputModel CDIOutput in CDITransformationLst.Where(x => x.cdim_transform_id == TransformationOutput.cdim_transform_id))
                    {
                        inc_key += 1;
                        string strCurrentConditional = string.Empty;
                        string strDetailType = CDIOutput.transform_condn_typ_cd;
                        string strDetailTypeDescription = string.Empty;
                        if (inc_key > 1)
                            strDetailTypeDescription += " AND";
                        string strPatternString = CDIOutput.pattern_match_string;
                        switch (strDetailType)
                        {
                            case "C": strDetailTypeDescription += " Contains "; break;
                            case "D": strDetailTypeDescription += " Does not Contain "; break;
                            case "E": strDetailTypeDescription += " Exact Match "; break;
                            case "F": strDetailTypeDescription += " Starts With "; break;
                        }
                        //if (strCurrentConditional == string.Empty)
                        //{
                            strCurrentConditional = strDetailTypeDescription + "\"" + strPatternString + "\"";
                        //}
                        //else
                        //{
                        //    strCurrentConditional += " AND " + strDetailTypeDescription + "\"" + strPatternString + "\"";
                        //}
                        transformationOutputModel.conditional = strCurrentConditional;
                        TransformationOutputModel transformationOutputModelTemp = (TransformationOutputModel)transformationOutputModel.Clone();
                        transformationOutput.Add(transformationOutputModelTemp);
                    }
                    //transformationOutput.Add(transformationOutputModel);
                }
            }

            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.TransformationOutputModel, Business.Orgler.EnterpriseOrgs.TransformationOutputModel>();
            var transformationResult = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.TransformationOutputModel>, IList<Business.Orgler.EnterpriseOrgs.TransformationOutputModel>>(transformationOutput);
            transformOutput.output = transformationResult.ToList();
            return transformOutput;
        }

        /* Purpose: This method is used to add/remove/edit transformation rules on an enterprise */
        public IList<Business.Orgler.EnterpriseOrgs.TransformationUpdateOutput> updateTransformation(ARC.Donor.Business.Orgler.EnterpriseOrgs.TransformationUpdateInput input)
        {
            //Instantiate the data layer object for transformations functionality
            Data.Orgler.EnterpriseOrgs.Transformations gd = new Data.Orgler.EnterpriseOrgs.Transformations();

            Data.Entities.Orgler.EnterpriseOrgs.TransformationUpdateFormatInput dataInput = new Data.Entities.Orgler.EnterpriseOrgs.TransformationUpdateFormatInput();
            dataInput.ent_org_id = input.ent_org_id;
            dataInput.ent_org_branch = input.ent_org_branch;
            dataInput.cdim_transform_id = input.cdim_transform_id;
            dataInput.notes = input.notes;
            dataInput.active_ind = input.active_ind;
            dataInput.req_typ = input.req_typ;
            dataInput.userId = input.userId;
            
            string strPatternString = string.Empty;

            //Frame the sql string 
            if(input.ltCondition != null)
            {
                if(input.ltCondition.Count > 0)
                {
                    foreach(Business.Orgler.EnterpriseOrgs.TransformationConditionInput cond in input.ltCondition)
                    {
                        if (!string.IsNullOrEmpty(cond.condition_typ.ToString().Trim()) && !string.IsNullOrEmpty(cond.pattern_string.ToString().Trim()))
                        {
                            strPatternString = (string.IsNullOrEmpty(strPatternString)) ? cond.condition_typ.ToString().Trim() + "|" + cond.pattern_string.ToString().Trim().Replace("'","''")
                                                                                : strPatternString + "," + cond.condition_typ.ToString().Trim() + "|" + cond.pattern_string.ToString().Trim().Replace("'", "''");
                        }
                    }
                }
            }

            dataInput.pattern_match_string = strPatternString;

            //call the data layer method to find the transformation rule on an enterprise from database.
            var outList = gd.getTransformationUpdate(dataInput);

            //Map the various business objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.TransformationUpdateOutput, Business.Orgler.EnterpriseOrgs.TransformationUpdateOutput>();

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.TransformationUpdateOutput>, IList<Business.Orgler.EnterpriseOrgs.TransformationUpdateOutput>>(outList);
            return result;
        }

        /* Purpose: This method is used to get the potential affiliations for the configured transformation rules */
        public IList<Business.Orgler.EnterpriseOrgs.TransformationSmokeTestSummary> smokeTestTransformations(ARC.Donor.Business.Orgler.EnterpriseOrgs.TransformationUpdateInput input)
        {
            //Instantiate the data layer object for transformations functionality
            Data.Orgler.EnterpriseOrgs.Transformations gd = new Data.Orgler.EnterpriseOrgs.Transformations();

            string strWhereClause = string.Empty;

            //Frame the sql string 
            if (input.ltCondition != null)
            {
                if (input.ltCondition.Count > 0)
                {
                    strWhereClause = frameTransformationRule(input.ltCondition);
                }
            }

            //call the data layer method to find the potential affiliation count for the rules configured by user
            var outList = gd.getSmokeTestAffiliationCount(strWhereClause, input.ent_org_id);
            Mapper.CreateMap<Data.Entities.Orgler.EnterpriseOrgs.TransformationSmokeTestSummary, Business.Orgler.EnterpriseOrgs.TransformationSmokeTestSummary>();
            return Mapper.Map<IList<Data.Entities.Orgler.EnterpriseOrgs.TransformationSmokeTestSummary>, IList<Business.Orgler.EnterpriseOrgs.TransformationSmokeTestSummary>>(outList);
        }

        /* Purpose: This method is used to get the potential affiliations for the configured transformation rules */
        public IList<ARC.Donor.Business.Orgler.EnterpriseOrgs.TransformationSmokeTestResults> smokeTestTransformationsExport(ARC.Donor.Business.Orgler.EnterpriseOrgs.TransformationUpdateInput input, int recCount)
        {
            //Instantiate the data layer object for transformations functionality
            Data.Orgler.EnterpriseOrgs.Transformations gd = new Data.Orgler.EnterpriseOrgs.Transformations();

            string strWhereClause = string.Empty;

            //Frame the sql string 
            if (input.ltCondition != null)
            {
                if (input.ltCondition.Count > 0)
                {
                    strWhereClause = frameTransformationRule(input.ltCondition);
                }
            }

            Mapper.CreateMap<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.TransformationSmokeTestResults, ARC.Donor.Business.Orgler.EnterpriseOrgs.TransformationSmokeTestResults>();
            
            //call the data layer method to find the potential affiliation count for the rules configured by user
            var outList = gd.smokeTestTransformationsExport(strWhereClause, recCount, input.ent_org_id);

            return Mapper.Map<IList<ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.TransformationSmokeTestResults>, IList<ARC.Donor.Business.Orgler.EnterpriseOrgs.TransformationSmokeTestResults>>(outList);
        }

        public string frameTransformationRule(List<Business.Orgler.EnterpriseOrgs.TransformationConditionInput> input)
        {
            string strWhereClause = string.Empty;
            foreach (Business.Orgler.EnterpriseOrgs.TransformationConditionInput cond in input)
            {
                string strTempWhereClause = string.Empty;
                if (!string.IsNullOrEmpty(cond.condition_typ.ToString().Trim()) && !string.IsNullOrEmpty(cond.pattern_string.ToString().Trim()))
                {
                    string strCond = string.Empty;
                    strCond = cond.pattern_string.ToString().Trim();
                    strCond = strCond.Replace("'", "''");
                    switch (cond.condition_typ.ToString().ToLower().Trim())
                    {
                        case "contains":
                            strTempWhereClause = @" ' '|| bz_cln_cnst_org_nm ||' ' LIKE '% " + strCond + " %' ";
                            break;
                        case "does not contain":
                            strTempWhereClause = @" ' '|| bz_cln_cnst_org_nm ||' ' NOT LIKE '% " + strCond + " %' ";
                            break;
                        case "exact match":
                            strTempWhereClause = @" ' '|| bz_cln_cnst_org_nm ||' ' LIKE ' " + strCond + " ' ";
                            break;
                        case "starts with":
                            strTempWhereClause = @" ' '|| bz_cln_cnst_org_nm ||' ' LIKE ' " + strCond + " %' ";
                            break;
                    }
                    strWhereClause = (string.IsNullOrEmpty(strWhereClause)) ? strTempWhereClause
                                                                        : strWhereClause + " AND " + strTempWhereClause;
                }
            }
            return strWhereClause;
        }
    }
}
