using System;
using System.Security.Cryptography;

namespace CrossCutting.Security.Generators
{
    /// <summary>
    /// Secure random number generator
    /// </summary>
    internal class SecureRNG
    {
        private readonly RNGCryptoServiceProvider _rngProvider = new RNGCryptoServiceProvider();
         
        public int Next()
        {
            byte[] randomBuffer = new byte[4];
            _rngProvider.GetBytes(randomBuffer);
            int result = BitConverter.ToInt32(randomBuffer, 0);

            return result;
        }
 
        public int Next(int maximumValue)
        {
            // Do not use Next() % maximumValue because the distribution is not OK
            return Next(0, maximumValue);
        }
 
        public int Next(int minimumValue, int maximumValue)
        {
            int seed = Next();
 
            //  Generate uniformly distributed random integers within a given range.
            return new Random(seed).Next(minimumValue, maximumValue);
        }
    }
}
