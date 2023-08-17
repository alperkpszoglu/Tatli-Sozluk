using System.Security.Cryptography;
using System.Text;

namespace SozlukApp.Common.Infrastructure
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
