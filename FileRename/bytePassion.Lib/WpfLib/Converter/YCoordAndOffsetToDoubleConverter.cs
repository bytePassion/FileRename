using System.Globalization;
using bytePassion.Lib.Types.SemanticTypes;
using bytePassion.Lib.WpfLib.ConverterBase;

namespace bytePassion.Lib.WpfLib.Converter
{
	public class YCoordAndOffsetToDoubleConverter : GenericParameterizedValueConverter<YCoord, double, double>
	{
		protected override double Convert     (YCoord value, double offset, CultureInfo culture) => value.Value + offset;
		protected override YCoord ConvertBack (double value, double offset, CultureInfo culture) => new YCoord(value - offset);
	}
}