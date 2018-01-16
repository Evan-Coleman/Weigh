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
            _ea = ea;
            Title = AppResources.InitialSetupPageTitle;
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            SelectImperialCommand = new DelegateCommand(SelectImperial);
            SelectMetricCommand = new DelegateCommand(SelectMetric);
            SelectMaleCommand = new DelegateCommand(SelectMale);
            SelectFemaleCommand = new DelegateCommand(SelectFemale);
            ImperialSelectedBorderColor = (Color)Application.Current.Resources["ButtonSelected"];
            MaleSelectedBorderColor = (Color)Application.Current.Resources["ButtonSelected"];
            // Initialize app SettingVals
            SettingVals = new SettingVals();
            SettingValsValidated = new SettingValsValidated();
            SettingVals.MinDate = DateTime.UtcNow.AddDays(10);
            SettingVals.GoalDate = DateTime.UtcNow.AddDays(180);
            SettingVals.BirthDate = DateTime.Parse("2/25/1988");
            //SettingVals.BirthDate = DateTime.UtcNow.AddYears(-21);
            BirthDateMinDate = DateTime.UtcNow.AddYears(-150);
            BirthDateMaxDate = DateTime.UtcNow.AddYears(-1);
            // Setting units to default imperial
            SettingVals.Units = true;
            SettingVals.WaistSizeEnabled = true;
            SettingVals.PickerSelectedItem = AppResources.LightActivityPickItem;
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

        public DelegateCommand SaveInfoCommand { get; set; }
        public DelegateCommand SelectImperialCommand { get; set; }
        public DelegateCommand SelectMetricCommand { get; set; }
        public DelegateCommand SelectMaleCommand { get; set; }
        public DelegateCommand SelectFemaleCommand { get; set; }

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

        private List<string> _pickerSource;

        public List<string> PickerSource
        {
            get => _pickerSource;
            set => SetProperty(ref _pickerSource, value);
        }

        private WeightEntry _newWeight;

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

        private string _noteEntry;

        public string NoteEntry
        {
            get => _noteEntry;
            set => SetProperty(ref _noteEntry, value);
        }

        private DateTime _birthDateMinDate;
        public DateTime BirthDateMinDate
        {
            get { return _birthDateMinDate; }
            set { SetProperty(ref _birthDateMinDate, value); }
        }

        private DateTime _birthDateMaxDate;
        public DateTime BirthDateMaxDate
        {
            get { return _birthDateMaxDate; }
            set { SetProperty(ref _birthDateMaxDate, value); }
        }

        private IEventAggregator _ea;

        #endregion

        #region Methods

        private bool CanExecute()
        {
            return SettingValsValidated.ValidateProperties();
        }

        private void SelectImperial()
        {
            MetricSelectedBorderColor = Color.Default;
            ImperialSelectedBorderColor = (Color)Application.Current.Resources["ButtonSelected"];
            SettingVals.Units = true;
        }

        private void SelectMetric()
        {
            MetricSelectedBorderColor = (Color)Application.Current.Resources["ButtonSelected"];
            ImperialSelectedBorderColor = Color.Default;
            SettingVals.Units = false;
        }

        private void SelectMale()
        {
            MaleSelectedBorderColor = (Color)Application.Current.Resources["ButtonSelected"];
            FemaleSelectedBorderColor = Color.Default;
            SettingVals.Sex = false;
        }

        private void SelectFemale()
        {
            MaleSelectedBorderColor = Color.Default;
            FemaleSelectedBorderColor = (Color)Application.Current.Resources["ButtonSelected"];
            SettingVals.Sex = true;
        }

        private async void SaveInfoAsync()
        {
            SettingValsValidated.Age = ((DateTime.UtcNow - SettingVals.BirthDate).Days / 365).ToString();
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
                SettingVals.InitialWeighDate = DateTime.UtcNow;
                SettingVals.LastWeight = SettingVals.InitialWeight;
                SettingVals.LastWeighDate = DateTime.UtcNow;
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