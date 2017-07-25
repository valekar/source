using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using Teradata.Client.Provider;
namespace ARC.Donor.Data.SQL.Orgler.EnterpriseOrgs
{
    public class Affiliations
    {
        /* Method name: getAffiliatedMasterBridgeSQL
       * Input Parameters: Enterprise organisation id,whose affiliations needs to be found.
       * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
       * Purpose:This method is used to find all the affiliations of a particular enterprise  */
        public static CrudOperationOutput getAffiliatedMasterBridgeSQL(int NoOfRecords, string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for affiliation
            crudOperationsOutput.strSPQuery = string.Empty;

            if (NoOfRecords > 0)
                crudOperationsOutput.strSPQuery += string.Format(@"SELECT TOP {0} mstr_brid.* " , NoOfRecords.ToString()); 
            else
                crudOperationsOutput.strSPQuery += "SELECT mstr_brid.* ";

            crudOperationsOutput.strSPQuery +=
                    @" FROM arc_orgler_vws.ent_org_dtl_mstr_brdg mstr_brid
                    LEFT JOIN arc_orgler_vws.ent_org_dtl_mstr_brdg_export mstr_brid_export
	                    ON mstr_brid_export.rec_typ = 'MASTER'
	                    AND mstr_brid.ent_org_id = mstr_brid_export.ent_org_id
	                    AND mstr_brid.cnst_mstr_id = mstr_brid_export.cnst_mstr_id
                    WHERE mstr_brid.ent_org_id = ? 
                    ORDER BY 
	                    (CASE WHEN mstr_brid_export.act_val_ind = 1 THEN 1
					          WHEN mstr_brid_export.act_unval_ind = 1 THEN 2
					          WHEN mstr_brid_export.inact_val_ind = 1 THEN 3
					          WHEN mstr_brid_export.inact_unval_ind = 1 THEN 4
					          WHEN mstr_brid_export.pros_ind = 1 THEN 5
	                     ELSE 6 END),
	                     mstr_brid_export.cnst_affil_strt_ts DESC,
	                     mstr_brid_export.cnst_mstr_id DESC;";

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        //static string to get the Affiliation query
        static readonly string strAffiliationQuery = @" SELECT TOP 50 ent_org_linked_cnst.*,coalesce(bzfc_cnst_giving_prfl.bza_lftm_gift_amt,0) AS bza_lftm_gift_amt FROM dw_stuart_vws.ent_org_linked_cnst ent_org_linked_cnst INNER JOIN arc_mdm_vws.bzfc_cnst_giving_prfl bzfc_cnst_giving_prfl ON
ent_org_linked_cnst.cnst_mstr_id= bzfc_cnst_giving_prfl.cnst_mstr_id WHERE ent_org_id = ? ;";

        //static string to get the Bridge query
        static readonly string bridgeQuery = @" select top 50 * from dw_stuart_vws.ent_org_bridge where ent_org_id = ? ;";

        /* Method name: getAffiliationSQL
        * Input Parameters: Enterprise organisation id,whose affiliations needs to be found.
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose:This method is used to find all the affiliations of a particular enterprise  */
        public static CrudOperationOutput getAffiliationSQL(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for affiliation
            crudOperationsOutput.strSPQuery = strAffiliationQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.VarChar, 100));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }
        /* Method name: getBridgeSQL
   * Input Parameters: Enterprise organisation id,whose bridge(ie. the source system ) needs to be found.
   * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
   * Purpose:This method is used to find all the bridges of a particular enterprise  */
        public static CrudOperationOutput getBridgeSQL(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for bridge
            crudOperationsOutput.strSPQuery = bridgeQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.VarChar, 100));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        /* Method name: getHierarchySQL
       * Input Parameters:enterprise org id whose hieracrchy needs to found
       * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
       * Purpose:This method is used to know the hierarchy of an enterprise.  */
        public static CrudOperationOutput getEnterpriseAffiliationCountSQL(List<string> listEnterpriseOrgs)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            string strCommaSeparatedCSVInput = string.Empty;
            foreach (string s in listEnterpriseOrgs)
            {
               strCommaSeparatedCSVInput = strCommaSeparatedCSVInput == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedCSVInput + ", " + s + "";
            }

            string strEnterpriseAffiliationCountQuery = @"select ent_org_id as id, cnt_of_mstrs as cnt from dw_stuart_vws.strx_ent_org_affil_cnt 
            where ent_org_id in (" + strCommaSeparatedCSVInput + ") ;";

            //populate the query part of the object with the query for hierarchy
            crudOperationsOutput.strSPQuery = strEnterpriseAffiliationCountQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            //ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", strCommaSeparatedCSVInput, "IN", TdType.BigInt, 100));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        /* Method name: getHierarchySQL
       * Input Parameters:enterprise org id whose hieracrchy needs to found
       * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
       * Purpose:This method is used to know the hierarchy of an enterprise.  */
        public static CrudOperationOutput getEnterpriseBridgeCountCountSQL(List<string> listEnterpriseOrgs)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            string strCommaSeparatedCSVInput = string.Empty;
            foreach (string s in listEnterpriseOrgs)
            {
                strCommaSeparatedCSVInput = strCommaSeparatedCSVInput == string.Empty ? "" + s + ""
                                                                    : strCommaSeparatedCSVInput + ", " + s + "";
            }

            //static query to know the hierarchy
            string strEnterpriseBridgeCountQuery = @"select ent_org_id as id,arc_srcsys_cd, counter as cnt from dw_stuart_vws.strx_ent_org_bridge_cnt 
            where ent_org_id in (" + strCommaSeparatedCSVInput + ") ;";

            //populate the query part of the object with the query for hierarchy
            crudOperationsOutput.strSPQuery = strEnterpriseBridgeCountQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            //ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", strCommaSeparatedCSVInput, "IN", TdType.VarChar, 100));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        /* Method name: getAffiliatedMasterBridgeExportSQL
       * Input Parameters: Enterprise organisation id,whose affiliations needs to be found.
       * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
       * Purpose:This method is used to find all the affiliations of a particular enterprise  */
        public static CrudOperationOutput getAffiliatedMasterBridgeExportSQL(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for affiliation
            crudOperationsOutput.strSPQuery = @"SELECT *
                                                FROM	arc_orgler_vws.ent_org_dtl_mstr_brdg_export
                                                WHERE ent_org_id = ?
                                                ORDER BY cnst_mstr_id DESC, rec_typ DESC;";

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.VarChar, 100));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        //Method to get the SQL to fetch the summary information corresponding to the affiliated bridges
        public static CrudOperationOutput getAffilatedMasterBridgeSummarySQL(string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for affiliation
            //SUM(CASE WHEN rec_typ = 'BRIDGE' THEN 1 ELSE 0 END) AS total_brid_cnt,
            crudOperationsOutput.strSPQuery = @"SELECT mstr_brid.ent_org_id,
                                                    (SELECT COUNT(*) FROM arc_orgler_vws.ent_org_dtl_mstr_brdg WHERE ent_org_id = ?) AS total_brid_cnt,
                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN pros_ind ELSE 0 END) AS pros_ind,
                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN act_val_ind ELSE 0 END) AS act_val_ind,
                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN inact_val_ind ELSE 0 END) AS inact_val_ind,
                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN act_unval_ind ELSE 0 END) AS act_unval_ind,
                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN inact_unval_ind ELSE 0 END) AS inact_unval_ind,
                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN 1 ELSE 0 END) AS total_mstr_cnt
                                                FROM 
                                                (
	                                                SELECT DISTINCT ent_org_id, cnst_mstr_id
	                                                FROM arc_orgler_vws.ent_org_dtl_mstr_brdg
                                                ) mstr_brid
                                                LEFT JOIN arc_orgler_vws.ent_org_dtl_mstr_brdg_export mstr_brid_export
	                                                ON mstr_brid.ent_org_id = mstr_brid_export.ent_org_id
	                                                AND mstr_brid.cnst_mstr_id = mstr_brid_export.cnst_mstr_id
                                                    AND mstr_brid_export.rec_typ = 'MASTER'
                                                WHERE mstr_brid.ent_org_id = ?
                                                GROUP BY mstr_brid.ent_org_id;";


