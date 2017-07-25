using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities.Orgler
{
    /* Name: listString
    * Purpose: New custom class to store composite output of two strings in text-status format */
    public class listString
    {
        public string strText { get; set; }
        public string status { get; set; }
    }

    /* Name: listStringInput
    * Purpose: New custom class to store composite output of two strings in text-status format */
    public class listStringInput
    {
        public List<string> listInputString { get; set; }
    }

    /* Name: idCounter
    * Purpose: New custom class to store count of an id */
    public class idCounter
    {
        public string id { get; set; }
        public int cnt { get; set; }
    }

    /* Name: bridgeCounter
    * Purpose: New custom class to store count of an id */
    public class bridgeCounter
    {
        public string id { get; set; }
        public string arc_srcsys_cd { get; set; }
        public int cnt { get; set; }
    }
}
