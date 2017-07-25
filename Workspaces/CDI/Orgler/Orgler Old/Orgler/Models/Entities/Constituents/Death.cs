using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities.Constituents
{
    public class Death
    {
        public string cnst_mstr_id { get; set; }
        public string cnst_srcsys_id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public string cnst_death_dt { get; set; }
        public string cnst_deceased_cd { get; set; }
        public string cnst_death_strt_ts { get; set; }
        public string cnst_death_end_dt { get; set; }
        public string cnst_death_best_los_ind { get; set; }
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

        public Death()
        {
            this.transNotes = string.Empty;
        }
    }

    //Class for Death Data Input Entity 
    public class ConstituentDeathInput
    {
        public string RequestType { get; set; }
        public Int64 MasterID { get; set; }
        public string UserName { get; set; }
        public string ConstType { get; set; }
        public string Notes { get; set; }
        public Int64? CaseNumber { get; set; }
        public string OldSourceSystemCode { get; set; }
        public string OldBestLOSInd { get; set; }
        public DateTime? NewDeathDate { get; set; }
        public char? NewDeceasedCode { get; set; }
        public string SourceSystemCode { get; set; }
        public byte BestLOS { get; set; }
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
      
        public ConstituentDeathInput()
        {
            System.Security.Principal.IPrincipal p = HttpContext.Current.User;
            UserName = p.GetUserName(); //p.Identity.Name;
            ConstType = string.Empty;
           // Notes = "This is a test";
           // CaseNumber = 0;
            OldSourceSystemCode = string.Empty;
            OldBestLOSInd = "0";
            NewDeathDate = default(DateTime);
            NewDeceasedCode = null;
            SourceSystemCode = string.Empty;
            BestLOS = 0;
        }
    }

    //Class for Death Data Output Entity 
    public class ConstituentDeathOutput
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }
}