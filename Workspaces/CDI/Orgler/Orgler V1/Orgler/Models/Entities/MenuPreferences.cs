using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Entities
{
    public class MenuPreferences
    {
        public string UserName { get; set; }
        public IList<string> Preferences { get; set; }
    }
}