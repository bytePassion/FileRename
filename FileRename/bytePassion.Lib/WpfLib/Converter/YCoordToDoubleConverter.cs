using System.Globalization;
using bytePassion.Lib.Types.SemanticTypes;
using bytePassion.Lib.WpfLib.ConverterBase;


namespace bytePassion.Lib.WpfLib.Converter
{
	public class YCoordToDoubleConverter : GenericValueConverter<YCoord, double>
    {
        protected override double Convert    (YCoord value, CultureInfo culture) => value.Value;
	    protected override YCoord ConvertBack(double value, CultureInfo culture) => new YCoord(value);
    }
}