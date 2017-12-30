using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigh.Events;
using Weigh.Helpers;
using Weigh.Models;
using Weigh.Extensions;
using Weigh.Behaviors;

namespace Weigh.ViewModels
{
    /// <summary>
    /// Page Displays all important information, and allows entry of new weights
    /// 
    /// Inputs:     (AppState.cs)->AppStateInfo
    /// Outputs:    WeightEntry->(Database,AppState.cs,GraphVM), Goals->(SettingsVM)
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        #region Fields
        private double _bmi;
        public double BMI
        {
            get { return _bmi; }
            set { SetProperty(ref _bmi, value); }
        }

        private double _bmr;
        public double BMR
        {
            get { return _bmr; }
            set { SetProperty(ref _bmr, value); }
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

        private DelegateCommand _addWeightToListCommand;
        public DelegateCommand AddWeightToListCommand
        {
            get { return _addWeightToListCommand; }
            set { SetProperty(ref _addWeightToListCommand, value); }
        }

        private double _goalWeight;
        public double GoalWeight
        {
            get { return _goalWeight; }
            set { SetProperty(ref _goalWeight, value); }
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

        private DateTime _goalDate;
        public DateTime GoalDate
        {
            get { return _goalDate; }
            set { SetProperty(ref _goalDate, value); }
        }

        private double _currentWeight;
        public double CurrentWeight
        {
            get { return _currentWeight; }
            set { SetProperty(ref _currentWeight, value); }
        }

        private double _newWeightEntry;
        public double NewWeightEntry
        {
            get { return _newWeightEntry; }
            set { SetProperty(ref _newWeightEntry, value); }
        }
        private bool _buttonEnabled;
        public bool ButtonEnabled
        {
            get { return _buttonEnabled; }
            set { SetProperty(ref _buttonEnabled, value); }
        }

        private WeightEntry _newWeight;
        private IEventAggregator _ea;
        #endregion

        #region Constructor
        public MainPageViewModel(INavigationService navigationService, IEventAggregator ea) 
            : base (navigationService)
        {
            _ea = ea;
            Title = "Main Page";
            ButtonEnabled = true;
            CalculateBMRBMI();
            AddWeightToListCommand = new DelegateCommand(AddWeightToList);
            GoalWeight = AppState.GoalWeight;
            DistanceToGoalWeight = AppState.Weight - GoalWeight;
            TimeLeftToGoal = (AppState.GoalDate - DateTime.UtcNow).Days;
            GoalDate = AppState.GoalDate;
            CurrentWeight = AppState.Weight;
            
        }
        #endregion

        #region Methods
        private void CalculateBMRBMI()
        {
            double Feet = AppState.HeightMajor;
            int Inches = AppState.HeightMinor;
            double Weight = AppState.Weight;

            // Units are metric if false, so do conversion here
            if (AppState.Units == false)
            {
                (Feet, Inches) = AppState.HeightMajor.CentimetersToFeetInches();
                Weight = AppState.Weight.KilogramsToPounds();
            }

            BMI = (Weight / Math.Pow(((Feet * 12) + Inches), 2)) * 703;

            // Categories based on site here: https://www.nhlbi.nih.gov/health/educational/lose_wt/BMI/bmicalc.htm
            if (BMI < 18.5)
            {
                BMICategory = "Underweight";
            }

            if (BMI >= 18.5 && BMI <= 24.9)
            {
                BMICategory = "Normal Weight";
            }

            if (BMI >= 25 && BMI <= 29.9)
            {
                BMICategory = "Overweight";
            }

            if (BMI >= 30)
            {
                BMICategory = "Obese";
            }

            // BMR based on equations at https://en.wikipedia.org/wiki/Harris%E2%80%93Benedict_equation
            // According to http://www.exercise4weightloss.com/bmr-calculator.html
            // -- Go 1000 calories lower than this calculation to lose 2 pounds a week which is the max advisable
            if (AppState.Sex == false)
            {
                BMR = 66 + (6.2 * Weight) + (12.7 * ((Feet * 12) + Inches)) - (6.76 * AppState.Age);
            }
            else
            {
                BMR = 655.1 + (4.35 * Weight) + (4.7 * ((Feet * 12) + Inches)) - (4.7 * AppState.Age);
            }
            if (AppState.PickerSelectedItem == "No Exercise")
            {
                BMR *= 1.2;
            }
            if (AppState.PickerSelectedItem == "Light Exercise")
            {
                BMR *= 1.375;
            }
            if (AppState.PickerSelectedItem == "Moderate Exercise")
            {
                BMR *= 1.55;
            }
            if (AppState.PickerSelectedItem == "Heavy Exercise")
            {
                BMR *= 1.725;
            }

            CalcDailyCaloricIntakeToMeetGoal();
        }

        private void CalcDailyCaloricIntakeToMeetGoal()
        {
            double weightPerWeekToMeetGoal = (AppState.Weight - AppState.GoalWeight) / (AppState.GoalDate - DateTime.UtcNow).TotalDays * 7;
            double RequiredCaloricDefecit = 500 * weightPerWeekToMeetGoal;
            RecommendedDailyCaloricIntake = (int)BMR - RequiredCaloricDefecit;
            if (AppState.Sex == true && RecommendedDailyCaloricIntake < 1200)
            {
                // Min calories/day for women is 1200
                // TODO: Implement something to handle this case
                GoalValidation.ValidateGoal();
                UpdateAfterValidation();
            }
            if (AppState.Sex == false && RecommendedDailyCaloricIntake < 1800)
            {
                // Min calories/day for men is 1800
                // TODO: Implement something to handle this case
                GoalValidation.ValidateGoal();
                UpdateAfterValidation();
            }
        }

        private void UpdateAfterValidation()
        {
            TimeLeftToGoal = (int)(AppState.GoalDate.ToLocalTime() - DateTime.UtcNow.ToLocalTime()).TotalDays;
            GoalDate = AppState.GoalDate;
            double weightPerWeekToMeetGoal = (AppState.Weight - AppState.GoalWeight) / (AppState.GoalDate - DateTime.UtcNow).TotalDays * 7;
            double RequiredCaloricDefecit = 500 * weightPerWeekToMeetGoal;
            RecommendedDailyCaloricIntake = (int)BMR - RequiredCaloricDefecit;
        }

        public async void AddWeightToList()
        {
            ButtonEnabled = false;
            // Disabling the 12hr restriction for now
            if ((AppState.LastWeighDate - DateTime.UtcNow).TotalHours > 11231232313232)
            {
                ButtonEnabled = true;
                // TODO: Add an error message, less than 12 hours since last entry
                return;
            }
            else
            {
                _newWeight = new WeightEntry();
                AppState.LastWeight = AppState.Weight;
                _newWeight.Weight = NewWeightEntry;
                AppState.Weight = _newWeight.Weight;
                CurrentWeight = _newWeight.Weight;
                AppState.LastWeighDate = DateTime.UtcNow;
                CalculateBMRBMI();
                DistanceToGoalWeight = AppState.Weight - GoalWeight;
                GoalValidation.ValidateGoal();
                await App.Database.SaveWeightAsync(_newWeight);
                _ea.GetEvent<AddWeightEvent>().Publish(_newWeight);
            }
            ButtonEnabled = true;
        }
        #endregion
    }
}
