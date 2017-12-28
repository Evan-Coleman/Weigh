using System;
using Weigh.ViewModels;
using Weigh.Views;
using DryIoc;
using Prism.DryIoc;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Weigh.Helpers;
using Weigh.Data;

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

        #region App State Variables

        public static bool Sex { get; set; }
        public static int Age { get; set; }
        public static double HeightMajor { get; set; }
        public static int HeightMinor { get; set; }
        public static double Weight { get; set; }
        public static bool Units { get; set; }
        public static string Picker { get; set; }
        public static double LastWeight { get; set; }
        public static double InitialWeight { get; set; }
        public static DateTime InitialWeightDate { get; set; }
        public static DateTime LastWeightDate { get; set; }
        public static double GoalWeight { get; set; }
        public static DateTime GoalDate { get; set; }

        #endregion

        private static WeightDatabase database;

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            // Will load all variables from storage in order to minimize disk time.
            InitializeApplicationState();
            if (Settings.FirstUse == "yes")
            {
                Settings.FirstUse = "no";
                await NavigationService.NavigateAsync("InitialSetupPage");
            }
            else
            {
                // Navigate to main page with main tab activated
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

        public static WeightDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new WeightDatabase(DependencyService.Get<IFileHelper>().GetPath("TodoSQLite.db3"));
                }
                return database;
            }
        }


        public void InitializeApplicationState()
        {
            Sex = Settings.Sex;
            Age = Settings.Age;
            HeightMajor = Settings.HeightMajor;
            HeightMinor = Settings.HeightMinor;
            Weight = Settings.Weight;
            Units = Settings.Units;
            Picker = Settings.PickerSelectedItem;
            LastWeight = Settings.LastWeight;
            InitialWeight = Settings.InitialWeight;
            InitialWeightDate = Settings.InitialWeightDate;
            LastWeightDate = Settings.LastWeighDate;
            GoalWeight = Settings.GoalWeight;
            GoalDate = Settings.GoalDate;
        }
    }
}
