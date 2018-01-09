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
using Weigh.Models;
using System.Collections.Generic;
using Weigh.Localization;

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

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            // Will load all variables from storage in order to minimize disk time.
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
                if (_database == null)
                {
                    _database = new WeightDatabase(DependencyService.Get<IFileHelper>().GetPath("TodoSQLite.db3"));
                }
                return _database;
            }
        }
    }
}
