namespace PlassonMST.Models
{
	public class MachineModel(string name, string description, IEnumerable<string> notes, OperationalStatusModel status)
	{
		public string Name { get; set; } = name;
		public string Description { get; set; } = description;
		public IEnumerable<string> Notes { get; set; } = notes;
		public OperationalStatusModel Status { get; set; } = status;

		public void Update(MachineModel newData)
		{
			Name = newData.Name;
			Description = newData.Description;
			Status = newData.Status;
			Notes = newData.Notes;
			NotifyModelChanged?.Invoke(this, EventArgs.Empty);
		}

		public bool HasStatus(OperationalStatusModel status) => Status == status;

		public event EventHandler NotifyModelChanged;
	}
}
