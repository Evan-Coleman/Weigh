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

namespace Weigh.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public double BMI { get; set; }
        public double BMR { get; set; }
        public int RecommendedDailyCaloricIntake { get; set; }
        public string BMICategory { get; set; }
        public DelegateCommand AddWeightToListCommand { get; set; }

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
        }

        private void CalculateBMRBMI()
        {
            BMI = (Settings.Weight / Math.Pow(((Settings.HeightMajor * 12) + Settings.HeightMinor), 2)) * 703;

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
            if (Settings.Sex == false)
            {
                BMR = 66 + (6.2 * Settings.Weight) + (12.7 * ((Settings.HeightMajor * 12) + Settings.HeightMinor)) - (6.76 * Settings.Age);
            }
            else
            {
                BMR = 655.1 + (4.35 * Settings.Weight) + (4.7 * ((Settings.HeightMajor * 12) + Settings.HeightMinor)) - (4.7 * Settings.Age);
            }
            if (Settings.PickerSelectedItem == "No Exercise")
            {
                BMR *= 1.2;
            }
            if (Settings.PickerSelectedItem == "Light Exercise")
            {
                BMR *= 1.375;
            }
            if (Settings.PickerSelectedItem == "Moderate Exercise")
            {
                BMR *= 1.55;
            }
            if (Settings.PickerSelectedItem == "Heavy Exercise")
            {
                BMR *= 1.725;
            }
            RecommendedDailyCaloricIntake = (int)BMR - 1000;
        }

        public async void AddWeightToList()
        {
            ButtonEnabled = false;
            _newWeight = new WeightEntry();
            _newWeight.Weight = NewWeightEntry;

            await App.Database.SaveWeightAsync(_newWeight);
            ButtonEnabled = true;
            _ea.GetEvent<AddWeightEvent>().Publish(_newWeight);
        }



    }
}
