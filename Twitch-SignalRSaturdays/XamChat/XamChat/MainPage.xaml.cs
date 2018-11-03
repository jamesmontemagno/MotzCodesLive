using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamChat.ViewModel;

namespace XamChat
{
    public partial class MainPage : ContentPage
    {
        ChatViewModel vm;
        ChatViewModel VM
        {
            get => vm ?? (vm = (ChatViewModel)BindingContext);
        }
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.ConnectCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            VM.DisconnectCommand.Execute(null);
        }
    }
}
