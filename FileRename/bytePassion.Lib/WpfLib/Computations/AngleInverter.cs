using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.WpfLib.ConverterBase;
using System.Globalization;


namespace bytePassion.Lib.WpfLib.Computations
{
    public class AngleInverter : GenericValueConverter<Angle, double>
    {
	    protected override double Convert(Angle value, CultureInfo culture)
	    {
		    return 360.0 - value.PosValue.Value;
	    }	    
    }
}
