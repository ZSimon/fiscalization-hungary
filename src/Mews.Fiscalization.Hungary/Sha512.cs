using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Mews.Fiscalization.Hungary
{
    public static class Sha512
    {
        public static string GetStringHash(string input)
        {
            using (var sha = SHA512.Create())
            {
                var encoding = Encoding.UTF8;
                var bytes = sha.ComputeHash(encoding.GetBytes(input));
                return String.Join("", bytes.Select(b => b.ToString("x2"))).ToUpper();
            }
        }

        public static string GetSha3Hash(string data)
        {
            var hashAlgorithm = new Org.BouncyCastle.Crypto.Digests.Sha3Digest(512);

            byte[] input = Encoding.ASCII.GetBytes(data);

            hashAlgorithm.BlockUpdate(input, 0, input.Length);

            byte[] result = new byte[64]; // 512 / 8 = 64
            hashAlgorithm.DoFinal(result, 0);

            string hashString = BitConverter.ToString(result);
            return hashString.Replace("-", "").ToUpperInvariant();
        }

        public static string GetRandomRequestId()
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var guid = Guid.NewGuid();
            var bits = new BitArray(guid.ToByteArray());
            var chars = new StringBuilder();
            var accumulator = 0;
            for (var i = 0; i <= bits.Length; i++)
            {
                if (i > 0 && (i % 5 == 0 || i == bits.Length))
                {
                    chars.Append(alphabet[accumulator]);
                    accumulator = 0;
                    if (i == bits.Length)
                    {
                        break;
                    }
                }
                accumulator += bits[i] ? (1 << i % 5) : 0;
            }

            return chars.ToString();
        }
    }
}