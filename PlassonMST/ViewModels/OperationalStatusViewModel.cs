using PlassonMST.Infra;
using PlassonMST.Managers;
using PlassonMST.Models;

namespace PlassonMST.ViewModels
{
	public class OperationalStatusViewModel : BaseViewModel
	{
		public OperationalStatusModel? StatusModel => Model as OperationalStatusModel;

		private string m_name = default!;
		public string Name
		{
			get { return m_name; }
			set
			{
				if (m_name != value)
				{
					m_name = value;
					OnPropertyChanged();
				}
			}
		}

		private string m_iconResource = default!;
		public string IconResource
		{
			get { return m_iconResource; }
			set
			{
				if (m_iconResource != value)
				{
					m_iconResource = value;
					OnPropertyChanged();
				}
			}
		}

		protected override void ModelChanged()
		{
			base.ModelChanged();
			if (StatusModel != null)
			{
				Name = StatusModel.Name;
				IconResource = StatusModel.IconResource;
			}
		}

		public bool Match(OperationalStatusModel filterStatus) => Model == filterStatus;

		public static IEnumerable<OperationalStatusViewModel> SupportedStatusesVMs => StatusManager.Instance.SupportedStatuses.Select<OperationalStatusModel, OperationalStatusViewModel>(model => new OperationalStatusViewModel() { Model = model });
	}
}
