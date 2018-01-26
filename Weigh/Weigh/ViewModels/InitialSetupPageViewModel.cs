using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Weigh.Helpers;
using Weigh.Localization;
using Weigh.Models;
using Xamarin.Forms;

namespace Weigh.ViewModels
{
    /// <summary>
    ///     Page will Prompt user for all initial data to begin
    ///     Inputs:     None
    ///     Outputs:    WeightEntry->(Database), AppStateInfo->(AppState.cs)
    /// </summary>
    public class InitialSetupPageViewModel : ViewModelBase
    {
        #region Constructor

        public InitialSetupPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            Title = AppResources.InitialSetupPageTitle;
            _ea = ea;

            SettingVals = new SettingVals();
            SettingValsValidated = new SettingValsValidated();

            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            SelectImperialCommand = new DelegateCommand(SelectImperial);
            SelectMetricCommand = new DelegateCommand(SelectMetric);
            SelectMaleCommand = new DelegateCommand(SelectMale);
            SelectFemaleCommand = new DelegateCommand(SelectFemale);

            // Initialize app SettingVals
            Settings.GoalMetNotified = false;
            SettingVals.MinDate = DateTime.UtcNow.ToLocalTime().AddDays(10);
            SettingVals.GoalDate = DateTime.UtcNow.ToLocalTime().AddDays(180);
            SettingVals.BirthDate = DateTime.Parse("2/25/1988");
            //SettingVals.BirthDate = DateTime.UtcNow.ToLocalTime().AddYears(-21);
            BirthDateMinDate = DateTime.UtcNow.ToLocalTime().AddYears(-150);
            BirthDateMaxDate = DateTime.UtcNow.ToLocalTime().AddYears(-1);
            MaxGoalDate = DateTime.UtcNow.ToLocalTime().AddYears(1);

            // Setting units to default imperial
            SettingVals.Units = true;
            SettingVals.WaistSizeEnabled = true;
            SettingVals.PickerSelectedItem = AppResources.LightActivityPickItem;
            ImperialSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            MaleSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
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

        private IEventAggregator _ea;

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
        private WeightEntry _newWeight;

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

        private string _noteEntry;

        public string NoteEntry
        {
            get => _noteEntry;
            set => SetProperty(ref _noteEntry, value);
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

        private bool CanExecute()
        {
            return SettingValsValidated.ValidateProperties();
        }

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
            if (SettingValsValidated.WaistSize == "" && SettingVals.WaistSizeEnabled == false)
            {
                SettingValsValidated.WaistSize = "0";
            }
            SettingValsValidated.Age = ((DateTime.UtcNow.ToLocalTime() - SettingVals.BirthDate).Days / 365).ToString();
            if (CanExecute() == false)
            {
                UserDialogs.Instance.Alert(AppResources.FormValidationPopupLabel);
            }
            else
            {

                // Nav using absolute path so user can't hit the back button and come back here
                _newWeight = new WeightEntry
                {
                    Weight = Convert.ToDouble(SettingValsValidated.Weight),
                    WaistSize = Convert.ToDouble(SettingValsValidated.WaistSize),
                    WeightDelta = 0,
                    Note = NoteEntry
                };
                await App.Database.SaveWeightAsync(_newWeight);
                // Pulling in vals from validated model
                SettingVals.InitializeFromValidated(SettingValsValidated);
                SettingVals.InitialWeight = Convert.ToDouble(SettingValsValidated.Weight);
                SettingVals.InitialWeighDate = DateTime.UtcNow.ToLocalTime();
                SettingVals.LastWeight = SettingVals.InitialWeight;
                SettingVals.LastWeighDate = DateTime.UtcNow.ToLocalTime();
                SettingVals.ValidateGoal();
                SettingVals.SaveSettingValsToDevice();
                Settings.FirstUse = "no";
                //await NavigationService.NavigateAsync("Weigh:///NavigatingAwareTabbedPage");
                await NavigationService.NavigateAsync(
                    $"Weigh:///NavigatingAwareTabbedPage?{KnownNavigationParameters.SelectedTab}=MainPage");
            }
        }

        #endregion
    }
}