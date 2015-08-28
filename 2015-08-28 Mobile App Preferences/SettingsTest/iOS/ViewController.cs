using System;
        
using UIKit;

namespace SettingsTest.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 1;

        public ViewController(IntPtr handle)
            : base(handle)
        {        
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Code to start the Xamarin Test Cloud Agent
          

            // Perform any additional setup after loading the view, typically from a nib.
            Button.AccessibilityIdentifier = "myButton";

            count = SettingsTest.Helpers.Settings.RandomInt;
            var title = string.Format("{0} clicks!", SettingsTest.Helpers.Settings.RandomInt);
            Button.SetTitle(title, UIControlState.Normal); 

            Button.TouchUpInside += delegate
            {
                count = SettingsTest.Helpers.Settings.RandomInt++;
                var title2 = string.Format("{0} clicks!", count);
                Button.SetTitle(title2, UIControlState.Normal);
            };

            if (SettingsTest.Helpers.Settings.FirstRun)
            {
                Label.Text = "First run!!!";
                SettingsTest.Helpers.Settings.FirstRun = false;
            }
            else
            {
                Label.Text = "You already ran this!";
            }
        }

        public override void DidReceiveMemoryWarning()
        {        
            base.DidReceiveMemoryWarning();        
            // Release any cached data, images, etc that aren't in use.        
        }
    }
}
