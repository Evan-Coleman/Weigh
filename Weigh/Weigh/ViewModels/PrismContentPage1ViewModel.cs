using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Weigh.ViewModels
{
	public class PrismContentPage1ViewModel : ViewModelBase
    {
        public PrismContentPage1ViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {

        }
	}
}
