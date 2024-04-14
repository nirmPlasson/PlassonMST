using PlassonMST.Infra;
using PlassonMST.ViewModels;
using PlassonMST.Views;

namespace PlassonMST
{
	public class PlassonMSTShellViewModel : BaseViewModel
	{
		private PlassonMSTViewModel m_plassonMST = new PlassonMSTViewModel();
		public PlassonMSTViewModel PlassonMST
		{
			get { return m_plassonMST; }
			set
			{
				if (m_plassonMST != value)
				{
					m_plassonMST = value;
					OnPropertyChanged();
				}
			}
		}

		private bool m_isStatusManagementModalVisible = default!;
		public bool IsStatusManagementModalVisible
		{
			get { return m_isStatusManagementModalVisible; }
			set
			{
				if (m_isStatusManagementModalVisible != value)
				{
					m_isStatusManagementModalVisible = value;
					OnPropertyChanged();
				}
			}
		}


	}
}
