using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Weigh.Events;
using Weigh.Helpers;
using Weigh.Localization;
using Weigh.Models;

namespace Weigh.ViewModels
{
    /// <summary>
    ///     Page Displays all important information, and allows entry of new weights
    ///     Inputs:     (AppState.cs)->AppStateInfo
    ///     Outputs:    WeightEntry->(Database,AppState.cs,GraphVM), Goals->(SettingsVM)
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        #region Constructor

        public MainPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
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

        #region Fields

        private DelegateCommand _addWeightToListCommand;

        public DelegateCommand AddWeightToListCommand
        {
            get => _addWeightToListCommand;
            set => SetProperty(ref _addWeightToListCommand, value);
        }

        private bool _buttonEnabled;

        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set => SetProperty(ref _buttonEnabled, value);
        }

        private double _newWaistSizeEntry;

        public double NewWaistSizeEntry
        {
            get => _newWaistSizeEntry;
            set => SetProperty(ref _newWaistSizeEntry, value);
        }

        private SettingVals _settingVals;

        public SettingVals SettingVals
        {
            get => _settingVals;
            set => SetProperty(ref _settingVals, value);
        }

        private readonly IEventAggregator _ea;

        #endregion

        #region Methods

        public async void AddWeightToList()
        {
            ButtonEnabled = false;
            await NavigationService.NavigateAsync("AddEntryPage");
            ButtonEnabled = true;
        }

        private void HandleUpdateSetupInfo(SettingValsValidated setupInfoValidated)
        {
            SettingVals.InitializeFromValidated(setupInfoValidated);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            SettingVals.InitializeSettingVals();

            if (SettingVals.ValidateGoal() == false)
                SettingVals.SaveSettingValsToDevice();
            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Publish(SettingVals);
            // TODO: Possibly remove
            _ea.GetEvent<UpdateWaistSizeEnabledToGraphEvent>().Publish(SettingVals.WaistSizeEnabled);
        }

        #endregion
    }
}