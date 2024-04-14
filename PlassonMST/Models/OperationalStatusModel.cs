namespace PlassonMST.Models
{
	public class OperationalStatusModel
	{
		//note: nirm: The application was designed with the intention to include status names as strings along with the icons
		//due to time constrains it was decided that this implementation will be pushed to "the next sprint" ;) - a single day is not enough...
		public string Name { get; private set; }
		public string IconResource { get; private set; }

		public override string ToString() => Name;

		public OperationalStatusModel(string name, string iconResource)
		{
			Name = name;
			IconResource = iconResource;
		}
	}
}
