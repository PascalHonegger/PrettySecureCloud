using System;
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

		public event EventHandler<PermissionCallbackEventArgs> RequestPermissionCallback;

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			for (int i = 0; i < permissions.Length; i++)
			{
				OnRequestPermissionCallback(requestCode, permissions[i], grantResults[i]);
			}

			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		protected virtual void OnRequestPermissionCallback(int requestCode, string permission, Permission result)
		{
			RequestPermissionCallback?.Invoke(this, new PermissionCallbackEventArgs(requestCode, permission, result));
		}
	}
}