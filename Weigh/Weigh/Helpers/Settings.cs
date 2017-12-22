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

        private const string HeightMajorKey = "height_major_key";
        private static readonly int HeightMajorDefault = 0;

        private const string HeightMinorKey = "height_minor_key";
        private static readonly int HeightMinorDefault = 0;

        private const string WeightKey = "weight_key";
        private static readonly double WeightDefault = 0.0;

        private const string UnitsKey = "units_key";
        private static readonly bool UnitsDefault = true;

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

        public static int HeightMajor
        {
            get
            {
                return AppSettings.GetValueOrDefault(HeightMajorKey, HeightMajorDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(HeightMajorKey, value);
            }
        }

        public static int HeightMinor
        {
            get
            {
                return AppSettings.GetValueOrDefault(HeightMinorKey, HeightMinorDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(HeightMinorKey, value);
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

        public static bool Units
        {
            get
            {
                return AppSettings.GetValueOrDefault(UnitsKey, UnitsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UnitsKey, value);
            }
        }

    }
}
