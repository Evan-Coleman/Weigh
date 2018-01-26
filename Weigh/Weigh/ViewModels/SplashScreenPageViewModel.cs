using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using Weigh.Helpers;
using Weigh.Models;

namespace Weigh.ViewModels
{
	public class SplashScreenPageViewModel : ViewModelBase
	{
	    public SplashScreenPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService)
	        : base(navigationService)
	    {
	        _navigationService = navigationService;

            SettingVals = new SettingVals();
            LatestWeightEntry = new WeightEntry();
            AllWeightEntries = new List<WeightEntry>();
	    }

	    private INavigationService _navigationService;

	    private SettingVals _settingVals;

	    public SettingVals SettingVals
	    {
	        get => _settingVals;
	        set => SetProperty(ref _settingVals, value);
	    }

	    private WeightEntry _latestWeightEntry;
	    public WeightEntry LatestWeightEntry
        {
	        get => _latestWeightEntry;
	        set => SetProperty(ref _latestWeightEntry, value);
	    }

	    private List<WeightEntry> _allWeightEntries;
	    public List<WeightEntry> AllWeightEntries
	    {
	        get => _allWeightEntries;
	        set => SetProperty(ref _allWeightEntries, value);
	    }

        public override async void OnNavigatedTo(NavigationParameters parameters)
	    {
	        if (Settings.FirstUse == "yes")
	        {
	            await NavigationService.NavigateAsync("/InitialSetupPage");
	            return;
	        }

	        LatestWeightEntry = await App.Database.GetLatestWeightasync();
            SettingVals.InitializeSettingVals();

	        // To ensure latest weight is displayed we will set it here
	        SettingVals.Weight = LatestWeightEntry.Weight;
	        SettingVals.LastWeighDate = LatestWeightEntry.WeighDate;
	        Settings.Weight = LatestWeightEntry.Weight;
	        Settings.LastWeighDate = LatestWeightEntry.WeighDate;

            if (SettingVals.ValidateGoal() == false)
	            SettingVals.SaveSettingValsToDevice();

	        AllWeightEntries = await App.Database.GetWeightsAsync();
            AllWeightEntries = AllWeightEntries.OrderByDescending(x => x.WeighDate).ToList();

            var p = new NavigationParameters
            {
                { "SettingVals", SettingVals },
                { "LatestWeightEntry", LatestWeightEntry },
                { "AllWeightEntriesSorted", AllWeightEntries }
            };

	        //Task.Delay(5000);
            await NavigationService.NavigateAsync("/NavigationPage/NavigatingAwareTabbedPage", p);
        }
    }
}
