using System.Globalization;
using bytePassion.Lib.Types.SemanticTypes.Base;
using bytePassion.Lib.WpfLib.ConverterBase;


namespace bytePassion.Lib.WpfLib.Converter
{
	public class SemanticDoubleTypeToDoubleConverter : GenericValueConverter<SemanticType<double>, double>
    {
        protected override double Convert(SemanticType<double> value, CultureInfo culture)
        {
            return value.Value;
        }
    }
}
