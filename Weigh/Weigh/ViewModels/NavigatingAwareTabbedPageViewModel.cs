using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Weigh.ViewModels
{
    /// <summary>
    /// Page Contains all pages in tabs
    /// 
    /// Inputs:     
    /// Outputs:    
    /// </summary>
    public class NavigatingAwareTabbedPageViewModel : TabbedPage, INavigatingAware
	{
        #region Fields
        #endregion

        #region Constructor
        public NavigatingAwareTabbedPageViewModel()
        {

        }
        #endregion

        #region Methods
        public void OnNavigatingTo(INavigationParameters parameters)
        {
            foreach (var child in Children)
            {
                (child as INavigatingAware)?.OnNavigatingTo(parameters);
                (child?.BindingContext as INavigatingAware)?.OnNavigatingTo(parameters);
            }
        }
        #endregion
    }
}
