using PlassonMST.Models;
using PlassonMST.ViewModels;
using System.Reflection.PortableExecutable;

namespace PlassonMST.Managers
{
	public class MachinesManager
	{
		private static MachinesManager? m_machinesManager;
		public static MachinesManager Instance { get { return m_machinesManager ??= new MachinesManager(); } }

		private List<MachineModel> m_machines = [];
		public List<MachineModel> Machines
		{
			get { return m_machines; }
			set
			{
				if (m_machines != value)
				{
					m_machines = value;
				}
			}
		}

		public MachinesManager()
		{
			//for demo purposes
			//live will use DB
			int max = Math.Min(20, OperationalStatusViewModel.SupportedStatusesVMs.Count());
			//for (int i = 0; i < IconsManager.Instance.AvailableIcons.Count; ++i)
			//{
			//	MachineModel model = new MachineModel("A" + i, "D" + i, new List<string>() { "N" + i, "n" + i }, new OperationalStatusModel(IconsManager.Instance.AvailableIcons[i], IconsManager.Instance.AvailableIcons[i]));
			//	Machines.Add(new MachineViewModel() { Model = model });
			//}
			for (int i = 0; i < 20; ++i)
			{
				Machines.Add(new MachineModel("A" + i, "D" + i, new List<string>() { "N" + i, "n" + i }, StatusManager.Instance.SupportedStatuses.ElementAt(i % max)));
			}
		}

		public void UpdateMachine(MachineModel oldModel, MachineModel newModel) => oldModel.Update(newModel);
	}
}
