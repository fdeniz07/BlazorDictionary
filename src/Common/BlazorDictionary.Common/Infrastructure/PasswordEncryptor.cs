using System.Security.Cryptography;
using System.Text;

namespace BlazorDictionary.Common.Infrastructure
{
    public class PasswordEncryptor
    {
        public static string Encrypt(string password)
        {
            using var md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(password); //Disaridan gönderilen password Byte[] döndürülür
            byte[] hasBytes = md5.ComputeHash(inputBytes); // Byte[] döndürdügümüz sifreyi md5 sifreleme tekniklerini kullanarak dönüstürüyoruz

            return Convert.ToHexString(hasBytes); // Sifrelenmis datayi stringe cevirip, geri döndürüyoruz.
        }
    }
}
