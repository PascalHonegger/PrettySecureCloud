using System;
using Android.Content.PM;

namespace PrettySecureCloud.Droid
{
	public class PermissionCallbackEventArgs : EventArgs
	{
		public Permission Result;
		public int RequestCode;
		public string Permission;

		public PermissionCallbackEventArgs(int requestCode, string permission, Permission result)
		{
			Result = result;
			RequestCode = requestCode;
			Permission = permission;
		}
	}
}