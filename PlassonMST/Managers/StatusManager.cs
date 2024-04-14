using PlassonMST.Infra;
using PlassonMST.Models;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Windows;
using System.Windows.Data;

namespace PlassonMST.Managers
{
	public class StatusManager : BaseViewModel
	{
		private static StatusManager? _statusManager;
		public static StatusManager Instance { get { return _statusManager ??= new StatusManager(); } }

		private ObservableCollection<OperationalStatusModel> m_supportedStatuses = [];
		public ObservableCollection<OperationalStatusModel> SupportedStatuses
		{
			get { return m_supportedStatuses; }
			set
			{
				if (m_supportedStatuses != value)
				{
					m_supportedStatuses = value;
					OnPropertyChanged();
				}
			}
		}

		private CollectionViewSource m_availableIcons = new CollectionViewSource();
		public CollectionViewSource AvailableIcons
		{
			get { return m_availableIcons; }
			set
			{
				if (m_availableIcons != value)
				{
					m_availableIcons = value;
					OnPropertyChanged();
				}
			}
		}

		private string m_selectedIcon = default!;
		public string SelectedIcon
		{
			get { return m_selectedIcon; }
			set
			{
				if (m_selectedIcon != value)
				{
					m_selectedIcon = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(CanAddStatus));
				}
			}
		}

		private OperationalStatusModel m_selectedStatus = default!;
		public OperationalStatusModel SelectedStatus
		{
			get { return m_selectedStatus; }
			set
			{
				if (m_selectedStatus != value)
				{
					m_selectedStatus = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(CanRemoveStatus));
				}
			}
		}

		private RelayCommand m_addStatusCommand = default!;
		public RelayCommand AddStatusCommand
		{
			get { return m_addStatusCommand; }
			set
			{
				if (m_addStatusCommand != value)
				{
					m_addStatusCommand = value;
					OnPropertyChanged();
				}
			}
		}
		public bool CanAddStatus => SelectedIcon != null && false == SupportedStatuses.Any(status => status.IconResource == SelectedIcon);

		private RelayCommand m_removeStatusCommand = default!;
		public RelayCommand RemoveStatusCommand
		{
			get { return m_removeStatusCommand; }
			set
			{
				if (m_removeStatusCommand != value)
				{
					m_removeStatusCommand = value;
					OnPropertyChanged();
				}
			}
		}
		public bool CanRemoveStatus => SelectedStatus != null;

		private StatusManager()
		{
			AddStatusCommand = new RelayCommand(addStatus);
			RemoveStatusCommand = new RelayCommand(removeStatus);
			//for demo purposes
			//live will use DB
			int max = Math.Min(5, IconsManager.Instance.AvailableIcons.Count);
			for (int i = 0; i < max; ++i)
			{
				SupportedStatuses.Add(new OperationalStatusModel(IconsManager.Instance.AvailableIcons[i], IconsManager.Instance.AvailableIcons[i]));
			}
			AvailableIcons.Source = IconsManager.Instance.AvailableIcons;
			AvailableIcons.Filter += filterAvailableIcons;
		}

		private void removeStatus()
		{
			if (CanRemoveStatus)
			{
				int existingMachinesCount = MachinesManager.Instance.Machines.Count(machine => machine.HasStatus(SelectedStatus));
				if (existingMachinesCount > 0)
				{
					MessageBox.Show($"Found {existingMachinesCount} machines that use this status.{System.Environment.NewLine}Please change all the relevant machines statuses before removing it.", "Status is in use", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
				else
				{
					SupportedStatuses.Remove(SelectedStatus);
					AvailableIcons.View.Refresh();
					OnPropertyChanged(nameof(CanRemoveStatus));
					OnPropertyChanged(nameof(CanAddStatus));
				}
			}
		}

		private void addStatus()
		{
			if (CanAddStatus)
			{
				SupportedStatuses.Add(new OperationalStatusModel(SelectedIcon, SelectedIcon));
				AvailableIcons.View.Refresh();
				OnPropertyChanged(nameof(CanAddStatus));
			}
		}

		private void filterAvailableIcons(object sender, FilterEventArgs e)
		{
			e.Accepted = false == SupportedStatuses.Any(status => status.IconResource.Equals(e.Item));
		}

		public override void Dispose()
		{
			if (AvailableIcons != null)
			{
				AvailableIcons.Filter -= filterAvailableIcons;
			}
		}
	}
}
