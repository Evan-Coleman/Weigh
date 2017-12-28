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

namespace Weigh.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public double BMI { get; set; }
        public double BMR { get; set; }
        public int RecommendedDailyCaloricIntake { get; set; }
        public string BMICategory { get; set; }
        public DelegateCommand AddWeightToListCommand { get; set; }
        public double GoalWeight { get; set; }
        public double DistanceToGoalWeight { get; set; }
        public int TimeLeftToGoal { get; set; }
        public DateTime GoalDate { get; set; }
        public double CurrentWeight { get; set; }

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
                BMICategory = "Underweight";
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
            RecommendedDailyCaloricIntake = (int)BMR - 1000;
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
                await App.Database.SaveWeightAsync(_newWeight);
                _ea.GetEvent<AddWeightEvent>().Publish(_newWeight);
            }
            ButtonEnabled = true;
        }



    }
}
