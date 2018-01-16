using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Weigh.Events;
using Weigh.Localization;
using Weigh.Models;

namespace Weigh.ViewModels
{
    public class AddEntryPageViewModel : ViewModelBase
    {
        #region Constructor

        public AddEntryPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            SettingVals = new SettingVals();
            SettingValsValidated = new SettingValsValidated();
            Title = AppResources.AddEntryPageTitle;
            _ea = ea;
            PickerSource = new List<string>
            {
                AppResources.LowActivityPickItem,
                AppResources.LightActivityPickItem,
                AppResources.MediumActivityPickItem,
                AppResources.HeavyActivityPickItem
            };
            AddWeightToListCommand = new DelegateCommand(AddWeightToList);
        }

        #endregion

        #region Fields

        private DelegateCommand _addWeightToListCommand;

        public DelegateCommand AddWeightToListCommand
        {
            get => _addWeightToListCommand;
            set => SetProperty(ref _addWeightToListCommand, value);
        }

        private IEventAggregator _ea;

        private List<string> _pickerSource;
        public List<string> PickerSource
        {
            get { return _pickerSource; }
            set { SetProperty(ref _pickerSource, value); }
        }

        private bool _buttonEnabled;
        public bool ButtonEnabled
        {
            get { return _buttonEnabled; }
            set { SetProperty(ref _buttonEnabled, value); }
        }

        private SettingValsValidated _settingValsValidated;
        public SettingValsValidated SettingValsValidated
        {
            get { return _settingValsValidated; }
            set { SetProperty(ref _settingValsValidated, value); }
        }

        private SettingVals _settingVals;
        public SettingVals SettingVals
        {
            get { return _settingVals; }
            set { SetProperty(ref _settingVals, value); }
        }

        private WeightEntry _newWeightEntry;
        public WeightEntry NewWeightEntry
        {
            get { return _newWeightEntry; }
            set { SetProperty(ref _newWeightEntry, value); }
        }

        private string _noteEntry;
        public string NoteEntry
        {
            get { return _noteEntry; }
            set { SetProperty(ref _noteEntry, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// When we come to this page we will always want to initialize SettingVals & Validated
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            SettingVals.InitializeSettingVals();
            SettingValsValidated.InitializeFromSettings(SettingVals);
        }

        public async void AddWeightToList()
        {
            ButtonEnabled = false;

            if (SettingValsValidated.ValidateProperties() == false)
            {
                UserDialogs.Instance.Alert(AppResources.FormValidationPopupLabel);
                ButtonEnabled = true;
            }
            else
            {
                SettingVals.InitializeFromValidated(SettingValsValidated);
                SettingVals.LastWeight = SettingVals.Weight;
                NewWeightEntry = new WeightEntry
                {
                    Weight = SettingVals.Weight,
                    WaistSize = SettingVals.WaistSize,
                    WeightDelta = (SettingVals.Weight - SettingVals.LastWeight),
                    Note = NoteEntry
                };
                SettingVals.LastWeighDate = DateTime.UtcNow;
                SettingVals.DistanceToGoalWeight = SettingVals.Weight - SettingVals.GoalWeight;

                SettingVals.ValidateGoal();
                SettingVals.SaveSettingValsToDevice();
                _ea.GetEvent<AddWeightEvent>().Publish(NewWeightEntry);
                _ea.GetEvent<SendSetupInfoToSettingsEvent>().Publish(SettingVals);
                await App.Database.SaveWeightAsync(NewWeightEntry);
                await NavigationService.NavigateAsync(
                    $"Weigh:///NavigatingAwareTabbedPage?{KnownNavigationParameters.SelectedTab}=MainPage");
            }
        }
        #endregion
    }
}