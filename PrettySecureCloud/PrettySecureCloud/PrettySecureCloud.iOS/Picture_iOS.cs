using System;
using Foundation;
using PrettySecureCloud.FileChooser;
using PrettySecureCloud.iOS.FlexChartImageSave.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Picture_IOs))]
namespace PrettySecureCloud.iOS
{ 
namespace FlexChartImageSave.iOS
	{
		public class Picture_IOs : IPicture
		{
			public bool SavePictureToDisk(string filename, byte[] imageData)
			{
				var chartImage = new UIImage(NSData.FromArray(imageData));
				chartImage.SaveToPhotosAlbum((image, error) =>
				{
				//you can retrieve the saved UI Image as well if needed using
				//var i = image as UIImage;
				if (error != null)
					{
						Console.WriteLine(error.ToString());
					}
				});

				return true;
			}
		}
	}
}
