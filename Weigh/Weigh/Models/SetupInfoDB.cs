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
    public class SetupInfoDB
    {
        // We only want 1 instance of SetupInfo in the DB, so will always return 1
        [PrimaryKey]
        public int ID { get { return 1; }}

        public SetupInfoDB()
        {

        }

        public SetupInfoDB(SetupInfo setupInfo)
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

        private bool _units;

        public bool Units
        {
            get { return _units; }
            set { _units = value; }
        }


        private string _age;

        public string Age
        {
            get { return _age; }
            set { _age = value; }
        }

        private string _heightMajor;

        public string HeightMajor
        {
            get { return _heightMajor; }
            set { _heightMajor = value; }
        }

        private string _heightMinor;

        public string HeightMinor
        {
            get { return _heightMinor; }
            set { _heightMinor = value; }
        }

        private string _weight;

        public string Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private string _waistSize;

        public string WaistSize
        {
            get { return _waistSize; }
            set { _waistSize = value; }
        }

        private string _pickerSelectedItem;

        public string PickerSelectedItem
        {
            get { return _pickerSelectedItem; }
            set { _pickerSelectedItem = value; }
        }

        /*
        private List<string> _pickerSource;
        public List<string> PickerSource
        {
            get { return _pickerSource; }
            set { SetProperty(ref _pickerSource, value); }
        }
        */

        private bool _sex;

        public bool Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        private string _goalWeight;

        public string GoalWeight
        {
            get { return _goalWeight; }
            set { _goalWeight = value; }
        }

        private DateTime _goalDate;

        public DateTime GoalDate
        {
            get { return _goalDate; }
            set { _goalDate = value; }
        }

        private DateTime _minDate;

        public DateTime MinDate
        {
            get { return _minDate; }
            set { _minDate = value; }
        }

        private double _lastWeight;

        public double LastWeight
        {
            get { return _lastWeight; }
            set { _lastWeight = value; }
        }

        private DateTime _lastWeighDate;

        public DateTime LastWeighDate
        {
            get { return _lastWeighDate; }
            set { _lastWeighDate = value; }
        }

        private double _initialWeight;

        public double InitialWeight
        {
            get { return _initialWeight; }
            set { _initialWeight = value; }
        }


        private DateTime _initialWeighDate;

        public DateTime InitialWeighDate
        {
            get { return _initialWeighDate; }
            set { _initialWeighDate = value; }
        }

        private double _BMR;

        public double BMR
        {
            get { return _BMR; }
            set { _BMR = value; }
        }

        private double _BMI;

        public double BMI
        {
            get { return _BMI; }
            set { _BMI = value; }
        }

        private double _recommendedDailyCaloricIntake;

        public double RecommendedDailyCaloricIntake
        {
            get { return _recommendedDailyCaloricIntake; }
            set { _recommendedDailyCaloricIntake = value; }
        }

        private string _bmiCategory;

        public string BMICategory
        {
            get { return _bmiCategory; }
            set { _bmiCategory = value; }
        }

        private double _distanceToGoalWeight;
        public double DistanceToGoalWeight
        {
            get { return _distanceToGoalWeight; }
            set { _distanceToGoalWeight = value; }
        }

        private int _timeLeftToGoal;
        public int TimeLeftToGoal
        {
            get { return _timeLeftToGoal; }
            set { _timeLeftToGoal = value; }
        }

        private string _weightLostToDate;
        public string WeightLostToDate
        {
            get { return _weightLostToDate; }
            set { _weightLostToDate = value; }
        }

        private double _requiredCaloricDefecit;
        public double RequiredCaloricDefecit
        {
            get { return _requiredCaloricDefecit; }
            set { _requiredCaloricDefecit = value; }
        }

        private double _weightPerDayToMeetGoal;
        public double WeightPerDayToMeetGoal
        {
            get { return _weightPerDayToMeetGoal; }
            set { _weightPerDayToMeetGoal = value; }
        }

        private int _daysToAddToMeetMinimum;
        public int DaysToAddToMeetMinimum
        {
            get { return _daysToAddToMeetMinimum; }
            set { _daysToAddToMeetMinimum = value; }
        }

        private double _weightPerWeekToMeetGoal;
        public double WeightPerWeekToMeetGoal
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
