using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossCutting.Security.Generators
{
    public static class ShuffleExtensions
    {
        private static readonly Lazy<SecureRNG> RandomSecure = new Lazy<SecureRNG>(() => new SecureRNG());

        public static IEnumerable<T> SecureShuffle<T>(this IEnumerable<T> source)
        {
            T[] sourceArray = source.ToArray();

            for (int counter = 0; counter < sourceArray.Length; counter++)
            {
                int randomIndex = RandomSecure.Value.Next(counter, sourceArray.Length);
                yield return sourceArray[randomIndex];
 
                sourceArray[randomIndex] = sourceArray[counter];
            }
        }
 
        public static string SecureTextShuffle(this string source)
        {
            char[] shuffeldChars = source.SecureShuffle().ToArray();

            return new string(shuffeldChars);
        }
    }
}
