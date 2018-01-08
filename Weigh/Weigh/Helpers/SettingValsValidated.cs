using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Prism.Mvvm;
using SQLite;
using Weigh.Helpers;
using Weigh.Localization;
using Weigh.Validation;

namespace Weigh.Models
{
    public class SettingValsValidated : ValidatableBase
    {
        public SettingValsValidated()
        {

        }
        /*
        public SetupInfo(SetupInfoDB setupInfo)
        {
            Units = setupInfo.Units;
            Age = setupInfo.Age;
            HeightMajor = setupInfo.HeightMajor;
            HeightMinor = setupInfo.HeightMinor;
            Weight = setupInfo.Weight;
            WaistSize = setupInfo.WaistSize;
            PickerSelectedItem = setupInfo.PickerSelectedItem;
            Sex = setupInfo.Sex;
            GoalWeight = setupInfo.GoalWeight;
            GoalDate = setupInfo.GoalDate;
            MinDate = setupInfo.MinDate;
            LastWeight = setupInfo.LastWeight;
            LastWeighDate = setupInfo.LastWeighDate;
            InitialWeight = setupInfo.InitialWeight;
            InitialWeighDate = setupInfo.InitialWeighDate;
            BMR = setupInfo.BMR;
            BMI = setupInfo.BMI;
            RecommendedDailyCaloricIntake = setupInfo.RecommendedDailyCaloricIntake;
            BMICategory = setupInfo.BMICategory;
            DistanceToGoalWeight = setupInfo.DistanceToGoalWeight;
            TimeLeftToGoal = setupInfo.TimeLeftToGoal;
            WeightLostToDate = setupInfo.WeightLostToDate;
            RequiredCaloricDefecit = setupInfo.RequiredCaloricDefecit; ;
            WeightPerDayToMeetGoal = setupInfo.WeightPerDayToMeetGoal; ;
            DaysToAddToMeetMinimum = setupInfo.DaysToAddToMeetMinimum; ;
            WeightPerWeekToMeetGoal = setupInfo.WeightPerWeekToMeetGoal;
        }
        */
        private int _age;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(3, MinimumLength = 2, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "AgeLengthValidationErrorMessage")]
        [Range(1, 150, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "AgeValueValidationErrorMessage")]
        public int Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); Settings.Age = value; }
        }

        private double _heightMajor;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(5, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "HeightMajorLengthValidationErrorMessage")]        
        [CustomValidation(typeof(SetupInfoValidation), "HeightMajorValidation")]
        public double HeightMajor
        {
            get { return _heightMajor; }
            set { SetProperty(ref _heightMajor, value); Settings.HeightMajor = value; }
        }

        private int _heightMinor;
        [CustomValidation(typeof(SetupInfoValidation), "HeightMinorValidation")]
        [CustomValidation(typeof(SetupInfoValidation), "HeightMinorLengthValidation")]
        public int HeightMinor
        {
            get { return _heightMinor; }
            set { SetProperty(ref _heightMinor, value); Settings.HeightMinor = value; }
        }

        private double _weight;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(7, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "WeightLengthValidationErrorMessage")]
        [Range(0.0, 1000.0, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "WeightValueValidationErrorMessage")]
        public double Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); Settings.Weight = value; }
        }

        private double _waistSize;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(5, MinimumLength = 2, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "WaistLengthValidationErrorMessage")]
        [Range(15.0, 200.0, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "WaistValueValidationErrorMessage")]
        public double WaistSize
        {
            get { return _waistSize; }
            set { SetProperty(ref _waistSize, value); Settings.WaistSize = value; }
        }
        
        private double _goalWeight;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(7, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "GoalWeightLengthValidationErrorMessage")]
        [Range(0.0, 1000.0, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "GoalWeightValueValidationErrorMessage")]
        public double GoalWeight
        {
            get { return _goalWeight; }
            set { SetProperty(ref _goalWeight, value); Settings.GoalWeight = value; }
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
            set { SetProperty(ref _goalDate, value); Settings.GoalDate = value; }
        }

        private bool _units;
        public bool Units
        {
            get { return _units; }
            set { SetProperty(ref _units, value); Settings.Units = value; }
        }

        private string _pickerSelectedItem;
        public string PickerSelectedItem
        {
            get { return _pickerSelectedItem; }
            set { SetProperty(ref _pickerSelectedItem, value); Settings.PickerSelectedItem = value; }
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
        }
    }
}
