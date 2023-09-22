using System.Security.Cryptography;
using System.Text;

namespace CommonUtils.Utils {
    /// <summary>
    /// Helper for operations with passwords like hashing and e.t.c
    /// </summary>
    public class PasswordHelper {
        const int keySize = 16;
        const int iterations = 3500;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="password">Value hash</param>
        /// <returns></returns>
        public string HashPassword(string password) {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                Encoding.UTF8.GetBytes("salt"),
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }
        /// <summary>
        /// Compare two passwords strings
        /// </summary>
        /// <param name="password">Input password</param>
        /// <param name="hash">Hashed password</param>
        /// <returns></returns>
        public bool VerifyPasword(string password, string hash) {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes("salt"), iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
