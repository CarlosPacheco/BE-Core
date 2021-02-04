using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CrossCutting.Helpers.Extensions
{
    /// <summary>
    /// String data type Extension methods class.
    /// </summary>
	public static class Strings
    {
        private static readonly Regex MultipleSpaces = new Regex(@" {2,}", RegexOptions.Compiled);

        #region Parse Numerics

        /// <summary>
        /// Converts the string representation of a number to it's 128-bit signed floating-point equivalent.
        /// </summary>
        /// <param name="str">Target string.</param>
        /// <returns>The <see cref="System.Decimal"/> value of the string.</returns>
        public static decimal ToDecimal(this string str)
        {
            return Convert.ToDecimal(str, System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the string representation of a number to it's 64-bit signed floating-point equivalent.
        /// </summary>
        /// <param name="str">Target string.</param>
        /// <returns>The <see cref="System.Double"/> value of the string.</returns>
        public static double ToDouble(this string str)
        {
            return Convert.ToDouble(str, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static int ToInt(this string str)
        {
            return Convert.ToInt32(str);
        }

        public static bool ToBoolean(this string str)
        {
            return Convert.ToBoolean(str);
        }

        public static long ToLong(this string str)
        {
            return Convert.ToInt64(str);
        }

        public static Guid ToGuid(this string str)
        {
            return Guid.Parse(str);
        }

        public static DateTime ToDateTime(this string str)
        {
            return Convert.ToDateTime(str);
        }

        public static DateTime? ToNullableDateTime(this string str)
        {
            return DateTime.TryParse(str, out DateTime date) ? (DateTime?)date : null;
        }

        public static int? ToNullableInt32(this string str)
        {
            int? result;
            if (string.IsNullOrWhiteSpace(str))
            {
                result = null;
            }
            else
            {
                result = int.Parse(str);
            }
            return result;
        }

        /// <summary>
        /// Converts the string representation of a number to it's 64-bit signed integer equivalent.
        /// </summary>
        /// <param name="str">Target string.</param>
        /// <returns>The Int64 value of the string. If string is empty, null, white space or the cast fails null is returned.</returns>
        public static long? ToNullableInt64(this string str)
        {
            long? result;
            if (string.IsNullOrWhiteSpace(str))
            {
                result = null;
            }
            else
            {
                result = long.Parse(str);
            }
            return result;
        }

        /// <summary>
        /// Tries to cast <see cref="string"/> to <see cref="decimal"/>.
        /// </summary>
        /// <param name="str">Target string.</param>
        /// <returns>The Decimal value of the string. If string is empty, null, white space or the cast fails null is returned.</returns>
        public static decimal? TryParseToNullableDecimal(this string str)
        {
            decimal? result;

            if (string.IsNullOrWhiteSpace(str))
            {
                result = null;
            }
            else
            {
                try
                {
                    result = decimal.Parse(str);
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }

        /// <summary>
        /// Tries to cast <see cref="string"/> to <see cref="double"/>.
        /// </summary>
        /// <param name="str">Target string.</param>
        /// <param name="value">Output variable.</param>
        /// <returns>The <see cref="double"/> value of the string. If string is empty, null, white space or the cast fails null is returned.</returns>
        public static bool TryParseToNullableDouble(this string str, out double? value)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                value = null;
                return false;
            }
            else
            {
                try
                {
                    value = double.Parse(str);
                }
                catch (Exception)
                {
                    value = null;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tries to cast <see cref="string"/> to <see cref="double"/>.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <param name="culture">An object that supplies culture-specific formatting information about the source string.</param>
        /// <param name="value">The output variable.</param>
        /// <returns>The <see cref="double"/> value of the string. If string is empty, null, white space or the cast fails null is returned.</returns>
        public static bool TryParseToNullableDouble(this string str, IFormatProvider culture, out double? value)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                value = null;
                return false;
            }
            else
            {
                try
                {
                    value = double.Parse(str, culture);
                }
                catch (Exception)
                {
                    value = null;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tries to cast <see cref="string"/> to <see cref="int"/>.
        /// </summary>
        /// <param name="str">Target string.</param>
        /// <returns>The Int32 value of the string. If string is empty, null, white space or the cast fails null is returned.</returns>
        public static int? TryParseToNullableInt32(this string str)
        {
            int? result;

            if (string.IsNullOrWhiteSpace(str))
            {
                result = null;
            }
            else
            {
                try
                {
                    result = int.Parse(str);
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }

        /// <summary>
        /// Tries to cast <see cref="string"/> to <see cref="long"/>.
        /// </summary>
        /// <param name="str">Target string.</param>
        /// <returns>The Int64 value of the string. If string is empty, null, white space or the cast fails null is returned.</returns>
        public static long? TryParseToNullableInt64(this string str)
        {
            long? result;

            if (string.IsNullOrWhiteSpace(str))
            {
                result = null;
            }
            else
            {
                try
                {
                    result = long.Parse(str);
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }

        #endregion

        #region String Manipulation

        public static string RemoveMultipleSpaces(this string value)
        {
            return MultipleSpaces.Replace(value, " ");
        }

        public static string GetFileName(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : Path.GetFileName(value);
        }

        public static string CombineAsURL(this string baseURL, params string[] segments)
        {
            {
                return string.Join("/", new[] { baseURL.TrimEnd('/') }
                    .Concat(segments.Select(s => s.Trim('/'))));
            }
        }

        #endregion

        #region String Format

        public static string FormatText(this string text, params object[] args)
        {
            return string.Format(text, args);
        }

        #endregion

        #region String Checks

        public static string IfEmptyOrWhiteSpace(this string str, string value)
        {
            return string.IsNullOrWhiteSpace(str) ? value : str;
        }

        public static bool IsBase64String(this string s)
        {
            s = s.Trim();
            return s.Length % 4 == 0 && Regex.IsMatch(s, "^[a-zA-Z0-9\\+/]*={0,3}$", RegexOptions.None);
        }

        /// <summary>
        /// Indicates whether the specified string is null or an System.String.Empty string.
        /// </summary>
        /// <param name="s">The string to test.</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Indicates whether the specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="s">The string to test.</param>
        /// <returns>true if the value parameter is null, empty, or consists only of white-space characters; otherwise, false.</returns>
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        #endregion

        #region Conditional Values Helpers

        public static string UseNullIfEmptyOrWhiteSpace(this string str)
        {
            return str.IfEmptyOrWhiteSpace(null);
        }

        /// <summary>
        /// If the <see cref="System.String"/> is null, emtpy or white space only the special <see cref="DBNull"/> value is returned. 
        /// </summary>
        /// <param name="value">The target string.</param>
        /// <returns><see cref="DBNull"/> if the target string is null, empty or all white spaces else the current value is returned.</returns>
        public static object UseDBNullIfEmptyOrNull(this string value)
        {
            object result;

            if (string.IsNullOrWhiteSpace(value))
            {
                result = DBNull.Value;
            }
            else
            {
                result = value;
            }

            return result;
        }

        #endregion
    }
}
