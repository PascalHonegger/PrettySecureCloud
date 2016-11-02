using System;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Java.IO;
using PrettySecureCloud.Droid;
using PrettySecureCloud.FileChooser;
using Xamarin.Forms;
using Uri = Android.Net.Uri;

[assembly: Dependency(typeof(Picture_Droid))]

namespace PrettySecureCloud.Droid
{
	public class Picture_Droid : IPicture
	{
		private const int StorageRequestCode = 0;

		public bool SavePictureToDisk(string filename, byte[] imageData)
		{
			if (ContextCompat.CheckSelfPermission(Forms.Context, Manifest.Permission.WriteExternalStorage) == Permission.Denied)
			{
				ActivityCompat.RequestPermissions((Activity) Forms.Context,
					new[] {Manifest.Permission.WriteExternalStorage}
					, StorageRequestCode);

				return false;
			}
			try
			{
				SavePictureToDiskWithoutCheckingPermissions(filename, imageData);

				return true;
			}
			catch (Exception e)
			{
				System.Console.Error.WriteLine(e.Message);
				return false;
			}
		}

		private static void SavePictureToDiskWithoutCheckingPermissions(string filename, byte[] imageData)
		{
			var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
			var pictures = dir.AbsolutePath;
			var filePath = System.IO.Path.Combine(pictures, filename);

			System.IO.File.WriteAllBytes(filePath, imageData);
			//mediascan adds the saved image into the gallery
			var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			mediaScanIntent.SetData(Uri.FromFile(new File(filePath)));
			Forms.Context.SendBroadcast(mediaScanIntent);
		}
	}
}