using ARC.Donor.Data.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class OrgAffiliators
    {
        public static string getOrgAffiliatorsSQL(int NoOfRecords, int PageNumber, string Master_id)
        {
            return string.Format(Qry, NoOfRecords,
                     PageNumber, string.Join(",", Master_id),
                     (((PageNumber - 1) * Convert.ToInt16(NoOfRecords)) + 1).ToString(),
                     (PageNumber * Convert.ToInt16(NoOfRecords)).ToString());
        }

        static readonly string Qry = @"SELECT   ent_org_id, dw_srcsys_trans_ts, ent_org_name, cln_cnst_org_nm, cnst_mstr_id, 
            cnst_affil_strt_ts, cnst_affil_end_ts, trans_key, user_id, row_stat_cd, appl_src_cd, load_id, 
            is_previous, transaction_key, trans_status, inactive_ind, strx_row_stat_cd, unique_trans_key
            FROM     dw_stuart_vws.strx_cnst_dtl_cnst_org_affil 
            WHERE    cnst_mstr_id = {2}
            AND      ( 
                              trans_status NOT IN ('Rejected') 
                     OR       trans_status IS NULL) 
            AND      (( 
                                       trans_status IN ('Reject') 
                              AND      unique_trans_key IS NOT NULL 
                              AND      unique_trans_key <> '') 
                     OR       ( 
                                       trans_status IN ('Processed') 
                              AND      strx_row_stat_cd = 'F' 
                              AND      unique_trans_key IS NOT NULL 
                              AND      unique_trans_key <> '') 
                     OR       ( 
                                       trans_status NOT IN ('Reject', 
                                                            'Processed')) 
                     OR       trans_status IS NULL) 
            ORDER BY transaction_key, 
                     load_id;";

        public static OrgAffiliatorsInput getWriteOrgAffiliatorsParameters(ARC.Donor.Data.Entities.Constituents.OrgAffiliatorsInput OrgAffiliatorsInput, string RequestType, out string strSPQuery, out List<object> parameters)
        {
            //Helper record tpo have cleaner version of the data from the input
            OrgAffiliatorsInput ConstHelper = new OrgAffiliatorsInput();

            if (RequestType.Equals("insert"))
            {

                ConstHelper.req_typ = RequestType;

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.mstr_id.ToString()))
                    ConstHelper.mstr_id = OrgAffiliatorsInput.mstr_id;

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.usr_nm))
                    ConstHelper.usr_nm = OrgAffiliatorsInput.usr_nm;

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.cnst_typ))
                    ConstHelper.cnst_typ = OrgAffiliatorsInput.cnst_typ;

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.notes))
                    ConstHelper.notes = OrgAffiliatorsInput.notes;

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.case_seq_num.ToString()))
                    ConstHelper.case_seq_num = OrgAffiliatorsInput.case_seq_num;

                ConstHelper.bk_ent_org_id = 0;

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.new_ent_org_id.ToString()))
                    ConstHelper.new_ent_org_id = OrgAffiliatorsInput.new_ent_org_id;

                int intNumberOfInputParameters = 8;
                List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_transaction_key" };

                strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_dtl_ent_org", intNumberOfInputParameters, listOutputParameters);

                var ParamObjects = new List<object>();
                ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.req_typ, "IN", TdType.VarChar, 100));
                ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.mstr_id, "IN", TdType.BigInt, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.usr_nm, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.cnst_typ, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.notes, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.case_seq_num, "IN", TdType.BigInt, 250));

                ParamObjects.Add(SPHelper.createTdParameter("i_bk_ent_org_id", ConstHelper.bk_ent_org_id, "IN", TdType.BigInt, 250));

                ParamObjects.Add(SPHelper.createTdParameter("i_new_ent_org_id", ConstHelper.new_ent_org_id, "IN", TdType.VarChar, 5000));

                parameters = ParamObjects;
                return ConstHelper;
            }
            else
            {
                ConstHelper.req_typ = RequestType; /*For Delete*/

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.mstr_id.ToString()))
                    ConstHelper.mstr_id = OrgAffiliatorsInput.mstr_id;

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.usr_nm))
                    ConstHelper.usr_nm = OrgAffiliatorsInput.usr_nm;

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.cnst_typ))
                    ConstHelper.cnst_typ = OrgAffiliatorsInput.cnst_typ;

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.notes))
                    ConstHelper.notes = OrgAffiliatorsInput.notes;

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.case_seq_num.ToString()))
                    ConstHelper.case_seq_num = OrgAffiliatorsInput.case_seq_num;

                if (!string.IsNullOrEmpty(OrgAffiliatorsInput.bk_ent_org_id.ToString()))
                    ConstHelper.bk_ent_org_id = OrgAffiliatorsInput.bk_ent_org_id;

                ConstHelper.new_ent_org_id = 0;

                int intNumberOfInputParameters = 8;
                List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_transaction_key" };

                strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_dtl_ent_org", intNumberOfInputParameters, listOutputParameters);

                var ParamObjects = new List<object>();
                ParamObjects.Add(SPHelper.createTdParameter("i_req_typ", ConstHelper.req_typ, "IN", TdType.VarChar, 100));
                ParamObjects.Add(SPHelper.createTdParameter("i_mstr_id", ConstHelper.mstr_id, "IN", TdType.BigInt, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", ConstHelper.usr_nm, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_cnst_typ", ConstHelper.cnst_typ, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_notes", ConstHelper.notes, "IN", TdType.VarChar, 250));
                ParamObjects.Add(SPHelper.createTdParameter("i_case_seq_num", ConstHelper.case_seq_num, "IN", TdType.BigInt, 250));

                ParamObjects.Add(SPHelper.createTdParameter("i_bk_ent_org_id", ConstHelper.bk_ent_org_id, "IN", TdType.BigInt, 250));

                ParamObjects.Add(SPHelper.createTdParameter("i_new_ent_org_id", ConstHelper.new_ent_org_id, "IN", TdType.VarChar, 5000));

                parameters = ParamObjects;
                return ConstHelper;
            }
        }
    }
}
