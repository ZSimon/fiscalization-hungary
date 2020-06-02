using System.Text;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Mews.Fiscalization.Hungary
{
    internal static class Aes
    {
        public static string Decrypt(string key, byte[] data)
        {
            var cipher = CipherUtilities.GetCipher("AES");
            cipher.Init(false, new KeyParameter(Encoding.UTF8.GetBytes(key)));
            byte[] final = cipher.DoFinal(data);
            return Encoding.UTF8.GetString(final);
        }
    }
}