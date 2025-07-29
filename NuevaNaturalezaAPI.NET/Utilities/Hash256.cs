using System;
using System.Security.Cryptography;
using System.Text;

namespace NuevaNaturalezaAPI.NET.Utilities
{

    public class Hash256
    {
        public static string Hash(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = SHA256.HashData(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
