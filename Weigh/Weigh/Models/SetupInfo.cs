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
    public class SetupInfo : ValidatableBase
    {

        // We only want 1 instance of SetupInfo in the DB, so will always return 1
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private bool _units;
        public bool Units
        {
            get { return _units; }
            set
            {
                SetProperty(ref _units, value);
                AppState.Units = value;
            }
        }

        private string _age;
        [Required(ErrorMessage = "This field is required")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "Minimum 2 chars, max 3 chars")]
        [Range(1, 150, ErrorMessage = "Must be within range 1-150")]
        public string Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }

        private string _heightMajor;
        [Required(ErrorMessage = "This field is required")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Minimum 1 chars, max 5 chars")]        
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
        [Required(ErrorMessage = "This field is required")]
        [StringLength(7, MinimumLength = 1, ErrorMessage = "Minimum 1 chars, max 7 chars")]
        [Range(0.0, 1000.0, ErrorMessage = "Must be within range 0-1000")]
        public string Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }

        private string _waistSize;
        [Required(ErrorMessage = "This field is required")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Minimum 2 chars, max 5 chars")]
        [Range(15.0, 200.0, ErrorMessage = "Must be within range 15-200")]
        public string WaistSize
        {
            get { return _waistSize; }
            set { SetProperty(ref _waistSize, value); }
        }

        private List<string> _pickerSource;
        public List<string> PickerSource
        {
            get { return _pickerSource; }
            set { SetProperty(ref _pickerSource, value); }
        }

        private string _pickerSelectedItem;
        public string PickerSelectedItem
        {
            get { return _pickerSelectedItem; }
            set { SetProperty(ref _pickerSelectedItem, value); }
        }

        private bool _sex;
        public bool Sex
        {
            get { return _sex; }
            set { SetProperty(ref _sex, value); }
        }        

        private string _goalWeight;
        [Required(ErrorMessage = "This field is required")]
        [StringLength(7, MinimumLength = 1, ErrorMessage = "Minimum 1 chars, max 7 chars")]
        [Range(0.0, 1000.0, ErrorMessage = "Must be within range 0-1000")]
        public string GoalWeight
        {
            get { return _goalWeight; }
            set { SetProperty(ref _goalWeight, value); }
        }

        private DateTime _goalDate;
        public DateTime GoalDate
        {
            get { return _goalDate; }
            set { SetProperty(ref _goalDate, value); }
        }

        private DateTime _minDate;
        public DateTime MinDate
        {
            get { return _minDate; }
            set { SetProperty(ref _minDate, value); }
        }

        private double _lastWeight;
        public double LastWeight
        {
            get { return _lastWeight; }
            set { SetProperty(ref _lastWeight, value); }
        }

        private DateTime _lastWeighDate;
        public DateTime LastWeighDate
        {
            get { return _lastWeighDate; }
            set { SetProperty(ref _lastWeighDate, value); }
        }

        private double _initialWeight;
        public double InitialWeight
        {
            get { return _initialWeight; }
            set { SetProperty(ref _initialWeight, value); }
        }

        private DateTime _initialWeighDate;
        public DateTime InitialWeighDate
        {
            get { return _initialWeighDate; }
            set { SetProperty(ref _initialWeighDate, value); }
        }

        private double _BMR;
        public double BMR
        {
            get { return _BMR; }
            set { SetProperty(ref _BMR, value); }
        }

        private double _BMI;
        public double BMI
        {
            get { return _BMI; }
            set { SetProperty(ref _BMI, value); }
        }

        private double _recommendedDailyCaloricIntake;
        public double RecommendedDailyCaloricIntake
        {
            get { return _recommendedDailyCaloricIntake; }
            set { SetProperty(ref _recommendedDailyCaloricIntake, value); }
        }

        private string _bmiCategory;
        public string BMICategory
        {
            get { return _bmiCategory; }
            set { SetProperty(ref _bmiCategory, value); }
        }

        private double _distanceToGoalWeight;
        public double DistanceToGoalWeight
        {
            get { return _distanceToGoalWeight; }
            set { SetProperty(ref _distanceToGoalWeight, value); }
        }

        private int _timeLeftToGoal;
        public int TimeLeftToGoal
        {
            get { return _timeLeftToGoal; }
            set { SetProperty(ref _timeLeftToGoal, value); }
        }

        private string _weightLostToDate;
        public string WeightLostToDate
        {
            get { return _weightLostToDate; }
            set { SetProperty(ref _weightLostToDate, value); }
        }

        private double _requiredCaloricDefecit;
        public double RequiredCaloricDefecit
        {
            get { return _requiredCaloricDefecit; }
            set { SetProperty(ref _requiredCaloricDefecit, value); }
        }

        private double _weightPerDayToMeetGoal;
        public double WeightPerDayToMeetGoal
        {
            get { return _weightPerDayToMeetGoal; }
            set { SetProperty(ref _weightPerDayToMeetGoal, value); }
        }

        private int _daysToAddToMeetMinimum;
        public int DaysToAddToMeetMinimum
        {
            get { return _daysToAddToMeetMinimum; }
            set { SetProperty(ref _daysToAddToMeetMinimum, value); }
        }

        private double _weightPerWeekToMeetGoal;
        public double WeightPerWeekToMeetGoal
        {
            get { return _weightPerWeekToMeetGoal; }
            set { SetProperty(ref _weightPerWeekToMeetGoal, value); }
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
