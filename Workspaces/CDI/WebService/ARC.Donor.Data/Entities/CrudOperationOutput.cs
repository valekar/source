using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities
{
    public class CrudOperationOutput
    {
        public string strSPQuery { get; set; }
        public List<object> parameters { get; set; }
    }

}
