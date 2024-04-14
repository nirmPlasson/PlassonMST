using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xaml;

namespace PlassonMST.Converters
{
	public class StaticResourceConverter : MarkupExtension, IValueConverter
	{
		private FrameworkElement? m_target;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var resourceKey = (string)value;
			if (resourceKey != null)
			{
				return m_target?.FindResource(resourceKey) ?? Application.Current.FindResource(resourceKey);
			}
			return Binding.DoNothing;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			var rootObjectProvider = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
			if (rootObjectProvider == null)
				return this;

			m_target = (FrameworkElement)rootObjectProvider.RootObject;
			return this;
		}
	}

}
