using System.Security.Cryptography;
using System.Text;

namespace Management.Extentions.Helpers
{
    public static class PasswordHelper
    {
        public static string CreateMd5(string password)
        {
            var inputBytes = Encoding.ASCII.GetBytes(password);
            var hashBytes = MD5.HashData(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
