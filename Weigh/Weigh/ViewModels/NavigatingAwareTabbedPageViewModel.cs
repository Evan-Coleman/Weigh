using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Weigh.ViewModels
{
	public class NavigatingAwareTabbedPageViewModel : TabbedPage, INavigatingAware
	{
        public NavigatingAwareTabbedPageViewModel()
        {

        }
        public void OnNavigatingTo(NavigationParameters parameters)
        {
            foreach (var child in Children)
            {
                (child as INavigatingAware)?.OnNavigatingTo(parameters);
                (child?.BindingContext as INavigatingAware)?.OnNavigatingTo(parameters);
            }
        }
    }
}
