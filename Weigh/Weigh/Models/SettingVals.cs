﻿using System;
using Acr.UserDialogs;
using Prism.Mvvm;
using Weigh.Extensions;
using Weigh.Helpers;
using Weigh.Localization;

namespace Weigh.Models
{
    public class SettingVals : BindableBase
    {
        #region Fields

        // Settings Section
        private int _age;

        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        private double _heightMajor;

        public double HeightMajor
        {
            get => _heightMajor;
            set => SetProperty(ref _heightMajor, value);
        }

        private int _heightMinor;

        public int HeightMinor
        {
            get => _heightMinor;
            set => SetProperty(ref _heightMinor, value);
        }

        private double _waistSize;

        public double WaistSize
        {
            get => _waistSize;
            set => SetProperty(ref _waistSize, value);
        }

        private double _goalWeight;

        public double GoalWeight
        {
            get => _goalWeight;
            set => SetProperty(ref _goalWeight, value);
        }

        private bool _units;

        public bool Units
        {
            get => _units;
            set => SetProperty(ref _units, value);
        }

        private double _weight;

        public double Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        private bool _sex;

        public bool Sex
        {
            get => _sex;
            set => SetProperty(ref _sex, value);
        }

        private DateTimeOffset _birthDate;

        public DateTimeOffset BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
        }

        ////


        private double _recommendedDailyCaloricIntake;

        public double RecommendedDailyCaloricIntake
        {
            get => _recommendedDailyCaloricIntake;
            set => SetProperty(ref _recommendedDailyCaloricIntake, value);
        }

        private double _BMI;

        public double BMI
        {
            get => _BMI;
            set => SetProperty(ref _BMI, value);
        }

        private string _BMICategory;

        public string BMICategory
        {
            get => _BMICategory;
            set => SetProperty(ref _BMICategory, value);
        }

        private double _BMR;

        public double BMR
        {
            get => _BMR;
            set => SetProperty(ref _BMR, value);
        }

        private double _weightPerWeekToMeetGoal;

        public double WeightPerWeekToMeetGoal
        {
            get => _weightPerWeekToMeetGoal;
            set => SetProperty(ref _weightPerWeekToMeetGoal, value);
        }

        private double _distanceToGoalWeight;

        public double DistanceToGoalWeight
        {
            get => _distanceToGoalWeight;
            set => SetProperty(ref _distanceToGoalWeight, value);
        }

        private double _weightLostToDate;

        public double WeightLostToDate
        {
            get => _weightLostToDate;
            set => SetProperty(ref _weightLostToDate, value);
        }

        private int _timeLeftToGoal;

        public int TimeLeftToGoal
        {
            get => _timeLeftToGoal;
            set => SetProperty(ref _timeLeftToGoal, value);
        }
        ////


        private DateTimeOffset _minDate;

        public DateTimeOffset MinDate
        {
            get => _minDate;
            set => SetProperty(ref _minDate, value);
        }

        private int _pickerSelectedItem;

        public int PickerSelectedItem
        {
            get => _pickerSelectedItem;
            set => SetProperty(ref _pickerSelectedItem, value);
        }

        private bool _waistSizeEnabled;

        public bool WaistSizeEnabled
        {
            get => _waistSizeEnabled;
            set => SetProperty(ref _waistSizeEnabled, value);
        }

        ////

        private DateTimeOffset _goalDate;

        public DateTimeOffset GoalDate
        {
            get => _goalDate;
            set => SetProperty(ref _goalDate, value);
        }

        private DateTimeOffset _initialWeighDate;

        public DateTimeOffset InitialWeighDate
        {
            get => _initialWeighDate;
            set => SetProperty(ref _initialWeighDate, value);
        }

        private double _initialWeight;

        public double InitialWeight
        {
            get => _initialWeight;
            set => SetProperty(ref _initialWeight, value);
        }

        private DateTimeOffset _lastWeighDate;

        public DateTimeOffset LastWeighDate
        {
            get => _lastWeighDate;
            set => SetProperty(ref _lastWeighDate, value);
        }

        private double _lastWeight;

        public double LastWeight
        {
            get => _lastWeight;
            set => SetProperty(ref _lastWeight, value);
        }

        private WeightEntry _latestWeight;
        public WeightEntry LatestWeight
        {
            get { return _latestWeight; }
            set { SetProperty(ref _latestWeight, value); }
        }
        #endregion

        #region Methods

        public void InitializeFromValidated(SettingValsValidated _validatedSettings)
        {
            Age = Convert.ToInt32(_validatedSettings.Age);
            GoalWeight = Convert.ToDouble(_validatedSettings.GoalWeight);
            HeightMajor = Convert.ToDouble(_validatedSettings.HeightMajor);
            HeightMinor = Convert.ToInt32(_validatedSettings.HeightMinor);
            WaistSize = Convert.ToDouble(_validatedSettings.WaistSize);
            Weight = Convert.ToDouble(_validatedSettings.Weight);
        }

