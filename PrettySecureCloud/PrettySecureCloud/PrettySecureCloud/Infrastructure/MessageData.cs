using Xamarin.Forms;

namespace PrettySecureCloud.Infrastructure
{
	/// <summary>
	/// The Data for a Message (Pop-Up)
	/// </summary>
	public class MessageData
	{
		/// <summary>
		/// Const value for the <see cref="MessagingCenter"/>
		/// </summary>
		public const string DisplayAlert = "DisplayMessage";

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageData" /> class.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <param name="cancel"></param>
		public MessageData(string title, string content, string cancel)
		{
			Title = title;
			Content = content;
			Cancel = cancel;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageData" /> class.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <param name="accept"></param>
		/// <param name="cancel"></param>
		public MessageData(string title, string content, string accept, string cancel)
		{
			Title = title;
			Content = content;
			Accept = accept;
			Cancel = cancel;
		}

		/// <summary>
		/// The Tiltle
		/// </summary>
		public string Title { get; }
		/// <summary>
		/// The Contnent
		/// </summary>
		public string Content { get; }
		/// <summary>
		/// Ok button text
		/// </summary>
		public string Accept { get; }
		/// <summary>
		/// Cancel button text
		/// </summary>
		public string Cancel { get; }
	}
}
