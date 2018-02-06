using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Weigh.Events;
using Weigh.Helpers;
using Weigh.Localization;
using Weigh.Models;
using Xamarin.Forms;

namespace Weigh.ViewModels
{
    /// <summary>
    ///     Page Displays Settings info and allows for editing
    ///     Inputs:     (AppState.cs)->AppStateInfo, (MainVM)->Weight+Goals
    ///     Outputs:    Goals->(MainVM,GraphsVM)
    /// </summary>
    public class SettingsPageViewModel : ViewModelBase
    {
        #region Constructor

        public SettingsPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            Title = AppResources.SettingsPageTitle;
            _ea = ea;

            SettingVals = new SettingVals();
            PickerSelectedIndex = 1;
            SettingValsValidated = new SettingValsValidated();

            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            DeleteAllCommand = new DelegateCommand(DeleteAll);
            SelectImperialCommand = new DelegateCommand(SelectImperial);
            SelectMetricCommand = new DelegateCommand(SelectMetric);
            SelectMaleCommand = new DelegateCommand(SelectMale);
            SelectFemaleCommand = new DelegateCommand(SelectFemale);

            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Subscribe(HandleNewSetupInfo);

            DeleteAllButton = AppResources.settings_page_delete_all_button;
            BirthDateMinDate = DateTimeOffset.Now.AddYears(-150);
            BirthDateMaxDate = DateTimeOffset.Now.AddYears(-1);
            ImperialSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            MaleSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            SettingVals.MinDate = DateTimeOffset.Now.AddDays(10);
            MaxGoalDate = DateTimeOffset.Now.AddYears(2);
            PickerSource = new ObservableCollection<string>
            {
                AppResources.LowActivityPickItem,
                AppResources.LightActivityPickItem,
                AppResources.MediumActivityPickItem,
                AppResources.HeavyActivityPickItem
            };
            MaleText = "\uf183  " + AppResources.MaleGenderSwitchLabel;
            FemaleText = "\uf182  " + AppResources.FemaleGenderSwitchLabel;

            _timelft = 5;
        }

        #endregion

        #region Fields

        private readonly IEventAggregator _ea;

        private int _timelft;


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

        public DelegateCommand SaveInfoCommand { get; set; }
        public DelegateCommand SelectImperialCommand { get; set; }
        public DelegateCommand SelectMetricCommand { get; set; }
        public DelegateCommand SelectMaleCommand { get; set; }
        public DelegateCommand SelectFemaleCommand { get; set; }
        public DelegateCommand DeleteAllCommand { get; set; }

        private ObservableCollection<string> _pickerSource;

        public ObservableCollection<string> PickerSource
        {
            get => _pickerSource;
            set => SetProperty(ref _pickerSource, value);
        }

        private Color _metricSelectedBorderColor;

        public Color MetricSelectedBorderColor
        {
            get => _metricSelectedBorderColor;
            set => SetProperty(ref _metricSelectedBorderColor, value);
        }

        private Color _imperialSelectedBorderColor;

        public Color ImperialSelectedBorderColor
        {
            get => _imperialSelectedBorderColor;
            set => SetProperty(ref _imperialSelectedBorderColor, value);
        }

        private Color _maleSelectedBorderColor;

        public Color MaleSelectedBorderColor
        {
            get => _maleSelectedBorderColor;
            set => SetProperty(ref _maleSelectedBorderColor, value);
        }

        private Color _femaleSelectedBorderColor;

        public Color FemaleSelectedBorderColor
        {
            get => _femaleSelectedBorderColor;
            set => SetProperty(ref _femaleSelectedBorderColor, value);
        }

        private DateTimeOffset _birthDateMinDate;

        public DateTimeOffset BirthDateMinDate
        {
            get => _birthDateMinDate;
            set => SetProperty(ref _birthDateMinDate, value);
        }

        private DateTimeOffset _birthDateMaxDate;

        public DateTimeOffset BirthDateMaxDate
        {
            get => _birthDateMaxDate;
            set => SetProperty(ref _birthDateMaxDate, value);
        }

        private DateTimeOffset _maxGoalDate;

        public DateTimeOffset MaxGoalDate
        {
            get => _maxGoalDate;
            set => SetProperty(ref _maxGoalDate, value);
        }

        private DateTime _goalDate;

