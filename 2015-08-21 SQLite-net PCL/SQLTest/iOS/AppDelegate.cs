using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using SQLTest.Database;

namespace SQLTest.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

           
            ToDoDatabase.Root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}

