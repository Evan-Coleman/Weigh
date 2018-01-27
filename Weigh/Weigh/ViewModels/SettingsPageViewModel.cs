using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Weigh.Events;
using Weigh.Helpers;
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
            
            BirthDateMinDate = DateTime.UtcNow.ToLocalTime().AddYears(-150);
            BirthDateMaxDate = DateTime.UtcNow.ToLocalTime().AddYears(-1);
            ImperialSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            MaleSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            SettingVals.MinDate = DateTime.UtcNow.ToLocalTime().AddDays(10);
            MaxGoalDate = DateTime.UtcNow.ToLocalTime().AddYears(1);
            PickerSource = new List<string>
            {
                AppResources.LowActivityPickItem,
                AppResources.LightActivityPickItem,
                AppResources.MediumActivityPickItem,
                AppResources.HeavyActivityPickItem
            };
            MaleText = "\uf183  " + AppResources.MaleGenderSwitchLabel;
            FemaleText = "\uf182  " + AppResources.FemaleGenderSwitchLabel;
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

        private string _maleText;
        public string MaleText
        {
            get => _maleText;
            set => SetProperty(ref _maleText, value);
        }

        private string _femaleText;
        public string FemaleText
        {
            get => _femaleText;
            set => SetProperty(ref _femaleText, value);
        }

        #endregion

        #region Methods

        private void SelectImperial()
        {
            MetricSelectedBorderColor = Color.Default;
            ImperialSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            SettingVals.Units = true;
            Settings.Units = true;
        }

        private void SelectMetric()
        {
            MetricSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            ImperialSelectedBorderColor = Color.Default;
            SettingVals.Units = false;
            Settings.Units = false;
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


        private void SaveInfoAsync()
        {
            // TODO: check this out and see what needs changing
            if (SettingValsValidated.ValidateProperties())
            {
                SettingVals.InitializeFromValidated(SettingValsValidated);
                SettingVals.ValidateGoal();
                SettingVals.SaveSettingValsToDevice();

                _ea.GetEvent<UpdateSetupInfoEvent>().Publish(SettingVals);

                UserDialogs.Instance.Toast(new ToastConfig(AppResources.SavedToast)
                                                          .SetPosition(ToastPosition.Top));
            }
        }

        private void HandleNewSetupInfo(SettingVals setupInfo)
        {
            SettingVals = setupInfo;
            SettingValsValidated.InitializeFromSettings(SettingVals);
            SettingVals.MinDate = DateTime.UtcNow.ToLocalTime().AddDays(10);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("SettingVals"))
            {
                SettingVals = (SettingVals)parameters["SettingVals"];
                SettingValsValidated.InitializeFromSettings(SettingVals);
                SettingVals.MinDate = DateTime.UtcNow.ToLocalTime().AddDays(10);
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            SettingVals.PickerSelectedItem = Settings.PickerSelectedItem;
        }

        public override void Destroy()
        {
            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Unsubscribe(HandleNewSetupInfo);
        }

        #endregion
    }
}