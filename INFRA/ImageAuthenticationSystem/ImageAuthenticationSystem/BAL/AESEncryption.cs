using System;
using System.Security.Cryptography;
using System.Text;

namespace ImageAuthenticationSystem.BAL
{
	public class AESEncryption
	{
		public AESEncryption()
		{
		}

        public static string EncryptStringToBytes(string plainText, string key)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = AdjustKeySize(Encoding.UTF8.GetBytes(key), aesAlg.KeySize / 8);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptStringFromBytes(string cipherText, string key)
        {
            string plaintext = null;

            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = AdjustKeySize(Encoding.UTF8.GetBytes(key), aesAlg.KeySize / 8);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public static byte[] AdjustKeySize(byte[] key, int size)
        {
            if (key.Length == size)
                return key;
            else if (key.Length < size)
            {
                // Pad the key with zeroes or any other suitable padding mechanism
                byte[] paddedKey = new byte[size];
                Array.Copy(key, 0, paddedKey, 0, key.Length);
                return paddedKey;
            }
            else
            {
                // Truncate the key if it's longer than the desired size
                byte[] truncatedKey = new byte[size];
                Array.Copy(key, 0, truncatedKey, 0, size);
                return truncatedKey;
            }
        }
    }
}
