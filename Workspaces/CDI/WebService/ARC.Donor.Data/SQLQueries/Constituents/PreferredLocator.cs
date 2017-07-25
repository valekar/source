using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class PreferredLocator
    {
        public static string getPreferredLocatorSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT *
        FROM DW_STUART_VWS.strx_cnst_dtl_cnst_birth
        WHERE cnst_mstr_id = {2} 
        AND   (trans_status NOT IN ('Rejected') 
        OR  trans_status IS NULL) 
        AND   ((trans_status IN ('Reject') 
        AND   unique_trans_key IS NOT NULL 
        AND   unique_trans_key <> '') 
        OR  (trans_status IN ('Processed') 
        AND   strx_row_stat_cd = 'F' 
        AND   unique_trans_key IS NOT NULL 
        AND   unique_trans_key <> '') 
        OR  (trans_status NOT IN ('Reject','Processed')) 
        OR  trans_status IS NULL) 
        ORDER  by transaction_key;";



        public static PreferredLocatorInput addPreferredLocatorSQL(PreferredLocatorInput preferredLocatorInput, string requestType, out string strSPQuery, out List<object> parameters)
        {
            PreferredLocatorInput helperLocator = new PreferredLocatorInput();
            if (!string.IsNullOrEmpty(preferredLocatorInput.cnst_mstr_id))
                helperLocator.cnst_mstr_id = preferredLocatorInput.cnst_mstr_id;
            if (!string.IsNullOrEmpty(preferredLocatorInput.arc_srcsys_cd))
                helperLocator.arc_srcsys_cd = preferredLocatorInput.arc_srcsys_cd;
            if (!string.IsNullOrEmpty(preferredLocatorInput.cnst_srcsys_id))
                helperLocator.cnst_srcsys_id = preferredLocatorInput.cnst_srcsys_id;
            if (!string.IsNullOrEmpty(preferredLocatorInput.cnst_typ))
                helperLocator.cnst_typ = preferredLocatorInput.cnst_typ;
            if (!string.IsNullOrEmpty(preferredLocatorInput.created_by))
                helperLocator.created_by = preferredLocatorInput.created_by;
            if (!string.IsNullOrEmpty(preferredLocatorInput.los_cd))
                helperLocator.los_cd = preferredLocatorInput.los_cd;
            if (!string.IsNullOrEmpty(preferredLocatorInput.notes))
                helperLocator.notes = preferredLocatorInput.notes;
            if (!string.IsNullOrEmpty(preferredLocatorInput.pref_loc_typ))
                helperLocator.pref_loc_typ = preferredLocatorInput.pref_loc_typ;
            if (!string.IsNullOrEmpty(preferredLocatorInput.pref_loc_id))
                helperLocator.pref_loc_id = preferredLocatorInput.pref_loc_id;
            if (!string.IsNullOrEmpty(preferredLocatorInput.user_id))
                helperLocator.user_id = preferredLocatorInput.user_id;

            int numberOfInputParameters = 11;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };
            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_pref_loc", numberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", helperLocator.cnst_mstr_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", helperLocator.cnst_typ, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_arc_srcsys_cd", helperLocator.arc_srcsys_cd, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_srcsys_id", helperLocator.cnst_srcsys_id, "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", helperLocator.los_cd, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref_loc_id", helperLocator.pref_loc_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref_loc_typ", helperLocator.pref_loc_typ, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", helperLocator.notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_created_by", helperLocator.created_by, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", helperLocator.user_id, "IN", TdType.ByteInt, 0));

            parameters = ParamObjects;

            return helperLocator;
        }


        public static PreferredLocatorInput updatePreferredLocatorSQL(PreferredLocatorInput preferredLocatorInput, string requestType, out string strSPQuery, out List<object> parameters)
        {
            PreferredLocatorInput helperLocator = new PreferredLocatorInput();
            if (!string.IsNullOrEmpty(preferredLocatorInput.cnst_mstr_id))
                helperLocator.cnst_mstr_id = preferredLocatorInput.cnst_mstr_id;
            if (!string.IsNullOrEmpty(preferredLocatorInput.arc_srcsys_cd))
                helperLocator.arc_srcsys_cd = preferredLocatorInput.arc_srcsys_cd;
            if (!string.IsNullOrEmpty(preferredLocatorInput.cnst_srcsys_id))
                helperLocator.cnst_srcsys_id = preferredLocatorInput.cnst_srcsys_id;
            if (!string.IsNullOrEmpty(preferredLocatorInput.cnst_typ))
                helperLocator.cnst_typ = preferredLocatorInput.cnst_typ;
            if (!string.IsNullOrEmpty(preferredLocatorInput.created_by))
                helperLocator.created_by = preferredLocatorInput.created_by;
            if (!string.IsNullOrEmpty(preferredLocatorInput.los_cd))
                helperLocator.los_cd = preferredLocatorInput.los_cd;
            if (!string.IsNullOrEmpty(preferredLocatorInput.notes))
                helperLocator.notes = preferredLocatorInput.notes;
            if (!string.IsNullOrEmpty(preferredLocatorInput.pref_loc_typ))
                helperLocator.pref_loc_typ = preferredLocatorInput.pref_loc_typ;
            if (!string.IsNullOrEmpty(preferredLocatorInput.pref_loc_id))
                helperLocator.pref_loc_id = preferredLocatorInput.pref_loc_id;
            if (!string.IsNullOrEmpty(preferredLocatorInput.user_id))
                helperLocator.user_id = preferredLocatorInput.user_id;

            int numberOfInputParameters = 11;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };
            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_pref_loc", numberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", helperLocator.cnst_mstr_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", helperLocator.cnst_typ, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_arc_srcsys_cd", helperLocator.arc_srcsys_cd, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_srcsys_id", helperLocator.cnst_srcsys_id, "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", helperLocator.los_cd, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref_loc_id", helperLocator.pref_loc_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref_loc_typ", helperLocator.pref_loc_typ, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", helperLocator.notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_created_by", helperLocator.created_by, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", helperLocator.user_id, "IN", TdType.ByteInt, 0));

            parameters = ParamObjects;

            return helperLocator;
        }


        public static PreferredLocatorInput inactivatePreferredLocatorSQL(PreferredLocatorInput preferredLocatorInput, string requestType, out string strSPQuery, out List<object> parameters)
        {
            PreferredLocatorInput helperLocator = new PreferredLocatorInput();
            if (!string.IsNullOrEmpty(preferredLocatorInput.cnst_mstr_id))
                helperLocator.cnst_mstr_id = preferredLocatorInput.cnst_mstr_id;
            if (!string.IsNullOrEmpty(preferredLocatorInput.arc_srcsys_cd))
                helperLocator.arc_srcsys_cd = preferredLocatorInput.arc_srcsys_cd;
            if (!string.IsNullOrEmpty(preferredLocatorInput.cnst_srcsys_id))
                helperLocator.cnst_srcsys_id = preferredLocatorInput.cnst_srcsys_id;
            if (!string.IsNullOrEmpty(preferredLocatorInput.cnst_typ))
                helperLocator.cnst_typ = preferredLocatorInput.cnst_typ;
            if (!string.IsNullOrEmpty(preferredLocatorInput.created_by))
                helperLocator.created_by = preferredLocatorInput.created_by;
            if (!string.IsNullOrEmpty(preferredLocatorInput.los_cd))
                helperLocator.los_cd = preferredLocatorInput.los_cd;
            if (!string.IsNullOrEmpty(preferredLocatorInput.notes))
                helperLocator.notes = preferredLocatorInput.notes;
            if (!string.IsNullOrEmpty(preferredLocatorInput.pref_loc_typ))
                helperLocator.pref_loc_typ = preferredLocatorInput.pref_loc_typ;
            if (!string.IsNullOrEmpty(preferredLocatorInput.pref_loc_id))
                helperLocator.pref_loc_id = preferredLocatorInput.pref_loc_id;
            if (!string.IsNullOrEmpty(preferredLocatorInput.user_id))
                helperLocator.user_id = preferredLocatorInput.user_id;

            int numberOfInputParameters = 11;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };
            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_pref_loc", numberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", helperLocator.cnst_mstr_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", helperLocator.cnst_typ, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_arc_srcsys_cd", helperLocator.arc_srcsys_cd, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_srcsys_id", helperLocator.cnst_srcsys_id, "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", helperLocator.los_cd, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref_loc_id", helperLocator.pref_loc_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref_loc_typ", helperLocator.pref_loc_typ, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", helperLocator.notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_created_by", helperLocator.created_by, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", helperLocator.user_id, "IN", TdType.ByteInt, 0));

            parameters = ParamObjects;

            return helperLocator;
        }
    }


     
}
