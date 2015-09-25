using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MVVMFramework.ViewModel;
using System.ComponentModel;
using MVVMFramework.Services;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;

namespace MVVMFramework.Droid
{
    [Activity(Label = "MVVMFramework.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public LoginViewModel VM
        {
            get { return App.Locator.Login; }
        }
        public EditText UsernameText{get;set;}
        public EditText PasswordText{get;set;}
        public TextView ComboText{get;set;}
        public ProgressBar ProgressBar{get;set;}
        public Button ButtonGet{get;set;}

        public Binding unBind, passBind, combBind, busyBind, updatedBind;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

           
            // Get our button from the layout resource,
            // and attach an event to it
            ButtonGet = FindViewById<Button>(Resource.Id.myButton);
            UsernameText = FindViewById<EditText>(Resource.Id.username);
            PasswordText = FindViewById<EditText>(Resource.Id.password);
            ComboText = FindViewById<TextView>(Resource.Id.combo);
            ProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);


            //So things don't link out. 
            //Only needed if linking all
            //UsernameText.TextChanged += (sender, e) => {};
            //PasswordText.TextChanged += (sender, e) => {};
            //ButtonGet.Click += (sender, e) => {};

            ButtonGet.SetCommand("Click", VM.GetPeopleCommand);

            unBind = this.SetBinding(() => VM.Username, 
                () => UsernameText.Text,
                BindingMode.TwoWay);

            passBind = this.SetBinding(() => VM.Password, 
                () => PasswordText.Text,
                BindingMode.TwoWay);

            combBind = this.SetBinding(() => VM.ComboDisplay,
                () => ComboText.Text,
                BindingMode.OneWay);

            busyBind = this.SetBinding(() => VM.IsBusy).WhenSourceChanges(() =>
                {
                    ButtonGet.Enabled = !VM.IsBusy;
                    if(VM.IsBusy)
                        ProgressBar.Visibility = ViewStates.Visible;
                    else
                        ProgressBar.Visibility = ViewStates.Invisible;
                });

            updatedBind = this.SetBinding(() => VM.People)
                .WhenSourceChanges(() =>
                    {
                        RunOnUiThread(() =>Toast.MakeText(this, "Count: " + VM.People.Count, ToastLength.Short).Show()); 

                    });
        }
            
    }
}


