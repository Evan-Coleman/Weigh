using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using Weigh.Helpers;

namespace Weigh.ViewModels
{
	public class SplashScreenPageViewModel : ViewModelBase
	{
	    public SplashScreenPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService)
	        : base(navigationService)
	    {
	        _navigationService = navigationService;
	    }

	    private INavigationService _navigationService;

	    public override async void OnNavigatedTo(NavigationParameters parameters)
	    {
            // TODO: Implement any initialization logic you need here. Example would be to handle automatic user login

            // Simulated long running task. You should remove this in your app.
	        //await Task.Delay(4000);

            // After performing the long running task we perform an absolute Navigation to remove the SplashScreen from
            // the Navigation Stack.
            if (Settings.FirstUse == "yes")
	            await NavigationService.NavigateAsync("/InitialSetupPage");
	        else
	            await NavigationService.NavigateAsync("/NavigationPage/NavigatingAwareTabbedPage");
        }
    }
}
