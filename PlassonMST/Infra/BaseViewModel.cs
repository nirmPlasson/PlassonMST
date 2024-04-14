using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PlassonMST.Infra
{
	public class BaseViewModel : INotifyPropertyChanged, IDisposable
	{
		private object m_model = default!;
		public object Model
		{
			get { return m_model; }
			set
			{
				if (m_model != value)
				{
					ModelChanging();
					m_model = value;
					ModelChanged();
					OnPropertyChanged();
				}
			}
		}

		protected virtual void ModelChanging() { }
		protected virtual void ModelChanged() { }

		public event PropertyChangedEventHandler? PropertyChanged;

		public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChangedEventHandler? handler = PropertyChanged;
			if (handler != null)
			{
				PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
				handler(this, args);
			}
		}

		public virtual void Dispose() { /* nothing to do in base class */ }
	}
}
