using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SettingsTest.Helpers;

namespace SettingsTest.Droid
{
    [Activity(Label = "SettingsTest.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
            var entry = FindViewById<EditText>(Resource.Id.random_int);
            var label = FindViewById<TextView>(Resource.Id.noramal_setting);
            var firstRun = FindViewById<TextView>(Resource.Id.first_run);

            entry.Text = Settings.RandomInt.ToString();
            label.Text = Settings.GeneralSetting;

            if (Settings.FirstRun)
            {
                firstRun.Text = "Welcome to this app";
                Settings.FirstRun = false;
            }
            else
            {
                firstRun.Text = "You already ran this!";
            }

            button.Click += delegate
            {
                    Settings.RandomInt = int.Parse(entry.Text.Trim());
                    Settings.GeneralSetting = "We saved " + Settings.RandomInt + " times";
                    label.Text = Settings.GeneralSetting;

            };

            FindViewById<Button>(Resource.Id.settings).Click += (sender, e) => 
                {
                    StartActivity(typeof(PrefsActivity));
                };
        }
    }
}


