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
        private string _heightFeet;
        public string HeightFeet
        {
            get { return _heightFeet; }
            set { SetProperty(ref _heightFeet, value); }
        }
        private string _heightInches;
        public string HeightInches
        {
            get { return _heightInches; }
            set { SetProperty(ref _heightInches, value); }
        }
        private string _weight;
        public string Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }

        public DelegateCommand SaveInfoCommand { get; set; }
        public DelegateCommand ChangeSexCommand { get; set; }

        public InitialSetupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Setup";
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            ChangeSexCommand = new DelegateCommand(ChangeSex);
            /*
            Name = Settings.Name;
            Sex = Settings.Sex;
            Age = Settings.Age;
            HeightFeet = Settings.HeightFeet;
            HeightInches = Settings.HeightInches;
            Weight = Settings.Weight;            */
        }

        private async void SaveInfoAsync()
        {
            Settings.Name = Name;
            Settings.Sex = Sex;
            Settings.Age = Convert.ToInt32(Age);
            Settings.HeightFeet = Convert.ToInt32(HeightFeet);
            Settings.HeightInches = Convert.ToInt32(HeightInches);
            Settings.Weight = Convert.ToDouble(Weight);
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        public void ChangeSex()
        {
            if (Sex == true)
            {
                Sex = false;
            }
            else
            {
                Sex = true;
            }
        }
    }
}
