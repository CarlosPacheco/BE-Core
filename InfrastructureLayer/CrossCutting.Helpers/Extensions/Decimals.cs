using System;

namespace CrossCutting.Helpers.Extensions
{
    /// <summary>
    /// Decimals data type Extension methods class.
    /// </summary>
    public static class Decimals
    {
        public static decimal Truncate(this decimal value, int precision)
        {
            int step = (int)Math.Pow(10, precision);
            return Math.Truncate(step * value) / step;
        }

        /// <summary>
        /// Get Number decimals
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetDecimals(this decimal value)
        {
            return BitConverter.GetBytes(decimal.GetBits(value)[3])[2];
        }
    }
}
