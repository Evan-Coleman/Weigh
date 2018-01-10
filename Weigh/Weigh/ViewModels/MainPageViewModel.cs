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
        private double _newWeightEntry;
        public double NewWeightEntry
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

        private double _newWaistSizeEntry;
        public double NewWaistSizeEntry
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
            //_ea.GetEvent<NewGoalEvent>().Subscribe(HandleNewGoal);
            _ea.GetEvent<UpdateSetupInfoEvent>().Subscribe(HandleUpdateSetupInfo);
            Title = AppResources.MainPageTitle;
            SettingValsValidated = new SettingValsValidated();
            ButtonEnabled = true;            
            AddWeightToListCommand = new DelegateCommand(AddWeightToList);
            NewWaistSizeEntry = Settings.WaistSize;
        }
        #endregion

        #region Methods
        public async void AddWeightToList()
        {
            // URGENT TODO: Issue with validation (Fresh install -> Enter info (235lb) -> Enter weight (234lb) = validation fail CHECK INTO IT
            ButtonEnabled = false;
            // Disabling the 12hr restriction for now
            if ((SettingValsValidated.LastWeighDate - DateTime.UtcNow).TotalHours > 11231232313232)
            {
                ButtonEnabled = true;
                // TODO: Add an error message, less than 12 hours since last entry
                return;
            }
            else
            {
                _newWeight = new WeightEntry();
                SettingValsValidated.LastWeight = Convert.ToDouble(SettingValsValidated.Weight);
                _newWeight.WeightDelta = NewWeightEntry - SettingValsValidated.LastWeight;
                _newWeight.Weight = NewWeightEntry;
                _newWeight.WaistSize = NewWaistSizeEntry;
                SettingValsValidated.WaistSize = _newWeight.WaistSize.ToString();
                SettingValsValidated.Weight = _newWeight.Weight.ToString();
                SettingValsValidated.Weight = _newWeight.Weight.ToString();
                SettingValsValidated.WaistSize = _newWeight.WaistSize.ToString();
                SettingValsValidated.LastWeighDate = DateTime.UtcNow;
                SettingValsValidated.DistanceToGoalWeight = Convert.ToDouble(SettingValsValidated.Weight) - Convert.ToDouble(SettingValsValidated.GoalWeight);

                SettingValsValidated.ValidateGoal();
                SettingValsValidated.SaveSettingValsToDevice();
                _ea.GetEvent<AddWeightEvent>().Publish(_newWeight);
                _ea.GetEvent<SendSetupInfoToSettingsEvent>().Publish(SettingValsValidated);
                await App.Database.SaveWeightAsync(_newWeight);
            }
            ButtonEnabled = true;
        }

        private void HandleUpdateSetupInfo(SettingValsValidated _setupInfo)
        {
            SettingValsValidated = _setupInfo;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            SettingValsValidated.InitializeSettingVals();
            
            if (SettingValsValidated.ValidateGoal() == false)
            {
                SettingValsValidated.SaveSettingValsToDevice();
            }
            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Publish(SettingValsValidated);
            _ea.GetEvent<UpdateWaistSizeEnabledToGraphEvent>().Publish(SettingValsValidated.WaistSizeEnabled);
        }
        #endregion
    }
}
