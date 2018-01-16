using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
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
            ea.GetEvent<AddWeightEvent>().Subscribe(HandleNewWeightEntry);
            //ea.GetEvent<UpdateWaistSizeEnabledToGraphEvent>().Subscribe(UpdateWaistSizeEnabled);
            Title = AppResources.GraphPageTitle;
            WeightList = new ObservableCollection<WeightEntry>();
            ChartData = new ObservableCollection<WeightEntry>();
            ShowWeekCommand = new DelegateCommand(ShowWeek);
            ShowMonthCommand = new DelegateCommand(ShowMonth);
            ShowYearCommand = new DelegateCommand(ShowYear);
            //ToggleWeightWaistSizeCommand = new DelegateCommand(ToggleWeightWaistSize);
            ShowDataMarker = true;
            WeekSelectedBorderColor = (Color)Application.Current.Resources["ButtonSelected"];
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

            NewGraphInstance();
            PopulateWeightList();
        }

        #endregion

        #region Fields

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

        private List<WeightEntry> _weightListReversed;

        public List<WeightEntry> WeightListReversed
        {
            get => _weightListReversed;
            set => SetProperty(ref _weightListReversed, value);
        }

        public List<WeightEntry> DataFromDatabase { get; set; }

        public DelegateCommand ShowWeekCommand { get; set; }
        public DelegateCommand ShowMonthCommand { get; set; }
        public DelegateCommand ShowYearCommand { get; set; }
        public DelegateCommand ToggleWeightWaistSizeCommand { get; set; }

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

        private void HandleNewWeightEntry(WeightEntry weight)
        {
            WeightList.Add(weight);
            WeightListReversed = WeightList.ToList();
            WeightListReversed.Reverse();
            WeightListReversed.ToObservableCollection();
            if (CurrentlySelectedGraphTimeline == "week" && ChartData.Count >= 7 ||
                CurrentlySelectedGraphTimeline == "month" && ChartData.Count >= 31 ||
                CurrentlySelectedGraphTimeline == "year" && ChartData.Count >= 365)
                ChartData.RemoveAt(0);
            ChartData.Add(weight);
            NewGraphInstance();
        }

        private void NewGraphInstance()
        {
        }

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
            ChartData = WeightList.Skip(Math.Max(0, WeightList.Count() - 7)).Take(7).ToObservableCollection();
            ShowDataMarker = true;
            WeekSelectedBorderColor = (Color)Application.Current.Resources["ButtonSelected"];
            MonthSelectedBorderColor = Color.Default;
            YearSelectedBorderColor = Color.Default;
            CurrentlySelectedGraphTimeline = "week";
        }

        private void ShowMonth()
        {
            ChartData = WeightList.Skip(Math.Max(0, WeightList.Count() - 31)).Take(31).ToObservableCollection();
            ShowDataMarker = false;
            WeekSelectedBorderColor = Color.Default;
            MonthSelectedBorderColor = (Color)Application.Current.Resources["ButtonSelected"];
            YearSelectedBorderColor = Color.Default;
            CurrentlySelectedGraphTimeline = "month";
        }

        private void ShowYear()
        {
            ChartData = WeightList.Skip(Math.Max(0, WeightList.Count() - 365)).Take(365).ToObservableCollection();
            ShowDataMarker = false;
            WeekSelectedBorderColor = Color.Default;
            MonthSelectedBorderColor = Color.Default;
            YearSelectedBorderColor = (Color)Application.Current.Resources["ButtonSelected"];
            CurrentlySelectedGraphTimeline = "year";
        }

        private async void PopulateWeightList()
        {
            DataFromDatabase = await App.Database.GetWeightsAsync();
            WeightList = DataFromDatabase.ToObservableCollection();
            WeightListReversed = WeightList.ToList();
            WeightListReversed.Reverse();
            WeightListReversed.ToObservableCollection();
            ChartData = DataFromDatabase.Skip(Math.Max(0, WeightList.Count() - 7)).Take(7).ToObservableCollection();
            /*
            // REMOVE WHEN NOT DEBUG
            WeightList.Add(new WeightEntry(173, new DateTime(2017, 8, 11),40));
            WeightList.Add(new WeightEntry(173, new DateTime(2017, 8, 10),39.5));
            WeightList.Add(new WeightEntry(173, new DateTime(2017, 8, 9),38.5));
            WeightList.Add(new WeightEntry(173, new DateTime(2017, 8, 8),37.5));
            WeightList.Add(new WeightEntry(173, new DateTime(2017, 8, 7),36.5));
            WeightList.Add(new WeightEntry(173, new DateTime(2017, 8, 6),35));
            WeightList.Add(new WeightEntry(173, new DateTime(2017, 8, 5),34.7));
            WeightList.Add(new WeightEntry(170, new DateTime(2017, 8, 4),34.6));
            WeightList.Add(new WeightEntry(171, new DateTime(2017, 8, 3),34.5));
            WeightList.Add(new WeightEntry(172, new DateTime(2017, 8, 2),34));
            WeightList.Add(new WeightEntry(173, new DateTime(2017, 8, 1),33.6));
                                                                      
            WeightList.Add(new WeightEntry(174, new DateTime(2017, 7, 18),33.5));
            WeightList.Add(new WeightEntry(175, new DateTime(2017, 7, 17),33.4));
            WeightList.Add(new WeightEntry(176, new DateTime(2017, 7, 16),33.3));
            WeightList.Add(new WeightEntry(177, new DateTime(2017, 7, 15),33.2));
            WeightList.Add(new WeightEntry(178, new DateTime(2017, 7, 14),33.1));
            WeightList.Add(new WeightEntry(179, new DateTime(2017, 7, 13),33));
            WeightList.Add(new WeightEntry(180, new DateTime(2017, 7, 12),32));
            WeightList.Add(new WeightEntry(181, new DateTime(2017, 7, 11),31));
            WeightList.Add(new WeightEntry(182, new DateTime(2017, 7, 10),30));
            /*
            WeightList.Add(new WeightEntry(183, new DateTime(2017, 7, 9)));
            WeightList.Add(new WeightEntry(184, new DateTime(2017, 7, 8)));
            WeightList.Add(new WeightEntry(185, new DateTime(2017, 7, 7)));
            WeightList.Add(new WeightEntry(186, new DateTime(2017, 7, 6)));
            WeightList.Add(new WeightEntry(187, new DateTime(2017, 7, 5)));
            WeightList.Add(new WeightEntry(188, new DateTime(2017, 7, 4)));
            WeightList.Add(new WeightEntry(189, new DateTime(2017, 7, 3)));
            WeightList.Add(new WeightEntry(190, new DateTime(2017, 7, 2)));
            WeightList.Add(new WeightEntry(191, new DateTime(2017, 7, 1)));

            WeightList.Add(new WeightEntry(192, new DateTime(2017, 6, 18)));
            WeightList.Add(new WeightEntry(193, new DateTime(2017, 6, 17)));
            WeightList.Add(new WeightEntry(194, new DateTime(2017, 6, 16)));
            WeightList.Add(new WeightEntry(195, new DateTime(2017, 6, 15)));
            WeightList.Add(new WeightEntry(196, new DateTime(2017, 6, 14)));
            WeightList.Add(new WeightEntry(197, new DateTime(2017, 6, 13)));
            WeightList.Add(new WeightEntry(198, new DateTime(2017, 6, 12)));
            WeightList.Add(new WeightEntry(199, new DateTime(2017, 6, 11)));
            WeightList.Add(new WeightEntry(200, new DateTime(2017, 6, 10)));
            WeightList.Add(new WeightEntry(201, new DateTime(2017, 6, 9)));
            WeightList.Add(new WeightEntry(202, new DateTime(2017, 6, 8)));
            WeightList.Add(new WeightEntry(203, new DateTime(2017, 6, 7)));
            WeightList.Add(new WeightEntry(204, new DateTime(2017, 6, 6)));
            WeightList.Add(new WeightEntry(205, new DateTime(2017, 6, 5)));
            WeightList.Add(new WeightEntry(206, new DateTime(2017, 6, 4)));
            WeightList.Add(new WeightEntry(207, new DateTime(2017, 6, 3)));
            WeightList.Add(new WeightEntry(208, new DateTime(2017, 6, 2)));
            WeightList.Add(new WeightEntry(209, new DateTime(2017, 6, 1)));

            WeightList.Add(new WeightEntry(210, new DateTime(2017, 5, 18)));
            WeightList.Add(new WeightEntry(211, new DateTime(2017, 5, 17)));
            WeightList.Add(new WeightEntry(212, new DateTime(2017, 5, 16)));
            WeightList.Add(new WeightEntry(213, new DateTime(2017, 5, 15)));
            WeightList.Add(new WeightEntry(214, new DateTime(2017, 5, 14)));
            WeightList.Add(new WeightEntry(215, new DateTime(2017, 5, 13)));
            WeightList.Add(new WeightEntry(216, new DateTime(2017, 5, 12)));
            WeightList.Add(new WeightEntry(217, new DateTime(2017, 5, 11)));
            WeightList.Add(new WeightEntry(218, new DateTime(2017, 5, 10)));
            WeightList.Add(new WeightEntry(219, new DateTime(2017, 5, 9)));
            WeightList.Add(new WeightEntry(220, new DateTime(2017, 5, 8)));
            WeightList.Add(new WeightEntry(221, new DateTime(2017, 5, 7)));
            WeightList.Add(new WeightEntry(222, new DateTime(2017, 5, 6)));
            WeightList.Add(new WeightEntry(223, new DateTime(2017, 5, 5)));
            WeightList.Add(new WeightEntry(224, new DateTime(2017, 5, 4)));
            WeightList.Add(new WeightEntry(225, new DateTime(2017, 5, 3)));
            WeightList.Add(new WeightEntry(226, new DateTime(2017, 5, 2)));
            WeightList.Add(new WeightEntry(227, new DateTime(2017, 5, 1)));

            WeightList.Add(new WeightEntry(228, new DateTime(2017, 4, 18)));
            WeightList.Add(new WeightEntry(229, new DateTime(2017, 4, 17)));
            WeightList.Add(new WeightEntry(230, new DateTime(2017, 4, 16)));
            WeightList.Add(new WeightEntry(231, new DateTime(2017, 4, 15)));
            WeightList.Add(new WeightEntry(232, new DateTime(2017, 4, 14)));
            WeightList.Add(new WeightEntry(233, new DateTime(2017, 4, 13)));
            WeightList.Add(new WeightEntry(234, new DateTime(2017, 4, 12)));
            WeightList.Add(new WeightEntry(235, new DateTime(2017, 4, 11)));
            WeightList.Add(new WeightEntry(236, new DateTime(2017, 4, 10)));
            WeightList.Add(new WeightEntry(237, new DateTime(2017, 4, 9)));
            WeightList.Add(new WeightEntry(238, new DateTime(2017, 4, 8)));
            WeightList.Add(new WeightEntry(239, new DateTime(2017, 4, 7)));
            WeightList.Add(new WeightEntry(240, new DateTime(2017, 4, 6)));
            WeightList.Add(new WeightEntry(241, new DateTime(2017, 4, 5)));
            WeightList.Add(new WeightEntry(242, new DateTime(2017, 4, 4)));
            WeightList.Add(new WeightEntry(243, new DateTime(2017, 4, 3)));
            WeightList.Add(new WeightEntry(244, new DateTime(2017, 4, 2)));
            WeightList.Add(new WeightEntry(245, new DateTime(2017, 4, 1)));

            WeightList.Add(new WeightEntry(246, new DateTime(2017, 3, 18)));
            WeightList.Add(new WeightEntry(247, new DateTime(2017, 3, 17)));
            WeightList.Add(new WeightEntry(248, new DateTime(2017, 3, 16)));
            WeightList.Add(new WeightEntry(249, new DateTime(2017, 3, 15)));
            WeightList.Add(new WeightEntry(250, new DateTime(2017, 3, 14)));
            WeightList.Add(new WeightEntry(251, new DateTime(2017, 3, 13)));
            WeightList.Add(new WeightEntry(252, new DateTime(2017, 3, 12)));
            WeightList.Add(new WeightEntry(253, new DateTime(2017, 3, 11)));
            WeightList.Add(new WeightEntry(254, new DateTime(2017, 3, 10)));
            WeightList.Add(new WeightEntry(255, new DateTime(2017, 3, 9)));
            WeightList.Add(new WeightEntry(256, new DateTime(2017, 3, 8)));
            WeightList.Add(new WeightEntry(257, new DateTime(2017, 3, 7)));
            WeightList.Add(new WeightEntry(258, new DateTime(2017, 3, 6)));
            WeightList.Add(new WeightEntry(259, new DateTime(2017, 3, 5)));
            WeightList.Add(new WeightEntry(260, new DateTime(2017, 3, 4)));
            WeightList.Add(new WeightEntry(261, new DateTime(2017, 3, 3)));
            WeightList.Add(new WeightEntry(262, new DateTime(2017, 3, 2)));
            WeightList.Add(new WeightEntry(263, new DateTime(2017, 3, 1)));

            WeightList.Add(new WeightEntry(264, new DateTime(2017, 2, 18)));
            WeightList.Add(new WeightEntry(265, new DateTime(2017, 2, 17)));
            WeightList.Add(new WeightEntry(266, new DateTime(2017, 2, 16)));
            WeightList.Add(new WeightEntry(267, new DateTime(2017, 2, 15)));
            WeightList.Add(new WeightEntry(268, new DateTime(2017, 2, 14)));
            WeightList.Add(new WeightEntry(269, new DateTime(2017, 2, 13)));
            WeightList.Add(new WeightEntry(270, new DateTime(2017, 2, 12)));
            WeightList.Add(new WeightEntry(271, new DateTime(2017, 2, 11)));
            WeightList.Add(new WeightEntry(272, new DateTime(2017, 2, 10)));
            WeightList.Add(new WeightEntry(273, new DateTime(2017, 2, 9)));
            WeightList.Add(new WeightEntry(274, new DateTime(2017, 2, 8)));
            WeightList.Add(new WeightEntry(275, new DateTime(2017, 2, 7)));
            WeightList.Add(new WeightEntry(276, new DateTime(2017, 2, 6)));
            WeightList.Add(new WeightEntry(278, new DateTime(2017, 2, 5)));
            WeightList.Add(new WeightEntry(279, new DateTime(2017, 2, 4)));
            WeightList.Add(new WeightEntry(280, new DateTime(2017, 2, 3)));
            WeightList.Add(new WeightEntry(281, new DateTime(2017, 2, 2)));
            WeightList.Add(new WeightEntry(282, new DateTime(2017, 2, 1)));

            WeightList.Add(new WeightEntry(283, new DateTime(2017, 1, 18)));
            WeightList.Add(new WeightEntry(284, new DateTime(2017, 1, 17)));
            WeightList.Add(new WeightEntry(285, new DateTime(2017, 1, 16)));
            WeightList.Add(new WeightEntry(286, new DateTime(2017, 1, 15)));
            WeightList.Add(new WeightEntry(287, new DateTime(2017, 1, 14)));
            WeightList.Add(new WeightEntry(288, new DateTime(2017, 1, 13)));
            WeightList.Add(new WeightEntry(289, new DateTime(2017, 1, 12)));
            WeightList.Add(new WeightEntry(290, new DateTime(2017, 1, 11)));
            WeightList.Add(new WeightEntry(291, new DateTime(2017, 1, 10)));
            WeightList.Add(new WeightEntry(292, new DateTime(2017, 1, 9)));
            WeightList.Add(new WeightEntry(293, new DateTime(2017, 1, 8)));
            WeightList.Add(new WeightEntry(294, new DateTime(2017, 1, 7)));
            WeightList.Add(new WeightEntry(295, new DateTime(2017, 1, 6)));
            WeightList.Add(new WeightEntry(296, new DateTime(2017, 1, 5)));
            WeightList.Add(new WeightEntry(297, new DateTime(2017, 1, 4)));
            WeightList.Add(new WeightEntry(298, new DateTime(2017, 1, 3)));
            WeightList.Add(new WeightEntry(299, new DateTime(2017, 1, 2)));
            WeightList.Add(new WeightEntry(300, new DateTime(2017, 1, 1)));
            */
        }

        #endregion
    }
}