        public void InitializeSettingVals()
        {
            Age = Settings.Age;
            HeightMajor = Settings.HeightMajor;
            HeightMinor = Settings.HeightMinor;
            WaistSize = Settings.WaistSize;
            GoalWeight = Settings.GoalWeight;
            Units = Settings.Units;
            Weight = Settings.Weight;
            Sex = Settings.Sex;
            BirthDate = Settings.BirthDate;

            RecommendedDailyCaloricIntake = Settings.RecommendedDailyCaloricIntake;
            BMI = Settings.BMI;
            BMICategory = Settings.BMICategory;
            BMR = Settings.BMR;
            WeightPerWeekToMeetGoal = Settings.WeightPerWeekToMeetGoal;
            DistanceToGoalWeight = Settings.DistanceToGoalWeight;
            WeightLostToDate = Settings.WeightLostToDate;
            TimeLeftToGoal = Settings.TimeLeftToGoal;

            MinDate = Settings.MinDate;
            PickerSelectedItem = Settings.PickerSelectedItem;
            WaistSizeEnabled = Settings.WaistSizeEnabled;

            GoalDate = Settings.GoalDate;
            InitialWeighDate = Settings.InitialWeightDate;
            InitialWeight = Settings.InitialWeight;
            LastWeighDate = Settings.LastWeighDate;
            LastWeight = Settings.LastWeight;
        }

        public void SaveSettingValsToDevice()
        {
            Settings.Age = Age;
            Settings.HeightMajor = HeightMajor;
            Settings.HeightMinor = HeightMinor;
            Settings.WaistSize = WaistSize;
            Settings.GoalWeight = GoalWeight;
            Settings.Units = Units;
            Settings.Weight = Weight;
            Settings.Sex = Sex;
            Settings.BirthDate = BirthDate.UtcDateTime;

            Settings.RecommendedDailyCaloricIntake = RecommendedDailyCaloricIntake;
            Settings.BMI = BMI;
            Settings.BMICategory = BMICategory;
            Settings.BMR = BMR;
            Settings.WeightPerWeekToMeetGoal = WeightPerWeekToMeetGoal;
            Settings.DistanceToGoalWeight = DistanceToGoalWeight;
            Settings.WeightLostToDate = WeightLostToDate;
            Settings.TimeLeftToGoal = TimeLeftToGoal;

            Settings.MinDate = MinDate.UtcDateTime;
            Settings.PickerSelectedItem = PickerSelectedItem;
            Settings.WaistSizeEnabled = WaistSizeEnabled;

            Settings.GoalDate = GoalDate.UtcDateTime;
            Settings.InitialWeightDate = InitialWeighDate.UtcDateTime;
            Settings.InitialWeight = InitialWeight;
            Settings.LastWeighDate = LastWeighDate.UtcDateTime;
            Settings.LastWeight = LastWeight;
        }

        public void CalculateBMI()
        {
            var feet = HeightMajor;
            var inches = HeightMinor;
            var weight = Weight;
            // Units are metric if false, so do conversion here
            if (Units == false)
            {
                (feet, inches) = HeightMajor.CentimetersToFeetInches();
                weight = Weight.KilogramsToPounds();
            }

            BMI = weight / Math.Pow(feet * 12 + inches, 2) * 703;
        }

        public void SetBMICategory()
        {
            // Categories based on site here: https://www.nhlbi.nih.gov/health/educational/lose_wt/BMI/bmicalc.htm
            if (BMI < 18.5) BMICategory = AppResources.UnderweightBMICategory;

            if (BMI >= 18.5 && BMI <= 24.9) BMICategory = AppResources.NormalWeightBMICategory;

            if (BMI >= 25 && BMI <= 29.9) BMICategory = AppResources.OverweightLabel;

            if (BMI >= 30) BMICategory = AppResources.ObeseLabel;
        }

        public void CalculateBMR()
        {
            var feet = HeightMajor;
            var inches = HeightMinor;
            var weight = Weight;
            var age = Age;
            // Units are metric if false, so do conversion here
            if (Units == false)
            {
                (feet, inches) = HeightMajor.CentimetersToFeetInches();
                weight = Weight.KilogramsToPounds();
            }

            if (Sex == false)
                BMR = 66 + 6.2 * weight + 12.7 * (feet * 12 + inches) - 6.76 * age;
            else
                BMR = 655.1 + 4.35 * weight + 4.7 * (feet * 12 + inches) - 4.7 * age;
            if (PickerSelectedItem == 0) BMR *= 1.15;
            if (PickerSelectedItem == 1) BMR *= 1.30;
            if (PickerSelectedItem == 2) BMR *= 1.40;
            if (PickerSelectedItem == 3) BMR *= 1.6;
        }

