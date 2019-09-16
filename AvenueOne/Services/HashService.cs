using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace AvenueOne.Services
{
    public class HashService
    {
        private static int _defaultHashSize = 20;
        private static int _defaultSaltSize = 16;
        private static int _defaultIterations = 10000;

        public static byte[] CreateSalt()
        {
            return CreateSalt(_defaultSaltSize);
        }
        public static byte[] CreateSalt(int SaltSize)
        {
            if (SaltSize <= 0)
                throw new ArgumentException("Salt size cannot be less than 0.");

            byte[] salt = new byte[SaltSize];
            //generate salt
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            rNGCryptoServiceProvider.GetBytes(salt);

            rNGCryptoServiceProvider.Dispose();
            return salt;
            
        }
        public static string Hash(string text)
        {
            return Hash(text, CreateSalt());
        }
        public static string Hash(string text, byte[]salt)
        {
            return Hash(text, salt, _defaultHashSize);
        }
        public static string Hash(string text, byte[] salt, int sizeHash)
        {
            return Hash(text, salt, sizeHash, _defaultIterations);
        }
        public static string Hash(string text, byte[] salt, int sizeHash, int sizeIteration)
        {
            //TODO: for more security create a salt to append at the middle and end of hash;
            int iterationSize = sizeIteration;
            int hashSize = sizeHash;
            int saltSize = _defaultSaltSize;
            //generate hash
             var pbkdf2 = new Rfc2898DeriveBytes(text, salt, iterationSize);
            byte[] hash = pbkdf2.GetBytes(hashSize);
            //combine hash and salt
            byte[] hashBytes = new byte[saltSize + hashSize];
            Array.Copy(salt, 0, hashBytes, 0, saltSize);
            Array.Copy(hash, 0, hashBytes, saltSize, hashSize);
            //convert to base64
            var hashedString = Convert.ToBase64String(hashBytes);

            pbkdf2.Dispose();
            return hashedString;
        }

        public static bool Verify(string text, string hashedText)
        {
            byte[] hashedBytes = Convert.FromBase64String(hashedText);
            byte[] salt = new byte[_defaultSaltSize];
            if (hashedBytes.Length < _defaultSaltSize) // the string was not long enough to be a hashed password.
                return false;
            //get original salt
            Array.Copy(hashedBytes, 0, salt, 0, _defaultSaltSize);
            //hash password with same salt
            text = Hash(text, salt);
            //compare
            if (text == hashedText)
                return true;
            return false;
        }

        //TODO: i want to append a random identifier to the hash, so i can identify that the hash came from this hash service
        //but i want the string that is appened to be random.
        // (perhaps convert string to int and the total equals a certain number?) if it  is the it was from this service.
        //private static void Identifier(string hash) 
        //{
        //    byte[] salt = new byte[_defaultSaltSize];
        //    //generate salt
        //    RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
        //    rNGCryptoServiceProvider.GetBytes(salt);
        //}
    }
}
