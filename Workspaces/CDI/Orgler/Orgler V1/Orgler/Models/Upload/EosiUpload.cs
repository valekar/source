﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.Upload
{
    public class EosiUploadInput
    {
        public Int64 strSeqKey { get; set; }
        public string strEnterpriseOrgId { get; set; }
        public string strMasterId { get; set; }
        public string strSourceSystemCode { get; set; }
        public string strSourceId { get; set; }
        public string strSecondarySourceId { get; set; }
        public string strParentEnterpriseOrgId { get; set; }
        public string strAltSourceCode { get; set; }
        public string strAltSourceId { get; set; }
        public string strOrgName { get; set; }
        public string strAddress1Street1 { get; set; }
        public string strAddress1Street2 { get; set; }
        public string strAddress1City { get; set; }
        public string strAddress1State { get; set; }
        public string strAddress1Zip { get; set; }
        public string strPhone1 { get; set; }
        public string strPhone2 { get; set; }
        public string strNaicsCode { get; set; }
        public string strCharacteristics1Code { get; set; }
        public string strCharacteristics1Value { get; set; }
        public string strCharacteristics2Code { get; set; }
        public string strCharacteristics2Value { get; set; }
        public string strRMIndicator { get; set; }
        public string strNotes { get; set; }
        public string strTransKey { get; set; }
    }

    public class EosiUploadControllerInput
    {
        public List<EosiUploadInput> input { get; set; }
        public string strUserName { get; set; }
        public string strUploadFileName { get; set; }
        public int intFileSize { get; set; }
        public string strDocExtention { get; set; }
    }
}