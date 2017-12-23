using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Microcharts;
using SkiaSharp;
using Entry = Microcharts.Entry;
using System.Collections.ObjectModel;
using Weigh.Models;
using Weigh.Extensions;
using Prism.Events;
using Weigh.Events;

namespace Weigh.ViewModels
{
	public class GraphsPageViewModel : ViewModelBase
	{
        private LineChart _myChart;
        public LineChart MyChart
        {
            get { return _myChart; }
            set { SetProperty(ref _myChart, value); }
        }
        private List<Microcharts.Entry> _myEntries;
        public List<Microcharts.Entry> MyEntries
        {
            get { return _myEntries; }
            set { SetProperty(ref _myEntries, value); }
        }
        private ObservableCollection<WeightEntry> _weightList;
        public ObservableCollection<WeightEntry> WeightList
        {
            get { return _weightList; }
            set { SetProperty(ref _weightList, value); }
        }

        public GraphsPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            ea.GetEvent<AddWeightEvent>().Subscribe(Handled);
            Title = "Graph Page";
            MyEntries = new List<Entry>();
            WeightList = new ObservableCollection<WeightEntry>();
/*
            MyEntries.Add(new Entry(237)
            {
                Label = "January",
                ValueLabel = "237",
            });*/


            MyChart = new LineChart { Entries = MyEntries };
            MyChart.LabelOrientation = Orientation.Horizontal;
            MyChart.LabelTextSize = 40;
            MyChart.PointSize = 25;
            MyChart.ValueLabelOrientation = Orientation.Horizontal;
            MyChart.PointMode = PointMode.Circle;
            MyChart.BackgroundColor = SKColors.Transparent;

            PopulateWeightList();
        }

        private void Handled(WeightEntry weight)
        {
            WeightList.Add(weight);
            MyEntries = new List<Entry>();
            PopulateGraph();
        }

        private async void PopulateWeightList()
        {
            var data = await App.Database.GetWeightsAsync();
            WeightList = data.ToObservableCollection();
            PopulateGraph();
        }

        private void PopulateGraph()
        {
            foreach (WeightEntry entry in WeightList.Skip(Math.Max(0, WeightList.Count() - 10)).Take(10))
            {
                MyEntries.Add(new Entry((float)entry.Weight)
                {
                    Label = entry.WeighDate.DayOfWeek.ToString(),
                    ValueLabel = entry.Weight.ToString()
                });
            }
        }
    }
}
