// Helpers/Settings.cs

using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Weigh.i18n;

namespace Weigh.Helpers
{
    /// <summary>
    ///     This is the Settings static class that can be used in your Core solution or in any
    ///     of your client applications. All settings are laid out the same exact way with getters
    ///     and setters.
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static DateTime BirthDate
        {
            get => AppSettings.GetValueOrDefault(BirthDateKey, BirthDateDefault);
            set => AppSettings.AddOrUpdateValue(BirthDateKey, value);
        }

        public static bool WaistSizeEnabled
        {
            get => AppSettings.GetValueOrDefault(WaistSizeEnabledKey, WaistSizeEnabledDefault);
            set => AppSettings.AddOrUpdateValue(WaistSizeEnabledKey, value);
        }

        public static DateTime MinDate
        {
            get => AppSettings.GetValueOrDefault(MinDateKey, MinDateDefault);
            set => AppSettings.AddOrUpdateValue(MinDateKey, value);
        }

        public static int TimeLeftToGoal
        {
            get => AppSettings.GetValueOrDefault(TimeLeftToGoalKey, TimeLeftToGoalDefault);
            set => AppSettings.AddOrUpdateValue(TimeLeftToGoalKey, value);
        }

        public static double WeightLostToDate
        {
            get => AppSettings.GetValueOrDefault(WeightLostToDateKey, WeightLostToDateDefault);
            set => AppSettings.AddOrUpdateValue(WeightLostToDateKey, value);
        }

        public static double DistanceToGoalWeight
        {
            get => AppSettings.GetValueOrDefault(DistanceToGoalWeightKey, DistanceToGoalWeightDefault);
            set => AppSettings.AddOrUpdateValue(DistanceToGoalWeightKey, value);
        }

        public static double WeightPerWeekToMeetGoal
        {
            get => AppSettings.GetValueOrDefault(WeightPerWeekToMeetGoalKey, WeightPerWeekToMeetGoalDefault);
            set => AppSettings.AddOrUpdateValue(WeightPerWeekToMeetGoalKey, value);
        }

        public static string BMICategory
        {
            get => AppSettings.GetValueOrDefault(BMICategoryKey, BMICategoryDefault);
            set => AppSettings.AddOrUpdateValue(BMICategoryKey, value);
        }

        public static double RecommendedDailyCaloricIntake
        {
            get =>
                AppSettings.GetValueOrDefault(RecommendedDailyCaloricIntakeKey, RecommendedDailyCaloricIntakeDefault);
            set => AppSettings.AddOrUpdateValue(RecommendedDailyCaloricIntakeKey, value);
        }

        public static double BMR
        {
            get => AppSettings.GetValueOrDefault(BMRKey, BMRDefault);
            set => AppSettings.AddOrUpdateValue(BMRKey, value);
        }

        public static double BMI
        {
            get => AppSettings.GetValueOrDefault(BMIKey, BMIDefault);
            set => AppSettings.AddOrUpdateValue(BMIKey, value);
        }

        public static double GoalWeight
        {
            get => AppSettings.GetValueOrDefault(GoalWeightKey, GoalWeightDefault);
            set => AppSettings.AddOrUpdateValue(GoalWeightKey, value);
        }

        public static double WaistSize
        {
            get => AppSettings.GetValueOrDefault(WaistSizeKey, WaistSizeDefault);
            set => AppSettings.AddOrUpdateValue(WaistSizeKey, value);
        }

        public static DateTime GoalDate
        {
            get => AppSettings.GetValueOrDefault(GoalDateKey, GoalDateDefault);
            set => AppSettings.AddOrUpdateValue(GoalDateKey, value);
        }

        public static double InitialWeight
        {
            get => AppSettings.GetValueOrDefault(InitialWeightKey, InitialWeightDefault);
            set => AppSettings.AddOrUpdateValue(InitialWeightKey, value);
        }

        public static DateTime InitialWeightDate
        {
            get => AppSettings.GetValueOrDefault(InitialWeighDateKey, InitialWeighDateDefault);
            set => AppSettings.AddOrUpdateValue(InitialWeighDateKey, value);
        }

        public static DateTime LastWeighDate
        {
            get => AppSettings.GetValueOrDefault(LastWeighDateKey, LastWeighDateDefault);
            set => AppSettings.AddOrUpdateValue(LastWeighDateKey, value);
        }

        public static double LastWeight
        {
            get => AppSettings.GetValueOrDefault(LastWeightKey, LastWeightDefault);
            set => AppSettings.AddOrUpdateValue(LastWeightKey, value);
        }

        public static int PickerSelectedItem
        {
            get => AppSettings.GetValueOrDefault(PickerKey, PickerDefault);
            set => AppSettings.AddOrUpdateValue(PickerKey, value);
        }

