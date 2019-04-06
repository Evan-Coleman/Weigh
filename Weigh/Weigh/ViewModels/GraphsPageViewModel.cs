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
using Weigh.Helpers;
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

            ShowWeekCommand = new DelegateCommand(ShowWeek);
            ShowMonthCommand = new DelegateCommand(ShowMonth);
            ShowYearCommand = new DelegateCommand(ShowYear);
            ItemTappedCommand = new DelegateCommand<SfListView>(HandleItemTapped);

            MaxChartDate = DateTime.Now;
            ShowWeek();

            _ea.GetEvent<AddWeightEvent>().Subscribe(HandleNewWeightEntry);
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

        public DelegateCommand ShowWeekCommand { get; set; }
        public DelegateCommand ShowMonthCommand { get; set; }
        public DelegateCommand ShowYearCommand { get; set; }
        public DelegateCommand<SfListView> ItemTappedCommand { get; set; }

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

        private DateTime _minChartDate;

        public DateTime MinChartDate
        {
            get => _minChartDate;
            set => SetProperty(ref _minChartDate, value);
        }

        private DateTime _maxChartDate;

        public DateTime MaxChartDate
        {
            get => _maxChartDate;
            set => SetProperty(ref _maxChartDate, value);
        }

        private bool _waistSizeEnabled;

        public bool WaistSizeEnabled
        {
            get => _waistSizeEnabled;
            set => SetProperty(ref _waistSizeEnabled, value);
        }

        public string CurrentlySelectedGraphTimeline { get; set; }

        public List<WeightEntry> DataFromDatabase { get; set; }

        #endregion

        #region Methods

        private void ShowWeek()
        {
            WaistSizeEnabled = Settings.WaistSizeEnabled;
            MaxChartDate = DateTime.Now.AddDays(0.5);
            MinChartDate = DateTime.Now.AddDays(-7);
            if (MaxChartDate > Settings.LastWeighDate)
            {
                MaxChartDate = Settings.LastWeighDate.AddDays(0.5);
                MinChartDate = MaxChartDate.AddDays(-7);
            }
            if (MinChartDate < Settings.InitialWeightDate)
            {
                MinChartDate = Settings.InitialWeightDate.AddDays(-1);
            }
            WeekSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            MonthSelectedBorderColor = Color.Default;
            YearSelectedBorderColor = Color.Default;
            CurrentlySelectedGraphTimeline = "week";
        }

        private void ShowMonth()
        {
            // TODO: FIGURE OUT LISTVIEW WAIST SHOWING
            WaistSizeEnabled = Settings.WaistSizeEnabled;
            MaxChartDate = DateTime.Now.AddDays(1);
            MinChartDate = DateTime.Now.AddDays(-31);
            if (MaxChartDate > Settings.LastWeighDate)
            {
                MaxChartDate = Settings.LastWeighDate.AddDays(1);
                MinChartDate = MaxChartDate.AddDays(-31);
            }
            if (MinChartDate < Settings.InitialWeightDate)
            {
                MinChartDate = Settings.InitialWeightDate.AddDays(-5);
            }
            WeekSelectedBorderColor = Color.Default;
            MonthSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            YearSelectedBorderColor = Color.Default;
            CurrentlySelectedGraphTimeline = "month";
        }

        private void ShowYear()
        {
            WaistSizeEnabled = Settings.WaistSizeEnabled;
            MaxChartDate = DateTime.Now.AddDays(1);
            MinChartDate = DateTime.Now.AddDays(-365);
            if (MaxChartDate > Settings.LastWeighDate)
            {
                MaxChartDate = Settings.LastWeighDate.AddDays(5);
                MinChartDate = MaxChartDate.AddDays(-365);
            }
            if (MinChartDate < Settings.InitialWeightDate)
            {
                MinChartDate = Settings.InitialWeightDate.AddDays(-10);
            }
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
            DataFromDatabase = DataFromDatabase.OrderByDescending(x => x.WeighDate.LocalDateTime).ToList();
            WeightList = DataFromDatabase.ToObservableCollection();

            ShowWeek();
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("AllWeightEntriesSorted"))
            {
                WaistSizeEnabled = Settings.WaistSizeEnabled;
                DataFromDatabase = (List<WeightEntry>) parameters["AllWeightEntriesSorted"];
                WeightList = DataFromDatabase.ToObservableCollection();
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