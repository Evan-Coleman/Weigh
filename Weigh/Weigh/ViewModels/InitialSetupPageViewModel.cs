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
        private SetupInfo _setupInfo;
        public SetupInfo SetupInfo
        {
            get { return _setupInfo; }
            set { SetProperty(ref _setupInfo, value); }
        }

        public DelegateCommand SaveInfoCommand { get; set; }        

        private WeightEntry _newWeight;
        #endregion

        #region Constructor
        public InitialSetupPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            Title = "Setup";
            SetupInfo = new SetupInfo();
            SetupInfo.MinDate = DateTime.UtcNow.AddDays(10);
            SetupInfo.GoalDate = AppState.GoalDate;
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            // Setting units to default imperial
            SetupInfo.Units = true;
            // TODO: get rid of hard coded strings!
            SetupInfo.PickerSource = new List<string> { "No Exercise", "Light Exercise", "Moderate Exercise", "Heavy Exercise" };
        }
        #endregion

        #region Methods
        private async void SaveInfoAsync()
        {
            AppState.Name = SetupInfo.Name;
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

            GoalValidation.ValidateGoal();

            // Nav using absolute path so user can't hit the back button and come back here
            _newWeight = new WeightEntry();
            _newWeight.Weight = AppState.Weight;
            _newWeight.WaistSize = AppState.WaistSize;
            await App.Database.SaveWeightAsync(_newWeight);
            await NavigationService.NavigateAsync("Weigh:///NavigatingAwareTabbedPage/MainPage");
        }
        #endregion
    }
}
