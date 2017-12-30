using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Weigh.ViewModels
{
    /// <summary>
    /// Page Contains all Prism base functionality
    /// 
    /// Inputs:    
    /// Outputs:               
    /// </summary>
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        #region Fields
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion

        #region Constructor
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        #endregion

        #region Methods
        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        public virtual void Destroy()
        {
            
        }
        #endregion
    }
}
