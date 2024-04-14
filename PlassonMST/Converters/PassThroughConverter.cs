using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace PlassonMST.Converters
{
	public class PassThroughConverter : MarkupExtension, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value;

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;

		public override object ProvideValue(IServiceProvider serviceProvider) => this;
	}
}
