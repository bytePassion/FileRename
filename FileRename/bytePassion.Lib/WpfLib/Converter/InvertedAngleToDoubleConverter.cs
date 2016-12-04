using System.Globalization;
using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.Types.SemanticTypes;
using bytePassion.Lib.WpfLib.ConverterBase;


namespace bytePassion.Lib.WpfLib.Converter
{
	public class InvertedAngleToDoubleConverter : GenericValueConverter<Angle, double>
    {
        protected override double Convert(Angle angle, CultureInfo culture)
        {
            return angle.Inverted.Value;
        }

        protected override Angle ConvertBack(double value, CultureInfo culture)
        {
            return new Angle(new Degree(value)).Inverted;
        }
    }
}