using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Constituents;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.SQL
{
    public class ConstituentSQL
    {
        public static string getConstituentAdvSearchResultsSQL(ListConstituentInputSearchModel ListConstituentInputSearchModel)
        {
            //create a string variable for the specific dynamic query
            String strInnerSearchDataQuery = string.Empty;
            String strFinalSearchDataQuery = string.Empty;

            foreach(ConstituentInputSearchModel srch in ListConstituentInputSearchModel.ConstituentInputSearchModel)
            { 
                dynamic srchdyn = createExpandoObjectFrom(srch);

                srchdyn.wherePartClause = string.Empty;

                srchdyn.separateSelect = string.Empty;
            
                Log.Info("Constructing CDI Search Query");

                //foreach (Dictionary<string, string> dictFilter in listFilters)
                //{
                string strSelectPartSpecificClause = string.Empty;
                string strFromPartSpecificClause = string.Empty;
                string strJoinPartSpecificClause = string.Empty;
                string strWherePartSpecificClause = " where 1=1 and data_stwrd.appl_src_cd = \'CDIM\' ";
                string strQualifyOrderByPartSpecificClause = string.Empty;

                //start of the sepcific clause
                strSelectPartSpecificClause += " select data_stwrd.constituent_id as constituent_id, data_stwrd.cnst_dsp_id as cnst_dsp_id, data_stwrd.name as name,data_stwrd.ent_org_name as ent_org_name , data_stwrd.first_name as first_name, data_stwrd.last_name as last_name, data_stwrd.constituent_type as constituent_type,data_stwrd.ent_has_eo as ent_has_eo ,data_stwrd.has_dsp_id as has_dsp_id  ";
                strSelectPartSpecificClause += ",data_stwrd.phone_number as phone_number ";
                strSelectPartSpecificClause += ",data_stwrd.email_address as email_address ";
                strSelectPartSpecificClause += ", data_stwrd.addr_line_1 as addr_line_1, data_stwrd.addr_line_2 as addr_line_2, data_stwrd.city as city, data_stwrd.state_cd as state_cd, data_stwrd.zip as zip ";
                strSelectPartSpecificClause += ", (CASE WHEN COALESCE(addr_line_1,'')<>'' THEN TRIM(addr_line_1) || ' ' ELSE '' END || CASE WHEN COALESCE(addr_line_2,'')<>'' THEN TRIM(addr_line_2) || ' ' ELSE '' END || CASE WHEN COALESCE(city,'')<>'' THEN TRIM(city) || ' ' ELSE '' END || CASE WHEN COALESCE(state_cd,'')<>'' THEN TRIM(state_cd) || ' ' ELSE '' END || CASE WHEN COALESCE(zip,'')<>'' THEN TRIM(zip) ELSE '' END) AS address ";

                strFromPartSpecificClause = " from " + "dw_stuart_vws.stwrd_dnr_prfle data_stwrd ";

                // to decide whether a join to person names or organization names table is required
                if (srchdyn.type.Equals("IN"))
                {
                    // if first name or last name is supplied in the search, then join with person names table
                    List<string> listPersonNames = new List<string>();
                    if (!string.IsNullOrEmpty(srchdyn.firstName))
                    {
                        listPersonNames.Add(srchdyn.firstName.ToString());

                        //dictFilter["first_name"] = dictFilter["first_name"].ToString().Replace("'", "''");

                    }
                    if (!string.IsNullOrEmpty(srchdyn.lastName))
                    {
                        listPersonNames.Add(srchdyn.lastName.ToString());

                        //dictFilter["last_name"] = dictFilter["last_name"].ToString().Replace("'", "''");

                    }
                    if (listPersonNames.Count > 0)
                    {
                        strJoinPartSpecificClause += " inner join arc_mdm_vws.strx_aux_cnst_prsn_nm cnst_prsn_nm on data_stwrd.constituent_id = cnst_prsn_nm.cnst_mstr_id ";

                        // depending on whether first and/or last names are/is supplied, include additional joins
                        strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_prsn_nm.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + ", cnst_prsn_nm.dw_srcsys_trans_ts desc";
                        if ((!string.IsNullOrEmpty(srchdyn.firstName)) || (!string.IsNullOrEmpty(srchdyn.lastName)))
                        {
                            if (!string.IsNullOrEmpty(srchdyn.firstName))
                            {
                                strJoinPartSpecificClause += " and cnst_prsn_nm.bz_cnst_prsn_first_nm like \'%" + srchdyn.firstName.Replace("'","''") + "%\' ";
                            }
                            if (!string.IsNullOrEmpty(srchdyn.lastName))
                            {
                                strJoinPartSpecificClause += " and cnst_prsn_nm.bz_cnst_prsn_last_nm like \'%" + srchdyn.lastName.Replace("'","''") + "%\' ";
                            }
                        }

                    }
                }

                else if (srchdyn.type.Equals( "OR"))
                {
                    List<string> listOrganizationNames = new List<string>();
                    if (!string.IsNullOrEmpty(srchdyn.organization_name))
                    {
                        string orgName = srchdyn.organization_name.ToString();
                        listOrganizationNames.Add(orgName);
                    }
                    // if organization name is supplied in the search, then join with organization names table
                    if (listOrganizationNames.Count > 0)
                    {
                        strJoinPartSpecificClause += " inner join arc_mdm_vws.strx_aux_cnst_org_nm cnst_org_nm on data_stwrd.constituent_id = cnst_org_nm.cnst_mstr_id ";
                        strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_org_nm.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + ", cnst_org_nm.dw_srcsys_trans_ts desc ";

                        if (!string.IsNullOrEmpty(srchdyn.organization_name))
                        {
                            string strOrgName = srchdyn.organization_name;
                            if (srchdyn.organization_name.Contains("'"))
                            {
                                strOrgName = strOrgName.Replace("'", "''");

                                //dictFilter["organization_name"] = strOrgName;

                            }
                            strJoinPartSpecificClause += " and cnst_org_nm.cnst_org_nm like \'%" + strOrgName + "%\' ";

                        }
                    }
                }

                if (!string.IsNullOrEmpty(srchdyn.sourceSystem))
                {
                    if (srchdyn.sourceSystem.ToString() == "LEXN" || srchdyn.sourceSystem.ToString() == "LNHH")
                    {
                        strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_mstr cnst_mstr_ln ";
                        if (srchdyn.sourceSystem.ToString() == "LEXN")
                        {

                            strJoinPartSpecificClause += " on  data_stwrd.constituent_id = cnst_mstr_ln.cnst_mstr_id and cnst_mstr_ln.cnst_dsp_id = '" + srchdyn.sourceSystemId.ToString() + "' ";
                        }
                        if (srchdyn.sourceSystem.ToString() == "LNHH")
                        {
                            strJoinPartSpecificClause += " on  data_stwrd.constituent_id = cnst_mstr_ln.cnst_mstr_id and cnst_mstr_ln.cnst_hsld_id = '" + srchdyn.sourceSystemId.ToString() + "' ";
                        }
                    }
                    else if (srchdyn.sourceSystem.ToString() == "SFFS")
                    {
                        strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_mstr_external_brid.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + " , cnst_mstr_external_brid.dw_srcsys_trans_ts desc ";
                        strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_mstr_external_brid cnst_mstr_external_brid on data_stwrd.constituent_id = cnst_mstr_external_brid.cnst_mstr_id";
                        if (!string.IsNullOrEmpty(srchdyn.sourceSystem))
                        {
                            strJoinPartSpecificClause += " and cnst_mstr_external_brid.arc_srcsys_cd = \'" + srchdyn.sourceSystem + "\' ";
                        }
                        if (!string.IsNullOrEmpty(srchdyn.sourceSystemId))
                        {
                            srchdyn.sourceSystemId = srchdyn.sourceSystemId.ToString().Replace("'", "''");
                            strJoinPartSpecificClause += " and cnst_mstr_external_brid.cnst_srcsys_scndry_id = \'" + srchdyn.sourceSystemId + "\' ";
                        }
                    }
                    else
                    {
                        strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_mstr_external_brid.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + " , cnst_mstr_external_brid.dw_srcsys_trans_ts desc ";
                        /*Keerthana 03-Feb-2015*/

                        strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_mstr_external_brid cnst_mstr_external_brid on data_stwrd.constituent_id = cnst_mstr_external_brid.cnst_mstr_id";
                        if (!string.IsNullOrEmpty(srchdyn.sourceSystem))
                        {
                            strJoinPartSpecificClause += " and cnst_mstr_external_brid.arc_srcsys_cd = \'" + srchdyn.sourceSystem + "\' ";
                        }
                        if (!string.IsNullOrEmpty(srchdyn.sourceSystemId))
                        {
                            srchdyn.sourceSystemId = srchdyn.sourceSystemId.ToString().Replace("'", "''");
                            /*Keerthana 03-Feb-2015

                            //like join for non-SF and equal for SF -- Dixit - 2/12 -2015*/

                            if (srchdyn.sourceSystem.ToString() == "SFFS")
                            {
                                strJoinPartSpecificClause += " and cnst_mstr_external_brid.cnst_srcsys_scndry_id = \'" + srchdyn.sourceSystemId + "\' ";
                            }
                            else
                            {
                                strJoinPartSpecificClause += " and cnst_mstr_external_brid.cnst_srcsys_scndry_id like \'%" + srchdyn.sourceSystemId + "%\' ";
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(srchdyn.chapterSystem))
                {
                    //strSelectPartSpecificClause = strSelectPartSpecificClause.Replace(" , null as source_system_id, null as source_system_code ", " , cnst_mstr_external_brid.cnst_srcsys_id as source_system_id, cnst_mstr_external_brid.arc_srcsys_cd as source_system_code ");
                    strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_mstr_external_brid.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + " , cnst_mstr_external_brid.dw_srcsys_trans_ts desc ";
                    /*Keerthana 03-Feb-2015*/

                    strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_mstr_external_brid cnst_mstr_external_brid on data_stwrd.constituent_id = cnst_mstr_external_brid.cnst_mstr_id";

                    if (!string.IsNullOrEmpty(srchdyn.chapterSystem))
                    {
                        strJoinPartSpecificClause += " and cnst_mstr_external_brid.arc_srcsys_cd = \'" + srchdyn.chapterSystem + "\' ";
                    }
                    if (!string.IsNullOrEmpty(srchdyn.sourceSystemId))
                    {
                        srchdyn.sourceSystemId = srchdyn.sourceSystemId.ToString().Replace("'", "''");
                        /*Keerthana 03-Feb-2015*/
                        strJoinPartSpecificClause += " and cnst_mstr_external_brid.cnst_srcsys_scndry_id like \'%" + srchdyn.sourceSystemId + "%\' ";

                    }
                }

                if (!string.IsNullOrEmpty(srchdyn.sourceSystemId))
                {
                    if(string.IsNullOrEmpty(srchdyn.chapterSystem) && string.IsNullOrEmpty(srchdyn.sourceSystem))
                    {
                        strWherePartSpecificClause += " AND 1=0 ";
                    }
                }

                if (!string.IsNullOrEmpty(srchdyn.chapterSystem) || !string.IsNullOrEmpty(srchdyn.sourceSystem)) 
                {
                    if (string.IsNullOrEmpty(srchdyn.sourceSystemId))
                    {
                        strWherePartSpecificClause += " AND 1=0 ";
                    }
                }

                List<string> listPhones = new List<string>();
                if (!string.IsNullOrEmpty(srchdyn.phone))
                {
                    listPhones.Add(srchdyn.phone.ToString());
                    // dictFilter["phone_number"] = dictFilter["phone_number"].ToString().Replace("'", "''");
                }
                // joining with phone numbers table, only when a phone number is supplied
                if (listPhones.Count > 0)
                {
                    strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_phn.dw_srcsys_trans_ts desc" : strQualifyOrderByPartSpecificClause + ", cnst_phn.dw_srcsys_trans_ts desc ";

                    //strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_phn_typ2 cnst_phn on data_stwrd.constituent_id = cnst_phn.cnst_mstr_id and cnst_phn.cnst_phn_num like \'%" + strPhoneNumber + "%\'";

                    strJoinPartSpecificClause += " inner join arc_mdm_vws.strx_aux_cnst_phn_typ2 cnst_phn on data_stwrd.constituent_id = cnst_phn.cnst_mstr_id ";
                    if (!string.IsNullOrEmpty(srchdyn.phone))
                    {
                        strJoinPartSpecificClause += " and cnst_phn.cnst_phn_num like \'%" + srchdyn.phone + "%\' ";
                    }
                }

                // joining with email address table, only when an email address is supplied
                List<string> listEmails = new List<string>();
                if (!string.IsNullOrEmpty(srchdyn.emailAddress))
                {
                    listEmails.Add(srchdyn.emailAddress.ToString());
                    //dictFilter["email_address"] = dictFilter["email_address"].ToString().Replace("'", "''");
                }

                if (listEmails.Count > 0)
                {
                    strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_email.dw_srcsys_trans_ts desc" : strQualifyOrderByPartSpecificClause + ", cnst_email.dw_srcsys_trans_ts desc ";

                    strJoinPartSpecificClause += " inner join arc_mdm_vws.strx_aux_cnst_email_typ2 cnst_email on data_stwrd.constituent_id = cnst_email.cnst_mstr_id ";
                    if (!string.IsNullOrEmpty(srchdyn.emailAddress))
                    {
                        strJoinPartSpecificClause += " and cnst_email.cnst_email_addr like \'%" + srchdyn.emailAddress + "%\' ";
                    }
                }

                //address check
                List<string> listAddresses = new List<string>();
                if (!string.IsNullOrEmpty(srchdyn.addressLine))
                {
                    listAddresses.Add(srchdyn.addressLine.ToString());

                    //dictFilter["address_line"] = dictFilter["address_line"].ToString().Replace("'", "''");
                }
                if (!string.IsNullOrEmpty(srchdyn.city))
                {
                    listAddresses.Add(srchdyn.city.ToString());

                    //dictFilter["city"] = dictFilter["city"].ToString().Replace("'", "''");
                }

                if (!string.IsNullOrEmpty(srchdyn.state))
                {
                    listAddresses.Add(srchdyn.state.ToString());
                }

                if (!string.IsNullOrEmpty(srchdyn.zip))
                {
                    listAddresses.Add(srchdyn.zip.ToString());
                    //dictFilter["zip"] = dictFilter["zip"].ToString().Replace("'", "''");
                }
                // joining with the address table, only when an address criteria is provided
                if (listAddresses.Count > 0)
                {
                    strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_addr.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + ", cnst_addr.dw_srcsys_trans_ts desc ";

                    strJoinPartSpecificClause += " inner join arc_mdm_vws.strx_aux_cnst_addr  cnst_addr on data_stwrd.constituent_id = cnst_addr.cnst_mstr_id ";
                    if (!string.IsNullOrEmpty(srchdyn.addressLine))
                    {
                        strJoinPartSpecificClause += " and ( cnst_addr.bz_cnst_addr_line1_addr like \'%" + srchdyn.addressLine.Replace("'", "''") + "%\' or cnst_addr.bz_cnst_addr_line2_addr like \'%" + srchdyn.addressLine.Replace("'", "''") + "%\') ";
                    }
                    if (!string.IsNullOrEmpty(srchdyn.city))
                    {
                        strJoinPartSpecificClause += " and cnst_addr.bz_cnst_addr_city_nm like \'%" + srchdyn.city + "%\' ";
                    }
                    if (!string.IsNullOrEmpty(srchdyn.state))
                    {
                        strJoinPartSpecificClause += " and cnst_addr.cnst_addr_state_cd = \'" + srchdyn.state + "\' ";
                    }
                    if (!string.IsNullOrEmpty(srchdyn.zip))
                    {
                        strJoinPartSpecificClause += " and cnst_addr.cnst_addr_zip_5_cd = \'" + srchdyn.zip + "\' ";
                    }
                }

                strWherePartSpecificClause += " and constituent_type = \'" + srchdyn.type + "\' ";

                List<string> listIds = new List<string>();
                if (!string.IsNullOrEmpty(srchdyn.masterId))
                {
                    listIds.Add(srchdyn.masterId.ToString());

                    //dictFilter["master_id"] = dictFilter["master_id"].ToString().Replace("'", "");
                }

                if (listIds.Count > 0)
                {
                    if (!strJoinPartSpecificClause.Contains("cnst_mstr_ln"))
                    {
                        strWherePartSpecificClause += " AND data_stwrd.row_stat_cd <> \'L\' ";
                        if (!string.IsNullOrEmpty(srchdyn.masterId))
                        {
                            if (!string.IsNullOrEmpty(srchdyn.wherePartClause))
                            {
                                srchdyn.wherePartClause += " and data_stwrd.constituent_id in ( sel data_stwrd.constituent_id from dw_stuart_vws.stwrd_dnr_prfle data_stwrd  where data_stwrd.constituent_id = \'" + srchdyn.masterId.ToString() + "\'  union  select data_stwrd.constituent_id from dw_stuart_vws.stwrd_dnr_prfle data_stwrd inner join dw_stuart_vws.cnst_mstr_id_map cnst_mstr_id_map ON data_stwrd.constituent_id = cnst_mstr_id_map.new_cnst_mstr_id  and ( cnst_mstr_id_map.constituent_id = \'" + srchdyn.masterId.ToString() + "\'  )) ";
                            }
                            else
                            {
                                srchdyn.wherePartClause = " data_stwrd.constituent_id in ( sel data_stwrd.constituent_id from dw_stuart_vws.stwrd_dnr_prfle data_stwrd  where data_stwrd.constituent_id = \'" + srchdyn.masterId.ToString() + "\'  union  select data_stwrd.constituent_id from dw_stuart_vws.stwrd_dnr_prfle data_stwrd inner join dw_stuart_vws.cnst_mstr_id_map cnst_mstr_id_map ON data_stwrd.constituent_id = cnst_mstr_id_map.new_cnst_mstr_id  and ( cnst_mstr_id_map.constituent_id = \'" + srchdyn.masterId.ToString() + "\'  )) ";
                            }
                        }
                    }
                }

                // set qualify, only if required
                string strQualifyClause = string.Empty;
                if (strQualifyOrderByPartSpecificClause != string.Empty)
                {
                    strQualifyClause = " qualify row_number() over (partition by data_stwrd.constituent_id order by " + strQualifyOrderByPartSpecificClause + " ) <=1";
                }

                if (!string.IsNullOrEmpty(srchdyn.wherePartClause))
                {
                    string strParticularWhereClause = srchdyn.wherePartClause.ToString();
                    if (strWherePartSpecificClause == string.Empty)
                    {
                        strWherePartSpecificClause = "( " + strParticularWhereClause + ")";
                    }
                    else
                    {
                        strWherePartSpecificClause = strWherePartSpecificClause + " and ( " + strParticularWhereClause + ")";
                    }
                }

                strInnerSearchDataQuery = " " + strSelectPartSpecificClause + strFromPartSpecificClause + strJoinPartSpecificClause + strWherePartSpecificClause + strQualifyClause;

                if (!string.IsNullOrEmpty(srchdyn.separateSelect))
                {
                    srchdyn.separateSelect += strInnerSearchDataQuery;
                }
                else
                {
                    srchdyn.separateSelect = strInnerSearchDataQuery;
                }

                if (!string.IsNullOrEmpty(srchdyn.separateSelect))
                {
                    string strParticularSelectClause = srchdyn.separateSelect.ToString();
                    if (strFinalSearchDataQuery == string.Empty)
                    {
                        strFinalSearchDataQuery = "( " + strParticularSelectClause + ")";
                    }
                    else
                    {
                        strFinalSearchDataQuery = strFinalSearchDataQuery + " union  ( " + strParticularSelectClause + ")";
                    }
                }
            }

            //order by query
            string strResultOrderByClause = string.Empty;
            if (ListConstituentInputSearchModel.ConstituentInputSearchModel[0].type.Equals("IN"))
            {
                strResultOrderByClause = " last_name DESC, first_name DESC, constituent_id ASC ";
            }
            else
            {
                strResultOrderByClause = " name DESC, constituent_id ASC ";
            }

            //always left join on transaction table

           //string strTransSelectPartSpecificClause = " ,coalesce(trans.trans_key,-1) as request_transaction_key ";
           // string strTransJoinPartSpecificClause = " left outer join ( SELECT trans_cnst.cnst_id,trans_cnst.trans_key FROM dw_stuart_vws.trans_cnst trans_cnst inner join  dw_stuart_vws.trans trans ON  trans_cnst.trans_key=trans.trans_key WHERE trans.trans_typ_id <> 4 ) trans_constituent on x.constituent_id = trans_constituent.cnst_id left outer join dw_stuart_vws.trans trans on trans_constituent.trans_key = trans.trans_key and trans.trans_stat in ('In Progress','Waiting for approval') and trans.sub_trans_typ_id in ( select distinct sub_trans_typ_id from dw_stuart_vws.sub_trans_typ sub_trans_typ where sub_trans_typ.sub_trans_typ_dsc in ('merge','do not merge')) ";
           // string strTransQualifyOrderByPartSpecificClause = " , trans.trans_key desc ";

            // construct the final query that must be issued

            string topcount = ListConstituentInputSearchModel.AnswerSetLimit;
            //String strFinalQuery = "select top " + topcount + "  query.* from ( select x.* " + strTransSelectPartSpecificClause + " from ( " + strFinalSearchDataQuery + ") x " + strTransJoinPartSpecificClause + "qualify row_number() over (partition by x.constituent_id order by x.constituent_id " + strTransQualifyOrderByPartSpecificClause + " ) <=1 ) query order by " + strResultOrderByClause + " ; ";
            String strFinalQuery = "select top " + topcount + "  query.* from ( select  x.*  from ( " + strFinalSearchDataQuery + ") x ) query order by " + strResultOrderByClause + " ; ";

            //Log.Info("CDI Search Query Constructed: " + strFinalQuery.ToString());

            return strFinalQuery;
        
        }

        //Chiranjib 18/04/2016 - To add dynamic properties to the received model object 
        private static ExpandoObject createExpandoObjectFrom(object source)
        {
            var result = new ExpandoObject();
            IDictionary<string, object> dictionary = result;
            foreach(var property in source.GetType().GetProperties().Where(p => p.GetMethod.IsPublic))
            {
                dictionary[property.Name] = property.GetValue(source);
            }
            return result;
        }

        public static string getBridgeCount(string strResultMasterIdsCSV)
        {
            string strMasterCountQuery = " select * from dw_stuart_vws.strx_cnst_bridge_cnt where cnst_mstr_id in (" + strResultMasterIdsCSV + ")";
            return strMasterCountQuery;
        }

    }
}

//without aux tables

/*
using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Constituents;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.SQL
{
    public class ConstituentSQL
    {
        public static string getConstituentAdvSearchResultsSQL(ListConstituentInputSearchModel ListConstituentInputSearchModel)
        {
            //create a string variable for the specific dynamic query
            String strInnerSearchDataQuery = string.Empty;
            String strFinalSearchDataQuery = string.Empty;

            foreach(ConstituentInputSearchModel srch in ListConstituentInputSearchModel.ConstituentInputSearchModel)
            { 
                dynamic srchdyn = createExpandoObjectFrom(srch);

                srchdyn.wherePartClause = string.Empty;

                srchdyn.separateSelect = string.Empty;
            
                Log.Info("Constructing CDI Search Query");

                //foreach (Dictionary<string, string> dictFilter in listFilters)
                //{
                string strSelectPartSpecificClause = string.Empty;
                string strFromPartSpecificClause = string.Empty;
                string strJoinPartSpecificClause = string.Empty;
                string strWherePartSpecificClause = " where 1=1 and data_stwrd.appl_src_cd = \'CDIM\' ";
                string strQualifyOrderByPartSpecificClause = string.Empty;

                //start of the sepcific clause
                strSelectPartSpecificClause += " select data_stwrd.constituent_id as constituent_id, data_stwrd.cnst_dsp_id as cnst_dsp_id, data_stwrd.name as name, data_stwrd.first_name as first_name, data_stwrd.last_name as last_name, data_stwrd.constituent_type as constituent_type ";
                strSelectPartSpecificClause += ",data_stwrd.phone_number as phone_number ";
                strSelectPartSpecificClause += ",data_stwrd.email_address as email_address ";
                strSelectPartSpecificClause += ", data_stwrd.addr_line_1 as addr_line_1, data_stwrd.addr_line_2 as addr_line_2, data_stwrd.city as city, data_stwrd.state_cd as state_cd, data_stwrd.zip as zip ";
                strSelectPartSpecificClause += ", (CASE WHEN COALESCE(addr_line_1,'')<>'' THEN TRIM(addr_line_1) || ' ' ELSE '' END || CASE WHEN COALESCE(addr_line_2,'')<>'' THEN TRIM(addr_line_2) || ' ' ELSE '' END || CASE WHEN COALESCE(city,'')<>'' THEN TRIM(city) || ' ' ELSE '' END || CASE WHEN COALESCE(state_cd,'')<>'' THEN TRIM(state_cd) || ' ' ELSE '' END || CASE WHEN COALESCE(zip,'')<>'' THEN TRIM(zip) ELSE '' END) AS address ";

                strFromPartSpecificClause = " from " + "dw_stuart_vws.stwrd_dnr_prfle data_stwrd ";

                // to decide whether a join to person names or organization names table is required
                if (srchdyn.type.Equals("IN"))
                {
                    // if first name or last name is supplied in the search, then join with person names table
                    List<string> listPersonNames = new List<string>();
                    if (!string.IsNullOrEmpty(srchdyn.firstName))
                    {
                        listPersonNames.Add(srchdyn.firstName.ToString());

                        //dictFilter["first_name"] = dictFilter["first_name"].ToString().Replace("'", "''");

                    }
                    if (!string.IsNullOrEmpty(srchdyn.lastName))
                    {
                        listPersonNames.Add(srchdyn.lastName.ToString());

                        //dictFilter["last_name"] = dictFilter["last_name"].ToString().Replace("'", "''");

                    }
                    if (listPersonNames.Count > 0)
                    {
                        strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_prsn_nm cnst_prsn_nm on data_stwrd.constituent_id = cnst_prsn_nm.cnst_mstr_id ";

                        // depending on whether first and/or last names are/is supplied, include additional joins
                        strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_prsn_nm.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + ", cnst_prsn_nm.dw_srcsys_trans_ts desc";
                        if ((!string.IsNullOrEmpty(srchdyn.firstName)) || (!string.IsNullOrEmpty(srchdyn.lastName)))
                        {
                            if (!string.IsNullOrEmpty(srchdyn.firstName))
                            {
                                strJoinPartSpecificClause += " and cnst_prsn_nm.bz_cnst_prsn_first_nm like \'%" + srchdyn.firstName + "%\' ";
                            }
                            if (!string.IsNullOrEmpty(srchdyn.lastName))
                            {
                                strJoinPartSpecificClause += " and cnst_prsn_nm.bz_cnst_prsn_last_nm like \'%" + srchdyn.lastName + "%\' ";
                            }
                        }

                    }
                }

                else if (srchdyn.type.Equals( "OR"))
                {
                    List<string> listOrganizationNames = new List<string>();
                    if (!string.IsNullOrEmpty(srchdyn.organization_name))
                    {
                        listOrganizationNames.Add(srchdyn.organization_name.ToString());
                    }
                    // if organization name is supplied in the search, then join with organization names table
                    if (listOrganizationNames.Count > 0)
                    {
                        strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_org_nm cnst_org_nm on data_stwrd.constituent_id = cnst_org_nm.cnst_mstr_id ";
                        strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_org_nm.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + ", cnst_org_nm.dw_srcsys_trans_ts desc ";

                        if (!string.IsNullOrEmpty(srchdyn.organization_name))
                        {
                            if (srchdyn.organization_name.Contains("'"))
                            {
                                string strOrgName = srchdyn.organization_name.Replace("'", "''");

                                //dictFilter["organization_name"] = strOrgName;

                            }
                            strJoinPartSpecificClause += " and cnst_org_nm.cnst_org_nm like \'%" + srchdyn.organization_name + "%\' ";

                        }
                    }
                }

                if (!string.IsNullOrEmpty(srchdyn.sourceSystem))
                {
                    if (srchdyn.sourceSystem.ToString() == "LEXN" || srchdyn.sourceSystem.ToString() == "LNHH")
                    {
                        strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_mstr cnst_mstr_ln ";
                        if (srchdyn.sourceSystem.ToString() == "LEXN")
                        {

                            strJoinPartSpecificClause += " on  data_stwrd.constituent_id = cnst_mstr_ln.cnst_mstr_id and cnst_mstr_ln.cnst_dsp_id = '" + srchdyn.sourceSystemId.ToString() + "' ";
                        }
                        if (srchdyn.sourceSystem.ToString() == "LNHH")
                        {
                            strJoinPartSpecificClause += " on  data_stwrd.constituent_id = cnst_mstr_ln.cnst_mstr_id and cnst_mstr_ln.cnst_hsld_id = '" + srchdyn.sourceSystemId.ToString() + "' ";
                        }
                    }
                    else if (srchdyn.sourceSystem.ToString() == "SFFS")
                    {
                        strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_mstr_external_brid.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + " , cnst_mstr_external_brid.dw_srcsys_trans_ts desc ";
                        strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_mstr_external_brid cnst_mstr_external_brid on data_stwrd.constituent_id = cnst_mstr_external_brid.cnst_mstr_id";
                        if (!string.IsNullOrEmpty(srchdyn.sourceSystem))
                        {
                            strJoinPartSpecificClause += " and cnst_mstr_external_brid.arc_srcsys_cd = \'" + srchdyn.sourceSystem + "\' ";
                        }
                        if (!string.IsNullOrEmpty(srchdyn.sourceSystemId))
                        {
                            srchdyn.sourceSystemId = srchdyn.sourceSystemId.ToString().Replace("'", "''");
                            strJoinPartSpecificClause += " and cnst_mstr_external_brid.cnst_srcsys_scndry_id = \'" + srchdyn.sourceSystemId + "\' ";
                        }
                    }
                    else
                    {
                        strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_mstr_external_brid.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + " , cnst_mstr_external_brid.dw_srcsys_trans_ts desc ";
                        //Keerthana 03-Feb-2015

                        strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_mstr_external_brid cnst_mstr_external_brid on data_stwrd.constituent_id = cnst_mstr_external_brid.cnst_mstr_id";
                        if (!string.IsNullOrEmpty(srchdyn.sourceSystem))
                        {
                            strJoinPartSpecificClause += " and cnst_mstr_external_brid.arc_srcsys_cd = \'" + srchdyn.sourceSystem + "\' ";
                        }
                        if (!string.IsNullOrEmpty(srchdyn.sourceSystemId))
                        {
                            srchdyn.sourceSystemId = srchdyn.sourceSystemId.ToString().Replace("'", "''");
                            /*Keerthana 03-Feb-2015

                            //like join for non-SF and equal for SF -- Dixit - 2/12 -2015

                            if (srchdyn.sourceSystem.ToString() == "SFFS")
                            {
                                strJoinPartSpecificClause += " and cnst_mstr_external_brid.cnst_srcsys_scndry_id = \'" + srchdyn.sourceSystemId + "\' ";
                            }
                            else
                            {
                                strJoinPartSpecificClause += " and cnst_mstr_external_brid.cnst_srcsys_scndry_id like \'%" + srchdyn.sourceSystemId + "%\' ";
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(srchdyn.chapterSystem))
                {
                    //strSelectPartSpecificClause = strSelectPartSpecificClause.Replace(" , null as source_system_id, null as source_system_code ", " , cnst_mstr_external_brid.cnst_srcsys_id as source_system_id, cnst_mstr_external_brid.arc_srcsys_cd as source_system_code ");
                    strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_mstr_external_brid.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + " , cnst_mstr_external_brid.dw_srcsys_trans_ts desc ";
                    //Keerthana 03-Feb-2015

                    strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_mstr_external_brid cnst_mstr_external_brid on data_stwrd.constituent_id = cnst_mstr_external_brid.cnst_mstr_id";

                    if (!string.IsNullOrEmpty(srchdyn.chapterSystem))
                    {
                        strJoinPartSpecificClause += " and cnst_mstr_external_brid.arc_srcsys_cd = \'" + srchdyn.chapterSystem + "\' ";
                    }
                    if (!string.IsNullOrEmpty(srchdyn.sourceSystemId))
                    {
                        srchdyn.sourceSystemId = srchdyn.sourceSystemId.ToString().Replace("'", "''");
                        //Keerthana 03-Feb-2015
                        strJoinPartSpecificClause += " and cnst_mstr_external_brid.cnst_srcsys_scndry_id like \'%" + srchdyn.sourceSystemId + "%\' ";

                    }
                }

                if (!string.IsNullOrEmpty(srchdyn.sourceSystemId))
                {
                    if(string.IsNullOrEmpty(srchdyn.chapterSystem) && string.IsNullOrEmpty(srchdyn.sourceSystem))
                    {
                        strWherePartSpecificClause += " AND 1=0 ";
                    }
                }

                if (!string.IsNullOrEmpty(srchdyn.chapterSystem) || !string.IsNullOrEmpty(srchdyn.sourceSystem)) 
                {
                    if (string.IsNullOrEmpty(srchdyn.sourceSystemId))
                    {
                        strWherePartSpecificClause += " AND 1=0 ";
                    }
                }

                List<string> listPhones = new List<string>();
                if (!string.IsNullOrEmpty(srchdyn.phone))
                {
                    listPhones.Add(srchdyn.phone.ToString());
                    // dictFilter["phone_number"] = dictFilter["phone_number"].ToString().Replace("'", "''");
                }
                // joining with phone numbers table, only when a phone number is supplied
                if (listPhones.Count > 0)
                {
                    strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_phn.dw_srcsys_trans_ts desc" : strQualifyOrderByPartSpecificClause + ", cnst_phn.dw_srcsys_trans_ts desc ";

                    //strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_phn_typ2 cnst_phn on data_stwrd.constituent_id = cnst_phn.cnst_mstr_id and cnst_phn.cnst_phn_num like \'%" + strPhoneNumber + "%\'";
                
                    strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_phn_typ2 cnst_phn on data_stwrd.constituent_id = cnst_phn.cnst_mstr_id ";
                    if (!string.IsNullOrEmpty(srchdyn.phone))
                    {
                        strJoinPartSpecificClause += " and cnst_phn.cnst_phn_num like \'%" + srchdyn.phone + "%\' ";
                    }
                }

                // joining with email address table, only when an email address is supplied
                List<string> listEmails = new List<string>();
                if (!string.IsNullOrEmpty(srchdyn.emailAddress))
                {
                    listEmails.Add(srchdyn.emailAddress.ToString());
                    //dictFilter["email_address"] = dictFilter["email_address"].ToString().Replace("'", "''");
                }

                if (listEmails.Count > 0)
                {
                    strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_email.dw_srcsys_trans_ts desc" : strQualifyOrderByPartSpecificClause + ", cnst_email.dw_srcsys_trans_ts desc ";

                    strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_email_typ2 cnst_email on data_stwrd.constituent_id = cnst_email.cnst_mstr_id ";
                    if (!string.IsNullOrEmpty(srchdyn.emailAddress))
                    {
                        strJoinPartSpecificClause += " and cnst_email.cnst_email_addr like \'%" + srchdyn.emailAddress + "%\' ";
                    }
                }

                //address check
                List<string> listAddresses = new List<string>();
                if (!string.IsNullOrEmpty(srchdyn.addressLine))
                {
                    listAddresses.Add(srchdyn.addressLine.ToString());

                    //dictFilter["address_line"] = dictFilter["address_line"].ToString().Replace("'", "''");
                }
                if (!string.IsNullOrEmpty(srchdyn.city))
                {
                    listAddresses.Add(srchdyn.city.ToString());

                    //dictFilter["city"] = dictFilter["city"].ToString().Replace("'", "''");
                }

                if (!string.IsNullOrEmpty(srchdyn.state))
                {
                    listAddresses.Add(srchdyn.state.ToString());
                }

                if (!string.IsNullOrEmpty(srchdyn.zip))
                {
                    listAddresses.Add(srchdyn.zip.ToString());

                    //dictFilter["zip"] = dictFilter["zip"].ToString().Replace("'", "''");
                }
                // joining with the address table, only when an address criteria is provided
                if (listAddresses.Count > 0)
                {
                    strQualifyOrderByPartSpecificClause = strQualifyOrderByPartSpecificClause == string.Empty ? " cnst_addr.dw_srcsys_trans_ts desc " : strQualifyOrderByPartSpecificClause + ", cnst_addr.dw_srcsys_trans_ts desc ";

                    strJoinPartSpecificClause += " inner join arc_mdm_vws.bz_cnst_addr cnst_addr on data_stwrd.constituent_id = cnst_addr.cnst_mstr_id ";
                    if (!string.IsNullOrEmpty(srchdyn.addressLine))
                    {
                        strJoinPartSpecificClause += " and ( cnst_addr.bz_cnst_addr_line1_addr like \'%" + srchdyn.addressLine + "%\' or cnst_addr.bz_cnst_addr_line2_addr like \'%" + srchdyn.addressLine + "%\') ";
                    }
                    if (!string.IsNullOrEmpty(srchdyn.city))
                    {
                        strJoinPartSpecificClause += " and cnst_addr.bz_cnst_addr_city_nm like \'%" + srchdyn.city + "%\' ";
                    }
                    if (!string.IsNullOrEmpty(srchdyn.state))
                    {
                        strJoinPartSpecificClause += " and cnst_addr.cnst_addr_state_cd = \'" + srchdyn.state + "\' ";
                    }
                    if (!string.IsNullOrEmpty(srchdyn.zip))
                    {
                        strJoinPartSpecificClause += " and cnst_addr.cnst_addr_zip_5_cd = \'" + srchdyn.zip + "\' ";
                    }
                }

                strWherePartSpecificClause += " and constituent_type = \'" + srchdyn.type + "\' ";

                List<string> listIds = new List<string>();
                if (!string.IsNullOrEmpty(srchdyn.masterId))
                {
                    listIds.Add(srchdyn.masterId.ToString());

                    //dictFilter["master_id"] = dictFilter["master_id"].ToString().Replace("'", "");
                }

                if (listIds.Count > 0)
                {
                    if (!strJoinPartSpecificClause.Contains("cnst_mstr_ln"))
                    {
                        strWherePartSpecificClause += " AND data_stwrd.row_stat_cd <> \'L\' ";
                        if (!string.IsNullOrEmpty(srchdyn.masterId))
                        {
                            if (!string.IsNullOrEmpty(srchdyn.wherePartClause))
                            {
                                srchdyn.wherePartClause += " and data_stwrd.constituent_id in ( sel data_stwrd.constituent_id from dw_stuart_vws.stwrd_dnr_prfle data_stwrd  where data_stwrd.constituent_id = \'" + srchdyn.masterId.ToString() + "\'  union all select data_stwrd.constituent_id from dw_stuart_vws.stwrd_dnr_prfle data_stwrd inner join dw_stuart_vws.cnst_mstr_id_map cnst_mstr_id_map ON data_stwrd.constituent_id = cnst_mstr_id_map.new_cnst_mstr_id  and ( cnst_mstr_id_map.constituent_id = \'" + srchdyn.masterId.ToString() + "\'  )) ";
                            }
                            else
                            {
                                srchdyn.wherePartClause = " data_stwrd.constituent_id in ( sel data_stwrd.constituent_id from dw_stuart_vws.stwrd_dnr_prfle data_stwrd  where data_stwrd.constituent_id = \'" + srchdyn.masterId.ToString() + "\'  union all select data_stwrd.constituent_id from dw_stuart_vws.stwrd_dnr_prfle data_stwrd inner join dw_stuart_vws.cnst_mstr_id_map cnst_mstr_id_map ON data_stwrd.constituent_id = cnst_mstr_id_map.new_cnst_mstr_id  and ( cnst_mstr_id_map.constituent_id = \'" + srchdyn.masterId.ToString() + "\'  )) ";
                            }
                        }
                    }
                }

                // set qualify, only if required
                string strQualifyClause = string.Empty;
                if (strQualifyOrderByPartSpecificClause != string.Empty)
                {
                    strQualifyClause = " qualify row_number() over (partition by data_stwrd.constituent_id order by " + strQualifyOrderByPartSpecificClause + " ) <=1";
                }

                if (!string.IsNullOrEmpty(srchdyn.wherePartClause))
                {
                    string strParticularWhereClause = srchdyn.wherePartClause.ToString();
                    if (strWherePartSpecificClause == string.Empty)
                    {
                        strWherePartSpecificClause = "( " + strParticularWhereClause + ")";
                    }
                    else
                    {
                        strWherePartSpecificClause = strWherePartSpecificClause + " and ( " + strParticularWhereClause + ")";
                    }
                }

                strInnerSearchDataQuery = " " + strSelectPartSpecificClause + strFromPartSpecificClause + strJoinPartSpecificClause + strWherePartSpecificClause + strQualifyClause;

                if (!string.IsNullOrEmpty(srchdyn.separateSelect))
                {
                    srchdyn.separateSelect += strInnerSearchDataQuery;
                }
                else
                {
                    srchdyn.separateSelect = strInnerSearchDataQuery;
                }

                if (!string.IsNullOrEmpty(srchdyn.separateSelect))
                {
                    string strParticularSelectClause = srchdyn.separateSelect.ToString();
                    if (strFinalSearchDataQuery == string.Empty)
                    {
                        strFinalSearchDataQuery = "( " + strParticularSelectClause + ")";
                    }
                    else
                    {
                        strFinalSearchDataQuery = strFinalSearchDataQuery + " union all ( " + strParticularSelectClause + ")";
                    }
                }
            }

            //order by query
            string strResultOrderByClause = string.Empty;
            if (ListConstituentInputSearchModel.ConstituentInputSearchModel[0].type.Equals("IN"))
            {
                strResultOrderByClause = " last_name DESC, first_name DESC, constituent_id ASC ";
            }
            else
            {
                strResultOrderByClause = " name DESC, constituent_id ASC ";
            }

            //always left join on transaction table

           //string strTransSelectPartSpecificClause = " ,coalesce(trans.trans_key,-1) as request_transaction_key ";
           // string strTransJoinPartSpecificClause = " left outer join ( SELECT trans_cnst.cnst_id,trans_cnst.trans_key FROM dw_stuart_vws.trans_cnst trans_cnst inner join  dw_stuart_vws.trans trans ON  trans_cnst.trans_key=trans.trans_key WHERE trans.trans_typ_id <> 4 ) trans_constituent on x.constituent_id = trans_constituent.cnst_id left outer join dw_stuart_vws.trans trans on trans_constituent.trans_key = trans.trans_key and trans.trans_stat in ('In Progress','Waiting for approval') and trans.sub_trans_typ_id in ( select distinct sub_trans_typ_id from dw_stuart_vws.sub_trans_typ sub_trans_typ where sub_trans_typ.sub_trans_typ_dsc in ('merge','do not merge')) ";
           // string strTransQualifyOrderByPartSpecificClause = " , trans.trans_key desc ";

            // construct the final query that must be issued

            string topcount = ListConstituentInputSearchModel.AnswerSetLimit;
            //String strFinalQuery = "select top " + topcount + "  query.* from ( select x.* " + strTransSelectPartSpecificClause + " from ( " + strFinalSearchDataQuery + ") x " + strTransJoinPartSpecificClause + "qualify row_number() over (partition by x.constituent_id order by x.constituent_id " + strTransQualifyOrderByPartSpecificClause + " ) <=1 ) query order by " + strResultOrderByClause + " ; ";
            String strFinalQuery = "select top " + topcount + "  query.* from ( select x.*  from ( " + strFinalSearchDataQuery + ") x ) query order by " + strResultOrderByClause + " ; ";

            //Log.Info("CDI Search Query Constructed: " + strFinalQuery.ToString());

            return strFinalQuery;
        
        }

        //Chiranjib 18/04/2016 - To add dynamic properties to the received model object 
        private static ExpandoObject createExpandoObjectFrom(object source)
        {
            var result = new ExpandoObject();
            IDictionary<string, object> dictionary = result;
            foreach(var property in source.GetType().GetProperties().Where(p => p.GetMethod.IsPublic))
            {
                dictionary[property.Name] = property.GetValue(source);
            }
            return result;
        }

        public static string getBridgeCount(string strResultMasterIdsCSV)
        {
            string strMasterCountQuery = " select * from dw_stuart_vws.strx_cnst_bridge_cnt where cnst_mstr_id in (" + strResultMasterIdsCSV + ")";
            return strMasterCountQuery;
        }

    }
}


*/