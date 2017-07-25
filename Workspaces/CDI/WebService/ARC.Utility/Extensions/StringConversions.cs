using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ARC.Utility
{
    //    var myEnum = "Pending".ToEnum<Status>();
    //    var left = "Faraz Masood Khan".Left(100000);// works;
    //    var right = "CoreSystem Library".Right(10);
    //    var formatted = "My name is {0}.".Format("Faraz");
    //    var result = "CoreSystem".In("CoreSystem", "Library");

    public static class StringExtension
    {
        public static System.Security.SecureString ToSecureString(this String str)
        {
            System.Security.SecureString secureString = new System.Security.SecureString();
            foreach (Char c in str)
                secureString.AppendChar(c);

            return secureString;
        }

        public static NameValueCollection ToNameValueCollection(this String str, Char OuterSeparator, Char NameValueSeparator)
        {
            NameValueCollection nvText = null;
            str = str.TrimEnd(OuterSeparator);
            if (!String.IsNullOrEmpty(str))
            {
                String[] arrStrings = str.TrimEnd(OuterSeparator).Split(OuterSeparator);

                foreach (String s in arrStrings)
                {
                    Int32 posSep = s.IndexOf(NameValueSeparator);
                    String name = s.Substring(0, posSep);
                    String value = s.Substring(posSep + 1);
                    if (nvText == null)
                        nvText = new NameValueCollection();
                    nvText.Add(name, value);
                }
            }
            return nvText;
        }
        public static bool In(this string value, params string[] stringValues)
        {
            foreach (string otherValue in stringValues)
                if (string.Compare(value, otherValue) == 0)
                    return true;

            return false;
        }

        public static T ToEnum<T>(this string value)
            where T : struct
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        public static string Right(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(value.Length - length) : value;
        }

        
        public static string Left(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(0, length) : value;
        }

        public static string Format(this string value, object arg0)
        {
            return string.Format(value, arg0);
        }

        public static string Format(this string value, params object[] args)
        {
            return string.Format(value, args);
        }               
            
        public static string Truncate(this string text, int maxLength)
        {
            const string suffix = "...";
            string truncatedString = text;

            if (maxLength <= 0) return truncatedString;
            int strLength = maxLength - suffix.Length;

            if (strLength <= 0) return truncatedString;

            if (text == null || text.Length <= maxLength) return truncatedString;

            truncatedString = text.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;
            return truncatedString;
        }

        public static string FormatFileSize(this long fileSize)
        {
            string[] suffix = { "bytes", "KB", "MB", "GB" };
            long j = 0;

            while (fileSize > 1024 && j < 4)
            {
                fileSize = fileSize / 1024;
                j++;
            }
            return (fileSize + " " + suffix[j]);
        }

        public static byte[] ToBytes(this string content)
        {
            byte[] bytes = new byte[content.Length * sizeof(char)];
            System.Buffer.BlockCopy(content.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string ToUrlString(this string str)
        {
            if (String.IsNullOrEmpty(str)) return "";
            string stFormD = str.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (char t in
               from t in stFormD
               let uc = CharUnicodeInfo.GetUnicodeCategory(t)
               where uc != UnicodeCategory.NonSpacingMark
               select t)
            {
                sb.Append(t);
            }
            return Regex.Replace(sb.ToString().Normalize(NormalizationForm.FormC), "[\\W\\s]{1,}", "-").Trim('-');
        }

        public static string TakeFrom(this string s, string searchFor)
        {
            if (s.Contains(searchFor))
            {
                int length = Math.Max(s.Length, 0);

                int index = s.IndexOf(searchFor);

                return s.Substring(index, length - index);
            }

            return s;
        }

        public static IEnumerable<int> IndicesOf(this string searchIn, string searchFor)
        {
            if (string.IsNullOrEmpty(searchFor)) yield break;

            int lastLoc = searchIn.IndexOf(searchFor);
            while (lastLoc != -1)
            {
                yield return lastLoc;
                lastLoc = searchIn.IndexOf(searchFor, startIndex: lastLoc + 1);
            }
        }
    }
}