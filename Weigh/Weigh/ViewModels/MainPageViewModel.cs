using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigh.Helpers;

namespace Weigh.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public double BMI { get; set; }
        public double BMR { get; set; }

        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Page";
            BMI = (Settings.Weight / Math.Pow(((Settings.HeightMajor * 12) + Settings.HeightMinor), 2)) * 703;

            // BMR based on equations at https://en.wikipedia.org/wiki/Harris%E2%80%93Benedict_equation
            // According to http://www.exercise4weightloss.com/bmr-calculator.html
            // -- Go 1000 calories lower than this calculation to lose 2 pounds a week which is the max advisable
            if (Settings.Sex == false)
            {
                BMR = 66 + (6.2 * Settings.Weight) + (12.7 * ((Settings.HeightMajor * 12) + Settings.HeightMinor)) - (6.76 * Settings.Age);
            }
            else
            {
                BMR = 655.1 + (4.35 * Settings.Weight) + (4.7 * ((Settings.HeightMajor * 12) + Settings.HeightMinor)) - (4.7 * Settings.Age);
            }
            if (Settings.PickerSelectedItem == "No Exercise")
            {
                BMR *= 1.2;
            }
            if (Settings.PickerSelectedItem == "Light Exercise")
            {
                BMR *= 1.375;
            }
            if (Settings.PickerSelectedItem == "Moderate Exercise")
            {
                BMR *= 1.55;
            }
            if (Settings.PickerSelectedItem == "Heavy Exercise")
            {
                BMR *= 1.725;
            }
        }

        
    }
}
