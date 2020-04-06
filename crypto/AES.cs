using System;
using System.IO;
using System.Security.Cryptography;

namespace ecv.crypto
{
    public class AES
    {
        private byte[] key;
        private byte[] IV;
        public AES(byte[] key, byte[] IV)
        {
            this.key = key;
            this.IV = IV;
        }
        public byte[] Encrypt(byte[] bytesToBeEncrypted)
        {
            byte[] encryptedBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = this.key;
                    AES.IV = this.IV;
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        public byte[] Decrypt(byte[] encryptedMsg)
        {
            byte[] decryptedBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = this.key;
                    AES.IV = this.IV;
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedMsg, 0, encryptedMsg.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }

        public void Store(byte[] encrypted)
        {
            byte[] rv = new byte[this.IV.Length + encrypted.Length];
            System.Buffer.BlockCopy(this.IV, 0, rv, 0, this.IV.Length);
            System.Buffer.BlockCopy(encrypted, 0, rv, this.IV.Length, encrypted.Length);
                        
        }
    }
}