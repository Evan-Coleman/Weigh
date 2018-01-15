﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Prism.Mvvm;
using SQLite;
using Weigh.Helpers;
using Weigh.Localization;
using Weigh.Extensions;
using Weigh.Validation;
using Acr.UserDialogs;

namespace Weigh.Models
{
    public class SettingVals : BindableBase
    {
        #region Fields
        private int _age;
        public int Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }

        private double _goalWeight;
        public double GoalWeight
        {
            get { return _goalWeight; }
            set { SetProperty(ref _goalWeight, value); }
        }

        private double _heightMajor;
        public double HeightMajor
        {
            get { return _heightMajor; }
            set { SetProperty(ref _heightMajor, value); }
        }

        private int _heightMinor;
        public int HeightMinor
        {
            get { return _heightMinor; }
            set { SetProperty(ref _heightMinor, value); }
        }

        private double _waistSize;
        public double WaistSize
        {
            get { return _waistSize; }
            set { SetProperty(ref _waistSize, value); }
        }

        private bool _units;
        public bool Units
        {
            get { return _units; }
            set { SetProperty(ref _units, value); }
        }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }

        private bool _sex;
        public bool Sex
        {
            get { return _sex; }
            set { SetProperty(ref _sex, value); }
        }

        ////



        private double _recommendedDailyCaloricIntake;
        public double RecommendedDailyCaloricIntake
        {
            get { return _recommendedDailyCaloricIntake; }
            set { SetProperty(ref _recommendedDailyCaloricIntake, value); }
        }

        private double _BMI;
        public double BMI
        {
            get { return _BMI; }
            set { SetProperty(ref _BMI, value); }
        }

        private string _BMICategory;
        public string BMICategory
        {
            get { return _BMICategory; }
            set { SetProperty(ref _BMICategory, value); }
        }

        private double _BMR;
        public double BMR
        {
            get { return _BMR; }
            set { SetProperty(ref _BMR, value); }
        }

        private double _weightPerWeekToMeetGoal;
        public double WeightPerWeekToMeetGoal
        {
            get { return _weightPerWeekToMeetGoal; }
            set { SetProperty(ref _weightPerWeekToMeetGoal, value); }
        }

        private double _distanceToGoalWeight;
        public double DistanceToGoalWeight
        {
            get { return _distanceToGoalWeight; }
            set { SetProperty(ref _distanceToGoalWeight, value); }
        }

        private double _weightLostToDate;
        public double WeightLostToDate
        {
            get { return _weightLostToDate; }
            set { SetProperty(ref _weightLostToDate, value); }
        }

        private int _timeLeftToGoal;
        public int TimeLeftToGoal
        {
            get { return _timeLeftToGoal; }
            set { SetProperty(ref _timeLeftToGoal, value); }
        }
        ////


        private DateTime _minDate;
        public DateTime MinDate
        {
            get { return _minDate; }
            set { SetProperty(ref _minDate, value); }
        }

        private string _pickerSelectedItem;
        public string PickerSelectedItem
        {
            get { return _pickerSelectedItem; }
            set { SetProperty(ref _pickerSelectedItem, value); }
        }

        private bool _waistSizeEnabled;
        public bool WaistSizeEnabled
        {
            get { return _waistSizeEnabled; }
            set { SetProperty(ref _waistSizeEnabled, value); }
        }

        ////

        private DateTime _goalDate;
        public DateTime GoalDate
        {
            get { return _goalDate; }
            set { SetProperty(ref _goalDate, value); }
        }

        private DateTime _initialWeighDate;
        public DateTime InitialWeighDate
        {
            get { return _initialWeighDate; }
            set { SetProperty(ref _initialWeighDate, value); }
        }

        private double _initialWeight;
        public double InitialWeight
        {
            get { return _initialWeight; }
            set { SetProperty(ref _initialWeight, value); }
        }

        private DateTime _lastWeighDate;
        public DateTime LastWeighDate
        {
            get { return _lastWeighDate; }
            set { SetProperty(ref _lastWeighDate, value); }
        }

        private double _lastWeight;
        public double LastWeight
        {
            get { return _lastWeight; }
            set { SetProperty(ref _lastWeight, value); }
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
            Weight = Settings.Weight;
            WaistSize = Settings.WaistSize;
            GoalWeight = Settings.GoalWeight;
            GoalDate = Settings.GoalDate;
            Units = Settings.Units;
            PickerSelectedItem = Settings.PickerSelectedItem;
            Sex = Settings.Sex;
            BMI = Settings.BMI;
            BMR = Settings.BMR;
            RecommendedDailyCaloricIntake = Settings.RecommendedDailyCaloricIntake;
            BMICategory = Settings.BMICategory;
            WeightPerWeekToMeetGoal = Settings.WeightPerWeekToMeetGoal;
            LastWeighDate = Settings.LastWeighDate;
            LastWeight = Settings.LastWeight;
            DistanceToGoalWeight = Settings.DistanceToGoalWeight;
            WeightLostToDate = Settings.WeightLostToDate;
            InitialWeight = Settings.InitialWeight;
            TimeLeftToGoal = Settings.TimeLeftToGoal;
            MinDate = Settings.MinDate;
            InitialWeighDate = Settings.InitialWeightDate;
            WaistSizeEnabled = Settings.WaistSizeEnabled;
        }
        public void SaveSettingValsToDevice()
        {
            Settings.Age = Age;
            Settings.HeightMajor = HeightMajor;
            Settings.HeightMinor = HeightMinor;
            Settings.Weight = Weight;
            Settings.WaistSize = WaistSize;
            Settings.GoalWeight = GoalWeight;
            Settings.GoalDate = GoalDate;
            Settings.Units = Units;
            Settings.PickerSelectedItem = PickerSelectedItem;
            Settings.Sex = Sex;
            Settings.BMI = BMI;
            Settings.BMR = BMR;
            Settings.RecommendedDailyCaloricIntake = RecommendedDailyCaloricIntake;
            Settings.BMICategory = BMICategory;
            Settings.WeightPerWeekToMeetGoal = WeightPerWeekToMeetGoal;
            Settings.LastWeighDate = LastWeighDate;
            Settings.LastWeight = LastWeight;
            Settings.DistanceToGoalWeight = DistanceToGoalWeight;
            Settings.WeightLostToDate = WeightLostToDate;
            Settings.InitialWeight = InitialWeight;
            Settings.TimeLeftToGoal = TimeLeftToGoal;
            Settings.MinDate = MinDate;
            Settings.InitialWeightDate = InitialWeighDate;
            Settings.WaistSizeEnabled = WaistSizeEnabled;
        }

        public void CalculateBMI()
        {
            double Feet = HeightMajor;
            int Inches = HeightMinor;
            double _weight = Weight;
            // Units are metric if false, so do conversion here
            if (Units == false)
            {
                (Feet, Inches) = HeightMajor.CentimetersToFeetInches();
                _weight = Weight.KilogramsToPounds();
            }

            BMI = (_weight / Math.Pow(((Feet * 12) + Inches), 2)) * 703;
        }
        public void SetBMICategory()
        {
            // Categories based on site here: https://www.nhlbi.nih.gov/health/educational/lose_wt/BMI/bmicalc.htm
            if (BMI < 18.5)
            {
                BMICategory = AppResources.UnderweightBMICategory;
            }

            if (BMI >= 18.5 && BMI <= 24.9)
            {
                BMICategory = AppResources.NormalWeightBMICategory;
            }

            if (BMI >= 25 && BMI <= 29.9)
            {
                BMICategory = AppResources.OverweightBMICategory;
            }

            if (BMI >= 30)
            {
                BMICategory = AppResources.ObeseWeightBMICategory;
            }
        }
        public void CalculateBMR()
        {
            double Feet = HeightMajor;
            int Inches = HeightMinor;
            double _weight = Weight;
            int _age = Age;
            // Units are metric if false, so do conversion here
            if (Units == false)
            {
                (Feet, Inches) = HeightMajor.CentimetersToFeetInches();
                _weight = Weight.KilogramsToPounds();
            }

            if (Sex == false)
            {
                BMR = 66 + (6.2 * _weight) + (12.7 * ((Feet * 12) + Inches)) - (6.76 * _age);
            }
            else
            {
                BMR = 655.1 + (4.35 * _weight) + (4.7 * ((Feet * 12) + Inches)) - (4.7 * _age);
            }
            if (PickerSelectedItem == AppResources.LowActivityPickItem)
            {
                BMR *= 1.2;
            }
            if (PickerSelectedItem == AppResources.LightActivityPickItem)
            {
                BMR *= 1.375;
            }
            if (PickerSelectedItem == AppResources.MediumActivityPickItem)
            {
                BMR *= 1.55;
            }
            if (PickerSelectedItem == AppResources.HeavyActivityPickItem)
            {
                BMR *= 1.725;
            }
        }

        public bool ValidateGoal()
        {
            CalculateBMI();
            CalculateBMR();
            SetBMICategory();

            TimeLeftToGoal = (GoalDate - DateTime.UtcNow).Days;
            DistanceToGoalWeight = Weight - GoalWeight;
            WeightLostToDate = InitialWeight - Weight;

            double Feet = HeightMajor;
            int Inches = HeightMinor;
            double _weight = Weight;
            double _goalWeight = GoalWeight;
            // Units are metric if false, so do conversion here
            if (Units == false)
            {
                (Feet, Inches) = HeightMajor.CentimetersToFeetInches();
                _weight = Weight.KilogramsToPounds();
                _goalWeight = GoalWeight.KilogramsToPounds();
            }





            double WeightPerDayToMeetGoal = (_weight - _goalWeight) / (GoalDate - DateTime.UtcNow).TotalDays;
            WeightPerWeekToMeetGoal = WeightPerDayToMeetGoal * 7;
            double RequiredCaloricDefecit = 500 * WeightPerWeekToMeetGoal;
            RecommendedDailyCaloricIntake = (int)BMR - RequiredCaloricDefecit;

            if (Sex == true && RecommendedDailyCaloricIntake < 1200)
            {
                // Min calories/day for women is 1200
                RequiredCaloricDefecit = BMR - 1300;
                WeightPerWeekToMeetGoal = RequiredCaloricDefecit / 500;
                int DaysToAddToMeetMinimum = (int)((_weight - _goalWeight) / (WeightPerWeekToMeetGoal / 7));
                GoalDate = DateTime.Now.ToLocalTime().AddDays(DaysToAddToMeetMinimum + 10);
                UserDialogs.Instance.Alert(string.Format(AppResources.GoalTooSoonPopup, _goalDate));
                return false;
                //Create(async token => await this.Dialogs.AlertAsync("Test alert", "Alert Title", null, token));
            }
            if (Sex == false && RecommendedDailyCaloricIntake < 1800)
            {
                // Min calories/day for men is 1800
                RequiredCaloricDefecit = BMR - 1900;
                WeightPerWeekToMeetGoal = RequiredCaloricDefecit / 500;
                int DaysToAddToMeetMinimum = (int)((_weight - _goalWeight) / (WeightPerWeekToMeetGoal / 7));
                GoalDate = DateTime.Now.ToLocalTime().AddDays(DaysToAddToMeetMinimum + 10);
                UserDialogs.Instance.Alert(string.Format(AppResources.GoalTooSoonPopup, GoalDate));
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
