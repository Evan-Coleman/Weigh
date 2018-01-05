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
using Weigh.Validation;

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
        private SetupInfo _setupInfo;
        public SetupInfo SetupInfo
        {
            get { return _setupInfo; }
            set { SetProperty(ref _setupInfo, value); }
        }

        public DelegateCommand SaveInfoCommand { get; set; }
        private IEventAggregator _ea;
        #endregion

        #region Constructor
        public SettingsPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            SetupInfo = new SetupInfo();
            _ea = ea;
            _ea.GetEvent<AddWeightEvent>().Subscribe(HandleNewWeightEntry);
            _ea.GetEvent<NewGoalEvent>().Subscribe(HandleNewGoal);
            Title = "Setup";
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            SetupInfo.MinDate = DateTime.UtcNow.AddDays(10);

            SetupInfo.GoalWeight = AppState.GoalWeight.ToString();
            SetupInfo.GoalDate = AppState.GoalDate;
            SetupInfo.Sex = AppState.Sex;
            SetupInfo.Age = AppState.Age.ToString();
            SetupInfo.HeightMajor = AppState.HeightMajor.ToString();
            SetupInfo.HeightMinor = AppState.HeightMinor.ToString();
            SetupInfo.Weight = AppState.Weight.ToString();
            SetupInfo.WaistSize = AppState.WaistSize.ToString();
            SetupInfo.Units = AppState.Units;
            SetupInfo.PickerSelectedItem = AppState.PickerSelectedItem;
            SetupInfo.PickerSource = new List<string> { "No Exercise", "Light Exercise", "Moderate Exercise", "Heavy Exercise" };
        }
        #endregion

        #region Methods
        private async void SaveInfoAsync()
        {
            AppState.Sex = SetupInfo.Sex;
            AppState.Age = Convert.ToInt32(SetupInfo.Age);
            AppState.HeightMajor = Convert.ToDouble(SetupInfo.HeightMajor);
            AppState.HeightMinor = Convert.ToInt32(SetupInfo.HeightMinor);
            AppState.Weight = Convert.ToDouble(SetupInfo.Weight);
            AppState.Units = SetupInfo.Units;
            AppState.GoalDate = SetupInfo.GoalDate;
            AppState.WaistSize = Convert.ToDouble(SetupInfo.WaistSize);
            /*
             if (GoalValidation.ValidateGoala() == false)
            {
                SetupInfo.GoalDate = AppState.GoalDate;
            }
            */
            _ea.GetEvent<NewGoalEvent>().Publish();
            AppState.PickerSelectedItem = SetupInfo.PickerSelectedItem;
            await NavigationService.NavigateAsync(
                $"Weigh:///NavigatingAwareTabbedPage?{KnownNavigationParameters.SelectedTab}=MainPage");
        }

        private void HandleNewWeightEntry(WeightEntry weight)
	    {
            SetupInfo.Weight = weight.Weight.ToString();
            SetupInfo.WaistSize = weight.WaistSize.ToString();
	    }
        private void HandleNewGoal()
        {
            SetupInfo.GoalDate = AppState.GoalDate;
            SetupInfo.GoalWeight = AppState.GoalWeight.ToString();
        }
        #endregion
    }
}