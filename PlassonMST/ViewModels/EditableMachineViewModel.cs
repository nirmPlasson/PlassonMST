using PlassonMST.Infra;
using PlassonMST.Managers;
using PlassonMST.Models;
using System.Collections.ObjectModel;

namespace PlassonMST.ViewModels
{
	public class EditableMachineViewModel : MachineViewModel
	{
		private string m_editableNote = default!;
		public string EditableNote
		{
			get { return m_editableNote; }
			set
			{
				if (m_editableNote != value)
				{
					m_editableNote = value;
					OnPropertyChanged();
				}
			}
		}

		public bool CanApplyChanges => false == string.IsNullOrWhiteSpace(Name);
		public RelayCommand<string> DeleteNoteCommand { get; set; }
		public RelayCommand AddNoteCommand { get; set; }
		public RelayCommand CancelChangesCommand { get; set; }
		public RelayCommand ApplyChangesCommand { get; set; }

		public ObservableCollection<OperationalStatusModel> SupportedStatuses => StatusManager.Instance.SupportedStatuses;

		public bool IsDirty
		{
			get
			{
				bool result = Machine == null || false == Name.Equals(Machine.Name) || false == Description.Equals(Machine.Description) || false == Status.Match(Machine.Status) || Notes.Count != Machine.Notes.Count();
				for (int i = Notes.Count - 1; result && i >= 0; --i)
				{
					result &= Notes[i].Equals(Machine.Notes.ElementAt(i));
				}
				return result;
			}
		}

		public EditableMachineViewModel()
		{
			DeleteNoteCommand = new RelayCommand<string>(deleteNote);
			AddNoteCommand = new RelayCommand(addNote);
			ApplyChangesCommand = new RelayCommand(applyChanges, canApplyChanges);
			CancelChangesCommand = new RelayCommand(cancelChanges);
		}

		private bool canApplyChanges() => CanApplyChanges;

		protected override void ModelChanged()
		{
			base.ModelChanged();
			EditableNote = string.Empty;
		}

		private void deleteNote(string note)
		{
			Notes.Remove(note);
		}

		private void addNote()
		{
			Notes.Add(EditableNote);
			EditableNote = string.Empty;
		}

		private void applyChanges()
		{
			if (CanApplyChanges)
			{
				MachinesManager.Instance.UpdateMachine(Machine, new Models.MachineModel(Name, Description, Notes.ToList(), Status?.StatusModel!));
			}
		}

		private void cancelChanges() => ModelChanged();

		protected override void NameChanged()
		{
			base.NameChanged();
			ApplyChangesCommand.RaiseCanExecuteChanged();
		}
	}
}
