using System.Text;
using System.Security.Cryptography;
using System;

namespace ARC.Utility.Models
{
    public enum eHashType
    {
        HMAC, HMACMD5, HMACSHA1, HMACSHA256, HMACSHA384, HMACSHA512,
        MACTripleDES, MD5, RIPEMD160, SHA1, SHA256, SHA384, SHA512
    }

    public static class Hasher
    {
        
//        string s = "Hello world!";
//        string hash = s.ComputeHash(Hasher.eHashType.RIPEMD160); 
//        MessageBox.Show(hash);
////      7f772647d88750add82d8e1a7a3e5c0902a346a3
        public static string ToMd5Hash(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] originalBytes = ASCIIEncoding.Default.GetBytes(value);
                byte[] encodedBytes = md5.ComputeHash(originalBytes);
                return BitConverter.ToString(encodedBytes).Replace("-", string.Empty);
            }
        }

        public static string GetSHA1Hash(this string stringToHash)
        {
            if (string.IsNullOrEmpty(stringToHash))
            {
                throw new ArgumentException("An empty string value cannot be hashed.");
            }

            Byte[] data = System.Text.Encoding.UTF8.GetBytes(stringToHash);
            Byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(data);
            return Convert.ToBase64String(hash);
        }

        
        
        private static byte[] GetHash(string input, eHashType hash)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);

            switch (hash)
            {
                case eHashType.HMAC:
                    return HMAC.Create().ComputeHash(inputBytes);

                case eHashType.HMACMD5:
                    return HMACMD5.Create().ComputeHash(inputBytes);

                case eHashType.HMACSHA1:
                    return HMACSHA1.Create().ComputeHash(inputBytes);

                case eHashType.HMACSHA256:
                    return HMACSHA256.Create().ComputeHash(inputBytes);

                case eHashType.HMACSHA384:
                    return HMACSHA384.Create().ComputeHash(inputBytes);

                case eHashType.HMACSHA512:
                    return HMACSHA512.Create().ComputeHash(inputBytes);

                case eHashType.MACTripleDES:
                    return MACTripleDES.Create().ComputeHash(inputBytes);

                case eHashType.MD5:
                    return MD5.Create().ComputeHash(inputBytes);

                case eHashType.RIPEMD160:
                    return RIPEMD160.Create().ComputeHash(inputBytes);

                case eHashType.SHA1:
                    return SHA1.Create().ComputeHash(inputBytes);

                case eHashType.SHA256:
                    return SHA256.Create().ComputeHash(inputBytes);

                case eHashType.SHA384:
                    return SHA384.Create().ComputeHash(inputBytes);

                case eHashType.SHA512:
                    return SHA512.Create().ComputeHash(inputBytes);

                default:
                    return inputBytes;
            }
        }

        public static string ComputeHash(this string input, eHashType hashType)
        {
            try
            {
                byte[] hash = GetHash(input, hashType);
                StringBuilder ret = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                    ret.Append(hash[i].ToString("x2"));

                return ret.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}