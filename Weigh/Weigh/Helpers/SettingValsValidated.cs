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
    public class SettingValsValidated : ValidatableBase
    {
        #region Fields
        private string _age;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(3, MinimumLength = 2, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "AgeLengthValidationErrorMessage")]
        [Range(1, 150, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "AgeValueValidationErrorMessage")]
        public string Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }

        private string _heightMajor;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(5, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "HeightMajorLengthValidationErrorMessage")]        
        [CustomValidation(typeof(SetupInfoValidation), "HeightMajorValidation")]
        public string HeightMajor
        {
            get { return _heightMajor; }
            set { SetProperty(ref _heightMajor, value); }
        }

        private string _heightMinor;
        [CustomValidation(typeof(SetupInfoValidation), "HeightMinorValidation")]
        [CustomValidation(typeof(SetupInfoValidation), "HeightMinorLengthValidation")]
        public string HeightMinor
        {
            get { return _heightMinor; }
            set { SetProperty(ref _heightMinor, value); }
        }

        private string _weight;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(7, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "WeightLengthValidationErrorMessage")]
        [Range(0.0, 1000.0, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "WeightValueValidationErrorMessage")]
        public string Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }

        private string _waistSize;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(5, MinimumLength = 2, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "WaistLengthValidationErrorMessage")]
        [Range(15.0, 200.0, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "WaistValueValidationErrorMessage")]
        public string WaistSize
        {
            get { return _waistSize; }
            set { SetProperty(ref _waistSize, value); }
        }
        
        private string _goalWeight;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(7, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "GoalWeightLengthValidationErrorMessage")]
        [Range(0.0, 1000.0, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "GoalWeightValueValidationErrorMessage")]
        public string GoalWeight
        {
            get { return _goalWeight; }
            set { SetProperty(ref _goalWeight, value); }
        }

        private DateTime _minDate;
        public DateTime MinDate
        {
            get { return _minDate; }
            set { SetProperty(ref _minDate, value); }
        }

        private DateTime _goalDate;
        public DateTime GoalDate
        {
            get { return _goalDate; }
            set { SetProperty(ref _goalDate, value); }
        }

        private bool _units;
        public bool Units
        {
            get { return _units; }
            set { SetProperty(ref _units, value); }
        }

        private string _pickerSelectedItem;
        public string PickerSelectedItem
        {
            get { return _pickerSelectedItem; }
            set { SetProperty(ref _pickerSelectedItem, value); }
        }

        private double _BMI;
        public double BMI
        {
            get { return _BMI; }
            set { SetProperty(ref _BMI, value); }
        }

        private bool _sex;
        public bool Sex
        {
            get { return _sex; }
            set { SetProperty(ref _sex, value); }
        }

        private double _BMR;
        public double BMR
        {
            get { return _BMR; }
            set { SetProperty(ref _BMR, value); }
        }

        private double _recommendedDailyCaloricIntake;
        public double RecommendedDailyCaloricIntake
        {
            get { return _recommendedDailyCaloricIntake; }
            set { SetProperty(ref _recommendedDailyCaloricIntake, value); }
        }

        private string _BMICategory;
        public string BMICategory
        {
            get { return _BMICategory; }
            set { SetProperty(ref _BMICategory, value); }
        }

        private double _weightPerWeekToMeetGoal;
        public double WeightPerWeekToMeetGoal
        {
            get { return _weightPerWeekToMeetGoal; }
            set { SetProperty(ref _weightPerWeekToMeetGoal, value); }
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

        private double _initialWeight;
        public double InitialWeight
        {
            get { return _initialWeight; }
            set { SetProperty(ref _initialWeight, value); }
        }
        #endregion

        #region Methods
        public void InitializeSettingVals()
        {
            Age = Settings.Age.ToString();
            HeightMajor = Settings.HeightMajor.ToString();
            HeightMinor = Settings.HeightMinor.ToString();
            Weight = Settings.Weight.ToString();
            WaistSize = Settings.WaistSize.ToString();
            GoalWeight = Settings.GoalWeight.ToString();
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
        }
        public void SaveSettingValsToDevice()
        {
            Settings.Age = Convert.ToInt32(Age);
            Settings.HeightMajor = Convert.ToDouble(HeightMajor);
            Settings.HeightMinor = Convert.ToInt32(HeightMinor);
            Settings.Weight = Convert.ToDouble(Weight);
            Settings.WaistSize = Convert.ToDouble(WaistSize);
            Settings.GoalWeight = Convert.ToDouble(GoalWeight);
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
        }

        public void CalculateBMI()
        {
            double Feet = Convert.ToDouble(HeightMajor);
            int Inches = Convert.ToInt32(HeightMinor);
            double _weight = Convert.ToDouble(Weight);
            // Units are metric if false, so do conversion here
            if (Units == false)
            {
                (Feet, Inches) = Convert.ToDouble(HeightMajor).CentimetersToFeetInches();
                _weight = Convert.ToDouble(Weight).KilogramsToPounds();
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
            double Feet = Convert.ToDouble(HeightMajor);
            int Inches = Convert.ToInt32(HeightMinor);
            double _weight = Convert.ToDouble(Weight);
            int _age = Convert.ToInt32(Age);
            // Units are metric if false, so do conversion here
            if (Units == false)
            {
                (Feet, Inches) = Convert.ToDouble(HeightMajor).CentimetersToFeetInches();
                _weight = Convert.ToDouble(Weight).KilogramsToPounds();
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

            double Feet = Convert.ToDouble(HeightMajor);
            int Inches = Convert.ToInt32(HeightMinor);
            double _weight = Convert.ToDouble(Weight);
            double _goalWeight = Convert.ToDouble(GoalWeight);
            // Units are metric if false, so do conversion here
            if (Units == false)
            {
                (Feet, Inches) = Convert.ToDouble(HeightMajor).CentimetersToFeetInches();
                _weight = Convert.ToDouble(Weight).KilogramsToPounds();
                _goalWeight = Convert.ToDouble(GoalWeight).KilogramsToPounds();
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
               GoalDate = DateTime.Now.ToLocalTime().AddDays(DaysToAddToMeetMinimum);
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
                GoalDate = DateTime.Now.ToLocalTime().AddDays(DaysToAddToMeetMinimum);
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
