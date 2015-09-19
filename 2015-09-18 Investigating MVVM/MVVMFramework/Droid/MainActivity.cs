using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MVVMFramework.ViewModels;
using System.ComponentModel;

namespace MVVMFramework.Droid
{
    [Activity(Label = "MVVMFramework.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        LoginViewModel viewModel;
        EditText username, password;
        TextView combo;
        ProgressBar progressBar;
        Button button;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            viewModel = new LoginViewModel();
            // Get our button from the layout resource,
            // and attach an event to it
            button = FindViewById<Button>(Resource.Id.myButton);

            username = FindViewById<EditText>(Resource.Id.username);
            password = FindViewById<EditText>(Resource.Id.password);

            combo = FindViewById<TextView>(Resource.Id.combo);

            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            username.TextChanged += (sender, e) => 
                {
                    viewModel.Username = username.Text;
                };

            password.TextChanged += (sender, e) => 
                {
                    viewModel.Password = password.Text;
                };


            username.Text = viewModel.Username;
            password.Text = viewModel.Password;
            combo.Text = viewModel.ComboDisplay;
            progressBar.Visibility = ViewStates.Gone;

           
            button.Click += delegate
            {
               viewModel.GetPeopleCommand.Execute(null);
            };
        }

        protected override void OnStart()
        {
            base.OnStart();
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            viewModel.People.CollectionChanged += ViewModel_People_CollectionChanged;
               
        }



        protected override void OnStop()
        {
            base.OnStop();
            viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            viewModel.People.CollectionChanged -= ViewModel_People_CollectionChanged;
        }

        void ViewModel_People_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RunOnUiThread(() =>Toast.MakeText(this, "Count: " + viewModel.People.Count, ToastLength.Short).Show());
            
        }

        void ViewModel_PropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            RunOnUiThread(() =>
                {
                    switch(e.PropertyName)
                    {
                        
                        case LoginViewModel.ComboDisplayPropertyName:
                            combo.Text = viewModel.ComboDisplay;
                            break;
                        case BaseViewModel.IsBusyPropertyName:

                            if(viewModel.IsBusy)
                            {
                                progressBar.Visibility = ViewStates.Visible;
                                progressBar.Indeterminate = true;
                                button.Enabled = false;

                            }
                            else
                            {
                                progressBar.Visibility = ViewStates.Gone;
                                progressBar.Indeterminate = false;
                                button.Enabled = true;
                            }

                            break;
                    }
                });
        }
    }
}


