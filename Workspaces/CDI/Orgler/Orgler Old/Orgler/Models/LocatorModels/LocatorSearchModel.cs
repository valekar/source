﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.LocatorModels
{
    public class LocatorSearchModel
    {
        public string emailKey { get; set; }
        public string emailAddress { get; set; }
        public string emailCategory { get; set; }
        public bool emailExactMatch { get; set; }
    }
}