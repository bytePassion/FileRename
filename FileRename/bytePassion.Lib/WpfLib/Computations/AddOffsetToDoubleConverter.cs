using System.Globalization;
using bytePassion.Lib.WpfLib.ConverterBase;


namespace bytePassion.Lib.WpfLib.Computations
{
	public class AddOffsetToDoubleConverter : GenericParameterizedValueConverter<double, double, string>
	{
		protected override double Convert(double value, string parameter, CultureInfo culture)
		{
			return value + double.Parse(parameter);
		}		
	}
}
