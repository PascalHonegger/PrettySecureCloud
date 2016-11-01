using Android.Content;
using Android.Net;
using Java.IO;
using PrettySecureCloud.Droid;
using PrettySecureCloud.FileChooser;

[assembly: Xamarin.Forms.Dependency(typeof(Picture_Droid))]

namespace PrettySecureCloud.Droid
{
	public class Picture_Droid : IPicture
	{
		public void SavePictureToDisk(string filename, byte[] imageData)
		{
			var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
			var pictures = dir.AbsolutePath;
			var filePath = System.IO.Path.Combine(pictures, filename);
			try
			{
				System.IO.File.WriteAllBytes(filePath, imageData);
				//mediascan adds the saved image into the gallery
				var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
				mediaScanIntent.SetData(Uri.FromFile(new File(filePath)));
				Xamarin.Forms.Forms.Context.SendBroadcast(mediaScanIntent);
			}
			catch (System.Exception e)
			{
				System.Console.WriteLine(e.ToString());
			}

		}
	}
}