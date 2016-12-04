using System.Globalization;
using bytePassion.Lib.Types.SemanticTypes;
using bytePassion.Lib.WpfLib.ConverterBase;

namespace bytePassion.Lib.WpfLib.Converter
{
	public class XCoordAndOffsetToDoubleConverter : GenericParameterizedValueConverter<XCoord, double, string>
	{
		protected override double Convert(XCoord value, string offset, CultureInfo culture)
		{			
			return (value?.Value ?? 0) + double.Parse(offset);
		}

		protected override XCoord ConvertBack(double value, string offset, CultureInfo culture)
		{
			return new XCoord(value -  double.Parse(offset));
		}
	}
}