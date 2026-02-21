using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace PRH.Server.Repository
{
    public static class CryptoHelper
    {
        // Aqui usei 16 caracteres (128 bits):
        private static readonly byte[] _key =
            Encoding.UTF8.GetBytes("TrigeniusPRH2025");
        
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                return plainText;

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            aes.GenerateIV();
            var iv = aes.IV;

            using var encryptor = aes.CreateEncryptor(aes.Key, iv);
            var plainBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] cipherBytes;
            using (var ms = new MemoryStream())
            {
                // Primeiro escreve o IV no início
                ms.Write(iv, 0, iv.Length);

                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.FlushFinalBlock();
                }

                cipherBytes = ms.ToArray();
            }

            return Convert.ToBase64String(cipherBytes);
        }

        public static string Decrypt(string base64CipherText)
        {
            if (string.IsNullOrWhiteSpace(base64CipherText))
                return base64CipherText;

            var allBytes = Convert.FromBase64String(base64CipherText);

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Extrai o IV (primeiros 16 bytes)
            var iv = new byte[16];
            Array.Copy(allBytes, 0, iv, 0, iv.Length);

            var cipherBytes = new byte[allBytes.Length - iv.Length];
            Array.Copy(allBytes, iv.Length, cipherBytes, 0, cipherBytes.Length);

            using var decryptor = aes.CreateDecryptor(aes.Key, iv);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.FlushFinalBlock();
            }

            var plainBytes = ms.ToArray();
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}