using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace ARC.Utility
{
    public enum MaskStyle
    {       
        All,
        AlphaNumericOnly,
    }

    public static class StringMask
    {
        public static readonly char DefaultMaskCharacter = '*';

        public static bool IsLengthAtLeast(this string value, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", length,
                                                        "The length must be a non-negative number.");
            }

            return value != null
                        ? value.Length >= length
                        : false;
        }

        public static string Mask(this string sourceValue, char maskChar, int numExposed, MaskStyle style)
        {
            var maskedString = sourceValue;

            if (sourceValue.IsLengthAtLeast(numExposed))
            {
                var builder = new StringBuilder(sourceValue.Length);
                int index = maskedString.Length - numExposed;

                if (style == MaskStyle.AlphaNumericOnly)
                {
                    CreateAlphaNumMask(builder, sourceValue, maskChar, index);
                }
                else
                {
                    builder.Append(maskChar, index);
                }

                builder.Append(sourceValue.Substring(index));
                maskedString = builder.ToString();
            }

            return maskedString;
        }

        public static string Mask(this string sourceValue, char maskChar, int numExposed)
        {
            return Mask(sourceValue, maskChar, numExposed, MaskStyle.All);
        }

        public static string Mask(this string sourceValue, char maskChar)
        {
            return Mask(sourceValue, maskChar, 0, MaskStyle.All);
        }

        public static string Mask(this string sourceValue, int numExposed)
        {
            return Mask(sourceValue, DefaultMaskCharacter, numExposed, MaskStyle.All);
        }

        public static string Mask(this string sourceValue)
        {
            return Mask(sourceValue, DefaultMaskCharacter, 0, MaskStyle.All);
        }

        public static string Mask(this string sourceValue, char maskChar, MaskStyle style)
        {
            return Mask(sourceValue, maskChar, 0, style);
        }

        public static string Mask(this string sourceValue, int numExposed, MaskStyle style)
        {
            return Mask(sourceValue, DefaultMaskCharacter, numExposed, style);
        }

        public static string Mask(this string sourceValue, MaskStyle style)
        {
            return Mask(sourceValue, DefaultMaskCharacter, 0, style);
        }

        private static void CreateAlphaNumMask(StringBuilder buffer, string source, char mask, int length)
        {
            for (int i = 0; i < length; i++)
            {
                buffer.Append(char.IsLetterOrDigit(source[i])
                                ? mask
                                : source[i]);
            }
        }
    }
}