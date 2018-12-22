using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace XamChat.Helpers
{
    public static class Settings
    {
        public static string ServerIP
        {
            get => Preferences.Get(nameof(ServerIP), DeviceInfo.Platform == DevicePlatform.Android ? "10.0.2.2" : "localhost");
            set => Preferences.Set(nameof(ServerIP), value);
        }

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
