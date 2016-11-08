using System.ComponentModel;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices.EditService
{
	/// <summary>
	/// Class for editing the Service
	/// </summary>
	public class EditServiceViewModel : ViewModelBase
	{
		private readonly ICloudService _clickedService;

		/// <summary>
		/// The user defined name
		/// </summary>
		public string CustomName { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EditServiceViewModel" /> class.
		/// </summary>
		/// <param name="clickedService"></param>
		public EditServiceViewModel(ICloudService clickedService)
		{
			_clickedService = clickedService;
			CustomName = _clickedService.CustomName;
			SaveChangesCommand = new Command(SaveChanges);
		}

		/// <summary>
		/// Command for <see cref="SaveChanges"/>
		/// </summary>
		public Command SaveChangesCommand { get; }

		private void SaveChanges()
		{
			Workers++;

			Service.UpdateServiceCompleted += UpdateServiceCompleted;

			Service.UpdateServiceAsync(_clickedService.Model.Id, CustomName);
		}

		private void UpdateServiceCompleted(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
		{
			Service.UpdateServiceCompleted -= UpdateServiceCompleted;

			if (HandleException(this, asyncCompletedEventArgs))
			{
				_clickedService.CustomName = CustomName;

				DisplayAlert(this, new MessageData("Geändert", "Die Werte wurden erfolgreich geändert", "Ok"));
			}

			Workers--;
		}
	}
}
