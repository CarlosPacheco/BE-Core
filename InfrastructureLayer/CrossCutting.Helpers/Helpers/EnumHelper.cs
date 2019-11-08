using System;
using System.Globalization;

namespace CrossCutting.Helpers.Helpers
{
    /// <summary>
    /// Enumerations helper class.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Casts a string value to an Enumeration type object.
        /// </summary>
        /// <typeparam name="TEnum">An Enumeration type.</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <returns>A Enumeration type variable whose value is the one represented by the string passed in parameters.</returns>
        public static TEnum Parse<TEnum>(string value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Parse<TEnum>(value as object);
        }

        /// <summary>
        /// Casts an object (of string or int type) value to an Enumeration type object.
        /// </summary>
        /// <typeparam name="TEnum">An Enumeration type.</typeparam>
        /// <param name="value">An object containing the name or value to convert.</param>
        /// <returns>A Enumeration type variable whose value is the one represented by the object passed in parameters.</returns>
        public static TEnum Parse<TEnum>(object value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            bool parsingFromString = value is string;
            bool parsingFromInteger = value is int || value is short || value is long || value is byte;

            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            if (value == null)
            {
                throw new ArgumentException("value is null");
            }

            if(!parsingFromInteger && !parsingFromString)
            {
                throw new ArgumentException("value is of an unsupported type");
            }

            if (parsingFromString && string.IsNullOrWhiteSpace((string) value))
            {
                throw new ArgumentException("value is null or empty string");
            }
            
            // Check if it's a string with numeric value
            //  (Can't check if defined before checking it's a numeric string)
            if (parsingFromString && long.TryParse((string)value, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out long parsedNumber))
            {
                if (!Enum.IsDefined(typeof(TEnum), (int)parsedNumber))
                {
                    throw new OverflowException("value is outside the range of the underlying type of enumType");
                }

                return (TEnum)Enum.ToObject(typeof(TEnum), parsedNumber);
            }

            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                throw new OverflowException("value is outside the range of the underlying type of enumType");
            }

            // From numeric type, direct cast.
            if (!parsingFromString)
            {
                return (TEnum) value;
            }

            // From string
            return (TEnum)Enum.Parse(typeof(TEnum), (string) value);
        }

        /// <summary>
        /// Tries to cast a string value to an Enumeration type object.
        /// </summary>
        /// <typeparam name="TEnum">An Enumeration type.</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <param name="result">The variable that will hold the conversion value.</param>
        /// <returns>True if the cast was possible, false otherwise.</returns>
        public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            try
            {
                result = Parse<TEnum>(value);                
            }
            catch (Exception)
            {
                result = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to cast an object to an Enumeration type object.
        /// </summary>
        /// <typeparam name="TEnum">An Enumeration type.</typeparam>
        /// <param name="value">An object containing the name or value to convert.</param>
        /// <param name="result">The variable that will hold the conversion value.</param>
        /// <returns>True if the cast was possible, false otherwise.</returns>
        public static bool TryParse<TEnum>(object value, out TEnum result) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            try
            {
                result = Parse<TEnum>(value);
            }
            catch (Exception)
            {
                result = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to cast a string value to an Enumeration type object, if cast fails a specified value is returned.
        /// </summary>
        /// <typeparam name="TEnum">An Enumeration type.</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <param name="defaultValue">The value to return in case of parse fail.</param>
        /// <returns>A Enumeration type variable whose value is the one represented by the string passed in parameters or the specified default value when parse fails.</returns>
        public static TEnum ParseOrDefault<TEnum>(string value, TEnum defaultValue = default) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            if (TryParse(value, out TEnum enumeration))
            {
                return enumeration;
            }

            return Enum.IsDefined(typeof(TEnum), defaultValue) ? defaultValue : default;
        }

        public static object UseDbNullEnum<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            object newValue = Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(TEnum)));
 
            //compare if is 0 (default enum value) with boolean cast because we need to know if is 0 or Not (false or >0 true)
            if (Enum.IsDefined(typeof(TEnum), value) && Convert.ToBoolean(newValue))
            {
                return newValue;
            }

            return DBNull.Value;
        }

        public static object UseDbNullIfUndefinedOrZero<TEnum>(this TEnum value)
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                return DBNull.Value;
            }

            Type enumUnderlyingType = Enum.GetUnderlyingType(typeof(TEnum));
            object defaultTypeValue = enumUnderlyingType.IsValueType ? Activator.CreateInstance(enumUnderlyingType) : null;

            // Compare if Enum value is equal to default underlying type value
            // This will always compare if enum value is 0 (zero) for all numeric underlying types (byte, short, int, ..., you get it!)
            object castedEnumValue = Convert.ChangeType(value, enumUnderlyingType);
            return castedEnumValue.Equals(defaultTypeValue) ? DBNull.Value : castedEnumValue;
        }

        public static bool IsUndefinedOrNullOrZero<TEnum>(this TEnum? value) where TEnum : struct
        {
            return !value.HasValue || value.Value.IsUndefinedOrNullOrZero();
        }

        public static bool IsUndefinedOrNullOrZero<TEnum>(this TEnum value)
        {
            if (value == null || !Enum.IsDefined(typeof(TEnum), value))
            {
                return false;
            }

            Type enumUnderlyingType = Enum.GetUnderlyingType(typeof(TEnum));
            object defaultTypeValue = enumUnderlyingType.IsValueType ? Activator.CreateInstance(enumUnderlyingType) : null;
            // Compare if Enum value is equal to default underlying type value
            // This will always compare if enum value is 0 (zero) for all numeric underlying types (byte, short, int, ..., you get it!)
            object castedEnumValue = Convert.ChangeType(value, enumUnderlyingType);

            return castedEnumValue.Equals(defaultTypeValue);
        }
    }
}
