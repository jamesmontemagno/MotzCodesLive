using System;
        
using UIKit;
using MVVMFramework.ViewModel;
using GCDiscreetNotification;
using System.ComponentModel;
using GalaSoft.MvvmLight.Helpers;

namespace MVVMFramework.iOS
{
    public partial class ViewController : UIViewController
    {

        public LoginViewModel VM
        {
            get { return AppDelegate.Locator.Login; }
        }
            


        Binding unBind, passBind, combBind, busyBind, updatedBind;

        public ViewController(IntPtr handle)
            : base(handle)
        {        
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


           
            // Perform any additional setup after loading the view, typically from a nib.
            Button.AccessibilityIdentifier = "myButton";
           
            Button.SetCommand("TouchUpInside", VM.GetPeopleCommand);

            unBind = this.SetBinding(() => VM.Username, 
                () => TextUsername.Text,
                BindingMode.TwoWay)
                .UpdateTargetTrigger("EditingChanged");

            passBind = this.SetBinding(() => VM.Password, 
                () => TextPassword.Text,
                BindingMode.TwoWay)
                .UpdateTargetTrigger("EditingChanged");

            combBind = this.SetBinding(() => VM.ComboDisplay,
                () => LabelCombo.Text,
                BindingMode.OneWay);

            busyBind = this.SetBinding(() => VM.IsBusy).WhenSourceChanges(() =>
                {
                    Button.Enabled = !VM.IsBusy;
                    if(VM.IsBusy)
                        ProgressBar.StartAnimating();
                    else
                        ProgressBar.StopAnimating();
                });

            updatedBind = this.SetBinding(() => VM.People)
                .WhenSourceChanges(() =>
                {
                        notificationView = new GCDiscreetNotificationView (
                            text: "There are " + VM.People.Count + " people",
                            activity: false,
                            presentationMode: GCDNPresentationMode.Bottom,
                            view: View
                        );


                        notificationView.ShowAndDismissAfter (1);
                });
        }



       

        GCDiscreetNotificationView notificationView;


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            //cleanup
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
            //cleanup
        }
    }
}
