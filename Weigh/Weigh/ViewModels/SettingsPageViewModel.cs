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
using Weigh.Localization;

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
        private List<string> _pickerSource;
        public List<string> PickerSource
        {
            get { return _pickerSource; }
            set { SetProperty(ref _pickerSource, value); }
        }

        private SettingValsValidated _settingValsValidated;
        public SettingValsValidated SettingValsValidated
        {
            get { return _settingValsValidated; }
            set { SetProperty(ref _settingValsValidated, value); }
        }

        public DelegateCommand SaveInfoCommand { get; set; }
        private IEventAggregator _ea;
        #endregion

        #region Constructor
        public SettingsPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            SettingValsValidated = new SettingValsValidated();
            _ea = ea;
            _ea.GetEvent<NewGoalEvent>().Subscribe(HandleNewGoal);
            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Subscribe(HandleNewSetupInfo);
            Title = AppResources.SettingsPageTitle;
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            SettingValsValidated.MinDate = DateTime.UtcNow.AddDays(10);
            PickerSource = new List<string> { AppResources.LowActivityPickItem, AppResources.LightActivityPickItem, AppResources.MediumActivityPickItem, AppResources.HeavyActivityPickItem };
        }
        #endregion

        #region Methods
        private async void SaveInfoAsync()
        {
            SettingValsValidated.ValidateGoal();
            SettingValsValidated.SaveSettingValsToDevice();
            var p = new NavigationParameters();
            p.Add("SettingValsValidated", SettingValsValidated);

            await NavigationService.NavigateAsync(
                $"Weigh:///NavigatingAwareTabbedPage?{KnownNavigationParameters.SelectedTab}=MainPage", p);
        }

        private void HandleNewGoal()
        {
            SettingValsValidated.GoalDate = Settings.GoalDate;
            SettingValsValidated.GoalWeight = Settings.GoalWeight.ToString();
        }
        private void HandleNewSetupInfo(SettingValsValidated _setupInfo)
        {
            SettingValsValidated = _setupInfo;
        }
        #endregion
    }
}