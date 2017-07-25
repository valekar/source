using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml;

namespace ARC.Utility
{
    public static class Parser
    {
        public static T Parse<T>(this string value)
        {
            T result = default(T);
            if (!string.IsNullOrEmpty(value))
            {
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));
                result = (T)tc.ConvertFrom(value);
            } return result;
        }

        public static IEnumerable<T> SplitTo<T>(this string str, params char[] separator) where T : IConvertible
        {
            foreach (var s in str.Split(separator, StringSplitOptions.None))
                yield return (T)Convert.ChangeType(s, typeof(T));
        }

        public static string LeftOf(this string s, char c)
        {
            int ndx = s.IndexOf(c);
            if (ndx >= 0)
            {
                return s.Substring(0, ndx);
            }

            return s;
        }

        public static string RightOf(this string s, char c)
        {
            int ndx = s.IndexOf(c);
            if (ndx == -1)
                return s;
            return s.Substring(ndx + 1);
        }

        public static T ConvertTo<T>(this string text)
        {
            T res = default(T);
            System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (tc.CanConvertFrom(text.GetType()))
                res = (T)tc.ConvertFrom(text);
            else
            {
                tc = System.ComponentModel.TypeDescriptor.GetConverter(text.GetType());
                if (tc.CanConvertTo(typeof(T)))
                    res = (T)tc.ConvertTo(text, typeof(T));
                else
                    throw new NotSupportedException();
            }
            return res;
        }

        public static object ForDatabase(this string str)
        {
            if (str == null)
            {
                return System.DBNull.Value;
            }

            return str;
        }
    }

    public static class ConvertExtensions
    {
        public static string StripHtml(this string input)
        {
            // Will this simple expression replace all tags???
            var tagsExpression = new Regex(@"</?.+?>");
            return tagsExpression.Replace(input, " ");
        }

        public static int ToInt(this string number, int defaultInt)
        {
            int resultNum = defaultInt;
            try
            {
                if (!string.IsNullOrEmpty(number))
                    resultNum = Convert.ToInt32(number);
            }
            catch
            {
            }
            return resultNum;
        }

        public static decimal ToDecimal(this string value)
        {
            decimal number;

            Decimal.TryParse(value, out number);

            return number;
        }

        public static Int16 ToInt16(this string value)
        {
            Int16 result = 0;

            if (!string.IsNullOrEmpty(value))
                Int16.TryParse(value, out result);

            return result;
        }

        public static Int32 ToInt32(this string value)
        {
            Int32 result = 0;

            if (!string.IsNullOrEmpty(value))
                Int32.TryParse(value, out result);

            return result;
        }

        public static Int64 ToInt64(this string value)
        {
            Int64 result = 0;

            if (!string.IsNullOrEmpty(value))
                Int64.TryParse(value, out result);

            return result;
        }

        public static bool Match(this string value, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(value);
        }

        public static string ToProperCase(this string text)
        {
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(text);
        }

        public static bool? ToBooleanNullable(this string value)
        {
            if (string.Compare("T", value, true) == 0)
            {
                return true;
            }
            if (string.Compare("F", value, true) == 0)
            {
                return false;
            }
            if (string.Compare("True", value, true) == 0)
            {
                return true;
            }
            if (string.Compare("False", value, true) == 0)
            {
                return false;
            }
            if (string.Compare("Y", value, true) == 0)
            {
                return true;
            }
            if (string.Compare("N", value, true) == 0)
            {
                return false;
            }
            if (string.Compare("Yes", value, true) == 0)
            {
                return true;
            }
            if (string.Compare("No", value, true) == 0)
            {
                return false;
            }
            if (string.Compare("1", value, true) == 0)
            {
                return true;
            }
            if (string.Compare("0", value, true) == 0)
            {
                return false;
            }
            bool result;
            if (bool.TryParse(value, out result))
            {
                return result;
            }
            else return null;
        }

        public static bool ToBoolean(this string value)
        {
            var val = value.ToLower().Trim();
            if (val == "false")
                return false;
            if (val == "f")
                return false;
            if (val == "true")
                return true;
            if (val == "t")
                return true;
            if (val == "yes")
                return true;
            if (val == "no")
                return false;
            if (val == "y")
                return true;
            if (val == "n")
                return false;
            if (val == "1")
                return true;
            if (val == "0")
                return false;
            return false;
        }


        public static string XmlSerialize<T>(this T objectToSerialise) where T : class
        {
            var serialiser = new XmlSerializer(typeof(T));
            string xml;
            MemoryStream memStream = null;
            try
            {
                memStream = new MemoryStream();
                using (var xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8))
                {
                    memStream = null;
                    serialiser.Serialize(xmlWriter, objectToSerialise);
                    xml = Encoding.UTF8.GetString(memStream.GetBuffer());                    
                }
                
            }
            finally
            {
                if (memStream!=null)
                memStream.Dispose();
            }
            // ascii 60 = '<' and ascii 62 = '>'
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml;
        }

        public static T ToEnum<T>(this string value)
        {
            return ToEnum<T>(value, false);
        }

        public static T ToEnum<T>(this string value, bool ignorecase)
        {
            if (value == null)
                throw new ArgumentNullException("Value");

            value = value.Trim();

            if (value.Length == 0)
                throw new ArgumentNullException("Must specify valid information for parsing in the string.", "value");

            Type t = typeof(T);
            if (!t.IsEnum)
                throw new ArgumentException("Type provided must be an Enum.", "T");

            return (T)Enum.Parse(t, value, ignorecase);
        }

        public static int ToInteger(this string value, int defaultvalue)
        {
            return (int)ToDouble(value, defaultvalue);
        }

        public static int ToInteger(this string value)
        {
            return ToInteger(value, 0);
        }

        public static double ToDouble(this string value, double defaultvalue)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            else return defaultvalue;
        }

        public static DateTime? ToDateTime(this string value, DateTime? defaultvalue)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            else return defaultvalue;
        }
        
        public static DateTime? ToDateTime(this string value)
        {
            return ToDateTime(value, null);
        }

        public static double ToDouble(this string value)
        {
            return ToDouble(value, 0);
        }

        public static T XmlDeserialize<T>(this string xml) where T : class
        {
            var serialiser = new XmlSerializer(typeof(T));
            T newObject;

            StringReader stringReader = null;            
            try
            {
                stringReader = new StringReader(xml);
                using (var xmlReader = new XmlTextReader(stringReader))
                {
                    try
                    {
                        stringReader = null;
                        newObject = serialiser.Deserialize(xmlReader) as T;
                        
                    }
                    catch (InvalidOperationException) // String passed is not Xml, return null
                    {
                        return null;
                    }
                }
            }
            finally
            {
                if (stringReader != null)
                stringReader.Dispose();
            }

            return newObject;
        }

        public static void ToException(this string message)
        {
            throw new Exception(message);
        }
    }
}