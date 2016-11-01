using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Permissions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace PrettySecureCloud.Droid
{
	[Activity(Label = "PrettySecureCloud", Icon = "@drawable/icon", MainLauncher = true,
		 ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			Forms.Init(this, bundle);
			LoadApplication(new App());
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}