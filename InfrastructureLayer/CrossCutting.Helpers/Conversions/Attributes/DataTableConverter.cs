using System;

namespace CrossCutting.Helpers.Conversions.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class DataTableConverter : Attribute
	{
		public bool ShouldConvert
		{
			get;
			set;
		}

		public DataTableConverter()
		{
			ShouldConvert = true;
		}

		public DataTableConverter(bool shouldConvert)
		{
			ShouldConvert = shouldConvert;
		}

	}
}
