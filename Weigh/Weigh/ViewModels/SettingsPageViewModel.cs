using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Weigh.Extensions;
using Weigh.Helpers;

namespace Weigh.ViewModels
{
	public class SettingsPageViewModel : ViewModelBase
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

        public DelegateCommand SaveInfoCommand { get; set; }

        public SettingsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Setup";
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);

            Name = App.Name;
            Sex = App.Sex;
            Age = App.Age.ToString();
            HeightMajor = App.HeightMajor.ToString();
            HeightMinor = App.HeightMinor.ToString();
            Weight = App.Weight.ToString();
            Units = App.Units;
            PickerSelectedItem = App.PickerSelectedItem;
            PickerSource = new List<string> { "No Exercise", "Light Exercise", "Moderate Exercise", "Heavy Exercise" };
        }

        private async void SaveInfoAsync()
        {
            App.Name = Name;
            App.Sex = Sex;
            App.Age = Convert.ToInt32(Age);
            App.HeightMajor = Convert.ToDouble(HeightMajor);
            App.HeightMinor = Convert.ToInt32(HeightMinor);
            App.Weight = Convert.ToDouble(Weight);
            App.Units = Units;
            App.PickerSelectedItem = PickerSelectedItem;
            await NavigationService.NavigateAsync(
                $"Weigh:///NavigatingAwareTabbedPage?{KnownNavigationParameters.SelectedTab}=MainPage");
        }
    }
}