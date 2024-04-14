using PlassonMST.Infra;
using PlassonMST.Models;
using System.Collections.ObjectModel;

namespace PlassonMST.ViewModels
{
	public class MachineViewModel : BaseViewModel
	{
		public MachineModel? Machine => Model as MachineModel;

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
					NameChanged();
				}
			}
		}

		private string m_description = default!;
		public string Description
		{
			get { return m_description; }
			set
			{
				if (m_description != value)
				{
					m_description = value;
					OnPropertyChanged();
				}
			}
		}

		private ObservableCollection<string> m_notes = [];
		public ObservableCollection<string> Notes
		{
			get { return m_notes; }
			set
			{
				if (m_notes != value)
				{
					m_notes = value;
					OnPropertyChanged();
				}
			}
		}

		private OperationalStatusViewModel m_status = new OperationalStatusViewModel();
		public OperationalStatusViewModel Status
		{
			get { return m_status; }
			set
			{
				if (m_status != value)
				{
					m_status = value;
					OnPropertyChanged();
				}
			}
		}

		protected override void ModelChanging()
		{
			base.ModelChanging();
			if (Machine != null)
			{
				Machine.NotifyModelChanged -= machine_NotifyModelChanged;
			}
		}

		protected override void ModelChanged()
		{
			base.ModelChanged();
			if (Machine != null)
			{
				Machine.NotifyModelChanged += machine_NotifyModelChanged;
				updateFromModel();
			}
		}

		private void updateFromModel()
		{
			Name = Machine?.Name ?? string.Empty;
			Description = Machine?.Description ?? string.Empty;
			Notes.Clear();
			if (Machine?.Notes is not null)
			{
				Notes.AddRange(Machine.Notes);
			}
			Status.Model = Machine?.Status!;
		}

		private void machine_NotifyModelChanged(object? sender, EventArgs e)
		{
			updateFromModel();
		}

		protected virtual void NameChanged() { }

		public bool HasStatus(OperationalStatusModel filterStatus) => Machine?.HasStatus(filterStatus) ?? false;
	}
}