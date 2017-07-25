using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Transaction;

namespace ARC.Donor.Data.SQL.Transaction
{
    public class TransactionSearchSQL
    {
        public static string getTransactionSearchResultsSQL(ListTransactionSearchInputModel listTransactionSearchInput)
        {
            string strTransactionSelectQuery = "select trans.trans_key, trans_typ.trans_typ_dsc, sub_trans_typ.sub_trans_typ_dsc, trans_typ.trans_typ_dsc || ' - ' || sub_trans_typ.sub_trans_typ_dsc as transaction_type, trans.trans_stat, sub_trans_typ.sub_trans_actn_typ, trans.trans_note, trans.user_id, trans.trans_create_ts, trans.trans_last_modified_ts, strx_case.case_key,trans.approved_by,trans.approved_dt from dw_stuart_vws.trans trans LEFT OUTER JOIN ( SEL inner_trans_cnst.* FROM dw_stuart_tbls.trans inner_trans INNER JOIN dw_stuart_vws.trans_cnst inner_trans_cnst ON  inner_trans.trans_key = inner_trans_cnst.trans_key AND inner_trans.trans_typ_id <> 4) trans_cnst on trans.trans_key = trans_cnst.trans_key inner join dw_stuart_vws.trans_typ trans_typ on trans.trans_typ_id = trans_typ.trans_typ_id inner join dw_stuart_vws.sub_trans_typ sub_trans_typ on trans.sub_trans_typ_id = sub_trans_typ.sub_trans_typ_id left outer join dw_stuart_vws.bz_strx_case strx_case on strx_case.case_key=trans.case_seq";
            string strPartitionByClause = " qualify row_number() over (partition by trans.trans_key order by trans.trans_last_modified_ts desc) <=1";
            string strOrderByClause = " order by query.trans_last_modified_ts desc";
            string strWhereClause = " where 1=1 ";
            string strUserInputWhereClause = string.Empty;
            if (listTransactionSearchInput != null)
            {
                foreach (TransactionSearchInputModel transSearchInput in listTransactionSearchInput.TransactionSearchInputModel)
                {
                    strUserInputWhereClause = strUserInputWhereClause == string.Empty ? " ( " + getWhereClauseforTransaction(transSearchInput) + " ) "
                                                    : strUserInputWhereClause + " or " + " ( " + getWhereClauseforTransaction(transSearchInput) + " ) ";

                }
                strWhereClause = strWhereClause + "and" + " ( " + strUserInputWhereClause + " ) ";
            }
            string strTransactionSearchQuery = strTransactionSelectQuery + strWhereClause + strPartitionByClause;
            strTransactionSearchQuery = "SELECT TOP 100 query.* from ( " + strTransactionSearchQuery + " ) query " + strOrderByClause + " ;";
            strTransactionSearchQuery = strTransactionSearchQuery.Replace("or  (  )  ", "");
            return strTransactionSearchQuery;
        }

        public static string getWhereClauseforTransaction(TransactionSearchInputModel transSearchInput)
        {
            string strWhereClause = string.Empty;
            string strPartWhereClause = string.Empty;
            if(!string.IsNullOrEmpty(transSearchInput.UserName))
            {
                strPartWhereClause = " trans.user_id = \'" + transSearchInput.UserName + "\' ";
                strWhereClause = strWhereClause == string.Empty ? strPartWhereClause : strWhereClause + " and " + strPartWhereClause;
            }
            if (!string.IsNullOrEmpty(transSearchInput.TransactionKey))
            {
                strPartWhereClause = " trans.trans_key = \'" + transSearchInput.TransactionKey + "\' ";
                strWhereClause = strWhereClause == string.Empty ? strPartWhereClause : strWhereClause + " and " + strPartWhereClause;
            }
            if (!string.IsNullOrEmpty(transSearchInput.TransactionType))
            {
                strPartWhereClause = " trans_typ.trans_typ_dsc = \'" + transSearchInput.TransactionType + "\' "; 
                strWhereClause = strWhereClause == string.Empty ? strPartWhereClause : strWhereClause + " and " + strPartWhereClause;
            }
            if (!string.IsNullOrEmpty(transSearchInput.SubTransactionType))
            {
                strPartWhereClause = " sub_trans_typ.sub_trans_typ_dsc = \'" + transSearchInput.SubTransactionType+ "\' ";
                strWhereClause = strWhereClause == string.Empty ? strPartWhereClause : strWhereClause + " and " + strPartWhereClause;
            }
            if (!string.IsNullOrEmpty(transSearchInput.Status))
            {
                strPartWhereClause = " trans.trans_stat = \'" + transSearchInput.Status + "\' ";
                strWhereClause = strWhereClause == string.Empty ? strPartWhereClause : strWhereClause + " and " + strPartWhereClause;
            }
            if (!string.IsNullOrEmpty(transSearchInput.MasterId))
            {
                strPartWhereClause = " trans_cnst.cnst_id = \'" + transSearchInput.MasterId + "\' ";
                strWhereClause = strWhereClause == string.Empty ? strPartWhereClause : strWhereClause + " and " + strPartWhereClause;
            }
            if (!string.IsNullOrEmpty(transSearchInput.FromDate))
            {
                strPartWhereClause = " trans.trans_create_ts >= \'" + transSearchInput.FromDate + "\' (DATE, FORMAT 'mm/dd/yyyy')";
                strWhereClause = strWhereClause == string.Empty ? strPartWhereClause : strWhereClause + " and " + strPartWhereClause;
            }
            if (!string.IsNullOrEmpty(transSearchInput.ToDate))
            {
                strPartWhereClause = " trans.trans_last_modified_ts <= \'" + transSearchInput.ToDate + "\' (DATE, FORMAT 'mm/dd/yyyy')";
                strWhereClause = strWhereClause == string.Empty ? strPartWhereClause : strWhereClause + " and " + strPartWhereClause;
            }

            return strWhereClause;
        }
    }
}
