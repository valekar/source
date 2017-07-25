using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stuart_V2.Models.Entities.Constituents
{
    public class Birth
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_birth_dy_num { get; set; }
        public string cnst_birth_mth_num { get; set; }
        public string cnst_birth_yr_num { get; set; }
        public string cnst_birth_strt_ts { get; set; }
        public string cnst_birth_end_dt { get; set; }
        public string cnst_birth_best_los_ind { get; set; }
        public string trans_key { get; set; }
        public string user_id { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string appl_src_cd { get; set; }
        public string load_id { get; set; }
        public string is_previous { get; set; }
        public string transaction_key { get; set; }
        public string trans_status { get; set; }
        public string inactive_ind { get; set; }
        public string strx_row_stat_cd { get; set; }
        public string unique_trans_key { get; set; }
        public string transNotes { get; set; }
        //for missy
        public string distinct_count { get; set; } 

        public Birth()
        {
            this.transNotes = string.Empty;
        }

    }

    //Class for Birth Data Input Entity 
    public class ConstituentBirthInput
    {
        public string RequestType { get; set; }
        public Int64 MasterID { get; set; }
        public string UserName { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public Int64? CaseNumber { get; set; }
        public string OldSourceSystemCode { get; set; }
        public string OldBestLOSInd { get; set; }
        public decimal? NewBirthDayNumber { get; set; }
        public decimal? NewBirthMonthNumber { get; set; }
        public decimal? NewBirthYearNumber { get; set; }
        public string SourceSystemCode { get; set; }
        public byte BestLOS { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }

        public ConstituentBirthInput()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            UserName = p.GetUserName(); // p.Identity.Name;
            ConstType = string.Empty;
            Notes = string.Empty;
            OldSourceSystemCode = string.Empty;
            OldBestLOSInd = "0";
            NewBirthDayNumber = 0;
            NewBirthMonthNumber = 0;
            NewBirthYearNumber = 0;
            SourceSystemCode = string.Empty;
            BestLOS = 0;
        }
    }

    //Class for Birth Data Output Entity 
    public class ConstituentBirthOutput
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }
}