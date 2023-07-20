using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SozlukAppCommon.Infrastructure
{
    public static class PasswordEncryptor
    {
        public static string Encrypt(string password)
        {
            using var md5 = MD5.Create();

            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] hashByte = md5.ComputeHash(bytes);

            return Convert.ToHexString(hashByte);
        }
    }
}
