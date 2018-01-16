using System;
using System.Collections.Generic;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Weigh.Events;
using Weigh.Helpers;
using Weigh.Localization;
using Weigh.Models;

namespace Weigh.ViewModels
{
    /// <summary>
    ///     Page Displays Settings info and allows for editing
    ///     Inputs:     (AppState.cs)->AppStateInfo, (MainVM)->Weight+Goals
    ///     Outputs:    Goals->(MainVM,GraphsVM)
    /// </summary>
    public class SettingsPageViewModel : ViewModelBase
    {
        #region Constructor

        public SettingsPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            SettingValsValidated = new SettingValsValidated();
            _ea = ea;
            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Subscribe(HandleNewSetupInfo);
            Title = AppResources.SettingsPageTitle;
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            SettingVals.MinDate = DateTime.UtcNow.AddDays(10);
            PickerSource = new List<string>
            {
                AppResources.LowActivityPickItem,
                AppResources.LightActivityPickItem,
                AppResources.MediumActivityPickItem,
                AppResources.HeavyActivityPickItem
            };
        }

        #endregion

        #region Fields

        private List<string> _pickerSource;

        public List<string> PickerSource
        {
            get => _pickerSource;
            set => SetProperty(ref _pickerSource, value);
        }

        private SettingValsValidated _settingValsValidated;

        public SettingValsValidated SettingValsValidated
        {
            get => _settingValsValidated;
            set => SetProperty(ref _settingValsValidated, value);
        }

        private SettingVals _settingVals;

        public SettingVals SettingVals
        {
            get => _settingVals;
            set => SetProperty(ref _settingVals, value);
        }

        public DelegateCommand SaveInfoCommand { get; set; }
        private readonly IEventAggregator _ea;

        #endregion

        #region Methods

        private async void SaveInfoAsync()
        {
            // TODO: check this out and see what needs changing
            if (SettingValsValidated.ValidateProperties() == true)
            {
                SettingVals.InitializeFromValidated(SettingValsValidated);
                SettingVals.SaveSettingValsToDevice();
                await NavigationService.NavigateAsync(
                    $"Weigh:///NavigatingAwareTabbedPage?{KnownNavigationParameters.SelectedTab}=MainPage");
            }
        }

        private void HandleNewSetupInfo(SettingVals setupInfo)
        {
            SettingVals = setupInfo;
            SettingVals.MinDate = DateTime.UtcNow.AddDays(10);
            SettingVals.PickerSelectedItem = Settings.PickerSelectedItem;
        }

        #endregion
    }
}