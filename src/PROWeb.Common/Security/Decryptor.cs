using System.Security.Cryptography;

namespace PROWeb.Common.Security
{
    public class Decryptor
    {
        private const int KeySize = 256;
        private const int BlockSize = 128;

        public static string Decrypt(EncryptionResult encryptionResult)
        {
            var key = Convert.FromBase64String(encryptionResult.Key);
            var (iv, encryptedData) = encryptionResult.GetIVAndEncryptedData();

            using var aes = Aes.Create();
            aes.KeySize = KeySize;
            aes.BlockSize = BlockSize;
            aes.Key = key;
            aes.IV = iv;

            // Create decryptor and decrypt the data
            using var decryptor = aes.CreateDecryptor();
            using var msDecrypt = new MemoryStream(encryptedData);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            try
            {
                return srDecrypt.ReadToEnd();
            }
            catch (CryptographicException ex)
            {
                // Log the error securely - avoid exposing details
                throw new CryptographicException("Decryption failed", ex);
            }
        }
    }
}
