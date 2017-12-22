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
            if (Settings.Sex == false)
            {
                BMR = 66 + (6.2 * Settings.Weight) + (12.7 * ((Settings.HeightMajor * 12) + Settings.HeightMinor)) - (6.76 * Settings.Age);
            }
            else
            {
                BMR = 655.1 + (4.35 * Settings.Weight) + (4.7 * ((Settings.HeightMajor * 12) + Settings.HeightMinor)) - (4.7 * Settings.Age);
            }
            BMR *= 1.375;
        }

        
    }
}
