using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace XamChat.Helpers
{
    public static class Settings
    {
        public static string UserName
        {
            get => Preferences.Get(nameof(UserName), string.Empty);
            set => Preferences.Set(nameof(UserName), value);
        }

        public static string Group
        {
            get => Preferences.Get(nameof(Group), string.Empty);
            set => Preferences.Set(nameof(Group), value);
        }
    }
}
