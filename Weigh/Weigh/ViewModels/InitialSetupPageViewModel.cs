using Prism.Commands;
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

        public InitialSetupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Setup";
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            
            Name = Settings.Name;
            Sex = Settings.Sex;
            Age = Settings.Age.ToString();
            HeightMajor = Settings.HeightMajor.ToString();
            HeightMinor = Settings.HeightMinor.ToString();
            Weight = Settings.Weight.ToString();
            Units = Settings.Units;
        }

        private async void SaveInfoAsync()
        {
            Settings.Name = Name;
            Settings.Sex = Sex;
            Settings.Age = Convert.ToInt32(Age);
            Settings.HeightMajor = Convert.ToInt32(HeightMajor);
            Settings.HeightMinor = Convert.ToInt32(HeightMinor);
            Settings.Weight = Convert.ToDouble(Weight);
            Settings.Units = Units;
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

    }
}
