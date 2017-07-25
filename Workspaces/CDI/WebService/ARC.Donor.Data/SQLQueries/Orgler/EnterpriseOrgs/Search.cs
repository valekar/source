using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Orgler.EnterpriseOrgs
{
    public class SearchSQL
    {
        public static string getEnterpriseOrgSearchResultsSQL(ListEnterpriseOrgInputSearchModel listEnterpriseOrgInputSearch)
        {
            //set the results limit based on the input criteria
            string strResultsLimit = string.IsNullOrEmpty(listEnterpriseOrgInputSearch.AnswerSetLimit) ? " 500 " : listEnterpriseOrgInputSearch.AnswerSetLimit.ToString();

            //bool variable to check if additional join are required
            bool boolEntOrgNameInput = false;
            bool boolRankProviderYearInput = false;
            bool boolNAICSInInput = false;
            bool boolSourceSystemInput = false;
            bool boolChapterSystemInput = false;
            bool boolTags = false;
            bool boolExcludeTransformations = false;
            bool boolIncludeSuperior = false;
            bool boolIncludeSubordinate = false;

            //check if additional join is reqd and set the boolean variable
            foreach (EnterpriseOrgInputSearchModel entOrgInput in listEnterpriseOrgInputSearch.EnterpriseOrgInputSearchModel)
            {
                //if org name is provided then set the corresponding boolean variable
                if (!string.IsNullOrEmpty(entOrgInput.EnterpriseOrgName))
                {
                    boolEntOrgNameInput = true;
                }
                if (entOrgInput.listNaicsCodes != null && entOrgInput.listNaicsCodes.Count > 0)
                {
                    boolNAICSInInput = true;
                }
                if (entOrgInput.RankProvider != null && entOrgInput.RankProvider.Count > 0)
                {
                    boolRankProviderYearInput = true;
                }
                if (entOrgInput.SourceSystem != null && entOrgInput.SourceSystem.Count > 0)
                {
                    boolSourceSystemInput = true;
                }
                if (entOrgInput.ChapterSystem != null && entOrgInput.ChapterSystem.Count > 0)
                {
                    boolChapterSystemInput = true;
                }
                if (entOrgInput.Tags != null && entOrgInput.Tags.Count > 0)
                {
                    boolTags = true;
                }
                if (entOrgInput.ExcludeTransformations == true)
                {
                    boolExcludeTransformations = true;
                }
                if (entOrgInput.IncludeSuperior == true)
                {
                    boolIncludeSuperior = true;
                }
                if (entOrgInput.IncludeSubordinate == true)
                {
                    boolIncludeSubordinate = true;
                }
            }
            string strEntOrgSearchQuery = " select distinct srch.*,transform_cnt.cnt_of_mappings as transformation_cnt, affil_cnt.cnt_of_mstrs as affil_cnt,naics.naics_cd,naics.sts,CASE WHEN  naics_ref_cd.naics_indus_title <> '' THEN naics_ref_cd.naics_indus_title ELSE NULL END AS naics_indus_title,CASE WHEN  tags.tag <> '' THEN tags.tag ELSE NULL END AS tag, MAX( CASE WHEN  affil.line_of_service_dtl = 'FR' THEN bridge_cnt.fr_cnt ELSE NULL END )fr_cnt,MAX(CASE WHEN  affil.line_of_service_dtl = 'FRCHPT' THEN bridge_cnt.frchpt_cnt ELSE NULL END)frchpt_cnt, MAX(CASE WHEN  affil.line_of_service_dtl = 'PHSS' THEN bridge_cnt.phss_cnt ELSE NULL END)phss_cnt, MAX(CASE WHEN  affil.line_of_service_dtl = 'BIO' THEN bridge_cnt.bio_cnt ELSE NULL END)bio_cnt,MAX(CASE WHEN  affil.line_of_service_dtl = 'EOSI' THEN bridge_cnt.eosi_cnt ELSE NULL END) eosi_cnt from arc_orgler_vws.bz_ent_org_srch srch ";

            //CASE WHEN  affil.line_of_service_dtl = 'FR' THEN bridge_cnt.fr_cnt ELSE NULL END fr_cnt,CASE WHEN  affil.line_of_service_dtl = 'FRCHPT' THEN bridge_cnt.frchpt_cnt ELSE NULL END frchpt_cnt,CASE WHEN  affil.line_of_service_dtl = 'PHSS' THEN bridge_cnt.phss_cnt ELSE NULL END phss_cnt,CASE WHEN  affil.line_of_service_dtl = 'BIO' THEN bridge_cnt.bio_cnt ELSE NULL END bio_cnt,CASE WHEN  affil.line_of_service_dtl = 'EOSI' THEN bridge_cnt.eosi_cnt ELSE NULL END eosi_cnt from arc_orgler_vws.bz_ent_org_srch srch ";

            //bridge_cnt.fr_cnt, bridge_cnt.frchpt_cnt, bridge_cnt.phss_cnt, bridge_cnt.bio_cnt, bridge_cnt.eosi_cnt from arc_orgler_vws.bz_ent_org_srch srch ";

            strEntOrgSearchQuery += " left outer join arc_orgler_vws.ent_org_transform_cnt transform_cnt on srch.ent_org_id = transform_cnt.ent_org_id ";

            strEntOrgSearchQuery += " left outer join arc_orgler_vws.ent_org_affil_cnt affil_cnt on srch.ent_org_id = affil_cnt.ent_org_id ";

            strEntOrgSearchQuery += " left outer join arc_orgler_vws.ent_org_bridge_cnt bridge_cnt on srch.ent_org_id = bridge_cnt.ent_org_id ";

            strEntOrgSearchQuery += " left outer join arc_orgler_tbls.ent_org_naics_map naics on srch.ent_org_id = naics.ent_org_id LEFT OUTER JOIN  arc_orgler_tbls.orgler_naics_ref_cd naics_ref_cd ON naics.naics_cd = naics_ref_cd.naics_cd";

            strEntOrgSearchQuery += " LEFT OUTER JOIN arc_orgler_tbls.ent_tags_mapping tags_mapping ON srch.ent_org_id = tags_mapping.ent_org_key LEFT OUTER JOIN arc_orgler_tbls.tags tags ON tags_mapping.tag_key = tags.tag_key ";
           // strEntOrgSearchQuery += " LEFT OUTER JOIN  arc_orgler_vws.ent_org_dtl_tags tags_mapping ON srch.ent_org_id = tags_mapping.ent_org_key";
            /* if (boolIncludeSubordinate == true)
            {
                strEntOrgSearchQuery += " LEFT OUTER JOIN arc_mdm_tbls.ent_org_rlshp ent_org_rlshp_sub ON srch.ent_org_id = ent_org_rlshp_sub.superior_ent_org_key  ";
            }
            if (boolIncludeSuperior == true)
            {
                strEntOrgSearchQuery += " LEFT OUTER JOIN arc_mdm_tbls.ent_org_rlshp ent_org_rlshp_sup ON srch.ent_org_id = ent_org_rlshp_sup.subord_ent_org_key  ";
            } */
            if (boolIncludeSubordinate == true)
            {
                strEntOrgSearchQuery += " LEFT OUTER JOIN arc_orgler_tbls.ent_org_id_map sub_map ON srch.ent_org_id = sub_map.ent_org_id  ";
            }
            if (boolIncludeSuperior == true)
            {
                strEntOrgSearchQuery += " LEFT OUTER JOIN arc_orgler_tbls.ent_org_id_map sup_map ON srch.ent_org_id = sup_map.parent_ent_org_id  ";
            }
            string strEntOrgInnerSearchQuery = " select DISTINCT affiliator.ent_org_id , srcsys.line_of_service_dtl from arc_orgler_vws.bz_ent_org_srch affiliator ";

            if (boolNAICSInInput == true)
            {
                strEntOrgInnerSearchQuery += " LEFT OUTER JOIN arc_orgler_tbls.ent_org_naics_map orgler_ent_org_naics_map ON affiliator.ent_org_id  = orgler_ent_org_naics_map.ent_org_id ";
            }
            if (boolRankProviderYearInput == true)
            {
                strEntOrgInnerSearchQuery += " LEFT OUTER JOIN arc_orgler_tbls.ent_org_rnk_map orgler_ent_org_rnk_map ON affiliator.ent_org_id = orgler_ent_org_rnk_map.ent_org_id LEFT OUTER JOIN arc_orgler_tbls.org_rnk_ref orgler_org_rnk_ref ON orgler_ent_org_rnk_map.ent_org_rnk_key = orgler_org_rnk_ref.org_rnk_key ";
            }
            if (boolSourceSystemInput == true || boolChapterSystemInput == true)
            {
                strEntOrgInnerSearchQuery += " LEFT JOIN arc_mdm_vws.bz_cnst_org_affil bz_cnst_org_affil ON bz_cnst_org_affil.ent_org_id = affiliator.ent_org_id LEFT JOIN arc_mdm_vws.bz_cnst_mstr_external_brid  bz_cnst_mstr_external_brid ON bz_cnst_mstr_external_brid.cnst_mstr_id = bz_cnst_org_affil.cnst_mstr_id  LEFT JOIN arc_mdm_tbls.arc_srcsys srcsys ON bz_cnst_mstr_external_brid.arc_srcsys_cd = srcsys.arc_srcsys_cd ";
            }
            else { strEntOrgInnerSearchQuery += " LEFT JOIN arc_mdm_vws.bz_cnst_org_affil bz_cnst_org_affil ON bz_cnst_org_affil.ent_org_id = affiliator.ent_org_id LEFT JOIN arc_mdm_vws.bz_cnst_mstr_external_brid  bz_cnst_mstr_external_brid ON bz_cnst_mstr_external_brid.cnst_mstr_id = bz_cnst_org_affil.cnst_mstr_id LEFT JOIN arc_mdm_tbls.arc_srcsys srcsys ON bz_cnst_mstr_external_brid.arc_srcsys_cd = srcsys.arc_srcsys_cd  "; }
           
            // strEntOrgInnerSearchQuery += "( SELECT    DISTINCT affiliator.ent_org_id , srcsys.line_of_service_dtl from arc_orgler_vws.bz_ent_org_srch affiliator";
            if (boolTags == true)
            {
                strEntOrgInnerSearchQuery += " LEFT OUTER JOIN arc_orgler_vws.ent_org_dtl_tags ent_tags_mapping ON affiliator.ent_org_id = ent_tags_mapping.ent_org_id";
            }
            if (boolEntOrgNameInput == true)
            {
                if (boolExcludeTransformations == false)
                {
                    strEntOrgInnerSearchQuery += " LEFT OUTER JOIN arc_orgler_vws.cdim_org_nm_transform cdim_org_nm_transform ON affiliator.ent_org_id  = cdim_org_nm_transform.ent_org_id ";
                }

            }
            

            string strWhereClauseQuery = string.Empty;
            foreach (EnterpriseOrgInputSearchModel entOrgInput in listEnterpriseOrgInputSearch.EnterpriseOrgInputSearchModel)
            {
                string strSingleWhereClause = buildEnterpriseOrgWhereClause(entOrgInput);
                strWhereClauseQuery = addMultipleWhereClauseToQuery(strWhereClauseQuery, strSingleWhereClause);
            }

            //add complete where clause to the query
            if(strWhereClauseQuery==" ()")
            {
                strEntOrgInnerSearchQuery = strWhereClauseQuery == string.Empty ? strEntOrgInnerSearchQuery + " where 1=1 "
                                                   : strEntOrgInnerSearchQuery + " where 1=1 and affiliator.row_stat_cd<>'L' AND cdim_org_nm_transform.transform_condn_typ_cd<>'D' ";
            }
            else
            strEntOrgInnerSearchQuery = strWhereClauseQuery == string.Empty ? strEntOrgInnerSearchQuery + " where 1=1 "
                                    : strEntOrgInnerSearchQuery + " where 1=1 and affiliator.row_stat_cd<>'L' AND cdim_org_nm_transform.transform_condn_typ_cd<>'D' " + " and  " + strWhereClauseQuery;

            strEntOrgSearchQuery += " INNER JOIN (" + strEntOrgInnerSearchQuery + ") affil ON srch.ent_org_id = affil.ent_org_id ";
           
                if (boolIncludeSubordinate == true)
                {
                    strEntOrgSearchQuery += " or sub_map.parent_ent_org_id = affil.ent_org_id ";
                }
                if (boolIncludeSuperior == true)
                {
                    strEntOrgSearchQuery += " or sup_map.ent_org_id = affil.ent_org_id ";
                }
            
            strEntOrgSearchQuery += " QUALIFY RANK() OVER (ORDER BY srch.ent_org_id) <= " + strResultsLimit + " GROUP BY srch.ent_org_id, srch.ent_org_name, srch.ent_org_src_cd, srch.nk_ent_org_id,srch.trans_key, srch.created_by, srch.created_at, srch.last_modified_by, srch.last_modified_at,srch.last_modified_by_all, srch.last_modified_at_all, srch.last_reviewed_by,srch.last_reviewed_at, srch.data_stwrd_usr, srch.dw_srcsys_trans_ts, srch.row_stat_cd,srch.load_id, srch.parent_ent_org_id, transformation_cnt,affil_cnt,naics.naics_cd,naics.sts,naics_ref_cd.naics_indus_title, tags.tag "; 

           

            /*
            strEntOrgSearchQuery = strEntOrgSearchQuery + " where srch.ent_org_id in ( " + strEntOrgInnerSearchQuery + ") ";
            if (boolIncludeSubordinate == true)
            {
                strEntOrgSearchQuery += " or ent_org_rlshp_sub.subord_ent_org_key in ( " + strEntOrgInnerSearchQuery + ") ";
            }
            if (boolIncludeSuperior == true)
            {
                strEntOrgSearchQuery += " or ent_org_rlshp_sup.superior_ent_org_key in ( " + strEntOrgInnerSearchQuery + ") ";
            }
            */
            //add partition by clause only if source system is provided by the user
            // strEntOrgSearchQuery += boolSourceIdInInput ? " QUALIFY ROW_NUMBER() OVER (PARTITION BY affiliator.ent_org_id ORDER BY affiliator.ent_org_id) = 1 ) affiliator " : " ) affiliator ";

            //return back the query
            return strEntOrgSearchQuery;

        }

        public static string buildEnterpriseOrgWhereClause(EnterpriseOrgInputSearchModel entOrgInput)
        {
            string strSpecificWhereClause = string.Empty;
            if (!string.IsNullOrEmpty(entOrgInput.EnterpriseOrgID)) 
                strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " affiliator.ent_org_id = \'" + entOrgInput.EnterpriseOrgID + "\' "
                                                             : strSpecificWhereClause + " and " + " affiliator.ent_org_id = \'" + entOrgInput.EnterpriseOrgID + "\' ";
            if (!string.IsNullOrEmpty(entOrgInput.EnterpriseOrgName))
            {
                if (entOrgInput.ExcludeTransformations == true)
                {
                    strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " affiliator.ent_org_name like \'%" + entOrgInput.EnterpriseOrgName + "%\' "
                                                             : strSpecificWhereClause + " and " + " affiliator.ent_org_name like  \'%" + entOrgInput.EnterpriseOrgName + "%\'";
                }
                else
                {    
                    //strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " ( affiliator.ent_org_name like  \'%" + entOrgInput.EnterpriseOrgName.Replace("'", "''") + "%\' OR (cdim_org_nm_transform.transform_condn_typ_cd <> 'D' AND cdim_org_nm_transform.pattern_match_string LIKE '%" + entOrgInput.EnterpriseOrgName.Replace("'", "''") + "%'))"
                    //                                      : strSpecificWhereClause + " and " + "((affiliator.ent_org_name like  \'%" + entOrgInput.EnterpriseOrgName.Replace("'", "''") + "%\') OR (cdim_org_nm_transform.transform_condn_typ_cd <> 'D' AND cdim_org_nm_transform.pattern_match_string LIKE '%" + entOrgInput.EnterpriseOrgName.Replace("'", "''") + "%'))";
                    strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " ( affiliator.ent_org_name like  \'%" + entOrgInput.EnterpriseOrgName.Replace("'", "''") + "%\' OR (cdim_org_nm_transform.pattern_match_string LIKE '%" + entOrgInput.EnterpriseOrgName.Replace("'", "''") + "%'))"
                                                          : strSpecificWhereClause + " and " + "((affiliator.ent_org_name like  \'%" + entOrgInput.EnterpriseOrgName.Replace("'", "''") + "%\') OR (cdim_org_nm_transform.pattern_match_string LIKE '%" + entOrgInput.EnterpriseOrgName.Replace("'", "''") + "%'))";
                } 
            }
            
            if (entOrgInput.RecentChanges == true)
                strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " affiliator.last_modified_at_all > last_reviewed_at "
                                                             : strSpecificWhereClause + " and " + " affiliator.last_modified_at_all > last_reviewed_at ";
            if (!string.IsNullOrEmpty(entOrgInput.Username))
                strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " affiliator.data_stwrd_usr = \'" + entOrgInput.Username + "\' "
                                                             : strSpecificWhereClause + " and " + " affiliator.data_stwrd_usr = \'" + entOrgInput.Username + "\' ";
            if (entOrgInput.SourceSystem != null)
            {
                if (entOrgInput.SourceSystem.Count > 0)
                {
                    string strSourceSystem = string.Empty;
                    foreach (string source in entOrgInput.SourceSystem)
                    {
                        strSourceSystem = strSourceSystem == string.Empty ? "'" + source + "'" : strSourceSystem + " ,'" + source + "'";
                    }
                    strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " bz_cnst_mstr_external_brid.arc_srcsys_cd in (" + strSourceSystem + ") "
                                                            : strSpecificWhereClause + " and " + " bz_cnst_mstr_external_brid.arc_srcsys_cd in (" + strSourceSystem + ") ";
                }
            }
            if (entOrgInput.ChapterSystem != null)
            {
                if (entOrgInput.ChapterSystem.Count > 0)
                {
                    string strChapterSystem = string.Empty;
                    foreach (string source in entOrgInput.ChapterSystem)
                    {
                        strChapterSystem = strChapterSystem == string.Empty ? "'" + source + "'" : strChapterSystem + " ,'" + source + "'";
                    }
                    strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " bz_cnst_mstr_external_brid.arc_srcsys_cd in (" + strChapterSystem + ") "
                                                            : strSpecificWhereClause + " and " + " bz_cnst_mstr_external_brid.arc_srcsys_cd in (" + strChapterSystem + ") ";
                }
            }
            if (entOrgInput.Tags != null)
            {
                if (entOrgInput.Tags.Count > 0)
                {
                    string strTags = string.Empty;
                    foreach (string tag in entOrgInput.Tags)
                    {
                        strTags = strTags == string.Empty ? "'" + tag + "'": strTags + " ,'" + tag + "'";
                    }
                    strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " tags.tag in (" + strTags + ") "
                                                            : strSpecificWhereClause + " and " + " tags.tag in  (" + strTags + ") ";
                }
            }
            if (entOrgInput.listNaicsCodes != null)
            {
                if (entOrgInput.listNaicsCodes.Count > 0)
                {
                    string strCsvNAICSCodes = string.Empty;
                    foreach (string naicsCode in entOrgInput.listNaicsCodes)
                    {
                        strCsvNAICSCodes = strCsvNAICSCodes == string.Empty ? naicsCode : strCsvNAICSCodes + " ," + naicsCode;
                    }
                    strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " orgler_ent_org_naics_map.naics_cd in (" + strCsvNAICSCodes + ") "
                                                            : strSpecificWhereClause + " and " + " orgler_ent_org_naics_map.naics_cd in (" + strCsvNAICSCodes + ") ";
                }
            }
            if (entOrgInput.RankProvider != null && entOrgInput.RankProvider.Count > 0)
            {
                string strRankProviderPartWhereClause = string.Empty;
                foreach(RankInput rank in entOrgInput.RankProvider)
                {
                    strRankProviderPartWhereClause = strRankProviderPartWhereClause == string.Empty ? " (orgler_org_rnk_ref.org_rnk_publsh_yr = " + rank.Year + " and orgler_org_rnk_ref.org_rnk_prvdr =\'" + rank.Provider + "\')"
                                                        : strRankProviderPartWhereClause + " or " + " (orgler_org_rnk_ref.org_rnk_publsh_yr = " + rank.Year + " and orgler_org_rnk_ref.org_rnk_prvdr =\'" + rank.Provider + "\')";
                }
                strSpecificWhereClause = strSpecificWhereClause == string.Empty ? "(" + strRankProviderPartWhereClause + ")" : strSpecificWhereClause + " and " + "(" + strRankProviderPartWhereClause +  ")";
            }
            if (entOrgInput.RankProvider != null && entOrgInput.RankProvider.Count > 0)
            {
                if (!string.IsNullOrEmpty(entOrgInput.RankFrom))
                    strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " orgler_org_rnk_ref.org_rnk > " + entOrgInput.RankFrom
                                                                 : strSpecificWhereClause + " and " + " orgler_org_rnk_ref.org_rnk > " + entOrgInput.RankFrom;

                if (!string.IsNullOrEmpty(entOrgInput.RankTo))
                    strSpecificWhereClause = strSpecificWhereClause == string.Empty ? " orgler_org_rnk_ref.org_rnk < " + entOrgInput.RankTo
                                                                 : strSpecificWhereClause + " and " + " orgler_org_rnk_ref.org_rnk < " + entOrgInput.RankTo;
            }
            return strSpecificWhereClause;


        }
        public static string addMultipleWhereClauseToQuery(string strCurrentWhereClauseQuery, string strSingleWhereClause)
        {
            string strWhereClauseQuery = string.Empty;
            if (strCurrentWhereClauseQuery == string.Empty)
            {
                strWhereClauseQuery = " (" + strSingleWhereClause + ")";// +"  affiliator";
            }
            else
            {
                if(!String.IsNullOrEmpty(strSingleWhereClause))
                {
                    strWhereClauseQuery = strCurrentWhereClauseQuery + " or ( " + strSingleWhereClause + ")";
                }
               
            }

            return strWhereClauseQuery;
        }
    }
}
