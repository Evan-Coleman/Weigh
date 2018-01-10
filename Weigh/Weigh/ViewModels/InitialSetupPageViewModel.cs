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
        IEventAggregator _ea;
        #endregion

        #region Constructor
        public InitialSetupPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            _ea = ea;
            Title = AppResources.InitialSetupPageTitle;
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);

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
