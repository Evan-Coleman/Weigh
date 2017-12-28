using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Weigh.Models;
using Weigh.Extensions;
using Prism.Events;
using Weigh.Events;
using Syncfusion.RangeNavigator.XForms;
using Syncfusion.SfChart.XForms;
using Weigh.Helpers;

namespace Weigh.ViewModels
{
	public class GraphsPageViewModel : ViewModelBase
	{
        private ObservableCollection<WeightEntry> _weightList;
        public ObservableCollection<WeightEntry> WeightList
        {
            get { return _weightList; }
            set { SetProperty(ref _weightList, value); }
        }

        private ObservableCollection<WeightEntry> _chartData;
        public ObservableCollection<WeightEntry> ChartData
        {
            get { return _chartData; }
            set { SetProperty(ref _chartData, value); }
        }

      

        public GraphsPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            ea.GetEvent<AddWeightEvent>().Subscribe(Handled);
            Title = "Graph Page";
            WeightList = new ObservableCollection<WeightEntry>();
            ChartData = new ObservableCollection<WeightEntry>();
            SfDateTimeRangeNavigator rangeNavigator = new SfDateTimeRangeNavigator();
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

        private void Handled(WeightEntry weight)
        {
            WeightList.Add(weight);
            ChartData.RemoveAt(0);
            ChartData.Add(weight);
            NewGraphInstance();
        }
        
        private void NewGraphInstance()
        {
            


        }

        private async void PopulateWeightList()
        {
            var data = await App.Database.GetWeightsAsync();
            //WeightList = data.ToObservableCollection();


            // REMOVE WHEN NOT DEBUG
            WeightList.Add(new WeightEntry(237, new DateTime(2017, 12, 1)));
            WeightList.Add(new WeightEntry(236, new DateTime(2017, 12, 2)));
            WeightList.Add(new WeightEntry(235, new DateTime(2017, 12, 3)));
            WeightList.Add(new WeightEntry(234, new DateTime(2017, 12, 4)));
            WeightList.Add(new WeightEntry(233, new DateTime(2017, 12, 5)));
            WeightList.Add(new WeightEntry(232, new DateTime(2017, 12, 6)));
            WeightList.Add(new WeightEntry(231, new DateTime(2017, 12, 7)));
            WeightList.Add(new WeightEntry(230, new DateTime(2017, 12, 8)));
            WeightList.Add(new WeightEntry(229, new DateTime(2017, 12, 9)));
            WeightList.Add(new WeightEntry(228, new DateTime(2017, 12, 10)));
            WeightList.Add(new WeightEntry(227, new DateTime(2017, 12, 11)));
            WeightList.Add(new WeightEntry(226, new DateTime(2017, 12, 12)));
            WeightList.Add(new WeightEntry(225, new DateTime(2017, 12, 13)));
            WeightList.Add(new WeightEntry(224, new DateTime(2017, 12, 14)));
            WeightList.Add(new WeightEntry(222, new DateTime(2017, 12, 15)));
            WeightList.Add(new WeightEntry(222, new DateTime(2017, 12, 16)));
            WeightList.Add(new WeightEntry(221, new DateTime(2017, 12, 17)));
            WeightList.Add(new WeightEntry(220, new DateTime(2017, 12, 18)));

            ChartData = WeightList.Skip(Math.Max(0, WeightList.Count() - 7)).Take(7).ToObservableCollection();
        }
    }
}
