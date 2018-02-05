using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            PickerSelectedIndex = 1;
            SettingValsValidated = new SettingValsValidated();

            AddWeightToListCommand = new DelegateCommand(AddWeightToList);
            DeleteEntryCommand = new DelegateCommand(HandleDeleteEntry);

            DeleteAction = false;
            DeleteActionEnabled = false;
            EntryDate = DateTime.Now;
            NewDate = DateTimeOffset.Now;
            MaxEntryDate = DateTimeOffset.Now;
            PickerSource = new ObservableCollection<string>
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

        private ObservableCollection<string> _pickerSource;

        public ObservableCollection<string> PickerSource
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

        private DateTimeOffset _newDate;
        public DateTimeOffset NewDate
        {
            get => _newDate;
            set => SetProperty(ref _newDate, value);
        }

        private DateTimeOffset _maxEntryDate;
        public DateTimeOffset MaxEntryDate
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

        private int _pickerSelectedIndex;
        public int PickerSelectedIndex
        {
            get => _pickerSelectedIndex;
            set => SetProperty(ref _pickerSelectedIndex, value);
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

            WeightEntry nextWeightEntry;
            WeightEntry previousWeightEntry;
            //DateTimeOffset newdate = EntryDate.Date + new TimeSpan(DateTimeOffset.UtcNow.Hour, DateTimeOffset.UtcNow.Minute, DateTimeOffset.UtcNow.Second);


            List<WeightEntry> entries = await App.Database.GetWeightsAsync();
            var dentries = entries.OrderByDescending(x => x.WeighDate.LocalDateTime).ToList();
            var index = dentries.TakeWhile(x => x.ID != SelectedWeightEntry.ID).Count();
            if (index == 0)
            {
                nextWeightEntry = null;
            }
            else
            {
                nextWeightEntry = dentries[index - 1];
            }

            if (index > dentries.Count - 2)
            {
                previousWeightEntry = null;
            }
            else
            {
                previousWeightEntry = dentries[index + 1];
            }

            // Delete case where we are deleting the first entry
            if (previousWeightEntry == null)
            {
                Settings.InitialWeightDate = nextWeightEntry.WeighDate.LocalDateTime;
                Settings.InitialWeight = nextWeightEntry.Weight;
                nextWeightEntry.WeightDelta = 0;
                await App.Database.SaveWeightAsync(nextWeightEntry);
            }
            // Delete case where we are deleting the latest entry
            else if (nextWeightEntry == null)
            {
                Settings.Weight = previousWeightEntry.Weight;
                Settings.LastWeighDate = previousWeightEntry.WeighDate.LocalDateTime;
                Settings.LastWeight = previousWeightEntry.Weight;
            }
            // Normal delete case
            else if (previousWeightEntry != null && nextWeightEntry != null)
            {
                nextWeightEntry.WeightDelta = nextWeightEntry.Weight - previousWeightEntry.Weight;
                await App.Database.SaveWeightAsync(nextWeightEntry);
            }

            await App.Database.DeleteWeightInfoAsync(SelectedWeightEntry);
            _ea.GetEvent<AddWeightEvent>().Publish();
            await NavigationService.GoBackAsync();
            return;
        }

        public async void EditWeight()
        {
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
            List<WeightEntry> dentries = entries.OrderByDescending(x => x.WeighDate.LocalDateTime).ToList();
            int selectedItemIndex = dentries.TakeWhile(x => x.ID != SelectedWeightEntry.ID).Count();
            NewDate = EntryDate;

            SettingVals.InitializeFromValidated(SettingValsValidated);
            double newDelta = 0.0;
            WeightEntry previousWeightEntry;
            WeightEntry nextWeightEntry;

            // For editing, we must remove the current entry before we look find prev/next entries
            await App.Database.DeleteWeightInfoAsync(SelectedWeightEntry);

            entries = await App.Database.GetWeightsAsync();
            dentries = entries.OrderByDescending(x => x.WeighDate.LocalDateTime).ToList();
            int index = dentries.TakeWhile(x => x.WeighDate.LocalDateTime > NewDate.LocalDateTime).Count();
            if (index == 0)
            {
                nextWeightEntry = null;
            }
            else
            {
                nextWeightEntry = dentries[index - 1];
            }

            if (index >= dentries.Count)
            {
                previousWeightEntry = null;
            }
            else
            {
                previousWeightEntry = dentries[index];
            }

            if (nextWeightEntry != null)
            {
                nextWeightEntry.WeightDelta = nextWeightEntry.Weight - SettingVals.Weight;
                await App.Database.SaveWeightAsync(nextWeightEntry);
            }

            if (previousWeightEntry != null)
            {
                newDelta = SettingVals.Weight - previousWeightEntry.Weight;
            }

            // Case: Moving current entry forward in time, need to update delta on prev entry
            if (SelectedWeightEntry.WeighDate.LocalDateTime < NewDate.LocalDateTime && previousWeightEntry != null)
            {
                index = dentries.TakeWhile(x => x.WeighDate.LocalDateTime > previousWeightEntry.WeighDate.LocalDateTime).Count();

                if (index < dentries.Count - 1)
                {
                    previousWeightEntry.WeightDelta = previousWeightEntry.Weight - dentries[index + 1].Weight;
                    await App.Database.SaveWeightAsync(previousWeightEntry);
                }
            }

            WeightEntry editedWeightEntry = new WeightEntry
            {
                Weight = SettingVals.Weight,
                WaistSize = SettingVals.WaistSize,
                WeightDelta = newDelta,
                WeighDate = NewDate,
                Note = NoteEntry
            };


            // Case: Edit most recent entry
            if (nextWeightEntry == null)
            {
                SettingVals.Weight = editedWeightEntry.Weight;
                SettingVals.LastWeighDate = editedWeightEntry.WeighDate.LocalDateTime;
                SettingVals.LastWeight = editedWeightEntry.Weight;
                SettingVals.DistanceToGoalWeight = editedWeightEntry.Weight - SettingVals.GoalWeight;
                SettingVals.PickerSelectedItem = PickerSelectedIndex;
                SettingVals.ValidateGoal();
                SettingVals.SaveSettingValsToDevice();
            }

            // Case: Edit first entry
            if (previousWeightEntry == null)
            {
                Settings.InitialWeight = editedWeightEntry.Weight;
                Settings.InitialWeightDate = editedWeightEntry.WeighDate.LocalDateTime;
            }


            // Case: Edit first weight and move it forward
            if (selectedItemIndex == dentries.Count)
            {
                WeightEntry firstWeightEntry = await App.Database.GetFirstWeightasync();

                Settings.InitialWeight = firstWeightEntry.Weight;
                Settings.InitialWeightDate = firstWeightEntry.WeighDate.UtcDateTime;
            }

            await App.Database.SaveWeightAsync(editedWeightEntry);

            _ea.GetEvent<AddWeightEvent>().Publish();
            await NavigationService.GoBackAsync();
        }



        public async void AddWeightToList()
        {
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

            NewDate = EntryDate;

            SettingVals.InitializeFromValidated(SettingValsValidated);
            double newDelta = 0.0;
            WeightEntry previousWeightEntry;
            WeightEntry nextWeightEntry;
            List<WeightEntry> entries = await App.Database.GetWeightsAsync();
            var dentries = entries.OrderByDescending(x => x.WeighDate.LocalDateTime).ToList();
            var index = dentries.TakeWhile(x => x.WeighDate.LocalDateTime > NewDate.LocalDateTime).Count();
            if (index == 0)
            {
                nextWeightEntry = null;
            }
            else
            {
                nextWeightEntry = dentries[index - 1];
            }

            if (index >= dentries.Count)
            {
                previousWeightEntry = null;
            }
            else
            {
                previousWeightEntry = dentries[index];
            }

            if (nextWeightEntry != null)
            {
                nextWeightEntry.WeightDelta = nextWeightEntry.Weight - SettingVals.Weight;
                await App.Database.SaveWeightAsync(nextWeightEntry);
            }

            if (previousWeightEntry != null)
            {
                newDelta = SettingVals.Weight - previousWeightEntry.Weight;
            }

            NewWeightEntry = new WeightEntry
            {
                Weight = SettingVals.Weight,
                WaistSize = SettingVals.WaistSize,
                WeightDelta = newDelta,
                WeighDate = NewDate.LocalDateTime,
                Note = NoteEntry
            };

            // Case: Edit most recent entry
            if (nextWeightEntry == null)
            {
                SettingVals.Weight = NewWeightEntry.Weight;
                SettingVals.LastWeighDate = NewWeightEntry.WeighDate.LocalDateTime;
                SettingVals.LastWeight = NewWeightEntry.Weight;
                SettingVals.DistanceToGoalWeight = NewWeightEntry.Weight - SettingVals.GoalWeight;
                SettingVals.PickerSelectedItem = PickerSelectedIndex;
                SettingVals.ValidateGoal();
                SettingVals.SaveSettingValsToDevice();
            }

            // Case: Edit first entry
            if (previousWeightEntry == null)
            {
                Settings.InitialWeight = NewWeightEntry.Weight;
                Settings.InitialWeightDate = NewWeightEntry.WeighDate.LocalDateTime;
            }
            await App.Database.SaveWeightAsync(NewWeightEntry);

            _ea.GetEvent<AddWeightEvent>().Publish();
            await NavigationService.GoBackAsync();
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
                EntryDate = SelectedWeightEntry.WeighDate.LocalDateTime;
                Title = AppResources.EditEntryPageTitle;
                DeleteAction = true;
                PickerSelectedIndex = SettingVals.PickerSelectedItem;
                AddWeightToListCommand = new DelegateCommand(EditWeight);
            }
            else
            {
                Title = AppResources.AddEntryPageTitle;
                AddWeightToListCommand = new DelegateCommand(AddWeightToList);
                PickerSelectedIndex = Settings.PickerSelectedItem;
            }

            if (parameters.ContainsKey("OnlyOneEntryLeft"))
            {
                DeleteAction = false;
            }
        }

        #endregion
    }
}