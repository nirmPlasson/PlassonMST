using PlassonMST.Infra;
using PlassonMST.Managers;
using PlassonMST.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace PlassonMST.ViewModels
{
	public class PlassonMSTViewModel : BaseViewModel
	{
		public static ObservableCollection<OperationalStatusModel> SupportedStatuses => StatusManager.Instance.SupportedStatuses;

		private ObservableCollection<MachineViewModel> m_machines = [];
		public ObservableCollection<MachineViewModel> Machines
		{
			get { return m_machines; }
			set
			{
				if (m_machines != value)
				{
					m_machines = value;
					OnPropertyChanged();
				}
			}
		}

		private MachineViewModel m_selectedMachine = default!;
		public MachineViewModel SelectedMachine
		{
			get { return m_selectedMachine; }
			set
			{
				if (m_selectedMachine != value)
				{
					bool proceedWithTheChange = true;
					if (m_selectedMachine != null && EditableMachine.IsDirty)
					{
						MessageBoxResult userDecision = MessageBox.Show($"There are unsaved machine changes.{System.Environment.NewLine}Do you want to discard the changes?", "Unsaved changes detected", MessageBoxButton.YesNo, MessageBoxImage.Stop);
						if (userDecision != MessageBoxResult.Yes)
						{
							proceedWithTheChange = false;
						}
					}

					MachineViewModel? oldMachine = m_selectedMachine;
					m_selectedMachine = value;
					if (proceedWithTheChange)
					{
						EditableMachine.Model = m_selectedMachine?.Model!;
						OnPropertyChanged();
					}
					else
					{
						Dispatcher.CurrentDispatcher.BeginInvoke(
							new Action(() =>
							{
								m_selectedMachine = oldMachine!; //without first changing and than restoring the original machine the UI won't go back to the original selection - go figure...
								OnPropertyChanged(nameof(SelectedMachine));
							}),
							DispatcherPriority.ContextIdle,
					null);
					}
				}
			}
		}

		private OperationalStatusModel m_filterStatus = default!;
		public OperationalStatusModel FilterStatus
		{
			get { return m_filterStatus; }
			set
			{
				if (m_filterStatus != value)
				{
					m_filterStatus = value;
					OnPropertyChanged();
					FilteredMachines.View.Refresh();
				}
			}
		}

		private CollectionViewSource m_filteredMachines = new CollectionViewSource();
		public CollectionViewSource FilteredMachines
		{
			get { return m_filteredMachines; }
			set
			{
				if (m_filteredMachines != value)
				{
					m_filteredMachines = value;
					OnPropertyChanged();
				}
			}
		}

		public EditableMachineViewModel EditableMachine { get; } = new EditableMachineViewModel();

		public RelayCommand ClearFilterCommand { get; set; }
		public RelayCommand StatusManagementCommand { get; set; }

		public PlassonMSTViewModel()
		{
			ClearFilterCommand = new RelayCommand(clearFilter);
			StatusManagementCommand = new RelayCommand(manageStatuses);
			FilteredMachines.Source = Machines;
			FilteredMachines.Filter += filter;
			foreach (MachineModel machineModel in MachinesManager.Instance.Machines)
			{
				Machines.Add(new MachineViewModel() { Model = machineModel });
			}
		}

		private void clearFilter() => FilterStatus = null;
		private void manageStatuses()
		{

		}

		private void filter(object sender, FilterEventArgs e)
		{
			if (FilterStatus == null)
			{
				e.Accepted = true;
			}
			else if (e.Item is MachineViewModel machine)
			{
				e.Accepted = machine.HasStatus(FilterStatus);
			}
			else
			{
				e.Accepted = false;
			}
		}
	}
}
