﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Weigh.Helpers;

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

        public DelegateCommand SaveInfoCommand { get; set; }
        #endregion

        #region Constructor
        public InitialSetupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Setup";
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            // Setting units to default imperial
            Units = true;
        }
        #endregion

        #region Methods
        private async void SaveInfoAsync()
        {
            Settings.Name = Name;
            Settings.Sex = Sex;
            Settings.Age = Convert.ToInt32(Age);
            // units false means metric, so do calculations here.
            if (Units == false)
            {
                double _feet = ((Convert.ToDouble(HeightMajor) / 2.54) / 12);
                int _iFeet = (int)_feet;
                double _inches = (_feet - (double)_iFeet) * 12;
                Settings.HeightMajor = _iFeet;
                Settings.HeightMinor = Convert.ToInt32(_inches);
                Settings.Weight = Convert.ToDouble(Weight) * 2.20462;
            }
            else
            {
                Settings.HeightMajor = Convert.ToInt32(HeightMajor);
                Settings.HeightMinor = Convert.ToInt32(HeightMinor);
                Settings.Weight = Convert.ToDouble(Weight);
            }
            Settings.Units = Units;
            // Nav using absolute path so user can't hit the back button and come back here
            await NavigationService.NavigateAsync("Weigh:///NavigatingAwareTabbedPage/MainPage");
        }
        #endregion

    }
}
