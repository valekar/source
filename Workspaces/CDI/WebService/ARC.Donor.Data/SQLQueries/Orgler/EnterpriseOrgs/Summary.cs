using ARC.Donor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Orgler.EnterpriseOrgs
{
    public class Summary
    {
        //static query to know the hierarchy
        static readonly string strEnterpriseOrgSummaryQuery = @"select  
                        ent_org_id, ent_org_name, ent_org_src_cd, nk_ent_org_id,
		                ent_org_dsc, fr_cnt, frchpt_cnt, phss_cnt, bio_cnt, eosi_cnt,
		                created_by, created_at, last_modified_by, last_modified_at, last_modified_by_all,
		                last_modified_at_all, last_reviewed_by, last_reviewed_at, data_stwrd_usr,
		                fr_rcnt_patrng_dt, fr_totl_dntn_cnt, fr_totl_dntn_val, fr_rcncy_scr,
		                fr_freq_scr, fr_dntn_scr, fr_totl_rfm_scr, bio_rcnt_patrng_dt,
		                bio_totl_dntn_cnt, bio_totl_dntn_val, bio_rcncy_scr, bio_freq_scr,
		                bio_dntn_scr, bio_totl_rfm_scr, hs_rcnt_patrng_dt, hs_totl_dntn_cnt,
		                hs_totl_dntn_val, hs_rcncy_scr, hs_freq_scr, hs_dntn_scr, hs_totl_rfm_scr,
                        (SELECT DISTINCT cnt_of_mstrs FROM arc_orgler_vws.ent_org_affil_cnt WHERE ent_org_id = ?) as mstr_cnt,
                        (SELECT SUM(brid_cnt) FROM arc_orgler_vws.ent_org_dtl_bridge_cnt WHERE ent_org_id = ?) as brid_cnt
                        from arc_orgler_vws.ent_org_dtl_smry  
                        where ent_org_id = ?";

        /* Method name: getHierarchySQL
        * Input Parameters:enterprise org id whose hieracrchy needs to found
        * Output Parameters: An object of CrudOperationOutput class which contains the query and the parameters required for execution.
        * Purpose:This method is used to know the hierarchy of an enterprise.  */
        public static CrudOperationOutput getSummarySQL(int NoOfRecords, int PageNumber, string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for hierarchy
            crudOperationsOutput.strSPQuery = strEnterpriseOrgSummaryQuery;

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }

        public static CrudOperationOutput getBridgeCountSQL(string enterpriseOrgId)
        {
            //Instantiate an object of type CrudOperationOutput
            CrudOperationOutput crudOperationsOutput = new CrudOperationOutput();

            //populate the query part of the object with the query for hierarchy
            crudOperationsOutput.strSPQuery = @"select  * 
                                                from arc_orgler_vws.ent_org_dtl_bridge_cnt  
                                                where ent_org_id = ?
                                                order by ent_org_id, line_of_service_cd";

            //create a list of paramaters required for this query, add them and assign it to the parameters part of the object
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("ent_org_id", enterpriseOrgId, "IN", TdType.BigInt, 0));
            crudOperationsOutput.parameters = ParamObjects;

            //return back the custom object containing the string and parameters
            return crudOperationsOutput;
        }
    }
}
