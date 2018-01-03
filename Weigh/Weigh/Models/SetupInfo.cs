using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Prism.Mvvm;
using Weigh.Helpers;
using Weigh.Validation;

namespace Weigh.Models
{
    public class SetupInfo : ValidatableBase
    {
        private string _name;
        [Required(ErrorMessage = "This field is required")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Minimum 2 chars, max 15 chars")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _age;
        [Required(ErrorMessage = "This field is required")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "Minimum 2 chars, max 3 chars")]
        [CustomValidation(typeof(SetupInfoValidation), "AgeValidation")]
        public string Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }

        private string _heightMajor;
        [Required(ErrorMessage = "This field is required")]
        [StringLength(6, MinimumLength = 1, ErrorMessage = "Minimum 1 chars, max 6 chars")]
        [CustomValidation(typeof(SetupInfoValidation), "HeightMajorValidation")]
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
        [Required(ErrorMessage = "This field is required")]
        [StringLength(4, MinimumLength = 2, ErrorMessage = "Minimum 2 chars, max 4 chars")]
        [CustomValidation(typeof(SetupInfoValidation), "WeightValidation")]
        public string Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }

        private string _waistSize;
        public string WaistSize
        {
            get { return _waistSize; }
            set { SetProperty(ref _waistSize, value); }
        }

        private string _goalWeight;
        public string GoalWeight
        {
            get { return _goalWeight; }
            set { SetProperty(ref _goalWeight, value); }
        }


        private bool _sex;
        public bool Sex
        {
            get { return _sex; }
            set { SetProperty(ref _sex, value); }
        }

        private bool _units;
        public bool Units
        {
            get { return _units; }
            set { SetProperty(ref _units, value);
                AppState.Units = value;
            }
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
    }
}
