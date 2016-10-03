namespace PrettySecureCloud.Infrastructure
{
	public class MessageData
	{
		public const string DisplayAlert = "DisplayMessage";

		public MessageData(string title, string content, string cancel)
		{
			Title = title;
			Content = content;
			Cancel = cancel;
		}

		public MessageData(string title, string content, string accept, string cancel)
		{
			Title = title;
			Content = content;
			Accept = accept;
			Cancel = cancel;
		}

		public string Title { get; set; }
		public string Content { get; set; }
		public string Accept { get; set; }
		public string Cancel { get; set; }
	}
}
