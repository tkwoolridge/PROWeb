using System.Security.Cryptography;

namespace PROWeb.Common.Security
{
    public class Encryptor
    {
        private const int _keySize = 256;
        private const int _blockSize = 128;

        public static EncryptionResult Encrypt(string plainText)
        {
            // Generate a random key and IV
            using var aes = Aes.Create();
            aes.KeySize = _keySize;
            aes.BlockSize = _blockSize;

            // Generate a random key and IV for each encryption operation
            aes.GenerateKey();
            aes.GenerateIV();

            byte[] encryptedData;

            // Create encryptor and encrypt the data
            using (var encryptor = aes.CreateEncryptor())
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                encryptedData = msEncrypt.ToArray();
            }

            // Package everything together, storing IV with the encrypted data
            var result = EncryptionResult.CreateEncryptedData(
                encryptedData,
                aes.IV,
                Convert.ToBase64String(aes.Key)
            );

            return result;
        }
    }
}
