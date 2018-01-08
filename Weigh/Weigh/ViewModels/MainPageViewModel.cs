using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigh.Events;
using Weigh.Helpers;
using Weigh.Models;
using Weigh.Extensions;
using Weigh.Behaviors;
using Weigh.Validation;
using Weigh.Localization;
using System.Threading.Tasks;

namespace Weigh.ViewModels
{
    /// <summary>
    /// Page Displays all important information, and allows entry of new weights
    /// 
    /// Inputs:     (AppState.cs)->AppStateInfo
    /// Outputs:    WeightEntry->(Database,AppState.cs,GraphVM), Goals->(SettingsVM)
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        #region Fields
        private DelegateCommand _addWeightToListCommand;
        public DelegateCommand AddWeightToListCommand
        {
            get { return _addWeightToListCommand; }
            set { SetProperty(ref _addWeightToListCommand, value); }
        }
        private string _newWeightEntry;
        public string NewWeightEntry
        {
            get { return _newWeightEntry; }
            set { SetProperty(ref _newWeightEntry, value); }
        }
        private bool _buttonEnabled;
        public bool ButtonEnabled
        {
            get { return _buttonEnabled; }
            set { SetProperty(ref _buttonEnabled, value); }
        }

        private string _newWaistSizeEntry;
        public string NewWaistSizeEntry
        {
            get { return _newWaistSizeEntry; }
            set { SetProperty(ref _newWaistSizeEntry, value); }
        }
        private SettingValsValidated _settingValsValidated;
        public SettingValsValidated SettingValsValidated
        {
            get { return _settingValsValidated; }
            set { SetProperty(ref _settingValsValidated, value); }
        }
        private WeightEntry _newWeight;
        private IEventAggregator _ea;
        #endregion

        #region Constructor
        public MainPageViewModel(INavigationService navigationService, IEventAggregator ea) 
            : base (navigationService)
        {
            _ea = ea;
            _ea.GetEvent<NewGoalEvent>().Subscribe(HandleNewGoal);
            _ea.GetEvent<UpdateSetupInfoEvent>().Subscribe(HandleUpdateSetupInfo);
            Title = AppResources.MainPageTitle;
            SettingValsValidated = new SettingValsValidated();
            ButtonEnabled = true;
            
            AddWeightToListCommand = new DelegateCommand(AddWeightToList);



        }
        #endregion

        #region Methods
        /*
        /// <summary>
        /// Gets all info from DB on initialization and sends it to subscribers
        /// </summary>
        private async void GetSetupInfoFromDatabase()
        {
            List<SettingVals> setupFromDB = new List<SettingVals>();
            setupFromDB = await App.Database.GetSetupInfoasync();
            SetupInfo = new SettingValsValidated(setupFromDB[0]);
            if (SetupInfo.ValidateGoal() == false)
            {
                UpdateAfterValidation();
            }
            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Publish(SetupInfo);

            return;
        }
        */

            /*
        private void UpdateAfterValidation()
        {
            SetupInfo.DistanceToGoalWeight = Convert.ToDouble(SetupInfo.Weight) - Convert.ToDouble(SetupInfo.GoalWeight);
            SetupInfo.TimeLeftToGoal = (SetupInfo.GoalDate - DateTime.UtcNow).Days;
            SetupInfo.WeightLostToDate = (Convert.ToDouble(SetupInfo.InitialWeight) - Convert.ToDouble(SetupInfo.Weight)).ToString();

            /*
            SetupInfo.TimeLeftToGoal = (int)(AppState.GoalDate.ToLocalTime() - DateTime.UtcNow.ToLocalTime()).TotalDays;
            SetupInfo.GoalDate = AppState.GoalDate;
            double weightPerWeekToMeetGoal = (AppState.Weight - AppState.GoalWeight) / (AppState.GoalDate - DateTime.UtcNow).TotalDays * 7;
            double RequiredCaloricDefecit = 500 * weightPerWeekToMeetGoal;
            SetupInfo.RecommendedDailyCaloricIntake = (int)SetupInfo.BMR - RequiredCaloricDefecit;
            // end linecomment here
        }
    */

        private void GetSetupInfoFromSettingsStore()
        {
            SettingValsValidated.InitializeSettingVals();
        }

        public async void AddWeightToList()
        {
            ButtonEnabled = false;
            // Disabling the 12hr restriction for now
            if ((AppState.LastWeighDate - DateTime.UtcNow).TotalHours > 11231232313232)
            {
                ButtonEnabled = true;
                // TODO: Add an error message, less than 12 hours since last entry
                return;
            }
            else
            {
                _newWeight = new WeightEntry();
                SetupInfo.LastWeight = Convert.ToDouble(SetupInfo.Weight);
                _newWeight.WeightDelta = SetupInfo.LastWeight - Convert.ToDouble(NewWeightEntry);
                _newWeight.Weight = Convert.ToDouble(NewWeightEntry);
                _newWeight.WaistSize = Convert.ToDouble(NewWaistSizeEntry);
                SetupInfo.WaistSize = _newWeight.WaistSize.ToString();
                SetupInfo.Weight = _newWeight.Weight.ToString();
                SetupInfo.Weight = _newWeight.Weight.ToString();
                SetupInfo.WaistSize = _newWeight.WaistSize.ToString();
                SetupInfo.LastWeighDate = DateTime.UtcNow;
                SetupInfo.DistanceToGoalWeight = Convert.ToDouble(SetupInfo.Weight) - Convert.ToDouble(SetupInfo.GoalWeight);

                if (SetupInfo.ValidateGoal() == false)
                {
                    UpdateAfterValidation();
                }
                _ea.GetEvent<AddWeightEvent>().Publish(_newWeight);
                _ea.GetEvent<SendSetupInfoToSettingsEvent>().Publish(SetupInfo);
                await App.Database.SaveWeightAsync(_newWeight);
                await App.Database.SaveSetupInfoAsync(Helpers.SettingVals);
            }
            ButtonEnabled = true;
            SetupInfo.WeightLostToDate = (Convert.ToDouble(SetupInfo.InitialWeight) - Convert.ToDouble(SetupInfo.Weight)).ToString();
        }

        private void HandleNewGoal()
        {
            //GoalDate = AppState.GoalDate;
            //GoalWeight = AppState.GoalWeight;
        }

        private void HandleUpdateSetupInfo(SettingValsValidated _setupInfo)
        {
            SetupInfo = _setupInfo;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("SettingValsValidated"))
            {
                SettingValsValidated = (SettingValsValidated)parameters["SettingValsValidated"];
            }
            else
            {
                GetSetupInfoFromSettingsStore();
            }
            if (SettingValsValidated.ValidateGoal() == false)
            {
                //UpdateAfterValidation();
            }
            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Publish(SettingValsValidated);
        }
        #endregion
    }
}
