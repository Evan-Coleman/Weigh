using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Syncfusion.ListView.XForms;
using Weigh.Events;
using Weigh.Extensions;
using Weigh.Localization;
using Weigh.Models;
using Xamarin.Forms;

namespace Weigh.ViewModels
{
    /// <summary>
    ///     Page will display a historical graph of entered data as well as a list of entries with the ability to click on them
    ///     to view in more detail
    ///     Inputs:     (MainPage)->New weight entries
    ///     Outputs:    None
    /// </summary>
    public class GraphsPageViewModel : ViewModelBase
    {
        #region Constructor

        public GraphsPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            Title = AppResources.GraphPageTitle;
            _ea = ea;

            WeightList = new ObservableCollection<WeightEntry>();
            ChartData = new ObservableCollection<WeightEntry>();

            ShowWeekCommand = new DelegateCommand(ShowWeek);
            ShowMonthCommand = new DelegateCommand(ShowMonth);
            ShowYearCommand = new DelegateCommand(ShowYear);
            ItemTappedCommand = new DelegateCommand<SfListView>(HandleItemTapped);
            //ToggleWeightWaistSizeCommand = new DelegateCommand(ToggleWeightWaistSize);

            ShowDataMarker = true;
            WeekSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            CurrentlySelectedGraphTimeline = "week";
            ToggleWeightOrWaistSize = "Weight";
            ToggleWeightOrWaistSizeLabel = AppResources.WeightLabel;
            //WaistSizeEnabled = Settings.WaistSizeEnabled;

            //SfDateTimeRangeNavigator rangeNavigator = new SfDateTimeRangeNavigator();
            /*
            ViewStartDateRange = Settings.LastWeighDate.AddDays(-10).ToString("MM/dd/yyyy");
            ViewEndDateRange = Settings.LastWeighDate.ToString("MM/dd/yyyy");

            StartDateRange = Settings.InitialWeightDate.ToString("MM/dd/yyyy");
            EndDateRange = Settings.LastWeighDate.ToString("MM/dd/yyyy");
            
            // REMOVE WHEN NOT DEBUG
            // .ToString("MM/dd/yyyy")
            Settings.InitialWeightDate = new DateTime(2017, 12, 1);
            StartDateRange = Settings.InitialWeightDate;
            EndDateRange = new DateTime(2017, 12, 18);
            // REMOVE WHEN NOT DEBUG
            ViewStartDateRange = new DateTime(2017, 12, 18).AddDays(-10);
            ViewEndDateRange = new DateTime(2017, 12, 18);
            */

            _ea.GetEvent<AddWeightEvent>().Subscribe(HandleNewWeightEntry);
            //ea.GetEvent<UpdateWaistSizeEnabledToGraphEvent>().Subscribe(UpdateWaistSizeEnabled);
        }

        #endregion

        #region Fields

        private readonly IEventAggregator _ea;

        private ObservableCollection<WeightEntry> _weightList;

        public ObservableCollection<WeightEntry> WeightList
        {
            get => _weightList;
            set => SetProperty(ref _weightList, value);
        }

        private ObservableCollection<WeightEntry> _chartData;

        public ObservableCollection<WeightEntry> ChartData
        {
            get => _chartData;
            set => SetProperty(ref _chartData, value);
        }

        public DelegateCommand ShowWeekCommand { get; set; }
        public DelegateCommand ShowMonthCommand { get; set; }
        public DelegateCommand ShowYearCommand { get; set; }
        public DelegateCommand ToggleWeightWaistSizeCommand { get; set; }
        public DelegateCommand<SfListView> ItemTappedCommand { get; set; }

        private bool _showDataMarker;

        public bool ShowDataMarker
        {
            get => _showDataMarker;
            set => SetProperty(ref _showDataMarker, value);
        }

        private Color _weekSelectedBorderColor;

        public Color WeekSelectedBorderColor
        {
            get => _weekSelectedBorderColor;
            set => SetProperty(ref _weekSelectedBorderColor, value);
        }

        private Color _monthSelectedBorderColor;

        public Color MonthSelectedBorderColor
        {
            get => _monthSelectedBorderColor;
            set => SetProperty(ref _monthSelectedBorderColor, value);
        }

        private Color _yearSelectedBorderColor;

