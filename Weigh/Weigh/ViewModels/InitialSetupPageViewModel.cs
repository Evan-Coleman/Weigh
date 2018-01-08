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

        private SetupInfo _setupInfo;
        public SetupInfo SetupInfo
        {
            get { return _setupInfo; }
            set { SetProperty(ref _setupInfo, value); }
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

            // Initialize app SetupInfo
            SetupInfo = new SetupInfo();
            SetupInfo.MinDate = DateTime.UtcNow.AddDays(10);
            SetupInfo.GoalDate = DateTime.UtcNow.AddDays(180);
            // Setting units to default imperial
            SetupInfo.Units = true;
            // TODO: get rid of hard coded strings!
            PickerSource = new List<string> { AppResources.LowActivityPickItem, AppResources.LightActivityPickItem, AppResources.MediumActivityPickItem, AppResources.HeavyActivityPickItem };
            SetupInfo.PickerSelectedItem = AppResources.LightActivityPickItem;
        }
        #endregion

        #region Methods
        private bool CanExecute()
        {
            return SetupInfo.ValidateProperties();
        }

        private async void SaveInfoAsync()
        {
            Console.WriteLine(App.SetupInfo.Age + "HELLO MANDAODA");
            if (CanExecute() == false)
            {
               UserDialogs.Instance.Alert(AppResources.FormValidationPopupLabel);
            }
            else
            {
                double Weight = Convert.ToDouble(SetupInfo.Weight);
                double WaistSize = Convert.ToDouble(SetupInfo.WaistSize);
                SetupInfoDB setupToDB = new SetupInfoDB(SetupInfo);
                // Remove if not wanted
                // AppState.Name = SetupInfo.Name;

                // Not needed in new SetupInfo model
                /*
                AppState.Sex = SetupInfo.Sex;
                AppState.Age = Convert.ToInt32(SetupInfo.Age);
                AppState.GoalWeight = Convert.ToDouble(SetupInfo.GoalWeight);
                AppState.GoalDate = SetupInfo.GoalDate;

                AppState.HeightMajor = Convert.ToDouble(SetupInfo.HeightMajor);
                AppState.HeightMinor = Convert.ToInt32(SetupInfo.HeightMinor);
                AppState.Weight = Convert.ToDouble(SetupInfo.Weight);
                AppState.WaistSize = Convert.ToDouble(SetupInfo.WaistSize);
                AppState.LastWeight = Convert.ToDouble(SetupInfo.Weight);
                AppState.InitialWeight = Convert.ToDouble(SetupInfo.Weight);

                AppState.LastWeighDate = DateTime.UtcNow;
                AppState.InitialWeightDate = DateTime.UtcNow;
                AppState.Units = SetupInfo.Units;
                AppState.PickerSelectedItem = SetupInfo.PickerSelectedItem;
                */
                
                if (SetupInfo.ValidateGoal() == false)
                {
                    setupToDB.GoalDate = SetupInfo.GoalDate;
                    setupToDB.RequiredCaloricDefecit = SetupInfo.RequiredCaloricDefecit;
                    setupToDB.WeightPerWeekToMeetGoal = SetupInfo.WeightPerWeekToMeetGoal;
                    setupToDB.DaysToAddToMeetMinimum = SetupInfo.DaysToAddToMeetMinimum;
                }
                // Nav using absolute path so user can't hit the back button and come back here
                _newWeight = new WeightEntry();
                _newWeight.Weight = Weight;
                _newWeight.WaistSize = WaistSize;
                _newWeight.WeightDelta = 0;
                await App.Database.SaveWeightAsync(_newWeight);
                await App.Database.NewSetupInfoAsync(setupToDB);

                // Sending the setupinfo to main page
                var p = new NavigationParameters();
                p.Add("SetupInfo", SetupInfo);

                
                //await NavigationService.NavigateAsync("Weigh:///NavigatingAwareTabbedPage");
                await NavigationService.NavigateAsync(
                $"Weigh:///NavigatingAwareTabbedPage?{KnownNavigationParameters.SelectedTab}=MainPage", p);
            }
        }
        #endregion
    }
}
