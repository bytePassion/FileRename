using bytePassion.Lib.WpfLib.ConverterBase;
using System.Globalization;


namespace bytePassion.Lib.WpfLib.Computations
{
	public class BoolInverter : GenericValueConverter<bool, bool>
	{
		protected override bool Convert    (bool value, CultureInfo culture) => !value;
		protected override bool ConvertBack(bool value, CultureInfo culture) => !value;
	}
}
