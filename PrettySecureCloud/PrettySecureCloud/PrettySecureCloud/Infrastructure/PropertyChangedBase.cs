using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PrettySecureCloud.Infrastructure
{
	/// <summary>
	/// Base implementation of the <see cref="INotifyPropertyChanged"/> interface
	/// </summary>
	public abstract class PropertyChangedBase : INotifyPropertyChanged
	{
		/// <summary>
		///     Notifies the GUI that a property has changed
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		///     Event invocator for <see cref="PropertyChanged"/>
		/// </summary>
		/// <param name="propertyName"></param>
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
