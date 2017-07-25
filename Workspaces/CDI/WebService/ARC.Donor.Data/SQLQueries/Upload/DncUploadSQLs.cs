using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL.Upload
{
    public class DncUploadValidationSQL
    {
        public static string getMasterIdValidationSQL(List<string> _masterIds)
        {
            string masterIDs = getProcessedValue(_masterIds);
            return string.Format(strMasterIdValidationQuery, masterIDs);
        }


        static readonly string strMasterIdValidationQuery = @" SELECT DISTINCT cnst_mstr_id from 
            arc_mdm_vws.bzal_cnst_mstr where row_stat_cd <> 'L' and cnst_mstr_id in ({0})";




        //valiadte SOurce System ID
        public static string getSourceSystemIdSQLValidation(List<string> sourceSystemIds)
        {
            try
            {
                string sourceSystemIDs = getProcessedValue(sourceSystemIds);
                string finalQuery = string.Format(strSourceSystemValidationQuery, sourceSystemIDs);
                return finalQuery;        
            }
            catch (Exception ex)
            {
                return "";
            }

           
        }


        static readonly string strSourceSystemValidationQuery = @"sel  DISTINCT source_system_id from 
            DW_STUART_VWS.strx_cnst_dtl_mstr_ext_brid where source_system_id in ({0})";


        //valiadte Source SYstem Code
        public static string getSourceSystemCodeSQLValidation(List<string> sourceSystemCodes)
        {
            string sourceSystemCDs = getProcessedValue(sourceSystemCodes);
            return string.Format(strSourceSystemCodeValidationQry, sourceSystemCDs);
        }

        static readonly string strSourceSystemCodeValidationQry = @"sel DISTINCT source_system_cd from 
            DW_STUART_VWS.strx_cnst_dtl_mstr_ext_brid where source_system_cd in ({0}) ";

        //validate COmm channeld
        public static string getCommChannelSQLValidation()
        {
            //string commchannels = getProcessedValue(commChannels);
            return string.Format(strCommChannelValidationQry);
        }

        static readonly string strCommChannelValidationQry = @"sel  'Email' as Communication_channel from sys_calendar.calendar where  calendar_date = '2016-01-01'
                                                                UNION
                                                                sel 'All' from sys_calendar.calendar  where  calendar_date = '2016-01-01' 
                                                                UNION
                                                                sel 'Phone' from sys_calendar.calendar  where  calendar_date = '2016-01-01' 
                                                                UNION 
                                                                sel 'Mail' from sys_calendar.calendar  where  calendar_date = '2016-01-01' 
                                                                UNION
                                                                sel 'Text' from sys_calendar.calendar  where  calendar_date = '2016-01-01' 
                                                                    ";



        //validate line of service codes
        public static string getLineOfServiceSQLValidation()
        {
            //string commchannels = getProcessedValue(commChannels);
            return string.Format(strLineOfServiceValidationQry);
        }

        static readonly string strLineOfServiceValidationQry = @"sel  'PHSS' as Line_of_Service_cd  from sys_calendar.calendar where  calendar_date = '2016-01-01'
                                                                UNION
                                                                sel 'Bio' from sys_calendar.calendar  where  calendar_date = '2016-01-01' 
                                                                UNION
                                                                sel 'FR' from sys_calendar.calendar  where  calendar_date = '2016-01-01' 
                                                                UNION 
                                                                sel 'All' from sys_calendar.calendar  where  calendar_date = '2016-01-01' 
                                                                 ";

        public static string getProcessedValue(List<string> values)
        {
            try
            {
                string finalValue = "";
                foreach (string value in values)
                {
                    finalValue += '\'' + value + '\'' + ",";
                }
                finalValue = finalValue.Remove(finalValue.Length - 1);
                return finalValue;
            }

            catch (Exception ex)
            {
                return "";
            }
            
        }

    }



    public class DncUploadSQL
    {
        public static CrudOperationOutput dncUploadCreateTrans(string userId)
        {
            string NameAndEmailUploadHelper;
            NameAndEmailUploadHelper = userId;
            CrudOperationOutput crudOutput = new CrudOperationOutput();
            int intNumberOfInputParameters = 7;
            List<string> listOutputParameters = new List<string> { "transKey", "transOutput" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_inst_trans", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();
            ParamObjects.Add(SPHelper.createTdParameter("typ", "dnc_upl", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("subTyp", "dnc_upl", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("actionType", DBNull.Value, "IN", TdType.VarChar, 5));
            ParamObjects.Add(SPHelper.createTdParameter("transStat", "In Progress", "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("transNotes", "Do Not Contact Upload", "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("userId", userId, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("caseSeqNum", DBNull.Value, "IN", TdType.BigInt, 20));

            crudOutput.parameters = ParamObjects;
            return crudOutput;
        }


        public static CrudOperationOutput insertDncUploadRecords(DncUploadParams dncParams, string username, long strTransactionKey, long max_seq_key)
        {
            CrudOperationOutput crudOutput = new CrudOperationOutput();
           
            int intNumberOfInputParameters = 30;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };
            crudOutput.strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.inst_stg_strx_dnc", intNumberOfInputParameters, listOutputParameters);
            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_seq_key", max_seq_key.CheckDBNull(), "IN", TdType.BigInt, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_mstr_id", string.IsNullOrEmpty(dncParams.init_mstr_id) ?
                                DBNull.Value : dncParams.init_mstr_id.CheckDBNull(), "IN", TdType.BigInt, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_srcsys_id", dncParams.cnst_srcsys_id, "IN", TdType.VarChar, 255));
            ParamObjects.Add(SPHelper.createTdParameter("i_arc_srcsys_cd", dncParams.arc_srcsys_cd, "IN", TdType.VarChar, 15));
            ParamObjects.Add(SPHelper.createTdParameter("i_line_of_service_cd", dncParams.line_of_service_cd, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_comm_channel", dncParams.comm_chan, "IN", TdType.VarChar, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_dnc_type", dncParams.dnc_type, "IN", TdType.VarChar, 20));

            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_line1_addr",dncParams.cnst_addr_line1, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_line2_addr", dncParams.cnst_addr_line2, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_city_nm", dncParams.cnst_addr_city, "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_state_cd", dncParams.cnst_addr_state, "IN", TdType.Char, 2));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_zip_5", dncParams.cnst_addr_zip5, "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_zip_4", dncParams.cnst_addr_zip4.CheckDBNull(), "IN", TdType.VarChar, 10));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_phn_num", dncParams.cnst_phn_num, "IN", TdType.VarChar, 15));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_extn_phn_num", dncParams.cnst_extn_phn_num, "IN", TdType.VarChar, 15));

            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_prefix_nm", dncParams.prefix_nm, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_first_nm", dncParams.prsn_frst_nm, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_middle_nm", dncParams.prsn_mid_nm, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_last_nm", dncParams.prsn_lst_nm, "IN", TdType.VarChar, 50));
            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_suffix_nm", dncParams.suffix_nm, "IN", TdType.VarChar, 50));

            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_org_nm", dncParams.cnst_org_nm, "IN", TdType.VarChar, 50));

            ParamObjects.Add(SPHelper.createTdParameter("i_cnst_email_addr", dncParams.cnst_email_addr, "IN", TdType.VarChar, 100));

            ParamObjects.Add(SPHelper.createTdParameter("i_trans_key", strTransactionKey.CheckDBNull(), "IN", TdType.BigInt, 200));

            ParamObjects.Add(SPHelper.createTdParameter("i_notes", dncParams.notes.CheckDBNull(), "IN", TdType.VarChar, 1000));           
            ParamObjects.Add(SPHelper.createTdParameter("i_user_id", username, "IN", TdType.VarChar, 1000));
            ParamObjects.Add(SPHelper.createTdParameter("i_row_stat_cd", "I", "IN", TdType.VarChar, 20));
            
            ParamObjects.Add(SPHelper.createTdParameter("i_load_id", 10, "IN", TdType.Integer, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_upld_typ_key", 1, "IN", TdType.Integer, 20));
            ParamObjects.Add(SPHelper.createTdParameter("i_upld_typ_dsc", "DNC Upload", "IN", TdType.VarChar, 100));
            ParamObjects.Add(SPHelper.createTdParameter("i_status", "In Progress", "IN", TdType.VarChar, 100));
            crudOutput.parameters = ParamObjects;
            //return gmHelper;
            return crudOutput;
        }

        public static string getMaxSeqKeySQL()
        {
            return string.Format(strMaxSeqKeyQuery).ToString();
        }
        static readonly string strMaxSeqKeyQuery = @"sel coalesce(MAX(seq_key),0) from dw_stuart_vws.strx_cnst_dnc_max_key";
    }
}
