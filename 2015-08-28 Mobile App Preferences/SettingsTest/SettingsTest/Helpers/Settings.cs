// Helpers/Settings.cs
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;

namespace SettingsTest.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        const string SettingsKey = "settings_key";
        static readonly string SettingsDefault = string.Empty;

        const string FirstRunKey = "first_run_key";
        private const bool FirstRunDefault = true;

        private const string RandomIntKey = "random_int_key";
        private const int RandomIntDefault = 5;


        private const string NotificationsKey = "notifications_key";
        private const bool NotificationsDefault = true;
        #endregion


        public static string GeneralSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
            }
        }

        public static bool Notifications
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(NotificationsKey, NotificationsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(NotificationsKey, value);
            }
        }

        public static bool FirstRun
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(FirstRunKey, FirstRunDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(FirstRunKey, value);
            }
        }

        public static int RandomInt
        {
            get
            {
                return AppSettings.GetValueOrDefault<int>(RandomIntKey, RandomIntDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<int>(RandomIntKey, value);
            }
        }

    }
}