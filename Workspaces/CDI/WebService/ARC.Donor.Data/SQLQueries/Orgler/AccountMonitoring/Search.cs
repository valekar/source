using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Orgler.AccountMonitoring;

namespace ARC.Donor.Data.SQL.Orgler.AccountMonitoring
{
    public class SearchSQL
    {
        /* Method name: getNewAccountSearchResultsSQL
         * Input Parameters: An object of NewAccountsInputModel class
         * Output Parameters: Query in string format after filtering for all the inputs provided
         * Purpose: This method creates the complete search sql for the new account search with applying all the input values */
        public static string getNewAccountSearchResultsSQL(NewAccountsInputModel newAccountInput)
        {
            //set the results limit based on the input criteria
            string strResultsLimit = string.IsNullOrEmpty(newAccountInput.AnswerSetLimit) ? " 500 " : newAccountInput.AnswerSetLimit.ToString();
            
            //string variable to select from the base view. All the search results should not have been confirmed
            string strNewAccountSearchQuery = " select top " + strResultsLimit + " * from arc_orgler_vws.bz_orgler_acc_smry where confirm_ind = 0 ";
            
            //add a filter in the where clause when the created date(from) is provided
            if(!string.IsNullOrEmpty(newAccountInput.createdDateFrom))
                strNewAccountSearchQuery += " and mstr_metric_ts >= \'" + newAccountInput.createdDateFrom + "\' (DATE, FORMAT 'mm/dd/yyyy') ";
            //add a filter in the where clause when the created date(to) is provided
            if (!string.IsNullOrEmpty(newAccountInput.createdDateTo))
                strNewAccountSearchQuery += " and mstr_metric_ts <= \'" + newAccountInput.createdDateTo + "\' (DATE, FORMAT 'mm/dd/yyyy') ";
            
            //add a filter in the where clause when the los input is provided and it is not "All"
            if (!string.IsNullOrEmpty(newAccountInput.los) && newAccountInput.los.ToString().ToLower() != "all")
                strNewAccountSearchQuery += " and line_of_service_cd = \'" + newAccountInput.los + "\'";

            //add a filter in the where clause when the matering type input is provided and it is not "All"
            if (!string.IsNullOrEmpty(newAccountInput.masteringType) && newAccountInput.masteringType.ToString().ToLower() != "all")
            {
                if (newAccountInput.masteringType.ToString().ToLower() == "new")
                    strNewAccountSearchQuery += " and mstrng_typ like 'new master%'";
                else if (newAccountInput.masteringType.ToString().ToLower() == "existing")
                    strNewAccountSearchQuery += " and mstrng_typ like 'merged to existing%'";
            }

            //add a filter in the where clause to search for enterprise org linkage(yes/no/premier) when the input is provided
            if (!string.IsNullOrEmpty(newAccountInput.enterpriseOrgAssociation) && newAccountInput.enterpriseOrgAssociation.ToString().ToLower() != "all")
            {
                if (newAccountInput.enterpriseOrgAssociation.ToString().ToLower() == "enterprise org found")
                    strNewAccountSearchQuery += " and ent_org_id is not null ";
                else if (newAccountInput.enterpriseOrgAssociation.ToString().ToLower() == "no enterprise org")
                    strNewAccountSearchQuery += " and ent_org_id is null ";
                else if (newAccountInput.enterpriseOrgAssociation.ToString().ToLower() == "premier enterprise org found")
                    strNewAccountSearchQuery += " and key_acc_ind = 1 ";
            }

            //add a filter to search for rule keyword when the input is provided. Note that there are three naics codes and thus three rule keywords
            if (newAccountInput.listRuleKeyword != null)
            {
                if (newAccountInput.listRuleKeyword.Count != 0)
                {
                    string strNAICSRuleKeywordString = string.Empty;
                    foreach (string s in newAccountInput.listRuleKeyword)
                    {
                        strNAICSRuleKeywordString = strNAICSRuleKeywordString == string.Empty ? "'" + s + "'" : strNAICSRuleKeywordString + ", '" + s + "'";
                    }
                    strNewAccountSearchQuery += " and (rule_keywrd1 IN (" + strNAICSRuleKeywordString + " ) or rule_keywrd2 IN (" + strNAICSRuleKeywordString + " ) or rule_keywrd3 IN (" + strNAICSRuleKeywordString + " ) )";
                }
            }

            //add a filter to search for naics stewarding status for an account when the input is provided. Note that this is a derived column in the view
            if (!string.IsNullOrEmpty(newAccountInput.naicsStatus) && newAccountInput.naicsStatus.ToString().ToLower() != "all")
            {
                if (newAccountInput.naicsStatus.ToString().ToLower() == "no naics suggestions")
                    strNewAccountSearchQuery += " and naics_stwrd_sts = 'no suggestions' ";
                else if (newAccountInput.naicsStatus.ToString().ToLower() == "stewardship complete")
                    strNewAccountSearchQuery += " and naics_stwrd_sts = 'stewardship complete' ";
                else if (newAccountInput.naicsStatus.ToString().ToLower() == "stewardship underway")
                    strNewAccountSearchQuery += " and naics_stwrd_sts = 'stewardship underway' ";
                else if (newAccountInput.naicsStatus.ToString().ToLower() == "naics suggestions")
                    strNewAccountSearchQuery += " and naics_stwrd_sts = 'suggested' ";
                else if (newAccountInput.naicsStatus.ToString().ToLower() == "all incomplete")
                    strNewAccountSearchQuery += " and naics_stwrd_sts in ('no suggestions', 'stewardship underway', 'suggested') ";
                 }

            //add a filter in the where clause when the enterpriseorgid input is provided 
            if (!string.IsNullOrEmpty(newAccountInput.enterpriseOrgId))
            {
               
                    strNewAccountSearchQuery += " and ent_org_id = \'" + newAccountInput.enterpriseOrgId + "\'";

            }
            //add a filter to search for naics code when the input is provided. Note that there are three naics codes and thus three rule keywords
            if (newAccountInput.listNaicsCodes != null)
            {
                if (newAccountInput.listNaicsCodes.Count != 0)
                {
                    string strNAICSCodeString = string.Empty;
                    if (!newAccountInput.listNaicsCodes.Contains(""))
                    {
                        foreach (string s in newAccountInput.listNaicsCodes)
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                strNAICSCodeString = strNAICSCodeString == string.Empty ? "'" + s + "%'" : strNAICSCodeString + ", '" + s + "'";
                            }
                        }
                        strNewAccountSearchQuery += " and (naics_cd1 LIKE ANY (" + strNAICSCodeString + " ) or naics_cd2 LIKE ANY (" + strNAICSCodeString + " ) or naics_cd3 LIKE ANY (" + strNAICSCodeString + " ) )";
                    }
                    else { }
                }
            }

