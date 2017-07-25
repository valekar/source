using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Case
{
   public class CaseInputSearchModel
    {
        public string CaseId { get; set; }
        public string CaseName { get; set; }
        public string ReferenceSource { get; set; }
        public string ReferenceId { get; set; }
        public string CaseType { get; set; }
        public string CaseStatus { get; set; }
        public string ConstituentName { get; set; }
        public string UserName { get; set; }
        public string ReportedDateFrom { get; set; }
        public string ReportedDateTo { get; set; }
        public string UserId { get; set; }
    }

    public class ListCaseInputSearchModel
    {
        public List<CaseInputSearchModel> CaseInputSearchModel { get; set; }
    }
}
