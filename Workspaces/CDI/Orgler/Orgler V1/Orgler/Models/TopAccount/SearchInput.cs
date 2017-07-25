using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Orgler.Models.TopAccount
{
    /* Name: TopOrgsInputModel
   * Purpose: This class is the input model for searching top organizations. */
    public class TopOrgsSearchInput
    {
        public string los { get; set; }
        public string naics_cd { get; set; }
        public string naicsStatus { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "RFM value must be numeric")]
        public string rfm_scr { get; set; }
        public List<string> listRuleKeyword { get; set; }
        public List<string> listNaicsCodes { get; set; }
        public string enterpriseOrgAssociation { get; set; }
        public string AnswerSetLimit { get; set; }
        public string srch_user_name { get; set; }
    }    
}