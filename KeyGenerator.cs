using System.Security.Cryptography;

namespace game
{
    public class KeyGenerator
    {
        public static byte[] CreatesKey128bit() 
        {
            var secretKey = new byte[16];
            using (var rngCsp = new RNGCryptoServiceProvider())
            rngCsp.GetBytes(secretKey);
            return secretKey;
        }
    }
}