using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace CrossCutting.Binders.TypeConverters
{
    public class EnumerableTypeConverter<T> : TypeConverter where T : struct, IConvertible
    {
        /// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        /// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
        /// <returns>
        /// <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
        /// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
        /// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if(value == null) return base.ConvertFrom(context, culture, value);

            string str = value as string;

            IList<T> list = new List<T>();

            if (!string.IsNullOrWhiteSpace(str))
            {
                string[] enumsStr = str.Split(',');

                foreach (string enumStr in enumsStr)
                {
                    list.Add((T)Enum.Parse(typeof(T), enumStr));
                }
            }

            return list;
        }

    }
}
