using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Prism.Mvvm;
using SQLite;
using Weigh.Helpers;
using Weigh.Localization;
using Weigh.Extensions;
using Weigh.Validation;
using Acr.UserDialogs;

namespace Weigh.Models
{
    public class SettingValsValidated : ValidatableBase
    {
        #region Fields
        private string _age;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(3, MinimumLength = 2, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "AgeLengthValidationErrorMessage")]
        [Range(1, 150, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "AgeValueValidationErrorMessage")]
        public string Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }

        private string _goalWeight;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(7, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "GoalWeightLengthValidationErrorMessage")]
        [Range(0.0, 1000.0, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "GoalWeightValueValidationErrorMessage")]
        public string GoalWeight
        {
            get { return _goalWeight; }
            set { SetProperty(ref _goalWeight, value); }
        }

        private string _heightMajor;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(5, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "HeightMajorLengthValidationErrorMessage")]        
        [CustomValidation(typeof(SetupInfoValidation), "HeightMajorValidation")]
        public string HeightMajor
        {
            get { return _heightMajor; }
            set { SetProperty(ref _heightMajor, value); }
        }

        private string _heightMinor;
        [CustomValidation(typeof(SetupInfoValidation), "HeightMinorValidation")]
        [CustomValidation(typeof(SetupInfoValidation), "HeightMinorLengthValidation")]
        public string HeightMinor
        {
            get { return _heightMinor; }
            set { SetProperty(ref _heightMinor, value); }
        }

        private string _waistSize;
        [CustomValidation(typeof(SetupInfoValidation), "WaistSizeValidation")]
        public string WaistSize
        {
            get { return _waistSize; }
            set { SetProperty(ref _waistSize, value); }
        }

        private string _weight;
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(7, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "WeightLengthValidationErrorMessage")]
        [Range(0.0, 1000.0, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "WeightValueValidationErrorMessage")]
        public string Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }
        #endregion
    }
}