        public static string FirstUse
        {
            get => AppSettings.GetValueOrDefault(FirstUseKey, FirstUseDefault);
            set => AppSettings.AddOrUpdateValue(FirstUseKey, value);
        }

        public static bool Sex
        {
            get => AppSettings.GetValueOrDefault(SexKey, SexDefault);
            set => AppSettings.AddOrUpdateValue(SexKey, value);
        }

        public static int Age
        {
            get => AppSettings.GetValueOrDefault(AgeKey, AgeDefault);
            set => AppSettings.AddOrUpdateValue(AgeKey, value);
        }

        public static double HeightMajor
        {
            get => AppSettings.GetValueOrDefault(HeightMajorKey, HeightMajorDefault);
            set => AppSettings.AddOrUpdateValue(HeightMajorKey, value);
        }

        public static int HeightMinor
        {
            get => AppSettings.GetValueOrDefault(HeightMinorKey, HeightMinorDefault);
            set => AppSettings.AddOrUpdateValue(HeightMinorKey, value);
        }

        public static double Weight
        {
            get => AppSettings.GetValueOrDefault(WeightKey, WeightDefault);
            set => AppSettings.AddOrUpdateValue(WeightKey, value);
        }

        public static bool Units
        {
            get => AppSettings.GetValueOrDefault(UnitsKey, UnitsDefault);
            set => AppSettings.AddOrUpdateValue(UnitsKey, value);
        }

        public static bool GoalMetNotified
        {
            get => AppSettings.GetValueOrDefault(GoalMetNotifiedKey, GoalMetNotifiedDefault);
            set => AppSettings.AddOrUpdateValue(GoalMetNotifiedKey, value);
        }

        #region Setting Constants

        private const string GoalMetNotifiedKey = "goal_met_notified_key";
        private static readonly bool GoalMetNotifiedDefault = false;

        private const string FirstUseKey = "first_use_key";
        private static readonly string FirstUseDefault = "yes";

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
        private static readonly int PickerDefault = 1;

        private const string LastWeightKey = "last_weight_key";
        private static readonly double LastWeightDefault = 0.0;

        private const string InitialWeightKey = "initial_weight_key";
        private static readonly double InitialWeightDefault = 0.0;

        private const string InitialWeighDateKey = "initial_weigh_date_key";
        private static readonly DateTime InitialWeighDateDefault = DateTime.UtcNow.ToLocalTime();

        private const string LastWeighDateKey = "last_weigh_date_key";
        private static readonly DateTime LastWeighDateDefault = DateTime.UtcNow.ToLocalTime();

        private const string GoalWeightKey = "goal_weight_key";
        private static readonly double GoalWeightDefault = 0.0;

        private const string GoalDateKey = "goal_date_key";
        private static readonly DateTime GoalDateDefault = DateTime.UtcNow.ToLocalTime().AddDays(180);

        private const string WaistSizeKey = "waist_size_key";
        private static readonly double WaistSizeDefault = 0.0;

        private const string BMIKey = "BMI_key";
        private static readonly double BMIDefault = 0.0;

        private const string BMRKey = "BMR_key";
        private static readonly double BMRDefault = 0.0;

        private const string RecommendedDailyCaloricIntakeKey = "Recommended_Daily_Caloric_Intake_key";
        private static readonly double RecommendedDailyCaloricIntakeDefault = 0.0;

        private const string BMICategoryKey = "BMI_Category_key";
        private static readonly string BMICategoryDefault = string.Empty;

        private const string WeightPerWeekToMeetGoalKey = "Weight_Per_Week_To_Meet_Goal_key";
        private static readonly double WeightPerWeekToMeetGoalDefault = 0.0;

        private const string DistanceToGoalWeightKey = "Distance_To_Goal_Weight_Key";
        private static readonly double DistanceToGoalWeightDefault = 0.0;

        private const string WeightLostToDateKey = "Weight_Lost_To_Date_Key";
        private static readonly double WeightLostToDateDefault = 0.0;

        private const string TimeLeftToGoalKey = "Time_Left_To_Goal_Key";
        private static readonly int TimeLeftToGoalDefault = 0;

        private const string MinDateKey = "Min_Date_Key";
        private static readonly DateTime MinDateDefault = DateTime.UtcNow.ToLocalTime().AddDays(10);

        private const string WaistSizeEnabledKey = "Waist_Size_Enabled_Key";
        private static readonly bool WaistSizeEnabledDefault = true;

        private const string BirthDateKey = "Birth_Date_Key";
        private static readonly DateTime BirthDateDefault = DateTime.UtcNow.ToLocalTime();

        #endregion
    }
}