        public bool ValidateGoal()
        {
            CalculateBMI();
            CalculateBMR();
            SetBMICategory();

            Age = (DateTimeOffset.Now - BirthDate.LocalDateTime).Days / 365;
            TimeLeftToGoal = Math.Max(0, (GoalDate.LocalDateTime - DateTimeOffset.Now).Days);
            DistanceToGoalWeight = Math.Max(0, Weight - GoalWeight);
            WeightLostToDate = InitialWeight - Weight;

            if (DistanceToGoalWeight <= 0)
            {
                // TODO: Implement goal popup & handling
                Settings.GoalMetNotified = true;
                //UserDialogs.Instance.Alert(string.Format(AppResources.GoalTooSoonPopup, GoalDate));
            }

            var feet = HeightMajor;
            var inches = HeightMinor;
            var weight = Weight;
            var goalWeight = GoalWeight;
            // Units are metric if false, so do conversion here
            if (Units == false)
            {
                (feet, inches) = HeightMajor.CentimetersToFeetInches();
                weight = Weight.KilogramsToPounds();
                goalWeight = GoalWeight.KilogramsToPounds();
            }


            var weightPerDayToMeetGoal = (weight - goalWeight) / (GoalDate.LocalDateTime - DateTimeOffset.Now).TotalDays;
            WeightPerWeekToMeetGoal = weightPerDayToMeetGoal * 7;
            var requiredCaloricDefecit = 500 * WeightPerWeekToMeetGoal;
            RecommendedDailyCaloricIntake = (int) BMR - requiredCaloricDefecit;

            if (Sex && RecommendedDailyCaloricIntake < 1200)
            {
                // Min calories/day for women is 1200
                requiredCaloricDefecit = BMR - 1300;
                RecommendedDailyCaloricIntake = (int)BMR - requiredCaloricDefecit;
                WeightPerWeekToMeetGoal = requiredCaloricDefecit / 500;
                var daysToAddToMeetMinimum = (int) ((weight - goalWeight) / (WeightPerWeekToMeetGoal / 7));
                GoalDate = DateTimeOffset.Now.AddDays(daysToAddToMeetMinimum + 10);
                TimeLeftToGoal = Math.Max(0, (GoalDate.LocalDateTime - DateTimeOffset.Now).Days);
                if (GoalDate > DateTimeOffset.Now.AddYears(2))
                {
                    GoalDate = DateTimeOffset.Now.AddYears(2);
                    UserDialogs.Instance.Alert(string.Format(AppResources.popup_goal_too_soon_label, GoalDate.LocalDateTime));
                }
                else
                {
                    UserDialogs.Instance.Alert(string.Format(AppResources.GoalTooSoonPopup, GoalDate.LocalDateTime));
                }
                return false;
                //Create(async token => await this.Dialogs.AlertAsync("Test alert", "Alert Title", null, token));
            }

            if (Sex == false && RecommendedDailyCaloricIntake < 1600)
            {
                // Min calories/day for men is 1600
                requiredCaloricDefecit = BMR - 1700;
                RecommendedDailyCaloricIntake = (int)BMR - requiredCaloricDefecit;
                WeightPerWeekToMeetGoal = requiredCaloricDefecit / 500;
                var daysToAddToMeetMinimum = (int) ((weight - goalWeight) / (WeightPerWeekToMeetGoal / 7));
                GoalDate = DateTimeOffset.Now.AddDays(daysToAddToMeetMinimum + 10);
                TimeLeftToGoal = Math.Max(0, (GoalDate.LocalDateTime - DateTimeOffset.Now).Days);
                if (GoalDate > DateTimeOffset.Now.AddYears(2))
                {
                    GoalDate = DateTimeOffset.Now.AddYears(2);
                    UserDialogs.Instance.Alert(string.Format(AppResources.popup_goal_too_soon_label, GoalDate.LocalDateTime));
                }
                else
                {
                    UserDialogs.Instance.Alert(string.Format(AppResources.GoalTooSoonPopup, GoalDate.LocalDateTime));
                }
                return false;
                // Keeping for future use maybe
                /*
                UserDialogs.Instance.Toast(new ToastConfig(string.Format("Goal date has been set to: {0:MM/dd/yy}", AppState.GoalDate))
                    .SetDuration(TimeSpan.FromSeconds(3))
                    .SetPosition(ToastPosition.Bottom));
                    */
            }

            return true;
        }

        #endregion
    }
}