using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamChat.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();            

			ToolbarDone.Clicked += ToolbarDone_Clicked;
		}

		private async void ToolbarDone_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			ToolbarDone.Clicked -= ToolbarDone_Clicked;
		}
	}
}