using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Utility.Extensions
{
    public class ERROR_MESSAGE
    {
        public static readonly string MASTER_ID = "Enter a valid master id ";
        public static readonly string COMMUNICATION_CHANNEL = "Enter a valid communication channel (Mandatory Field) ";
        public static readonly string LINE_OF_SERVICE_CODE = "Enter a valid line of service code (Mandatory Field) ";
        public static readonly string EMPTY_FIELDS = "Enter Master Id or Source System Id and Source System Code or alteast one locator value. ";
        public static readonly string SOURCE_SYSTEM_ID = "Enter a valid source system id ";
        public static readonly string SOURCE_SYSTEM_CODE = "Enter a valid source system code ";

        public static readonly string NAME_ORG = "Enter either individual person name or organization name";

        public static readonly string MESSAGE_PREF_TYPE = "Enter a valid message preference type.(Mandatory Field)";
        public static readonly string MESSAGE_PREF_VALUE = "Enter a valid message preference value.(Mandatory Field)";
        public static readonly string MESSAGE_PREF_COMBINATION = "Invalid combination of line of service code, communication channel, message preference type and message preference value";
    }
}
