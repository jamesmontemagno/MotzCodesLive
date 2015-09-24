// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MVVMFramework.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton Button { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelCombo { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIActivityIndicatorView ProgressBar { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextUsername { get; set; }

		[Action ("PasswordChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void PasswordChanged (UITextField sender);

		[Action ("UsernameChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UsernameChanged (UITextField sender);

		void ReleaseDesignerOutlets ()
		{
			if (LabelCombo != null) {
				LabelCombo.Dispose ();
				LabelCombo = null;
			}
			if (ProgressBar != null) {
				ProgressBar.Dispose ();
				ProgressBar = null;
			}
			if (TextPassword != null) {
				TextPassword.Dispose ();
				TextPassword = null;
			}
			if (TextUsername != null) {
				TextUsername.Dispose ();
				TextUsername = null;
			}
		}
	}
}
