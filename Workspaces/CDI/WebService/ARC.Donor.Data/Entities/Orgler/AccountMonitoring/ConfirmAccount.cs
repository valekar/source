using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler.AccountMonitoring
{
    /* Name: ConfirmAccountInput
    * Purpose: This class is the input model for confirming an organization account */
    public class ConfirmAccountInput
    {
        public string master_id { get; set; }
        public string usr_nm { get; set; }
    }
}
