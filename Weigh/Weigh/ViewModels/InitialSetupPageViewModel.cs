using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Weigh.Helpers;
using Weigh.Models;
using Weigh.Extensions;
using Weigh.Behaviors;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Events;

namespace Weigh.ViewModels
{
    /// <summary>
    /// Page will Prompt user for all initial data to begin
    /// 
    /// Inputs:     None
    /// Outputs:    WeightEntry->(Database), AppStateInfo->(AppState.cs)
    /// </summary>
    public class InitialSetupPageViewModel : ViewModelBase
	{
        #region Fields
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private bool _sex;
        public bool Sex
        {
            get { return _sex; }
            set { SetProperty(ref _sex, value); }
        }
        private string _age;
        public string Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }
        private string _heightMajor;
        public string HeightMajor
        {
            get { return _heightMajor; }
            set { SetProperty(ref _heightMajor, value); }
        }
        private string _heightMinor;
        public string HeightMinor
        {
            get { return _heightMinor; }
            set { SetProperty(ref _heightMinor, value); }
        }
        private string _weight;
        public string Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }


        private bool _units;
        public bool Units
        {
            get { return _units; }
            set { SetProperty(ref _units, value); }
        }

        private List<string> _pickerSource;
        public List<string> PickerSource
        {
            get { return _pickerSource; }
            set { SetProperty(ref _pickerSource, value); }
        }

        private string _pickerSelectedItem;
        public string PickerSelectedItem
        {
            get { return _pickerSelectedItem; }
            set { SetProperty(ref _pickerSelectedItem, value); }
        }

        private string _goalWeight;
        public string GoalWeight
        {
            get { return _goalWeight; }
            set { SetProperty(ref _goalWeight, value); }
        }

        private DateTime _goalDate;
        public DateTime GoalDate
        {
            get { return _goalDate; }
            set { SetProperty(ref _goalDate, value); }
        }

        private DateTime _minDate;
        public DateTime MinDate
        {
            get { return _minDate; }
            set { SetProperty(ref _minDate, value); }
        }

        private string _waistSize;
        public string WaistSize
        {
            get { return _waistSize; }
            set { SetProperty(ref _waistSize, value); }
        }

        private SetupInfo _setupInfo;
        public SetupInfo SetupInfo
        {
            get { return _setupInfo; }
            set { SetProperty(ref _setupInfo, value); }
        }

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set { SetProperty(ref _isValid, value); }
        }

        public DelegateCommand SaveInfoCommand { get; set; }
        public DelegateCommand ValidateWeightCommand { get; set; }
        

        private WeightEntry _newWeight;
        #endregion

        #region Constructor
        public InitialSetupPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            Title = "Setup";

            SetupInfo = new SetupInfo();
            MinDate = DateTime.UtcNow.AddDays(10);
            GoalDate = AppState.GoalDate;
            SaveInfoCommand = new DelegateCommand(SaveInfoAsync);
            //ValidateWeightCommand = new DelegateCommand(DoNothing, ValidateWeight);
            // Setting units to default imperial
            Units = true;
            // TODO: get rid of hard coded strings!
            PickerSource = new List<string> { "No Exercise", "Light Exercise", "Moderate Exercise", "Heavy Exercise" };
            //_vWeight = new ValidatableObject<double>();
            AddValidations();
        }
        #endregion

        #region Methods
        private async void SaveInfoAsync()
        {
            AppState.Name = Name;
            AppState.Sex = Sex;
            AppState.Age = Convert.ToInt32(Age);
            AppState.GoalWeight = Convert.ToDouble(GoalWeight);
            AppState.GoalDate = GoalDate;

            AppState.HeightMajor = Convert.ToDouble(HeightMajor);
            AppState.HeightMinor = Convert.ToInt32(HeightMinor);
            AppState.Weight = Convert.ToDouble(Weight);
            AppState.WaistSize = Convert.ToDouble(WaistSize);
            AppState.LastWeight = Convert.ToDouble(Weight);
            AppState.InitialWeight = Convert.ToDouble(Weight);
            
            AppState.LastWeighDate = DateTime.UtcNow;
            AppState.InitialWeightDate = DateTime.UtcNow;
            AppState.Units = Units;
            AppState.PickerSelectedItem = PickerSelectedItem;

            GoalValidation.ValidateGoal();

            // Nav using absolute path so user can't hit the back button and come back here
            _newWeight = new WeightEntry();
            _newWeight.Weight = AppState.Weight;
            _newWeight.WaistSize = AppState.WaistSize;
            await App.Database.SaveWeightAsync(_newWeight);
            await NavigationService.NavigateAsync("Weigh:///NavigatingAwareTabbedPage/MainPage");
        }

        private void DoNothing()
        {

        }

        /*
        private bool Validate()
        {
            //bool isValidWeight = _vWeight.IsValid;

            //return isValidWeight;
            VWeight.IsValid = _vWeight.IsValid;
            return _vWeight.IsValid;
        }

        private bool ValidateWeight ()
        {

            _vWeight.IsValid = _vWeight.Validate();
            VWeight = _vWeight;
            return _vWeight.IsValid;
        }
        */

        private void AddValidations()
        {
            //_vWeight.Validations.Add(new WeightWithinLimitsRule<double> { ValidationMessage = "Weight required." });
        }
        #endregion
    }
}
