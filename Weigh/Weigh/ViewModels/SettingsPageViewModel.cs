using System;
using System.Collections.Generic;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Weigh.Events;
using Weigh.Localization;
using Weigh.Models;
using Xamarin.Forms;

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
            Title = AppResources.SettingsPageTitle;
            _ea = ea;

            SettingVals = new SettingVals();
            SettingValsValidated = new SettingValsValidated();

            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            SelectImperialCommand = new DelegateCommand(SelectImperial);
            SelectMetricCommand = new DelegateCommand(SelectMetric);
            SelectMaleCommand = new DelegateCommand(SelectMale);
            SelectFemaleCommand = new DelegateCommand(SelectFemale);

            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Subscribe(HandleNewSetupInfo);
            BirthDateMinDate = DateTime.UtcNow.AddYears(-150);
            BirthDateMaxDate = DateTime.UtcNow.AddYears(-1);
            ImperialSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            MaleSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            SettingVals.MinDate = DateTime.UtcNow.AddDays(10);
            MaxGoalDate = DateTime.UtcNow.AddYears(1);
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

        private readonly IEventAggregator _ea;

        private SettingVals _settingVals;

        public SettingVals SettingVals
        {
            get => _settingVals;
            set => SetProperty(ref _settingVals, value);
        }

        private SettingValsValidated _settingValsValidated;

        public SettingValsValidated SettingValsValidated
        {
            get => _settingValsValidated;
            set => SetProperty(ref _settingValsValidated, value);
        }

        public DelegateCommand SaveInfoCommand { get; set; }
        public DelegateCommand SelectImperialCommand { get; set; }
        public DelegateCommand SelectMetricCommand { get; set; }
        public DelegateCommand SelectMaleCommand { get; set; }
        public DelegateCommand SelectFemaleCommand { get; set; }


        private List<string> _pickerSource;

        public List<string> PickerSource
        {
            get => _pickerSource;
            set => SetProperty(ref _pickerSource, value);
        }

        private Color _metricSelectedBorderColor;

        public Color MetricSelectedBorderColor
        {
            get => _metricSelectedBorderColor;
            set => SetProperty(ref _metricSelectedBorderColor, value);
        }

        private Color _imperialSelectedBorderColor;

        public Color ImperialSelectedBorderColor
        {
            get => _imperialSelectedBorderColor;
            set => SetProperty(ref _imperialSelectedBorderColor, value);
        }

        private Color _maleSelectedBorderColor;

        public Color MaleSelectedBorderColor
        {
            get => _maleSelectedBorderColor;
            set => SetProperty(ref _maleSelectedBorderColor, value);
        }

        private Color _femaleSelectedBorderColor;

        public Color FemaleSelectedBorderColor
        {
            get => _femaleSelectedBorderColor;
            set => SetProperty(ref _femaleSelectedBorderColor, value);
        }

        private DateTime _birthDateMinDate;

        public DateTime BirthDateMinDate
        {
            get => _birthDateMinDate;
            set => SetProperty(ref _birthDateMinDate, value);
        }

        private DateTime _birthDateMaxDate;

        public DateTime BirthDateMaxDate
        {
            get => _birthDateMaxDate;
            set => SetProperty(ref _birthDateMaxDate, value);
        }

        private DateTime _maxGoalDate;

        public DateTime MaxGoalDate
        {
            get => _maxGoalDate;
            set => SetProperty(ref _maxGoalDate, value);
        }

        #endregion

        #region Methods

        private void SelectImperial()
        {
            MetricSelectedBorderColor = Color.Default;
            ImperialSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            SettingVals.Units = true;
        }

        private void SelectMetric()
        {
            MetricSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            ImperialSelectedBorderColor = Color.Default;
            SettingVals.Units = false;
        }

        private void SelectMale()
        {
            MaleSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            FemaleSelectedBorderColor = Color.Default;
            SettingVals.Sex = false;
        }

        private void SelectFemale()
        {
            MaleSelectedBorderColor = Color.Default;
            FemaleSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            SettingVals.Sex = true;
        }


        private async void SaveInfoAsync()
        {
            // TODO: check this out and see what needs changing
            if (SettingValsValidated.ValidateProperties())
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
            SettingValsValidated.InitializeFromSettings(SettingVals);
            SettingVals.MinDate = DateTime.UtcNow.AddDays(10);
        }

        #endregion
    }
}