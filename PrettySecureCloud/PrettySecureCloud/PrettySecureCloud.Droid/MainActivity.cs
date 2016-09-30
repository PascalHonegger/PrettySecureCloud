using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Application(Icon = "@drawable/Icon", Theme = "@android:style/Theme.Holo.Light")]

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
	}
}