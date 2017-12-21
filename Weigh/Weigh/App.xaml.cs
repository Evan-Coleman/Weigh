using Weigh.ViewModels;
using Weigh.Views;
using DryIoc;
using Prism.DryIoc;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Weigh.Helpers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Weigh
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            if (Settings.FirstUse == "yes")
            {
                await NavigationService.NavigateAsync("InitialSetupPage");
            }
            else
            {
                await NavigationService.NavigateAsync(
                    $"NavigatingAwareTabbedPage?{KnownNavigationParameters.SelectedTab}=MainPage");
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<InitialSetupPage>();
            Container.RegisterTypeForNavigation<NavigatingAwareTabbedPage>();
            Container.RegisterTypeForNavigation<SettingsPage>();
            Container.RegisterTypeForNavigation<GraphsPage>();
        }
    }
}