            //Ordering based on both the monetary value and master id
            strNewAccountSearchQuery += " order by mntry_val desc, cnst_mstr_id desc, line_of_service_cd desc  ;";

            //return the query back to the calling method
            return string.Format(strNewAccountSearchQuery);

        }
        /* Method name: getTopOrgsSearchResultsSQL
     * Input Parameters: An object of TopOrgsInputModel class
     * Output Parameters: Query in string format after filtering for all the inputs provided
     * Purpose: This method creates the complete search sql for the top organization with applying all the input values */
        public static string getTopOrgsSearchResultsSQL(TopOrgsInputModel topOrgsInput)
        {
            //set the results limit based on the input criteria
            string strResultsLimit = string.IsNullOrEmpty(topOrgsInput.AnswerSetLimit) ? " 500 " : topOrgsInput.AnswerSetLimit.ToString();

            //string variable to select from the base view. All the search results should not have been confirmed
            string strTopOrgsSearchQuery = " select top " + strResultsLimit + " * from arc_orgler_vws.bz_orgler_top_acc_srch where 1=1 ";

            

            //add a filter in the where clause when the los input is provided and it is not "All"
            if (!string.IsNullOrEmpty(topOrgsInput.los) && topOrgsInput.los.ToString().ToLower() != "all")
                strTopOrgsSearchQuery += " and line_of_service_cd = \'" + topOrgsInput.los + "\'";

            //add a filter in the where clause when the rfm input is provided 
            if (!string.IsNullOrEmpty(topOrgsInput.rfm_scr) )
                strTopOrgsSearchQuery += " and rfm_scr = '" + topOrgsInput.rfm_scr + "'";

            //add a filter in the where clause to search for enterprise org linkage(yes/no/premier) when the input is provided
            if (!string.IsNullOrEmpty(topOrgsInput.enterpriseOrgAssociation) && topOrgsInput.enterpriseOrgAssociation.ToString().ToLower() != "all")
            {
                if (topOrgsInput.enterpriseOrgAssociation.ToString().ToLower() == "enterprise org found")
                    strTopOrgsSearchQuery += " and ent_org_id is not null ";
                else if (topOrgsInput.enterpriseOrgAssociation.ToString().ToLower() == "no enterprise org")
                    strTopOrgsSearchQuery += " and ent_org_id is null ";
                else if (topOrgsInput.enterpriseOrgAssociation.ToString().ToLower() == "premier enterprise org found")
                    strTopOrgsSearchQuery += " and key_acc_ind = 1 ";
            }

            //add a filter to search for rule keyword when the input is provided. Note that there are three naics codes and thus three rule keywords
            if (topOrgsInput.listRuleKeyword != null)
            {
                if (topOrgsInput.listRuleKeyword.Count != 0)
                {
                    string strNAICSRuleKeywordString = string.Empty;
                    foreach (string s in topOrgsInput.listRuleKeyword)
                    {
                        strNAICSRuleKeywordString = strNAICSRuleKeywordString == string.Empty ? "'" + s + "'" : strNAICSRuleKeywordString + ", '" + s + "'";
                    }
                    strTopOrgsSearchQuery += " and (rule_keywrd1 IN (" + strNAICSRuleKeywordString + " ) or rule_keywrd2 IN (" + strNAICSRuleKeywordString + " ) or rule_keywrd3 IN (" + strNAICSRuleKeywordString + " ) )";
                }
            }
            //add a filter to search for naics code when the input is provided. Note that there are three naics codes and thus three rule keywords
            if (topOrgsInput.listNaicsCodes!= null)
            {
                if (topOrgsInput.listNaicsCodes.Count != 0)
                {
                    string strNAICSCodeString = string.Empty;
                    if (!topOrgsInput.listNaicsCodes.Contains(""))
                    {
                        foreach (string s in topOrgsInput.listNaicsCodes)
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                strNAICSCodeString = strNAICSCodeString == string.Empty ? "'" + s + "%'" : strNAICSCodeString + ", '" + s + "'";
                            }
                        }
                        strTopOrgsSearchQuery += " and (naics_cd1 LIKE ANY (" + strNAICSCodeString + " ) or naics_cd2 LIKE ANY (" + strNAICSCodeString + " ) or naics_cd3 LIKE ANY (" + strNAICSCodeString + " ) )";
                    }
                    else {  }
                    }
            }
            //add a filter to search for naics stewarding status for an account when the input is provided. Note that this is a derived column in the view
            if (!string.IsNullOrEmpty(topOrgsInput.naicsStatus) && topOrgsInput.naicsStatus.ToString().ToLower() != "all")
            {
                if (topOrgsInput.naicsStatus.ToString().ToLower() == "no naics suggestions")
                    strTopOrgsSearchQuery += " and naics_stwrd_sts = 'no suggestions' ";
                else if (topOrgsInput.naicsStatus.ToString().ToLower() == "stewardship complete")
                    strTopOrgsSearchQuery += " and naics_stwrd_sts = 'stewardship complete' ";
                else if (topOrgsInput.naicsStatus.ToString().ToLower() == "stewardship underway")
                    strTopOrgsSearchQuery += " and naics_stwrd_sts = 'stewardship underway' ";
                else if (topOrgsInput.naicsStatus.ToString().ToLower() == "naics suggestions")
                    strTopOrgsSearchQuery += " and naics_stwrd_sts = 'suggested' ";
                else if (topOrgsInput.naicsStatus.ToString().ToLower() == "all incomplete")
                    strTopOrgsSearchQuery += " and naics_stwrd_sts in ('no suggestions', 'stewardship underway', 'suggested') ";
            }

            //Ordering based on both the monetary value and master id
            strTopOrgsSearchQuery += " order by mntry_val desc, cnst_mstr_id desc, line_of_service_cd desc ;";

            //return the query back to the calling method
            return string.Format(strTopOrgsSearchQuery);

        }
    }
}
