using System.Globalization;
using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.WpfLib.ConverterBase;


namespace bytePassion.Lib.WpfLib.Computations
{
	public class DivideAngleBy2 : GenericParameterizedValueConverter<Angle, double, bool>
    {	   
	    protected override double Convert(Angle value, bool invert, CultureInfo culture)
	    {
			return (invert ? -1 : 1) * value.Value / 2.0;
	    }	   
    }
}
