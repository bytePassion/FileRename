using System.Globalization;
using bytePassion.Lib.WpfLib.ConverterBase;

namespace bytePassion.Lib.WpfLib.Converter
{
	public class BoolToNullableBoolConverter : GenericValueConverter<bool, bool?>
	{
		protected override bool? Convert(bool value, CultureInfo culture)
		{
			return value;
		}

		protected override bool ConvertBack(bool? value, CultureInfo culture)
		{
			return value.HasValue && value.Value;			
		}
	}
}
