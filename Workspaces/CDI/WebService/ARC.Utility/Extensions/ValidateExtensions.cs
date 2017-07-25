using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace ARC.Utility
{
    public static class ValidateExtensions
    {       
        public static bool IsInValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (regex.IsMatch(s))
            {
                return false;
            }
            
            return true;
        }

        public static bool IsDate(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                DateTime dt;
                return (DateTime.TryParse(input, out dt));
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidUrl(this string text) {
            Regex rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(text);
        }

        public static bool IsGuid(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            const string pattern = @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$";
            return Regex.IsMatch(value, pattern);
        }

        public static bool IsValidIPAddress(this string s)
        {
            return Regex.IsMatch(s,
                    @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
        }

        public static bool IsStrongPassword(this string s)
        {
            bool isStrong = Regex.IsMatch(s, @"[\d]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[a-z]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[A-Z]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[\s~!@#\$%\^&\*\(\)\{\}\|\[\]\\:;'?,.`+=<>\/]");
            if (isStrong) isStrong = s.Length > 7;
            return isStrong;
        }

        public static bool IsUnicode(this string value)
        {
            int asciiBytesCount = System.Text.Encoding.ASCII.GetByteCount(value);
            int unicodBytesCount = System.Text.Encoding.UTF8.GetByteCount(value);

            if (asciiBytesCount != unicodBytesCount)
            {
                return true;
            }
            return false;
        }

        //used for validating emails
        public static bool IsInvalidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return false;
            }
            catch (FormatException)
            {
                return true;
            }
        }

        public static bool IsBoolean(this string value)
        {
            var val = value.ToLower().Trim();
            if (val == "false")
                return true;
            if (val == "f")
                return true;
            if (val == "true")
                return true;
            if (val == "t")
                return true;
            if (val == "yes")
                return true;
            if (val == "no")
                return true;
            if (val == "y")
                return true;
            if (val == "n")
                return true;
            if (val == "1")
                return true;
            if (val == "0")
                return true;
            return false;
        }
    }
}