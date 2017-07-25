using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities.AccountMonitoring
{
     public class AccountSearchInputModel
    {
        public string masterId { get; set; }
        public string naicsCode { get; set; }
        public string sourceSystem { get; set; }
      //  public string chapterSystem { get; set; }
        public string sourceSystemId { get; set; }
        public string organizationName { get; set; }
        public string addressLine { get; set; }
        public string city { set; get; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string emailAddress { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public string masteringType { get; set; }
        public bool naicsSuggestionPresentInd { get; set; }
        public bool potentialMergeInd { get; set; }
    }
    public class AccountSearchListInputModel
    {
        public List<AccountSearchInputModel> listAccountSearchInputModel { get; set; }
        public string AnswerSetLimit { get; set; }
    }

    public class AccountSearchOutputModel
    {
        public string arc_srcsys_cd { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string cnst_mstr_id { get; set; }
        public string acc_nm { get; set; }
        public string addr_line1 { get; set; }
        public string phn_num { get; set; }
        public string email_addr { get; set; }
        public string naics_cd1 { get; set; }
        public string naics_cd2 { get; set; }
        public string naics_cd3 { get; set; }
        public string rfm_scr { get; set; }
        public string identif_typ { get; set; }
        public bool naics_sggstn_prsnt_ind { get; set; }
        public bool pot_merge_ind { get; set; }
        public string monetary_val { get; set; }
        public string naics_dsc { get; set; }

    } 
    
}

