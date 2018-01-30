using Prism.Navigation;

namespace Weigh.ViewModels
{
    /// <summary>
    ///     Page Contains all Prism base functionality
    ///     Inputs:
    ///     Outputs:
    /// </summary>
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        #region Constructor

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #endregion

        #region Fields

        protected INavigationService NavigationService { get; }

        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
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