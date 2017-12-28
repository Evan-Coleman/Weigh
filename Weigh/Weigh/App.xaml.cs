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

        private static bool _sex;
        public static bool Sex
        {
            set { Settings.Sex = value; _sex = value; }
            get { return _sex; }
        }

        private static int _age;
        public static int Age
        {
            set { Settings.Age = value; _age = value; }
            get { return _age; }
        }

        private static double _heightMajor;
        public static double HeightMajor
        {
            set { Settings.HeightMajor = value; _heightMajor = value; }
            get { return _heightMajor; }
        }

        private static int _heightMinor;
        public static int HeightMinor
        {
            set { Settings.HeightMinor = value; _heightMinor = value; }
            get { return _heightMinor; }
        }

        private static double _weight;
        public static double Weight
        {
            set { Settings.Weight = value; _weight = value;
            }
            get { return _weight; }
        }
        private static bool _units;
        public static bool Units
        {
            set
            {
                Settings.Units = value; _units = value;
            }
            get { return _units; }
        }

        private static string _pickerSelectedItem;
        public static string PickerSelectedItem
        {
            set
            {
                Settings.PickerSelectedItem = value; _pickerSelectedItem = value;
            }
            get { return _pickerSelectedItem; }
        }

        private static double _lastWeight;
        public static double LastWeight
        {
            set
            {
                Settings.LastWeight = value; _lastWeight = value;
            }
            get { return _lastWeight; }
        }

        private static double _initialWeight;
        public static double InitialWeight
        {
            set
            {
                Settings.InitialWeight = value; _initialWeight = value;
            }
            get { return _initialWeight; }
        }

        private static DateTime _initialWeightDate;
        public static DateTime InitialWeightDate
        {
            set
            {
                Settings.InitialWeightDate = value; _initialWeightDate = value;
            }
            get { return _initialWeightDate; }
        }

        private static DateTime _lastWeighDate;
        public static DateTime LastWeighDate
        {
            set
            {
                Settings.LastWeighDate = value; _lastWeighDate = value;
            }
            get { return _lastWeighDate; }
        }

        private static double _goalWeight;
        public static double GoalWeight
        {
            set
            {
                Settings.GoalWeight = value; _goalWeight = value;
            }
            get { return _goalWeight; }
        }

        private static DateTime _goalDate;
        public static DateTime GoalDate
        {
            set
            {
                Settings.GoalDate = value; _goalDate = value;
            }
            get { return _goalDate; }
        }

        private static string _name;
        public static string Name
        {
            set
            {
                Settings.Name = value; _name = value;
            }
            get { return _name; }
        }

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
            PickerSelectedItem = Settings.PickerSelectedItem;
            LastWeight = Settings.LastWeight;
            InitialWeight = Settings.InitialWeight;
            InitialWeightDate = Settings.InitialWeightDate;
            LastWeighDate = Settings.LastWeighDate;
            GoalWeight = Settings.GoalWeight;
            GoalDate = Settings.GoalDate;
        }
    }
}
