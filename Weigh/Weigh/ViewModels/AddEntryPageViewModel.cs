﻿using System;
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
            Title = AppResources.AddEntryPageTitle;
            _ea = ea;

            SettingVals = new SettingVals();
            SettingValsValidated = new SettingValsValidated();

            AddWeightToListCommand = new DelegateCommand(AddWeightToList);

            //EntryDate = DateTime.UtcNow;
            //MaxEntryDate = DateTime.UtcNow;
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

        private readonly IEventAggregator _ea;

        private SettingVals _settingVals;

        public SettingVals SettingVals
        {
            get => _settingVals;
            set => SetProperty(ref _settingVals, value);
        }

        private SettingValsValidated _settingValsValidated;

        public SettingValsValidated SettingValsValidated
        {
            get => _settingValsValidated;
            set => SetProperty(ref _settingValsValidated, value);
        }

        private DelegateCommand _addWeightToListCommand;

        public DelegateCommand AddWeightToListCommand
        {
            get => _addWeightToListCommand;
            set => SetProperty(ref _addWeightToListCommand, value);
        }

        private List<string> _pickerSource;

        public List<string> PickerSource
        {
            get => _pickerSource;
            set => SetProperty(ref _pickerSource, value);
        }

        private bool _buttonEnabled;

        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set => SetProperty(ref _buttonEnabled, value);
        }

        private WeightEntry _newWeightEntry;

        public WeightEntry NewWeightEntry
        {
            get => _newWeightEntry;
            set => SetProperty(ref _newWeightEntry, value);
        }

        private string _noteEntry;

        public string NoteEntry
        {
            get => _noteEntry;
            set => SetProperty(ref _noteEntry, value);
        }

        /*
        private DateTime _entryDate;
        public DateTime EntryDate
        {
            get { return _entryDate; }
            set { SetProperty(ref _entryDate, value); }
        }

        private DateTime _maxEntryDate;
        public DateTime MaxEntryDate
        {
            get { return _maxEntryDate; }
            set { SetProperty(ref _maxEntryDate, value); }
        }
        */
        #endregion

        #region Methods

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
                NewWeightEntry = new WeightEntry
                {
                    Weight = SettingVals.Weight,
                    WaistSize = SettingVals.WaistSize,
                    WeightDelta = SettingVals.Weight - SettingVals.LastWeight,
                    Note = NoteEntry
                };
                SettingVals.LastWeight = SettingVals.Weight;
                SettingVals.LastWeighDate = DateTime.UtcNow;
                SettingVals.DistanceToGoalWeight = SettingVals.Weight - SettingVals.GoalWeight;

                SettingVals.ValidateGoal();
                SettingVals.SaveSettingValsToDevice();
                _ea.GetEvent<AddWeightEvent>().Publish(NewWeightEntry);
                /* Debug method to add tons of entries
                for (int i = 700; i > 190; i--)
                {
                    var WeightEntry = new WeightEntry();
                    WeightEntry.Weight = i;
                    WeightEntry.WeighDate = DateTime.UtcNow.AddDays(190 - i);
                    await App.Database.SaveWeightAsync(WeightEntry);
                }
                */
                await App.Database.SaveWeightAsync(NewWeightEntry);
                await NavigationService.GoBackAsync();
            }
        }

        /// <summary>
        ///     When we come to this page we will always want to initialize SettingVals & Validated
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            SettingVals.InitializeSettingVals();
            SettingValsValidated.InitializeFromSettings(SettingVals);
        }

        #endregion
    }
}