using System.Globalization;
using bytePassion.Lib.Types.SemanticTypes;
using bytePassion.Lib.WpfLib.ConverterBase;


namespace bytePassion.Lib.WpfLib.Converter
{
	public class XCoordToDoubleConverter : GenericValueConverter<XCoord, double>
    {
        protected override double Convert    (XCoord value, CultureInfo culture) => value?.Value ?? 0;
		protected override XCoord ConvertBack(double value, CultureInfo culture) => new XCoord(value);
    }
}
