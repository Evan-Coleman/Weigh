using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Weigh.Data;
using Weigh.Helpers;
using Weigh.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        private static WeightDatabase _database;

        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        public static WeightDatabase Database
        {
            get
            {
                if (_database == null)
                    _database = new WeightDatabase(DependencyService.Get<IFileHelper>().GetPath("TodoSQLite.db3"));
                return _database;
            }
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            if (Settings.FirstUse == "yes")
                await NavigationService.NavigateAsync("InitialSetupPage");
            else
                await NavigationService.NavigateAsync("NavigatingAwareTabbedPage?SelectedTab=MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<InitialSetupPage>();
            containerRegistry.RegisterForNavigation<NavigatingAwareTabbedPage>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
            containerRegistry.RegisterForNavigation<GraphsPage>();
            containerRegistry.RegisterForNavigation<AddEntryPage>();
        }
    }
}