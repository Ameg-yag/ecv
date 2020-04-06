using System.Security.Cryptography;

namespace ecv.crypto
{
    public static class RandomBytes
    {
        public static byte[] Generate(int bytes)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[bytes];
                rng.GetBytes(key);
                return key;
            }
        }
    }
}