        public Color YearSelectedBorderColor
        {
            get => _yearSelectedBorderColor;
            set => SetProperty(ref _yearSelectedBorderColor, value);
        }

        public string CurrentlySelectedGraphTimeline { get; set; }
        private string _toggleWeightOrWaistSize;

        public string ToggleWeightOrWaistSize
        {
            get => _toggleWeightOrWaistSize;
            set => SetProperty(ref _toggleWeightOrWaistSize, value);
        }

        private string _toggleWeightOrWaistSizeLabel;

        public string ToggleWeightOrWaistSizeLabel
        {
            get => _toggleWeightOrWaistSizeLabel;
            set => SetProperty(ref _toggleWeightOrWaistSizeLabel, value);
        }

        public List<WeightEntry> DataFromDatabase { get; set; }

        // Functionality removed, keeping code for possible future re-integration
        /*
        private bool _waistSizeEnabled;

        public bool WaistSizeEnabled
        {
            get => _waistSizeEnabled;
            set => SetProperty(ref _waistSizeEnabled, value);
        }
        */

        #endregion

        #region Methods

        // Functionality removed, keeping code for possible future re-integration
        /*
// Doesn't work to hide/show
private void UpdateWaistSizeEnabled(bool enabled)
{
    WaistSizeEnabled = enabled;
}



private void ToggleWeightWaistSize()
{
    if (ToggleWeightOrWaistSize == "Weight")
    {
        ToggleWeightOrWaistSize = "WaistSize";
        ToggleWeightOrWaistSizeLabel = AppResources.WaistSizeLabel;
    }
    else
    {
        ToggleWeightOrWaistSize = "Weight";
        ToggleWeightOrWaistSizeLabel = AppResources.WeightLabel;
    }
}
*/

        private void ShowWeek()
        {
            ChartData = WeightList.Take(7).ToObservableCollection();
            ShowDataMarker = true;
            WeekSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            MonthSelectedBorderColor = Color.Default;
            YearSelectedBorderColor = Color.Default;
            CurrentlySelectedGraphTimeline = "week";
        }

        private void ShowMonth()
        {
            ChartData = WeightList.Take(31).ToObservableCollection();
            ShowDataMarker = false;
            WeekSelectedBorderColor = Color.Default;
            MonthSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            YearSelectedBorderColor = Color.Default;
            CurrentlySelectedGraphTimeline = "month";
        }

        private void ShowYear()
        {
            ChartData = WeightList.Take(365).ToObservableCollection();
            ShowDataMarker = false;
            WeekSelectedBorderColor = Color.Default;
            MonthSelectedBorderColor = Color.Default;
            YearSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            CurrentlySelectedGraphTimeline = "year";
        }

        private async void HandleItemTapped(SfListView listView)
        {
            if (listView.SelectedItem != null)
            {
                NavigationParameters p = new NavigationParameters
                {
                    { "ItemTapped", listView.SelectedItem }
                };
                if (DataFromDatabase.Count == 1)
                {
                    p.Add("OnlyOneEntryLeft", true);
                }
                await NavigationService.NavigateAsync("AddEntryPage", p);
            }
        }

        private async void HandleNewWeightEntry()
        {
            DataFromDatabase = await App.Database.GetWeightsAsync();
            DataFromDatabase = DataFromDatabase.OrderByDescending(x => x.WeighDate).ToList();
            WeightList = DataFromDatabase.ToObservableCollection();

            ChartData = DataFromDatabase.Take(7).ToObservableCollection();
            ShowDataMarker = true;
            WeekSelectedBorderColor = (Color)Application.Current.Resources["ButtonSelected"];
            MonthSelectedBorderColor = Color.Default;
            YearSelectedBorderColor = Color.Default;
            CurrentlySelectedGraphTimeline = "week";
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("AllWeightEntriesSorted"))
            {
                DataFromDatabase = (List<WeightEntry>) parameters["AllWeightEntriesSorted"];
                WeightList = DataFromDatabase.ToObservableCollection();
                ChartData = DataFromDatabase.Take(7).ToObservableCollection();
            }
            // Only time this gets called is if we save new settings from the settingspage
            else
            {
                HandleNewWeightEntry();
            }
        }

        public override void Destroy()
        {
            _ea.GetEvent<AddWeightEvent>().Unsubscribe(HandleNewWeightEntry);
        }

        #endregion
    }
}