        public DateTime GoalDate
        {
            get => _goalDate;
            set => SetProperty(ref _goalDate, value);
        }

        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
        }

        private string _maleText;
        public string MaleText
        {
            get => _maleText;
            set => SetProperty(ref _maleText, value);
        }

        private string _femaleText;
        public string FemaleText
        {
            get => _femaleText;
            set => SetProperty(ref _femaleText, value);
        }

        private string _deleteAllButton;
        public string DeleteAllButton
        {
            get => _deleteAllButton;
            set => SetProperty(ref _deleteAllButton, value);
        }

        private int _pickerSelectedIndex;
        public int PickerSelectedIndex
        {
            get => _pickerSelectedIndex;
            set => SetProperty(ref _pickerSelectedIndex, value);
        }

        #endregion

        #region Methods

        private void SelectImperial()
        {
            MetricSelectedBorderColor = Color.Default;
            ImperialSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            SettingVals.Units = true;
            Settings.Units = true;
        }

        private void SelectMetric()
        {
            MetricSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            ImperialSelectedBorderColor = Color.Default;
            SettingVals.Units = false;
            Settings.Units = false;
        }

        private void SelectMale()
        {
            MaleSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            FemaleSelectedBorderColor = Color.Default;
            SettingVals.Sex = false;
        }

        private void SelectFemale()
        {
            MaleSelectedBorderColor = Color.Default;
            FemaleSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            SettingVals.Sex = true;
        }


        private void SaveInfoAsync()
        {
            SettingVals.BirthDate = BirthDate;
            SettingVals.GoalDate = GoalDate;
            SettingValsValidated.Age = Convert.ToInt32(Math.Floor((DateTimeOffset.Now - SettingVals.BirthDate).TotalDays / 365)).ToString();
            if (SettingValsValidated.ValidateProperties())
            {
                SettingVals.InitializeFromValidated(SettingValsValidated);
                SettingVals.PickerSelectedItem = PickerSelectedIndex;
                Settings.Age = SettingVals.Age;
                SettingVals.ValidateGoal();
                SettingVals.SaveSettingValsToDevice();
                GoalDate = SettingVals.GoalDate.LocalDateTime;
                _ea.GetEvent<UpdateSetupInfoEvent>().Publish(SettingVals);

                UserDialogs.Instance.Toast(new ToastConfig(AppResources.SavedToast)
                                                          .SetPosition(ToastPosition.Top));
            }
        }

        private async void DeleteAll()
        {
            TimeSpan timelefTimeSpan = new TimeSpan(0,0,5);

            if (_timelft != 5)
            {
                await App.Database.DeleteAllWeightsAsync();
                await NavigationService.NavigateAsync("/InitialSetupPage");
                Settings.FirstUse = "FALSE";
                return;
            }
            if (DeleteAllButton == AppResources.settings_page_delete_all_button)
            {
                //_buttonTimerStopwatch = new Stopwatch();
                //_buttonTimerStopwatch.Start();
                DeleteAllButton = string.Format(AppResources.settings_page_delete_all_confirmation_button, _timelft);

                Device.StartTimer(new TimeSpan(0,0,1), () =>
                {
                    if (timelefTimeSpan != null && _timelft > 0)
                    {
                        timelefTimeSpan = timelefTimeSpan.Subtract(TimeSpan.FromSeconds(1));
                        _timelft = timelefTimeSpan.Seconds;
                        DeleteAllButton = string.Format(AppResources.settings_page_delete_all_confirmation_button, _timelft);
                        return true;
                    }
                    _timelft = 5;
                    DeleteAllButton = AppResources.settings_page_delete_all_button;
                    return false;
                });
            }
        }

        private void HandleNewSetupInfo(SettingVals setupInfo)
        {
            SettingVals = setupInfo;
            SettingValsValidated.InitializeFromSettings(SettingVals);
            SettingVals.MinDate = DateTimeOffset.Now.AddDays(10);
            PickerSelectedIndex = SettingVals.PickerSelectedItem;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("SettingVals"))
            {
                SettingVals = (SettingVals)parameters["SettingVals"];
                SettingValsValidated.InitializeFromSettings(SettingVals);
                SettingVals.MinDate = DateTimeOffset.Now.AddDays(10);
                PickerSelectedIndex = SettingVals.PickerSelectedItem;
                BirthDate = SettingVals.BirthDate.LocalDateTime;
                GoalDate = SettingVals.GoalDate.LocalDateTime;
            }
        }

        public override void Destroy()
        {
            _ea.GetEvent<SendSetupInfoToSettingsEvent>().Unsubscribe(HandleNewSetupInfo);
        }

        #endregion
    }
}