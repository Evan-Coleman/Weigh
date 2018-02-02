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

            WeekSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            CurrentlySelectedGraphTimeline = "week";

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

        private ObservableCollection<WeightEntry> _chartData;

        public ObservableCollection<WeightEntry> ChartData
        {
            get => _chartData;
            set => SetProperty(ref _chartData, value);
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

        public string CurrentlySelectedGraphTimeline { get; set; }

        public List<WeightEntry> DataFromDatabase { get; set; }

        #endregion

        #region Methods

        private void ShowWeek()
        {
            ChartData = WeightList.Take(7).ToObservableCollection();
            WeekSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            MonthSelectedBorderColor = Color.Default;
            YearSelectedBorderColor = Color.Default;
            CurrentlySelectedGraphTimeline = "week";
        }

        private void ShowMonth()
        {
            ChartData = WeightList.Take(31).ToObservableCollection();
            WeekSelectedBorderColor = Color.Default;
            MonthSelectedBorderColor = (Color) Application.Current.Resources["ButtonSelected"];
            YearSelectedBorderColor = Color.Default;
            CurrentlySelectedGraphTimeline = "month";
        }

        private void ShowYear()
        {
            ChartData = WeightList.Take(365).ToObservableCollection();
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