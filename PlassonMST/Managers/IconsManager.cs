using System.Windows;
using System.Windows.Media;

namespace PlassonMST.Managers
{
	public class IconsManager
	{
		private static IconsManager? m_iconsManager;
		public static IconsManager Instance { get { return m_iconsManager ??= new IconsManager(); } }

		public List<string> AvailableIcons { get; } = [];
		private IconsManager()
		{
			if (!(Application.LoadComponent(new Uri("Resources/FeatherIcons.xaml", UriKind.Relative)) is ResourceDictionary rd))
			{
				return;
			}
			foreach (var key in rd.Keys)
			{
				if (rd[key] is DrawingImage)
				{
					AvailableIcons.Add((string)key);
				}
			}
		}
	}
}
