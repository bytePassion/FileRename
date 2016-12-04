using bytePassion.FileRename.RenameLogic.Enums;
using bytePassion.Lib.WpfLib.ConverterBase;
using System.Globalization;


namespace bytePassion.FileRename.Converter
{
	public class ReplaceTypeToBoolConverter : GenericParameterizedValueConverter<ReplaceType, bool, ReplaceType>
	{
		protected override bool Convert(ReplaceType replaceType, ReplaceType chosenSearchType, CultureInfo culture)
		{
			return replaceType == chosenSearchType;
		}

		protected override ReplaceType ConvertBack (bool buttonIsChecked, ReplaceType chosenSearchType, CultureInfo culture)
		{
			if (buttonIsChecked) 
				return chosenSearchType;			

			// return dummy because null is not possible
			return ReplaceType.Characters;
		}
	}
}
