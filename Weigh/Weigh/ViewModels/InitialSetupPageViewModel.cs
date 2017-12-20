using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Weigh.ViewModels
{
	public class InitialSetupPageViewModel : ViewModelBase
	{
        public InitialSetupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Setup";
        }
    }
}
