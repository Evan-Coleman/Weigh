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
        private SettingVals _settingVals;
        public SettingVals SettingVals
        {
            get { return _settingVals; }
            set { SetProperty(ref _settingVals, value); }
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
            SettingVals = new SettingVals();
            ButtonEnabled = true;            
            AddWeightToListCommand = new DelegateCommand(AddWeightToList);
            NewWaistSizeEntry = Settings.WaistSize;
        }
        #endregion

        #region Methods
        public async void AddWeightToList()
        {
            ButtonEnabled = false;
            await NavigationService.NavigateAsync("AddEntryPage");
            ButtonEnabled = true;
        }

        private void HandleUpdateSetupInfo(SettingValsValidated _setupInfo)
        {
            SettingVals.InitializeFromValidated(_setupInfo);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            SettingVals.InitializeSettingVals();
            
            if (SettingVals.ValidateGoal() == false)
            {
                SettingVals.SaveSettingValsToDevice();
            }
            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Publish(SettingVals);
            _ea.GetEvent<UpdateWaistSizeEnabledToGraphEvent>().Publish(SettingVals.WaistSizeEnabled);
        }
        #endregion
    }
}
