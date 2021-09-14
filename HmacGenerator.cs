using System.Security.Cryptography;
using System.Text;

namespace game
{
    public class HMACGenerator
    {
        public static byte[] CreatesHMAC(byte[] secretKey,string move) 
        {
            using var hmac = new HMACSHA256(secretKey);
            return hmac.ComputeHash(Encoding.Default.GetBytes(move));
        }
    }
}