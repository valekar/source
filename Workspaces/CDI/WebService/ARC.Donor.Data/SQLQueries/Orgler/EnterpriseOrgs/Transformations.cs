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
    public class Transformations
    {
        //query to get  transformation detail
        static readonly string strTransformationQuery = @" select * from arc_orgler_vws.ent_org_mapping where ent_org_id =? order by cdim_transform_id desc;";

        //query to get cdi transform detail
        static readonly string strCDITransformDetailQuery = @"select* from arc_orgler_vws.cdi_transform_dtl where ent_org_id = ? order by cdim_transform_id, (CASE WHEN transform_condn_typ_cd = 'E' THEN 0 WHEN transform_condn_typ_cd = 'C' THEN 1 WHEN transform_condn_typ_cd = 'F' THEN 2 WHEN transform_condn_typ_cd = 'D' THEN 3 ELSE 4 END), pattern_match_string;";
        //query to get stuart transform detail
        static string strStuartTransformDetailQuery = " select * from dw_stuart_vws.strx_transform_dtl where ent_org_id = ? order by strx_transform_id, (CASE WHEN transform_condn_typ_cd = 'E' THEN 0 WHEN transform_condn_typ_cd = 'C' THEN 1 WHEN transform_condn_typ_cd = 'F' THEN 2 WHEN transform_condn_typ_cd = 'D' THEN 3 ELSE 4 END), pattern_match_string;";

        /* Method name: getTransformationSQL
        * Input Parameters:enterprise org id whose transformation needs to found
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose:This method is used to know the transformation of an enterprise.  */
        public static CrudOperationOutput getTransformationSQL(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for transformation
            crudOperationsOutput.strSPQuery = strTransformationQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }
        /* Method name: getCDITransformationSQL
       * Input Parameters:enterprise org id whose CDI transformation needs to found
       * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
       * Purpose:This method is used to know the CDI transformation of an enterprise.  */
        public static CrudOperationOutput getCDITransformationSQL(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for CDI transform
            crudOperationsOutput.strSPQuery = strCDITransformDetailQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }
        /* Method name: getStuartTransformationSQL
       * Input Parameters:enterprise org id whose Stuart transformation needs to found
       * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
       * Purpose:This method is used to know the Stuart transformation of an enterprise.  */
        public static CrudOperationOutput getStuartTransformationSQL(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for potential merge
            crudOperationsOutput.strSPQuery = strStuartTransformDetailQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        /* Method name: getHierarchySQL
        * Input Parameters:enterprise org id whose hieracrchy needs to found
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose:This method is used to know the hierarchy of an enterprise.  */
        public static CrudOperationOutput getEnterpriseTransformationCountSQL(string ent_org_id)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            string strEnterpriseTransformationCountQuery = @"select bz_cnst_org_affil.cdim_transform_id as id, count(*) as cnt from arc_mdm_vws.bz_cnst_org_affil bz_cnst_org_affil INNER JOIN arc_mdm_vws.bz_cnst_mstr bz_cnst_mstr ON bz_cnst_org_affil.cnst_mstr_id = bz_cnst_mstr.cnst_mstr_id  where bz_cnst_org_affil.ent_org_id in (" + ent_org_id + ") group by bz_cnst_org_affil.ent_org_id, bz_cnst_org_affil.cdim_transform_id;";

            //populate the query part of the object with the query for hierarchy
            crudOperationsOutput.strSPQuery = strEnterpriseTransformationCountQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            //ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", strCommaSeparatedCSVInput, "IN", TdType.VarChar, 100));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        public static CrudOperationOutput getTransformationUpdateSQL(TransformationUpdateFormatInput input)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            int intInputParameters = 8;
            List<string> outParams = new List<string> { "o_outputMessage", "o_transaction_key" };

            //populate the query part of the object with the query for transformations
            crudOperationsOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_ent_org_mapping", intInputParameters, outParams);

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_id", input.ent_org_id, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_cdim_transform_id", input.cdim_transform_id, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_pattern_match_string", input.pattern_match_string, "IN", TdType.VarChar, 1200));
            ParamObjects.Add(SPHelper.createTdParameter("i_ent_org_branch", input.ent_org_branch, "IN", TdType.VarChar, 225));
            ParamObjects.Add(SPHelper.createTdParameter("i_active_ind", input.active_ind, "IN", TdType.ByteInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", input.req_typ, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", input.notes, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_userId", input.userId, "IN", TdType.VarChar, 50));

            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        //To get the possible affiliation count for the rules configured
        public static string getSmokeTestAffiliationCountSQL(string strFilter, string ent_org_id)
        {
            return string.Format(@"SELECT COUNT(*) AS total_affil_cnt,
	                                    SUM(CASE WHEN affil.cnst_mstr_id IS NULL THEN 1 ELSE 0 END) AS delta_affil_cnt
                                    FROM
                                    (
	                                    SELECT DISTINCT cnst_mstr_id, {1} as ent_org_id
	                                    FROM	 arc_mdm_vws.bz_cnst_org_nm 
	                                    WHERE	( {0} ) 
	                                    AND TRANSLATE_CHK(cnst_org_nm USING LATIN_TO_UNICODE) = 0 
                                    ) org_nm
                                    LEFT JOIN
                                    (
                                        SELECT DISTINCT ent_org_id, cnst_mstr_id
                                        FROM arc_mdm_vws.bz_cnst_org_affil
                                        WHERE ent_org_id = {1}
                                        
                                        UNION
                                        SELECT DISTINCT ent_org_id, cnst_mstr_id
                                        FROM arc_mdm_tbls.cnst_org_affil 
                                        WHERE ent_org_id = {1}
                                        AND row_stat_cd = 'L'
                                        AND cnst_affil_end_ts (DATE) <> '9999-12-31'(DATE)
                                        AND trans_key IS NOT NULL
                                    ) affil
                                    ON org_nm.cnst_mstr_id = affil.cnst_mstr_id
                                    GROUP BY org_nm.ent_org_id;", strFilter, ent_org_id);

            /*UNION
                                        SELECT DISTINCT aff.ent_org_id, aff.cnst_mstr_id
                                        FROM 
                                        ( 
                                            SELECT DISTINCT ent_org_id, cnst_mstr_id, trans_key
                                            FROM arc_mdm_tbls.cnst_org_affil 
                                            WHERE ent_org_id = {1}
                                            AND row_stat_cd = 'L'
                                            AND cnst_affil_end_ts (DATE) <> '9999-12-31'(DATE)
                                        ) aff
                                        INNER JOIN 
                                        (
                                            SELECT DISTINCT trans_key
                                            FROM dw_stuart_tbls.trans
                                            WHERE trans_typ_id IN (SELECT DISTINCT trans_typ_id FROM dw_stuart_tbls.trans_typ WHERE trans_typ_cd = 'loc_chg')
                                            AND sub_trans_typ_id IN (SELECT DISTINCT sub_trans_typ_id FROM dw_stuart_tbls.sub_trans_typ WHERE sub_trans_typ_cd = 'org_affil' AND sub_trans_actn_typ = 'Delete')
                                            AND trans_stat = 'Processed'
                                        ) trans
                                        ON aff.trans_key = trans.trans_key*/
        }

        //To get the possible affiliation count for the rules configured
        public static string getSmokeTestAffiliationExportSQL(string strFilter, int recCount, string ent_org_id)
        {
            return string.Format(@"
                                SELECT TOP {0} *
                                FROM 
                                (
                                SELECT {2} AS ent_org_id,  
                                        (SELECT DISTINCT ent_org_name FROM arc_mdm_vws.bz_cdim_enterprise_orgs WHERE ent_org_id = {2}) AS ent_org_name,
                                                smry.cnst_mstr_id,
				                                org_nm.bz_cln_cnst_org_nm AS cnst_org_nm,
				                                COALESCE(smry.cnst_line1_addr, '') 						
					                                || CASE WHEN COALESCE(smry.cnst_line1_addr, '') <> '' THEN ', ' || COALESCE(smry.cnst_line2_addr, '') ELSE COALESCE(smry.cnst_line2_addr, '')  END
					                                || CASE WHEN COALESCE(cnst_line2_addr, '') <> '' THEN ', ' || COALESCE(smry.cnst_addr_city, '') ELSE COALESCE(smry.cnst_addr_city, '')  END
					                                || CASE WHEN COALESCE(cnst_addr_city, '') <> '' THEN ', ' || COALESCE(smry.cnst_addr_state, '') ELSE COALESCE(smry.cnst_addr_state, '')  END
					                                || CASE WHEN COALESCE(cnst_addr_state, '') <> '' THEN ', ' || COALESCE(smry.cnst_addr_zip_5, '') ELSE COALESCE(smry.cnst_addr_zip_5, '')  END
					                                AS address, 
				                                smry.cnst_phn_num,
                                        org_nm.affil_exist_ind
                                FROM arc_mdm_vws.bzfc_arc_best_smry smry
                                INNER JOIN 
                                (
	                                SELECT cnst_org_nm.cnst_mstr_id, cnst_org_nm.bz_cln_cnst_org_nm, cnst_org_nm.cnst_org_nm_strt_dt, 
                                           CASE WHEN affil.cnst_mstr_id IS NULL THEN 'No' ELSE 'Yes' END AS affil_exist_ind
                                    FROM	 
                                    (
                                        SELECT cnst_mstr_id, bz_cln_cnst_org_nm, cnst_org_nm_strt_dt
                                        FROM arc_mdm_vws.bz_cnst_org_nm 
                                        WHERE	( {1} ) 
                                        AND TRANSLATE_CHK(cnst_org_nm USING LATIN_TO_UNICODE) = 0 
                                    ) cnst_org_nm
                                    LEFT JOIN
                                    (
	                                    SELECT DISTINCT cnst_mstr_id
	                                    FROM arc_mdm_vws.bz_cnst_org_affil
	                                    WHERE ent_org_id = {2}
                                        
                                        UNION
                                        SELECT DISTINCT cnst_mstr_id
                                        FROM arc_mdm_tbls.cnst_org_affil 
                                        WHERE ent_org_id = {2}
                                        AND row_stat_cd = 'L'
                                        AND cnst_affil_end_ts (DATE) <> '9999-12-31'(DATE)
                                        AND trans_key IS NOT NULL
                                    ) affil
                                        ON cnst_org_nm.cnst_mstr_id = affil.cnst_mstr_id
                                ) org_nm
                                ON smry.cnst_mstr_id = org_nm.cnst_mstr_id
                                QUALIFY ROW_NUMBER() OVER( PARTITION BY org_nm.cnst_mstr_id ORDER BY org_nm.cnst_org_nm_strt_dt DESC  )<=1
                                ) sub
                                ORDER BY affil_exist_ind, cnst_org_nm;", recCount.ToString(), strFilter, ent_org_id);
        }
    }
}
