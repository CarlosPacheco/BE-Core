using System;
using System.ComponentModel;
using System.Globalization;

namespace CrossCutting.Binders.TypeConverters
{
    public sealed class UtcDateTimeConverter : DateTimeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            DateTime? val = (DateTime?)base.ConvertFrom(context, culture, value);

            return val?.ToUniversalTime();
        }
    }
}