//SELECT 
//                                                    mstr_brid.ent_org_id,
//                                                    COUNT(*) AS total_brid_cnt,
//                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN pros_ind ELSE 0 END) AS pros_ind,
//                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN act_val_ind ELSE 0 END) AS act_val_ind,
//                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN inact_val_ind ELSE 0 END) AS inact_val_ind,
//                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN act_unval_ind ELSE 0 END) AS act_unval_ind,
//                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN inact_unval_ind ELSE 0 END) AS inact_unval_ind,
//                                                    SUM(CASE WHEN mstr_brid_export.ent_org_id IS NOT NULL THEN 1 ELSE 0 END) AS total_mstr_cnt
//                                                FROM arc_orgler_vws.ent_org_dtl_mstr_brdg mstr_brid
//                                                LEFT JOIN arc_orgler_vws.ent_org_dtl_mstr_brdg_export mstr_brid_export
//                                                    ON mstr_brid.ent_org_id = mstr_brid_export.ent_org_id
//                                                    AND mstr_brid.cnst_mstr_id = mstr_brid_export.cnst_mstr_id
//                                                    AND mstr_brid_export.rec_typ = 'MASTER'
//                                                WHERE mstr_brid.ent_org_id = ?
//                                                GROUP BY mstr_brid.ent_org_id;";

//            crudOperationsOutput.strSPQuery = @"SELECT 
//	                                                ent_org_id,
//	                                                (SELECT COUNT(*) FROM arc_orgler_vws.ent_org_dtl_mstr_brdg WHERE ent_org_id = ?) AS total_brid_cnt,
//	                                                SUM(CASE WHEN rec_typ = 'MASTER' THEN pros_ind ELSE 0 END) AS pros_ind,
//	                                                SUM(CASE WHEN rec_typ = 'MASTER' THEN act_val_ind ELSE 0 END) AS act_val_ind,
//	                                                SUM(CASE WHEN rec_typ = 'MASTER' THEN inact_val_ind ELSE 0 END) AS inact_val_ind,
//	                                                SUM(CASE WHEN rec_typ = 'MASTER' THEN act_unval_ind ELSE 0 END) AS act_unval_ind,
//	                                                SUM(CASE WHEN rec_typ = 'MASTER' THEN inact_unval_ind ELSE 0 END) AS inact_unval_ind,
//                                                    SUM(CASE WHEN rec_typ = 'MASTER' THEN 1 ELSE 0 END) AS total_mstr_cnt
//                                                FROM arc_orgler_vws.ent_org_dtl_mstr_brdg_export
//                                                WHERE ent_org_id = ?
//                                                GROUP BY ent_org_id;";

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        //Method to get the SQL to fetch the org type summary information corresponding to the affiliated bridges
        public static CrudOperationOutput getAffilatedMasterBridgeOrgTypesSQL(string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for affiliation
            crudOperationsOutput.strSPQuery = @"SELECT ent_org_id , 
                                                    eosi_org_typ, 
                                                    COUNT(*) AS org_cnt
                                                FROM arc_orgler_vws.ent_org_dtl_mstr_brdg_export
                                                WHERE ent_org_id = ?
                                                AND rec_typ = 'MASTER'
                                                GROUP BY ent_org_id , eosi_org_typ;";

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }
    }
  
}
