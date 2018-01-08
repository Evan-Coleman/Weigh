using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Prism.Mvvm;
using SQLite;
using Weigh.Helpers;
using Weigh.Validation;

namespace Weigh.Models
{
    public static class SettingVals
    {
        // We only want 1 instance of SetupInfo in the DB, so will always return 1
        [PrimaryKey]
        public static int ID { get { return 1; } }

        
        public static void InitializeSettingVals(SettingValsValidated setupInfo)
        {
           
            Age = setupInfo.Age;
            HeightMajor = setupInfo.HeightMajor;
            HeightMinor = setupInfo.HeightMinor;
            Weight = setupInfo.Weight;
            WaistSize = setupInfo.WaistSize;
            GoalWeight = setupInfo.GoalWeight;
            /*
            Units = setupInfo.Units;
            PickerSelectedItem = setupInfo.PickerSelectedItem;
            Sex = setupInfo.Sex;
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
            */
        }
        

        private static bool _units;

        public static bool Units
        {
            get { return _units; }
            set { _units = value; }
        }


        private static string _age;

        public static string Age
        {
            get { return _age; }
            set { _age = value; }
        }

        private static string _heightMajor;

        public static string HeightMajor
        {
            get { return _heightMajor; }
            set { _heightMajor = value; }
        }

        private static string _heightMinor;

        public static string HeightMinor
        {
            get { return _heightMinor; }
            set { _heightMinor = value; }
        }

        private static string _weight;

        public static string Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private static string _waistSize;

        public static string WaistSize
        {
            get { return _waistSize; }
            set { _waistSize = value; }
        }

        private static string _pickerSelectedItem;

        public static string PickerSelectedItem
        {
            get { return _pickerSelectedItem; }
            set { _pickerSelectedItem = value; }
        }

        /*
        private static List<string> _pickerSource;
        public static List<string> PickerSource
        {
            get { return _pickerSource; }
            set { SetProperty(ref _pickerSource, value); }
        }
        */

        private static bool _sex;

        public static bool Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        private static string _goalWeight;

        public static string GoalWeight
        {
            get { return _goalWeight; }
            set { _goalWeight = value; }
        }

        private static DateTime _goalDate;

        public static DateTime GoalDate
        {
            get { return _goalDate; }
            set { _goalDate = value; }
        }



        private static double _lastWeight;

        public static double LastWeight
        {
            get { return _lastWeight; }
            set { _lastWeight = value; }
        }

        private static DateTime _lastWeighDate;

        public static DateTime LastWeighDate
        {
            get { return _lastWeighDate; }
            set { _lastWeighDate = value; }
        }

        private static double _initialWeight;

        public static double InitialWeight
        {
            get { return _initialWeight; }
            set { _initialWeight = value; }
        }


        private static DateTime _initialWeighDate;

        public static DateTime InitialWeighDate
        {
            get { return _initialWeighDate; }
            set { _initialWeighDate = value; }
        }

        private static double _BMR;

        public static double BMR
        {
            get { return _BMR; }
            set { _BMR = value; }
        }

        private static double _BMI;

        public static double BMI
        {
            get { return _BMI; }
            set { _BMI = value; }
        }

        private static double _recommendedDailyCaloricIntake;

        public static double RecommendedDailyCaloricIntake
        {
            get { return _recommendedDailyCaloricIntake; }
            set { _recommendedDailyCaloricIntake = value; }
        }

        private static string _bmiCategory;

        public static string BMICategory
        {
            get { return _bmiCategory; }
            set { _bmiCategory = value; }
        }

        private static double _distanceToGoalWeight;
        public static double DistanceToGoalWeight
        {
            get { return _distanceToGoalWeight; }
            set { _distanceToGoalWeight = value; }
        }

        private static int _timeLeftToGoal;
        public static int TimeLeftToGoal
        {
            get { return _timeLeftToGoal; }
            set { _timeLeftToGoal = value; }
        }

        private static string _weightLostToDate;
        public static string WeightLostToDate
        {
            get { return _weightLostToDate; }
            set { _weightLostToDate = value; }
        }

        private static double _requiredCaloricDefecit;
        public static double RequiredCaloricDefecit
        {
            get { return _requiredCaloricDefecit; }
            set { _requiredCaloricDefecit = value; }
        }

        private static double _weightPerDayToMeetGoal;
        public static double WeightPerDayToMeetGoal
        {
            get { return _weightPerDayToMeetGoal; }
            set { _weightPerDayToMeetGoal = value; }
        }

        private static int _daysToAddToMeetMinimum;
        public static int DaysToAddToMeetMinimum
        {
            get { return _daysToAddToMeetMinimum; }
            set { _daysToAddToMeetMinimum = value; }
        }

        private static double _weightPerWeekToMeetGoal;
        public static double WeightPerWeekToMeetGoal
        {
            get { return _weightPerWeekToMeetGoal; }
            set { _weightPerWeekToMeetGoal = value; }
        }


        // TODO: Remove if name not wanted in future
        /*
private string _name;
[Required(ErrorMessage = "This field is required")]
[StringLength(25, MinimumLength = 1, ErrorMessage = "Minimum 1 chars, max 25 chars")]
public string Name
{
    get { return _name; }
    set { SetProperty(ref _name, value); }
}
*/
    }
}
