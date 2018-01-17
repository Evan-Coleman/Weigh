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
using Prism;
using Prism.Ioc;

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
                await NavigationService.NavigateAsync("InitialSetupPage");
            }
            else
            {
                // Navigate to main page with main tab activated
                //await NavigationService.NavigateAsync($"NavigatingAwareTabbedPage?{KnownNavigationParameters.SelectedTab}=MainPage");
                //await NavigationService.NavigateAsync("NavigatingAwareTabbedPage?SelectedTab=MainPage");
                await NavigationService.NavigateAsync("PrismContentPage1");
            }
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
            containerRegistry.RegisterForNavigation<PrismContentPage1>();
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
