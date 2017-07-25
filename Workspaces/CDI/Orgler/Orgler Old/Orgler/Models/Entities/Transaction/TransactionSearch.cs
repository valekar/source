using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities.Transaction
{
    public class TransactionSearchInputModel
    {
        public string UserName { get; set; }
        public string TransactionKey { get; set; }
        public string TransactionType { get; set; }
        public string SubTransactionType { get; set; }
        public string Status { set; get; }
        public string MasterId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LoggedInUser { get; set; }
        public TransactionSearchInputModel()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            LoggedInUser = p.GetUserName(); //p.Identity.Name;
        }
    }

    public class TransactionSearchOutputModel
    {
        public string trans_key { get; set; }
        public string trans_typ_dsc { get; set; }
        public string sub_trans_typ_dsc { get; set; }
        public string transaction_type { get; set; }
        public string trans_stat { get; set; }
        public string sub_trans_actn_typ { get; set; }
        public string trans_note { get; set; }
        public string user_id { get; set; }
        public string trans_create_ts { get; set; }
        public string trans_last_modified_ts { get; set; }
        public string case_key { get; set; }
        public string approved_by { get; set; }
        public string approved_dt { get; set; }
    }

    public class ListTransactionSearchInputModel
    {
        public List<TransactionSearchInputModel> TransactionSearchInputModel { get; set; }
    }
}