using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Events;
using Weigh.Events;
using Weigh.Extensions;
using Weigh.Helpers;
using Weigh.Models;
using Weigh.Behaviors;

namespace Weigh.ViewModels
{
    /// <summary>
    /// Page Displays Settings info and allows for editing
    /// 
    /// Inputs:     (AppState.cs)->AppStateInfo, (MainVM)->Weight+Goals
    /// Outputs:    Goals->(MainVM,GraphsVM)
    /// </summary>
    public class SettingsPageViewModel : ViewModelBase
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

        private double _goalWeight;
        public double GoalWeight
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

        public DelegateCommand SaveInfoCommand { get; set; }
        private IEventAggregator _ea;
        #endregion

        #region Constructor
        public SettingsPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            _ea = ea;
            _ea.GetEvent<AddWeightEvent>().Subscribe(HandleNewWeightEntry);
            _ea.GetEvent<NewGoalEvent>().Subscribe(HandleNewGoal);
            Title = "Setup";
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            MinDate = DateTime.UtcNow.AddDays(10);

            GoalWeight = AppState.GoalWeight;
            GoalDate = AppState.GoalDate;
            Name = AppState.Name;
            Sex = AppState.Sex;
            Age = AppState.Age.ToString();
            HeightMajor = AppState.HeightMajor.ToString();
            HeightMinor = AppState.HeightMinor.ToString();
            Weight = AppState.Weight.ToString();
            Units = AppState.Units;
            PickerSelectedItem = AppState.PickerSelectedItem;
            PickerSource = new List<string> { "No Exercise", "Light Exercise", "Moderate Exercise", "Heavy Exercise" };
        }
        #endregion

        #region Methods
        private async void SaveInfoAsync()
        {
            AppState.Name = Name;
            AppState.Sex = Sex;
            AppState.Age = Convert.ToInt32(Age);
            AppState.HeightMajor = Convert.ToDouble(HeightMajor);
            AppState.HeightMinor = Convert.ToInt32(HeightMinor);
            AppState.Weight = Convert.ToDouble(Weight);
            AppState.Units = Units;
            AppState.GoalDate = GoalDate;
            if (GoalValidation.ValidateGoal() == false)
            {
                GoalDate = AppState.GoalDate;
            }
            _ea.GetEvent<NewGoalEvent>().Publish();
            AppState.PickerSelectedItem = PickerSelectedItem;
            await NavigationService.NavigateAsync(
                $"Weigh:///NavigatingAwareTabbedPage?{KnownNavigationParameters.SelectedTab}=MainPage");
        }

        private void HandleNewWeightEntry(WeightEntry weight)
	    {
	        Weight = weight.Weight.ToString();
	    }
        private void HandleNewGoal()
        {
            GoalDate = AppState.GoalDate;
            GoalWeight = AppState.GoalWeight;
        }
        #endregion
    }
}