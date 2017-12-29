﻿using Prism.Commands;
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
    public class MainPageViewModel : ViewModelBase
    {
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

        public MainPageViewModel(INavigationService navigationService, IEventAggregator ea) 
            : base (navigationService)
        {
            _ea = ea;
            Title = "Main Page";
            ButtonEnabled = true;
            CalculateBMRBMI();
            AddWeightToListCommand = new DelegateCommand(AddWeightToList);
            GoalWeight = App.GoalWeight;
            DistanceToGoalWeight = App.Weight - GoalWeight;
            TimeLeftToGoal = (App.GoalDate - DateTime.UtcNow).Days;
            GoalDate = App.GoalDate;
            CurrentWeight = App.Weight;
        }

        private void CalculateBMRBMI()
        {
            double Feet = App.HeightMajor;
            int Inches = App.HeightMinor;
            double Weight = App.Weight;

            // Units are metric if false, so do conversion here
            if (App.Units == false)
            {
                (Feet, Inches) = App.HeightMajor.CentimetersToFeetInches();
                Weight = App.Weight.KilogramsToPounds();
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
            if (App.Sex == false)
            {
                BMR = 66 + (6.2 * Weight) + (12.7 * ((Feet * 12) + Inches)) - (6.76 * App.Age);
            }
            else
            {
                BMR = 655.1 + (4.35 * Weight) + (4.7 * ((Feet * 12) + Inches)) - (4.7 * App.Age);
            }
            if (App.PickerSelectedItem == "No Exercise")
            {
                BMR *= 1.2;
            }
            if (App.PickerSelectedItem == "Light Exercise")
            {
                BMR *= 1.375;
            }
            if (App.PickerSelectedItem == "Moderate Exercise")
            {
                BMR *= 1.55;
            }
            if (App.PickerSelectedItem == "Heavy Exercise")
            {
                BMR *= 1.725;
            }

            CalcDailyCaloricIntakeToMeetGoal();
        }

        private void CalcDailyCaloricIntakeToMeetGoal()
        {
            double weightPerWeekToMeetGoal = (App.Weight - App.GoalWeight) / (App.GoalDate - DateTime.UtcNow).TotalDays * 7;
            double RequiredCaloricDefecit = 500 * weightPerWeekToMeetGoal;
            RecommendedDailyCaloricIntake = (int)BMR - RequiredCaloricDefecit;
            if (App.Sex == true && RecommendedDailyCaloricIntake < 1200)
            {
                // Min calories/day for women is 1200
                // TODO: Implement something to handle this case
                GoalValidation.ValidateGoal();
                UpdateAfterValidation();
            }
            if (App.Sex == false && RecommendedDailyCaloricIntake < 1800)
            {
                // Min calories/day for men is 1800
                // TODO: Implement something to handle this case
                GoalValidation.ValidateGoal();
                UpdateAfterValidation();
            }
        }

        private void UpdateAfterValidation()
        {
            TimeLeftToGoal = (int)(App.GoalDate.ToLocalTime() - DateTime.UtcNow.ToLocalTime()).TotalDays;
            GoalDate = App.GoalDate;
            double weightPerWeekToMeetGoal = (App.Weight - App.GoalWeight) / (App.GoalDate - DateTime.UtcNow).TotalDays * 7;
            double RequiredCaloricDefecit = 500 * weightPerWeekToMeetGoal;
            RecommendedDailyCaloricIntake = (int)BMR - RequiredCaloricDefecit;
        }

        public async void AddWeightToList()
        {
            ButtonEnabled = false;
            // Disabling the 12hr restriction for now
            if ((App.LastWeighDate - DateTime.UtcNow).TotalHours > 11231232313232)
            {
                ButtonEnabled = true;
                // TODO: Add an error message, less than 12 hours since last entry
                return;
            }
            else
            {
                _newWeight = new WeightEntry();
                App.LastWeight = App.Weight;
                _newWeight.Weight = NewWeightEntry;
                App.Weight = _newWeight.Weight;
                CurrentWeight = _newWeight.Weight;
                App.LastWeighDate = DateTime.UtcNow;
                CalculateBMRBMI();
                DistanceToGoalWeight = App.Weight - GoalWeight;
                GoalValidation.ValidateGoal();
                await App.Database.SaveWeightAsync(_newWeight);
                _ea.GetEvent<AddWeightEvent>().Publish(_newWeight);
            }
            ButtonEnabled = true;
        }



    }
}
