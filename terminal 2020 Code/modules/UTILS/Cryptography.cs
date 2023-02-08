using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace sorec_gamma.modules.UTILS
{
    public static class Cryptography
    {
        private static byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private static int BlockSize = 128;

        public static string Encrypt(string value, string password)
        {
            if (value == null || password == null || password == "") return null;
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            //Encrypt
            SymmetricAlgorithm crypt = Aes.Create();
            HashAlgorithm hash = MD5.Create();
            crypt.BlockSize = BlockSize;
            crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(password));
            crypt.IV = IV;
            string result = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream =
                   new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes, 0, bytes.Length);
                }

                result = Convert.ToBase64String(memoryStream.ToArray());
            }
            return result;
        }

        public static string Decrypt(string value, string password)
        {
            string result = null;
            if (password == null || password == "") return null;
            //Decrypt
            byte[] bytes = Convert.FromBase64String(value);
            SymmetricAlgorithm crypt = Aes.Create();
            HashAlgorithm hash = MD5.Create();
            crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(password));
            crypt.IV = IV;

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                using (CryptoStream cryptoStream =
                   new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    byte[] decryptedBytes = new byte[bytes.Length];
                    cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                    result = Encoding.Unicode.GetString(decryptedBytes);
                }
            }
            return result;
        }
    }
}
