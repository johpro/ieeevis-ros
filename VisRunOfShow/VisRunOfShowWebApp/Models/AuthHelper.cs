using System.Text;
using System;
using System.Security.Cryptography;

namespace VisRunOfShowWebApp.Models
{
    public class AuthHelper
    {
        private readonly SHA256 _sha256 = SHA256.Create();
        private readonly string _privateKey;

        public AuthHelper(string privateKey)
        {
            _privateKey = privateKey;
        }

        public string GetKey(string? content)
        {
            var bytes = Encoding.UTF8.GetBytes(content + _privateKey);
            var hash = _sha256.ComputeHash(bytes)[..8];
            return BytesToTokenBase64(hash);
        }

       

        public string GetTokenBase64EncodedHash(string toHash)
        {
            var bytes = Encoding.UTF8.GetBytes(toHash);
            var hash = _sha256.ComputeHash(bytes);
            return BytesToTokenBase64(hash);
        }

        public static string BytesToTokenBase64(byte[] bytes)
        {
            var s = Convert.ToBase64String(bytes, Base64FormattingOptions.None).ToCharArray();
            var len = 0;
            for (; len < s.Length; len++)
            {
                var ch = s[len];
                if (ch == '=')
                    break;
                if (ch == '+')
                    s[len] = '-';
                else if (ch == '/')
                    s[len] = '_';
            }

            return new string(s.AsSpan(0, len));

        }


        public static bool SafeCompareEquality(string? s1, string? s2)
        {
            if (s1 == null || s2 == null)
                return s1 == null && s2 == null;
            var maxLen = Math.Max(s1.Length, s2.Length);
            var minLen = Math.Min(s1.Length, s2.Length);
            var isCorrect = true;
            for (int i = 0; i < maxLen; i++)
            {
                if (i >= minLen || s1[i] != s2[i])
                {
                    isCorrect = false;
                }
            }

            return isCorrect;
        }

    }
}
