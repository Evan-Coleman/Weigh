using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Weigh.Helpers;
using Weigh.Models;
using Weigh.Extensions;
using Weigh.Behaviors;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Events;
using Acr.UserDialogs;
using System.Threading.Tasks;
using Weigh.Validation;
using Weigh.Events;
using Weigh.Localization;

namespace Weigh.ViewModels
{
    /// <summary>
    /// Page will Prompt user for all initial data to begin
    /// 
    /// Inputs:     None
    /// Outputs:    WeightEntry->(Database), AppStateInfo->(AppState.cs)
    /// </summary>
    public class InitialSetupPageViewModel : ViewModelBase
	{
        #region Fields      
        public DelegateCommand SaveInfoCommand { get; set; }
        public DelegateCommand SelectImperialCommand { get; set; }
        public DelegateCommand SelectMetricCommand { get; set; }
        public DelegateCommand SelectMaleCommand { get; set; }
        public DelegateCommand SelectFemaleCommand { get; set; }

        private Color _metricSelectedBorderColor;
        public Color MetricSelectedBorderColor
        {
            get { return _metricSelectedBorderColor; }
            set { SetProperty(ref _metricSelectedBorderColor, value); }
        }

        private Color _imperialSelectedBorderColor;
        public Color ImperialSelectedBorderColor
        {
            get { return _imperialSelectedBorderColor; }
            set { SetProperty(ref _imperialSelectedBorderColor, value); }
        }

        private Color _maleSelectedBorderColor;
        public Color MaleSelectedBorderColor
        {
            get { return _maleSelectedBorderColor; }
            set { SetProperty(ref _maleSelectedBorderColor, value); }
        }

        private Color _femaleSelectedBorderColor;
        public Color FemaleSelectedBorderColor
        {
            get { return _femaleSelectedBorderColor; }
            set { SetProperty(ref _femaleSelectedBorderColor, value); }
        }

        private List<string> _pickerSource;
        public List<string> PickerSource
        {
            get { return _pickerSource; }
            set { SetProperty(ref _pickerSource, value); }
        }

        private WeightEntry _newWeight;

        private SettingValsValidated _settingValsValidated;
        public SettingValsValidated SettingValsValidated
        {
            get { return _settingValsValidated; }
            set { SetProperty(ref _settingValsValidated, value); }
        }

        private string _noteEntry;
        public string NoteEntry
        {
            get { return _noteEntry; }
            set { SetProperty(ref _noteEntry, value); }
        }
        IEventAggregator _ea;
        #endregion

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
            ImperialSelectedBorderColor = Color.LightBlue;
            MaleSelectedBorderColor = Color.LightBlue;
            // Initialize app SettingVals
            SettingValsValidated = new SettingValsValidated();
            SettingValsValidated.MinDate = DateTime.UtcNow.AddDays(10);
            SettingValsValidated.GoalDate = DateTime.UtcNow.AddDays(180);
            // Setting units to default imperial
            SettingValsValidated.Units = true;
            SettingValsValidated.WaistSizeEnabled = true;
            SettingValsValidated.PickerSelectedItem = AppResources.LightActivityPickItem;
            PickerSource = new List<string> { AppResources.LowActivityPickItem, AppResources.LightActivityPickItem, AppResources.MediumActivityPickItem, AppResources.HeavyActivityPickItem };
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
            ImperialSelectedBorderColor = Color.LightBlue;
            SettingValsValidated.Units = true;
        }

        private void SelectMetric()
        {
            MetricSelectedBorderColor = Color.LightBlue;
            ImperialSelectedBorderColor = Color.Default;
            SettingValsValidated.Units = false;
        }

        private void SelectMale()
        {
            MaleSelectedBorderColor = Color.LightBlue;
            FemaleSelectedBorderColor = Color.Default;
            SettingValsValidated.Sex = false;
        }

        private void SelectFemale()
        {
            MaleSelectedBorderColor = Color.Default;
            FemaleSelectedBorderColor = Color.LightBlue;
            SettingValsValidated.Sex = true;
        }

        private async void SaveInfoAsync()
        {
            if (CanExecute() == false)
            {
               UserDialogs.Instance.Alert(AppResources.FormValidationPopupLabel);
            }
            else
            {
                // Nav using absolute path so user can't hit the back button and come back here
                _newWeight = new WeightEntry();
                _newWeight.Weight = Convert.ToDouble(SettingValsValidated.Weight);
                _newWeight.WaistSize = Convert.ToDouble(SettingValsValidated.WaistSize);
                _newWeight.WeightDelta = 0;
                _newWeight.Note = NoteEntry;
                await App.Database.SaveWeightAsync(_newWeight);
                SettingValsValidated.InitialWeight = Convert.ToDouble(SettingValsValidated.Weight);
                SettingValsValidated.InitialWeighDate = DateTime.UtcNow;
                SettingValsValidated.LastWeight = SettingValsValidated.InitialWeight;
                SettingValsValidated.LastWeighDate = DateTime.UtcNow;
                SettingValsValidated.ValidateGoal();
                SettingValsValidated.SaveSettingValsToDevice();
                Settings.FirstUse = "no";
                //await NavigationService.NavigateAsync("Weigh:///NavigatingAwareTabbedPage");
                await NavigationService.NavigateAsync(
                $"Weigh:///NavigatingAwareTabbedPage?{KnownNavigationParameters.SelectedTab}=MainPage");
            }
        }
        #endregion
    }
}
