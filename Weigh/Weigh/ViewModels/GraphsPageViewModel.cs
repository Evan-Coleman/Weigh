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

        public GraphsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Graph Page";
            MyEntries = new List<Entry>();

            MyEntries.Add(new Entry(237)
            {
                Label = "January",
                ValueLabel = "237",
                Color = SKColor.Parse("#FF1493")
            });

            MyEntries.Add(new Entry(236)
            {
                Label = "January",
                ValueLabel = "236",
                Color = SKColor.Parse("#FF1493")
            });

            MyEntries.Add(new Entry(235)
            {
                Label = "January",
                ValueLabel = "235",
                Color = SKColor.Parse("#00CED1")
            });

            MyEntries.Add(new Entry(234)
            {
                Label = "February",
                ValueLabel = "234",
                Color = SKColor.Parse("#FF1493")
            });

            MyEntries.Add(new Entry(233)
            {
                Label = "February",
                ValueLabel = "233",
                Color = SKColor.Parse("#FF1493")
            });

            MyEntries.Add(new Entry(233)
            {
                Label = "February",
                ValueLabel = "233",
                Color = SKColor.Parse("#00CED1")
            });





            MyChart = new LineChart { Entries = MyEntries };
            MyChart.LabelOrientation = Orientation.Horizontal;
            MyChart.LabelTextSize = 40;
            MyChart.PointSize = 25;
            MyChart.ValueLabelOrientation = Orientation.Horizontal;
            MyChart.PointMode = PointMode.Circle;
        }

    }
}
