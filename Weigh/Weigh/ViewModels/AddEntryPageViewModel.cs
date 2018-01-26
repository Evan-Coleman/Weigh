using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Weigh.Events;
using Weigh.Helpers;
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
            _ea = ea;

            SettingVals = new SettingVals();
            SettingValsValidated = new SettingValsValidated();

            AddWeightToListCommand = new DelegateCommand(AddWeightToList);
            DeleteEntryCommand = new DelegateCommand(HandleDeleteEntry);

            DeleteAction = false;
            DeleteActionEnabled = false;
            EntryDate = DateTime.UtcNow.ToLocalTime();
            MaxEntryDate = DateTime.UtcNow.ToLocalTime();
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

        private DelegateCommand _deleteEntryCommand;

        public DelegateCommand DeleteEntryCommand
        {
            get => _deleteEntryCommand;
            set => SetProperty(ref _deleteEntryCommand, value);
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

        private DateTime _entryDate;
        public DateTime EntryDate
        {
            get => _entryDate;
            set => SetProperty(ref _entryDate, value);
        }

        private DateTime _maxEntryDate;
        public DateTime MaxEntryDate
        {
            get => _maxEntryDate;
            set => SetProperty(ref _maxEntryDate, value);
        }

        private WeightEntry _selectedWeightEntry;
        public WeightEntry SelectedWeightEntry
        {
            get => _selectedWeightEntry;
            set => SetProperty(ref _selectedWeightEntry, value);
        }

        private double _weightDelta;
        public double WeightDelta
        {
            get => _weightDelta;
            set => SetProperty(ref _weightDelta, value);
        }

        private bool _deleteAction;
        public bool DeleteAction
        {
            get => _deleteAction;
            set => SetProperty(ref _deleteAction, value);
        }

        private bool _deleteActionEnabled;
        public bool DeleteActionEnabled
        {
            get => _deleteActionEnabled;
            set => SetProperty(ref _deleteActionEnabled, value);
        }

        #endregion

        #region Methods

        private async void HandleDeleteEntry()
        {
            DeleteActionEnabled = true;
            //AddWeightToList();
            ButtonEnabled = false;
            Settings.WaistSizeEnabled = SettingVals.WaistSizeEnabled;
            if (SettingValsValidated.WaistSize == "")
            {
                SettingValsValidated.WaistSize = "0";
            }
            if (SettingValsValidated.ValidateProperties() == false)
            {
                UserDialogs.Instance.Alert(AppResources.FormValidationPopupLabel);
                ButtonEnabled = true;
                return;
            }

            List<WeightEntry> entries = await App.Database.GetWeightsAsync();
            WeightEntry previousWeightEntry = entries
                .OrderByDescending(x => x.WeighDate).FirstOrDefault(x => x.WeighDate < EntryDate);

            // Since we're adding an older entry, we want to make sure and update the next entry's weightdelta
            WeightEntry nextWeightEntry = entries
                .OrderBy(x => x.WeighDate).FirstOrDefault(x => x.WeighDate > EntryDate);

            // Delete case where we are deleting the first entry
            if (previousWeightEntry == null)
            {
                Settings.InitialWeightDate = nextWeightEntry.WeighDate;
                Settings.InitialWeight = nextWeightEntry.Weight;
                nextWeightEntry.WeightDelta = 0;
                await App.Database.SaveWeightAsync(nextWeightEntry);
            }
            // Delete case where we are deleting the latest entry
            else if (nextWeightEntry == null)
            {
                Settings.Weight = previousWeightEntry.Weight;
                Settings.LastWeighDate = previousWeightEntry.WeighDate;
                Settings.LastWeight = previousWeightEntry.Weight;
            }
            // Normal delete case
            else if (previousWeightEntry != null && nextWeightEntry != null)
            {
                nextWeightEntry.WeightDelta = nextWeightEntry.Weight - previousWeightEntry.Weight;
                await App.Database.SaveWeightAsync(nextWeightEntry);
            }

            await App.Database.DeleteWeightInfoAsync(SelectedWeightEntry);
            _ea.GetEvent<AddWeightEvent>().Publish(NewWeightEntry);
            await NavigationService.GoBackAsync();
            return;
        }

        public async void AddWeightToList()
        {
            // TODO: Handle case of editing the newest entry to a later date
            ButtonEnabled = false;
            Settings.WaistSizeEnabled = SettingVals.WaistSizeEnabled;
            if (SettingValsValidated.WaistSize == "")
            {
                SettingValsValidated.WaistSize = "0";
            }
            if (SettingValsValidated.ValidateProperties() == false)
            {
                UserDialogs.Instance.Alert(AppResources.FormValidationPopupLabel);
                ButtonEnabled = true;
            }
            else
            {
                DateTime newdate = EntryDate.Date +
                                   new TimeSpan(DateTime.UtcNow.ToLocalTime().Hour, DateTime.UtcNow.ToLocalTime().Minute, DateTime.UtcNow.ToLocalTime().Second);
                if (SelectedWeightEntry != null)
                {
                    await App.Database.DeleteWeightInfoAsync(SelectedWeightEntry);
                }
                SettingVals.InitializeFromValidated(SettingValsValidated);
                if (SettingVals.LastWeighDate.Date < newdate.Date)
                {
                    NewWeightEntry = new WeightEntry
                    {
                        Weight = SettingVals.Weight,
                        WaistSize = SettingVals.WaistSize,
                        WeightDelta = SettingVals.Weight - SettingVals.LastWeight,
                        WeighDate = newdate.Date,
                        Note = NoteEntry
                    };

                    SettingVals.LastWeighDate = newdate.Date;
                    SettingVals.LastWeight = SettingVals.Weight;
                    SettingVals.DistanceToGoalWeight = SettingVals.Weight - SettingVals.GoalWeight;

                    SettingVals.ValidateGoal();
                    SettingVals.SaveSettingValsToDevice();
                }
                // If we're adding an older entry than the latest, we won't update current stats in Settings
                else
                {
                    List<WeightEntry> entries = await App.Database.GetWeightsAsync();
                    WeightEntry previousWeightEntry = entries
                        .OrderByDescending(x => x.WeighDate).FirstOrDefault(x => x.WeighDate < newdate);

                    // Since we're adding an older entry, we want to make sure and update the next entry's weightdelta
                    WeightEntry nextWeightEntry = entries
                        .OrderBy(x => x.WeighDate).FirstOrDefault(x => x.WeighDate > newdate);
                    if (nextWeightEntry != null)
                    {
                        nextWeightEntry.WeightDelta = nextWeightEntry.Weight - SettingVals.Weight;
                        await App.Database.SaveWeightAsync(nextWeightEntry);
                    }
                    // Special case of adding an entry before any other entry
                    // TODO: Potentially change Settings values for initial weigh
                    if (previousWeightEntry == null)
                    {
                        if (DeleteActionEnabled == false)
                        {
                            NewWeightEntry = new WeightEntry
                            {
                                Weight = SettingVals.Weight,
                                WaistSize = SettingVals.WaistSize,
                                WeightDelta = 0,
                                WeighDate = newdate,
                                Note = NoteEntry
                            };
                            SettingVals.LastWeighDate = newdate;
                            SettingVals.LastWeight = SettingVals.Weight;
                            SettingVals.InitialWeighDate = newdate;
                            SettingVals.InitialWeight = SettingVals.Weight;
                        }
                        else if (nextWeightEntry != null)
                        {
                            SettingVals.LastWeighDate = nextWeightEntry.WeighDate;
                            SettingVals.LastWeight = nextWeightEntry.Weight;
                            nextWeightEntry.WeightDelta = 0;
                            SettingVals.InitialWeighDate = nextWeightEntry.WeighDate;
                            SettingVals.InitialWeight = nextWeightEntry.Weight;
                            // Delete old and save new
                            //await App.Database.DeleteWeightInfoAsync(nextWeightEntry);
                            await App.Database.SaveWeightAsync(nextWeightEntry);
                        }
                    }
                    else
                    {
                            NewWeightEntry = new WeightEntry
                            {
                                Weight = SettingVals.Weight,
                                WaistSize = SettingVals.WaistSize,
                                WeightDelta = SettingVals.Weight - previousWeightEntry.Weight,
                                WeighDate = newdate,
                                Note = NoteEntry
                            };
                        
                    }
                }

                



                /* Debug method to add tons of entries
                for (int i = 700; i > 190; i--)
                {
                    var WeightEntry = new WeightEntry();
                    WeightEntry.Weight = i;
                    WeightEntry.WeighDate = DateTime.UtcNow.ToLocalTime().AddDays(190 - i);
                    await App.Database.SaveWeightAsync(WeightEntry);
                }
                */
                // TODO: If delete first entry, stuff breaks
                if (DeleteActionEnabled == false)
                {
                    await App.Database.SaveWeightAsync(NewWeightEntry);
                }

                WeightEntry latestWeight = await App.Database.GetLatestWeightasync();
                SettingVals.Weight = latestWeight.Weight;
                SettingVals.LastWeighDate = latestWeight.WeighDate;

                SettingVals.SaveSettingValsToDevice();
                _ea.GetEvent<AddWeightEvent>().Publish(NewWeightEntry);
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
            if (parameters.ContainsKey("ItemTapped"))
            {
                SelectedWeightEntry = (WeightEntry)parameters["ItemTapped"];

                SettingValsValidated.Weight = SelectedWeightEntry.Weight.ToString();
                SettingValsValidated.WaistSize = SelectedWeightEntry.WaistSize.ToString();
                WeightDelta = SelectedWeightEntry.WeightDelta;
                NoteEntry = SelectedWeightEntry.Note;
                EntryDate = SelectedWeightEntry.WeighDate;
                Title = AppResources.EditEntryPageTitle;
                DeleteAction = true;
            }
            else
            {
                Title = AppResources.AddEntryPageTitle;
            }

            if (parameters.ContainsKey("OnlyOneEntryLeft"))
            {
                DeleteAction = false;
            }
        }

        #endregion
    }
}