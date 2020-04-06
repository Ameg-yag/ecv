
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ecv.crypto
{
    public class AESGCM
    {
        private byte[] _key;
        private byte[] _nonce;
        private int keySize = 16;
        private int nonceSize = 12;

        public AESGCM(byte[] key)
        {
            this._key = key;
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                this._key = sha256Hash.ComputeHash(this._key);  
            }  
            this._nonce = new byte[this.nonceSize];
            RandomNumberGenerator.Fill(this._nonce);
        }

        public byte[] Encrypt(byte[] toEncrypt, byte[] associatedData = null)
        {
            byte[] tag = new byte[this.keySize];
            byte[] nonce = new byte[this.nonceSize];
            byte[] cipherText = new byte[toEncrypt.Length];

            using (var cipher = new AesGcm(this._key))
            {
                cipher.Encrypt(nonce, toEncrypt, cipherText, tag, associatedData);
                return Concat(tag, Concat(nonce, cipherText));
            }
        }

        public byte[] Decrypt(byte[] cipherText, byte[] associatedData = null)
        {
            byte[] tag = SubArray(cipherText, 0, this.keySize);
            byte[] nonce = SubArray(cipherText, this.keySize, nonceSize);

            byte[] toDecrypt = SubArray(cipherText, this.keySize + this.nonceSize, cipherText.Length - tag.Length - nonce.Length);
            byte[] decryptedData = new byte[toDecrypt.Length];

            using (var cipher = new AesGcm(this._key))
            {
                cipher.Decrypt(nonce, toDecrypt, tag, decryptedData, associatedData);

                return decryptedData;
            }
        }
        private byte[] Concat(byte[] firstArray, byte[] secondArray)
        {
            byte[] output = new byte[firstArray.Length + secondArray.Length];
            System.Buffer.BlockCopy(firstArray, 0, output, 0, firstArray.Length);
            System.Buffer.BlockCopy(secondArray, 0, output, firstArray.Length, secondArray.Length);
            return output;
        }

        private byte[] SubArray(byte[] data, int start, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, start, result, 0, length);

            return result;
        }
    }
}