﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Weigh.Helpers;
using Weigh.Models;
using Weigh.Extensions;

namespace Weigh.ViewModels
{
	public class InitialSetupPageViewModel : ViewModelBase
	{
        #region Fields
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private bool _sex;
        public bool Sex
        {
            get { return _sex; }
            set { SetProperty(ref _sex, value); }
        }
        private string _age;
        public string Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }
        private string _heightMajor;
        public string HeightMajor
        {
            get { return _heightMajor; }
            set { SetProperty(ref _heightMajor, value); }
        }
        private string _heightMinor;
        public string HeightMinor
        {
            get { return _heightMinor; }
            set { SetProperty(ref _heightMinor, value); }
        }
        private string _weight;
        public string Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }
        private bool _units;
        public bool Units
        {
            get { return _units; }
            set { SetProperty(ref _units, value); }
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

        private string _goalWeight;
        public string GoalWeight
        {
            get { return _goalWeight; }
            set { SetProperty(ref _goalWeight, value); }
        }

        private string _goalDate;
        public string GoalDate
        {
            get { return _goalDate; }
            set { SetProperty(ref _goalDate, value); }
        }

        public DelegateCommand SaveInfoCommand { get; set; }

        private WeightEntry _newWeight;
        #endregion

        #region Constructor
        public InitialSetupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Setup";
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            // Setting units to default imperial
            Units = true;
            // TODO: get rid of hard coded strings!
            PickerSource = new List<string> { "No Exercise", "Light Exercise", "Moderate Exercise", "Heavy Exercise" };
        }
        #endregion

        #region Methods
        private async void SaveInfoAsync()
        {
            
            Settings.Name = Name;
            Settings.Sex = Sex;
            Settings.Age = Convert.ToInt32(Age);
            Settings.GoalWeight = Convert.ToDouble(GoalWeight);
            Settings.GoalDate = DateTime.UtcNow.AddDays(Convert.ToDouble(GoalDate));

            Settings.HeightMajor = Convert.ToDouble(HeightMajor);
            Settings.HeightMinor = Convert.ToInt32(HeightMinor);
            Settings.Weight = Convert.ToDouble(Weight);
            Settings.LastWeight = Convert.ToDouble(Weight);
            Settings.InitialWeight = Convert.ToDouble(Weight);
            
            Settings.LastWeighDate = DateTime.UtcNow;
            Settings.InitialWeightDate = DateTime.UtcNow;
            Settings.Units = Units;
            Settings.PickerSelectedItem = PickerSelectedItem;
            // Nav using absolute path so user can't hit the back button and come back here
            _newWeight = new WeightEntry();
            _newWeight.Weight = Settings.Weight;
            await App.Database.SaveWeightAsync(_newWeight);
            await NavigationService.NavigateAsync("Weigh:///NavigatingAwareTabbedPage/MainPage");
        }
        #endregion

    }
}
