using System;
using System.Security.Cryptography;
using System.Text;
using ecv.crypto;

namespace ecv
{
    class Program
    {

        static void Main(string[] args)
        {
            var msg = "ENCRYPT THIS";


            var key = "password";
            var byteKey = Encoding.ASCII.GetBytes(key);
            var gcm = new AESGCM(byteKey);
            var content = gcm.Encrypt(Encoding.ASCII.GetBytes(msg));
            Console.WriteLine(Encoding.UTF8.GetString(content));

            byte[] decrypted = null;
            try
            {
                decrypted = gcm.Decrypt(content);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Invalid password or corrupted data");
            }

            Console.WriteLine(Encoding.UTF8.GetString(decrypted));


        }
    }
}