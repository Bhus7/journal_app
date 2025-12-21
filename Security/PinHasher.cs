using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JournalApp.Security
{
    public static class PinHasher
    {
        public static string CreateSalt(int size = 16)
        {
            var saltBytes = new byte[size];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public static string CreatePinHash(string pin, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                pin,
                saltBytes,
                10000,
                HashAlgorithmName.SHA256 
                );

            byte[] hashBytes = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hashBytes);

        }

        public static bool VerifyPin(string enteredPin, string savedHash, string savedSalt)
        {
            string newHash = CreatePinHash(enteredPin, savedSalt);
            return newHash == savedHash;
        }
    }
}
