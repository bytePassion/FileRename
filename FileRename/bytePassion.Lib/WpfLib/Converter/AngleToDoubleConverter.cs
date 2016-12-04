using System.Globalization;
using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.Types.SemanticTypes;
using bytePassion.Lib.WpfLib.ConverterBase;


namespace bytePassion.Lib.WpfLib.Converter
{
	public class AngleToDoubleConverter : GenericValueConverter<Angle, double>
    {
	    protected override double Convert    (Angle  angle, CultureInfo culture) => angle.Value;
	    protected override Angle  ConvertBack(double value, CultureInfo culture) => new Angle(new Degree(value));
    }
}
