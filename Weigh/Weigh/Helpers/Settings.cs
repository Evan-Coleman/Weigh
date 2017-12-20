// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Weigh.Helpers
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

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string FirstUseKey = "first_use_key";
        private static readonly string FirstUseDefault = "yes";

        private const string NameKey = "name_key";
        private static readonly string NameDefault = string.Empty;

        private const string SexKey = "sex_key";
        private static readonly bool SexDefault = false;

        private const string AgeKey = "age_key";
        private static readonly int AgeDefault = 0;

        private const string HeightFeetKey = "height_feet_key";
        private static readonly int HeightFeetDefault = 0;

        private const string HeightInchesKey = "height_inches_key";
        private static readonly int HeightInchesDefault = 0;

        private const string WeightKey = "weight_key";
        private static readonly double WeightDefault = 0.0;

        private const string WeightKey = "weight_key";
        private static readonly bool WeightDefault = false;

        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        public static string FirstUse
        {
            get
            {
                return AppSettings.GetValueOrDefault(FirstUseKey, FirstUseDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FirstUseKey, value);
            }
        }

        public static string Name
        {
            get
            {
                return AppSettings.GetValueOrDefault(NameKey, NameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(NameKey, value);
            }
        }

        public static bool Sex
        {
            get
            {
                return AppSettings.GetValueOrDefault(SexKey, SexDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SexKey, value);
            }
        }

        public static int Age
        {
            get
            {
                return AppSettings.GetValueOrDefault(AgeKey, AgeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AgeKey, value);
            }
        }

        public static int HeightFeet
        {
            get
            {
                return AppSettings.GetValueOrDefault(HeightFeetKey, HeightFeetDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(HeightFeetKey, value);
            }
        }

        public static int HeightInches
        {
            get
            {
                return AppSettings.GetValueOrDefault(HeightInchesKey, HeightInchesDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(HeightInchesKey, value);
            }
        }

        public static double Weight
        {
            get
            {
                return AppSettings.GetValueOrDefault(WeightKey, WeightDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(WeightKey, value);
            }
        }

    }
}
