using MvvmHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamChat.Helpers;
using XamChat.View;

namespace XamChat.ViewModel
{
    public class LobbyViewModel : BaseViewModel
    {
        public List<string> Rooms { get; }
        public LobbyViewModel()
        {
            Rooms = new List<string>
            {
                ".NET",
                "ASP.NET",
                "Xamarin"
            };
        }

        public string UserName
        {
            get => Settings.UserName;
            set
            {
                if (value == UserName)
                    return;
                Settings.UserName = value;
                OnPropertyChanged();
            }
        }


        public async Task GoToGroupChat(INavigation navigation, string group)
        {
            if (string.IsNullOrWhiteSpace(group))
                return;

            if (string.IsNullOrWhiteSpace(UserName))
                return;

            Settings.Group = group;
            await navigation.PushAsync(new GroupChatPage());
        }
        
    }
}
