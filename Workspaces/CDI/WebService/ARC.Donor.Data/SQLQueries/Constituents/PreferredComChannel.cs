using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
     public class PreferredComChannel
    {
        public static string getPreferredComChannelSQL(int NoOfRecords, int PageNumber, string Master_id)
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


        public static PreferredComChannelInput addPreferredComChannelSQL(PreferredComChannelInput preferredComChannel, string requestType, out string strSPQuery, out List<object> parameters)
        {
            PreferredComChannelInput helperComChannel = new PreferredComChannelInput();
            if (!string.IsNullOrEmpty(preferredComChannel.cnst_mstr_id))
                helperComChannel.cnst_mstr_id = preferredComChannel.cnst_mstr_id;
            if (!string.IsNullOrEmpty(preferredComChannel.arc_srcsys_cd))
                helperComChannel.arc_srcsys_cd = preferredComChannel.arc_srcsys_cd;
            if (!string.IsNullOrEmpty(preferredComChannel.cnst_typ))
                helperComChannel.cnst_typ = preferredComChannel.cnst_typ;
            if (!string.IsNullOrEmpty(preferredComChannel.created_by))
                helperComChannel.created_by = preferredComChannel.created_by;
            if (!string.IsNullOrEmpty(preferredComChannel.los_cd))
                helperComChannel.los_cd = preferredComChannel.los_cd;
            if (!string.IsNullOrEmpty(preferredComChannel.notes))
                helperComChannel.notes = preferredComChannel.notes;
            if (!string.IsNullOrEmpty(preferredComChannel.pref_chan))
                helperComChannel.pref_chan = preferredComChannel.pref_chan;
            if (!string.IsNullOrEmpty(preferredComChannel.user_id))
                helperComChannel.user_id = preferredComChannel.user_id;

            int numberOfInputParameters = 9;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };
            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_pref_chan", numberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", helperComChannel.cnst_mstr_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", helperComChannel.cnst_typ, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_arc_srcsys_cd", helperComChannel.arc_srcsys_cd, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", helperComChannel.los_cd, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref_chan", helperComChannel.pref_chan, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", helperComChannel.notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_created_by", helperComChannel.created_by, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", helperComChannel.user_id, "IN", TdType.ByteInt, 0));

            parameters = ParamObjects;

            return helperComChannel;
        }



        public static PreferredComChannelInput updatePreferredComChannelSQL(PreferredComChannelInput preferredComChannel, string requestType, out string strSPQuery, out List<object> parameters)
        {
            PreferredComChannelInput helperComChannel = new PreferredComChannelInput();
            if (!string.IsNullOrEmpty(preferredComChannel.cnst_mstr_id))
                helperComChannel.cnst_mstr_id = preferredComChannel.cnst_mstr_id;
            if (!string.IsNullOrEmpty(preferredComChannel.arc_srcsys_cd))
                helperComChannel.arc_srcsys_cd = preferredComChannel.arc_srcsys_cd;
            if (!string.IsNullOrEmpty(preferredComChannel.cnst_typ))
                helperComChannel.cnst_typ = preferredComChannel.cnst_typ;
            if (!string.IsNullOrEmpty(preferredComChannel.created_by))
                helperComChannel.created_by = preferredComChannel.created_by;
            if (!string.IsNullOrEmpty(preferredComChannel.los_cd))
                helperComChannel.los_cd = preferredComChannel.los_cd;
            if (!string.IsNullOrEmpty(preferredComChannel.notes))
                helperComChannel.notes = preferredComChannel.notes;
            if (!string.IsNullOrEmpty(preferredComChannel.pref_chan))
                helperComChannel.pref_chan = preferredComChannel.pref_chan;
            if (!string.IsNullOrEmpty(preferredComChannel.user_id))
                helperComChannel.user_id = preferredComChannel.user_id;

            int numberOfInputParameters = 9;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };
            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_pref_chan", numberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", helperComChannel.cnst_mstr_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", helperComChannel.cnst_typ, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_arc_srcsys_cd", helperComChannel.arc_srcsys_cd, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", helperComChannel.los_cd, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref_chan", helperComChannel.pref_chan, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", helperComChannel.notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_created_by", helperComChannel.created_by, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", helperComChannel.user_id, "IN", TdType.ByteInt, 0));

            parameters = ParamObjects;

            return helperComChannel;
        }


        public static PreferredComChannelInput inactivatePreferredComChannelSQL(PreferredComChannelInput preferredComChannel, string requestType, out string strSPQuery, out List<object> parameters)
        {
            PreferredComChannelInput helperComChannel = new PreferredComChannelInput();
            if (!string.IsNullOrEmpty(preferredComChannel.cnst_mstr_id))
                helperComChannel.cnst_mstr_id = preferredComChannel.cnst_mstr_id;
            if (!string.IsNullOrEmpty(preferredComChannel.arc_srcsys_cd))
                helperComChannel.arc_srcsys_cd = preferredComChannel.arc_srcsys_cd;
            if (!string.IsNullOrEmpty(preferredComChannel.cnst_typ))
                helperComChannel.cnst_typ = preferredComChannel.cnst_typ;
            if (!string.IsNullOrEmpty(preferredComChannel.created_by))
                helperComChannel.created_by = preferredComChannel.created_by;
            if (!string.IsNullOrEmpty(preferredComChannel.los_cd))
                helperComChannel.los_cd = preferredComChannel.los_cd;
            if (!string.IsNullOrEmpty(preferredComChannel.notes))
                helperComChannel.notes = preferredComChannel.notes;
            if (!string.IsNullOrEmpty(preferredComChannel.pref_chan))
                helperComChannel.pref_chan = preferredComChannel.pref_chan;
            if (!string.IsNullOrEmpty(preferredComChannel.user_id))
                helperComChannel.user_id = preferredComChannel.user_id;

            int numberOfInputParameters = 9;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "transaction_key" };
            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_cnst_pref_chan", numberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", requestType, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", helperComChannel.cnst_mstr_id, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", helperComChannel.cnst_typ, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_arc_srcsys_cd", helperComChannel.arc_srcsys_cd, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", helperComChannel.los_cd, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_pref_chan", helperComChannel.pref_chan, "IN", TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_notes", helperComChannel.notes, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_created_by", helperComChannel.created_by, "IN", TdType.VarChar, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", helperComChannel.user_id, "IN", TdType.ByteInt, 0));

            parameters = ParamObjects;

            return helperComChannel;
        }
    }
}
