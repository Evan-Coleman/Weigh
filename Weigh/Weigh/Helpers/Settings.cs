// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;

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
        private static readonly double HeightMajorDefault = 0;

        private const string HeightMinorKey = "height_minor_key";
        private static readonly int HeightMinorDefault = 0;

        private const string WeightKey = "weight_key";
        private static readonly double WeightDefault = 0.0;

        private const string UnitsKey = "units_key";
        private static readonly bool UnitsDefault = true;

        private const string PickerKey = "picker_key";
        private static readonly string PickerDefault ="Light Exercise";

        private const string LastWeightKey = "last_weight_key";
        private static readonly double LastWeightDefault = 0.0;

        private const string InitialWeightKey = "initial_weight_key";
        private static readonly double InitialWeightDefault = 0.0;

        private const string InitialWeighDateKey = "initial_weigh_date_key";
        private static readonly DateTime InitialWeighDateDefault = DateTime.UtcNow;

        private const string LastWeighDateKey = "last_weigh_date_key";
        private static readonly DateTime LastWeighDateDefault = DateTime.UtcNow;

        private const string GoalWeightKey = "goal_weight_key";
        private static readonly double GoalWeightDefault = 0.0;

        private const string GoalDateKey = "goal_date_key";
        private static readonly DateTime GoalDateDefault = DateTime.UtcNow.AddDays(180);

        private const string WaistSizeKey = "waist_size_key";
        private static readonly double WaistSizeDefault = 0.0;

        #endregion

        public static double GoalWeight
        {
            get
            {
                return AppSettings.GetValueOrDefault(GoalWeightKey, GoalWeightDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(GoalWeightKey, value);
            }
        }

        public static double WaistSize
        {
            get
            {
                return AppSettings.GetValueOrDefault(WaistSizeKey, WaistSizeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(WaistSizeKey, value);
            }
        }

        public static DateTime GoalDate
        {
            get
            {
                return AppSettings.GetValueOrDefault(GoalDateKey, GoalDateDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(GoalDateKey, value);
            }
        }

        public static double InitialWeight
        {
            get
            {
                return AppSettings.GetValueOrDefault(InitialWeightKey, InitialWeightDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(InitialWeightKey, value);
            }
        }

        public static DateTime InitialWeightDate
        {
            get
            {
                return AppSettings.GetValueOrDefault(InitialWeighDateKey, InitialWeighDateDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(InitialWeighDateKey, value);
            }
        }

        public static DateTime LastWeighDate
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastWeighDateKey, LastWeighDateDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastWeighDateKey, value);
            }
        }

        public static double LastWeight
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastWeightKey, LastWeightDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastWeightKey, value);
            }
        }

        public static string PickerSelectedItem
        {
            get
            {
                return AppSettings.GetValueOrDefault(PickerKey, PickerDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(PickerKey, value);
            }
        }

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

        public static double HeightMajor
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
