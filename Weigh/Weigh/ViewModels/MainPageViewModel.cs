﻿using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using Weigh.Events;
using Weigh.Helpers;
using Weigh.Localization;
using Weigh.Models;
using Microcharts;
using SkiaSharp;

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

        private Chart _daysLeftChart;

        public Chart DaysLeftChart
        {
            get => _daysLeftChart;
            set => SetProperty(ref _daysLeftChart, value);
        }

        private Chart _weightLeftChart;

        public Chart WeightLeftChart
        {
            get => _weightLeftChart;
            set => SetProperty(ref _weightLeftChart, value);
        }

        private double _weightProgress;

        public double WeightProgress
        {
            get => _weightProgress;
            set => SetProperty(ref _weightProgress, value);
        }

        private double _timeProgress;

        public double TimeProgress
        {
            get => _timeProgress;
            set => SetProperty(ref _timeProgress, value);
        }


        private int _currentDay;

        public int CurrentDay
        {
            get => _currentDay;
            set => SetProperty(ref _currentDay, value);
        }

        private string _scheduleStatus;

        public string ScheduleStatus
        {
            get => _scheduleStatus;
            set => SetProperty(ref _scheduleStatus, value);
        }

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
            InitializeCharts();
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            SettingVals.InitializeSettingVals();

            if (SettingVals.ValidateGoal() == false)
                SettingVals.SaveSettingValsToDevice();
            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Publish(SettingVals);
            // TODO: Possibly remove
            _ea.GetEvent<UpdateWaistSizeEnabledToGraphEvent>().Publish(SettingVals.WaistSizeEnabled);

            InitializeCharts();
        }

        private void InitializeCharts()
        {
            // Weight left chart

            double TotalWeightToLose = SettingVals.InitialWeight - SettingVals.GoalWeight;
            double WeightProgressToGoal = TotalWeightToLose - SettingVals.DistanceToGoalWeight;
            WeightProgress = (WeightProgressToGoal / TotalWeightToLose) * 100;
            WeightLeftChart = new RadialGaugeChart()
            {
                Entries = new[]
            {
                new Entry(Math.Min(1,(float)WeightProgress))
                {
                    Color = SKColor.FromHsv(100, 100, 100),
                },
            },
                BackgroundColor = SKColors.Transparent,
                MinValue = 0,
                MaxValue = 100,
                Margin = 0,
                AnimationDuration = TimeSpan.FromSeconds(3.5),
                LineSize = 40,
            };

            // Time left chart

            double TotalDaysToGo = (SettingVals.GoalDate - SettingVals.InitialWeighDate).TotalDays;
            double TimeProgressToGoal = TotalDaysToGo - SettingVals.TimeLeftToGoal;
            CurrentDay = (int)TimeProgressToGoal;
            TimeProgress = (TimeProgressToGoal / TotalDaysToGo) * 100;
            DaysLeftChart = new RadialGaugeChart()
            {
                Entries = new[]
            {
                new Entry(Math.Min(1,(float)TimeProgress))
                {
                    Color = SKColor.FromHsv(100, 100, 100),
                },
            },
                BackgroundColor = SKColors.Transparent,
                MinValue = 0,
                MaxValue = 100,
                Margin = 0,
                AnimationDuration = TimeSpan.FromSeconds(3.5),
                LineSize = 60,
            };

            if (TimeProgress <= WeightProgress)
            {
                ScheduleStatus = AppResources.OnScheduleLabel;
            }
            else
            {
                ScheduleStatus = AppResources.OffScheduleLabel;
            }
        }

        #endregion
    }
}