using System.Globalization;
using bytePassion.Lib.Types.SemanticTypes;
using bytePassion.Lib.WpfLib.ConverterBase;

namespace bytePassion.Lib.WpfLib.Computations
{
	public class GetWidthFromSize : GenericValueConverter<Size, double>
	{
		protected override double Convert (Size value, CultureInfo culture)
		{
			return value.Width.Value;
		}
	}
}