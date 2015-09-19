using System;
        
using UIKit;
using MVVMFramework.ViewModels;
using GCDiscreetNotification;
using System.ComponentModel;

namespace MVVMFramework.iOS
{
    public partial class ViewController : UIViewController
    {
        LoginViewModel viewModel;
        public ViewController(IntPtr handle)
            : base(handle)
        {        
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Code to start the Xamarin Test Cloud Agent
           
            viewModel = new LoginViewModel();
            // Perform any additional setup after loading the view, typically from a nib.
            Button.AccessibilityIdentifier = "myButton";
            Button.TouchUpInside += delegate
            {
                viewModel.GetPeopleCommand.Execute(null);
                      
            };


        }

        partial void PasswordChanged(UITextField sender)
        {
            viewModel.Password = TextPassword.Text;
        }

        partial void UsernameChanged(UITextField sender)
        {
            viewModel.Username = TextUsername.Text;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            viewModel.People.CollectionChanged += ViewModel_People_CollectionChanged;

        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            viewModel.People.CollectionChanged -= ViewModel_People_CollectionChanged;

        }

        void ViewModel_PropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            InvokeOnMainThread(() =>
                {
                    switch(e.PropertyName)
                    {
                        case LoginViewModel.ComboDisplayPropertyName:
                            LabelCombo.Text = viewModel.ComboDisplay;
                            break;
                        case BaseViewModel.IsBusyPropertyName:

                            if(viewModel.IsBusy)
                            {
                                ProgressBar.StartAnimating();
                                Button.Enabled = false;

                            }
                            else
                            {
                                ProgressBar.StopAnimating();
                                Button.Enabled = true;
                            }

                            break;
                    }
                });
        }

        GCDiscreetNotificationView notificationView;
        void ViewModel_People_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            InvokeOnMainThread(async () =>
                {
                     notificationView = new GCDiscreetNotificationView (
                        text: "There are " + viewModel.People.Count + " people",
                            activity: false,
                            presentationMode: GCDNPresentationMode.Bottom,
                            view: View
                        );

                  
                    notificationView.ShowAndDismissAfter (1);
                });
        }

        public override void DidReceiveMemoryWarning()
        {        
            base.DidReceiveMemoryWarning();        
            // Release any cached data, images, etc that aren't in use.        
        }
    }
}
