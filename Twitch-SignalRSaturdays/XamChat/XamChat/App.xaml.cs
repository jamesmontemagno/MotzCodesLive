using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamChat.Core;
using XamChat.View;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamChat
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<ChatService>();

            MainPage = new HomePage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
